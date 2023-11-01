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

Build the RESTfulAPI project into a docker image
```console
docker build -t restfulapi:v1 .
```

Run the RESTfulAPI image into a docker container at http://localhost:8080/
```console
docket network create restfulapinetwork
docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db -e MONGO_INITDB_ROOT_USERNAME=mongoadmin -e MONGO_INITDB_ROOT_PASSWORD=mongoadmin --network=restfulapinetwork mongo
docker run -it --rm -p 8080:80 -e MongoDBSettings:Host=mongo -e MongoDBSettings:Password=mongoadmin --network=restfulapinetwork restfulapi:v1
```