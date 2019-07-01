using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp10SimpleDelegate
{
    //delegate
    public delegate int BinaryOp(int x, int y);
    class Program
    {
        static void Main(string[] args)
        {
            BinaryOp b = new BinaryOp(SimpleMath.Add);
            Console.WriteLine(b(8,9));
            Console.ReadLine();
        }
    }
    public class SimpleMath
    {
        public static int Add(int x, int y)
        {
            return x + y;
        }
        public static int Substract(int x,int y)
        {
            return x - y;
        }
    }
}
