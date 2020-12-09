using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace s
{
    public partial class Form2 : Form
    {

        public Form2()
        {
            InitializeComponent();
        }
        public int[] k = new int[10];
        public string s;

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            s = text2.Text.ToString();

        }
    }
}
