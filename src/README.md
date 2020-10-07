# Code
Envisioned to be 2 services
- Account: Account and user management
- Exchange: Everything else

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
