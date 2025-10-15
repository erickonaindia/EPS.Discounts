using EPS.Discounts.Application.Interfaces;
using EPS.Discounts.Domain.Entities;
using EPS.Discounts.Domain.Enums;
using System.Security.Cryptography;

namespace EPS.Discounts.Application.Services;

public sealed class DiscountService : IDiscountService
{
    private static readonly char[] Alphabet = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789".ToCharArray();
    private const int MaxBatch = 2000;
    private readonly IDiscountRepository _repo;
    private readonly IUnitOfWork _uow;

    public DiscountService(IDiscountRepository repo, IUnitOfWork uow)
    { 
        _repo = repo; 
        _uow = uow; 
    }

    public async Task<bool> GenerateAsync(int count, int length, CancellationToken ct = default)
    {
        if (count <= 0 || count > MaxBatch)
            return false;
        if (length < 7 || length > 8)
            return false;
        
        var codes = new List<DiscountCode>(count);
        var set = new HashSet<string>(StringComparer.Ordinal);
        
        for (int i = 0; i < count; i++)
        {
            string code;
            do
            {
                code = CreateCode(length);
            } 
            while (!set.Add(code));

            codes.Add(new DiscountCode { Id = Guid.NewGuid(), Code = code });
        }
        await _repo.BulkInsertAsync(codes, ct);
        await _uow.SaveChangesAsync(ct);
        
        return true;
    }

    public async Task<UseCodeResult> UseAsync(string code, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(code))
            return UseCodeResult.Invalid;
        
        var entity = await _repo.GetByCodeAsync(code, ct);
        if (entity is null)
            return UseCodeResult.NotFound;
        if (entity.IsUsed)
            return UseCodeResult.AlreadyUsed;
        
        entity.IsUsed = true;
        entity.UsedUtc = DateTime.UtcNow;
        
        await _repo.MarkUsedAsync(entity, ct);
        await _uow.SaveChangesAsync(ct);
        
        return UseCodeResult.Success;
    }

    private static string CreateCode(int length)
    {
        Span<byte> b = stackalloc byte[length];
        RandomNumberGenerator.Fill(b);
        var chars = new char[length];
        
        for (int i = 0; i < length; i++)
            chars[i] = Alphabet[b[i] % Alphabet.Length];
        
        return new string(chars);
    }
}