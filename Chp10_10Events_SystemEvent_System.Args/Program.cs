using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 System.Object argument represents a reference to the object that sent the event such as Car
 System.EventArgs represents information regarding event at hand
     */
namespace Chp10_10Events_SystemEvent_System.Args
{
    class Program
    {
        static void Main(string[] args)
        {
            Car c = new Car("Fiat",10,100);

            Car.CarEngineHandler handlerOk = new Car.CarEngineHandler(EventGood);
            c.EventOk += handlerOk;

            Car.CarEngineHandler handlerCritical1 = new Car.CarEngineHandler(CriticalEvent1);
            Car.CarEngineHandler handlerCritical2 = new Car.CarEngineHandler(CriticalEvent2);
            c.EventCritical += handlerCritical1 + handlerCritical2;

            Car.CarEngineHandler handlerDead = new Car.CarEngineHandler(DeadEvent);
            c.EventDead += handlerDead;

            Console.WriteLine("*******speeding up the car***************");
            for (int i = 0; i < 6; i++)
            {
                c.Accelerate(20);
            }
            Console.ReadLine();
        }
        static void EventGood(object sender, CarEventArgs e)
        {
            Console.WriteLine("{0} says: {1}",sender,e.msg);
        }
        static void CriticalEvent1(object sender,CarEventArgs e)
        {
            Console.WriteLine("{0} says {1}",sender,e.msg);
        }
        static void CriticalEvent2(object sender, CarEventArgs e)
        {
            Console.WriteLine("auuuuuuuuuu {0} says {1}", sender, e.msg);
        }
        static void DeadEvent(object sender, CarEventArgs e)
        {
            Console.WriteLine("{0} says {1}", sender, e.msg);
        }
    }
    class Car
    {
        public delegate void CarEngineHandler(object sender, CarEventArgs e);
        public event CarEngineHandler EventOk;
        public event CarEngineHandler EventCritical;
        public event CarEngineHandler EventDead;

        public string Name { get; set; }
        public int MaxSpeed { get; set; }
        public int CurrentSpeed { get; set; }
        private bool IsCarDead;

        public Car(string Name, int CurrentSpeed, int MaxSpeed)
        {
            this.Name = Name;
            this.MaxSpeed = MaxSpeed;
            this.CurrentSpeed = CurrentSpeed;
        }

        public void Accelerate(int delta)
        {
            if (IsCarDead && EventDead!=null)
            {
                EventDead(this,new CarEventArgs(":-( , afsos, car ki mrityu ho chuki hai"));
            }
            else
            {
                CurrentSpeed += delta;
                if (10==(MaxSpeed - CurrentSpeed))
                {
                    EventCritical(this,new CarEventArgs(":-< ,sabdhan apka car kabhi bhi burn ho sakta hai"));
                }
                else if (CurrentSpeed >= MaxSpeed)
                {
                    IsCarDead = true;
                }
                else
                {
                    EventOk(this, new CarEventArgs(":-) ,sukoon se baithyie aapki car sahi chal rahi hai"));
                }
            }
        }
    }
    class CarEventArgs: EventArgs
    {
        public readonly string msg;
        public CarEventArgs(string message)
        {
            msg = message;
        }
    }
}
