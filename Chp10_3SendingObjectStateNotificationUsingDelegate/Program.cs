using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp10_3SendingObjectStateNotificationUsingDelegate
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*********Delegate as event enablers*******\n");
            Car c1 = new Car("SlugBug",100,10);
            c1.RegisterWithCarEngine(new Car.CarEngineHandler(OnCarEngineEvent));
            Console.WriteLine("***** Speeding up *****");
            for (int i = 0; i < 6; i++)
                c1.Accelerate(20);
            Console.ReadLine();
        }
        public static void OnCarEngineEvent(string msg)
        {
            Console.WriteLine("\n***** Message From Car Object *****");
            Console.WriteLine("=> {0}", msg);
            Console.WriteLine("***********************************\n");
        }
    }
    class Car
    {
        public delegate void CarEngineHandler(string msgFromCaller);
        private CarEngineHandler listOfHandlers;
        public void RegisterWithCarEngine(CarEngineHandler methodToCall)
        {
            listOfHandlers = methodToCall;
        }

        public int CurrentSpeed { get; set; }
        public int MaxSpeed { get; set; }
        public string PetName { get; set; }
        //is car alive or dead?
        private bool isCarDead;
        //
        public Car()
        {
            MaxSpeed = 100;
        }
        public Car(string name,int maxSp, int currSp)
        {
            PetName = name;
            MaxSpeed = maxSp;
            CurrentSpeed = currSp;
        }
        public void Accelerate(int delta)
        {
            if (isCarDead)
            {
                if (listOfHandlers != null)
                {
                    listOfHandlers("Sorry this car is dead");
                }
            }
            else
            {
                CurrentSpeed += delta;
                //is this car almost dead
                if (10 ==(MaxSpeed - CurrentSpeed) && listOfHandlers != null)
                {
                    listOfHandlers("Careful buddy, this engine is gonna blow");
                }
                if (CurrentSpeed>= MaxSpeed)
                {
                    isCarDead = true;
                }
                else
                {
                    Console.WriteLine("CurrentSpeed = {0}",CurrentSpeed);
                }
            }
        }
    }
}
