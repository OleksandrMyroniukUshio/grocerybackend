FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build
WORKDIR /src
COPY ["groceries-api.csproj", "./"]
RUN dotnet restore "groceries-api.csproj"

COPY . .
WORKDIR "/src/."
RUN dotnet build "groceries-api.csproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish "groceries-api.csproj" -c Release -o /app/publish 

FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "groceries-api.dll"]
