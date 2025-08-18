FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MyRecipeBook.WebApi/MyRecipeBook.WebApi.csproj", "MyRecipeBook.WebApi/"]
RUN dotnet restore "MyRecipeBook.WebApi/MyRecipeBook.WebApi.csproj"
COPY . .
WORKDIR "/src/MyRecipeBook.WebApi"
RUN dotnet build "./MyRecipeBook.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MyRecipeBook.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyRecipeBook.WebApi.dll"]
