#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app






FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["DiabloII.Items.Api/DiabloII.Items.Api.csproj", "DiabloII.Items.Api/"]
COPY ["DiabloII.Items/DiabloII.Items.Generator.csproj", "DiabloII.Items/"]
COPY ["DiabloII.Items.Reader/DiabloII.Items.Reader.csproj", "DiabloII.Items.Reader/"]
RUN dotnet restore "DiabloII.Items.Api/DiabloII.Items.Api.csproj"
COPY . .
WORKDIR "/src/DiabloII.Items.Api"
RUN dotnet build "DiabloII.Items.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DiabloII.Items.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DiabloII.Items.Api.dll"]

CMD ASPNETCORE_URLS=http://*:$PORT dotnet DiabloII.Items.Api.dll