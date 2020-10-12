# Order Service
WebService bootstrap for Order Service

## App Details
### To run in Docker
Linux

    1. cd src/DemoExchange.OrderService
    2. demo-exchange-order-up

### API examples

Get an order: 

    curl http://localhost:8080/v1/orders/{orderId}

Submit a BUY MARKET order:

    curl -d '{"AccountId":"curl-123", "Ticker":"ERX", "Quantity":100}' -H "Content-Type: application/json" -X POST http://localhost:8080/v1/market-order

Submit a SELL MARKET order:

    curl -d '{"AccountId":"curl-sell-123", "Ticker":"ERX", "Quantity":100, "Action":1}' -H "Content-Type: application/json" -X POST http://localhost:8080/v1/market-order

Submit a BUY LIMIT DAY order:

    curl -d '{"AccountId":"curl-123", "Action":0, "Ticker":"ERX", "Type":1, "Quantity":100, "OrderPrice":8.00055439, "TimeInForce":0}' -H "Content-Type: application/json" -X POST http://localhost:8080/v1/orders

