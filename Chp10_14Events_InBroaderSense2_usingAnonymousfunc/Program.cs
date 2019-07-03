using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chp10_14Events_InBroaderSense2_usingAnonymousfunc//this program doesnt 
//uses Anonymous function please refer to next Chp10_15Events... sln name
{
    class Program
    {
        static void Main(string[] args)
        {
            FireUp fp = new FireUp();
            fp.Run();
        }
    }
    class TimeEventArgs : EventArgs
    {
      public  int hour;
      public  int minute;
      public  int second;
        public TimeEventArgs(int hour,int minute,int second)
        {
            this.hour = hour;
            this.minute = minute;
            this.second = second;
        }
    }
    //publisher class
    //publishes a delegate to which subscriber subscribe
    class Clock
    {
        public delegate void SecondChangeHandler(object clock,TimeEventArgs eventArgs);
        public event SecondChangeHandler listOfHandlers;

        public int hour;
        public int minute;
        public int second;

        public void Run()
        {
            for (; ;)
            {
                Thread.Sleep(200);
                DateTime dt = DateTime.Now;
                if (dt.Second!=second)
                {
                    TimeEventArgs ta = new TimeEventArgs(dt.Hour,dt.Minute,dt.Second);
                    if (listOfHandlers!=null)
                    {
                        listOfHandlers(this, ta);
                    }
                }
                this.hour = dt.Hour;
                this.minute = dt.Minute;
                this.second = dt.Second;
            }
        }
    }
    //subscriber class1
    class DisplayTime
    {
        //helper method to subscribe to publisher's delegate
        public void Subscribe(Clock clock)
        {
            Clock.SecondChangeHandler handler = new Clock.SecondChangeHandler(ShowDisplayTime);
            clock.listOfHandlers += handler;
        }
        //event handler to implement publishers delegate
        public void ShowDisplayTime(object clock, TimeEventArgs args)
        {
            Console.WriteLine("The time is {0}::{1}::{2}", args.hour,args.minute,args.second);
        }
    }
    //subscriber class2
    class TimeLogger
    {
        //helper method to subscribe to publisher's delegate
        public void Subscribe(Clock clock)
        {
            clock.listOfHandlers += new Clock.SecondChangeHandler(LogInfo);
            //delegate (object clck,TimeEventArgs args)
            //{
            //    Console.WriteLine("Logging the time infos ==> Time {0}::{1}::{2}", args.hour, args.minute, args.second);
            //};
        }
        //wvwnt handler to implement the publisher's delegate
        public void LogInfo(object clock, TimeEventArgs args)
        {
            Console.WriteLine("Logging the time infos ==> Time {0}::{1}::{2}", args.hour, args.minute, args.second);
        }
    }
    class FireUp
    {
        public void Run()
        {
            Clock clock = new Clock();

            DisplayTime d = new DisplayTime();
            d.Subscribe(clock);

            TimeLogger t = new TimeLogger();
            t.Subscribe(clock);

            clock.Run();
        }
    }
    

    
}
