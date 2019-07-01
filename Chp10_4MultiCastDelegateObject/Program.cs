using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp10_4MultiCastDelegateObject
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            Car c = new Car("safari", 10, 100);
            Car.CarEventHandler delegateHandler1 = new Car.CarEventHandler(p.FirstEvent);
            Car.CarEventHandler delegateHandler2 = new Car.CarEventHandler(p.SecondEvent);
            c.RegisterEvent(delegateHandler1);
            c.RegisterEvent(delegateHandler2);
            for (int i = 0; i < 6; i++)
            {
                c.Accelerate(20);
            }
            Console.ReadLine();
        }
        public void FirstEvent(string s)
        {
            Console.WriteLine("*********Message from Car Object*********");
            Console.WriteLine(s);
            Console.WriteLine("*****************************************");
        }
        public void SecondEvent(string s)
        {
            Console.WriteLine("********2ndMessage from Car Object*********");
            Console.WriteLine(s + "from 2nd event");
            Console.WriteLine("*****************************************");
        }
    }
    class Car
    {
        public delegate void CarEventHandler(string s);
        private CarEventHandler Listofhandler;
        public void RegisterEvent(CarEventHandler registerMethod)
        {
            Listofhandler += registerMethod;
        }
        public int CarSpeed { get; set; }
        public int CarMaxSpeed { get; set; }
        public string CarName { get; set; }
        private bool IsCarDead;

        public Car()
        {
            CarMaxSpeed = 100;
        }
        public Car(string cname,int currSpedd,int Maxspeed)
        {
            CarName = cname;
            CarSpeed = currSpedd;
            CarMaxSpeed = Maxspeed;
        }
        public void Accelerate(int delta)
        {
            if (IsCarDead && Listofhandler!= null)
            {
                Listofhandler("Car is dead");
            }
            else
            {
                CarSpeed += delta;
                if (10==(CarMaxSpeed-CarSpeed) && Listofhandler != null)
                {
                    Listofhandler("Car engine is going to blow");
                }
                else
                {
                    if (CarSpeed >= CarMaxSpeed)
                    {
                        IsCarDead = true;
                    }
                    else
                    {
                        Console.WriteLine("Current Speed = {0} ",CarSpeed);
                    }
                }
            }
        }
    }
}
