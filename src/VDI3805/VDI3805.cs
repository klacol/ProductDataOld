using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Compression;
using System.IO;

namespace VDI3805
{
    public class VDI3805
    {
        public ManufacturerFile manufacturerFile { get; set; }
        public List<DataSet> dataSets { get; set; }
        
        public VDI3805(string filename)
        {
           string DataFileName = string.Empty;

           using (ZipArchive zip = ZipFile.Open(filename, ZipArchiveMode.Read))
                foreach (ZipArchiveEntry entry in zip.Entries)
                    if (entry.Name.Substring(entry.Name.Length-3,3) == "VDI")
                    {
                        DataFileName = entry.Name;
                        string tmpFileName = Path.Combine(Path.GetTempPath(), DataFileName);
                        entry.ExtractToFile(tmpFileName,true);
                        StreamReader FileReader = new StreamReader(tmpFileName, Encoding.Default);
                        string line;
                        dataSets = new List<DataSet>();

                        while ((line = FileReader.ReadLine()) != null)
                        {
                            char delimiter = ';';
                            string[] lineParts = line.Split(delimiter);
                            DataSet dataSet = new DataSet();
                            dataSet.Fields = new List<string>();
                            dataSet.Fields.Add(string.Empty);
                            for (int i = 0; i < lineParts.Length; i++)
                            {
                                if (i == 0) dataSet.SetType = lineParts[i];
                                dataSet.Fields.Add(lineParts[i]);
                            }
                            dataSets.Add(dataSet);
                        }
                    }

            // Decode Filename according to spectification in VDI 3805, Part 1, Page 18
            // Structure of the filename: "pp_hhh_dd_rrr_aaa_iii_ooo.VDI"
            // 1 - pp number of the relevant part of VDI 3805
            // 2 - hhh manufacturer’s name
            // 3 - dd country code as per ISO 3166 ALPHA - 2
            // 4 - rrr issue date of the part of VDI 3805 in YYYYMM format
            // 5 - aaa issue date of file in YYYYMMDD format
            // 6 - iii content:
            //         KATALOG for product catalogue
            //         ANGEBOT for range catalogue
            //         PRODUKT for single product
            // 7 - ooo optional manufacturer’s text(if unused, also omit the _ character!).

            //Sample without Country Code: PART03_Broetje_200406_20160324_Katalog.VDI    (This is wrong, The country code is missing)
            //Sample with Country Code:    PART03_Broetje_DE_200406_20160324_Katalog.VDI (This is correct)

            manufacturerFile = new ManufacturerFile(DataFileName);
            manufacturerFile.RegisterLeadData(dataSets.Where(x=>x.SetType=="010").FirstOrDefault());
            manufacturerFile.RegisterProductData(dataSets.Where(x => x.SetType == "800").ToList());
            manufacturerFile.RegisterProperties(manufacturerFile.VDI3805PartNumber,dataSets.Where(x => x.SetType == "700").ToList());

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
            manufacturerFile.MapBsNumbers();

        }

    }
}
