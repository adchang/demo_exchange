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

- XEntity: Persistence object for the domain data type
- XContext: Database context
- XContextFactory: Factory for the database context
- XModel: Client DTO representing the domain data type
- XViewModel: Client DTO representing the view model
- XTransformer: Entity to ViewModel transformers
- XService: Service for the domain
- XServiceController: Controller for the service domain
- XServiceResponse: Response object from service processing
- XBase: Client base class for the domain data type
- TestX: For testing purposes

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

