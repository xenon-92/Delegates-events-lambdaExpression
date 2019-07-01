using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp10_2DelegateObject
{
    class Program
    {
        //delegate
        public delegate int binaryOp(int x,int y);

        static void Main(string[] args)
        {
            binaryOp b = new binaryOp(SimpleMath.Add);
            b.Invoke(5,4);
            SimplMath m = new SimplMath();
            binaryOp bm = new binaryOp(m.Mul);
            DisplayDelegateInfo(b);
            DisplayDelegateInfo(bm);
            bm(4,14);
            Console.ReadLine();
        }
        static void DisplayDelegateInfo(Delegate delobj)
        {
            foreach (Delegate d in delobj.GetInvocationList())
            {
                Console.WriteLine("The method is "+d.Method);// names of the method(s) maintained by a delegate 
                Console.WriteLine("The target is "+d.Target);// name of the class defining the method
                /*This doesnt print any thing for d.Target when {delobj = b}because, the method is static and there is no object to reference*/
                /*But it prints for the other method*/
            }
        }
    }
    public class SimpleMath
    {
        public static int Add(int x, int y)
        {
            return x + y;
        }
        public static int Substract(int x, int y)
        {
            return x - y;
        }
    }
    public class SimplMath
    {
        public int Mul(int x, int y)
        {
            return x * y;
        }
    }
}
