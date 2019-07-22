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
    public partial class Form2 : Form
    {
        public Form2()
        {
            
            InitializeComponent();
            this.ActiveControl = textBox1;
        }

    private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            this.ActiveControl = textBox1;
            Form1 main = this.Owner as Form1;
            if (textBox1.Text != "")
            {
                main.find();

            }
            else
                MessageBox.Show("Введите данные для поиска", "Ошибка!",
      MessageBoxButtons.OK, MessageBoxIcon.Warning);
            
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Enter))
            {
                button1_Click(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 main = this.Owner as Form1;
            for (int i = 0; i < main.dataGridView1.Rows.Count; i++)
                main.dataGridView1.Rows[i].Visible = true;
        }
    }
}
