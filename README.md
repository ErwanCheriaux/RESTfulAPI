# MountainBike
Sandbox **REST API** in **C#** with **.Net Core 7**.  
Save data with **MongoDB** (**Docker container**).  
Developed with **Visual Studio Code**.  

MountainBike project is built into a **Docker image** and orchestrated with **Kubernetes**.  
**Loadbalancing** tested with Postman (hint: disable keep-alive connection header).

## CLI
Create a new ASP.NET Core Web API project template
```console
dotnet new webapi -n MountainBike
```

If the solution structure changes, delete .sln file and regenerate it with the corresponding .csproj
```console
dotnet new sln -n MountainBike
dotnet sln add .\MountainBike.Api\
```

To avoid the following warning "The selected launch configuration is configured to launch a web browser but no trusted development certificate was found. Create a trusted self-signed certificate?"
```console
dotnet dev-certs https --trust
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

Build the MountainBike project into a docker image
```console
docker build -t erwancheriaux/mountainbike:v1 .
```

Run the MountainBike image into a docker container at http://localhost:8080/
```console
docker network create mountainbikenetwork
docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db -e MONGO_INITDB_ROOT_USERNAME=mongoadmin -e MONGO_INITDB_ROOT_PASSWORD=mongoadmin --network=mountainbikenetwork mongo
docker run -it --rm -p 8080:80 -e MongoDBSettings:Host=mongo -e MongoDBSettings:Password=mongoadmin --network=mountainbikenetwork erwancheriaux/mountainbike:v1
```

Kubernetes secretes
```console
kubectl create secret generic mountainbike-secrets --from-literal=mongodb-password='mongoadmin'
```

Kubernetes deployment and service
```console
cd .\Kubernetes\
kubectl apply -f .\mongodb.yaml
kubectl get statefulset
kubectl apply -f .\MountainBike.yaml
kubectl get deployments

kubectl get pods
kubectl logs <pod name>
```

Kubernetes persistent terminal display
```console
kubectl get pods -w
kubectl logs <pod name> -f
```

Kubernetes scale
```console
kubectl scale deployments/mountainbike-deployment --replicas=3
```