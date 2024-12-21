FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

COPY "Domain/UsersService.Domain.Core/*.csporj" "Domain/UsersService.Domain.Core/"
COPY "Domain/UsersService.Domain.Interfaces/*.csporj" "Domain/UsersService.Domain.Interfaces/"
COPY "Infrastructure/UsersService.Infrastructure.Data/*.csporj" "Infrastructure/UsersService.Infrastructure.Data/"
COPY "Infrastructure/UsersService.Infrastructure.Services/*.csporj" "Infrastructure/UsersService.Infrastructure.Services/"
COPY "Services/UsersService.Services.Interfaces/*.csproj" "Services/UsersService.Services.Interfaces/"
COPY "API/*.csproj" "API/"
RUN dotnet restore "API/API.csproj"

COPY "Domain/UsersService.Domain.Core/" "Domain/UsersService.Domain.Core/"
COPY "Domain/UsersService.Domain.Interfaces/" "Domain/UsersService.Domain.Interfaces/"
COPY "Infrastructure/UsersService.Infrastructure.Data/" "Infrastructure/UsersService.Infrastructure.Data/"
COPY "Infrastructure/UsersService.Infrastructure.Services/" "Infrastructure/UsersService.Infrastructure.Services/"
COPY "Services/UsersService.Services.Interfaces/" "Services/UsersService.Services.Interfaces/"
COPY "API/" "API/"
WORKDIR "/source/API"
RUN dotnet publish -o /app

FROM mcr.microsoft.com/dotnet/nightly/aspnet:8.0
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=build /app .
ENTRYPOINT ["./API"]