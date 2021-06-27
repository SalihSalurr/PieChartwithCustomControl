using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proje2
{
    public partial class My2DPie : Control
    {
        public My2DPie()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Control sınıfındaki OnPaint metodu override edilerek kontrolün görsel görünümü metot içerisinde gerçekleştiriliyor.
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            float d1 = 0, d2, d3, d4, d5, d6, total;

            // Textbox içerisinde değer yoksa 0 varsa gelen değer değişkene eşitleniyor.
            d1 = textBox1.Text == "" ? 0 : int.Parse(textBox1.Text);       
            d2 = textBox2.Text == "" ? 0 : int.Parse(textBox2.Text);
            d3 = textBox3.Text == "" ? 0 : int.Parse(textBox3.Text);
            d4 = textBox4.Text == "" ? 0 : int.Parse(textBox4.Text);
            d5 = textBox5.Text == "" ? 0 : int.Parse(textBox5.Text);
            d6 = textBox6.Text == "" ? 0 : int.Parse(textBox6.Text);

            // Yüzdelik hesabı yapmak için toplanıyor.
            total = d1 + d2 + d3 + d4 + d5 + d6;                           

            float pd1, pd2, pd3, pd4, pd5, pd6;

            // Pastanın çevresine göre yüzdelik hesabı yapıyor.
            pd1 = (d1 / total) * 360;                                      
            pd2 = (d2 / total) * 360;
            pd3 = (d3 / total) * 360;
            pd4 = (d4 / total) * 360;
            pd5 = (d5 / total) * 360;
            pd6 = (d6 / total) * 360;

            Pen p = new Pen(Color.Black, 4);

            Graphics g = this.CreateGraphics();

            //Textbox1 in lokasyonuna göre grafiğe lokasyon alarak çizdiriyor.
            Rectangle rec = new Rectangle(textBox1.Location.X + textBox1.Size.Width + 60, 50, 250, 250);

            // Her bir pasta değeri için farklı renk belirleniyor.
            Brush b1 = new SolidBrush(Color.Pink);                         
            Brush b2 = new SolidBrush(Color.Aqua);
            Brush b3 = new SolidBrush(Color.Yellow);
            Brush b4 = new SolidBrush(Color.Orange);
            Brush b5 = new SolidBrush(Color.DarkRed);
            Brush b6 = new SolidBrush(Color.Magenta);

            g.Clear(Form1.DefaultBackColor);

            // İlk değer sıfır değilse grafiği çizdirmeye başlıyor.
            if (!(d1 == 0))         
            {
                // Kontrolün içerisindeki MyDrawPie metodu ile Pen, Rectangle, başlangıç ve bitiş açısını göndrerek dilimin sınırları çizdiriliyor.
                MyDrawPie(p, rec, 0, pd1);
                // Graphics sınıfının FillPie metodu ile Brush, Rectangle, başlangıç ve bitiş açısını göndrerek dilimin içi boyanıyor.
                g.FillPie(b1, rec, 0, pd1);                                 
                g.DrawPie(p, rec, pd1, pd2);
                g.FillPie(b2, rec, pd1, pd2);
                MyDrawPie(p, rec, pd1 + pd2, pd3);
                g.FillPie(b3, rec, pd1 + pd2, pd3);
                MyDrawPie(p, rec, pd1 + pd2 + pd3, pd4);
                g.FillPie(b4, rec, pd1 + pd2 + pd3, pd4);
                MyDrawPie(p, rec, pd1 + pd2 + pd3 + pd4, pd5);
                g.FillPie(b5, rec, pd1 + pd2 + pd3 + pd4, pd5);
                MyDrawPie(p, rec, pd1 + pd2 + pd3 + pd4 + pd5, pd6);
                g.FillPie(b6, rec, pd1 + pd2 + pd3 + pd4 + pd5, pd6);
            }
            
        }


        /// <summary>
        /// Bu metot içerisinde pastanın dış çemberi ve dilimleri belirleniyor. 
        /// </summary>
        /// <param name="pen">Çizim yapabilmek için gereken sınıf.</param>
        /// <param name="rect">Pastanın hangi sınırlar içerisinde ve kontrolün hangi kısmında duracağının parametresi.</param>
        /// <param name="startAngle">Çizilecek olan pasta dilimlerinin her birinin başlangıç açısı.</param>
        /// <param name="sweepAngle">Çizilecek olan pasta dilimlerinin her birinin bitiş açısı.</param>
        public void MyDrawPie(System.Drawing.Pen pen, System.Drawing.Rectangle rect, float startAngle, float sweepAngle)
        {
            int startX1, endX1, startY1, endY1, LineLength;

            // Başlangıç kordinatlarını bulmak için
            startX1 = rect.X + rect.Width / 2;                          
            startY1 = rect.Y + rect.Height / 2;
            // Yarıçapı bulmak için
            LineLength = rect.Width / 2;                                

            double DEGREES_TO_RADIANS = Math.PI / 180.0;
            // Bitiş kordinatlarını bulmak için
            endX1 = startX1 + (int)(LineLength * Math.Cos(startAngle * DEGREES_TO_RADIANS));      
            endY1 = startY1 + (int)(LineLength * Math.Sin(startAngle * DEGREES_TO_RADIANS));


            Graphics g = this.CreateGraphics();

            // Graphics sınıfının DrawLine metoduna başlangıç ve bitiş kordinatlaro gönderilerek pastanın yan çizgileri çizdiriliyor.
            g.DrawLine(pen, startX1, startY1, endX1, endY1);
            // Graphics sınıfının DrawLarc metoduna başlangıç ve bitiş açıları gönderilerek pastanın arka yüzeyi çizdiriliyor. 
            g.DrawArc(pen, rect, startAngle, sweepAngle);                

        }
    }
}
