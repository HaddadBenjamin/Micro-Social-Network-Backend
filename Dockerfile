# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
# WORKDIR /source
	
# copy csproj and restore as distinct layers
COPY *.sln .
COPY */*.csproj ./aspnetapp/
RUN dotnet restore #  Restaure les dépendances d’un projet.
	
# copy everything else and build app
COPY */. ./aspnetapp/
WORKDIR /aspnetapp
RUN dotnet publish -c release -o /app --no-restore # Publie l’application et ses dépendances dans un dossier pour le déploiement sur un système d’hébergement.
# -c release : configuration de release (de production) : comment faire pour publier en test, hom, prod de façon éxplicite ?
# -o /app : spécifie le chemin d’accès du répertoire de sortie.
# --no-restore : n'effectue pas de restauration implicite à l’exécution de la commande.
	
# final stage/image
FROM mcr.microsoft.com/dotnet/core/sdk:3.1
WORKDIR /app
COPY --from=build /app ./ # Logiquement on ne devrait avoir besoin que de ça, rien d'autre, donc j'ai l'impression que mon image est lourde pour rien là.
ENTRYPOINT ["dotnet", "aspnetapp.dll"] # La dll ne devrait pas être bonne, faire la commande et voir ce que ça me génère.
		
# Comment je m'assure que c'est bien le projet DiabloII.Application qui est runné ?