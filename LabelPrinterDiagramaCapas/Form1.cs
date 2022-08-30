using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using BLL;


namespace LabelPrinterDiagramaCapas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            //variables
            string sArchivo_Carga = openFileDialog1.FileName;
            string sArchivo;
            Narchivo BLLarchivo = new Narchivo();
           
            //archivo a cargar
            openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName != null)
            {                
                textBox1.Text = sArchivo_Carga;
                sArchivo = BLLarchivo.Procesar_Archivo(sArchivo_Carga);
            }

           
        }
    }
}
