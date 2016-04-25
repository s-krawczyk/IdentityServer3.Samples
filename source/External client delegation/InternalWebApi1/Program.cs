using System;
using Microsoft.Owin.Hosting;
using Serilog;

namespace InternalWebApi1
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      // logging
      Log.Logger = new LoggerConfiguration()
          .WriteTo
          .LiterateConsole(outputTemplate: "{Timestamp:HH:MM} [{Level}] ({Name:l}){NewLine} {Message}{NewLine}{Exception}")
          .CreateLogger();

      using (WebApp.Start<Startup>("http://localhost:44339"))
      {
        Console.WriteLine("server running...");
        Console.ReadLine();
      }
    }
  }
}