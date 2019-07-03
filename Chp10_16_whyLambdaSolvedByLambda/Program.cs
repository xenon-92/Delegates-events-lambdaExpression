using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp10_16_whyLambdaSolvedByLambda
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>
            {
                44,2,1,9,8,67,20
            };
            List<int> evenNos = list.FindAll(
                delegate(int i)
                {
                    return i % 2 == 0;
                }
                );
            foreach (var item in evenNos)
            {
                Console.WriteLine(item);
            }
            //********or even in better way**************
            List<int> otherList = new List<int>
            {
                44,2,1,9,8,67,20
            };
            List<int> EvenNo = otherList.FindAll(i => (i % 2) == 0);
            foreach (var item in EvenNo)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }
    }
}
