using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp10_15Events_InBroaderSense2_usingAnonymousfunc
{
    class Program
    {
        static void Main(string[] args)
        {
            FireUp fu = new FireUp();
            fu.Run();
            Console.ReadLine();
        }
    }
    class CarEventArgs:EventArgs
    {
        public string msg;
        public CarEventArgs(string msg)
        {
            this.msg = msg;
        }
    }
    //Publisher class
    //publishes a delegate, that subscriber class subscribes
    class Car
    {
        //the subscriber class must impelement this delaget
        public delegate void CarEngineHandler(object car,CarEventArgs args);
        public event CarEngineHandler OkEvent;
        public event CarEngineHandler CriticalEvent;
        public event CarEngineHandler DeadEvent;

        public string CarName { get; set; }
        public int MaxSpeed { get; set; }
        public int CurrentSpeed { get; set; }
        private bool isCarDead;

        public Car(string CarName, int CurrentSpeed, int MaxSpeed)
        {
            this.CarName = CarName;
            this.CurrentSpeed = CurrentSpeed;
            this.MaxSpeed = MaxSpeed;
        }

        public void Accelerate(int delta)
        {
            CarEventArgs args;
            if (isCarDead && DeadEvent!=null)//notify the subscriber
            {
                args = new CarEventArgs("Car is dead baby");
                DeadEvent(this,args);//notify the subscriber
            }
            else
            {
                CurrentSpeed += delta;
                if (10==(MaxSpeed-CurrentSpeed) && CriticalEvent!=null)//notify the subscriber
                {
                    args = new CarEventArgs("Watch out boy!!");
                    CriticalEvent(this,args);//notify the subscriber
                }
                else if (CurrentSpeed>=MaxSpeed)
                {
                    isCarDead = true;
                }
                else
                {
                    args = new CarEventArgs("Car is fine");
                    OkEvent(this,args);//notify the subscriber
                }
            }
        }
    }
    //subscriber class1
    class OkSubscriber
    {
        //helper method to subscribe the publisher's class deleagte
        public void Subscribe(Car car)
        {
            car.OkEvent += new Car.CarEngineHandler(DisplayOk);
        }
        //event handler implementing the subscriber's delegate***should have same signature as that of the delegate
        public void DisplayOk(object clock,CarEventArgs args)
        {
            Console.WriteLine("Gadi badiya chal raha hai ==> msg from publisher {0}",args.msg);
        }
    }
    //subscriber class2
    class CriticalSubscriber
    {
        //helper method to subscribe the publishers delegate

        //event handler implementing the publsihers delegate
        public void Subscribe(Car car)
        {
            car.CriticalEvent += delegate (object clock,CarEventArgs args)
            {
                Console.WriteLine("May day!! may day!! watch out ==> msg from publsiher {0}",args.msg);
            };
        }
    }
    class CriticalSubscriber2
    {
        //helper method to susbcribe the publishers delegate
        public void Subscribe(Car car)
        {
            car.CriticalEvent += new Car.CarEngineHandler(Display);
        }
        //event handler implementing the publsihers delegate
        public void Display(object car,CarEventArgs args)
        {
            Console.WriteLine("SOS, break out of the car ==> msg from publsiher {0}",args.msg);
        }
    }
    class DeadSubscriber
    {
        //helper method to subscribe the publishers delegate
        //*******doing with anynomus function*********
        public void Subscribe(Car car)
        {
            car.DeadEvent += delegate (object cr,CarEventArgs args)
            {
                Console.WriteLine("Sorry but the car is immovable ==> msg from publishers {0}",args.msg);
            };
        }

        //event handler implementing the publishers delegate
        
    }
    class FireUp
    {
        public void Run()
        {
            Car c = new Car("INNOVA",10,100);

            OkSubscriber os = new OkSubscriber();
            os.Subscribe(c);

            CriticalSubscriber cs1 = new CriticalSubscriber();
            cs1.Subscribe(c);

            CriticalSubscriber2 cs2 = new CriticalSubscriber2();
            cs2.Subscribe(c);

            DeadSubscriber ds = new DeadSubscriber();
            ds.Subscribe(c);
            for (int i = 0; i < 6; i++)
            {
                c.Accelerate(20);
            }
            
        }
    }
}
