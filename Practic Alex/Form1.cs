using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Practic_Alex
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.F))
            {
                button4_Click(null, null);
                return true;
            }
            if (keyData == (Keys.Control | Keys.S))
            {
                button5_Click(null, null);
                return true;
            }
            if(keyData == (Keys.Control | Keys.Z))
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    dataGridView1.Rows[i].Visible = true;
                hdn = false;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        public void find()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                dataGridView1.Rows[i].Visible = true;
            bool found = false;
            dataGridView1.ClearSelection();
            for (int i = 0; i < dataGridView1.RowCount; i++)
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (f.checkBox1.Checked ?  (dataGridView1.Rows[i].Cells[j].Value.ToString().ToLower().Contains(f.textBox1.Text.ToLower())): (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(f.textBox1.Text)))
                    {
                        dataGridView1.Rows[i].Cells[j].Selected = true;
                        found = true;
                    }
            if (!found)
                MessageBox.Show("Результатов не обнаружено", "Результат",
          MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                { bool pes = false;
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        if (dataGridView1.Rows[i].Cells[j].Selected == true)
                            pes = true;
                        
                    }
                    if(!pes)
                            dataGridView1.Rows[i].Visible = false;
                }
            }
        }
        SQLiteDataReader read;
        Form2 f;
        Form3 gr;
        Form4 graphic;
        public void add()
        {
            dataGridView1.Rows.Add();
            string s = (from DataGridViewRow row in dataGridView1.Rows
                                where row.Cells[0].FormattedValue.ToString() != string.Empty
                                select Convert.ToInt32(row.Cells[0].FormattedValue)).Max().ToString();
            MessageBox.Show(s);    
        }
        bool filechoose;
        Type[] xr;
        public Type[] xri;
        public string FileName;
        string tableName;
        public string tableName2;

        private void button1_Click(object sender, EventArgs e)
        {
            filechoose = false;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Файл базы данных|*.db";
            openFileDialog1.Title = "Выберите файл базы данных";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filechoose = true;
            }
            if (filechoose)
            {
                button2.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                button8.Enabled = true;
                button9.Enabled = true;

                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                dataGridView1.Visible = true;
                SQLiteConnection conn;
                conn = new SQLiteConnection("Data Source=" + openFileDialog1.FileName + "; Version=3;");
                FileName = openFileDialog1.FileName;
                conn.Open();
                DataTable schema = conn.GetSchema("Tables", new string[] { null, null, null, "table" });
                tableName = schema.Rows[0][2].ToString();
                
                    
                
                SQLiteCommand comm = new SQLiteCommand("Select * From "+schema.Rows[0][2], conn);
                using (read = comm.ExecuteReader())
                {
                    xr = new Type[read.FieldCount];
                    for (var i = 0; i < read.FieldCount; i++)
                    {
                        dataGridView1.Columns.Add(read.GetName(i), read.GetName(i));
                            xr[i] = read.GetFieldType(i);

                    }
                    int c = 0;
                    while (read.Read())
                    {
                        dataGridView1.Rows.Add();
                        for (var i = 0; i < read.FieldCount; i++)
                        {
                            if(xr[i].ToString()!= "System.DateTime")
                            dataGridView1.Rows[c].Cells[i].Value = read.GetValue(i).ToString();
                            else
                                dataGridView1.Rows[c].Cells[i].Value = read.GetValue(i);
                        }
                        c++;
                    }
                }
                for (int j=0; j<dataGridView1.Columns.Count; j++)
                    if(xr[j].ToString() == "System.DateTime")
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DateTime rx = (DateTime)dataGridView1.Rows[i].Cells[j].Value;
                    dataGridView1.Rows[i].Cells[j].Value = rx.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                }

                

                    graphic = new Form4();
                    graphic.Owner = this;

                graphic.paint();
                dataGridView1.Columns[dataGridView1.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            f = new Form2();
            f.Owner = this;
            f.Show();
        }
        bool selch = false;
        int crow = 0;
        int ccell = 0;
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Saved = false;
            if (dataGridView1.CurrentCell.Value != null)
            {
                try
                {
                    Convert.ChangeType(dataGridView1.CurrentCell.Value.ToString(), xr[dataGridView1.CurrentCellAddress.X]);
                    if (xr[dataGridView1.CurrentCellAddress.X].ToString() == "System.DateTime")
                    {
                        Regex rgx = new Regex(@"^[0-9]{4}(-)[0-9]{2}(-)[0-9]{2}$");
                        if (!rgx.IsMatch(dataGridView1.CurrentCell.Value.ToString()))
                        {
                            dataGridView1.CurrentCell.Value = "";
                            MessageBox.Show("Некорректный ввод!" + "\n" +
                                "Введите данные в формате yyyy-mm-dd", "Ошибка!",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        if (DateTime.Compare(DateTime.Parse(dataGridView1.CurrentCell.Value.ToString()), DateTime.Now) > 0)
                        {
                            dataGridView1.CurrentCell.Value = "";
                            MessageBox.Show("Некорректный ввод!" + "\n" +
                                "Введенная вами дата должна предшествовать текущей!", "Ошибка!",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    if (dataGridView1.Columns[dataGridView1.CurrentCellAddress.X].HeaderText == "Факультет")
                    {
                        if (dataGridView1.CurrentCell.Value.ToString().ToLower() == "фэи")
                            dataGridView1.CurrentCell.Value = "ФЭИ";
                        else if (dataGridView1.CurrentCell.Value.ToString().ToLower() == "фам")
                            dataGridView1.CurrentCell.Value = "ФАМ";
                        else if (dataGridView1.CurrentCell.Value.ToString().ToLower() == "вф")
                            dataGridView1.CurrentCell.Value = "ВФ";
                        else
                        {
                            dataGridView1.CurrentCell.Value = "";
                            MessageBox.Show("Некорректный ввод!" + "\n" +
                                "Необходимо ввести один из трех факультетов:" + "\n" + "ФЭИ" + "\n" + "ФАМ" + "\n" + "ВФ", "Ошибка!",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    if (dataGridView1.Columns[dataGridView1.CurrentCellAddress.X].HeaderText == "Курс")
                    {
                        if(int.Parse(dataGridView1.CurrentCell.Value.ToString()) <1 || int.Parse(dataGridView1.CurrentCell.Value.ToString()) > 5)                        
                            dataGridView1.CurrentCell.Value = "";
                            MessageBox.Show("Некорректный ввод!" + "\n" +
                                "Курс должен быть от 1 до 5!", "Ошибка!",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
                    }
                    if (dataGridView1.Columns[dataGridView1.CurrentCellAddress.X].HeaderText == "Сред. балл")
                    {
                      if (int.Parse(dataGridView1.CurrentCell.Value.ToString()) > 100 || int.Parse(dataGridView1.CurrentCell.Value.ToString()) < 0)
                        {
                            dataGridView1.CurrentCell.Value = "";
                            MessageBox.Show("Некорректный ввод!" + "\n" +
                                "Средний балл должен быть от 0 до 100!", "Ошибка!",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    if (dataGridView1.Columns[dataGridView1.CurrentCellAddress.X].HeaderText == "Год поступления")
                    {
                        if (int.Parse(dataGridView1.CurrentCell.Value.ToString()) > 2017)
                        {
                            dataGridView1.CurrentCell.Value = "";
                            MessageBox.Show("Некорректный ввод!" + "\n" +
                                "Нельзя ввести еще не наступивший год!", "Ошибка!",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            if (int.Parse(dataGridView1.CurrentCell.Value.ToString()) <= 0)
                            {
                                dataGridView1.CurrentCell.Value = "";
                                MessageBox.Show("Некорректный ввод!" + "\n" +
                                    "Год поступления должен быть положительным числом!", "Ошибка!",
              MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            
                        }
                    }
                    if (dataGridView1.Columns[dataGridView1.CurrentCellAddress.X].HeaderText == "ФИО")
                    {
                        bool check = true;
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            if (dataGridView1.Rows[i].Cells[dataGridView1.CurrentCellAddress.X].Value != null)
                                if (Regex.Replace(dataGridView1.CurrentCell.Value.ToString().ToLower().Trim(), "[ ]+", " ") == Regex.Replace(dataGridView1.Rows[i].Cells[dataGridView1.CurrentCellAddress.X].Value.ToString().ToLower().Trim(), "[ ]+", " ") && i != dataGridView1.CurrentCellAddress.Y)
                                    check = false;
                        if (!check)
                        {
                            dataGridView1.CurrentCell.Value = "";
                            MessageBox.Show("Некорректный ввод!" + "\n" +
                            "Наименование продукта должно быть уникальным!", "Ошибка!",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    if (dataGridView1.Columns[dataGridView1.CurrentCellAddress.X].HeaderText == "Цена")
                    {
                        if (int.Parse(dataGridView1.CurrentCell.Value.ToString()) <= 0)
                        {
                            dataGridView1.CurrentCell.Value = "";
                            MessageBox.Show("Некорректный ввод!" + "\n" +
                                "Цена должна быть больше нуля!", "Ошибка!",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    if (dataGridView1.Columns[dataGridView1.CurrentCellAddress.X].HeaderText == "Поступило(ед.)")
                    {
                        if (int.Parse(dataGridView1.CurrentCell.Value.ToString()) <= 0)
                        {
                            dataGridView1.CurrentCell.Value = "";
                            MessageBox.Show("Некорректный ввод!" + "\n" +
                                "Количество поступившего товара должно быть больше нуля!", "Ошибка!",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            dataGridView1.Rows[dataGridView1.CurrentCellAddress.Y].Cells[dataGridView1.CurrentCellAddress.X + 1].Value = "";
                            MessageBox.Show("Необходимо заново ввести количество проданного товара!", "Внимание!",
          MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
                catch
                {
                    dataGridView1.CurrentCell.Value = "";
                    switch (xr[dataGridView1.CurrentCellAddress.X].ToString())
                    {
                        case "System.DateTime":
                            MessageBox.Show("Некорректный ввод!" + "\n" +
                                "Введите данные в формате yyyy-mm-dd", "Ошибка!",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case "System.Int64":
                            MessageBox.Show("Некорректный ввод!" + "\n" +
                                "Введите данные в целочисленном формате!", "Ошибка!",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }

                }
            }
            selch = true;
            crow = e.RowIndex;
            ccell = e.ColumnIndex;
        }
        bool close = false;
        bool error = false;
        bool Saved = true;
        private void button5_Click(object sender, EventArgs e)
        {
            bool save = true;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value == null)
                        save = false;
                    else if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "")
                        save = false;
            DialogResult b = DialogResult.Yes;
            if (!close)
                b = MessageBox.Show("Сохранить все изменения?", "Подтверждение сохранения", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (b == DialogResult.Yes)
                if (save)
                {
                    try
                    {
                        SQLiteConnection conn = new SQLiteConnection("Data Source=" + FileName + "; Version=3;");
                        conn.Open();
                        SQLiteCommand cmd = conn.CreateCommand();
                        cmd.CommandText = string.Format("DELETE FROM " + tableName, conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        conn = new SQLiteConnection("Data Source=" + FileName + "; Version=3;");
                        conn.Open();
                        cmd = conn.CreateCommand();
                        cmd.CommandText = string.Format("SELECT * FROM " + tableName);
                        DataTable dt = new DataTable();
                        foreach (DataGridViewColumn col in dataGridView1.Columns)
                        {
                            dt.Columns.Add(col.HeaderText);
                        }

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            DataRow dRow = dt.NewRow();
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                dRow[cell.ColumnIndex] = cell.Value;
                            }
                            dt.Rows.Add(dRow);
                        }
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                        SQLiteCommandBuilder builder = new SQLiteCommandBuilder(adapter);
                        adapter.Update(dt);
                        conn.Close();
                        MessageBox.Show("Все данные были сохранены в " + FileName, "Сохранение завершено!");
                        Saved = true;
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }
                    if (close)
                        error = false;
                }

                else
                {
                    MessageBox.Show("Невозможно произвести сохранение при наличии пустых ячеек!", "Ошибка!",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (close)
                        error = true;
                }
        }
        bool hdn = false;
        private void button6_Click(object sender, EventArgs e)
        {
            bool chh = true;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value == null)
                        chh = false;
                    else if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "")
                        chh = false;
            if (chh)
            {
                if (!hdn)
                {
                    DateTime now = DateTime.Now;
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (((now - DateTime.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString())).TotalDays < int.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString())) || (int.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString()) == int.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString())))
                            dataGridView1.Rows[i].Visible = false;
                    }
                    hdn = true;
                }
                else
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        dataGridView1.Rows[i].Visible = true;
                    hdn = false;
                }
            }
            else
                MessageBox.Show("Невозможно произвести данную операцию при наличии пустых ячеек!", "Ошибка!",
           MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        

        private void dataGridView1_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.CellValue1.ToString() != "" && e.CellValue2.ToString() != "")
                if (int.TryParse(e.CellValue1.ToString(), out int res) && int.TryParse(e.CellValue2.ToString(), out int res2))
                {
                    int a, b;
                        a = int.Parse(e.CellValue1.ToString());
                        b = int.Parse(e.CellValue2.ToString());
                    e.SortResult = a.CompareTo(b);
                    e.Handled = true;
                }

            

        }
        
        private void button8_Click(object sender, EventArgs e)
        {
            bool chh = true;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value == null)
                        chh = false;
                    else if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "")
                        chh = false;
            if (chh)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1.Columns[j].HeaderText == "Сред. балл")
                        dataGridView1.Sort(dataGridView1.Columns[j], ListSortDirection.Descending);
                gr = new Form3();
                gr.Owner = this;
                gr.Show();
                gr.diagram();
            }
            else
                MessageBox.Show("Невозможно произвести данную операцию при наличии пустых ячеек!", "Ошибка!",
           MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Saved == false)
            {
                close = true;
                var b = MessageBox.Show("Сохранить все изменения?", "Подтверждение сохранения", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (b == DialogResult.Yes)
                {
                    button5_Click(null, null);
                    e.Cancel = error;
                }
                if (b == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    close = false;
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            bool chh = true;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value == null)
                        chh = false;
                    else if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "")
                        chh = false;
            if (chh)

                graphic.Show();
            else
                MessageBox.Show("Невозможно произвести данную операцию при наличии пустых ячеек!", "Ошибка!",
           MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (selch)
            {
                dataGridView1.CurrentCell = dataGridView1.Rows[crow].Cells[ccell];
                selch = false;
            }
        }
    }
}
