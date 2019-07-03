using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Chp10_11Events_InBroaderSense
{
    class Program
    {
        static void Main(string[] args)
        {
            Tester t = new Tester();
            t.Run();
        }
    }
    class TimeInfoEventArgs : EventArgs // the real data
    {
        public int hour;
        public int minute;
        public int second;

        public TimeInfoEventArgs(int hour,int minute,int second)
        {
            this.hour = hour;
            this.minute = minute;
            this.second = second;
        }
    }
    //The Publisher Class: the class that other classes will observe
    //this class publishes one delegate SecondChangeHandler
    class Clock
    {
        //the delegate that subscribers  must implement
        public delegate void SecondChangeHandler(object Clock, TimeInfoEventArgs timeInformation);
        //an instance of the delegate
        public SecondChangeHandler listOfHandlers;


        int hour;
        int minute;
        int second;

        //the event raising method
        public void Run()
        {
            for (; ; )
            {
                Thread.Sleep(100);
                //getting the current time
                DateTime dt = DateTime.Now;
                //if the second has changed, notify the Subscriber
                if (dt.Second!=second)
                {
                    TimeInfoEventArgs timeInfo = new TimeInfoEventArgs(dt.Hour, dt.Minute, dt.Second);
                    //if any one has subscribed, then notify them
                    if (listOfHandlers!=null)
                    {
                        listOfHandlers(this, timeInfo);
                    }
                }
                //update the state
                this.hour = dt.Hour;
                this.minute = dt.Minute;
                this.second = dt.Second;
            }
        }
    }
    //Subscriber Class1
    class DisplayClock
    {
        //given a clock subscribe to its SecondChangeHandler event
        //it is a helper method that is used to subscribe to the clock's SecondChanged{Publisher's delegate} delegate
        public void Subscribe(Clock theClock)
        {
            Clock.SecondChangeHandler handler1 = new Clock.SecondChangeHandler(TimeHasChanged);
            theClock.listOfHandlers += handler1;
        }
        //the method that implements the delegated functionality
        //it is an event handler
        public void TimeHasChanged(object clock,TimeInfoEventArgs ti)
        {
            Console.WriteLine("Current time is {0}:{1}:{2}",ti.hour,ti.minute,ti.second);
        }
    }
    //subscriber class2
    class LogCurrentTime
    {
        //subscribe to publisher's delegate
        public void Subscribe(Clock theClock)
        {
            theClock.listOfHandlers += new Clock.SecondChangeHandler(WriteLogEntry);
        }
        //event handler
        public void WriteLogEntry(object clock,TimeInfoEventArgs ts)
        {
            Console.WriteLine("Logging into the file {0}:{1}:{2}", ts.hour, ts.minute, ts.second);
        }
    }
    class Tester
    {
        public void Run()
        {
            Clock theClock = new Clock();//publisher class

            DisplayClock dc = new DisplayClock();//subscriber class
            dc.Subscribe(theClock);//instance of Publisher class just created[theClock] is subscribed by the Subscriber Instance

            LogCurrentTime lt = new LogCurrentTime();
            lt.Subscribe(theClock);

            //Run the Clock's Run Method
            theClock.Run();
        }
    }
}
