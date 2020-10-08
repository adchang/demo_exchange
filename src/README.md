# Code
Envisioned to be 2 services
- Account: Account and user management
- Exchange: Everything else

It is important to differentiate Account and Account Portfolio. Account manages demographics, where as Account Portfolio manages portfolio balances and holdings. Account is initially separated out to familiarize a microservices architecture, and serve as a template. With microservices, we would also need an API gateway, and gaining experience on k8s deployment with microservices architecture will be a valuable operation experience without burdening ourselves with complexity. This approach, with vertical scaling, should suffice for initial few years, allowing ourselves to focus on features.

Eventually though, transaction volume will necessitate horizontal scaling and partitioning. The order service is designed to be partitionable by ticker; an order service can be configured to process specific ticker(s). Some aspects of account portfolio should be split out from order service. However, the two domains should remain on the same database server to ensure we still have transactional integrity. Portfolio balance is not something we want to rely on eventual consistency; we need to know portfolio holdings at point of order fulfillment.

## Interface
Client-side library with ready-to-use implementations

## Naming convention
Where X is the domain,

- XModel: DTO representing the domain data type
- XViewModel: DTO representing the view model
- XService: Service
- XServiceResponse: Response object from service processing
- XEntity: Persistence object
- XBase: Base class if applicable
- TestX: For testing purposes

## Preprocessor symbols
- DEBUG: For testing purposes
- DIAGNOSTICS: For diagnosing issues in staging
- PERF, PERF_FINE, PERF_FINEST: For performance logging
