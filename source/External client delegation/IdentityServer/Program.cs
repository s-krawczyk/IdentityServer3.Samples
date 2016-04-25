using System;
using Microsoft.Owin.Hosting;
using Serilog;

namespace IdentityServer
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

      // hosting identityserver
      using (WebApp.Start<Startup>("http://localhost:44333"))
      {
        Console.WriteLine("server running...");
        Console.ReadLine();
      }
    }
  }
}