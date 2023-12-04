using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLAClusterModel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            //this.Refresh();
            //DLA cluster = new DLA(this);
            //cluster.Growth(false);
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int temp = int.Parse(textBox2.Text);
            DLA cluster = new DLA(this, temp);
            cluster.Growth(false);
            textBox1.Refresh();
        }
    }
}
