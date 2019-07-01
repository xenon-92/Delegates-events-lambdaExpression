using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 Drawbacks of delegate, the boilerplate part
 Moreover, when you use delegates in the raw as your application’s callback mechanism, if you do
not define a class’s delegate member variables as private, the caller will have direct access to the delegate
objects. In this case, the caller could reassign the variable to a new delegate object (effectively deleting
the current list of functions to call) and, worse yet, the caller would be able to directly invoke the
delegate’s invocation list.
     */
namespace Chp10_7BolierPlateDelegates
{
    class Program
    {
        static void Main(string[] args)
        {
            Car c = new Car();
            //ConsoleColor previous = Console.ForegroundColor;
            //Console.ForegroundColor = ConsoleColor.DarkYellow;
            //Console.BackgroundColor = ConsoleColor.Red;
            Car.CarEngineHandler Handler1 = new Car.CarEngineHandler(CallWhenCarExplodes);
            c.listOfHandlers = Handler1;
            c.Accelerate(10);
            //Console.ForegroundColor = ConsoleColor.Magenta;
            //Console.BackgroundColor = ConsoleColor.DarkBlue;
            Car.CarEngineHandler Handler2 = new Car.CarEngineHandler(CallMeToo);
            c.listOfHandlers = Handler2;
            c.Accelerate(10);
            //Console.BackgroundColor = ConsoleColor.Black;
            //Console.ForegroundColor = previous;
            c.listOfHandlers("Trying to access, which is basically a drawback");
            Console.ReadLine();
            //just to watch
            Handler1("aiyla, aisa bhi hota hai kya?");
            Console.ReadLine();
            
        }
        static void CallWhenCarExplodes(string msg)
        {
            Console.WriteLine(msg);
        }
        static void CallMeToo(string msg)
        {
            Console.WriteLine(msg);
        }
    }
    class Car
    {
        public delegate void CarEngineHandler(string msgFromCaller);
        public CarEngineHandler listOfHandlers;//public and not private

        public void Accelerate(int delta)
        {
            if (listOfHandlers!=null)
            {
                listOfHandlers("Sorry, this car is dead");
            }
        }
    }
}
