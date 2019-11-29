FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["DartLeague.Web/DartLeague.Web.csproj", "DartLeague.Web/"]
RUN dotnet restore "DartLeague.Web/DartLeague.Web.csproj"
COPY . .
WORKDIR "/src/DartLeague.Web"
RUN dotnet build "DartLeague.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DartLeague.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DartLeague.Web.dll"]