# Order Service
WebService bootstrap for Order Service

## App Details
### To run in Docker
Linux

    1. cd src/DemoExchange.OrderService
    2. demo-exchange-order-up

curl http:/localhost:8080/v1/orders?AccountId=123

curl -d '{"AccountId":"123", "Ticker":"ERX", "Quantity":"100"}' -H "Content-Type: application/json" -X POST http:/localhost:8080/v1/market-order
