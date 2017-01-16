using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectiveCSharp
{
    class FibbonacciProgram
    {
        static void Main(string[] args)
        {
            //IEnumerable<int> collections = Program.TestFibbonacciWithYield(100).Skip(50).Take(6);
            IEnumerable<int> collections = FibbonacciProgram.TestFibbonacciWithYield(10);


            foreach (int fibonnacciSeries in collections)
            {
                Console.WriteLine(fibonnacciSeries + " ,");
            }
            Console.ReadKey();

            //List<string> tokenList = new List<string>();
            //List<Stream> fileStreamList = new List<Stream>();
            //List<CustomClass> classList = new List<CustomClass>();

            List<int> tokenList = new List<int>();
            List<double> price = new List<double>();
            List<CustomStruct> structList = new List<CustomStruct>();


        }

        private static IEnumerable<int> TestFibbonacciWithYield(int limit)
        {
            int f1 = 0, f2 = 1, f3 = 0;
            Console.WriteLine("Fibonacci Series:");
            //You dont need the IList<int> collections;
            for (int i = 0; i < limit; i++)
            {
                f3 = f1 + f2;
                Console.WriteLine("Fibonacci Series sequence(i): "  + i);
                f1 = f2;
                f2 = f3;
                yield return f1;

            }
        }

        private static IEnumerable<int> TestFibbonacciWithoutYield(int limit)
        {
            int f1 = 0, f2 = 1, f3 = 0;
            Console.WriteLine("Fibonacci Series:");
            IList<int> collections = new List<int>();
            
            for (int i = 0; i < limit; i++)
            {
                f3 = f1 + f2;
                Console.WriteLine("Fibonacci Series sequence:" + i);
                collections.Add(f1);
                f1 = f2;
                f2 = f3;
            }
            return collections;
        }
    }

    internal struct CustomStruct
    {
    }
}
