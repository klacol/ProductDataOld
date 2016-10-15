using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Ifc4;
using System.Linq;
using CafmConnect.Manufacturer;
using VDI3805;
using System.IO.Compression;

namespace CafmConnect
{
    public partial class Workspace
    {
        Dictionary<string, Ifc4.Document> m_ifc4Documents;
        Dictionary<string, string> m_ifc4TextDocuments;
        static Workspace m_current = null;
        const string m_extension = ".ifcxml";

        private List<IfcClassificationReference> m_TempAllIfcClassificationReferenceCollection = null;
        private List<IfcClassificationReference> m_TempCatalogueFilteredIfcClassificationReferenceCollection = null;

        public Workspace()
        {
            m_current = this;
            m_ifc4Documents = new Dictionary<string, Ifc4.Document>();
            m_ifc4TextDocuments = new Dictionary<string, string>();
        }

        public Dictionary<string, Document> Documents
        {
            get
            {
                return Current.m_ifc4Documents;
            }
        }

        public Dictionary<string, string> TextDocuments
        {
            get
            {
                return Current.m_ifc4TextDocuments;
            }
        }

        public static Workspace Current
        {
            get
            {
                if (m_current == null)
                    m_current = new Workspace();

                return m_current;
            }
        }

        #region Create files

        /// <summary>
        /// Create a new object based on the CAFM-Connect specification.
        /// The file will be stored to your temp dir! Make sure you call SaveCcFileAs to save it to your desired directory!
        /// </summary>
        /// <param name="author">Name of the author of the file</param>
        /// <param name="organization">Name of the organisation, that created the file</param>
        /// <param name="originatingSystem">name of the software spplication, that creates the file</param>
        /// <param name="authorization">name of the person, that has authorized the content of the file</param>
        public string CreateCcFile(string author, string organization, string originatingSystem, string authorization)
        {
            string tempModelFilename = InitializeCcFile();
            Document doc = null;

            string a = "1";

            if (!Current.Documents.ContainsKey(tempModelFilename))
            {
                doc = Ifc4.Workspace.CurrentWorkspace.OpenDocument(tempModelFilename);
                doc.IfcXmlDocument.Header.Author = author;
                doc.IfcXmlDocument.Header.Organization = organization;
                doc.IfcXmlDocument.Header.OriginatingSystem = originatingSystem;
                doc.IfcXmlDocument.Header.Authorization = authorization;
                Current.Documents.Add(tempModelFilename, doc);
            }

            return tempModelFilename;
        }

        public string CreateCcFileFromVDI3805(VDI3805.VDI3805 vdi3805)
        {
            string tempModelFilename = InitializeCcFile();
            Document doc = null;

            if (!Current.Documents.ContainsKey(tempModelFilename))
            {
                doc = Ifc4.Workspace.CurrentWorkspace.OpenDocument(tempModelFilename);
                doc.IfcXmlDocument.Header.Author = vdi3805.ManufacturerName;
                doc.IfcXmlDocument.Header.Organization = vdi3805.ManufacturerText;
                doc.IfcXmlDocument.Header.OriginatingSystem = vdi3805.Filename;
                doc.IfcXmlDocument.Header.Authorization = vdi3805.ManufacturerUrl;
                Current.Documents.Add(tempModelFilename, doc);
            }
            string siteGuid = AddNewSite(tempModelFilename, vdi3805.ManufacturerName,vdi3805.ManufacturerName,vdi3805.ManufacturerName2,vdi3805.ManufacturerText, vdi3805.LeadData_010.IssueMonth, vdi3805.CountryCode);
            foreach (ProductMainGroup1_100 pmg in vdi3805.LeadData_010.ProductMainGroup1_100s)
            {
                CafmConnect.Manufacturer.CcManufacturerProduct product = new CcManufacturerProduct("461");
                product.Description = pmg.ProductDesignation;
                product.Name = pmg.ProductDesignation;
                //product.Attributes.Add(new CcManufacturerProductDetail("Anzahl Haltestellen", "Anzahl Haltestellen", "10"));
                //product.Attributes.Add(new CcManufacturerProductDetail("Tragkraft in Personen", "Tragkraft in Personen", "5"));
                //product.Attributes.Add(new CcManufacturerProductDetail("Tragkraft", "Tragkraft", (i * 2).ToString()));

                if (siteGuid != null) AddNewProduct(tempModelFilename, siteGuid, "461", product);

            }

            return tempModelFilename;
        }
        #endregion

        #region Load files
        /// <summary>
        /// Loads the specified ifcxml file into the current workspace
        /// </summary>
        /// <param name="filename">path to existing ifcxml file without extension</param>
        public void LoadCcFile(string filename)
        {
            if (File.Exists(filename))
            {
                if (!Current.Documents.ContainsKey(filename))
                {
                    Document doc = Ifc4.Workspace.CurrentWorkspace.OpenDocument(filename, Ifc4.Document.IfcFileType.IfcXml);
                    Current.Documents.Add(filename, doc);
                }
            }
        }

        /// <summary>
        /// Loads a collection of files into the current workspace
        /// </summary>
        /// <param name="filenames">List of ifcxml files to load, without extensions</param>
        public void LoadCcFiles(List<string> filenames)
        {
            foreach (string file in filenames)
            {
                LoadCcFile(file);
            }
        }
        #endregion

        #region Save files
        /// <summary>
        /// Saves the IfcXml file from current workspace based on the CAFM-Connect specification
        /// </summary>
        /// <param name="filename">Path to the file to open, without the extension. if an filename with extensions will be passed, the extension will be removed and the extension .ifcxml will be automatically applied</param>
        public void SaveCcFile(string filename)
        {
            if (Current.Documents.ContainsKey(filename))
            {
                Current.Documents[filename].Workspace.SaveDocument(Path.GetFileNameWithoutExtension(filename) + "ifcxml");
            }
        }

        /// <summary>
        /// Saves the IfcXml file from current workspace based on the CAFM-Connect specification to a new filename
        /// </summary>
        /// <param name="currentFilename"></param>
        /// <param name="newFilename"></param>
        public void SaveCcFileAs(string currentFilename, string newFilename, bool overrideFile = true, bool newFile = false)
        {
            if (Current.Documents.ContainsKey(currentFilename))
            {
                if (File.Exists(newFilename))
                {
                    if (overrideFile)
                        File.Delete(newFilename);
                }

                bool result = Current.Documents[currentFilename].Workspace.SaveDocument(newFilename);
                if (newFile) File.SetCreationTime(newFilename, DateTime.Now);
                File.SetLastWriteTime(newFilename, DateTime.Now);


                //m_ifc4TextDocuments.Add(currentFilename           
            }
        }

        #endregion


        #region Edit files

        public string AddNewSite(string currentFilename, string siteCode, string siteDescription, string siteStreet, string sitePostalCode, string siteTown, string siteCountry)
        {
            if (Current.Documents.ContainsKey(currentFilename))
            {
                Ifc4.IfcSite ifcSite = Current.Documents[currentFilename].Project.Sites.AddNewSite();
                ifcSite.GlobalId = Ifc4.GlobalId.ConvertToIfcGuid(Guid.NewGuid());
                ifcSite.Name = siteCode;
                ifcSite.LongName = siteDescription;
                ifcSite.Description = siteCode;

                siteStreet = SetString(siteStreet);
                sitePostalCode = SetString(sitePostalCode);
                siteTown = SetString(siteTown);
                siteCountry = SetString(siteCountry);

                var ifcPostalAddress = (from item in Current.Documents[currentFilename].IfcXmlDocument.Items.OfType<Ifc4.IfcPostalAddress>()
                                        where
                                                 item.AddressLines != null &&
                                                 item.AddressLines.IfcLabelwrapper.Any() &&
                                                 item.AddressLines.IfcLabelwrapper[0].Value == siteStreet &&
                                                 item.Town.Equals(siteTown) &&
                                                 item.PostalCode.Equals(sitePostalCode)
                                        select item).FirstOrDefault();

                if (ifcPostalAddress == null)
                {
                    ifcPostalAddress = new Ifc4.IfcPostalAddress()
                    {
                        Id = Current.Documents[currentFilename].GetNextSid()
                    };

                    ifcPostalAddress.AddressLines = new Ifc4.IfcPostalAddressAddressLines();
                    ifcPostalAddress.AddressLines.IfcLabelwrapper.Add(new Ifc4.IfcLabelwrapper() { Value = siteStreet });
                    ifcPostalAddress.Town = siteTown;
                    ifcPostalAddress.PostalCode = sitePostalCode;
                    ifcPostalAddress.Country = siteCountry;

                    Current.Documents[currentFilename].IfcXmlDocument.Items.Add(ifcPostalAddress);
                }
                ifcSite.SiteAddress = new Ifc4.IfcPostalAddress() { Ref = ifcPostalAddress.Id };
                return ifcSite.GlobalId;
            }

            return null;
            
        }

        public void AddNewProduct(string currentFilename, string siteGuid, string classificationCode, CcManufacturerProduct product)
        {
            if (Current.Documents.ContainsKey(currentFilename))
            {

                // ---------------------------------------
                // z.B. 
                string catalogueName = "CAFMConnectCatalogueOfObjectTypes";

                if (m_TempAllIfcClassificationReferenceCollection == null)
                m_TempAllIfcClassificationReferenceCollection = GetIfcClassificationReferenceCollectionFromCatalogue(currentFilename,catalogueName);
                
                var filteredIfcClassificationReference = m_TempAllIfcClassificationReferenceCollection.FirstOrDefault(item => item.Identification == product.Code);
                // ---------------------------------------

                Ifc4.CcFacility newSystem = Current.Documents[currentFilename].Project.Facilities.Where(x => x.IfcSystem.Name == product.Code).FirstOrDefault();
                if (newSystem == null)
                { 
                    newSystem = Current.Documents[currentFilename].Project.Facilities.AddNewSystem(Current.Documents[currentFilename].Project);
                    newSystem.IfcSystem.GlobalId = Ifc4.GlobalId.ConvertToIfcGuid(Guid.NewGuid());
                    newSystem.IfcSystem.Name = product.Code;
                    newSystem.IfcSystem.Description = product.Description;
                }
                CcFacility newFacility = newSystem.Facilities.AddNewFacility();
                newFacility.ObjectTypeId = filteredIfcClassificationReference.Id; // z.B "i2139"
                newFacility.Number = product.Name;
                newFacility.IfcObjectDefinition.Name = product.Code;
                newFacility.Description = product.Description;

                var propertyDescriptors = from propertyDescriptor in System.ComponentModel.TypeDescriptor.GetProperties(newFacility).Cast<System.ComponentModel.PropertyDescriptor>().OfType<Ifc4.CustomModel.CustomPropertyDescriptor>()
                                          select propertyDescriptor;

                foreach (CcManufacturerProductDetail attribute in product.Attributes)
                foreach (var propertyDescriptor in propertyDescriptors)
                {
                    if (propertyDescriptor.IsReadOnly)
                        continue;

                    if (propertyDescriptor.Converter == null)
                        continue;

                    if (propertyDescriptor.IfcPropertyName==attribute.AttributeCcName)
                    {
                        newFacility.SetValue(propertyDescriptor, attribute.AttributeValue);
                        continue;
                    }
                }

            }
        }


        #endregion

        private List<IfcClassificationReference> GetIfcClassificationReferenceCollectionFromCatalogue(string currentFilename, string catalogueName)
        {
            // suche die IfcClassification, die am Project mit der Description (catalogueType) angemeldet ist
            var document = Current.Documents[currentFilename];
            var relatingClassificationRefs = document.IfcXmlDocument.Items.OfType<Ifc4.IfcRelAssociatesClassification>()
                                            .Where(root => root.RelatedObjects.Items.Exists(item => item.Ref == document.Project.Id))
                                            .Select(item => item.RelatingClassification.Item.Ref);

            var classification = document.IfcXmlDocument.Items.OfType<IfcClassification>().SingleOrDefault(item => item.Description == catalogueName && relatingClassificationRefs.Contains(item.Id));
            if (classification == null)
                return null;

            // ermittle alle IfcClassificationReferences die zu der Catalogue IfcClassifification gehören
            m_TempAllIfcClassificationReferenceCollection = null;
            m_TempCatalogueFilteredIfcClassificationReferenceCollection = new List<IfcClassificationReference>();
            IterateTempIfcClassificationReference(document, classification.Id);
            return m_TempCatalogueFilteredIfcClassificationReferenceCollection;
        }

        private void IterateTempIfcClassificationReference(Ifc4.Document document, string sid)
        {
            if (m_TempAllIfcClassificationReferenceCollection == null)
                m_TempAllIfcClassificationReferenceCollection = document.IfcXmlDocument.Items.OfType<Ifc4.IfcClassificationReference>().ToList();

            var classificationReferenceCollection = m_TempAllIfcClassificationReferenceCollection.Where(item => item.ReferencedSource != null && item.ReferencedSource.Item.Ref == sid).Select(item => item);
            foreach (var classificationReference in classificationReferenceCollection)
            {
                m_TempCatalogueFilteredIfcClassificationReferenceCollection.Add(classificationReference);
                IterateTempIfcClassificationReference(document, classificationReference.Id);
            }
        }

        private string SetString(string value)
        {
            if (value == null) return string.Empty;
            else return value;
        }

        private string InitializeCcFile()
        {
           // ------------------------------------------------------
           // Please work every time with the catalogue template file
           // Check the Original Source: http://katalog.cafm-connect.org/CC-Katalog/CAFM-ConnectFacilitiesViewTemplate.ifcxml
           //-------------------------------------------------------

           string tempPath = Path.GetTempPath();
           string tempName = Guid.NewGuid().ToString() + ".ifcxml";

           string tempModelFilename = Path.Combine(tempPath, tempName);
           using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream("CafmConnect.Catalogue.CAFM-ConnectFacilitiesViewTemplate.ifcxml"))
            {
                using (var file = new FileStream(tempModelFilename, FileMode.Create, FileAccess.Write))
                {
                    resource?.CopyTo(file);
                }
            }

           return tempModelFilename;
        }

        public string GetModelOfCcFile(string dataFileName)
        {
           string content = string.Empty;
           using (ZipArchive zip = ZipFile.Open(dataFileName, ZipArchiveMode.Read))
                foreach (ZipArchiveEntry entry in zip.Entries)
                    if (Path.GetFileNameWithoutExtension(entry.Name) == Path.GetFileNameWithoutExtension(dataFileName))
                    {
                        dataFileName = entry.Name;
                        string tmpFileName = Path.Combine(Path.GetTempPath(), dataFileName);
                        entry.ExtractToFile(tmpFileName, true);
                        using (StreamReader reader = new StreamReader(tmpFileName))
                            {
                                content = reader.ReadToEnd();
                            }
                    }
            return content;
        }
    }
}
