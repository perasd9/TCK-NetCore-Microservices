##Overview

TCK-NetCore-Microservices is a sample project demonstrating a microservices architecture built with .NET Core.
This repository showcases different microservices handling various business domains such as users, sporting events, places, and reservations.
The project utilizes REST and gRPC services, performance testing, and resilient communication patterns using Polly.

Clone the project.
Run the following commands:

```bash
$ git clone https://github.com/perasd9/TCK-NetCore-Microservices.git
$ cd TCK-NetCore-Microservices
```
For each microservice, navigate to the corresponding directory and run:

```bash
$ dotnet ef 'database' update
$ dotnet run --project <Microservice>.API
```

##Features

    -Microservices Architecture: Each business domain is represented as an independent microservice.
    -gRPC and REST APIs: Exposes both gRPC and REST endpoints for interaction.
    -Polly for Resilience: Implements retry, circuit breaker, and fallback policies.
    -Caching: Utilizes server-side and memory caching for optimized data retrieval.
    -Authentication and Authorization: Implements JWT-based authentication for secure access.
    -Performance Testing: K6 is used to measure performance across services. Performance testing was best established with complex objects i.e Place objects.
    -Clean Architecture: Maintains separation of concerns, promoting clean code and maintainability.
    -Modified Saga Pattern: Implements distributed transaction management.

##Microservices

I used [yarp reverse proxy](https://microsoft.github.io/reverse-proxy/articles/index.html) to route synchronous requests to the corresponding microservice.
Each microservice has its dependencies such as databases, files etc. Each microservice is decoupled from other microservices and developed and deployed separately.
Microservices talk to each other with Rest or gRPC for synchronous calls.

    -Users.API: Manages user-related operations, including authentication and authorization.
    -Places.API: Manages place-related data.
    -SportingEvents.API: Handles sporting events data.
    -Reservations.API: Processes reservations and integrates with external services for loyalty points and ticket management.
    -TypesOfSportingEvents.API: Manages types of sporting events.

##Prerequisites

    -.NET 7 SDK
    -Postman or any gRPC client
    -K6 for performance testing
    -SQL Server database provider