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
            m.MathHandler += ((string str, int i) => {
                Console.WriteLine("Message{0} :: result {1}",str,i);
            });
            m.Add(40,90);
            /************************************************************/
            Pub p = new Pub();
            p.handler += ((kp)=> {
                Console.WriteLine(kp);
                return kp.GetHashCode();
            });
             p.ActionEvent();
            /*---uncommnet to see sub class in action
            Sub s = new Sub();
            s.Register(p);
            p.ActionEvent();
            Console.ReadLine();
            */
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
    class Pub
    {
        public delegate int Delhandler(string str);
        public event Delhandler handler;
        public void ActionEvent()
        {
            if (handler!=null)
            {
               int xp =  handler("Some kind of love");
                Console.WriteLine(Convert.ToString(xp));
            }
        }
    }
    class Sub
    {
        public void Register(Pub p)
        {
            p.handler += ((str)=> {
                Console.WriteLine(str);
                return str.GetHashCode();
            });
        }
    }
}
