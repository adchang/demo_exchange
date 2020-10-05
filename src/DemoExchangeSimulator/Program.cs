using System;

namespace DemoExchangeSimulator {
  class Program {
    static void Main(string[] args) {
      Console.WriteLine("\nHello! I am a simulator for DemoExchange\n");
      Console.WriteLine("How many BUY orders to seed?: ");
      Console.WriteLine("How many SELL orders to seed?: ");
      Console.WriteLine("");

      Simulator sim = new Simulator();
      sim.Start(300, 300, 180, 10);
    }
  }
}
