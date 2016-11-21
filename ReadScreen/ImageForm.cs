using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadScreen
{
    public partial class ImageForm : Form
    {
        Fitter fFitter;
        MainForm fMainForm;
        String fImagePath;
        Image fImage;
        Size fImgSize;


        PointF fOrigin;
        float fScale;


        bool fTracking;
        Point fCurLoc;


        public ImageForm()
        {
            InitializeComponent();
        }

        override protected void OnPaint(PaintEventArgs e)
        {
            draw(e.Graphics);
        }

        void draw(Graphics gr)
        {
            RectangleF dest = default(RectangleF);
            dest.Location = fOrigin;
            dest.Width = fScale * fImgSize.Width;
            dest.Height = fScale * fImgSize.Height;
            
            gr.Clear(Color.White);
            gr.DrawImage(fImage, dest);



            int lenlong = 3;
            Font drawFont = new Font("Arial", 14);
            {
                Pen lnpen = new Pen(Color.Blue, 1);
                SolidBrush drawBrush = new SolidBrush(Color.Blue);


                for (int i = 0; i < fFitter.fCal.Count; ++i)
                {
                    var pt = fFitter.fCal.ElementAt(i).Ps;
                    float x = pt.X * fScale + fOrigin.X;
                    float y = pt.Y * fScale + fOrigin.Y;
                    var l = new PointF(x - lenlong, y);
                    var r = new PointF(x + lenlong, y);
                    var t = new PointF(x, y - lenlong);
                    var b = new PointF(x, y + lenlong);
                    gr.DrawLine(lnpen, l, r);
                    gr.DrawLine(lnpen, t, b);
                    String str = String.Format("C{0}", i);
                    gr.DrawString(str, drawFont, drawBrush, x + lenlong, y + lenlong);
                }
            }
            {
                Pen lnpen = new Pen(Color.Red, 1);
                SolidBrush drawBrush = new SolidBrush(Color.Red);
                for (int i = 0; i < fFitter.fRes.Count; ++i)
                {
                    var pt = fFitter.fRes.ElementAt(i).Ps;
                    float x = pt.X * fScale + fOrigin.X;
                    float y = pt.Y * fScale + fOrigin.Y;
                    var l = new PointF(x - lenlong, y);
                    var r = new PointF(x + lenlong, y);
                    var t = new PointF(x, y - lenlong);
                    var b = new PointF(x, y + lenlong);
                    gr.DrawLine(lnpen, l, r);
                    gr.DrawLine(lnpen, t, b);
                    String str = String.Format("R{0}", i);
                    gr.DrawString(str, drawFont, drawBrush, x + lenlong, y + lenlong);
                }
            }

        }


        public void OnFitterChanged()
        {
            Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            float x = (e.Location.X - fOrigin.X) / fScale;
            float y = (e.Location.Y - fOrigin.Y) / fScale;
            if (e.Button == MouseButtons.Right)
            {
                DataPoint point = default(DataPoint);
                point.Ps.X = x;
                point.Ps.Y = y;
                fFitter.fCal.Add(point);
                Invalidate();
                fMainForm.OnFitterChanged();
            }
            else if (e.Button == MouseButtons.Middle)
            {
                fFitter.AddRes(new PointF(x, y));
                Invalidate();
                fMainForm.OnFitterChanged();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                fTracking = true;
                fCurLoc = e.Location;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (fTracking)
            {
                fOrigin.X += e.Location.X - fCurLoc.X;
                fOrigin.Y += e.Location.Y - fCurLoc.Y;
                fCurLoc = e.Location;

                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            fTracking = false;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            float oldScale = fScale;
            float newScale = (float)(oldScale * Math.Exp(0.15 * e.Delta / SystemInformation.MouseWheelScrollDelta));

            do
            {
                if (newScale < oldScale && oldScale <= 0.1) break;
                if (newScale > oldScale && oldScale >= 100) break;
                fScale = newScale;
                //System.Console.WriteLine(fScale);
                fOrigin.X = (float)((fOrigin.X - e.X) * fScale / oldScale) + e.X;
                fOrigin.Y = (float)((fOrigin.Y - e.Y) * fScale / oldScale) + e.Y;

            } while (false);
            Invalidate();
        }


        public void postInit(String fileName, Fitter fitter, MainForm mainForm)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            ResizeRedraw = true;

            fMainForm = mainForm;
            fImagePath = fileName;
            fImage = new Bitmap(fImagePath);
            fImgSize = fImage.Size;

            fOrigin = default(PointF);
            fScale = 1;

            fFitter = fitter;
        }
    }
}
