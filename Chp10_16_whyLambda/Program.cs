using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp10_16_whyLambda
{
    
    class Program
    {
        static void Main(string[] args)
        {
            TraditionalDelegateSyntax();
            Console.ReadLine();
        }
        static void TraditionalDelegateSyntax()
        {
            List<int> listInt = new List<int>
            {
                20,1,4,8,9,44
            };
            //call list.FindAll() using traditional delegate syntax
            Predicate<int> callback = new Predicate<int>(IsEvenNo);
            List<int> evenNo = listInt.FindAll(callback);
            foreach (var item in evenNo)
            {
                Console.WriteLine(item);
            }
        }
        //target for predicate syntax
        static bool IsEvenNo(int i)
        {
            return (i % 2) == 0;
        }
    }
}
