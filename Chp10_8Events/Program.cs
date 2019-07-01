using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp10_8Events
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*******fun with events**********");
            Car car = new Car("ford",10,100);
            //register event handlers
            Car.CarEngineHandler handlerOk = new Car.CarEngineHandler(CarNormal);
            car.CarIsOk += handlerOk;

            Car.CarEngineHandler handlerAboutTo1 = new Car.CarEngineHandler(CarAboutToBlow);
            Car.CarEngineHandler handlerAboutTo2 = new Car.CarEngineHandler(CarIsAlmostDoomed);
            car.AboutToExplode += handlerAboutTo1 + handlerAboutTo2;

            Car.CarEngineHandler handlerDead = new Car.CarEngineHandler(CarExploded);
            car.Exploded += handlerDead;
            //end of handler registration
            Console.WriteLine("*******speeding up**********");
            for (int i = 0; i < 6; i++)
            {
                car.Accelerate(20);
            }
            //de register events
            car.Exploded -= handlerDead;
            Console.WriteLine("*************Speeding up again************");
            for (int i = 0; i < 6; i++)
            {
                car.Accelerate(20);
            }
            Console.ReadLine();
        }
        /**************following are the list of events*********************/
        static void CarExploded(string str)
        {
            Console.WriteLine("=> sad news is that {0}",str);
        }
        static void CarAboutToBlow(string str)
        {
            Console.WriteLine(str);
        }
        static void CarIsAlmostDoomed(string str)
        {
            Console.WriteLine("==> crtitcal msg from car {0}",str);
        }
        static void CarNormal(string str)
        {
            Console.WriteLine("==> car is normal", str);
        }
        /*************************end of events***********************************/
    }
    class Car
    {
        public delegate void CarEngineHandler(string msg);
        /************************************************************************************************
         With this, you have configured the car to send three custom events without having to define custom
          registration functions or declare delegate member variables
             */
        public event CarEngineHandler Exploded;//sending C# events
        public event CarEngineHandler AboutToExplode;//sending C# events
        public event CarEngineHandler CarIsOk;//sending C# events
        //***********************************************************************************************
        public string CarName { get; set; }
        public int MaxSpeed { get; set; }
        public int CurrentSpeed { get; set; }
        private bool isCarDead;

        public Car(string name, int CurrSpd, int maxSpd)
        {
            CarName = name;
            CurrentSpeed = CurrSpd;
            MaxSpeed = maxSpd;
        }
        public void Accelerate(int incomingDelta)
        {
            if (isCarDead && Exploded != null)
            {
                Exploded("The car is dead");
            }
            else
            {
                CurrentSpeed += incomingDelta;
                if (10 == (MaxSpeed - CurrentSpeed))
                {
                    AboutToExplode("Watch out, boy");
                }
               else if (CurrentSpeed >= MaxSpeed)
                {
                    isCarDead = true;
                }
                else
                {
                    CarIsOk("Gadi Badiya chal rahi hai");
                }
            }
        }
    }
}
