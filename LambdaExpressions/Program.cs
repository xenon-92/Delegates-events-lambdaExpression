using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chp10_17_LambdaExpressions
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>
            {
                1,2,3,44,67,89,88,46,34,100
            };
            List<int> evenNo = numbers.FindAll(//anonymous function style
                delegate (int i)
                {
                    return i % 2 == 0;
                }
                );
            //parameter list => statement to process these arguments
            List<int> LambdaImplicit = numbers.FindAll(i=>i%2==0);//lambda style, implicitly typed
            List<int> LambdaExplicit = numbers.FindAll((int i)=>i%2==0);// lambda style,explicitly typed
            //This is the style to be followed, wrapping up of parameter and expression arguments in parenthesis
            List<int> LambdaBestStyle = numbers.FindAll((int i)=>((i%2)==0));
        }
    }
}
