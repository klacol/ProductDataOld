using System;
using System.Collections;
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

    [DisplayName("Vorlaufdaten")]
    public class LeadData_010
    {
        [LocalizedDescription("RecordType")]
        public string RecordType;

        [LocalizedDescription("NumberGuidelinePart")]
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

        public List<BuildingSystemNumber_800> BuildingSystemNumber_800s;

        public List<ProductMainGroup1_100> ProductMainGroup1_100s;
        public List<ProductAccessory_900> ProductAccessory_900s;
        public List<AccessorySelection1_930> AccessorySelection1_930s;
        public List<MediaData_960> MediaData_960s;
        public List<GeometryData_970> GeometryData_970s;

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

            BuildingSystemNumber_800s = new BuildingSystemNumber_800().Register(recordSets);  //This List is placed in the "written rule" in the class ProductMainGroup2_110

            ProductMainGroup1_100s = new ProductMainGroup1_100().Register(recordSets, BuildingSystemNumber_800s);
            ProductAccessory_900s = new ProductAccessory_900().Register(recordSets);
            AccessorySelection1_930s = new AccessorySelection1_930().Register(recordSets);
            MediaData_960s = new MediaData_960().Register(recordSets);
            GeometryData_970s = new GeometryData_970().Register(recordSets);
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
        public List<ProductMainGroup2_110> ProductMainGroup2_110s;

        public ProductMainGroup1_100() { }

        public List<ProductMainGroup1_100> Register(List<RecordSet> recordSets, List<BuildingSystemNumber_800> BsNumbers)
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
                
                ProductMainGroup1_100.ProductMainGroup2_110s = new ProductMainGroup2_110().Register(recordSets, BsNumbers, BsNumbers.Where(x => x.Productmaingroup1_100 == ProductMainGroup1_100.Index)
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
        public List<AccessoryMainGroup2_160> AccessoryMainGroup2_160s;
        public List<ProductVariantA_200> ProductVariantA_200s;
        public List<ProductVariantB_300> ProductVariantB_300s;
        public List<ProductVariantC_400> ProductVariantC_400s;
        public List<ProductVariantD_500> ProductVariantD_500s;
        public List<FunctionsDeclaration_600> FunctionsDeclaration_600s;
        public List<ProductElementData_700> ProductElementData_700s;     
        public List<AccessoryGroup_830> AccessoryGroup_830s;

        public ProductMainGroup2_110() { }

        public List<ProductMainGroup2_110> Register(List<RecordSet> recordSets, List<BuildingSystemNumber_800> BsNumbers, string[] indexFilter)
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

                ProductMainGroup2_110.AccessoryMainGroup2_160s = new AccessoryMainGroup2_160().Register(recordSets,BsNumbers.Where(x => x.Productmaingroup2_110 == ProductMainGroup2_110.Index)
                                                                                                                                                   .Select(y => y.Accessorymaingroup2_160).ToArray());
                ProductMainGroup2_110.ProductVariantA_200s = new ProductVariantA_200().Register(recordSets, BsNumbers, BsNumbers.Where(x => x.Productmaingroup2_110 == ProductMainGroup2_110.Index)
                                                                                                                                                   .Select(y => y.ProductvariantA_200).ToArray());
                ProductMainGroup2_110.ProductVariantB_300s = new ProductVariantB_300().Register(recordSets, BsNumbers.Where(x => x.Productmaingroup2_110 == ProductMainGroup2_110.Index)
                                                                                                                                                   .Select(y => y.ProductvariantB_300).ToArray());
                ProductMainGroup2_110.ProductVariantC_400s = new ProductVariantC_400().Register(recordSets, BsNumbers.Where(x => x.Productmaingroup2_110 == ProductMainGroup2_110.Index)
                                                                                                                                                   .Select(y => y.ProductvariantC_400).ToArray());
                ProductMainGroup2_110.ProductVariantD_500s = new ProductVariantD_500().Register(recordSets, BsNumbers.Where(x => x.Productmaingroup2_110 == ProductMainGroup2_110.Index)
                                                                                                                                                   .Select(y => y.ProductvariantD_500).ToArray());
                ProductMainGroup2_110.FunctionsDeclaration_600s = new FunctionsDeclaration_600().Register(recordSets);

                ProductMainGroup2_110.ProductElementData_700s = new ProductElementData_700().Register(recordSets);
                ProductMainGroup2_110.AccessoryGroup_830s = new AccessoryGroup_830().Register(recordSets);
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
        public List<CrossReferenceAccessory_160_01> CrossReferenceAccessory_160_01s;

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
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "160.01")
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
        public List<VariantTypeA1_250> VariantTypeA1_250s;
        public List<VariantTypeA2_260> VariantTypeA2_260s;
        public List<CrossReferenceAccessory_200_01> CrossReferenceAccessory_200_01s;

        public ProductVariantA_200() { }

        public List<ProductVariantA_200> Register(List<RecordSet> recordSets, List<BuildingSystemNumber_800> BsNumbers,string[] indexFilter)
        {
            List<ProductVariantA_200> ProductVariantA_200s = new List<ProductVariantA_200>();
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

                ProductVariantA_200.VariantTypeA1_250s = new VariantTypeA1_250().Register(recordSets, BsNumbers, BsNumbers.Where(x => x.Productmaingroup2_110 == ProductVariantA_200.Index)
                                                                                                                                                   .Select(y => y.VarianttypeA1_250).ToArray());
                ProductVariantA_200.VariantTypeA2_260s = new VariantTypeA2_260().Register(recordSets, BsNumbers, BsNumbers.Where(x => x.Productmaingroup2_110 == ProductVariantA_200.Index)
                                                                                                                                   .Select(y => y.VarianttypeA2_260).ToArray());

                ProductVariantA_200.CrossReferenceAccessory_200_01s = new CrossReferenceAccessory_200_01().Register(recordSets, BsNumbers, BsNumbers.Where(x => x.Productmaingroup2_110 == ProductVariantA_200.Index)
                                                                                                                   .Select(y => y.VarianttypeA1_250).ToArray());


                ProductVariantA_200s.Add(ProductVariantA_200);
            }

            return ProductVariantA_200s;
        }
    }

    public class CrossReferenceAccessory_200_01
    {
        public string RecordType;
        public string Index;
        public string AccessoryIndex;
        public string Quantity;
        public string FixedOrOptionalAllocation; // f= fixed o=optional
        public string AllocationToAccessory; //P=per project; A=per article

        public CrossReferenceAccessory_200_01() { }

        public List<CrossReferenceAccessory_200_01> Register(List<RecordSet> recordSets, List<BuildingSystemNumber_800> BsNumbers, string[] indexFilter)
        {
            List<CrossReferenceAccessory_200_01> CrossReferenceAccessory_200_01s = new List<CrossReferenceAccessory_200_01>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "200.01")
                                                                    .Where(x => indexFilter.Contains(x.Index))
                                                                    .ToList())
            {
                CrossReferenceAccessory_200_01 CrossReferenceAccessory_200_01 = new CrossReferenceAccessory_200_01();
                while (dataSet.Fields.Count <= 6) dataSet.Fields.Add(string.Empty);
                CrossReferenceAccessory_200_01.RecordType = dataSet.Fields[1];
                CrossReferenceAccessory_200_01.Index = ("000" + dataSet.Fields[2]).Right(3);
                CrossReferenceAccessory_200_01.AccessoryIndex = dataSet.Fields[3];
                CrossReferenceAccessory_200_01.Quantity = dataSet.Fields[4];
                CrossReferenceAccessory_200_01.FixedOrOptionalAllocation = dataSet.Fields[5];
                CrossReferenceAccessory_200_01.AllocationToAccessory = dataSet.Fields[5];
                CrossReferenceAccessory_200_01s.Add(CrossReferenceAccessory_200_01);
            }

            return CrossReferenceAccessory_200_01s;
        }
    }

    public class VariantTypeA1_250
    {
        public string RecordType;
        public string Index;
        public string AccessoryDesignation;
        public string MediaDetails;
        public string AccessorySelection1;
        public List<CrossReferenceAccessory_250_01> CrossReferenceAccessory_250_01s;

        public VariantTypeA1_250() { }

        public List<VariantTypeA1_250> Register(List<RecordSet> recordSets, List<BuildingSystemNumber_800> BsNumbers, string[] indexFilter)
        {
            List<VariantTypeA1_250> VariantTypeA1_250s = new List<VariantTypeA1_250>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "250")
                                                                    .Where(x => indexFilter.Contains(x.Index))
                                                                    .ToList())
            {
                VariantTypeA1_250 VariantTypeA1_250 = new VariantTypeA1_250();
                while (dataSet.Fields.Count <= 5) dataSet.Fields.Add(string.Empty);
                VariantTypeA1_250.RecordType = dataSet.Fields[1];
                VariantTypeA1_250.Index = ("000" + dataSet.Fields[2]).Right(3);
                VariantTypeA1_250.AccessoryDesignation = dataSet.Fields[3];
                VariantTypeA1_250.MediaDetails = dataSet.Fields[4];
                VariantTypeA1_250.AccessorySelection1 = dataSet.Fields[5];

                VariantTypeA1_250.CrossReferenceAccessory_250_01s = new CrossReferenceAccessory_250_01().Register(recordSets, BsNumbers, BsNumbers.Where(x => x.Productmaingroup2_110 == VariantTypeA1_250.Index)
                                                                                                                                                   .Select(y => y.VarianttypeA1_250).ToArray());

                VariantTypeA1_250s.Add(VariantTypeA1_250);
            }

            return VariantTypeA1_250s;
        }
    }

    public class CrossReferenceAccessory_250_01
    {
        public string RecordType;
        public string Index;
        public string AccessoryIndex;
        public string Quantity;
        public string FixedOrOptionalAllocation; // f= fixed o=optional
        public string AllocationToAccessory; //P=per project; A=per article

        public CrossReferenceAccessory_250_01() { }

        public List<CrossReferenceAccessory_250_01> Register(List<RecordSet> recordSets, List<BuildingSystemNumber_800> BsNumbers,string[] indexFilter)
        {
            List<CrossReferenceAccessory_250_01> CrossReferenceAccessory_250_01s = new List<CrossReferenceAccessory_250_01>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "250.01")
                                                                    .Where(x => indexFilter.Contains(x.Index))
                                                                    .ToList())
            {
                CrossReferenceAccessory_250_01 CrossReferenceAccessory_250_01 = new CrossReferenceAccessory_250_01();
                while (dataSet.Fields.Count <= 6) dataSet.Fields.Add(string.Empty);
                CrossReferenceAccessory_250_01.RecordType = dataSet.Fields[1];
                CrossReferenceAccessory_250_01.Index = ("000" + dataSet.Fields[2]).Right(3);
                CrossReferenceAccessory_250_01.AccessoryIndex = dataSet.Fields[3];
                CrossReferenceAccessory_250_01.Quantity = dataSet.Fields[4];
                CrossReferenceAccessory_250_01.FixedOrOptionalAllocation = dataSet.Fields[5];
                CrossReferenceAccessory_250_01.AllocationToAccessory = dataSet.Fields[5];
                CrossReferenceAccessory_250_01s.Add(CrossReferenceAccessory_250_01);
            }

            return CrossReferenceAccessory_250_01s;
        }
    }

    public class VariantTypeA2_260
    {
        public string RecordType;
        public string Index;
        public string AccessoryIndex900;
        public string Quantity;
        public string FixedOrOptionalAllocation; // f= fixed o=optional
        public string AllocationToAccessory; //P=per project; A=per article
        public List<CrossReferenceAccessory_260_01> CrossReferenceAccessory_260_01s;

        public VariantTypeA2_260() { }

        public List<VariantTypeA2_260> Register(List<RecordSet> recordSets, List<BuildingSystemNumber_800> BsNumbers,string[] indexFilter)
        {
            List<VariantTypeA2_260> VariantTypeA2_260s = new List<VariantTypeA2_260>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "260")
                                                                    .Where(x => indexFilter.Contains(x.Index))
                                                                    .ToList())
            {
                VariantTypeA2_260 VariantTypeA2_260 = new VariantTypeA2_260();
                while (dataSet.Fields.Count <= 6) dataSet.Fields.Add(string.Empty);
                VariantTypeA2_260.RecordType = dataSet.Fields[1];
                VariantTypeA2_260.Index = ("000" + dataSet.Fields[2]).Right(3);
                VariantTypeA2_260.AccessoryIndex900 = dataSet.Fields[3];
                VariantTypeA2_260.Quantity = dataSet.Fields[4];
                VariantTypeA2_260.FixedOrOptionalAllocation = dataSet.Fields[5];
                VariantTypeA2_260.AllocationToAccessory = dataSet.Fields[5];

                VariantTypeA2_260.CrossReferenceAccessory_260_01s = new CrossReferenceAccessory_260_01().Register(recordSets, BsNumbers, BsNumbers.Where(x => x.Productmaingroup2_110 == VariantTypeA2_260.Index)
                                                                                                                                   .Select(y => y.VarianttypeA2_260).ToArray());


                VariantTypeA2_260s.Add(VariantTypeA2_260);
            }

            return VariantTypeA2_260s;
        }
    }

    public class CrossReferenceAccessory_260_01
    {
        public string RecordType;
        public string Index;
        public string AccessoryIndex900;
        public string Quantity;
        public string FixedOrOptionalAllocation; // f= fixed o=optional
        public string AllocationToAccessory; //P=per project; A=per article

        public CrossReferenceAccessory_260_01() { }

        public List<CrossReferenceAccessory_260_01> Register(List<RecordSet> recordSets, List<BuildingSystemNumber_800> BsNumbers, string[] indexFilter)
        {
            List<CrossReferenceAccessory_260_01> CrossReferenceAccessory_260_01s = new List<CrossReferenceAccessory_260_01>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "260.01")
                                                                    .Where(x => indexFilter.Contains(x.Index))
                                                                    .ToList())
            {
                CrossReferenceAccessory_260_01 CrossReferenceAccessory_260_01 = new CrossReferenceAccessory_260_01();
                while (dataSet.Fields.Count <= 6) dataSet.Fields.Add(string.Empty);
                CrossReferenceAccessory_260_01.RecordType = dataSet.Fields[1];
                CrossReferenceAccessory_260_01.Index = ("000" + dataSet.Fields[2]).Right(3);
                CrossReferenceAccessory_260_01.AccessoryIndex900 = dataSet.Fields[3];
                CrossReferenceAccessory_260_01.Quantity = dataSet.Fields[4];
                CrossReferenceAccessory_260_01.FixedOrOptionalAllocation = dataSet.Fields[5];
                CrossReferenceAccessory_260_01.AllocationToAccessory = dataSet.Fields[5];
                CrossReferenceAccessory_260_01s.Add(CrossReferenceAccessory_260_01);
            }

            return CrossReferenceAccessory_260_01s;
        }
    }

    public class ProductVariantB_300
    {
        public string RecordType;
        public string Index;
        public string AccessoryDesignation;
        public string MediaDetails;
        public string AccessorySelection1;
        public List<VariantTypeB1_350> VariantTypeB1_350s;
        public List<VariantTypeB2_360> VariantTypeB2_360s;
        public List<CrossReferenceAccessory_300_01> CrossReferenceAccessory_300_01s;

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
        public string RecordType;
        public string Index;
        public string AccessoryIndex;
        public string Quantity;
        public string FixedOrOptionalAllocation; // f= fixed o=optional
        public string AllocationToAccessory; //P=per project; A=per article

        public CrossReferenceAccessory_300_01() { }

        public List<CrossReferenceAccessory_300_01> Register(List<RecordSet> recordSets, List<BuildingSystemNumber_800> BsNumbers, string[] indexFilter)
        {
            List<CrossReferenceAccessory_300_01> CrossReferenceAccessory_300_01s = new List<CrossReferenceAccessory_300_01>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "300.01")
                                                                    .Where(x => indexFilter.Contains(x.Index))
                                                                    .ToList())
            {
                CrossReferenceAccessory_300_01 CrossReferenceAccessory_300_01 = new CrossReferenceAccessory_300_01();
                while (dataSet.Fields.Count <= 6) dataSet.Fields.Add(string.Empty);
                CrossReferenceAccessory_300_01.RecordType = dataSet.Fields[1];
                CrossReferenceAccessory_300_01.Index = ("000" + dataSet.Fields[2]).Right(3);
                CrossReferenceAccessory_300_01.AccessoryIndex = dataSet.Fields[3];
                CrossReferenceAccessory_300_01.Quantity = dataSet.Fields[4];
                CrossReferenceAccessory_300_01.FixedOrOptionalAllocation = dataSet.Fields[5];
                CrossReferenceAccessory_300_01.AllocationToAccessory = dataSet.Fields[5];
                CrossReferenceAccessory_300_01s.Add(CrossReferenceAccessory_300_01);
            }

            return CrossReferenceAccessory_300_01s;
        }
    }

    public class VariantTypeB1_350
    {
        public string RecordType;
        public string Index;
        public string AccessoryDesignation;
        public string MediaDetails;
        public string AccessorySelection1;
        public List<CrossReferenceAccessory_350_01> CrossReferenceAccessory_350_01s;

        public VariantTypeB1_350() { }

        public List<VariantTypeB1_350> Register(List<RecordSet> recordSets, List<BuildingSystemNumber_800> BsNumbers, string[] indexFilter)
        {
            List<VariantTypeB1_350> VariantTypeB1_350s = new List<VariantTypeB1_350>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "350")
                                                                    .Where(x => indexFilter.Contains(x.Index))
                                                                    .ToList())
            {
                VariantTypeB1_350 VariantTypeB1_350 = new VariantTypeB1_350();
                while (dataSet.Fields.Count <= 5) dataSet.Fields.Add(string.Empty);
                VariantTypeB1_350.RecordType = dataSet.Fields[1];
                VariantTypeB1_350.Index = ("000" + dataSet.Fields[2]).Right(3);
                VariantTypeB1_350.AccessoryDesignation = dataSet.Fields[3];
                VariantTypeB1_350.MediaDetails = dataSet.Fields[4];
                VariantTypeB1_350.AccessorySelection1 = dataSet.Fields[5];

                VariantTypeB1_350.CrossReferenceAccessory_350_01s = new CrossReferenceAccessory_350_01().Register(recordSets, BsNumbers, BsNumbers.Where(x => x.Productmaingroup2_110 == VariantTypeB1_350.Index)
                                                                                                                                                   .Select(y => y.VarianttypeA1_250).ToArray());

                VariantTypeB1_350s.Add(VariantTypeB1_350);
            }

            return VariantTypeB1_350s;
        }
    }

    public class CrossReferenceAccessory_350_01
    {
        public string RecordType;
        public string Index;
        public string AccessoryIndex;
        public string Quantity;
        public string FixedOrOptionalAllocation; // f= fixed o=optional
        public string AllocationToAccessory; //P=per project; A=per article

        public CrossReferenceAccessory_350_01() { }

        public List<CrossReferenceAccessory_350_01> Register(List<RecordSet> recordSets, List<BuildingSystemNumber_800> BsNumbers, string[] indexFilter)
        {
            List<CrossReferenceAccessory_350_01> CrossReferenceAccessory_350_01s = new List<CrossReferenceAccessory_350_01>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "350.01")
                                                                    .Where(x => indexFilter.Contains(x.Index))
                                                                    .ToList())
            {
                CrossReferenceAccessory_350_01 CrossReferenceAccessory_350_01 = new CrossReferenceAccessory_350_01();
                while (dataSet.Fields.Count <= 6) dataSet.Fields.Add(string.Empty);
                CrossReferenceAccessory_350_01.RecordType = dataSet.Fields[1];
                CrossReferenceAccessory_350_01.Index = ("000" + dataSet.Fields[2]).Right(3);
                CrossReferenceAccessory_350_01.AccessoryIndex = dataSet.Fields[3];
                CrossReferenceAccessory_350_01.Quantity = dataSet.Fields[4];
                CrossReferenceAccessory_350_01.FixedOrOptionalAllocation = dataSet.Fields[5];
                CrossReferenceAccessory_350_01.AllocationToAccessory = dataSet.Fields[5];
                CrossReferenceAccessory_350_01s.Add(CrossReferenceAccessory_350_01);
            }

            return CrossReferenceAccessory_350_01s;
        }
    }

    public class VariantTypeB2_360
    {
        public string RecordType;
        public string Index;
        public string AccessoryIndex900;
        public string Quantity;
        public string FixedOrOptionalAllocation; // f= fixed o=optional
        public string AllocationToAccessory; //P=per project; A=per article
        public List<CrossReferenceAccessory_360_01> CrossReferenceAccessory_360_01s;

        public VariantTypeB2_360() { }

        public List<VariantTypeB2_360> Register(List<RecordSet> recordSets, List<BuildingSystemNumber_800> BsNumbers, string[] indexFilter)
        {
            List<VariantTypeB2_360> VariantTypeB2_360s = new List<VariantTypeB2_360>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "360")
                                                                    .Where(x => indexFilter.Contains(x.Index))
                                                                    .ToList())
            {
                VariantTypeB2_360 VariantTypeB2_360 = new VariantTypeB2_360();
                while (dataSet.Fields.Count <= 6) dataSet.Fields.Add(string.Empty);
                VariantTypeB2_360.RecordType = dataSet.Fields[1];
                VariantTypeB2_360.Index = ("000" + dataSet.Fields[2]).Right(3);
                VariantTypeB2_360.AccessoryIndex900 = dataSet.Fields[3];
                VariantTypeB2_360.Quantity = dataSet.Fields[4];
                VariantTypeB2_360.FixedOrOptionalAllocation = dataSet.Fields[5];
                VariantTypeB2_360.AllocationToAccessory = dataSet.Fields[5];

                VariantTypeB2_360.CrossReferenceAccessory_360_01s = new CrossReferenceAccessory_360_01().Register(recordSets, BsNumbers, BsNumbers.Where(x => x.Productmaingroup2_110 == VariantTypeB2_360.Index)
                                                                                                                                   .Select(y => y.VarianttypeA2_260).ToArray());


                VariantTypeB2_360s.Add(VariantTypeB2_360);
            }

            return VariantTypeB2_360s;
        }
    }

    public class CrossReferenceAccessory_360_01
    {
        public string RecordType;
        public string Index;
        public string AccessoryIndex;
        public string Quantity;
        public string FixedOrOptionalAllocation; // f= fixed o=optional
        public string AllocationToAccessory; //P=per project; A=per article

        public CrossReferenceAccessory_360_01() { }

        public List<CrossReferenceAccessory_360_01> Register(List<RecordSet> recordSets, List<BuildingSystemNumber_800> BsNumbers, string[] indexFilter)
        {
            List<CrossReferenceAccessory_360_01> CrossReferenceAccessory_360_01s = new List<CrossReferenceAccessory_360_01>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "360.01")
                                                                    .Where(x => indexFilter.Contains(x.Index))
                                                                    .ToList())
            {
                CrossReferenceAccessory_360_01 CrossReferenceAccessory_360_01 = new CrossReferenceAccessory_360_01();
                while (dataSet.Fields.Count <= 6) dataSet.Fields.Add(string.Empty);
                CrossReferenceAccessory_360_01.RecordType = dataSet.Fields[1];
                CrossReferenceAccessory_360_01.Index = ("000" + dataSet.Fields[2]).Right(3);
                CrossReferenceAccessory_360_01.AccessoryIndex = dataSet.Fields[3];
                CrossReferenceAccessory_360_01.Quantity = dataSet.Fields[4];
                CrossReferenceAccessory_360_01.FixedOrOptionalAllocation = dataSet.Fields[5];
                CrossReferenceAccessory_360_01.AllocationToAccessory = dataSet.Fields[5];
                CrossReferenceAccessory_360_01s.Add(CrossReferenceAccessory_360_01);
            }

            return CrossReferenceAccessory_360_01s;
        }
    }

    public class ProductVariantC_400
    {
        public string RecordType;
        public string Index;
        public string AccessoryDesignation;
        public string MediaDetails;
        public string AccessorySelection1;
        public List<VariantTypeC1_450> VariantTypeC1_450s;
        public List<VariantTypeC2_460> VariantTypeC2_460s;
        public List<CrossReferenceAccessory_400_01> CrossReferenceAccessory_400_01s;

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
        public string RecordType;
        public string Index;
        public string AccessoryIndex;
        public string Quantity;
        public string FixedOrOptionalAllocation; // f= fixed o=optional
        public string AllocationToAccessory; //P=per project; A=per article

        public CrossReferenceAccessory_400_01() { }

        public List<CrossReferenceAccessory_400_01> Register(List<RecordSet> recordSets, List<BuildingSystemNumber_800> BsNumbers, string[] indexFilter)
        {
            List<CrossReferenceAccessory_400_01> CrossReferenceAccessory_400_01s = new List<CrossReferenceAccessory_400_01>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "400.01")
                                                                    .Where(x => indexFilter.Contains(x.Index))
                                                                    .ToList())
            {
                CrossReferenceAccessory_400_01 CrossReferenceAccessory_400_01 = new CrossReferenceAccessory_400_01();
                while (dataSet.Fields.Count <= 6) dataSet.Fields.Add(string.Empty);
                CrossReferenceAccessory_400_01.RecordType = dataSet.Fields[1];
                CrossReferenceAccessory_400_01.Index = ("000" + dataSet.Fields[2]).Right(3);
                CrossReferenceAccessory_400_01.AccessoryIndex = dataSet.Fields[3];
                CrossReferenceAccessory_400_01.Quantity = dataSet.Fields[4];
                CrossReferenceAccessory_400_01.FixedOrOptionalAllocation = dataSet.Fields[5];
                CrossReferenceAccessory_400_01.AllocationToAccessory = dataSet.Fields[5];
                CrossReferenceAccessory_400_01s.Add(CrossReferenceAccessory_400_01);
            }

            return CrossReferenceAccessory_400_01s;
        }
    }

    public class VariantTypeC1_450
    {
        public string RecordType;
        public string Index;
        public string AccessoryDesignation;
        public string MediaDetails;
        public string AccessorySelection1;
        public List<CrossReferenceAccessory_450_01> CrossReferenceAccessory_450_01s;

        public VariantTypeC1_450() { }

        public List<VariantTypeC1_450> Register(List<RecordSet> recordSets, List<BuildingSystemNumber_800> BsNumbers, string[] indexFilter)
        {
            List<VariantTypeC1_450> VariantTypeC1_450s = new List<VariantTypeC1_450>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "450")
                                                                    .Where(x => indexFilter.Contains(x.Index))
                                                                    .ToList())
            {
                VariantTypeC1_450 VariantTypeC1_450 = new VariantTypeC1_450();
                while (dataSet.Fields.Count <= 5) dataSet.Fields.Add(string.Empty);
                VariantTypeC1_450.RecordType = dataSet.Fields[1];
                VariantTypeC1_450.Index = ("000" + dataSet.Fields[2]).Right(3);
                VariantTypeC1_450.AccessoryDesignation = dataSet.Fields[3];
                VariantTypeC1_450.MediaDetails = dataSet.Fields[4];
                VariantTypeC1_450.AccessorySelection1 = dataSet.Fields[5];

                VariantTypeC1_450.CrossReferenceAccessory_450_01s = new CrossReferenceAccessory_450_01().Register(recordSets, BsNumbers, BsNumbers.Where(x => x.Productmaingroup2_110 == VariantTypeC1_450.Index)
                                                                                                                                                   .Select(y => y.VarianttypeC1_450).ToArray());

                VariantTypeC1_450s.Add(VariantTypeC1_450);
            }

            return VariantTypeC1_450s;
        }
    }

    public class CrossReferenceAccessory_450_01
    {
        public string RecordType;
        public string Index;
        public string AccessoryIndex;
        public string Quantity;
        public string FixedOrOptionalAllocation; // f= fixed o=optional
        public string AllocationToAccessory; //P=per project; A=per article

        public CrossReferenceAccessory_450_01() { }

        public List<CrossReferenceAccessory_450_01> Register(List<RecordSet> recordSets, List<BuildingSystemNumber_800> BsNumbers, string[] indexFilter)
        {
            List<CrossReferenceAccessory_450_01> CrossReferenceAccessory_450_01s = new List<CrossReferenceAccessory_450_01>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "450.01")
                                                                    .Where(x => indexFilter.Contains(x.Index))
                                                                    .ToList())
            {
                CrossReferenceAccessory_450_01 CrossReferenceAccessory_450_01 = new CrossReferenceAccessory_450_01();
                while (dataSet.Fields.Count <= 6) dataSet.Fields.Add(string.Empty);
                CrossReferenceAccessory_450_01.RecordType = dataSet.Fields[1];
                CrossReferenceAccessory_450_01.Index = ("000" + dataSet.Fields[2]).Right(3);
                CrossReferenceAccessory_450_01.AccessoryIndex = dataSet.Fields[3];
                CrossReferenceAccessory_450_01.Quantity = dataSet.Fields[4];
                CrossReferenceAccessory_450_01.FixedOrOptionalAllocation = dataSet.Fields[5];
                CrossReferenceAccessory_450_01.AllocationToAccessory = dataSet.Fields[5];
                CrossReferenceAccessory_450_01s.Add(CrossReferenceAccessory_450_01);
            }

            return CrossReferenceAccessory_450_01s;
        }
    }

    public class VariantTypeC2_460
    {
        public string RecordType;
        public string Index;
        public string AccessoryIndex900;
        public string Quantity;
        public string FixedOrOptionalAllocation; // f= fixed o=optional
        public string AllocationToAccessory; //P=per project; A=per article
        public List<CrossReferenceAccessory_460_01> CrossReferenceAccessory_460_01s;

        public VariantTypeC2_460() { }

        public List<VariantTypeC2_460> Register(List<RecordSet> recordSets, List<BuildingSystemNumber_800> BsNumbers, string[] indexFilter)
        {
            List<VariantTypeC2_460> VariantTypeC2_460s = new List<VariantTypeC2_460>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "460")
                                                                    .Where(x => indexFilter.Contains(x.Index))
                                                                    .ToList())
            {
                VariantTypeC2_460 VariantTypeC2_460 = new VariantTypeC2_460();
                while (dataSet.Fields.Count <= 6) dataSet.Fields.Add(string.Empty);
                VariantTypeC2_460.RecordType = dataSet.Fields[1];
                VariantTypeC2_460.Index = ("000" + dataSet.Fields[2]).Right(3);
                VariantTypeC2_460.AccessoryIndex900 = dataSet.Fields[3];
                VariantTypeC2_460.Quantity = dataSet.Fields[4];
                VariantTypeC2_460.FixedOrOptionalAllocation = dataSet.Fields[5];
                VariantTypeC2_460.AllocationToAccessory = dataSet.Fields[5];

                VariantTypeC2_460.CrossReferenceAccessory_460_01s = new CrossReferenceAccessory_460_01().Register(recordSets, BsNumbers, BsNumbers.Where(x => x.Productmaingroup2_110 == VariantTypeC2_460.Index)
                                                                                                                                   .Select(y => y.VarianttypeA2_260).ToArray());


                VariantTypeC2_460s.Add(VariantTypeC2_460);
            }

            return VariantTypeC2_460s;
        }
    }

    public class CrossReferenceAccessory_460_01
    {
        public string RecordType;
        public string Index;
        public string AccessoryIndex;
        public string Quantity;
        public string FixedOrOptionalAllocation; // f= fixed o=optional
        public string AllocationToAccessory; //P=per project; A=per article

        public CrossReferenceAccessory_460_01() { }

        public List<CrossReferenceAccessory_460_01> Register(List<RecordSet> recordSets, List<BuildingSystemNumber_800> BsNumbers, string[] indexFilter)
        {
            List<CrossReferenceAccessory_460_01> CrossReferenceAccessory_460_01s = new List<CrossReferenceAccessory_460_01>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "460.01")
                                                                    .Where(x => indexFilter.Contains(x.Index))
                                                                    .ToList())
            {
                CrossReferenceAccessory_460_01 CrossReferenceAccessory_460_01 = new CrossReferenceAccessory_460_01();
                while (dataSet.Fields.Count <= 6) dataSet.Fields.Add(string.Empty);
                CrossReferenceAccessory_460_01.RecordType = dataSet.Fields[1];
                CrossReferenceAccessory_460_01.Index = ("000" + dataSet.Fields[2]).Right(3);
                CrossReferenceAccessory_460_01.AccessoryIndex = dataSet.Fields[3];
                CrossReferenceAccessory_460_01.Quantity = dataSet.Fields[4];
                CrossReferenceAccessory_460_01.FixedOrOptionalAllocation = dataSet.Fields[5];
                CrossReferenceAccessory_460_01.AllocationToAccessory = dataSet.Fields[5];
                CrossReferenceAccessory_460_01s.Add(CrossReferenceAccessory_460_01);
            }

            return CrossReferenceAccessory_460_01s;
        }
    }

    public class ProductVariantD_500
    {
        public string RecordType;
        public string Index;
        public string AccessoryDesignation;
        public string MediaDetails;
        public string AccessorySelection1;
        public List<VariantTypeD1_550> VariantTypeD1_550s;
        public List<VariantTypeD2_560> VariantTypeD2_560s;
        public List<CrossReferenceAccessory_500_01> CrossReferenceAccessory_500_01s;

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
        public string RecordType;
        public string Index;
        public string AccessoryIndex;
        public string Quantity;
        public string FixedOrOptionalAllocation; // f= fixed o=optional
        public string AllocationToAccessory; //P=per project; A=per article

        public CrossReferenceAccessory_500_01() { }

        public List<CrossReferenceAccessory_500_01> Register(List<RecordSet> recordSets, List<BuildingSystemNumber_800> BsNumbers, string[] indexFilter)
        {
            List<CrossReferenceAccessory_500_01> CrossReferenceAccessory_500_01s = new List<CrossReferenceAccessory_500_01>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "500.01")
                                                                    .Where(x => indexFilter.Contains(x.Index))
                                                                    .ToList())
            {
                CrossReferenceAccessory_500_01 CrossReferenceAccessory_500_01 = new CrossReferenceAccessory_500_01();
                while (dataSet.Fields.Count <= 6) dataSet.Fields.Add(string.Empty);
                CrossReferenceAccessory_500_01.RecordType = dataSet.Fields[1];
                CrossReferenceAccessory_500_01.Index = ("000" + dataSet.Fields[2]).Right(3);
                CrossReferenceAccessory_500_01.AccessoryIndex = dataSet.Fields[3];
                CrossReferenceAccessory_500_01.Quantity = dataSet.Fields[4];
                CrossReferenceAccessory_500_01.FixedOrOptionalAllocation = dataSet.Fields[5];
                CrossReferenceAccessory_500_01.AllocationToAccessory = dataSet.Fields[5];
                CrossReferenceAccessory_500_01s.Add(CrossReferenceAccessory_500_01);
            }

            return CrossReferenceAccessory_500_01s;
        }
    }

    public class VariantTypeD1_550
    {
        public string RecordType;
        public string Index;
        public string AccessoryDesignation;
        public string MediaDetails;
        public string AccessorySelection1;
        public List<CrossReferenceAccessory_550_01> CrossReferenceAccessory_550_01s;

        public VariantTypeD1_550() { }

        public List<VariantTypeD1_550> Register(List<RecordSet> recordSets, List<BuildingSystemNumber_800> BsNumbers, string[] indexFilter)
        {
            List<VariantTypeD1_550> VariantTypeD1_550s = new List<VariantTypeD1_550>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "550")
                                                                    .Where(x => indexFilter.Contains(x.Index))
                                                                    .ToList())
            {
                VariantTypeD1_550 VariantTypeD1_550 = new VariantTypeD1_550();
                while (dataSet.Fields.Count <= 5) dataSet.Fields.Add(string.Empty);
                VariantTypeD1_550.RecordType = dataSet.Fields[1];
                VariantTypeD1_550.Index = ("000" + dataSet.Fields[2]).Right(3);
                VariantTypeD1_550.AccessoryDesignation = dataSet.Fields[3];
                VariantTypeD1_550.MediaDetails = dataSet.Fields[4];
                VariantTypeD1_550.AccessorySelection1 = dataSet.Fields[5];

                VariantTypeD1_550.CrossReferenceAccessory_550_01s = new CrossReferenceAccessory_550_01().Register(recordSets, BsNumbers, BsNumbers.Where(x => x.Productmaingroup2_110 == VariantTypeD1_550.Index)
                                                                                                                                                   .Select(y => y.VarianttypeA1_250).ToArray());

                VariantTypeD1_550s.Add(VariantTypeD1_550);
            }

            return VariantTypeD1_550s;
        }
    }

    public class CrossReferenceAccessory_550_01
    {
        public string RecordType;
        public string Index;
        public string AccessoryIndex;
        public string Quantity;
        public string FixedOrOptionalAllocation; // f= fixed o=optional
        public string AllocationToAccessory; //P=per project; A=per article

        public CrossReferenceAccessory_550_01() { }

        public List<CrossReferenceAccessory_550_01> Register(List<RecordSet> recordSets, List<BuildingSystemNumber_800> BsNumbers, string[] indexFilter)
        {
            List<CrossReferenceAccessory_550_01> CrossReferenceAccessory_550_01s = new List<CrossReferenceAccessory_550_01>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "550.01")
                                                                    .Where(x => indexFilter.Contains(x.Index))
                                                                    .ToList())
            {
                CrossReferenceAccessory_550_01 CrossReferenceAccessory_550_01 = new CrossReferenceAccessory_550_01();
                while (dataSet.Fields.Count <= 6) dataSet.Fields.Add(string.Empty);
                CrossReferenceAccessory_550_01.RecordType = dataSet.Fields[1];
                CrossReferenceAccessory_550_01.Index = ("000" + dataSet.Fields[2]).Right(3);
                CrossReferenceAccessory_550_01.AccessoryIndex = dataSet.Fields[3];
                CrossReferenceAccessory_550_01.Quantity = dataSet.Fields[4];
                CrossReferenceAccessory_550_01.FixedOrOptionalAllocation = dataSet.Fields[5];
                CrossReferenceAccessory_550_01.AllocationToAccessory = dataSet.Fields[5];
                CrossReferenceAccessory_550_01s.Add(CrossReferenceAccessory_550_01);
            }

            return CrossReferenceAccessory_550_01s;
        }
    }

    public class VariantTypeD2_560
    {
        public string RecordType;
        public string Index;
        public string AccessoryIndex900;
        public string Quantity;
        public string FixedOrOptionalAllocation; // f= fixed o=optional
        public string AllocationToAccessory; //P=per project; A=per article
        public List<CrossReferenceAccessory_560_01> CrossReferenceAccessory_560_01s;

        public VariantTypeD2_560() { }

        public List<VariantTypeD2_560> Register(List<RecordSet> recordSets, List<BuildingSystemNumber_800> BsNumbers, string[] indexFilter)
        {
            List<VariantTypeD2_560> VariantTypeD2_560s = new List<VariantTypeD2_560>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "560")
                                                                    .Where(x => indexFilter.Contains(x.Index))
                                                                    .ToList())
            {
                VariantTypeD2_560 VariantTypeD2_560 = new VariantTypeD2_560();
                while (dataSet.Fields.Count <= 6) dataSet.Fields.Add(string.Empty);
                VariantTypeD2_560.RecordType = dataSet.Fields[1];
                VariantTypeD2_560.Index = ("000" + dataSet.Fields[2]).Right(3);
                VariantTypeD2_560.AccessoryIndex900 = dataSet.Fields[3];
                VariantTypeD2_560.Quantity = dataSet.Fields[4];
                VariantTypeD2_560.FixedOrOptionalAllocation = dataSet.Fields[5];
                VariantTypeD2_560.AllocationToAccessory = dataSet.Fields[5];

                VariantTypeD2_560.CrossReferenceAccessory_560_01s = new CrossReferenceAccessory_560_01().Register(recordSets, BsNumbers, BsNumbers.Where(x => x.Productmaingroup2_110 == VariantTypeD2_560.Index)
                                                                                                                                   .Select(y => y.VarianttypeA2_260).ToArray());


                VariantTypeD2_560s.Add(VariantTypeD2_560);
            }

            return VariantTypeD2_560s;
        }
    }

    public class CrossReferenceAccessory_560_01
    { 
        public string RecordType;
        public string Index;
        public string AccessoryIndex;
        public string Quantity;
        public string FixedOrOptionalAllocation; // f= fixed o=optional
        public string AllocationToAccessory; //P=per project; A=per article

        public CrossReferenceAccessory_560_01() { }

        public List<CrossReferenceAccessory_560_01> Register(List<RecordSet> recordSets, List<BuildingSystemNumber_800> BsNumbers, string[] indexFilter)
        {
            List<CrossReferenceAccessory_560_01> CrossReferenceAccessory_560_01s = new List<CrossReferenceAccessory_560_01>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "560.01")
                                                                    .Where(x => indexFilter.Contains(x.Index))
                                                                    .ToList())
            {
                CrossReferenceAccessory_560_01 CrossReferenceAccessory_560_01 = new CrossReferenceAccessory_560_01();
                while (dataSet.Fields.Count <= 6) dataSet.Fields.Add(string.Empty);
                CrossReferenceAccessory_560_01.RecordType = dataSet.Fields[1];
                CrossReferenceAccessory_560_01.Index = ("000" + dataSet.Fields[2]).Right(3);
                CrossReferenceAccessory_560_01.AccessoryIndex = dataSet.Fields[3];
                CrossReferenceAccessory_560_01.Quantity = dataSet.Fields[4];
                CrossReferenceAccessory_560_01.FixedOrOptionalAllocation = dataSet.Fields[5];
                CrossReferenceAccessory_560_01.AllocationToAccessory = dataSet.Fields[5];
                CrossReferenceAccessory_560_01s.Add(CrossReferenceAccessory_560_01);
            }

            return CrossReferenceAccessory_560_01s;
        }
    }

    public class FunctionsDeclaration_600

    {
        public string RecordType;
        public string Index;
        public string DeclarationOfFunctionsAndParameters;
        public string Comment;
        public List<FunctionsDefinition_610> FunctionsDefinition_610s;

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
        public string RecordType;
        public string Index;
        public string DeclarationOfFunctionsAndParameters;
        public string Comment;

        public FunctionsDefinition_610() { }

        public List<FunctionsDefinition_610> Register(List<RecordSet> recordSets)
        {
            List<FunctionsDefinition_610> FunctionsDefinition_610s = new List<FunctionsDefinition_610>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "610")
                                                                 .ToList())
            {
                FunctionsDefinition_610 FunctionsDefinition_610 = new FunctionsDefinition_610();
                while (dataSet.Fields.Count <= 4) dataSet.Fields.Add(string.Empty);
                FunctionsDefinition_610.RecordType = dataSet.Fields[1];
                FunctionsDefinition_610.Index = ("000" + dataSet.Fields[2]).Right(3);
                FunctionsDefinition_610.DeclarationOfFunctionsAndParameters = dataSet.Fields[3];
                FunctionsDefinition_610.Comment = dataSet.Fields[4];
                FunctionsDefinition_610s.Add(FunctionsDefinition_610);
            }

            return FunctionsDefinition_610s;
        }
    }

    public class ProductElementData_700
    {
        public string RecordType;
        public int Index;
        public List<ProductSpecificPart_710> ProductSpecificPart_710s;
        public List<AccessoryProductElement_760> AccessoryProductElement_760s;
        public List<CrossReferenceGeometry_720> CrossReferenceGeometry_720s;
        public List<FunctionInternalData_730> FunctionInternalData_730s;

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

    public class ProductSpecificPart_710
    {
        public ProductSpecificPart_710() { }

    }

    public class AccessoryProductElement_760
    {
        public string RecordType;
        public string Index;
        public string AccessoryIndex900;
        public string Quantity;
        public string FixedOrOptionalAllocation; // f= fixed o=optional
        public string AllocationToAccessory; //P=per project; A=per article
        public List<CrossReferenceAccessory_760_01> CrossReferenceAccessory_760_01s;

        public AccessoryProductElement_760() { }

        public List<AccessoryProductElement_760> Register(List<RecordSet> recordSets, List<BuildingSystemNumber_800> BsNumbers, string[] indexFilter)
        {
            List<AccessoryProductElement_760> AccessoryProductElement_760s = new List<AccessoryProductElement_760>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "760")
                                                                    .Where(x => indexFilter.Contains(x.Index))
                                                                    .ToList())
            {
                AccessoryProductElement_760 AccessoryProductElement_760 = new AccessoryProductElement_760();
                while (dataSet.Fields.Count <= 6) dataSet.Fields.Add(string.Empty);
                AccessoryProductElement_760.RecordType = dataSet.Fields[1];
                AccessoryProductElement_760.Index = ("000" + dataSet.Fields[2]).Right(3);
                AccessoryProductElement_760.AccessoryIndex900 = dataSet.Fields[3];
                AccessoryProductElement_760.Quantity = dataSet.Fields[4];
                AccessoryProductElement_760.FixedOrOptionalAllocation = dataSet.Fields[5];
                AccessoryProductElement_760.AllocationToAccessory = dataSet.Fields[5];

                AccessoryProductElement_760.CrossReferenceAccessory_760_01s = new CrossReferenceAccessory_760_01().Register(recordSets, BsNumbers, BsNumbers.Where(x => x.Productmaingroup2_110 == AccessoryProductElement_760.Index)
                                                                                                                                   .Select(y => y.VarianttypeA2_260).ToArray());


                AccessoryProductElement_760s.Add(AccessoryProductElement_760);
            }

            return AccessoryProductElement_760s;
        }
    }

    public class CrossReferenceAccessory_760_01
    {
        public string RecordType;
        public string Index;
        public string AccessoryIndex;
        public string Quantity;
        public string FixedOrOptionalAllocation; // f= fixed o=optional
        public string AllocationToAccessory; //P=per project; A=per article

        public CrossReferenceAccessory_760_01() { }

        public List<CrossReferenceAccessory_760_01> Register(List<RecordSet> recordSets, List<BuildingSystemNumber_800> BsNumbers, string[] indexFilter)
        {
            List<CrossReferenceAccessory_760_01> CrossReferenceAccessory_760_01s = new List<CrossReferenceAccessory_760_01>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "760.01")
                                                                    .Where(x => indexFilter.Contains(x.Index))
                                                                    .ToList())
            {
                CrossReferenceAccessory_760_01 CrossReferenceAccessory_760_01 = new CrossReferenceAccessory_760_01();
                while (dataSet.Fields.Count <= 6) dataSet.Fields.Add(string.Empty);
                CrossReferenceAccessory_760_01.RecordType = dataSet.Fields[1];
                CrossReferenceAccessory_760_01.Index = ("000" + dataSet.Fields[2]).Right(3);
                CrossReferenceAccessory_760_01.AccessoryIndex = dataSet.Fields[3];
                CrossReferenceAccessory_760_01.Quantity = dataSet.Fields[4];
                CrossReferenceAccessory_760_01.FixedOrOptionalAllocation = dataSet.Fields[5];
                CrossReferenceAccessory_760_01.AllocationToAccessory = dataSet.Fields[5];
                CrossReferenceAccessory_760_01s.Add(CrossReferenceAccessory_760_01);
            }

            return CrossReferenceAccessory_760_01s;
        }

    }

    public class CrossReferenceGeometry_720
    {
        public string RecordType;
        public string Index;
        public string IndexPointerToRecordType970;
        public string IndexPointerToRecordType160;
        public string IndexPointerToRecordType200;
        public string IndexPointerToRecordType250;
        public string IndexPointerToRecordType260;
        public string IndexPointerToRecordType300;
        public string IndexPointerToRecordType350;
        public string IndexPointerToRecordType360;
        public string IndexPointerToRecordType400;
        public string IndexPointerToRecordType450;
        public string IndexPointerToRecordType460;
        public string IndexPointerToRecordType500;
        public string IndexPointerToRecordType550;
        public string IndexPointerToRecordType560;
        public string IndexPointerToRecordType760;


        public CrossReferenceGeometry_720() { }

        public List<CrossReferenceGeometry_720> Register(List<RecordSet> recordSets, List<BuildingSystemNumber_800> BsNumbers, string[] indexFilter)
        {
            List<CrossReferenceGeometry_720> CrossReferenceGeometry_720s = new List<CrossReferenceGeometry_720>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "720")
                                                                    .Where(x => indexFilter.Contains(x.Index))
                                                                    .ToList())
            {
                CrossReferenceGeometry_720 CrossReferenceGeometry_720 = new CrossReferenceGeometry_720();
                while (dataSet.Fields.Count <= 17) dataSet.Fields.Add(string.Empty);
                CrossReferenceGeometry_720.RecordType = dataSet.Fields[1];
                CrossReferenceGeometry_720.Index = ("000" + dataSet.Fields[2]).Right(3);
                CrossReferenceGeometry_720.IndexPointerToRecordType970 = dataSet.Fields[3];
                CrossReferenceGeometry_720.IndexPointerToRecordType160 = dataSet.Fields[4];
                CrossReferenceGeometry_720.IndexPointerToRecordType200 = dataSet.Fields[5];
                CrossReferenceGeometry_720.IndexPointerToRecordType250 = dataSet.Fields[6];
                CrossReferenceGeometry_720.IndexPointerToRecordType260 = dataSet.Fields[7];
                CrossReferenceGeometry_720.IndexPointerToRecordType300 = dataSet.Fields[8];
                CrossReferenceGeometry_720.IndexPointerToRecordType350 = dataSet.Fields[9];
                CrossReferenceGeometry_720.IndexPointerToRecordType360 = dataSet.Fields[10];
                CrossReferenceGeometry_720.IndexPointerToRecordType400 = dataSet.Fields[11];
                CrossReferenceGeometry_720.IndexPointerToRecordType450 = dataSet.Fields[12];
                CrossReferenceGeometry_720.IndexPointerToRecordType460 = dataSet.Fields[13];
                CrossReferenceGeometry_720.IndexPointerToRecordType500 = dataSet.Fields[14];
                CrossReferenceGeometry_720.IndexPointerToRecordType550 = dataSet.Fields[15];
                CrossReferenceGeometry_720.IndexPointerToRecordType560 = dataSet.Fields[16];
                CrossReferenceGeometry_720.IndexPointerToRecordType760 = dataSet.Fields[17];
                CrossReferenceGeometry_720s.Add(CrossReferenceGeometry_720);
            }

            return CrossReferenceGeometry_720s;
        }


    }

    public class FunctionInternalData_730
    {
        public string RecordType;
        public string Index;
        public int NumberOfAssignedFields;
        public List<string> FieldDefinitions;

        public FunctionInternalData_730() { }

        public List<FunctionInternalData_730> Register(List<RecordSet> recordSets, List<BuildingSystemNumber_800> BsNumbers, string[] indexFilter)
        {
            List<FunctionInternalData_730> FunctionInternalData_730s = new List<FunctionInternalData_730>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "730")
                                                                    .Where(x => indexFilter.Contains(x.Index))
                                                                    .ToList())
            {
                FunctionInternalData_730 FunctionInternalData_730 = new FunctionInternalData_730();
                while (dataSet.Fields.Count <= 6) dataSet.Fields.Add(string.Empty);
                FunctionInternalData_730.RecordType = dataSet.Fields[1];
                FunctionInternalData_730.Index = ("000" + dataSet.Fields[2]).Right(3);
                FunctionInternalData_730.NumberOfAssignedFields = Convert.ToInt32(dataSet.Fields[3]);

                List<string> fieldDefinitions = new List<string>();
                for (int i = 4; i < dataSet.Fields.Count; i += 1)
                {
                    fieldDefinitions.Add(dataSet.Fields[i]);
                }
                FunctionInternalData_730.FieldDefinitions = fieldDefinitions;

                FunctionInternalData_730s.Add(FunctionInternalData_730);
            }

            return FunctionInternalData_730s;
        }

    }

    public class BuildingSystemNumber_800
    {
        public string RecordType;
        public string Index;
        public string Number;
        public string Comment;
        public List<ArticelNumber_810> ArticelNumber_810s;
        public FollowingRecordFunctionName_820 FollowingRecordFunctionName_820;

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

        public BuildingSystemNumber_800() { }

        public List<BuildingSystemNumber_800> Register(List<RecordSet> recordSets)
        {
            List<BuildingSystemNumber_800> BuildingSystemNumber_800s = new List<BuildingSystemNumber_800>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "800").ToList())
            {
                BuildingSystemNumber_800 BuildingSystemNumber_800 = new BuildingSystemNumber_800();
                while (dataSet.Fields.Count <= 6) dataSet.Fields.Add(string.Empty);
                BuildingSystemNumber_800.RecordType = dataSet.Fields[1].Substring(0, 3);
                BuildingSystemNumber_800.Index = ("000" + dataSet.Fields[2]).Right(3);
                BuildingSystemNumber_800.Number = dataSet.Fields[3].Substring(0, 53);
                BuildingSystemNumber_800.Comment = dataSet.Fields[4];

                //The BS number consists of numerals and question marks only and is constructed according to the following pattern
                // AAABBBCCCDDDEEEFFFGGGHHHIIIJJJKKKLLLMMMNNNOOOPPPPPQQQ""
                BuildingSystemNumber_800.Productmaingroup1_100 = BuildingSystemNumber_800.Number.Substring(0, 3);
                BuildingSystemNumber_800.Productmaingroup2_110 = BuildingSystemNumber_800.Number.Substring(3, 3);
                BuildingSystemNumber_800.Accessorymaingroup2_160 = BuildingSystemNumber_800.Number.Substring(6, 3);
                BuildingSystemNumber_800.ProductvariantA_200 = BuildingSystemNumber_800.Number.Substring(9, 3);
                BuildingSystemNumber_800.VarianttypeA1_250 = BuildingSystemNumber_800.Number.Substring(12, 3);
                BuildingSystemNumber_800.VarianttypeA2_260 = BuildingSystemNumber_800.Number.Substring(15, 3);
                BuildingSystemNumber_800.ProductvariantB_300 = BuildingSystemNumber_800.Number.Substring(18, 3);
                BuildingSystemNumber_800.VarianttypeB1_350 = BuildingSystemNumber_800.Number.Substring(21, 3);
                BuildingSystemNumber_800.VarianttypeB2_360 = BuildingSystemNumber_800.Number.Substring(24, 3);
                BuildingSystemNumber_800.ProductvariantC_400 = BuildingSystemNumber_800.Number.Substring(27, 3);
                BuildingSystemNumber_800.VarianttypeC1_450 = BuildingSystemNumber_800.Number.Substring(30, 3);
                BuildingSystemNumber_800.VarianttypeC2_460 = BuildingSystemNumber_800.Number.Substring(33,3);
                BuildingSystemNumber_800.ProductvariantD_500 = BuildingSystemNumber_800.Number.Substring(36,3);
                BuildingSystemNumber_800.VarianttypeD1_550 = BuildingSystemNumber_800.Number.Substring(39,3);
                BuildingSystemNumber_800.VarianttypeD2_560 = BuildingSystemNumber_800.Number.Substring(42, 3);
                BuildingSystemNumber_800.Productelementdata_700 = BuildingSystemNumber_800.Number.Substring(45, 5);
                BuildingSystemNumber_800.Accessoryproductelement_760 = BuildingSystemNumber_800.Number.Substring(50, 3);

                BuildingSystemNumber_800s.Add(BuildingSystemNumber_800);
            }

            return BuildingSystemNumber_800s;
        }
    }

    public class ArticelNumber_810
    {
        public string RecordType;
        public string Index;
        public string Number;
        public string Comment;
        public ArticelNumber_810() { }

        public List<ArticelNumber_810> Register(List<RecordSet> recordSets)
        {
            List<ArticelNumber_810> ArticelNumber_810s = new List<ArticelNumber_810>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "810").ToList())
            {
                ArticelNumber_810 ArticelNumber_810 = new ArticelNumber_810();
                while (dataSet.Fields.Count <= 6) dataSet.Fields.Add(string.Empty);
                ArticelNumber_810.RecordType = dataSet.Fields[1].Substring(0, 3);
                ArticelNumber_810.Index = ("000" + dataSet.Fields[2]).Right(3);
                ArticelNumber_810.Number = dataSet.Fields[3].Substring(0, 53);
                ArticelNumber_810.Comment = dataSet.Fields[4];

                ArticelNumber_810s.Add(ArticelNumber_810);
            }

            return ArticelNumber_810s;
        }
    }

    public class FollowingRecordFunctionName_820
    {
        public string RecordType;
        public string Index;
        public string FunctionName;
        public string Comment;
        public FollowingRecordFunctionName_820() { }

        public List<FollowingRecordFunctionName_820> Register(List<RecordSet> recordSets)
        {
            List<FollowingRecordFunctionName_820> FollowingRecordFunctionName_820s = new List<FollowingRecordFunctionName_820>();
            foreach (RecordSet dataSet in recordSets.Where(x => x.RecordType == "820").ToList())
            {
                FollowingRecordFunctionName_820 FollowingRecordFunctionName_820 = new FollowingRecordFunctionName_820();
                while (dataSet.Fields.Count <= 6) dataSet.Fields.Add(string.Empty);
                FollowingRecordFunctionName_820.RecordType = dataSet.Fields[1].Substring(0, 3);
                FollowingRecordFunctionName_820.Index = ("000" + dataSet.Fields[2]).Right(3);
                FollowingRecordFunctionName_820.FunctionName = dataSet.Fields[3];
                FollowingRecordFunctionName_820.Comment = dataSet.Fields[4];

                FollowingRecordFunctionName_820s.Add(FollowingRecordFunctionName_820);
            }

            return FollowingRecordFunctionName_820s;
        }
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
        public List<CrossReferenceAccessoryGeometry_920> CrossReferenceAccessoryGeometry_920s;

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
        public List<AccessorySelection2_930_01> AccessorySelection2_930_01s;
        
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
        public List<Requirement1_930_02> Requirement1_930_02s;
        public List<AccessoryArticle_930_03> AccessoryArticle_930_03s;

        public AccessorySelection2_930_01() { }

    }

    public class Requirement1_930_02
    {
        public Requirement1_930_02() { }

    }

    public class AccessoryArticle_930_03
    {
        public List<Requirement2_930_04> Requirement2_930_04s;

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
