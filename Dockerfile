# Runtime image (recommended non-alpine)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Create non-root user
RUN addgroup --system appgroup \
    && adduser --system --ingroup appgroup appuser

EXPOSE 8080


# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ProductApplication.csproj ./
RUN dotnet restore ProductApplication.csproj

COPY . .
RUN dotnet publish ProductApplication.csproj \
    -c Release \
    -o /app/publish \
    --no-restore


# Final image
FROM base AS final
WORKDIR /app

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

COPY --from=build /app/publish .

RUN chown -R appuser:appgroup /app
USER appuser

ENTRYPOINT ["dotnet", "ProductApplication.dll"]