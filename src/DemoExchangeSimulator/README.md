# Simulator
A CLI app to simulate order submission and fullfilment

## Features
- 8 tickers: ERX, SPY, DIA, QQQ, UPRO, SPXU, OILU, OILD
- Initial limit order seeding: Specify minimum number of desired orders for each ticker; randomized price and quantity
- Number of trades: Specify number of trades to execute; randomized market and limit orders, price, and quantity
- Number of threads: Specify number of concurrent order submission during trading

## App Details
### To run App
Linux

    1. cd src/DemoExchangeSimulator
    2. dotnet build -c release
    3. ./bin/release/net5.0/DemoExchangeSimulator
