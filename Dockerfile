FROM node:20.18.0-alpine3.20 AS webbuild
WORKDIR /src/client-web

COPY client-web/package.json ./
# COPY client-web/package-lock.json ./

RUN --mount=type=cache,target=/root/.npm npm install

COPY client-web/ ./
RUN npm run build

# --- Build .NET API ---
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore EPS.Discounts.sln
RUN dotnet publish src/EPS.Discounts.API/EPS.Discounts.API.csproj -c Release -o /app/publish

# --- Runtime ---
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish ./
# Copiamos el artefacto web desde el stage webbuild al wwwroot del runtime
COPY --from=webbuild /src/client-web/dist/ ./wwwroot/web/
ENV ASPNETCORE_URLS=http://0.0.0.0:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "EPS.Discounts.API.dll"]
