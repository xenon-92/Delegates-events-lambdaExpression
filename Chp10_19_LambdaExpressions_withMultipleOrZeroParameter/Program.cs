using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp10_19_LambdaExpressions_withMultipleOrZeroParameter
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleMath m = new SimpleMath();
            //m.Register((msg,result)=> {
            //    Console.WriteLine("Message {0} :: Result {1}",msg,result);
            //});
            //m.Add(12,25);
            m.MathHandler += ((string s, int i) => {
                Console.WriteLine("Message{0} :: result {1}",s,i);
            });
            m.Add(40,90);
            Console.ReadLine();
        }
    }
    class SimpleMath
    {
        public delegate void MathMessage(string str,int add);
        public event MathMessage MathHandler;

        //public void Register(MathMessage mathX)
        //{
        //    MathHandler += mathX;
        //}
        public void Add(int x, int y)
        {
            if (MathHandler!=null)
            {
                MathHandler("The result of the addition is",x+y);
            }
        }
    }
}
