FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY DevStore.Api/DevStore.Api.csproj DevStore.Api/
COPY Infrastructure/DevStore.Infrastructure.csproj Infrastructure/
COPY Domain/DevStore.Domain.csproj Domain/
RUN dotnet restore DevStore.Api/DevStore.Api.csproj
COPY . .
WORKDIR /src/DevStore.Api
RUN dotnet build DevStore.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish DevStore.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DevStore.Api.dll"]
