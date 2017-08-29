[![Stories in Ready](https://badge.waffle.io/maxrgus/alfrek-core.png?label=ready&title=Ready)](http://waffle.io/maxrgus/alfrek-core)
# Alfrek Core

Readme work in progress.

## Useful commands
### Run the app after cloning
Restore packages:<br>
```dotnet restore```


Add your connectionString in appsettings.Development.json
```javascript
"ConnectionStrings": {
    "Default": "" // Add your localhost string in appsettings.Development
  }
```
Set environment to Development
- On Mac/Linux<br>
```export ASPNETCORE_ENVIRONMENT=Development```
- On Windows<br>
```set ASPNETCORE_ENVIRONMENT=Development```


Create and/or update the database<br>
```dotnet ef database update``` (Make sure SQL Server is running)

Run the app<br>
```dotnet run```

### Start SQLServer in Docker
```
sudo docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=YOURPASSWORD' -p 1433:1433 -v /usr/docker/alfrek:/var/opt/mssql -m 4g --memory-swap 0 -d microsoft/mssql-server-linux
```

### Check Docker containers
```sudo docker ps```

# Contributors
