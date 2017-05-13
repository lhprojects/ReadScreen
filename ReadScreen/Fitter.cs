using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadScreen
{

    public class IFitter
    {
        public double fXYAngle;
        public bool fLogX;
        public bool fLogY;
        public Matrix fQ; // Q(xp,yp)^T + d = (x,y)^T
        public Matrix fd;
        public List<ResultPoint> fResults = new List<ResultPoint>();
        public List<CalPoint> fCal = new List<CalPoint>();
        public bool fCalibrationValid;
        public Dictionary<String, double> fInitialValues = new Dictionary<String, double>();

        public virtual String Evaluate() { throw new Exception(); }

        public void RmCalIdx(int i)
        {
            fCal.RemoveAt(i);
        }
        public void RmResIdx(int i)
        {
            fResults.RemoveAt(i);
        }

        public void AddResult(PointF pt)
        {
            ResultPoint dp = new ResultPoint(pt);
            OnCompute(dp);
            fResults.Add(dp);
        }

        public void AddCal(PointF pt)
        {
            CalPoint dp = new CalPoint(pt, null, null);
            fCal.Add(dp);
        }

        public virtual void OnCompute(ResultPoint dp)
        {
            if (!fCalibrationValid)
            {
                dp.XValid = false;
                dp.YValid = false;
                return;
            }

            Matrix p = new Matrix(2, 1);
            p[0, 0] = dp.Ps.X;
            p[1, 0] = dp.Ps.Y;
            Matrix v = fQ * p + fd;
            dp.X = v[0, 0];
            dp.Y = v[1, 0];

            dp.X = fLogX ? Math.Exp(dp.X) : dp.X;
            dp.Y = fLogY ? Math.Exp(dp.Y) : dp.Y;
            dp.XValid = true;
            dp.YValid = true;
        }

        public IFitter()
        {
            fLogX = false;
            fLogY = false;
        }

    }

    public class ResultPoint
    {
        public ResultPoint(PointF ps) { Ps = ps; XValid = false; YValid = false; }

        public PointF Ps; // in pixels
        public double X;
        public double Y;
        public bool XValid;
        public bool YValid;

    }

    public class CalPoint
    {
        public CalPoint(PointF ps, String x, String y)
        {
            Ps = ps;
            fX = x;
            fY = y;
        }

        public PointF Ps; // in pixels
        public String fX;
        public String fY;

    }


    public class Fitter : IFitter
    {

        public Fitter()
        {
        }


        // https://en.wikipedia.org/wiki/Linear_regression
        static void LinearRegression(double[/*N*/,/*p*/] X, double[/*N*/] y, double[/*p*/] beta)
        {
            int N = y.Length;
            int p = beta.Length;
            Matrix X_ = new Matrix(N, p);
            X_.mat = (double[,])X.Clone();
            Matrix y_ = new Matrix(N, 1);
            for (int i = 0; i < N; ++i) y_[i, 0] = y[i];

            Matrix XT_ = Matrix.Transpose(X_);
            Matrix beta_ = (XT_ * X_).Invert() * (XT_ * y_);

            for (int i = 0; i < p; ++i) beta[i] = beta_[i, 0];

        }

        override public String Evaluate()
        {
            fCalibrationValid = false;
            String err = "Done";

            var Beta1 = new double[3];
            var Beta2 = new double[3];

            int N = fCal.Count;
            double[,] X = new double[N, 3];

            bool linear = true;
            try
            {
                int i = 0;
                foreach (var c in fCal)
                {
                    X[i, 1] = Convert.ToDouble(c.fX);
                    X[i, 2] = Convert.ToDouble(c.fY);
                    ++i;
                }
            }
            catch (Exception)
            {
                linear = false;
            }


            if (linear && false)
            {
                if (N < 3)
                {
                    err = "Too less calibration points!";
                    return err;
                }

                double[] y1 = new double[N];
                double[] y2 = new double[N];
                for (int i = 0; i < N; ++i)
                {
                    X[i, 0] = 1;
                    if (fLogX) X[i, 1] = Math.Log(X[i, 1]);
                    if (fLogX) X[i, 2] = Math.Log(X[i, 2]);
                    y1[i] = fCal.ElementAt(i).Ps.X;
                    y2[i] = fCal.ElementAt(i).Ps.Y;
                }
                LinearRegression(X, y1, Beta1);
                LinearRegression(X, y2, Beta2);
                fCalibrationValid = true;
            }
            else
            {
                try
                {

                    Calculator cal = new Calculator(fInitialValues);
                    var pxx = cal.ParseExpression("@pxx");
                    var pxy = cal.ParseExpression("@pxy");
                    var pyx = cal.ParseExpression("@pyx");
                    var pyy = cal.ParseExpression("@pyy");
                    var px0 = cal.ParseExpression("@px0");
                    var py0 = cal.ParseExpression("@py0");
                    Expression chi2 = null;
                    foreach (var c in fCal)
                    {
                        var px = new NumberExpression(c.Ps.X);
                        var py = new NumberExpression(c.Ps.Y);
                        var x = cal.ParseExpression(c.fX);
                        var y = cal.ParseExpression(c.fY);
                        var c2 = ((pxx * x + pxy * y + px0 - px) ^ 2) + ((pyx * x + pyy * y + py0 - py) ^ 2);
                        if (chi2 == null) chi2 = c2;
                        else chi2 = chi2 + c2;
                    }
                    cal.SetRootExpression(chi2);
                    Minimizer minzer = new Minimizer(cal);
                    double mn;
                    var result = minzer.Minimize(out mn);
                    Beta1[0] = result["@px0"];
                    Beta1[1] = result["@pxx"];
                    Beta1[2] = result["@pxy"];
                    Beta2[0] = result["@py0"];
                    Beta2[1] = result["@pyx"];
                    Beta2[2] = result["@pyy"];
                    fCalibrationValid = true;
                    err = "chi2 = " + mn.ToString();
                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e.StackTrace);
                    err = "Minization error";
                }
            }

            if(fCalibrationValid){
                // xp = b10 + b1x*x + b1y*y
                // yp = b20 + b2x*x + b2y*y
                // (xp,yp)^T =  (b10,b20)^T + (b1x, b1y // b2x b2y)(x,y)^T
                var m = new Matrix(2, 2);
                m[0, 0] = Beta1[1];
                m[0, 1] = Beta1[2];
                m[1, 0] = Beta2[1];
                m[1, 1] = Beta2[2];
                var d = new Matrix(2, 1);
                d[0, 0] = Beta1[0];
                d[1, 0] = Beta2[0];
                try
                {
                    fQ = m.Invert();
                } catch(Exception) {
                    err = "invert wrong";
                    return err;
                }
                fd = fQ * (-d);

                double xvx = Beta1[1];
                double xvy = Beta2[1];
                double xr = 1 / Math.Sqrt(xvx * xvx + xvy * xvy);
                double yvx = Beta1[2];
                double yvy = Beta2[2];
                double yr = 1 / Math.Sqrt(yvx * yvx + yvy * yvy);
                double cc = xvx * yvy - xvy * yvx;
                cc = cc * xr * yr;

                fXYAngle = 0;
                if (cc >= 1) cc = 1;
                else if (cc <= -1) cc = -1;
                fXYAngle = Math.Asin(cc) / Math.PI * 180;
                // Note the defintion of Y axias in pixels coordinates
                fXYAngle = -fXYAngle;

                foreach (var r in fResults)
                {
                    OnCompute(r);
                }
            }

            return err;
        }
    }
}
