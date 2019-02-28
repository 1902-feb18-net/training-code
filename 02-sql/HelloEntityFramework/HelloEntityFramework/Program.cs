using System;

namespace HelloEntityFramework
{
    class Program
    {
        // EF database-first approach steps:
        //
        // 1. have startup project, and data access library project.
        // 2. reference data access from startup project.
        // 3. add NuGet packages to the startup project:
        //     - Microsoft.EntityFrameworkCore.Tools
        //     - Microsoft.EntityFrameworkCore.SqlServer

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
