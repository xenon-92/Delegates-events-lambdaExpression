using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp10_19_RetrofittingTheCarExampleWithDelegateAnonLambda
{
    class Program
    {
        static void Main(string[] args)
        {
            FireUp fp = new FireUp();
            fp.Run();
            Console.ReadLine();
        }
    }
    class CarEventArgs:EventArgs
    {
        public string msg = string.Empty;
        public CarEventArgs(string msg)
        {
            this.msg = msg;
        }
    }
    //publisher class
    //Publishes a delegate to which subscriber class subscribe
    class Car
    {
        public delegate void CarEngineHandler(object car,CarEventArgs args);
        public event CarEngineHandler OkEvent;
        public event CarEngineHandler CriticalEvent;
        public event CarEngineHandler DeadEvent;

        public string CarName { get; set; }
        public int MaxSpeed { get; set; }
        public int CurrentSpeed { get; set; }
        private bool isCarDead;

        public Car(string CarName, int MaxSpeed, int CurrentSpeed)
        {
            this.CarName = CarName;
            this.MaxSpeed = MaxSpeed;
            this.CurrentSpeed = CurrentSpeed;            
        }

        public void Accelerate(int delta)
        {
            if (isCarDead && DeadEvent!= null)
            {
                CarEventArgs args = new CarEventArgs("msg from Publisher ==> the car is dead");
                DeadEvent(this,args);
            }
            else
            {
                CurrentSpeed += delta;
                if (10==(MaxSpeed-CurrentSpeed))
                {
                    CarEventArgs args = new CarEventArgs("msg from Publisher ==> the car is in critical condition");
                    CriticalEvent(this,args);
                }
                else if (CurrentSpeed>=MaxSpeed)
                {
                    isCarDead = true;
                }
                else
                {
                    CarEventArgs args = new CarEventArgs("msg from Publisher ==> the car is in good condition");
                    OkEvent(this,args);
                }
            }
        }
    }
    //Subscriber class1
    class OkSubscriber
    {
        //helper method to register the publishers delegate
        public void Subscribe(Car car)
        {
            car.OkEvent += new Car.CarEngineHandler(Dispaly);
        }
        //event handler to implement the publishers delegate
        public void Dispaly(object pub,CarEventArgs args)
        {
            Console.WriteLine("Message from OkSubscriber is {0}", args.msg);
        }
    }
    //subscriber class 2
    class CriticalSubscriber1
    {
        //helper method to register the publishers delegate

        //event handler to implement the publishers delegate
        public void Subscribe(Car car)
        {
            car.CriticalEvent += delegate (object pub,CarEventArgs args)//anonymous function
            {
                Console.WriteLine("Message from CriticalSubscriber1 is {0}", args.msg);
            };
        }
    }
    class CriticalSubscriber2
    {
        public void Subscribe(Car car)
        {
            car.CriticalEvent += ((object pub, CarEventArgs args) =>//explicit lambda expression
            {
                Console.WriteLine("Message from CriticalSubscriber2 is {0}", args.msg);
            });
        }
    }
    class DeadSubscriber
    {
        public void Subscribe(Car car)
        {
            car.DeadEvent += ((pub,args) => { //implicit lambda expression
                Console.WriteLine("Message from DeadSubscriber is {0}", args.msg);
            });
        }
    }
    class FireUp
    {
        public void Run()
        {
            Car c = new Car("Innova",100,10);

            OkSubscriber os = new OkSubscriber();
            os.Subscribe(c);

            CriticalSubscriber1 cs1 = new CriticalSubscriber1();
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
