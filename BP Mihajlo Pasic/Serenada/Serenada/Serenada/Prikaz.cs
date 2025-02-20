using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Serenada
{
    public partial class Prikaz : Form
    {
        public Prikaz(DataTable dtPrikaz)
        {
            InitializeComponent();
            dataGridView1.DataSource = dtPrikaz;
        }
    }
}
