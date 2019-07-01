using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rough2
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            Cab c = new Cab();
            Cab.DelSpeedHandler delXXX = new Cab.DelSpeedHandler(p.CabEngineEvent);
            c.RegisterSpeed(delXXX);
            for (int i = 0; i < 6; i++)
                c.CheckSpeed(20);
            Console.ReadLine();
        }
        public void CabEngineEvent(string s)
        {
            Console.WriteLine("\n***** Message From Car Object *****");
            Console.WriteLine("=> {0}", s);
            Console.WriteLine("***********************************\n");
        }
    }
    class Cab
    {
        /*delegate*/
        public delegate void DelSpeedHandler(string arg);
        private DelSpeedHandler DelSpeed;
        public void RegisterSpeed(DelSpeedHandler del)
        {
            DelSpeed = del;
        }

        public int CurrentSpeed { get; set; }
        public int MaxSpeed { get; set; }
        public string CabName { get; set; }
        private bool isOverspeed;


        public Cab()
        {
            MaxSpeed = 100;
        }
        public Cab(string name,int maxS,int currS)
        {
            CabName = name;
            MaxSpeed = maxS;
            CurrentSpeed = currS;
        }
        public void CheckSpeed(int speed)
        {
            if (isOverspeed)
            {
                DelSpeed("Car is dead");
            }
            else
            {
                CurrentSpeed = CurrentSpeed + speed;
                if (CurrentSpeed > 120)
                {
                    DelSpeed("car is dead");
                    isOverspeed = true;

                }
                if (CurrentSpeed > 80)
                {
                    DelSpeed("car is overspeeding");
                }                
                else
                {
                    DelSpeed("car is overmax");
                }
            }
        }
    }
}
