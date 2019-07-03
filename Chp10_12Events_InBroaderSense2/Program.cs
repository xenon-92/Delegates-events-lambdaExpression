using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chp10_12Events_InBroaderSense2
{
    class Program
    {
        static void Main(string[] args)
        {
            FireUp fp = new FireUp();
            fp.Run();
        }
    }
    class TimeEventArgs: EventArgs
    {
        public int hour;
        public int second;
        public int minute;
        public TimeEventArgs(int hour, int second, int minute)
        {
            this.hour = hour;
            this.minute = minute;
            this.second = second;
        }
    }
    //publisher class
    // has a delegate, that it publishes
    class Clock
    {
        //subscriber must implement this deleagte
        public delegate void SecondChangeHandler(object clock,TimeEventArgs args);
        public event SecondChangeHandler listOfHandlers;

        private int hour;
        private int minute;
        private int second;

        public void Run()
        {
            for (;;)
            {
                Thread.Sleep(100);
                DateTime dt = DateTime.Now;
                //notify to the subscriber
                if (dt.Second!=second)
                {
                    TimeEventArgs args = new TimeEventArgs(dt.Hour,dt.Second,dt.Minute);
                    if (listOfHandlers!=null)//if anyone has subscribed then notify them;
                    {
                        listOfHandlers(this,args);
                    }
                }
                this.hour = dt.Hour;
                this.minute = dt.Minute;
                this.second = dt.Second;
            }
        }
    }
    //Subscriber class1
    class ShowTime
    {
        //helper method to subscribe the publishers delegate
        public void Subscribe(Clock clock)
        {
            Clock.SecondChangeHandler handler1= new Clock.SecondChangeHandler(TimeHasChanged);
            clock.listOfHandlers += handler1;
        }
        //event handler, whose signature matches with the delegate, and implements the publisher's deleagted functionality
        public void TimeHasChanged(object clock, TimeEventArgs args)
        {
            Console.WriteLine("The Time is {0}::{1}::{2}",args.hour,args.minute,args.second);
        }
    }
    //Subscriber class2
    class LogTime
    {
        //helper method to subscribe the publishers delegate
        public void Subscribe(Clock clock)
        {
            Clock.SecondChangeHandler handler2 = new Clock.SecondChangeHandler(LoggingTime);
            clock.listOfHandlers += handler2;
        }
        //event handler that matches signature with publisher's deleagte and implements publisher's delegate
        public void LoggingTime(object clock,TimeEventArgs args)
        {
            Console.WriteLine("Logging in the logger, logging time => {0}::{1}::{2}",args.hour,args.minute,args.second);
        }
    }
    class FireUp
    {
        public void Run()
        {
            Clock clock = new Clock();

            ShowTime st = new ShowTime();
            st.Subscribe(clock);

            LogTime lt = new LogTime();
            lt.Subscribe(clock);

            clock.Run();
        }
    }
}
