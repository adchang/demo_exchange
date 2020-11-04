# Code

## Goals

- POC an opinionated tech-stack for a high-performance exchange that is primarily Microsoft, C# centric backend with an eye towards potential polyglot microservices.
- Learn dotnet and other packages. Small business domain to focus on flushing out coding conventions and implementing new aspects of a robust and secure application environment. Follow along with [commit history like a tutorial](https://docs.google.com/document/d/1cFWrQyfAGYdtYoevVmW26hGT1Nw2f16iywPYAVQoVTM/edit#heading=h.xu2up6xyb01e).

**Out of scope**
- <del>Frontend, but favor [Flutter](https://flutter.dev/) for multi-platform UI</del>
- <del>Async processing, event propagation</del>
- Monitoring
- Big data and ML

## Architecture Overview

### Domain

Envisioned to be 2 services
- Account: Account and user management
- Exchange: Everything else

It is important to differentiate Account and Account Portfolio. Account manages demographics, where as Account Portfolio manages portfolio balances and holdings. Account is initially separated out to familiarize a microservices architecture, and serve as a template. With microservices, we would also need an API gateway, and gaining experience on k8s deployment with microservices architecture will be a valuable operation experience without burdening ourselves with complexity. This approach, with vertical scaling, should suffice for initial few years, allowing ourselves to focus on features.

Eventually though, transaction volume will necessitate horizontal scaling and partitioning. The order service is designed to be partitionable by ticker; an order service can be configured to process specific ticker(s). Some aspects of account portfolio should be split out from order service. However, the two domains should remain on the same database server to ensure we still have transactional integrity. Portfolio balance is not something we want to rely on eventual consistency; we need to know portfolio holdings at point of order fulfillment.

### Tiers

- API Facade/Gateway: L7 proxy for service aggregation, user authentication, basic validation, rate limiting, and statistics. Supports both OpenAPI (HTTP1.1) and gRPC (HTTP2)

    Implements service.proto

- gRPC Services: Service wrapper for user authorization, row-level security, and response packaging.

    Note: OpenAPI equivalent would be implemented using dotnet Controller (DemoExchangeOrderService is a sample implementation). Microservice-to-microservice should really use HTTP2 over HTTP1.1. Additionally, streams over websocket.

- Services: Business logic layer.

### Frameworks & Packages

**RPC & Data Serialization**: [gRPC](https://grpc.io/), [protobuf](https://developers.google.com/protocol-buffers)

**Persistence & Cacheing**: [SQLServer in-memory](https://docs.microsoft.com/en-us/sql/relational-databases/in-memory-database), [Redis](https://redis.io/), 

**DevOps**: [GitHub](https://github.com/), [Travis](https://travis-ci.org/)

**Operational**: [Kubernetes](https://kubernetes.io/), [Docker](https://www.docker.com/), [Istio](https://istio.io/)

## Api and Interface
Client-side libraries with ready-to-use implementations. Api is the public library and Interface is the internal library.

## Naming convention
Where X is the domain,

- XEntity: Persistence object for the domain data type.
- XContext: Database context
- XContextFactory: Factory for the database context
- X: Protobuf for over-the-wire serialization. Base class in inheritance hierarchy
- XBL: POCO representing the business logic layer. Inherits from XEntity.
- XTransformer: Request/Entity/BL transformers
- XService: Service for the domain
- XRequest: Protobuf representing the request
- XResponse: Response object from service processing
- XBase: Client base class for the domain data type
- TestX: For testing purposes

## [CI/CD pipeline](https://docs.google.com/document/d/1cFWrQyfAGYdtYoevVmW26hGT1Nw2f16iywPYAVQoVTM/edit#heading=h.50grtwc2j6jd)

## Logging

- Verbose: For diagnostics purposes and may contain sensitive application data. Should not be enabled in production.

  Must be surrounded by DIAGNOSTICS symbol

- Debug: Transient, for development purposes only.

  Can be left in code if it helps with development, but must be surrounded by DEBUG symbol

- Information: General flow of the application
- Warning: Abnormal or unexpected event in the application flow, typically from programming error
- Error: Application execution is stopped due to a failure in current activity
- Fatal: Unrecoverable application or crash that requires immediate attention

## Preprocessor symbols
- DEBUG: For testing purposes

  Methods should start with Test
  
- DIAGNOSTICS: For diagnosing issues in staging

  Methods should start with Diagnostics

- PERF, PERF_FINE, PERF_FINEST: For performance logging

  Methods should start with TestPerf

## Building
### DLL release
1. Remove -SNAPSHOT in csproj, commit & push
2. dotnet-pack: Creates a nupkg in staging directory (/opt/adc/nuget_stg -> /home/adchang/janus/nuget_stg)
3. In Windows, nuget add C:\adc\vm\hela\nuget_stg\PACKAGE -source c:\adc\vm\hela\nuget
4. Increment version num and add -SNAPSHOT in csproj, commit & push

NOTE: /home/adchang/janus is a shared folder with windows host

### Docker build
1. Copy nuget directory to project root (Docker cannot follow symlinks)
2. Remove -SNAPSHOT in csproj & .bash_aliases_demo_exchange, commit & push
3. XXX-build
4. docker-tag XXX:VERSION
5. docker-push XXX:VERSION
6. Update docker-compose to use the new version
7. Increment version num and add -SNAPSHOT in csproj & .bash_aliases_demo_exchange, commit & push
