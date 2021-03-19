using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace chordprogression
{
    class ColorChange
    {

        public int degree()
        {
            return 1;
        }

        private void colorChangeOfButton(Form1 form1, int degree)
        {
            form1.buttonList[0].BackColor = Color.FromArgb(255, 0, 255 - degree); //こうやって適応度関数の値に応じてコードの色を変えることができる
        }
    }
}
