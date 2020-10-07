using System;

namespace DemoExchangeSimulator {
  class Program {
    static void Main(string[] args) {
      Console.WriteLine("\nHello! I am a simulator for DemoExchange\n");
      Console.WriteLine("How many orders to seed?: ");
      Console.WriteLine("");

      Simulator sim = new Simulator();
      sim.Start(0, 3883, 10);
    }
  }
}
