using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadScreen
{

    public struct DataPoint
    {
        public PointF Ps;
        public double X;
        public double Y;
        public bool XValid;
        public bool YValid;

    }
    public class Fitter
    {
        public List<DataPoint> fCal;
        public List<DataPoint> fRes;
        public bool LogX;
        public bool LogY;

        public double R;
        public double U;


        public bool CalibrationValid;
        public double[] Beta1;
        public double[] Beta2;


        public Fitter()
        {
            LogX = false;
            LogY = false;
            fCal = new List<DataPoint>();
            fRes = new List<DataPoint>();
        }

        public void RmCalIdx(int i)
        {
            fCal.RemoveAt(i);
        }
        public void RmResIdx(int i)
        {
            fRes.RemoveAt(i);
        }

        public void AddRes(PointF pt)
        {
            DataPoint dp = new DataPoint();
            dp.Ps = pt;
            fRes.Add(dp);
            compute(fRes.Count - 1);
        }

        public void compute(int i)
        {
            var res = fRes.ElementAt(i);
            if (!CalibrationValid)
            {
                res.XValid = false;
                res.YValid = false;
                fRes[i] = res;
                return;
            }
            //   px = b10 + b11*x + b12*y
            //   py = b20 + b21*x + b22*y
            double a = Beta1[1];
            double b = Beta1[2];
            double c = Beta2[1];
            double d = Beta2[2];
            double det = -b * c + a * d;

            double px = res.Ps.X;
            double py = res.Ps.Y;
            px -= Beta1[0];
            py -= Beta2[0];

            double x = (d * px - b * py) / det;
            double y = (-c * px + a * py) / det;

            res.X = LogX ? Math.Exp(x) : x;
            res.Y = LogY ? Math.Exp(y) : y;
            res.XValid = true;
            res.YValid = true;
            fRes[i] = res;
        }

        void Inverse(double [,] mat, double [,] inv)
        {
            double a = mat[0, 0];
            double b = mat[0, 1];
            double c = mat[0, 2];
            double d = mat[1, 0];
            double e = mat[1, 1];
            double f = mat[1, 2];
            double g = mat[2, 0];
            double h = mat[2, 1];
            double i = mat[2, 2];
            double det = -c * e * g + b * f * g + c * d * h - a * f * h - b * d * i + a * e * i;

            inv[0, 0] = -f * h + e * i;
            inv[0, 1] = c * h - b * i;
            inv[0, 2] = -c * e + b * f;
            inv[1, 0] = f * g - d * i;
            inv[1, 1] = -c * g + a * i;
            inv[1, 2] = c * d - a * f;
            inv[2, 0] = -e * g + d * h;
            inv[2, 1] = b * g - a * h;
            inv[2, 2] = -b * d + a * e;

            for (int k = 0; k < 3; ++k)
            {
                for (int j = 0; j < 3; ++j)
                {
                    inv[k, j] /= det;
                }

            }
        }

        void LinearRegression(double[][] x, double[] y, double[] beta)
        {
            int N = y.Length;
            double[] t = new double[3];
            for (int i = 0; i < N; ++i)
            {
                for (int j = 0; j < t.Length; ++j)
                {
                    t[j] += x[i][j] * y[i];
                }
            }

            double[,] mat = new double[t.Length, t.Length];
            double[,] inv = new double[t.Length, t.Length];
            for (int k = 0; k < t.Length; ++k)
            {
                for (int j = 0; j < t.Length; ++j)
                {
                    for (int i = 0; i < N; ++i)
                    {
                        mat[k, j] += x[i][k] * x[i][j];
                    }
                }
            }
            Inverse(mat, inv);

            for (int k = 0; k < t.Length; ++k)
            {
                beta[k] = 0;
                for (int j = 0; j < t.Length; ++j)
                {
                    beta[k] += inv[k, j] * t[j];
                }
            }
        }

        public String Evaluate()
        {
            String err = "Done";
            bool valid = true;
            do {
                int nValid = 0;
                foreach (var p in fCal)
                {
                    nValid += p.XValid && p.YValid ? 1　: 0;
                }
                if (nValid < 3)
                {
                    err = "Too Less Data to Calibrate";
                    valid = false;
                    break;
                }
            } while (false);
            CalibrationValid = valid;

            R = 0;
            U = 0;
            if (CalibrationValid)
            {
                int N = fCal.Count;
                Beta1 = new double[3];
                Beta2 = new double[3];
                double[] y1 = new double[N];
                double[] y2 = new double[N];
                double[][] X = new double[N][];
                for (int i = 0; i < N; ++i)
                {
                    double x0 = 1;
                    double x1 = fCal.ElementAt(i).X;
                    if (LogX) x1 = Math.Log(x1);
                    double x2 = fCal.ElementAt(i).Y;
                    if (LogY) x2 = Math.Log(x2);
                    y1[i] = fCal.ElementAt(i).Ps.X;
                    y2[i] = fCal.ElementAt(i).Ps.Y;
                    X[i] = new double[] { x0, x1, x2 };
                }
                LinearRegression(X, y1, Beta1);
                LinearRegression(X, y2, Beta2);

                double xvx = Beta1[1];
                double xvy = Beta2[1];
                double xr = 1 / Math.Sqrt(xvx * xvx + xvy * xvy);
                double yvx = Beta1[2];
                double yvy = Beta2[2];
                double yr = 1 / Math.Sqrt(yvx * yvx + yvy * yvy);
                double cc = xvx * yvy - xvy * yvx;
                cc = cc * xr * yr;
                if (cc >= 1) cc = 1;
                else if (cc <= -1) cc = -1;
                U = Math.Asin(cc) / Math.PI * 180;
                // Note the defintion of Y axias in pixels coordinates
                U = -U;
            }

            for (int i = 0; i < fRes.Count; ++i)
            {
                compute(i);
            }

            return err;
        }
    }
}
