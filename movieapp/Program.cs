using System.Diagnostics;
using System;

namespace MovieApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Module1Helper.DeleteItem();

            if (Debugger.IsAttached) 
            {
                Console.Write("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
