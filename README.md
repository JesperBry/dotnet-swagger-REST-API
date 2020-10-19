[![Build status](https://github.com/JesperBry/dotnet%2Dswagger%2DREST%2DAPI/workflows/dotnet%2Dcore.yml/badge.svg)](https://github.com/JesperBry/dotnet%2Dswagger%2DREST%2DAPI/actions)
# dotnet-swagger-REST-API

## Creating local HTTPS certificate
```
dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\restAPI.pfx -p <password>
dotnet dev-cert https --trust
dotnet user-secrets set "Kestrel:Certificates:Development:Password" "<password>"
```

## Building with Docker-compose

Make sure docker is installed and that a local HTTPS certificate is added to user-secrets

Then run:
```
docker-compose build
docker-compose up
```

Available on https://localhost:8001/swagger/index.html/

## Run without Docker
```
cd /restAPI
dotnet run
```
Available on https://localhost:5001/swagger/index.html/
