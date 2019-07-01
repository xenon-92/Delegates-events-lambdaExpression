using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp10_6GenericDelegates
{
    public delegate void GenericDelegate<T>(T args);
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            GenericDelegate<string> g1 = new GenericDelegate<string>(p.Event1);
            g1("ruby");
            GenericDelegate<int> g2 = new GenericDelegate<int>(p.Event2);
            g2(11);
            Console.ReadLine();
        }
        public void Event1(string str)
        {
            Console.WriteLine(str.ToUpper());
        }
        public void Event2(int str)
        {
            Console.WriteLine(str+99);
        }
    }
}
