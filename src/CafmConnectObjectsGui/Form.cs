using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VDI3805;
using System.Web.Script.Serialization;


namespace CafmConnectObjectsGui
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            VDI3805.VDI3805 vdi3805;
            int size = -1;
            //DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (1 == 1) // Test result.
            {
                string filename = openFileDialog1.FileName;
                filename = @"samples\PART03_Broetje_GFX.zip";
                try
                {
                    vdi3805 = new VDI3805.VDI3805(filename);

                    //var json = new JavaScriptSerializer();
                    //json.MaxJsonLength = Int32.MaxValue;
                    //json.Serialize(vdi3805.herstellerDatensatz);
                    //textBox1.Text=json.ToString();


                    //propertyGrid.SelectedObject = vdi3805.herstellerDatensatz;

                    textBox1.Text += "Datei:       " + vdi3805.manufacturerFile.Filename + Environment.NewLine;
                    textBox1.Text += "Hersteller:  " + vdi3805.manufacturerFile.ManufacturerName + Environment.NewLine;
                    textBox1.Text += "Inhalt:       " + vdi3805.manufacturerFile.ManufacturerText + Environment.NewLine;
                    textBox1.Text += "---------------------------------------" + Environment.NewLine;

                    foreach (BsNumber bsNumber in vdi3805.manufacturerFile.Products.Take(2))
                    {
                        textBox1.Text += "BsNummer:              " + bsNumber.Comment + Environment.NewLine;
                        if (bsNumber.Productmaingroup1_100 != "000")
                        {
                            textBox1.Text += "Productmaingroup1_100 : " + bsNumber.Productmaingroup1_100 + Environment.NewLine;
                            foreach (ProductMainGroup1 pmg1 in bsNumber.ProductMainGroups1) 
                            {
                                textBox1.Text += "    Index                 :" + pmg1.Index + Environment.NewLine;
                                textBox1.Text += "    ProductDesignation    :" + pmg1.ProductDesignation + Environment.NewLine;
                            }
                        }
                        if (bsNumber.Productmaingroup2_110 != "000")
                        { 
                            textBox1.Text += "Productmaingroup2_110 :" + bsNumber.Productmaingroup2_110 + Environment.NewLine;
                            foreach (ProductMainGroup2 pmg2 in bsNumber.ProductMainGroups2)
                            {
                                textBox1.Text += "    Index                 :" + pmg2.Index + Environment.NewLine;
                                textBox1.Text += "    ProductDesignation    :" + pmg2.ProductDesignation + Environment.NewLine;
                                textBox1.Text += "    Comment               :" + pmg2.Comment + Environment.NewLine;
                            }
                        }
                        if (bsNumber.Accessorymaingroup2_160 != "000")
                            textBox1.Text += "Accessorymaingroup2_160 :" + bsNumber.Accessorymaingroup2_160 + Environment.NewLine;

                        if (bsNumber.ProductvariantA_200 != "000")
                            textBox1.Text += "ProductvariantA_200 :" + bsNumber.ProductvariantA_200 + Environment.NewLine;
                        if (bsNumber.VarianttypeA1_250 != "000")
                            textBox1.Text += "VarianttypeA1_250 :" + bsNumber.VarianttypeA1_250 + Environment.NewLine;
                        if (bsNumber.VarianttypeA2_260 != "000")
                            textBox1.Text += "VarianttypeA2_260 :" + bsNumber.VarianttypeA2_260 + Environment.NewLine;

                        if (bsNumber.ProductvariantB_300 != "000")
                            textBox1.Text += "ProductvariantB_300 :" + bsNumber.ProductvariantB_300 + Environment.NewLine;
                        if (bsNumber.VarianttypeB1_350 != "000")
                            textBox1.Text += "VarianttypeB1_350 :" + bsNumber.VarianttypeB1_350 + Environment.NewLine;
                        if (bsNumber.VarianttypeB2_360 != "000")
                            textBox1.Text += "VarianttypeB2_360 :" + bsNumber.VarianttypeB2_360 + Environment.NewLine;

                        if (bsNumber.ProductvariantC_400 != "000")
                            textBox1.Text += "ProductvariantC_400 :" + bsNumber.ProductvariantC_400 + Environment.NewLine;
                        if (bsNumber.VarianttypeC1_450 != "000")
                            textBox1.Text += "VarianttypeC1_450 :" + bsNumber.VarianttypeC1_450 + Environment.NewLine;
                        if (bsNumber.VarianttypeC2_460 != "000")
                            textBox1.Text += "VarianttypeC2_460 :" + bsNumber.VarianttypeC2_460 + Environment.NewLine;


                        if (bsNumber.ProductvariantD_500 != "000")
                            textBox1.Text += "ProductvariantD_500 :" + bsNumber.ProductvariantD_500 + Environment.NewLine;
                        if (bsNumber.VarianttypeD1_550 != "000")
                            textBox1.Text += "VarianttypeD1_550 :" + bsNumber.VarianttypeD1_550 + Environment.NewLine;
                        if (bsNumber.VarianttypeD2_560 != "000")
                            textBox1.Text += "VarianttypeD2_560 :" + bsNumber.VarianttypeD2_560 + Environment.NewLine;

                        if (bsNumber.Productelementdata_700 != "000")
                        {
                            textBox1.Text += "Productelementdata_700 :" + bsNumber.Productelementdata_700 + Environment.NewLine;
                            foreach (Property prop in bsNumber.Properties)
                            {
                                textBox1.Text += "    "+ prop.FieldName+"         :" + prop.Value + Environment.NewLine;
                            }
                        }
                        if (bsNumber.Accessoryproductelement_760 != "000")
                            textBox1.Text += "Accessoryproductelement_760 :" + bsNumber.Accessoryproductelement_760 + Environment.NewLine;

                        textBox1.Text += "---------------------------------------" + Environment.NewLine;

                    }
                }
                catch (Exception ex)
                {
                }
            }           
        }
    }
}
