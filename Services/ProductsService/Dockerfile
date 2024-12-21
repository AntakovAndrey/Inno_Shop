FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

COPY "Domain/ProductsService.Domain.Core/*.csporj" "Domain/ProductsService.Domain.Core/"

COPY "API/*.csproj" "API/"
RUN dotnet restore "API/API.csproj"

COPY "Domain/ProductsService.Domain.Core/" "Domain/ProductsService.Domain.Core/"

COPY "API/" "API/"
WORKDIR "/source/API"
RUN dotnet publish -o /app

FROM mcr.microsoft.com/dotnet/nightly/aspnet:8.0
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=build /app .
ENTRYPOINT ["./API"]