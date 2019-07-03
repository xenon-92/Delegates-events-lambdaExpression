﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace X
{
    // a class to hold the information about the event
    // in this case it will hold only information
    // available in the clock class, but could hold
    // additional state information
    public class TimeInfoEventArgs : EventArgs
    {
        public int hour;
        public int minute;
        public int second;

        public TimeInfoEventArgs(int hour, int minute, int second)
        {
            this.hour = hour;
            this.minute = minute;
            this.second = second;
        }
    }

    // The publisher: the class that other classes
    // will observe. This class publishes one delegate:
    // SecondChangeHandler.
    public class Clock
    {
        private int hour;
        private int minute;
        private int second;

        // the delegate the subscribers must implement
        public delegate void SecondChangeHandler(object clock,
                             TimeInfoEventArgs timeInformation);

        // an instance of the delegate
        public SecondChangeHandler SecondChanged;

        // set the clock running
        // it will raise an event for each new second
        public void Run()
        {
            for (; ; )
            {
                // sleep 100 milliseconds
                Thread.Sleep(100);
                // get the current time
                System.DateTime dt = System.DateTime.Now;
                // if the second has changed
                // notify the subscribers
                if (dt.Second != second)
                {
                    // create the TimeInfoEventArgs object
                    // to pass to the subscriber
                    TimeInfoEventArgs timeInformation =
                         new TimeInfoEventArgs(dt.Hour, dt.Minute, dt.Second);

                    // if anyone has subscribed, notify them
                    if (SecondChanged != null)
                    {
                        SecondChanged(this, timeInformation);
                    }
                }

                // update the state
                this.second = dt.Second;
                this.minute = dt.Minute;
                this.hour = dt.Hour;
            }
        }
    }

    // A subscriber: DisplayClock subscribes to the
    // clock's events. The job of DisplayClock is
    // to display the current time
    public class DisplayClock
    {
        // given a clock, subscribe to
        // its SecondChangeHandler event
        public void Subscribe(Clock theClock)
        {
            theClock.SecondChanged +=
                 new Clock.SecondChangeHandler(TimeHasChanged);
        }

        // the method that implements the
        // delegated functionality
        public void TimeHasChanged(object theClock, TimeInfoEventArgs ti)
        {
            Console.WriteLine("Current Time: {0}:{1}:{2}",
              ti.hour.ToString(), ti.minute.ToString(), ti.second.ToString());
        }
    }
    // a second subscriber whose job is to write to a file
    public class LogCurrentTime
    {
        public void Subscribe(Clock theClock)
        {
            theClock.SecondChanged +=
                  new Clock.SecondChangeHandler(WriteLogEntry);
        }

        // this method should write to a file
        // we write to the console to see the effect
        // this object keeps no state
        public void WriteLogEntry(object theClock, TimeInfoEventArgs ti)
        {
            Console.WriteLine("Logging to file: {0}:{1}:{2}",
               ti.hour.ToString(), ti.minute.ToString(), ti.second.ToString());
        }
    }

    public class Tester
    {
        public void Run()
        {
            // create a new clock
            Clock theClock = new Clock();

            // create the display and tell it to
            // subscribe to the clock just created
            DisplayClock dc = new DisplayClock();
            dc.Subscribe(theClock);

            // create a Log object and tell it
            // to subscribe to the clock
            LogCurrentTime lct = new LogCurrentTime();
            lct.Subscribe(theClock);

            // Get the clock started
            theClock.Run();
        }
    }

    public class Program
    {
        public static void Main()
        {
            Tester t = new Tester();
            t.Run();
        }
    }
}

