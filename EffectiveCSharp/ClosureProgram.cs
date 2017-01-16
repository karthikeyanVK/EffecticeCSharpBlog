using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectiveCSharp
{
    class ClosureProgram
    {
        static void Main(string[] args)
        {
            var closureCall = TestClosure();
            Console.WriteLine($"Closure Incremented Value:  {closureCall(5)}");
            Console.WriteLine($"Closure Incremented Value:  {closureCall(6)}");
            Console.ReadKey();

            //Console.WriteLine($"Function Incremented Value:  {ClosureProgram.StraightForwardFunction(5)} ");
            //Console.WriteLine($"Function Incremented Value:  {ClosureProgram.StraightForwardFunction(6)} ");
            //Console.ReadKey();
            

            MyLargeClass largeObject = new MyLargeClass();
            WeakReference w = new WeakReference(largeObject);
            largeObject = w.Target as MyLargeClass;
            if (largeObject == null)
            {
                largeObject = new MyLargeClass();
            }
        }


        //Console.WriteLine($"Function Incremented Value:  {ClosureProgram.StraightForwardFunction(5)} ");
        //    Console.WriteLine($"Function Incremented Value:  {ClosureProgram.StraightForwardFunction(6)} ");
        //    Console.ReadKey();
        private static int  StraightForwardFunction(int noToPrint)
        {
            var mainScope = 0;
            return IncrementingFunction(noToPrint, ref mainScope);
            //Console.WriteLine("Incremented Value: " + IncrementingFunction(noToPrint));
        }
        private static int IncrementingFunction(int value, ref int mainScope)
        {
            mainScope = mainScope + 1;
            value = value + mainScope;

            return value;
        }
        public static Func<int, int> TestClosure()
        {
            var mainScope = 0;
            return delegate (int local)
            {
                mainScope = mainScope + 1;
                return local + mainScope;
            };
             
        }
    }
}
