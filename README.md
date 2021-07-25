# Shopping App using .NET 5.0 microservices architecture

The app consists of the following:

**Catalog microservice**
 - ASP.NET Core Web API application
 - REST API principles, CRUD operations
 - MongoDB database connection and containerization
 - Repository Pattern Implementation
 - Swagger Open API implementation

**Basket microservice**

 - ASP.NET Web API application
 - REST API principles, CRUD operations
 - Redis database connection and containerization
 - Consume Discount Grpc Service for inter-service sync communication to calculate product final price
 - Publish BasketCheckout Queue with using MassTransit and RabbitMQ

**Discount microservice**

 - ASP.NET Grpc Server application
 - Highly Performant inter-service gRPC Communication with Basket Microservice
 - Exposed Grpc Services using Protobuf messages
 - Used Dapper for micro-orm implementation to simplify data access and ensure high performance
 - PostgreSQL database connection and containerization

**Ordering Microservice**
 - Implementing DDD, CQRS, and Clean Architecture with using Best Practices
 - Developed CQRS with using MediatR, FluentValidation and AutoMapper packages
 - Consumed RabbitMQ BasketCheckout event queue with using MassTransit-RabbitMQ Configuration
 - SqlServer database connection and containerization
 - Using Entity Framework Core ORM and auto migrate to SqlServer when application startup

**API Gateway Ocelot Microservice**
 - Implemented API Gateways with Ocelot
 - Sample microservices/containers to reroute through the API Gateways
 - Run multiple different API Gateway/BFF container types
 - The Gateway aggregation pattern in Shopping.Aggregator

**Ancillary Containers**
  - Use Portainer for Container lightweight management UI which allows you to easily manage your different Docker environments
  - pgAdmin PostgreSQL Tools feature rich Open Source administration and development platform for PostgreSQL

**Docker Compose establishment with all microservices on docker**
  - Containerization of microservices
  - Containerization of databases
  - Override Environment variables


**Microservices Communication**
 - Sync inter-service gRPC Communication
 - Async Microservices Communication with RabbitMQ Message-Broker Service
 - Using RabbitMQ Publish/Subscribe Topic Exchange Model
 - Using MassTransit for abstraction over RabbitMQ Message-Broker system
 - Publishing BasketCheckout event queue from Basket microservices and Subscribing this event from Ordering microservices
 - Create RabbitMQ EventBus.Messages library and add references Microservices

**WebUI ShoppingApp Microservice**
 - ASP.NET Core Web Application with Bootstrap 4 and Razor template
 - Call Ocelot APIs with HttpClientFactory and Polly


***-- To Do***
**Microservices Cross-Cutting Implementations**
 - Implementing Centralized Distributed Logging with Elastic Stack (ELK);  Elasticsearch, Logstash, Kibana and SeriLog for Microservices
 - Use the HealthChecks feature in back-end ASP.NET microservices
 - Using Watchdog in separate service that can watch health and load across services, and report health about the microservices by querying the HealthChecks

**Microservices Resilience Implementations**
 - Making Microservices more resilient Use IHttpClientFactory to implement resilient HTTP requests
 - Implement Retry and Circuit Breaker patterns with exponential backoff with IHttpClientFactory and Polly policies


  ## Installation


You need to have the following: 
1. .NET 5.0 or later.
2. Docker Desktop
3. VS Code or Visual Studio 2019

After Docker Desktop has been installed, start the program and then ensure that it has been set up to use minimum amount of the following by going to the Settings > Advanced option, from the Docker icon in the system tray:
Memory: 4 GB
CPU: 2


## Docker

Download the repository
CD into it 

```sh
cd src
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
```

This will cause the Docker to create the images and get them running. 

You can launch microservices using the below urls:
 - Catalog API -> http://localhost:8000/swagger/index.html
 - Basket API -> http://localhost:8001/swagger/index.html
 - Discount API -> http://localhost:8002/swagger/index.html
 - Ordering API -> http://localhost:8004/swagger/index.html
 - Shopping.Aggregator -> http://localhost:8005/swagger/index.html
 - API Gateway -> http://localhost:8010/Catalog
 - Rabbit Management Dashboard -> http://localhost:15672 -- guest/guest
 - Portainer -> http://localhost:9000 -- admin/admin1234
 - pgAdmin PostgreSQL -> http://localhost:5050 -- admin@aspnetrun.com/admin1234
 - Web UI -> http://localhost:8006

 - Elasticsearch -> http://localhost:9200 -- To be developed
 - Kibana -> http://localhost:5601 -- To be developed
 - Web Status -> http://localhost:8007 -- To be developed
