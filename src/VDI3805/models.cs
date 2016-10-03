using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDI3805
{
    public class RecordSet
    {
        public string RecordType { get; set; }
        public string Index { get; set; }
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

        public LeadData_010 LeadData_010;

        public void RegisterProperties(string VdiPartNumber,List<RecordSet> productDataSets)
        {
            //CAST PRODUCT TYPE
            //
            List<HeatGenerator> HeatGenerators = new List<HeatGenerator>();
            foreach (RecordSet productDataSet in productDataSets) HeatGenerators.Add(new HeatGenerator(productDataSet, CountryCode, "421.10"));
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
      
        public void MapBsNumbers()
        {
            //foreach (BsNumber_800 bsNumber in Products)
            //{ 
            //    foreach (ProductMainGroup1_100 pmg1 in Prod
            //                                   .Where(x => ("000"+x.Index).Right(3) == bsNumber.Productmaingroup1_100)
            //                                   .OrderBy(x => x.Index))
            //        bsNumber.ProductMainGroups1.Add(pmg1);

            //    foreach (ProductMainGroup2_110 pmg2 in ProductMainGroups2
            //                                   .Where(x => ("000" + x.Index).Right(3) == bsNumber.Productmaingroup2_110)
            //                                   .OrderBy(x => x.Index))
            //        bsNumber.ProductMainGroups2.Add(pmg2);

            //    foreach (HeatGenerator hg in HeatGenerators)
            //           foreach (Property prop in hg.Properties
            //                               .Where(x=>x.FieldName.TextDe=="Index")
            //                               .Where(y=>("00000" + y.Value).Right(5) == bsNumber.Productelementdata_700)
            //                               .OrderBy(z => z.ItemNumber))
            //                                    bsNumber.Properties.Add(prop);

            //}
        }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class LeadData_010
    {
        public string RecordType;
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
        public List<ProductMainGroup1_100> ProductMainGroup1s_100;
        public List<BsNumber_800> BsNumber_800;
        public List<ProductAccessory_900> ProductAccessories_900;
        public List<AccessorySelection1_930> AccessorySelection1s_930;
        public List<MediaData_960> MediaDatas_960;
        public List<GeometryData_970> GeometryDatas_970;

        private LeadData_010() {}

        public LeadData_010(List<RecordSet> recordSets)
        {
            RecordSet dataSet = recordSets.Where(x => x.RecordType == "010").FirstOrDefault();

            while (dataSet.Fields.Count <= 12) dataSet.Fields.Add(string.Empty);
            RecordType = dataSet.Fields[1];
            NumberGuidelinePart = dataSet.Fields[2];
            IssueMonth = dataSet.Fields[3];
            ManufacturerName = dataSet.Fields[4];
            RevisionDateFile = dataSet.Fields[5];
            ManufacturerUrl = dataSet.Fields[6];
            Comment = dataSet.Fields[7];
            GlobalLocationNumber = dataSet.Fields[8];
            DataRecordValidation = dataSet.Fields[9];
            NameTestAuthority = dataSet.Fields[10];
            MediaDetails = dataSet.Fields[11];

            BsNumber_800 = new BsNumber_800().Register(recordSets);

            ProductMainGroup1s_100 = new ProductMainGroup1_100().Register(recordSets, BsNumber_800);
            ProductAccessories_900 = new ProductAccessory_900().Register(recordSets);
            AccessorySelection1s_930 = new AccessorySelection1_930().Register(recordSets);
            MediaDatas_960 = new MediaData_960().Register(recordSets);
            GeometryDatas_970 = new GeometryData_970().Register(recordSets);
        }
    }

    public class ProductMainGroup1_100
    {
        public string RecordType;
        public string Index;
        public string SortNumberDisplaySequence;
        public string ProductDesignation;
        public string ProductMainGroup;
        public string ProductClassification;
        public string CreationDate;
        public string Comment;
        public string MediaDetails;
        public List<ProductMainGroup2_110> ProductMainGroups2_110;

        public ProductMainGroup1_100() { }

        public List<ProductMainGroup1_100> Register(List<RecordSet> recordSets, List<BsNumber_800> BsNumbers)
        {
            List<ProductMainGroup1_100> ProductMainGroup1s_100 = new List<ProductMainGroup1_100>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "100").ToList())
            {
                ProductMainGroup1_100 ProductMainGroup1_100 = new ProductMainGroup1_100();
                while (dataSet.Fields.Count <= 9) dataSet.Fields.Add(string.Empty);
                ProductMainGroup1_100.RecordType = dataSet.Fields[1];
                ProductMainGroup1_100.Index = ("000"+dataSet.Fields[2]).Right(3);
                ProductMainGroup1_100.SortNumberDisplaySequence = dataSet.Fields[3];
                ProductMainGroup1_100.ProductDesignation = dataSet.Fields[4];
                ProductMainGroup1_100.ProductMainGroup = dataSet.Fields[5];
                ProductMainGroup1_100.ProductClassification = dataSet.Fields[6];
                ProductMainGroup1_100.CreationDate = dataSet.Fields[7];
                ProductMainGroup1_100.Comment = dataSet.Fields[8];
                ProductMainGroup1_100.MediaDetails = dataSet.Fields[9];
                
                ProductMainGroup1_100.ProductMainGroups2_110 = new ProductMainGroup2_110().Register(recordSets, BsNumbers, BsNumbers.Where(x => x.Productmaingroup1_100 == ProductMainGroup1_100.Index)
                                                                                                                                    .Select(y => y.Productmaingroup2_110).ToArray());
                ProductMainGroup1s_100.Add(ProductMainGroup1_100);
            }
            return ProductMainGroup1s_100;
        }
    }

    public class ProductMainGroup2_110
    {
        public string RecordType;
        public string Index;
        public string SortNumberDisplaySequence;
        public string ProductDesignation;
        public string ProductMainGroup;
        public string ProductClassification;
        public string CreationDate;
        public string Comment;
        public string MediaDetails;
        public List<AccessoryMainGroup2_160> AccessoryMainGroup2_160;
        public List<ProductVariantA_200> ProductVariantA_200;
        public List<ProductVariantB_300> ProductVariantB_300;
        public List<ProductVariantC_400> ProductVariantC_400;
        public List<ProductVariantD_500> ProductVariantD_500;
        public List<FunctionsDeclaration_600> FunctionsDeclarations_600;
        public List<ProductElementData_700> ProductElementData_700;     
        public List<AccessoryGroup_830> AccessoryGroup_830;

        public ProductMainGroup2_110() { }

        public List<ProductMainGroup2_110> Register(List<RecordSet> recordSets, List<BsNumber_800> BsNumbers, string[] indexFilter)
        {
            List<ProductMainGroup2_110> ProductMainGroup2s_110 = new List<ProductMainGroup2_110>();

            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "110")
                                                    .Where(x=> indexFilter.Contains(x.Index))
                                                    .ToList())
            {
                ProductMainGroup2_110 ProductMainGroup2_110 = new ProductMainGroup2_110();
                while (dataSet.Fields.Count <= 9) dataSet.Fields.Add(string.Empty);
                ProductMainGroup2_110.RecordType = dataSet.Fields[1];
                ProductMainGroup2_110.Index = ("000" + dataSet.Fields[2]).Right(3);
                ProductMainGroup2_110.SortNumberDisplaySequence = dataSet.Fields[3];
                ProductMainGroup2_110.ProductDesignation = dataSet.Fields[4];
                ProductMainGroup2_110.ProductMainGroup = dataSet.Fields[5];
                ProductMainGroup2_110.ProductClassification = dataSet.Fields[6];
                ProductMainGroup2_110.CreationDate = dataSet.Fields[7];
                ProductMainGroup2_110.Comment = dataSet.Fields[8];
                ProductMainGroup2_110.MediaDetails = dataSet.Fields[9];

                ProductMainGroup2_110.AccessoryMainGroup2_160 = new AccessoryMainGroup2_160().Register(recordSets,BsNumbers.Where(x => x.Productmaingroup2_110 == ProductMainGroup2_110.Index)
                                                                                                                                                   .Select(y => y.Accessorymaingroup2_160).ToArray());
                ProductMainGroup2_110.ProductVariantA_200 = new ProductVariantA_200().Register(recordSets, BsNumbers.Where(x => x.Productmaingroup2_110 == ProductMainGroup2_110.Index)
                                                                                                                                                   .Select(y => y.ProductvariantA_200).ToArray());
                ProductMainGroup2_110.ProductVariantB_300 = new ProductVariantB_300().Register(recordSets, BsNumbers.Where(x => x.Productmaingroup2_110 == ProductMainGroup2_110.Index)
                                                                                                                                                   .Select(y => y.ProductvariantB_300).ToArray());
                ProductMainGroup2_110.ProductVariantC_400 = new ProductVariantC_400().Register(recordSets, BsNumbers.Where(x => x.Productmaingroup2_110 == ProductMainGroup2_110.Index)
                                                                                                                                                   .Select(y => y.ProductvariantC_400).ToArray());
                ProductMainGroup2_110.ProductVariantD_500 = new ProductVariantD_500().Register(recordSets, BsNumbers.Where(x => x.Productmaingroup2_110 == ProductMainGroup2_110.Index)
                                                                                                                                                   .Select(y => y.ProductvariantD_500).ToArray());
                ProductMainGroup2_110.FunctionsDeclarations_600 = new FunctionsDeclaration_600().Register(recordSets);

                ProductMainGroup2_110.ProductElementData_700 = new ProductElementData_700().Register(recordSets);
                ProductMainGroup2_110.AccessoryGroup_830 = new AccessoryGroup_830().Register(recordSets);
                ProductMainGroup2s_110.Add(ProductMainGroup2_110);
            }

            return ProductMainGroup2s_110;
        }
    }

    public class AccessoryMainGroup2_160
    {
        public string RecordType;
        public string Index;
        public string VariantVariantTypeAccessoryDesignation;
        public string IndexOfStartingDataRecord960;
        public string IndexOfRecordOfRecordType930;
        public List<CrossReferenceAccessory_160_01> CrossReferenceAccessory_160_01;

        public AccessoryMainGroup2_160() { }

        public List<AccessoryMainGroup2_160> Register(List<RecordSet> recordSets, string[] indexFilter)
        {
            List<AccessoryMainGroup2_160> AccessoryMainGroup2s_160 = new List<AccessoryMainGroup2_160>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "160")
                                                    .Where(x => indexFilter.Contains(x.Index))
                                                    .ToList())
            {
                AccessoryMainGroup2_160 AccessoryMainGroup2_160 = new AccessoryMainGroup2_160();
                while (dataSet.Fields.Count <= 5) dataSet.Fields.Add(string.Empty);
                AccessoryMainGroup2_160.RecordType = dataSet.Fields[1];
                AccessoryMainGroup2_160.Index = ("000" + dataSet.Fields[2]).Right(3);
                AccessoryMainGroup2_160.VariantVariantTypeAccessoryDesignation = dataSet.Fields[3];
                AccessoryMainGroup2_160.IndexOfStartingDataRecord960 = dataSet.Fields[4];
                AccessoryMainGroup2_160.IndexOfRecordOfRecordType930 = dataSet.Fields[5];
                AccessoryMainGroup2s_160.Add(AccessoryMainGroup2_160);
            }

            //CrossReferenceAccessory_160_01 = new CrossReferenceAccessory_160_01().Register(recordSets);
            return AccessoryMainGroup2s_160;
        }
    }

    public class CrossReferenceAccessory_160_01
    {
        public string RecordType;
        public string Index;
        public string AccessoryIndex;
        public string Quantity;
        public string FixedOrOptionalAllocation; // f= fixed o=optional
        public string AllocationToAccessory; //P=per project; A=per article

        public CrossReferenceAccessory_160_01() { }

        public List<CrossReferenceAccessory_160_01> Register(List<RecordSet> recordSets, string[] indexFilter)
        {
            List<CrossReferenceAccessory_160_01> CrossReferenceAccessories_160_01 = new List<CrossReferenceAccessory_160_01>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "160")
                                                                    .Where(x => indexFilter.Contains(x.Index))
                                                                    .ToList())
            {
                CrossReferenceAccessory_160_01 CrossReferenceAccessory_160_01 = new CrossReferenceAccessory_160_01();
                while (dataSet.Fields.Count <= 6) dataSet.Fields.Add(string.Empty);
                CrossReferenceAccessory_160_01.RecordType = dataSet.Fields[1];
                CrossReferenceAccessory_160_01.Index = ("000" + dataSet.Fields[2]).Right(3);
                CrossReferenceAccessory_160_01.AccessoryIndex = dataSet.Fields[3];
                CrossReferenceAccessory_160_01.Quantity = dataSet.Fields[4];
                CrossReferenceAccessory_160_01.FixedOrOptionalAllocation = dataSet.Fields[5];
                CrossReferenceAccessory_160_01.AllocationToAccessory = dataSet.Fields[5];
                CrossReferenceAccessories_160_01.Add(CrossReferenceAccessory_160_01);
            }

            return CrossReferenceAccessories_160_01;
        }
    }

    public class ProductVariantA_200
    {
        public string RecordType;
        public string Index;
        public string AccessoryDesignation;
        public string MediaDetails;
        public string AccessorySelection1;

        public ProductVariantA_200() { }

        public List<ProductVariantA_200> Register(List<RecordSet> recordSets, string[] indexFilter)
        {
            List<ProductVariantA_200> ProductVariantAs_200 = new List<ProductVariantA_200>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "200")
                                                                    .Where(x => indexFilter.Contains(x.Index))
                                                                    .ToList())
            {
                ProductVariantA_200 ProductVariantA_200 = new ProductVariantA_200();
                while (dataSet.Fields.Count <= 5) dataSet.Fields.Add(string.Empty);
                ProductVariantA_200.RecordType = dataSet.Fields[1];
                ProductVariantA_200.Index = ("000" + dataSet.Fields[2]).Right(3);
                ProductVariantA_200.AccessoryDesignation = dataSet.Fields[3];
                ProductVariantA_200.MediaDetails = dataSet.Fields[4];
                ProductVariantA_200.AccessorySelection1 = dataSet.Fields[5];
                ProductVariantAs_200.Add(ProductVariantA_200);
            }

            return ProductVariantAs_200;
        }
    }

    public class CrossReferenceAccessory_200_01
    {

        public CrossReferenceAccessory_200_01() { }
    }

    public class VariantTypeA1_250
    {
        public VariantTypeA1_250() { }
    }

    public class CrossReferenceAccessory_250_01
    {
        public CrossReferenceAccessory_250_01() { }
    }

    public class VariantTypeA2_260
    {
        public VariantTypeA2_260() { }
    }

    public class CrossReferenceAccessory_260_01
    {
        public CrossReferenceAccessory_260_01() { }
    }

    public class ProductVariantB_300
    {
        public string RecordType;
        public string Index;
        public string AccessoryDesignation;
        public string MediaDetails;
        public string AccessorySelection1;

        public ProductVariantB_300() { }

        public List<ProductVariantB_300> Register(List<RecordSet> recordSets, string[] indexFilter)
        {
            List<ProductVariantB_300> ProductVariantBs_300 = new List<ProductVariantB_300>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "300")
                                                                    .Where(x => indexFilter.Contains(x.Index))
                                                                    .ToList())
            {
                ProductVariantB_300 ProductVariantB_300 = new ProductVariantB_300();
                while (dataSet.Fields.Count <= 5) dataSet.Fields.Add(string.Empty);
                ProductVariantB_300.RecordType = dataSet.Fields[1];
                ProductVariantB_300.Index = ("000" + dataSet.Fields[2]).Right(3);
                ProductVariantB_300.AccessoryDesignation = dataSet.Fields[3];
                ProductVariantB_300.MediaDetails = dataSet.Fields[4];
                ProductVariantB_300.AccessorySelection1 = dataSet.Fields[5];
                ProductVariantBs_300.Add(ProductVariantB_300);
            }

            return ProductVariantBs_300;
        }
    }

    public class CrossReferenceAccessory_300_01
    {
        public CrossReferenceAccessory_300_01() { }
    }

    public class VariantTypeB1_350
    {
        public VariantTypeB1_350() { }
    }

    public class CrossReferenceAccessory_350_01
    {
        public CrossReferenceAccessory_350_01() { }
    }

    public class VariantTypeB2_360
    {
        public VariantTypeB2_360() { }
    }

    public class CrossReferenceAccessory_360_01
    {
        public CrossReferenceAccessory_360_01() { }
    }

    public class ProductVariantC_400
    {
        public string RecordType;
        public string Index;
        public string AccessoryDesignation;
        public string MediaDetails;
        public string AccessorySelection1;

        public ProductVariantC_400() { }

        public List<ProductVariantC_400> Register(List<RecordSet> recordSets, string[] indexFilter)
        {
            List<ProductVariantC_400> ProductVariantCs_400 = new List<ProductVariantC_400>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "400")
                                                                    .Where(x => indexFilter.Contains(x.Index))
                                                                    .ToList())
            {
                ProductVariantC_400 ProductVariantC_400 = new ProductVariantC_400();
                while (dataSet.Fields.Count <= 5) dataSet.Fields.Add(string.Empty);
                ProductVariantC_400.RecordType = dataSet.Fields[1];
                ProductVariantC_400.Index = ("000" + dataSet.Fields[2]).Right(3);
                ProductVariantC_400.AccessoryDesignation = dataSet.Fields[3];
                ProductVariantC_400.MediaDetails = dataSet.Fields[4];
                ProductVariantC_400.AccessorySelection1 = dataSet.Fields[5];
                ProductVariantCs_400.Add(ProductVariantC_400);
            }

            return ProductVariantCs_400;
        }
    }

    public class CrossReferenceAccessory_400_01
    {
        public CrossReferenceAccessory_400_01() { }
    }

    public class VariantTypeC1_450
    {
        public VariantTypeC1_450() { }
    }

    public class CrossReferenceAccessory_450_01
    {
        public CrossReferenceAccessory_450_01() { }
    }

    public class VariantTypeC2_460
    {
        public VariantTypeC2_460() { }
    }

    public class CrossReferenceAccessory_460_01
    {
        public CrossReferenceAccessory_460_01() { }
    }

    public class ProductVariantD_500
    {
        public string RecordType;
        public string Index;
        public string AccessoryDesignation;
        public string MediaDetails;
        public string AccessorySelection1;

        public ProductVariantD_500() { }

        public List<ProductVariantD_500> Register(List<RecordSet> recordSets, string[] indexFilter)
        {
            List<ProductVariantD_500> ProductVariantD_500s = new List<ProductVariantD_500>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "500")
                                                                 .Where(x => indexFilter.Contains(x.Index))
                                                                 .ToList())
            {
                ProductVariantD_500 ProductVariantD_500 = new ProductVariantD_500();
                while (dataSet.Fields.Count <= 5) dataSet.Fields.Add(string.Empty);
                ProductVariantD_500.RecordType = dataSet.Fields[1];
                ProductVariantD_500.Index = ("000" + dataSet.Fields[2]).Right(3);
                ProductVariantD_500.AccessoryDesignation = dataSet.Fields[3];
                ProductVariantD_500.MediaDetails = dataSet.Fields[4];
                ProductVariantD_500.AccessorySelection1 = dataSet.Fields[5];
                ProductVariantD_500s.Add(ProductVariantD_500);
            }

            return ProductVariantD_500s;
        }
    }

    public class CrossReferenceAccessory_500_01
    {
        public CrossReferenceAccessory_500_01() { }
    }

    public class VariantTypeD1_550
    {
        public VariantTypeD1_550() { }
    }

    public class CrossReferenceAccessory_550_01
    {
        public CrossReferenceAccessory_550_01() { }
    }

    public class VariantTypeD2_560
    {
        public VariantTypeD2_560() { }
    }

    public class FunctionsDeclaration_600

    {
        public string RecordType;
        public string Index;
        public string DeclarationOfFunctionsAndParameters;
        public string Comment;

        public FunctionsDeclaration_600() { }

        public List<FunctionsDeclaration_600> Register(List<RecordSet> recordSets)
        {
            List<FunctionsDeclaration_600> FunctionsDeclarations_600 = new List<FunctionsDeclaration_600>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "600")
                                                                 .ToList())
            {
                FunctionsDeclaration_600 FunctionsDeclaration_600 = new FunctionsDeclaration_600();
                while (dataSet.Fields.Count <= 4) dataSet.Fields.Add(string.Empty);
                FunctionsDeclaration_600.RecordType = dataSet.Fields[1];
                FunctionsDeclaration_600.Index = ("000" + dataSet.Fields[2]).Right(3);
                FunctionsDeclaration_600.DeclarationOfFunctionsAndParameters = dataSet.Fields[3];
                FunctionsDeclaration_600.Comment = dataSet.Fields[4];
                FunctionsDeclarations_600.Add(FunctionsDeclaration_600);
            }

            return FunctionsDeclarations_600;
        }
    }

    public class FunctionsDefinition_610
    {
        public FunctionsDefinition_610() { }
    }

    public class ProductElementData_700
    {
        public string RecordType;
        public int Index;

        public ProductElementData_700() { }

        public List<ProductElementData_700> Register(List<RecordSet> recordSets)
        {
            List<ProductElementData_700> ProductElementDatas_700 = new List<ProductElementData_700>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "160").ToList())
            {
                ProductElementData_700 ProductElementData_700 = new ProductElementData_700();
                while (dataSet.Fields.Count <= 6) dataSet.Fields.Add(string.Empty);
                ProductElementData_700.RecordType = dataSet.Fields[1];
                ProductElementData_700.Index = Convert.ToInt32(dataSet.Fields[2]);
                ProductElementDatas_700.Add(ProductElementData_700);
            }

            return ProductElementDatas_700;
        }


        //manufacturerFile.RegisterProductData(dataSets.Where(x => x.SetType == "800").ToList());
        //manufacturerFile.RegisterProperties(manufacturerFile.VDI3805PartNumber, dataSets.Where(x => x.SetType == "700").ToList());
        //manufacturerFile.MapBsNumbers();
        //switch (datensatz.SetType)
        //    {
        //        case "100":
        //            manufacturerFile.ProductMainGroups1.Add(new ProductMainGroup1(datensatz));
        //            break;
        //        case "110":
        //            manufacturerFile.ProductMainGroups2.Add(new ProductMainGroup2(datensatz));
        //            break;
        //        case "160": case "260": case "360": case "460": case "560": case "760":
        //        case "200": case "300": case "400": case "500":
        //        case "250": case "350": case "450": case "550":
        //            manufacturerFile.Variants.Add(new Variant(datensatz));
        //            break;
        //        case "600":
        //            manufacturerFile.Declarations.Add(new Declaration(datensatz));
        //            break;
        //        case "700":
        //            switch (manufacturerFile.VDI3805PartNumber)
        //            {
        //                case "PART02": //Heizungsarmaturen
        //                    break;
        //                case "PART03": //Wärmeerzeuger
        //                               //Heat Generators
        //                               //VDI2552 : 421.10

        //                    manufacturerFile.HeatGenerators.Add(new HeatGenerator(datensatz, manufacturerFile.CountryCode, "421.10"));
        //                    break;
        //                case "PART04": //Pumpen
        //                    break;
        //                case "PART05": //Luftdurchlässe
        //                    break;
        //                case "PART06": //Heizkörper
        //                    break;
        //                case "PART07": //Ventilatoren
        //                    break;
        //                case "PART08": //Brenner
        //                    break;
        //                case "PART09": //Modullüftungsgeräte
        //                    break;
        //                case "PART10": //Luftfilter
        //                    break;
        //                case "PART11": //Wärmetauscher Fluid/ Wasserdampf - Luft
        //                    break;
        //                case "PART14": //RLT - Schalldämpfer(passiv)
        //                    break;
        //                case "PART16": //Brandschutzklappe
        //                    break;
        //                case "PART17": //Armaturen für die Trinkwasserinstallation
        //                    break;
        //                case "PART18": //Flächenheizung / -kühlung
        //                    break;
        //                case "PART19": //Sonnenkollektoren
        //                    break;
        //                case "PART20": //Speicher und Durchlauferhitzer
        //                    break;
        //                case "PART22": //Wärmepumpen
        //                    break;
        //                case "PART23": //Wohnungslüftungsgeräte
        //                    break;
        //                case "PART25": //Deckenkühlelemente
        //                    break;
        //                case "PART29": //Rohre und Formstücke
        //                    break;
        //                case "PART32": //Verteiler / Sammler
        //                    break;
        //                case "PART35": //Klappen, Blenden und Volumenstromregler
        //                    break;
        //                case "PART37": //Dezentrale Fassadenlüftungsgeräte
        //                    break;
        //                case "PART99": //Allgemeine Komponenten
        //                    break;
        //            }
        //            break;
        //        default:
        //            break;

        //    }
        //}

    }

    public class ProductSpecifics_710
    {
        public ProductSpecifics_710() { }

    }

    public class AccessoryProductElement_760
    {
        public AccessoryProductElement_760() { }

    }

    public class CrossReferenceAccessory_760_01
    {
        public CrossReferenceAccessory_760_01() { }

    }

    public class CrossReferenceGeometry_720
    {
        public CrossReferenceGeometry_720() { }

    }

    public class FunctionInternalData_730
    {
        public FunctionInternalData_730() { }

    }

    public class BsNumber_800
    {
        public string RecordType;
        public string Index;
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

        public BsNumber_800() { }

        public List<BsNumber_800> Register(List<RecordSet> recordSets)
        {
            List<BsNumber_800> BsNumbers_800 = new List<BsNumber_800>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "800").ToList())
            {
                BsNumber_800 BsNumber_800 = new BsNumber_800();
                while (dataSet.Fields.Count <= 6) dataSet.Fields.Add(string.Empty);
                BsNumber_800.RecordType = dataSet.Fields[1].Substring(0, 3);
                BsNumber_800.Index = ("000" + dataSet.Fields[2]).Right(3);
                BsNumber_800.Number = dataSet.Fields[3].Substring(0, 53);
                BsNumber_800.Comment = dataSet.Fields[4];

                //The BS number consists of numerals and question marks only and is constructed according to the following pattern
                // AAABBBCCCDDDEEEFFFGGGHHHIIIJJJKKKLLLMMMNNNOOOPPPPPQQQ""
                BsNumber_800.Productmaingroup1_100 = BsNumber_800.Number.Substring(0, 3);
                BsNumber_800.Productmaingroup2_110 = BsNumber_800.Number.Substring(3, 3);
                BsNumber_800.Accessorymaingroup2_160 = BsNumber_800.Number.Substring(6, 3);
                BsNumber_800.ProductvariantA_200 = BsNumber_800.Number.Substring(9, 3);
                BsNumber_800.VarianttypeA1_250 = BsNumber_800.Number.Substring(12, 3);
                BsNumber_800.VarianttypeA2_260 = BsNumber_800.Number.Substring(15, 3);
                BsNumber_800.ProductvariantB_300 = BsNumber_800.Number.Substring(18, 3);
                BsNumber_800.VarianttypeB1_350 = BsNumber_800.Number.Substring(21, 3);
                BsNumber_800.VarianttypeB2_360 = BsNumber_800.Number.Substring(24, 3);
                BsNumber_800.ProductvariantC_400 = BsNumber_800.Number.Substring(27, 3);
                BsNumber_800.VarianttypeC1_450 = BsNumber_800.Number.Substring(30, 3);
                BsNumber_800.VarianttypeC2_460 = BsNumber_800.Number.Substring(33,3);
                BsNumber_800.ProductvariantD_500 = BsNumber_800.Number.Substring(36,3);
                BsNumber_800.VarianttypeD1_550 = BsNumber_800.Number.Substring(39,3);
                BsNumber_800.VarianttypeD2_560 = BsNumber_800.Number.Substring(42, 3);
                BsNumber_800.Productelementdata_700 = BsNumber_800.Number.Substring(45, 5);
                BsNumber_800.Accessoryproductelement_760 = BsNumber_800.Number.Substring(50, 3);

                BsNumbers_800.Add(BsNumber_800);
            }

            return BsNumbers_800;
        }
    }

    public class ArticelNumber_810
    {
        public ArticelNumber_810() { }

    }

    public class FollowingRecordFunctionName_820
    {
        public FollowingRecordFunctionName_820() { }

    }

    public class AccessoryGroup_830
    {
        public string RecordType;
        public string Index;
        public string IndexPointerToRecordType930;

        public AccessoryGroup_830() { }

        public List<AccessoryGroup_830> Register(List<RecordSet> recordSets)
        {
            List<AccessoryGroup_830> AccessoryGroup_830s = new List<AccessoryGroup_830>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "830")
                                                                 .ToList())
            {
                AccessoryGroup_830 AccessoryGroup_830 = new AccessoryGroup_830();
                while (dataSet.Fields.Count <= 4) dataSet.Fields.Add(string.Empty);
                AccessoryGroup_830.RecordType = dataSet.Fields[1].Substring(0, 3);
                AccessoryGroup_830.Index = ("000" + dataSet.Fields[2]).Right(3);
                AccessoryGroup_830.IndexPointerToRecordType930 = dataSet.Fields[3];

                AccessoryGroup_830s.Add(AccessoryGroup_830);
            }

            return AccessoryGroup_830s;
        }

    }

    public class ProductAccessory_900
    {
        public string RecordType;
        public string Index;
        public string NumberOfGuideline;
        public string NameOfAccessoryManufacturer;
        public string RevisionDateOfFile;
        public string BsNumberOfAccessory;
        public string ManufacturerReferenceNumberOfAccessory;
        public string DatanormNumberOfAccessory;
        public string StlbNumberOfAccessory;
        public string GtinNumberOfAccessory;
        public string AccessoryDescription;
        public string QuantityUnit;  //see Table 2
        public string QuantiyPerPackingUnit; //see Table 2
        public string NameOfPackingUnit;
        public string MediaLinkNumber;
        public string MassNetKg;

        public ProductAccessory_900() { }

        public List<ProductAccessory_900> Register(List<RecordSet> recordSets)
        {

            List<ProductAccessory_900> ProductAccessories_900 = new List<ProductAccessory_900>();

            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "900").ToList())
            {
                ProductAccessory_900 ProductAccessory_900 = new ProductAccessory_900();
                while (dataSet.Fields.Count <= 16) dataSet.Fields.Add(string.Empty);

                ProductAccessory_900.RecordType = dataSet.Fields[1];
                ProductAccessory_900.Index = dataSet.Fields[2];
                ProductAccessory_900.NumberOfGuideline = dataSet.Fields[3];
                ProductAccessory_900.NameOfAccessoryManufacturer = dataSet.Fields[4];
                ProductAccessory_900.RevisionDateOfFile = dataSet.Fields[5];
                ProductAccessory_900.BsNumberOfAccessory = dataSet.Fields[6];
                ProductAccessory_900.ManufacturerReferenceNumberOfAccessory = dataSet.Fields[7];
                ProductAccessory_900.DatanormNumberOfAccessory = dataSet.Fields[8];
                ProductAccessory_900.StlbNumberOfAccessory = dataSet.Fields[9];
                ProductAccessory_900.GtinNumberOfAccessory = dataSet.Fields[10];
                ProductAccessory_900.AccessoryDescription = dataSet.Fields[11];
                ProductAccessory_900.QuantityUnit = dataSet.Fields[12];
                ProductAccessory_900.QuantiyPerPackingUnit = dataSet.Fields[13];
                ProductAccessory_900.NameOfPackingUnit = dataSet.Fields[14];
                ProductAccessory_900.MediaLinkNumber = dataSet.Fields[15];
                ProductAccessory_900.MassNetKg = dataSet.Fields[16];
                ProductAccessories_900.Add(ProductAccessory_900);
            }

            return ProductAccessories_900;
        }
    }

    public class CrossReferenceAccessoryGeometry_920
    {
        public CrossReferenceAccessoryGeometry_920() { }

    }

    public class AccessorySelection1_930
    {
        public string RecordType;
        public string Index;
        public string AccessoryRange;
        public string MinimumSelectable;
        public string MaximumSelectable;

        public AccessorySelection1_930() { }

        public List<AccessorySelection1_930> Register(List<RecordSet> recordSets)
        {
            List<AccessorySelection1_930> AccessorySelection1s_930 = new List<AccessorySelection1_930>();

            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "930").ToList())
            {
                AccessorySelection1_930 AccessorySelection1_930 = new AccessorySelection1_930();
                while (dataSet.Fields.Count <= 5) dataSet.Fields.Add(string.Empty);
                AccessorySelection1_930.RecordType = dataSet.Fields[1];
                AccessorySelection1_930.Index = dataSet.Fields[2];
                AccessorySelection1_930.AccessoryRange = dataSet.Fields[3];
                AccessorySelection1_930.MinimumSelectable = dataSet.Fields[4];
                AccessorySelection1_930.MaximumSelectable = dataSet.Fields[5];
                AccessorySelection1s_930.Add(AccessorySelection1_930);
            }
            return AccessorySelection1s_930;
        }

    }

    public class AccessorySelection2_930_01
    {
        public AccessorySelection2_930_01() { }

    }

    public class Requirement1_930_02
    {
        public Requirement1_930_02() { }

    }

    public class AccessoryArticle_930_03
    {
        public AccessoryArticle_930_03() { }

    }

    public class Requirement2_930_04
    {
        public Requirement2_930_04() { }

    }

    public class MediaData_960
    {
        public string RecordType;
        public int Index;
        public string MediaLinkNumber;
        public string LinkType;
        public string FileNameOfLink;
        public string ExtensionOfFileName;
        public string ContentDescription;

        public MediaData_960() { }


        public List<MediaData_960> Register(List<RecordSet> recordSets)
        {
            List<MediaData_960> MediaDatas_960 = new List<MediaData_960>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "960").ToList())
            {
                MediaData_960 MediaData_960 = new MediaData_960();
                while (dataSet.Fields.Count <= 7) dataSet.Fields.Add(string.Empty);
                MediaData_960.RecordType = dataSet.Fields[1];
                MediaData_960.Index = Convert.ToInt32(dataSet.Fields[2]);
                MediaData_960.MediaLinkNumber = dataSet.Fields[3];
                MediaData_960.LinkType = dataSet.Fields[4];
                MediaData_960.FileNameOfLink = dataSet.Fields[5];
                MediaData_960.ExtensionOfFileName = dataSet.Fields[6];
                MediaData_960.ContentDescription = dataSet.Fields[7];
                MediaDatas_960.Add(MediaData_960);
            }
            return MediaDatas_960;
        }
    }

    public class GeometryData_970
    {
        public string RecordType;
        public int Index;
        public string ComponentDesignation;
        public string SymbolNumber;
        public string DisturbanceSpaceForm;
        public string PositionX;
        public string PositionY;
        public string PositionZ;
        public string X1;
        public string Y1;
        public string Z1;
        public string X2;
        public string Y2;
        public string Z2;
        public string Parameter01;
        public string Parameter02;
        public string Parameter03;
        public string Parameter04;
        public string Parameter05;
        public string Parameter06;
        public string Parameter07;
        public string Parameter08;
        public string Parameter09;
        public string Parameter10;
        public string IndexOfStartingData;

        public List<ProductSpace_970_11> ProductSpaces_970_11;
        public List<OperatingSpace_970_12> OperatingSpaces_970_12;
        public List<PlacementSpace_970_13> PlacementSpaces_970_13;
        public List<InstallationSpace_970_14> InstallationSpaces_970_14;
        public List<ConnectionData_970_01> ConnectionDatas_970_01;
        public List<FormDataA_970_02> FormDataAs_970_02;
        public List<FormDataB_970_03> FormDataBs_970_03;
        public List<MaterialData_970_04> MaterialDatas_970_04;
        public List<SymbolDataA_970_05> SymbolDataAs_970_05;
        public List<SymbolDataB_970_06> SymbolDataBs_970_06;

        public GeometryData_970() { }

        public List<GeometryData_970> Register(List<RecordSet> recordSets)
            {
                List<GeometryData_970> GeometryDatas_970 = new List<GeometryData_970>();
                foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "970").ToList())
                {
                    GeometryData_970 GeometryData_970 = new GeometryData_970();
                    while (dataSet.Fields.Count <= 25) dataSet.Fields.Add(string.Empty);
                    GeometryData_970.RecordType = dataSet.Fields[1];
                    GeometryData_970.Index = Convert.ToInt16(dataSet.Fields[2]);
                    GeometryData_970.ComponentDesignation = dataSet.Fields[3];
                    GeometryData_970.SymbolNumber = dataSet.Fields[4];
                    GeometryData_970.DisturbanceSpaceForm = dataSet.Fields[5];
                    GeometryData_970.PositionX = dataSet.Fields[6];
                    GeometryData_970.PositionY = dataSet.Fields[7];
                    GeometryData_970.PositionZ = dataSet.Fields[8];
                    GeometryData_970.X1 = dataSet.Fields[9];
                    GeometryData_970.Y1 = dataSet.Fields[10];
                    GeometryData_970.Z1 = dataSet.Fields[11];
                    GeometryData_970.X2 = dataSet.Fields[12];
                    GeometryData_970.Y2 = dataSet.Fields[13];
                    GeometryData_970.Z2 = dataSet.Fields[14];
                    GeometryData_970.Parameter01 = dataSet.Fields[15];
                    GeometryData_970.Parameter02 = dataSet.Fields[16];
                    GeometryData_970.Parameter03 = dataSet.Fields[17];
                    GeometryData_970.Parameter04 = dataSet.Fields[18];
                    GeometryData_970.Parameter05 = dataSet.Fields[19];
                    GeometryData_970.Parameter06 = dataSet.Fields[20];
                    GeometryData_970.Parameter07 = dataSet.Fields[21];
                    GeometryData_970.Parameter08 = dataSet.Fields[22];
                    GeometryData_970.Parameter09 = dataSet.Fields[23];
                    GeometryData_970.Parameter10 = dataSet.Fields[24];
                    GeometryData_970.IndexOfStartingData = dataSet.Fields[25];
                    GeometryDatas_970.Add(GeometryData_970);
                }
            return GeometryDatas_970;
            }
    }   

    public class ConnectionData_970_01
    {
        public ConnectionData_970_01() { }

    }

    public class FormDataA_970_02
    {
        public FormDataA_970_02() { }

    }

    public class FormDataB_970_03
    {
        public FormDataB_970_03() { }

    }

    public class MaterialData_970_04
    {
        public MaterialData_970_04() { }

    }

    public class SymbolDataA_970_05
    {
        public SymbolDataA_970_05() { }

    }

    public class SymbolDataB_970_06
    {
        public SymbolDataB_970_06() { }

    }

    public class ProductSpace_970_11
    {
        public ProductSpace_970_11() { }

    }

    public class OperatingSpace_970_12
    {
        public OperatingSpace_970_12() { }

    }

    public class PlacementSpace_970_13
    {
        public PlacementSpace_970_13() { }

    }

    public class InstallationSpace_970_14
    {
        public InstallationSpace_970_14() { }

    }

    public class MaterialAssignment_970_41
    {
        public MaterialAssignment_970_41() { }

    }

    public class HeatGenerator  //VDI 3805 Part 3
    {
        public bool IsValid = true;

        public List<Property> Properties;

        public HeatGenerator() { }

        public HeatGenerator(RecordSet record, string countryCode, string Vdi2552Code)
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
