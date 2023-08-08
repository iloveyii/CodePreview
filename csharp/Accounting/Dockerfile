
FROM mcr.microsoft.com/dotnet/sdk:7.0 as build
WORKDIR /source
COPY . .

RUN dotnet restore "./Server/Accounting.Server.csproj" --disable-parallel
RUN dotnet publish "./Server/Accounting.Server.csproj" -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/sdk:7.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 3000
ENTRYPOINT [ "dotnet", "./Accounting.Server.dll", "--urls", "http://0.0.0.0:3000" ]

# Build : docker build -t Accounting .
# Run : docker run --name Accounting -d -p 5005:3000 --rm Accounting
# SSH to container: docker exec -ti blazorapp bash