using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y
{    
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>
            {
                1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16
            };

            Predicate<int> callback = new Predicate<int>(CheckEven);
            List<int> evenNos = list.FindAll(callback);

            List<int> evenNo = list.FindAll(delegate(int i)
            {
                return i % 2 == 0;
            });

            List<int> even = list.FindAll((int i)=>i%2==0);
            
            
        }
        public static bool CheckEven(int number)
        {
            return number % 2 == 0;
        }
    }
}
