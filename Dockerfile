# # Étape 1 : Image d'exécution
# FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# WORKDIR /app
# EXPOSE 5000

# # Étape 2 : Image de build
# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# WORKDIR /src
# # Copier le fichier projet et restaurer les dépendances
# COPY ["Labo_Cts_backend.csproj", "./"]
# RUN dotnet restore "./Labo_Cts_backend.csproj"
# # Copier l'ensemble des fichiers sources
# COPY ["./", "./"]
# WORKDIR "/src/"
# # Compiler l'application en mode Release
# RUN dotnet build "Labo_Cts_backend.csproj" -c Release -o /app/build

# # Étape 3 : Publication
# FROM build AS publish
# RUN dotnet publish "Labo_Cts_backend.csproj" -c Release -o /app/publish

# # Étape 4 : Construction de l'image finale
# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "Labo_Cts_backend.dll"]

# Utilisation de l'image officielle .NET SDK pour la compilation
# Utiliser l'image .NET SDK pour la compilation
# Étape 1 : Build
# Étape 1 : Utiliser l'image .NET SDK pour la compilation
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app

EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ["./Labo_Cts_backend.csproj", "./"]

RUN dotnet restore "./Labo_Cts_backend.csproj"

COPY ["./", "./"]



WORKDIR "/src/"

RUN dotnet build "Labo_Cts_backend.csproj" -c Release -o /app/build

FROM build AS publish

RUN dotnet publish "Labo_Cts_backend.csproj" -c Release -o /app/publish

FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Labo_Cts_backend.dll"]





