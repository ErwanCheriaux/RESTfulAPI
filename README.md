# RESTful API
Sandbox **REST API** in **C#** with **.Net Core 7**.  
Save data with **MongoDB** (**Docker container**).  
Developed with **Visual Studio Code**.

## CLI
Create a new ASP.NET Core Web API project template
```console
dotnet new webapi -n RESTfulAPI
```

Add the MongoDB .NET driver and healthcheck to your .NET project
```console
dotnet add package MongoDB.Driver
dotnet add package AspNetCore.HealthChecks.MongoDB
```

Run a MongoDB container in Docker with a mongodbdata volume and mongoadmin credentials
```console
docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db -e MONGO_INITDB_ROOT_USERNAME=mongoadmin -e MONGO_INITDB_ROOT_PASSWORD=mongoadmin mongo
```

Save MongoDB password into .Net Secret Manager 
```console
dotnet user-secrets init
dotnet user-secrets set MongoDBSettings:Password mongoadmin
```