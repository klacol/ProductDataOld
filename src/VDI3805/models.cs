using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDI3805
{
    public class DataSet
    {
        public string SetType { get; set; }
        public List<string> Fields { get; set; }
    }

    public class EnumValue
    {
        public string Code { get; set; }
        public string ValueDe { get; set; }
        public string ValueEn { get; set; }

        public EnumValue(string code, string valueDe, string valueEn)
        {
            Code = code;
            ValueDe = valueDe;
            ValueEn = valueEn;
        }
    }

    public class Property
    {
        public int ItemNumber { get; set; }
        public TextLabel FieldName { get; set; }
        public string Value { get; set; }
        public string Unit { get; set; }
        public string Format { get; set; }
        public string Remarks { get; set; }
        public List<string> EnumValues { get; set; }

        public Property(int itemNumber,string fieldName,string value,string unit,string format,string remarks, List<string> allowedValues)
        {
            ItemNumber = itemNumber;
            FieldName = new TextLabel(fieldName, fieldName);
            Value = value;
            Unit = unit;
            Format = format;
            Remarks = remarks;

            EnumValues = allowedValues;
            if (allowedValues!=null)
            if (!allowedValues.Any(x => x.ToUpper().Contains(value.ToUpper())))
            {
                    //Attention: The file contains an enum value, that is not allowed in the VDI 3805!!

                    Exception ex = new Exception("Attention: The file contains an enum value, that is not allowed in the VDI 3805!!");
                    //throw ex;
            }
        }

}

    public class TextLabel
    {
        public string Code;
        public string TextDe;
        public string TextEn;
        public TextLabel(string code,string textDe)
        {
            Code = code;
            TextDe = textDe;
        }
    }

    public class PropertySet
    {
        public string VDI3805PartNumber { get; set; }

        public string VDI2552Code { get; set; }

        public TextLabel PropertySetName { get; set; }

        public List<Property> Properties { get; set; }

        public PropertySet(string vDI3805PartNumber,string vDI2552Code, string propertySetName)
            {
            VDI2552Code = VDI2552Code;
            VDI3805PartNumber = vDI3805PartNumber;
            PropertySetName = new TextLabel(propertySetName, propertySetName);
            }


    }

    public class ManufacturerFile
    {
        public string Filename;
        public string VDI3805PartNumber;
        public string ManufacturerName;
        public string CountryCode;
        public string IssueDateVDI3805;
        public string IssueDateFile;
        public string Content;
        public string ManufacturerText;
        public bool FileIsValid;
        public string SetType;
        public string NumberGuidelinePart;
        public string IssueMonth;
        public string ManufacturerName2;
        public string RevisionDateFile;
        public string ManufacturerUrl;
        public string Comment;
        public string GlobalLocationNumber;
        public string DataRecordValidation;
        public string NameTestAuthority;
        public string MediaDetails;

        public List<BsNumber> Products;
        public List<ProductMainGroup1> ProductMainGroups1;
        public List<ProductMainGroup2> ProductMainGroups2;
        public List<Variant> Variants;
        public List<Declaration> Declarations;
        public List<HeatGenerator> HeatGenerators;

        public ManufacturerFile(string dataFileName)
        {
            RegisterFilename(dataFileName);
            Products = new List<BsNumber>();
            
            ProductMainGroups1 = new List<ProductMainGroup1>();
            ProductMainGroups2 = new List<ProductMainGroup2>();
            Variants = new List<Variant>();
            Declarations = new List<Declaration>();
            HeatGenerators = new List<HeatGenerator>();
            
        }

        private void RegisterFilename(string dataFileName)
            {
            string[] fileNameParts = dataFileName.Substring(0, dataFileName.Length - 4).Split('_');

            Filename = dataFileName;
            VDI3805PartNumber = fileNameParts[0];
            ManufacturerName = fileNameParts[1];
            CountryCode = fileNameParts[2];
            
            //Dirty workaround for file with missing country code => fallback to DE

            if ((CountryCode == "DE") || (CountryCode =="EN"))
            {
                IssueDateVDI3805 = fileNameParts[3];
                IssueDateFile = fileNameParts[4];
                Content = fileNameParts[5].ToUpper();
                if (fileNameParts.Length == 7) ManufacturerText = fileNameParts[6];
            }
            else
            {
                CountryCode = "DE";
                IssueDateVDI3805 = fileNameParts[2];
                IssueDateFile = fileNameParts[3];
                Content = fileNameParts[4].ToUpper();
                if (fileNameParts.Length==6) ManufacturerText = fileNameParts[5];
            }
         }

        public void RegisterLeadData(DataSet leadDataSet)
        {
            while (leadDataSet.Fields.Count <= 12) leadDataSet.Fields.Add(string.Empty);
            SetType = leadDataSet.Fields[1];
            NumberGuidelinePart = leadDataSet.Fields[2];
            IssueMonth = leadDataSet.Fields[3];
            ManufacturerName = leadDataSet.Fields[4];
            RevisionDateFile = leadDataSet.Fields[5];
            ManufacturerUrl = leadDataSet.Fields[6];
            Comment = leadDataSet.Fields[7];
            GlobalLocationNumber = leadDataSet.Fields[8];
            DataRecordValidation = leadDataSet.Fields[9];
            NameTestAuthority = leadDataSet.Fields[10];
            MediaDetails = leadDataSet.Fields[11];
        }

        public void RegisterProductData(List<DataSet> productDataSets)
        {
            foreach (DataSet productDataSet in productDataSets)
            {
                BsNumber product = new BsNumber();
                while (productDataSet.Fields.Count <= 4) productDataSet.Fields.Add(string.Empty);
                product.SetType = productDataSet.Fields[1].Substring(0, 3);
                product.Index = Convert.ToInt32(productDataSet.Fields[2]);
                product.Number = productDataSet.Fields[3].Substring(0, 53);
                product.Comment = productDataSet.Fields[4];

                //The BS number consists of numerals and question marks only and is constructed according to the following pattern
                // AAABBBCCCDDDEEEFFFGGGHHHIIIJJJKKKLLLMMMNNNOOOPPPPPQQQ""
                product.Productmaingroup1_100 = product.Number.Substring(0, 3);
                product.Productmaingroup2_110 = product.Number.Substring(3, 3);
                product.Accessorymaingroup2_160 = product.Number.Substring(6, 3);
                product.ProductvariantA_200 = product.Number.Substring(9, 3);
                product.VarianttypeA1_250 = product.Number.Substring(12, 3);
                product.VarianttypeA2_260 = product.Number.Substring(15, 3);
                product.ProductvariantB_300 = product.Number.Substring(18, 3);
                product.VarianttypeB1_350 = product.Number.Substring(21, 3);
                product.VarianttypeB2_360 = product.Number.Substring(24, 3);
                product.ProductvariantC_400 = product.Number.Substring(27, 3);
                product.VarianttypeC1_450 = product.Number.Substring(30, 3);
                product.VarianttypeC2_460 = product.Number.Substring(33, 3);
                product.ProductvariantD_500 = product.Number.Substring(36, 3);
                product.VarianttypeD1_550 = product.Number.Substring(39, 3);
                product.VarianttypeD2_560 = product.Number.Substring(42, 3);
                product.Productelementdata_700 = product.Number.Substring(45, 5);
                product.Accessoryproductelement_760 = product.Number.Substring(50, 3);

                Products.Add(product);
            }
        }

        public void RegisterProperties(string VdiPartNumber,List<DataSet> productDataSets)
        {
            //CAST PRODUCT TYPE
            //
            List<HeatGenerator> HeatGenerators = new List<HeatGenerator>();
            foreach (DataSet productDataSet in productDataSets) HeatGenerators.Add(new HeatGenerator(productDataSet, CountryCode, "421.10"));
            //foreach (BsNumber product in Products)
            //{
            //    product.Productelementdata_700
            //    foreach (DataSet productDataSet in productDataSets.Where(x=>x)

            //    while (productDataSet.Fields.Count <= 4) productDataSet.Fields.Add(string.Empty);
            //    product.SetType = productDataSet.Fields[1].Substring(0, 3);
            //    product.Index = Convert.ToInt32(productDataSet.Fields[2]);
            //    product.Number = productDataSet.Fields[3].Substring(0, 53);
            //    product.Comment = productDataSet.Fields[4];


            //    Products.Add(product);
            //}
        }

        public void RegisterProductMainGroup1(List<DataSet> DataSets)
        {
            foreach (DataSet dataSet in DataSets)
            {
                ProductMainGroup1 productMainGroup1 = new ProductMainGroup1();
                while (dataSet.Fields.Count <= 9) dataSet.Fields.Add(string.Empty);
                productMainGroup1.SetType = dataSet.Fields[1];
                productMainGroup1.Index = dataSet.Fields[2];
                productMainGroup1.SortNumberDisplaySequence = dataSet.Fields[3];
                productMainGroup1.ProductDesignation = dataSet.Fields[4];
                productMainGroup1.ProductMainGroup = dataSet.Fields[5];
                productMainGroup1.ProductClassification = dataSet.Fields[6];
                productMainGroup1.CreationDate = dataSet.Fields[7];
                productMainGroup1.Comment = dataSet.Fields[8];
                productMainGroup1.MediaDetails = dataSet.Fields[9];

                ProductMainGroups1.Add(productMainGroup1);
            }
        }

        public void RegisterProductMainGroup2(List<DataSet> DataSets)
        {
            foreach (DataSet dataSet in DataSets)
            {
                ProductMainGroup2 productMainGroup2 = new ProductMainGroup2();
                while (dataSet.Fields.Count <= 9) dataSet.Fields.Add(string.Empty);
                productMainGroup2.SetType = dataSet.Fields[1];
                productMainGroup2.Index = dataSet.Fields[2];
                productMainGroup2.SortNumberDisplaySequence = dataSet.Fields[3];
                productMainGroup2.ProductDesignation = dataSet.Fields[4];
                productMainGroup2.ProductMainGroup = dataSet.Fields[5];
                productMainGroup2.ProductClassification = dataSet.Fields[6];
                productMainGroup2.CreationDate = dataSet.Fields[7];
                productMainGroup2.Comment = dataSet.Fields[8];
                productMainGroup2.MediaDetails = dataSet.Fields[9];
            }
        }

        public void MapBsNumbers()
        {
            foreach (BsNumber bsNumber in Products)
            { 
                foreach (ProductMainGroup1 pmg1 in ProductMainGroups1
                                               .Where(x => ("000"+x.Index).Right(3) == bsNumber.Productmaingroup1_100)
                                               .OrderBy(x => x.Index))
                    bsNumber.ProductMainGroups1.Add(pmg1);

                foreach (ProductMainGroup2 pmg2 in ProductMainGroups2
                                               .Where(x => ("000" + x.Index).Right(3) == bsNumber.Productmaingroup2_110)
                                               .OrderBy(x => x.Index))
                    bsNumber.ProductMainGroups2.Add(pmg2);

                foreach (HeatGenerator hg in HeatGenerators)
                       foreach (Property prop in hg.Properties
                                           .Where(x=>x.FieldName.TextDe=="Index")
                                           .Where(y=>("00000" + y.Value).Right(5) == bsNumber.Productelementdata_700)
                                           .OrderBy(z => z.ItemNumber))
                                                bsNumber.Properties.Add(prop);

            }
        }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class LeadData
    {
        public string SetType;
        public string NumberGuidelinePart;
        public string IssueMonth;
        public string ManufacturerName;
        public string RevisionDateFile;
        public string ManufacturerUrl;
        public string Comment;
        public string GlobalLocationNumber;
        public string DataRecordValidation;
        public string NameTestAuthority;
        public string MediaDetails;
    
        public LeadData(DataSet record)
        {
            while (record.Fields.Count <= 12) record.Fields.Add(string.Empty);
            SetType = record.Fields[1];
            NumberGuidelinePart = record.Fields[2];
            IssueMonth = record.Fields[3];
            ManufacturerName = record.Fields[4];
            RevisionDateFile = record.Fields[5];
            ManufacturerUrl = record.Fields[6];
            Comment = record.Fields[7];
            GlobalLocationNumber = record.Fields[8];
            DataRecordValidation = record.Fields[9];
            NameTestAuthority = record.Fields[10];
            MediaDetails = record.Fields[11];
        }
    }

    public class ProductMainGroup1
    {
        public string SetType;
        public string Index;
        public string SortNumberDisplaySequence;
        public string ProductDesignation;
        public string ProductMainGroup;
        public string ProductClassification;
        public string CreationDate;
        public string Comment;
        public string MediaDetails;
    }

    public class ProductMainGroup2
    {
        public string SetType;
        public string Index;
        public string SortNumberDisplaySequence;
        public string ProductDesignation;
        public string ProductMainGroup;
        public string ProductClassification;
        public string CreationDate;
        public string Comment;
        public string MediaDetails;
    }

    public class Variant
    {
        public string SetType;
        public string Index;
        public string VariantDesignation;
        public string MediaDetails;
        public string AccessorySelection1;

        public Variant(DataSet record)
        {
            while (record.Fields.Count <= 5) record.Fields.Add(string.Empty);
            SetType = record.Fields[1];
            Index = record.Fields[2];
            VariantDesignation = record.Fields[3];
            MediaDetails = record.Fields[4];
            AccessorySelection1 = record.Fields[5];
        }
    }

    public class Declaration
    {
        public string SetType;
        public string Index;
        public string DeclarationsFunctionsParameters;
        public string Comment;

        public Declaration(DataSet record)
        {
            while (record.Fields.Count <= 4) record.Fields.Add(string.Empty);
            SetType = record.Fields[1];
            Index = record.Fields[2];
            DeclarationsFunctionsParameters = record.Fields[3];
            Comment = record.Fields[4];
        }
    }

    public class BsNumber
    {
        public string SetType;
        public int Index;
        public string Number;
        public string Comment;
        public string Productmaingroup1_100;
        public string Productmaingroup2_110;
        public string Accessorymaingroup2_160;
        public string ProductvariantA_200;
        public string VarianttypeA1_250;
        public string VarianttypeA2_260;
        public string ProductvariantB_300;
        public string VarianttypeB1_350;
        public string VarianttypeB2_360;
        public string ProductvariantC_400;
        public string VarianttypeC1_450;
        public string VarianttypeC2_460;
        public string ProductvariantD_500;
        public string VarianttypeD1_550;
        public string VarianttypeD2_560;
        public string Productelementdata_700;
        public string Accessoryproductelement_760;

        public List<ProductMainGroup1> ProductMainGroups1;
        public List<ProductMainGroup2> ProductMainGroups2;
        public List<Property> Properties;

        //public BsNumber(DataSet record)
        //{
        //    while (record.Fields.Count <= 4) record.Fields.Add(string.Empty);
        //    SetType = record.Fields[1].Substring(0,3);
        //    Index = Convert.ToInt32(record.Fields[2]);
        //    Number = record.Fields[3].Substring(0,53);
        //    Comment = record.Fields[4];

        //    //The BS number consists of numerals and question marks only and is constructed according to the following pattern
        //    // AAABBBCCCDDDEEEFFFGGGHHHIIIJJJKKKLLLMMMNNNOOOPPPPPQQQ""
        //    Productmaingroup1_100 = Number.Substring(0, 3);
        //    Productmaingroup2_110 = Number.Substring(3, 3);
        //    Accessorymaingroup2_160 = Number.Substring(6, 3);
        //    ProductvariantA_200 = Number.Substring(9, 3);
        //    VarianttypeA1_250 = Number.Substring(12, 3);
        //    VarianttypeA2_260 = Number.Substring(15, 3);
        //    ProductvariantB_300 = Number.Substring(18, 3);
        //    VarianttypeB1_350 = Number.Substring(21, 3);
        //    VarianttypeB2_360 = Number.Substring(24, 3);
        //    ProductvariantC_400 = Number.Substring(27, 3);
        //    VarianttypeC1_450 = Number.Substring(30, 3);
        //    VarianttypeC2_460 = Number.Substring(33, 3);
        //    ProductvariantD_500 = Number.Substring(36, 3);
        //    VarianttypeD1_550 = Number.Substring(39, 3);
        //    VarianttypeD2_560 = Number.Substring(42, 3);
        //    Productelementdata_700 = Number.Substring(45, 5);
        //    Accessoryproductelement_760 = Number.Substring(50, 3);

        //    ProductMainGroups1 = new List<ProductMainGroup1>();
        //    ProductMainGroups2 = new List<ProductMainGroup2>();
        //    Properties = new List<Property>();
        //}
    }


    public class HeatGenerator  //VDI 3805 Part 3
    {
        public bool IsValid = true;

        public List<Property> Properties;

        public HeatGenerator(DataSet record, string countryCode, string Vdi2552Code)
        {

            // The following list is copied from VDI 3805 , Part 3, Heat Generators
            // Do not add new attributes to the list, please follow the standard
            // The Copyright is at VDI Verein Deutscher Ingeniere
            // Please purchase a copy of the VDI 3805 before using this code in your application

            Properties = new List<Property>();

            foreach (string prop in record.Fields)
            {
                Properties.Add(new Property(1, "SetType", record.Fields[1], "", "A3", "700", null));
                Properties.Add(new Property(2, "Index", record.Fields[2], "", "N", "1 to 99999", null));
                Properties.Add(new Property(3, "SortNumberDisplaySequence", record.Fields[3], "", "N", "", null));
                Properties.Add(new Property(4, "ProductRange", record.Fields[4], "", "A", "", null));
                Properties.Add(new Property(5, "ProductName", record.Fields[5], "", "A", "", null));
                Properties.Add(new Property(6, "NominalPower", record.Fields[6], "kW", "N", "", null));
                Properties.Add(new Property(7, "TotalNetMass", record.Fields[7], "kg", "N", "Of heat generator", null));
                Properties.Add(new Property(8, "WaterContent", record.Fields[8], "liter", "N", "Of heat generator", null));
                Properties.Add(new Property(9, "GasContent", record.Fields[9], "liter", "N", "", null));
                Properties.Add(new Property(10, "ThermalLoadLevel3", record.Fields[10], "kW", "N", "", null));
                Properties.Add(new Property(11, "ThermalLoadLevel2", record.Fields[11], "kW", "N", "", null));
                Properties.Add(new Property(12, "ThermalLoadLevel1", record.Fields[12], "kW", "N", "Min. load", null));
                Properties.Add(new Property(13, "LowerModulationLimit", record.Fields[13], "kW", "N", "", null));

                List<EnumValue> enumDesignationForModelOfHeatExchanger = new List<EnumValue>();
                enumDesignationForModelOfHeatExchanger.Add(new EnumValue("1", "Standard-Kessel",""));
                enumDesignationForModelOfHeatExchanger.Add(new EnumValue("2", "Niedertemperatur-Kessel", ""));
                enumDesignationForModelOfHeatExchanger.Add(new EnumValue("3", "Brennwert-Kessel", ""));
                enumDesignationForModelOfHeatExchanger.Add(new EnumValue("4", "Standard-Kombi-Kessel", ""));
                enumDesignationForModelOfHeatExchanger.Add(new EnumValue("5", "Niedertemperatur-Kombi-Kessel", ""));
                enumDesignationForModelOfHeatExchanger.Add(new EnumValue("6", "Brennwert-Kombi-Kessel", ""));
                enumDesignationForModelOfHeatExchanger.Add(new EnumValue("7", "Durchlauferhitzer für TW-Bereitung", ""));
                enumDesignationForModelOfHeatExchanger.Add(new EnumValue("8", "Trinkwasser-Speicher", ""));
                enumDesignationForModelOfHeatExchanger.Add(new EnumValue("9", "Niederdruck-Dampferzeuger(NDD)", ""));
                enumDesignationForModelOfHeatExchanger.Add(new EnumValue("10", "Hochdruck-Dampferzeuger(HDD)", ""));
                enumDesignationForModelOfHeatExchanger.Add(new EnumValue("11", "Hochdruck-Heißwasser - Kessel", ""));
                enumDesignationForModelOfHeatExchanger.Add(new EnumValue("999","Sonstige", ""));

                Properties.Add(new Property(14, "CodeNumberDesignationForModelOfHeatExchanger", record.Fields[14], "", "N", "Code number", enumDesignationForModelOfHeatExchanger.Select(x=>x.Code).ToList()));
                switch (countryCode)
                {
                    case "DE":
                        Properties.Add(new Property(15, "DesignationForModelOfHeatExchanger", record.Fields[15], "", "A", "", enumDesignationForModelOfHeatExchanger.Select(x => x.ValueDe).ToList()));
                        break;
                    case "EN":
                        Properties.Add(new Property(15, "DesignationForModelOfHeatExchanger", record.Fields[15], "", "A", "", enumDesignationForModelOfHeatExchanger.Select(x => x.ValueEn).ToList()));
                        break;
                }

                List<EnumValue> enumDesignationForDesignOfHeatExchanger = new List<EnumValue>();
                enumDesignationForDesignOfHeatExchanger.Add(new EnumValue("1", "Zug-Kessel", ""));
                enumDesignationForDesignOfHeatExchanger.Add(new EnumValue("2", "Zug-Kessel Flammumkehr", ""));
                enumDesignationForDesignOfHeatExchanger.Add(new EnumValue("3", "Zug-Kessel", ""));
                enumDesignationForDesignOfHeatExchanger.Add(new EnumValue("999", "Sonstige", ""));

                Properties.Add(new Property(16, "CodeNumberForDesignOfHeatExchanger", record.Fields[16], "", "N", "", enumDesignationForModelOfHeatExchanger.Select(x => x.Code).ToList()));
                switch (countryCode)
                {
                    case "DE":
                        Properties.Add(new Property(17, "DesignationForDesignOfHeatExchanger", record.Fields[17], "", "A", "", enumDesignationForDesignOfHeatExchanger.Select(x => x.ValueDe).ToList()));
                        break;
                    case "EN":
                        Properties.Add(new Property(17, "DesignationForDesignOfHeatExchanger", record.Fields[17], "", "A", "", enumDesignationForDesignOfHeatExchanger.Select(x => x.ValueEn).ToList()));
                        break;
                }

                Properties.Add(new Property(18, "StandbyLossBoilerAtAverageWatertemperature70Degree", record.Fields[18], "%", "N", "DIN V 4701-10, otherwise function in accordance EN 304, EN 297, EN 656", null));

                List<EnumValue> enumDesignationForAirInlet = new List<EnumValue>();
                enumDesignationForAirInlet.Add(new EnumValue("1", "raumluftunabhängig", ""));
                enumDesignationForAirInlet.Add(new EnumValue("2", "raumluftabhängig", ""));
                enumDesignationForAirInlet.Add(new EnumValue("3", "raumluftabhängig oder -unabhängig", ""));
                enumDesignationForAirInlet.Add(new EnumValue("999", "Sonstige", ""));

                Properties.Add(new Property(19, "CodeNumberForAirInlet", record.Fields[19], "", "N", "", null));

                switch (countryCode)
                {
                    case "DE":
                        Properties.Add(new Property(20, "DesignationForAirInlet", record.Fields[20], "", "A", "", enumDesignationForAirInlet.Select(x => x.ValueDe).ToList()));
                        break;
                    case "EN":
                        Properties.Add(new Property(20, "DesignationForAirInlet", record.Fields[20], "", "A", "", enumDesignationForAirInlet.Select(x => x.ValueEn).ToList()));
                        break;
                }

                Properties.Add(new Property(21, "HeatingCirculatingPumpIntegratedInDevice", record.Fields[21], "", "N", "0 = no, 1 = yes", null));
                Properties.Add(new Property(22, "HeatingCirculatingPumpRegulated", record.Fields[22], "", "N", "0 = no, 1 = yes", null));
                Properties.Add(new Property(23, "AirConnection", record.Fields[23], "mm", "N", "", null));
                Properties.Add(new Property(24, "ExhaustConnection", record.Fields[24], "mm", "N", "", null));
            }

        }
    }

    static class Extensions
    {
        /// <summary>
        /// Get substring of specified number of characters on the right.
        /// </summary>
        public static string Right(this string value, int length)
        {
            return value.Substring(value.Length - length);
        }
    }
}
