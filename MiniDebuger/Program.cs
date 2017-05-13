using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniDebuger
{
    class Program
    {
        static void Main(string[] args)
        {
            String str = "x^2+2*x*y+3*x-y+2*y^2+z^2+0.5*x*z+w+w^4/(1+w^2)";
            var init = new Dictionary<string, double> { { "x", 0 }, { "y", 0 }, { "w",-0.5}, { "z", -0.1} };

            var cal = new ReadScreen.Calculator(init);
            cal.SetRootExpression(str);
            var minzer = new ReadScreen.Minimizer(cal);
            double min;
            minzer.Minimize(out min);


        }
    }
}
