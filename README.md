# RESTful API
Sandbox **REST API** in **C#** with **.Net Core 7**.  
Save data with **MongoDB** (**Docker container**).  
Developed with **Visual Studio Code**.

## CLI
Create a new ASP.NET Core Web API project template
```console
dotnet new webapi -n RESTfulAPI
```

Add the MongoDB .NET driver as a NuGet package to your .NET project
```console
dotnet add package MongoDB.Driver
```

Run a MongoDB container in Docker
```console
docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db mongo
```