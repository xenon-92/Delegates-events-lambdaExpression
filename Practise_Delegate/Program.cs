using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practise_Delegate
{
    class Program
    {
        static void Main(string[] args)
        {
            Car.DelegateCarHandler delegateCarHandler1 = new Car.DelegateCarHandler(Program.Event1);
            Car.DelegateCarHandler delegateCarHandler2 = new Car.DelegateCarHandler(Program.Event2);
            Car c = new Car("innova",100,10);
            c.RegisterHandler(delegateCarHandler1);
            c.RegisterHandler(delegateCarHandler2);
            for (int i = 0; i < 5; i++)
            {
                c.Accelerate(20);
            }
            Console.ReadLine();
        }
        public static void Event1(string str)
        {
            Console.WriteLine(str);
        }
        public static void Event2(string str)
        {
            Console.WriteLine(str);
        }
        
    }
    class Car
    {
        public delegate void DelegateCarHandler(string str);
        private DelegateCarHandler listOfHandlers;
        public void RegisterHandler(DelegateCarHandler methodToCall)
        {
            listOfHandlers += methodToCall;
        }
        public string CarName { get; set; }
        public int CarMaxSpeed { get; set; }
        public int CarCurrentSpeed { get; set; }
        private bool isCarDead;
        
        public Car(string name,int maxS,int CurrentS)
        {
            CarName = name;
            CarMaxSpeed = maxS;
            CarCurrentSpeed = CurrentS;
        }
        public void Accelerate(int deltaSpeed)
        {
            if (isCarDead && listOfHandlers!= null)
            {
                listOfHandlers("Car is dead");
            }
            else
            {
                CarCurrentSpeed += deltaSpeed;
                if ( (CarMaxSpeed - CarCurrentSpeed) == 10 && listOfHandlers!= null)
                {
                    listOfHandlers("Car is in verge of dead");
                    isCarDead = true;                   
                }
                else
                {
                    Console.WriteLine("curren speed {0}",deltaSpeed);
                }
            }
        }
    }
}
