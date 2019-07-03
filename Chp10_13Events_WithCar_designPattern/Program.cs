using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp10_13Events_WithCar_designPattern
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
    class CarEventArgs: EventArgs
    {
        public string msg;
        public CarEventArgs(string msg)
        {
            this.msg = msg;
        }
    }
       //Publisher class
       //has a deleagte that it publishes
    class Car
    {
        //Subscriber must implement this delegate
        public delegate void CarHandlerDelegate(object car,CarEventArgs eventArgs);
        public event CarHandlerDelegate OkEvent;
        public event CarHandlerDelegate CriticalEvent;
        public event CarHandlerDelegate DeadEvent;

        public string Name { get; set; }
        public int MaxSpeed { get; set; }
        public int CurrentSpeed { get; set; }
        private bool isCarDead;

        public Car(string Name, int CurrentSpeed, int MaxSpeed)
        {
            this.Name = Name;
            this.CurrentSpeed = CurrentSpeed;
            this.MaxSpeed = MaxSpeed;
        }

        public void Accelerate(int delta)
        {
            if (isCarDead && DeadEvent!=null)
            {
                CarEventArgs args = new CarEventArgs("May Day!! May Day!!");
                DeadEvent(this,args);
            }
            else
            {
                CurrentSpeed += delta;
                if (10==(MaxSpeed-CurrentSpeed))
                {
                    CriticalEvent(this, new CarEventArgs("Watch out boy!! you are gonna blow up"));
                }
                else if (CurrentSpeed>=MaxSpeed)
                {
                    isCarDead = true;
                }
                else
                {
                    OkEvent(this, new CarEventArgs("Great Going, boy"));
                }
            }
        }
    }
    //Subscriber class1
    class ShowGoodMsg
    {
        //helper method to subscribe the publishers delegate
        public void Subscribe(Car car)
        {
            Car.CarHandlerDelegate handler1 = new Car.CarHandlerDelegate(Display);
            car.OkEvent += handler1;
        }
        //event handler to implement publishers delegate
        public void Display(object car,CarEventArgs args)
        {
            Console.WriteLine("The message is good ==>{0}",args.msg);
        }
    }
    //Subscriber class2
    class CriticalMsg1
    {
        //helper method to subscribe the publishers delegate
        public void Subscribe(Car car)
        {
            car.CriticalEvent += new Car.CarHandlerDelegate(Display);
        }
        //event handler implementing the publishers delegate
        public void Display(object gari,CarEventArgs args)
        {
            Console.WriteLine("This message is critical ==>{0}",args.msg);
        }
    }
    class CriticalMsg2
    {
        //helper method to subscribe the publishers delegate
        public void Subscribe(Car car)
        {
            car.CriticalEvent += new Car.CarHandlerDelegate(Display);
        }
        //event handler that implements the publishers delegate
        public void Display(object o,CarEventArgs args)
        {
            Console.WriteLine("Tik,Tik, you are running out of time, please jump off your car ==>{0}",args.msg);
        }
    }
    class DeadMsg
    {
        //helper method to subscribe the publishers delegate
        public void Subscribe(Car car)
        {
            car.DeadEvent += new Car.CarHandlerDelegate(Display);
        }
        //event handler method that implements the publishers delegate
        public void Display(object o,CarEventArgs args)
        {
            Console.WriteLine("The car is dead, sorry==>{0}",args.msg);
        }
    }

    class FireUp
    {
        public void Run()
        {
            Car car = new Car("Innova",10,100);

            ShowGoodMsg sm = new ShowGoodMsg();
            sm.Subscribe(car);

            CriticalMsg1 c1 = new CriticalMsg1();
            c1.Subscribe(car);

            CriticalMsg2 c2 = new CriticalMsg2();
            c2.Subscribe(car);

            DeadMsg dm = new DeadMsg();
            dm.Subscribe(car);

            for (int i = 0; i < 6; i++)
            {
                car.Accelerate(20);
            }
        }
    }
}
