using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capstone_Application
{
    /// <summary>
    /// Inherits from PictureBox; adds Interpolation Mode Setting
    /// </summary>
    public class PixelBox : PictureBox
    {

        public PixelBox()
        {
            InterpolationMode = InterpolationMode.Default;
        }
        public InterpolationMode InterpolationMode { get; set; }

        protected override void OnPaint(PaintEventArgs paintEventArgs)
        {
            //if (this.InvokeRequired)
            //{
                Invoke(new Action(() => InvokePaint(paintEventArgs)));
            //}
            //else
            //{
            //    paintEventArgs.Graphics.InterpolationMode = InterpolationMode;
            //    paintEventArgs.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
            //    base.OnPaint(paintEventArgs);
            //}
        }

        void InvokePaint(PaintEventArgs paintEventArgs)
        {
            paintEventArgs.Graphics.InterpolationMode = InterpolationMode;
            paintEventArgs.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
            base.OnPaint(paintEventArgs);
        }
    }
}
