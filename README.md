# EPS.Discounts gRPC + SignalR + Vite CLient

## Docker
```bash
docker compose up --build
```
- API gRPC/gRPC-Web: http://localhost:8080
- Embedded web client: http://localhost:8080/web/


## EF Core Migrations
- An initial migration was included in `EPS.Discounts.Infrastructure/Migrations`.
- The API applies `Database.Migrate()` at the start.
- If you change the model, generate new migrations with:
  ```bash
  dotnet tool restore
  dotnet ef migrations add <Name> -p src/EPS.Discounts.Infrastructure -s src/EPS.Discounts.API
  ```
