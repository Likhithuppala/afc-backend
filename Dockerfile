# Stage 1: Base Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Stage 2: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# 1. Copy ALL csproj files first (This optimizes Docker layer caching)
COPY ["afcbackend/afcbackend.csproj", "afcbackend/"]
COPY ["Afc.DTOs/Afc.DTOs.csproj", "Afc.DTOs/"]
COPY ["Afc.Core/Afc.Core.csproj", "Afc.Core/"]
COPY ["Business/Business.csproj", "Business/"]
COPY ["Repository/Repository.csproj", "Repository/"]

# 2. Restore dependencies
RUN dotnet restore "./afcbackend/afcbackend.csproj"

# 3. Copy the entire source code
COPY . .

# 4. Build the Web project
WORKDIR "/src/afcbackend"
RUN dotnet build "./afcbackend.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Stage 3: Publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./afcbackend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Stage 4: Final Production Image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "afcbackend.dll"]