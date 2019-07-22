using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Data.SQLite;

namespace Practic_Alex
{

    public partial class Form4 : Form
    {

        public Form4()
        {
            InitializeComponent();
        }
        Form1 main;
        class mylistelem
        {
            public string Faq;
            public string Kurs;
            public string God;
            public int Bad;
            public int Good;
            public int Nice;
            public mylistelem Next;
            public mylistelem(mylistelem next, int bad, int good, int nice, string faq, string kurs, string god)
            {
                Next = next;
                Bad = bad;
                Good = good;
                Nice = nice;
                Faq = faq;
                Kurs = kurs;
                God = god;
            }

        }
        static int cnt = 0;
        int mode = 1;
        class mylist
        {
            public mylistelem _head = null;
            public mylistelem _current = null;
            public mylistelem first(int bad, int good, int nice, string faq, string kurs, string god)
            {
                mylistelem temp = new mylistelem(null, bad, good, nice, faq, kurs, god);
                if (_head == null)
                {
                    cnt = 0;
                    _head = temp;
                    _current = _head;
                }
                else
                {
                    _current.Next = temp;
                    _current = temp;
                    _current.Next = null;
                }
                cnt++;
                return temp;
            }
        }
        mylistelem current;
        mylist list;
        public void paint()
        {
            main = this.Owner as Form1;
            list = new mylist();
            current = null;
            list._head = null;
            list._current = null;
            switch (mode) {
                case 1:
            for (int i = 0; i < main.dataGridView1.Rows.Count; i++)
            {
                bool nice = true;
                if (list._head != null)
                {
                    current = list._head;
                    while (current != null)
                    {
                        if (current.Faq == main.dataGridView1.Rows[i].Cells[3].Value.ToString())
                        {
                            nice = false;
                            if (int.Parse(main.dataGridView1.Rows[i].Cells[7].Value.ToString()) < 61)
                                current.Bad++;
                            else if (int.Parse(main.dataGridView1.Rows[i].Cells[7].Value.ToString()) >= 76 && int.Parse(main.dataGridView1.Rows[i].Cells[7].Value.ToString()) < 90)
                                current.Good++;
                            else if (int.Parse(main.dataGridView1.Rows[i].Cells[7].Value.ToString()) >= 90)
                                current.Nice++;
                        }
                        current = current.Next;
                    }
                }
                else
                {
                    if (int.Parse(main.dataGridView1.Rows[i].Cells[7].Value.ToString()) < 61)
                        list.first(1, 0, 0, main.dataGridView1.Rows[i].Cells[3].Value.ToString(), "", "");
                    else if (int.Parse(main.dataGridView1.Rows[i].Cells[7].Value.ToString()) >= 76 && int.Parse(main.dataGridView1.Rows[i].Cells[7].Value.ToString()) < 90)
                        list.first(0, 1, 0, main.dataGridView1.Rows[i].Cells[3].Value.ToString(),"", "");
                    else if (int.Parse(main.dataGridView1.Rows[i].Cells[7].Value.ToString()) >= 90)
                        list.first(0, 0, 1, main.dataGridView1.Rows[i].Cells[3].Value.ToString(), "", "");
                    else
                        list.first(0, 0, 0, main.dataGridView1.Rows[i].Cells[3].Value.ToString(), "", "");
                    nice = false;
                }
                //MessageBox.Show(dataGridView1.Rows[i].Cells[1].Value.ToString().Substring(5, 2) + " - " + month[int.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString().Substring(5, 2))-1]);
                if (nice)
                {
                    if (int.Parse(main.dataGridView1.Rows[i].Cells[7].Value.ToString()) < 61)
                        list.first(1, 0, 0, main.dataGridView1.Rows[i].Cells[3].Value.ToString(), "", "");
                    else if (int.Parse(main.dataGridView1.Rows[i].Cells[7].Value.ToString()) >= 76 && int.Parse(main.dataGridView1.Rows[i].Cells[7].Value.ToString()) < 90)
                        list.first(0, 1, 0, main.dataGridView1.Rows[i].Cells[3].Value.ToString(), "", "");
                    else if (int.Parse(main.dataGridView1.Rows[i].Cells[7].Value.ToString()) >= 90)
                        list.first(0, 0, 1, main.dataGridView1.Rows[i].Cells[3].Value.ToString(), "", "");
                    else
                        list.first(0, 0, 0, main.dataGridView1.Rows[i].Cells[3].Value.ToString(), "", "");
                }
            }
            
            int[] masX = new int[cnt];
            int[] masG = new int[cnt];
            int[] masN = new int[cnt];
            string[] masY = new string[cnt];
            current = list._head;
            for (int i = 0; i < cnt; ++i)
            {
                masX[i] = current.Bad;
                masY[i] = current.Faq;
                masG[i] = current.Good;
                masN[i] = current.Nice;
                current = current.Next;
            }
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
            chart1.DataBind();
            this.chart1.Series[1].EmptyPointStyle.IsValueShownAsLabel = false;
            for (int i = 0; i < cnt; i++)
            {
                this.chart1.Series[0].Points.AddXY(masY[i], masX[i]);
                this.chart1.Series[1].Points.AddXY(masY[i], masG[i]);
                this.chart1.Series[2].Points.AddXY(masY[i], masN[i]);

            }
                    break;
                case 2:
                    for (int j = 0; j < main.dataGridView1.ColumnCount; j++)
                        if (main.dataGridView1.Columns[j].HeaderText == "Курс")
                            main.dataGridView1.Sort(main.dataGridView1.Columns[j], ListSortDirection.Ascending);
                    for (int i = 0; i < main.dataGridView1.Rows.Count; i++)
                    {
                        bool nice = true;
                        if (list._head != null)
                        {
                            current = list._head;
                            while (current != null)
                            {
                                if (current.Kurs == main.dataGridView1.Rows[i].Cells[5].Value.ToString())
                                {
                                    nice = false;
                                    if (int.Parse(main.dataGridView1.Rows[i].Cells[7].Value.ToString()) < 61)
                                        current.Bad++;
                                    else if (int.Parse(main.dataGridView1.Rows[i].Cells[7].Value.ToString()) >= 76 && int.Parse(main.dataGridView1.Rows[i].Cells[7].Value.ToString()) < 90)
                                        current.Good++;
                                    else if (int.Parse(main.dataGridView1.Rows[i].Cells[7].Value.ToString()) >= 90)
                                        current.Nice++;
                                }
                                current = current.Next;
                            }
                        }
                        else
                        {
                            if (int.Parse(main.dataGridView1.Rows[i].Cells[7].Value.ToString()) < 61)
                                list.first(1, 0, 0, "" ,main.dataGridView1.Rows[i].Cells[5].Value.ToString(), "");
                            else if (int.Parse(main.dataGridView1.Rows[i].Cells[7].Value.ToString()) >= 76 && int.Parse(main.dataGridView1.Rows[i].Cells[7].Value.ToString()) < 90)
                                list.first(0, 1, 0, "",main.dataGridView1.Rows[i].Cells[5].Value.ToString(), "");
                            else if (int.Parse(main.dataGridView1.Rows[i].Cells[7].Value.ToString()) >= 90)
                                list.first(0, 0, 1, "", main.dataGridView1.Rows[i].Cells[5].Value.ToString(), "");
                            else
                                list.first(0, 0, 0, "", main.dataGridView1.Rows[i].Cells[5].Value.ToString(), "");
                            nice = false;
                        }
                        //MessageBox.Show(dataGridView1.Rows[i].Cells[1].Value.ToString().Substring(5, 2) + " - " + month[int.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString().Substring(5, 2))-1]);
                        if (nice)
                        {
                            if (int.Parse(main.dataGridView1.Rows[i].Cells[7].Value.ToString()) < 61)
                                list.first(1, 0, 0, "", main.dataGridView1.Rows[i].Cells[5].Value.ToString(), "");
                            else if (int.Parse(main.dataGridView1.Rows[i].Cells[7].Value.ToString()) >= 76 && int.Parse(main.dataGridView1.Rows[i].Cells[7].Value.ToString()) < 90)
                                list.first(0, 1, 0,"", main.dataGridView1.Rows[i].Cells[5].Value.ToString(), "");
                            else if (int.Parse(main.dataGridView1.Rows[i].Cells[7].Value.ToString()) >= 90)
                                list.first(0, 0, 1,"", main.dataGridView1.Rows[i].Cells[5].Value.ToString(), "");
                            else
                                list.first(0, 0, 0,"", main.dataGridView1.Rows[i].Cells[5].Value.ToString(), "");
                        }
                    }
                    int[] masAX = new int[cnt];
                    int[] masAG = new int[cnt];
                    int[] masAN = new int[cnt];
                    string[] masAY = new string[cnt];
                    current = list._head;
                    for (int i = 0; i < cnt; ++i)
                    {
                        masAX[i] = current.Bad;
                        masAY[i] = current.Kurs;
                        masAG[i] = current.Good;
                        masAN[i] = current.Nice;
                        current = current.Next;
                    }
                    chart1.Series[0].Points.Clear();
                    chart1.Series[1].Points.Clear();
                    chart1.Series[2].Points.Clear();
                    chart1.DataBind();
                    this.chart1.Series[1].EmptyPointStyle.IsValueShownAsLabel = false;
                    for (int i = 0; i < cnt; i++)
                    {
                        this.chart1.Series[0].Points.AddXY(masAY[i], masAX[i]);
                        this.chart1.Series[1].Points.AddXY(masAY[i], masAG[i]);
                        this.chart1.Series[2].Points.AddXY(masAY[i], masAN[i]);

                    }
                    break;
                case 3:
                    for (int j = 0; j < main.dataGridView1.ColumnCount; j++)
                        if (main.dataGridView1.Columns[j].HeaderText == "Год поступления")
                            main.dataGridView1.Sort(main.dataGridView1.Columns[j], ListSortDirection.Ascending);
                    for (int i = 0; i < main.dataGridView1.Rows.Count; i++)
                    {
                        bool nice = true;
                        if (list._head != null)
                        {
                            current = list._head;
                            while (current != null)
                            {
                                if (current.God == main.dataGridView1.Rows[i].Cells[8].Value.ToString())
                                {
                                    nice = false;
                                    if (int.Parse(main.dataGridView1.Rows[i].Cells[7].Value.ToString()) < 61)
                                        current.Bad++;
                                    else if (int.Parse(main.dataGridView1.Rows[i].Cells[7].Value.ToString()) >= 76 && int.Parse(main.dataGridView1.Rows[i].Cells[7].Value.ToString()) < 90)
                                        current.Good++;
                                    else if (int.Parse(main.dataGridView1.Rows[i].Cells[7].Value.ToString()) >= 90)
                                        current.Nice++;
                                }
                                current = current.Next;
                            }
                        }
                        else
                        {
                            if (int.Parse(main.dataGridView1.Rows[i].Cells[7].Value.ToString()) < 61)
                                list.first(1, 0, 0, "", "", main.dataGridView1.Rows[i].Cells[8].Value.ToString());
                            else if (int.Parse(main.dataGridView1.Rows[i].Cells[7].Value.ToString()) >= 76 && int.Parse(main.dataGridView1.Rows[i].Cells[7].Value.ToString()) < 90)
                                list.first(0, 1, 0, "", "", main.dataGridView1.Rows[i].Cells[8].Value.ToString());
                            else if (int.Parse(main.dataGridView1.Rows[i].Cells[7].Value.ToString()) >= 90)
                                list.first(0, 0, 1, "", "", main.dataGridView1.Rows[i].Cells[8].Value.ToString());
                            else
                                list.first(0, 0, 0, "", "", main.dataGridView1.Rows[i].Cells[8].Value.ToString());
                            nice = false;
                        }
                        //MessageBox.Show(dataGridView1.Rows[i].Cells[1].Value.ToString().Substring(5, 2) + " - " + month[int.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString().Substring(5, 2))-1]);
                        if (nice)
                        {
                            if (int.Parse(main.dataGridView1.Rows[i].Cells[7].Value.ToString()) < 61)
                                list.first(1, 0, 0, "", "", main.dataGridView1.Rows[i].Cells[8].Value.ToString());
                            else if (int.Parse(main.dataGridView1.Rows[i].Cells[7].Value.ToString()) >= 76 && int.Parse(main.dataGridView1.Rows[i].Cells[7].Value.ToString()) < 90)
                                list.first(0, 1, 0, "", "", main.dataGridView1.Rows[i].Cells[8].Value.ToString());
                            else if (int.Parse(main.dataGridView1.Rows[i].Cells[7].Value.ToString()) >= 90)
                                list.first(0, 0, 1, "", "", main.dataGridView1.Rows[i].Cells[8].Value.ToString());
                            else
                                list.first(0, 0, 0, "", "", main.dataGridView1.Rows[i].Cells[8].Value.ToString());
                        }
                    }
                    int summa = 0;
                  
                    int[] xmasX = new int[2];
                    string[] xmasY = { "2018", "2019" };
                    current = list._head;
                    for (int i = 0; i < cnt; i++)
                    {
                        summa += current.Bad;
                        current = current.Next;
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        if (i > 0)
                            summa += xmasX[i - 1];
                        xmasX[i] = summa / (cnt  + i);
                        if (summa % (cnt + i) > 4)
                            xmasX[i]++;
                    }
                    
                    int[] masBX = new int[cnt];
                    string[] masBY = new string[cnt];
                    current = list._head;
                    for (int i = 0; i < cnt; ++i)
                    {
                        masBX[i] = current.Bad;
                        masBY[i] = current.God;
                        current = current.Next;
                    }
                    chart1.Series[0].Points.Clear();
                    chart1.Series[1].Points.Clear();
                    chart1.Series[2].Points.Clear();
                    chart1.DataBind();
                    this.chart1.Series[1].EmptyPointStyle.IsValueShownAsLabel = false;
                    for (int i = 0; i < cnt; i++)
                    {
                        this.chart1.Series[0].Points.AddXY(masBY[i], masBX[i]);
                            

                    }
                    for(int i = 0; i<2; i++)
                    {
                        this.chart1.Series[0].Points.AddXY(xmasY[i], xmasX[i]);
                    }
                    break;
            }


        }
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }
        

       

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        



        

        private void button4_Click(object sender, EventArgs e)
        {
            bool empty = true;
            for (int i = 0; i < main.dataGridView1.Rows.Count; i++)
                for (int j = 0; j < main.dataGridView1.Columns.Count; j++)
                    if (main.dataGridView1.Rows[i].Cells[j].Value == null)
                        empty = false;
                    else if (main.dataGridView1.Rows[i].Cells[j].Value.ToString() == "")
                        empty = false;
            if (empty)
            {
                while (list._head != null)
                    list._head = list._head.Next;
                cnt = 0;
                mode = 1;
                paint();
            }
            else
            {
                MessageBox.Show("Невозможно перерисовать график при наличии пустых ячеек в таблице!", "Ошибка!",
MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool empty = true;
            for (int i = 0; i < main.dataGridView1.Rows.Count; i++)
                for (int j = 0; j < main.dataGridView1.Columns.Count; j++)
                    if (main.dataGridView1.Rows[i].Cells[j].Value == null)
                        empty = false;
                    else if (main.dataGridView1.Rows[i].Cells[j].Value.ToString() == "")
                        empty = false;
            if (empty)
            {
                while (list._head != null)
                    list._head = list._head.Next;
                cnt = 0;
                mode = 2;
                paint();
            }
            else
            {
                MessageBox.Show("Невозможно перерисовать график при наличии пустых ячеек в таблице!", "Ошибка!",
MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool empty = true;
            for (int i = 0; i < main.dataGridView1.Rows.Count; i++)
                for (int j = 0; j < main.dataGridView1.Columns.Count; j++)
                    if (main.dataGridView1.Rows[i].Cells[j].Value == null)
                        empty = false;
                    else if (main.dataGridView1.Rows[i].Cells[j].Value.ToString() == "")
                        empty = false;
            if (empty)
            {
                while (list._head != null)
                    list._head = list._head.Next;
                cnt = 0;
                mode = 3;
                paint();
            }
            else
            {
                MessageBox.Show("Невозможно перерисовать график при наличии пустых ячеек в таблице!", "Ошибка!",
MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }

}
/*public void paint()
        {
            mylist list = new mylist();
            mylistelem current = null;
            Form1 main = this.Owner as Form1;
            int L = main.dataGridView1.Rows.Count;
            string k;
            bool check = false;
            int m = 0;
            for (int j = 0; j < main.dataGridView1.Columns.Count; j++)
                if (main.dataGridView1.Columns[j].HeaderText == "Поступление на работу")
                    main.dataGridView1.Sort(main.dataGridView1.Columns[j], ListSortDirection.Ascending);

            for (int j = 0; j < main.dataGridView1.Columns.Count; j++)
                if (main.dataGridView1.Columns[j].HeaderText == "Поступление на работу")
                    for (int i = 0; i < main.dataGridView1.Rows.Count; i++)
                    {
                        k = main.dataGridView1.Rows[i].Cells[j].Value.ToString().Substring(0, 4);
                        current = list._head;
                        check = false;
                        while (current != null)
                        {
                            if (current.Year == int.Parse(k))
                            {
                                check = true;
                                current.Oklad += int.Parse(main.dataGridView1.Rows[i].Cells[j + 1].Value.ToString());
                            }
                            current = current.Next;
                        }
                        if (!check)
                        {
                            list.first(int.Parse(k), (int.Parse(main.dataGridView1.Rows[i].Cells[j + 1].Value.ToString())));
                            ++m;
                        }
                    }
            current = list._head;
            while (current.Next != null)
            {
                current.Next.Oklad += current.Oklad;
                current = current.Next;
            }

            int summa = 0;
            int medium = 0;
            int high = 0;
            int[] masX = new int[10];
            int[] masY = new int[10];
            current = list._head;
           for (int i = 0; i < m; i++)
           {
               masY[i] = current.Year;
               masX[i] = current.Oklad;
               current = current.Next;
           }
           
            current = list._head;
            for (int i = 0; i < (m - 1); i++)
            {
                summa += current.Next.Oklad - current.Oklad;
                current = current.Next;
            }
            masY[m] = 2018;
            masY[m + 1] = 2019;
            medium = summa / m;
            masX[m] = masX[m - 1] + medium;
            high = (summa + (masX[m] - masX[m - 1])) / (m + 1);
            masX[m + 1] = masX[m] + high;
            chart1.DataBind();
            this.chart1.Series[0].Points.DataBindXY(masY, masX);
            label1.Text = "Прогноз роста размера фонда на 2018 = +" + medium + "руб.";
            label2.Text = "Прогноз размера фонда на 2018 = " + masX[m] + "руб.";
            label3.Text = "Средняя з/п на 2018 = " + (masX[m] / main.dataGridView1.Rows.Count) + "руб.";
            label4.Text = "Прогноз роста размера фонда на 2019 = +" + high + "руб.";
        }*/
