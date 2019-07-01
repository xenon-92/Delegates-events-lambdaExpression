using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp10_5MultiCastDelegateObject_RemoveTargetsFromDelegatesInvocationLst
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            Car c = new Car("safari",10,100);
            Car.DelegateCarHandler handler1 = new Car.DelegateCarHandler(p.Event1);
            Car.DelegateCarHandler handler2 = new Car.DelegateCarHandler(p.Event2);
            Car.DelegateCarHandler handler3 = new Car.DelegateCarHandler(p.Event3);
            c.RegisterHandler(handler1);
            c.RegisterHandler(handler2);
            c.RegisterHandler(handler3);
            for (int i = 0; i < 6; i++)
            {
                c.Accelerate(20);
            }
            c.DeRegisterHandler(handler3);
            c.DeRegisterHandler(handler1);
            for (int i = 0; i < 5; i++)
            {
                c.Accelerate(-20);
            }
            Console.ReadLine();
        }
        public void Event1(string str1)
        {
            Console.WriteLine("Message from car Event1");
            Console.WriteLine("=> "+str1);
            Console.WriteLine("**********************");
        }
        public void Event2(string str2)
        {
            char[] chr = str2.ToCharArray();
            string reverted = string.Empty;
            for (int i=chr.Length-1; i >= 0; i--)
            {
                reverted += chr[i].ToString();
            }
            Console.WriteLine("Message from car Event2");
            Console.WriteLine("****"+reverted+"*****");
            Console.WriteLine("***********************");
        }
        public void Event3(string str3)
        {
            Console.WriteLine("Message from Car Event3");
            Console.WriteLine("=> "+str3);
            Console.WriteLine("***********************");
        }
    }
    class Car
    {
        //delegation and event registration and de-registration
        public delegate void DelegateCarHandler(string methodToCall);
        private DelegateCarHandler listOfHandlers;
        public void RegisterHandler(DelegateCarHandler handler)
        {
            listOfHandlers += handler;
        }
        public void DeRegisterHandler(DelegateCarHandler handler)
        {
            listOfHandlers -= handler;
        }
        //***********end of delegation and event registration and de-registration****************


        public int CurrentSpeed { get; set; }
        public int MaxSpeed { get; set; }
        public string CarName { get; set; }
        private bool isCarDead;

        public Car()
        {
            MaxSpeed = 100;
        }
        public Car(string name, int currentSpeed, int maxSpeed)
        {
            CarName = name;
            CurrentSpeed = currentSpeed;
            MaxSpeed = maxSpeed;
        }
        public void Accelerate(int pivot)
        {
            if (isCarDead && listOfHandlers != null)
            {
                listOfHandlers("The Car is dead");
            }
            else
            {
                CurrentSpeed += pivot;
                if (10==(MaxSpeed - CurrentSpeed) && listOfHandlers!= null)
                {
                    isCarDead = true;
                    listOfHandlers("Check out, bro you are on verge of fu**ing up your car");
                }
                else
                {
                    Console.WriteLine("your speed is {0} ",CurrentSpeed);
                }
            }
        }
    }
}
