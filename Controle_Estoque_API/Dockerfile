FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Controle_Estoque_API/Controle_Estoque_API.csproj", "Controle_Estoque_API/"]
RUN dotnet restore "Controle_Estoque_API/Controle_Estoque_API.csproj"
COPY . .
WORKDIR "/src/Controle_Estoque_API"
RUN dotnet build "Controle_Estoque_API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Controle_Estoque_API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Controle_Estoque_API.dll"]
