using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp10_7BolierPlateDelegates
{
    class HowPrivateSolvesIt
    {
        public static void Main()
        {
            Cab cab = new Cab();
            Cab.DelegateHandler handler1 = new Cab.DelegateHandler(CallWhenCarExplodes);
            handler1("handler1 commencing the work");
            cab.RegisterHandler(handler1);
            cab.Accelerate(10);
            Cab.DelegateHandler handler2 = new Cab.DelegateHandler(CallMeToo);
            handler2("handler2 commencing the work");
            cab.RegisterHandler(handler2);
            cab.Accelerate(20);
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
    class Cab
    {
        public delegate void DelegateHandler(string msgFromCaller);
        private DelegateHandler listOfHandlers;
        public void RegisterHandler(DelegateHandler registerTheMethod)
        {
            listOfHandlers = registerTheMethod;
        }
        public void Accelerate(int delta)
        {
            if (listOfHandlers!=null)
            {
                listOfHandlers("the Car is dead");
            }
        }
    }
}
