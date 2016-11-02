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
        private string workingFolder;

        public Form()
        {
            InitializeComponent();

            linkLabel1.Text = string.Empty;
            linkLabelVDI3085.Text = string.Empty;
            labelCounter.Text = string.Empty;
            labelStopwatch.Text = string.Empty;
            buttonOpenFolder.Visible = false;
            buttonCreateCcFileFromVDI.Visible = false;
            textBoxCafmConnectFile.Visible = false;

        }

        private void buttonVDI3805Read_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Path.Combine( Application.StartupPath, "Samples");
            //openFileDialog.Filter = "VDI Files (*.zip)|*.txt|All files (*.*)|*.*";


            DialogResult result = openFileDialog.ShowDialog(); 
            if (result.ToString()=="OK")
            {
                string filename = openFileDialog.FileName;
                workingFolder = Path.GetDirectoryName(filename);
                //filename = @"samples\PART03_Broetje_GFX.zip";
                textBoxVdiFile.Text = "Reading and analyzing the VDI3805 file, this can take a while, please wait...";
                textBoxVdiFile.Refresh();

                vdi3805 = new VDI3805.VDI3805(filename);
                textBoxVdiFile.Text += Environment.NewLine + "Reading of file finished...";
                textBoxVdiFile.Refresh();

                string fileContentAsXml = vdi3805.ToXml();
                fileNameXml = filename.Replace("zip", "xml");
                File.WriteAllText(fileNameXml, fileContentAsXml);

                linkLabelVDI3085.Text = fileNameXml;
                linkLabelVDI3085.Links.Clear();
                linkLabelVDI3085.Links.Add(0, 1000, fileNameXml);
                linkLabelVDI3085.Enabled=true;
                linkLabelVDI3085.Refresh();
                var lineCount = File.ReadLines(fileNameXml).Count();
                textBoxVdiFile.Text += Environment.NewLine + "Intermediate XML file saved sucessufully on disk (" + lineCount.ToString()+" Lines)";
                textBoxVdiFile.Text += Environment.NewLine + "Loading the file...";
                textBoxVdiFile.Refresh();

                textBoxVdiFile.Text = fileContentAsXml;
                buttonCreateCcFileFromVDI.Visible = true;
                textBoxCafmConnectFile.Visible = true;
            }           
        }

        private void buttonShowXml_Click(object sender, EventArgs e)
        {

            System.Diagnostics.Process.Start(fileNameXml);

        }

        private void buttonCafmConnectCreate_Click(object sender, EventArgs e)
        {
            linkLabel1.Text = string.Empty;
            textBoxCafmConnectFile.Text = "Please wait, this takes a while, sometimes several minutes.";
            textBoxCafmConnectFile.Text += Environment.NewLine + "We work hard to optimize the code, to make this happen faster...";
            textBoxCafmConnectFile.Text += Environment.NewLine + "When the file is created, please open it with the CAFM-Conenct Editor.";
            textBoxCafmConnectFile.Refresh();
            int maxVariants = Convert.ToInt16(textBox2.Text);

            string fileName = Path.GetTempPath()+maxVariants.ToString()+".ifczip";

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

                if (siteGuid != null) ws.AddNewProduct(key, siteGuid, "461", product, product.Description);

                labelCounter.Text = i.ToString();
                labelCounter.Refresh();

                labelStopwatch.Text = (sw.ElapsedMilliseconds / i).ToString();
                labelStopwatch.Refresh();

            }

            ws.SaveCcFileAs(key, fileName, true, true);
            sw.Stop();

            linkLabel1.Text = fileName;
            textBoxCafmConnectFile.Text = ws.GetModelOfCcFile(fileName);

        }

        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        private void buttonCreateCcFileFromVDI_Click(object sender, EventArgs e)
        {
            if (this.textBoxVdiFile.Text == string.Empty)
                textBoxCafmConnectFile.Text = "Please read first the VDI 3805 file. Thank you.";
            else
            {

                int numberOfProductMainGroup100 = vdi3805.LeadData_010.ProductMainGroup1_100s.Count();
                int numberOfProductMainGroup110 = 0;
                int numberOfProductElementData700 = 0;
                foreach (ProductMainGroup1_100 pmg100 in vdi3805.LeadData_010.ProductMainGroup1_100s)
                { 
                    numberOfProductMainGroup110 += pmg100.ProductMainGroup2_110s.Count();
                    foreach (ProductMainGroup2_110 pmg110 in pmg100.ProductMainGroup2_110s)
                    {
                        numberOfProductElementData700 += pmg110.ProductElementData_700s.Count();
                    }
                }
                textBoxCafmConnectFile.Text += Environment.NewLine + "----------------------------------------------------------------";
                textBoxCafmConnectFile.Text += Environment.NewLine + "ProductMainGroup (100):" + vdi3805.LeadData_010.ProductMainGroup1_100s.Count();
                textBoxCafmConnectFile.Text += Environment.NewLine + "ProductMainGroup (110):" + numberOfProductMainGroup110;
                textBoxCafmConnectFile.Text += Environment.NewLine + "ProductElementData (700):" + numberOfProductElementData700;
                textBoxCafmConnectFile.Text += Environment.NewLine + "Please wait, this takes a while, sometimes several minutes.";
                textBoxCafmConnectFile.Text += Environment.NewLine + "We work hard to optimize the code, to make this happen faster...";

                textBoxCafmConnectFile.Text += Environment.NewLine + "When the file is created, please open it with the CAFM-Connect Editor.";
                textBoxCafmConnectFile.Text += Environment.NewLine + "----------------------------------------------------------------";

                vdi3805.LeadData_010.ProductMainGroup1_100s.Count();
                textBoxCafmConnectFile.Refresh();

                textBoxCafmConnectFile.Refresh();
                linkLabel1.Text = string.Empty;
                int maxVariants = Convert.ToInt16(textBox2.Text);

                string fileName = Path.Combine(workingFolder,Path.GetFileNameWithoutExtension(vdi3805.Filename))+".ifczip";

                CafmConnect.Workspace ws = new Workspace();
                ws.eventHandler += new EventHandler(AddInfoToTextBoxCafmConnectFile);
                string key = ws.CreateCcFileFromVDI3805(vdi3805);

                ws.SaveCcFileAs(key, fileName, true, true);

                linkLabel1.Text = fileName;
                textBoxCafmConnectFile.Text += Environment.NewLine + "----------------------------------------------------------------";
                textBoxCafmConnectFile.Text += Environment.NewLine + ws.GetModelOfCcFile(fileName);
                buttonOpenFolder.Visible = true;

            }
        }

        private void buttonOpenFolder_Click(object sender, EventArgs e)
        {
            Process.Start(workingFolder);
        }


        private void AddInfoToTextBoxCafmConnectFile(object s, EventArgs e)
        {
            var ws = s as CafmConnect.Workspace;

            textBoxCafmConnectFile.AppendText(ws.LastAction+Environment.NewLine);
           
        }
    }
}
