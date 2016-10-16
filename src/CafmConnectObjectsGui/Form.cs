using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CafmConnect.Manufacturer;
using VDI3805;
using System.Web.Script.Serialization;
using System.IO;
using CafmConnect;
using System.Diagnostics;
using System.IO.Compression;

namespace CafmConnectObjectsGui
{
    public partial class Form : System.Windows.Forms.Form
    {

        private string fileNameXml;
        private VDI3805.VDI3805 vdi3805;

        public Form()
        {
            InitializeComponent();

            linkLabel1.Text = string.Empty;
            linkLabelVDI3085.Text = string.Empty;
            labelCounter.Text = string.Empty;
            labelStopwatch.Text = string.Empty;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonVDI3805Read_Click(object sender, EventArgs e)
        {
            
            DialogResult result = openFileDialog1.ShowDialog(); 
            if (result.ToString()=="OK")
            {
                string filename = openFileDialog1.FileName;
                //filename = @"samples\PART03_Broetje_GFX.zip";
                textBox1.Text = "Please wait...";
                textBox1.Refresh();

                vdi3805 = new VDI3805.VDI3805(filename);

                string fileContentAsXml = vdi3805.ToXml();
                fileNameXml = filename.Replace("zip", "xml");
                File.WriteAllText(fileNameXml, fileContentAsXml);

                linkLabelVDI3085.Text = fileNameXml;
                linkLabelVDI3085.Links.Clear();
                linkLabelVDI3085.Links.Add(0, 1000, fileNameXml);
                linkLabelVDI3085.Enabled=true;

                textBox1.Text = fileContentAsXml;
            }           
        }

        private void buttonShowXml_Click(object sender, EventArgs e)
        {

            System.Diagnostics.Process.Start(fileNameXml);

        }

        private void buttonCafmConnectCreate_Click(object sender, EventArgs e)
        {
            linkLabel1.Text = string.Empty;
            textBox3.Text = "Please wait...";
            textBox3.Refresh();
            int maxVariants = Convert.ToInt16(textBox2.Text);

            string fileName = @"c:\tmp\MyProductFile"+maxVariants.ToString()+".ifczip";

            CafmConnect.Workspace ws = new Workspace();
            string key = ws.CreateCcFile("Author", "Organization", "System", "Authorization");

            string siteGuid = ws.AddNewSite(key,"CGN","SiteDescription","SiteStreet","50667","Cologne","DE");
            Stopwatch sw = new Stopwatch();

            sw.Start();
            
            for (int i = 1; i <= maxVariants; i++)
            {
                CafmConnect.Manufacturer.CcManufacturerProduct product = new CcManufacturerProduct("461");
                product.Description = "Aufzug "+i.ToString();
                product.Name= "Variante " + i.ToString();
                product.Attributes.Add(new CcManufacturerProductDetail("Anzahl Haltestellen", "Anzahl Haltestellen", "10"));
                product.Attributes.Add(new CcManufacturerProductDetail("Tragkraft in Personen", "Tragkraft in Personen", "5"));
                product.Attributes.Add(new CcManufacturerProductDetail("Tragkraft", "Tragkraft", (i*2).ToString()));

                if (siteGuid != null) ws.AddNewProduct(key, siteGuid, "461", product);

                labelCounter.Text = i.ToString();
                labelCounter.Refresh();

                labelStopwatch.Text = (sw.ElapsedMilliseconds / i).ToString();
                labelStopwatch.Refresh();

            }

            ws.SaveCcFileAs(key, fileName, true, true);
            sw.Stop();

            linkLabel1.Text = fileName;
            textBox3.Text = ws.GetModelOfCcFile(fileName);

        }

        private void Form_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text == string.Empty)
                textBox3.Text = "Please read first the VDI 3805 file. Thank you.";
            else
            {
                textBox3.Text = "Please wait...";
                textBox3.Refresh();
                linkLabel1.Text = string.Empty;
                int maxVariants = Convert.ToInt16(textBox2.Text);

                string folder = @"c:\tmp";
                string fileName = Path.Combine(folder,Path.GetFileNameWithoutExtension(vdi3805.Filename))+".ifczip";

                Stopwatch sw = new Stopwatch();
                sw.Start();

                CafmConnect.Workspace ws = new Workspace();
                string key = ws.CreateCcFileFromVDI3805(vdi3805);

                ws.SaveCcFileAs(key, fileName, true, true);
                sw.Stop();

                linkLabel1.Text = fileName;
                textBox3.Text = ws.GetModelOfCcFile(fileName);
            }
        }
    }
}
