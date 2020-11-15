using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace nyolcadikhet.Entities
{
    public class Ball : Toy
    {
        protected override void DrawImage(Graphics g)
        {
            g.FillEllipse(BallColor, 0, 0, Width, Height);
        }
        public SolidBrush BallColor { get; private set; }

        public Ball(Color color)
        {
            BallColor = new SolidBrush(color);
        }
        
    }
}
