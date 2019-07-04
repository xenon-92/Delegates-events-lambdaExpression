using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp10_18_LambdaExpressions_MultipleStatements
{
    
    class Program
    {
        
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>
            {
                5,6,7,33,2,1,2567,89,90,100
            };
            //processing each argument within a group of statement            
            List<int> evenNumbers = numbers.FindAll((int i)=>
            {
                Console.WriteLine("Value of i is Currently {0}",i);
                bool isEven = ((i % 2) == 0);
                if (isEven)
                {
                    Console.WriteLine("i={0} is even number",i);
                }
                return isEven;
            }
            );
            /*
             In this case our parameter list (a single integer) is being processed by a set of code statements.
             the statement are broken down in to two code statement for better readability.
             bool isEven = ((i % 2) == 0);
             return isEven;
             */
        }
    }
}
