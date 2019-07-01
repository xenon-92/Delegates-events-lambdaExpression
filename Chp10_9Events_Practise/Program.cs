using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp10_9Events_Practise
{
    class Program
    {
        static void Main(string[] args)
        {
            Cab c = new Cab("lexus", 10, 100);
            //register events
            Cab.CarEngineHandler handlerOk = new Cab.CarEngineHandler(EventGood);
            c.OkEvent += handlerOk;

            Cab.CarEngineHandler handlerCritical1 = new Cab.CarEngineHandler(CriticalEvent1);
            Cab.CarEngineHandler handlerCritical2 = new Cab.CarEngineHandler(CriticalEvent2);
            c.CriticalEvent += handlerCritical1 + handlerCritical2;

            Cab.CarEngineHandler handlerDead = new Cab.CarEngineHandler(DeadEvent);
            c.DeadEvent += handlerDead;

            Console.WriteLine("Speeding up");
            for (int i = 0; i < 6; i++)
            {
                c.Accelerate(20);
            }
            //deregister events
            c.DeadEvent -= handlerDead;
            c.CurrentSpeed = 10;
            for (int i = 0; i < 6; i++)
            {
                c.Accelerate(20);
            }
            Console.ReadLine();
        }
        static void EventGood(string args)
        {
            Console.WriteLine("GOOD EVENT => {0}",args);
        }
        static void CriticalEvent1(string args)
        {
            Console.WriteLine("CriticalEvent1 => {0}", args);
        }
        static void CriticalEvent2(string args)
        {
            Console.WriteLine("CriticalEvent2 => {0}", args);
        }
        static void DeadEvent(string args)
        {
            Console.WriteLine("DeadEvent => {0}", args);
        }
    }
    class Cab
    {
        public delegate void CarEngineHandler(string msg);

        public event CarEngineHandler OkEvent;//class Cab sends out these events
        public event CarEngineHandler CriticalEvent;
        public event CarEngineHandler DeadEvent;

        public int MaxSpeed { get; set; }
        public int CurrentSpeed { get; set; }
        public string Name { get; set; }
        private bool isCarDead;

        public Cab(string Name, int CurrentSpeed, int MaxSpeed)
        {
            this.Name = Name;
            this.MaxSpeed = MaxSpeed;
            this.CurrentSpeed = CurrentSpeed;
        }

        public void Accelerate(int deltaSpeed)
        {
            if (isCarDead && DeadEvent!=null)
            {
                DeadEvent(":-( car is dead");
            }            
            else
            {
                CurrentSpeed += deltaSpeed;
                if (10==(MaxSpeed - CurrentSpeed))
                {
                    CriticalEvent(":-< watch out, boy");
                }
                else if (CurrentSpeed>=MaxSpeed)
                {
                    isCarDead = true;
                }
                else
                {
                    OkEvent(" :-)car is Fine Dude");
                }
            }
        }
    }
}
