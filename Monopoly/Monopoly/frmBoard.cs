using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monopoly
{
    public partial class frmBoard : Form1
    {
        public frmBoard()
        {
            InitializeComponent();
        }

        private void frmBoard_Load(object sender, EventArgs e)
        {
            listBoxTileNames.Items.Add("Serengeti");
            listBoxTileNames.Items.Add("Harrys");
            listBoxTileNames.Items.Add("Aim Steel 1");
            listBoxTileNames.Items.Add("Aim Steel 2");
            listBoxTileNames.Items.Add("Steel Centre 1");
            listBoxTileNames.Items.Add("Steel Centre 2");
            listBoxTileNames.Items.Add("Sameer Parts Ltd");
            listBoxTileNames.Items.Add("Hajees Bus");
            listBoxTileNames.Items.Add("BANK M");
            listBoxTileNames.Items.Add("Community Chest");
            listBoxTileNames.Items.Add("Chance");
            listBoxTileNames.Items.Add("Hot Plate");
            listBoxTileNames.Items.Add("Kipara Engineering");
            listBoxTileNames.Items.Add("Impala");
            listBoxTileNames.Items.Add("Leopard Tours");
            listBoxTileNames.Items.Add("NBC");
            listBoxTileNames.Items.Add("Sea House");
            listBoxTileNames.Items.Add("NHC 1");
            listBoxTileNames.Items.Add("NHC 2");
            listBoxTileNames.Items.Add("NHC 3");
            listBoxTileNames.Items.Add("NHC 4");
            listBoxTileNames.Items.Add("NHC 5");

        }

        private void listBoxTileNames_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
 
        }
    }
}
