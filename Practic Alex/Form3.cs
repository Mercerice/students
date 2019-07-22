using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practic_Alex
{
    public partial class Form3 : Form
    {

        private List<DataItem> data;
        private Color[] palette = new Color[] { Color.Crimson, Color.DeepPink, Color.Tomato, Color.Yellow, Color.Indigo, Color.Chocolate, Color.Olive, Color.Green, Color.Violet, Color.Lime, Color.DarkSlateBlue };

        public Form3()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
        }
        public int mode = 0;
        public void diagram()
        {
            Form1 main = this.Owner as Form1;
            data = new List<DataItem>();
            float summ = 0;
            float summost = 0;
            for (int j = 0; j < main.dataGridView1.ColumnCount; j++)
                if (main.dataGridView1.Columns[j].HeaderText == "Сред. балл")
                {
                    for (int i = 1; i < 11; i++)
                    {
                        data.Add(new DataItem() { Value = float.Parse(main.dataGridView1.Rows[i - 1].Cells[j].Value.ToString()), Title = main.dataGridView1.Rows[i - 1].Cells[0].Value.ToString()+ "("+ main.dataGridView1.Rows[i - 1].Cells[j].Value.ToString() +")" });
                        summ += float.Parse(main.dataGridView1.Rows[i - 1].Cells[j].Value.ToString());
                    }
                    for (int i = 0; i < main.dataGridView1.RowCount; i++)
                        summost += float.Parse(main.dataGridView1.Rows[i].Cells[j].Value.ToString());
                }
            
            //MessageBox.Show("Сумма оклада самых высокооплачиваемых сотрудников: " + summ + "\nСумма оклада остальных сотрудников: " + summost);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            //считаем коэфф для нормализации
            var k = 360f / data.Sum(di => di.Value);
            //настройка графики
            e.Graphics.TranslateTransform(Width / 2, Height / 2);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //
            var angle = 0f;
            var radius = 100f;
            using (var brush = new SolidBrush(Color.White))
                for (int i = 0; i < data.Count; i++)
                {
                    brush.Color = palette[i % palette.Length];
                    var a = k * data[i].Value;
                    e.Graphics.FillPie(brush, -radius, -radius, radius * 2, radius * 2, angle, a);
                    //
                    var y = radius * (float)Math.Sin(Math.PI * (angle + a / 2) / 180);
                    var x = radius * (float)Math.Cos(Math.PI * (angle + a / 2) / 180);

                    e.Graphics.DrawLine(Pens.Blue, x, y, x * 1.8f, y * 1.8f);
                    var size = e.Graphics.MeasureString(data[i].Title, Font);
                    var rect = new RectangleF(x * 1.9f - size.Width / 2, y * 1.9f - (y > 0 ? 0 : size.Height), size.Width, size.Height);
                    e.Graphics.FillRectangle(Brushes.White, rect);
                    e.Graphics.DrawString(data[i].Title, Font, Brushes.Black, rect);
                    //
                    angle += a;
                }
        }
    }
    class DataItem
    {
        public float Value;
        public string Title;
    }
}
