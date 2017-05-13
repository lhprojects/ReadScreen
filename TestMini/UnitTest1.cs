using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TestMini
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            String str = "x^2-2*x+1";
            var init = new Dictionary<string, double>{ { "x", 0 } };
            var cal = new ReadScreen.Calculator(init);
            cal.SetRootExpression(str);
            var minzer = new ReadScreen.Minimizer(cal);
            double min;
            minzer.Minimize(out min);
            Assert.IsTrue(Math.Abs(min - 0) < 1E-6);
        }

        [TestMethod]
        public void TestMethod2()
        {
            String str = "x^2+x*x+x*x*x*x+2*x";
            var init = new Dictionary<string, double> { { "x", 0 } };
            var cal = new ReadScreen.Calculator(init);
            cal.SetRootExpression(str);
            var minzer = new ReadScreen.Minimizer(cal);
            double min;
            minzer.Minimize(out min);
            Assert.IsTrue(Math.Abs(min - -0.456129) < 1E-5);
        }

        [TestMethod]
        public void TestMethod3()
        {
            String str = "x^2+2*x*y+3*x-y+2*y^2";
            var init = new Dictionary<string, double> { { "x", 0 }, { "y", 0} };
            var cal = new ReadScreen.Calculator(init);
            cal.SetRootExpression(str);
            var minzer = new ReadScreen.Minimizer(cal);
            double min;
            minzer.Minimize(out min);
            Assert.IsTrue(Math.Abs(min - -6.25) < 1E-5);
        }

        [TestMethod]
        public void TestMetho4()
        {
            String str = "x^2+2*x*y+3*x+(-y+2*y^2)";
            var init = new Dictionary<string, double> { { "x", 0 }, { "y", 0 } };
            var cal = new ReadScreen.Calculator(init);
            cal.SetRootExpression(str);
            var minzer = new ReadScreen.Minimizer(cal);
            double min;
            minzer.Minimize(out min);
            Assert.IsTrue(Math.Abs(min - -6.25) < 1E-5);
        }

        [TestMethod]
        public void TestMetho5()
        {
            String str = "x^2+2*x*y+3*x-y+2*y^2+z^2+0.5*x*z+w+w^4/(1+w^2)";
            var init = new Dictionary<string, double> { { "x", 0 }, { "y", 0 }, { "z", 1}, { "w", -0.5} };
            var cal = new ReadScreen.Calculator(init);
            cal.SetRootExpression(str);
            var minzer = new ReadScreen.Minimizer(cal);
            double min;
            minzer.Minimize(out min);
            Assert.IsTrue(Math.Abs(min - -7.67525) < 1E-5);
        }

    }

}
