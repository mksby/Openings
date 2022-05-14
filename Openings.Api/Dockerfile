FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Openings.Api/Openings.Api.csproj", "Openings.Api/"]
RUN dotnet restore "Openings.Api/Openings.Api.csproj"
COPY . .
WORKDIR "/src/Openings.Api"
RUN dotnet build "Openings.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Openings.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Openings.Api.dll"]
