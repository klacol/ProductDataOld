using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Compression;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace VDI3805
{
    public class VDI3805
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

        private List<RecordSet> dataSets;

        private VDI3805()
        { }
        
        public VDI3805(string filename)
        {
           string DataFileName = string.Empty;

           using (ZipArchive zip = ZipFile.Open(filename, ZipArchiveMode.Read))
                foreach (ZipArchiveEntry entry in zip.Entries)
                    if (entry.Name.Substring(entry.Name.Length-3,3).ToUpper() == "VDI")
                    {
                        DataFileName = entry.Name;
                        string tmpFileName = Path.Combine(Path.GetTempPath(), DataFileName);
                        entry.ExtractToFile(tmpFileName,true);
                        StreamReader FileReader = new StreamReader(tmpFileName, Encoding.Default);
                        string line;
                        dataSets = new List<RecordSet>();

                        while ((line = FileReader.ReadLine()) != null)
                        {
                            char delimiter = ';';
                            string[] lineParts = line.Split(delimiter);
                            RecordSet dataSet = new RecordSet();
                            dataSet.Fields = new List<string>();
                            dataSet.Fields.Add(string.Empty);
                            for (int i = 0; i < lineParts.Length; i++)
                            {
                                if (i == 0) dataSet.RecordType = lineParts[i];
                                if ((i == 1) && (dataSet.RecordType !="010")) dataSet.Index = ("000" + lineParts[i]).Right(3);

                                dataSet.Fields.Add(lineParts[i]);
                            }
                            dataSets.Add(dataSet);
                        }
                    }

            RegisterFilename(DataFileName);
            LeadData_010 = new LeadData_010(dataSets, VDI3805PartNumber);
        }

        private void RegisterFilename(string dataFileName)
        {
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

            string[] fileNameParts = dataFileName.Substring(0, dataFileName.Length - 4).Split('_');

            Filename = dataFileName;
            VDI3805PartNumber = fileNameParts[0].ToUpper();
            ManufacturerName = fileNameParts[1];
            CountryCode = fileNameParts[2];

            //Dirty workaround for file with missing country code => fallback to DE

            if ((CountryCode == "DE") || (CountryCode == "EN"))
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
                if (fileNameParts.Length == 6) ManufacturerText = fileNameParts[5];
            }
        }

        public string ToXml()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(VDI3805));
            StringWriter stringWriter = new StringWriter();

            var settingsPrettyprint = new XmlWriterSettings();
            settingsPrettyprint.OmitXmlDeclaration = true;
            settingsPrettyprint.Indent = true;
            settingsPrettyprint.NewLineOnAttributes = true;

            using (XmlWriter writer = XmlWriter.Create(stringWriter, settingsPrettyprint))
            {
                xmlSerializer.Serialize(writer, this);
            }

            return stringWriter.ToString();
        }

        public string GetCclassification()
        {
            string Cclassification = string.Empty;

            Dictionary<string, string> mapping = new Dictionary<string, string>();

            mapping.Add("PART02", "422.82");    //HeatingValueAssemblies / Heizungsarmaturen
            mapping.Add("PART03", "421.10");    //HeatGenerator / Wärmeerzeuger
            mapping.Add("PART04","");           //Pumps / Pumpen
            mapping.Add("PART05", "");          //AirOpenings / Luftdurchlässe
            mapping.Add("PART06", "423.17");    //Radiators / Heizkörper
            mapping.Add("PART07", "");          //Fans / Ventilatoren
            mapping.Add("PART08", "");          //Burners / Brenner
            mapping.Add("PART09", "");          //ModularVentilationEquipments / Modullüftungsgeräte
            mapping.Add("PART10", "");          //Luftfilter
            mapping.Add("PART11", "");          //Wärmetauscher Fluid/ Wasserdampf - Luft
            mapping.Add("PART14", "");          //RLT - Schalldämpfer(passiv)
            mapping.Add("PART16", "");          //Brandschutzklappe
            mapping.Add("PART17", "");          //Armaturen für die Trinkwasserinstallation
            mapping.Add("PART18", "");          //Flächenheizung / -kühlung
            mapping.Add("PART19", "");          //Sonnenkollektoren
            mapping.Add("PART20", "");          //Speicher und Durchlauferhitzer
            mapping.Add("PART22", "");          //Wärmepumpen
            mapping.Add("PART23", "");          //Wohnungslüftungsgeräte
            mapping.Add("PART25", "");          //Deckenkühlelemente
            mapping.Add("PART29", "");          //Rohre und Formstücke
            mapping.Add("PART32", "");          //Verteiler / Sammler
            mapping.Add("PART35", "");          //Klappen, Blenden und Volumenstromregler
            mapping.Add("PART37", "");          //Dezentrale Fassadenlüftungsgeräte
            mapping.Add("PART99", "");          //Allgemeine Komponenten

            return mapping[VDI3805PartNumber];
        }
    }
}
