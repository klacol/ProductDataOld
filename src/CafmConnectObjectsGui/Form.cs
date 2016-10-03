using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CafmConnectObjects;
using VDI3805;
using System.Web.Script.Serialization;
using System.IO;

namespace CafmConnectObjectsGui
{
    public partial class Form : System.Windows.Forms.Form
    {

        private string fileNameXml;

        public Form()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonVDI3805Read_Click(object sender, EventArgs e)
        {
            VDI3805.VDI3805 vdi3805;
            int size = -1;
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (1 == 1) // Test result.
            {
                string filename = openFileDialog1.FileName;
                //filename = @"samples\PART03_Broetje_GFX.zip";

                vdi3805 = new VDI3805.VDI3805(filename);

                //string fileContent = vdi3805.Print();
                string fileContentAsXml = vdi3805.ToXml();
                fileNameXml = filename.Replace("zip", "xml");
                File.WriteAllText(fileNameXml, fileContentAsXml);

                linkLabel2.Text = fileNameXml;
                linkLabel2.Links.Add(0, 1000, fileNameXml);
                linkLabel2.Enabled=true;

                textBox1.Text = fileContentAsXml;
            }           
        }

        private void buttonShowXml_Click(object sender, EventArgs e)
        {

            System.Diagnostics.Process.Start(fileNameXml);

        }

        private void buttonCafmConnectCreate_Click(object sender, EventArgs e)
        {
            CafmConnect cc = new CafmConnect();
            cc.CreateNewFile();

        }

        private void Form_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }
    }
}
