#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ConnectionStrings:DbConnection="Server=db-server;Database=aspnet-restAPI-test;User Id=ENVID;Password=ENVPW;Trusted_Connection=False;MultipleActiveResultSets=true"

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["restAPI/restAPI.csproj", "restAPI/"]
RUN dotnet restore "restAPI/restAPI.csproj"
COPY . .
WORKDIR "/src/restAPI"
RUN dotnet build "restAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "restAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "restAPI.dll"]
