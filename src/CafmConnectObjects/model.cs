using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using etask.Ifc4;

namespace CafmConnectObjects
{
    public class CafmConnect
    {
        public void CreateNewFile()
        { 
            etask.Ifc4.Document document = null;

            // NUR weil mit einer template Datei gearbeitet wird!!!
            bool isBinaryCatalogue;
            string tempFullName = String.Empty;
                using (System.IO.Stream stream = GetCatalogueFromResource(out isBinaryCatalogue))
                {
                    if (stream != null)
                    {
                        using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                        {
                            stream.CopyTo(memoryStream);
                            tempFullName = System.IO.Path.GetTempFileName();
                            System.IO.File.WriteAllBytes(tempFullName, memoryStream.ToArray());

                        }
                    }
                }
                etask.Ifc4.Document.IfcFileType ifcFileType = isBinaryCatalogue ? etask.Ifc4.Document.IfcFileType.IfcXmlBin : etask.Ifc4.Document.IfcFileType.IfcXml;
                document = etask.Ifc4.Workspace.CurrentWorkspace.OpenDocument(tempFullName, ifcFileType);
                document.PopulateDefaultUosHeader();
                document.FullName = String.Empty;

                try { System.IO.File.Delete(tempFullName); }
                catch { }
        }

        public static System.IO.Stream GetCatalogueFromResource(out bool isBinaryCatalogue)
        {
            isBinaryCatalogue = false;

            try
            {
                string resourceName = "CafmConnectObjects.Catalogues.CAFM-ConnectFacilitiesViewTemplate.ifcxml";

                isBinaryCatalogue = resourceName.EndsWith(".bin", StringComparison.OrdinalIgnoreCase);
                var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                return assembly.GetManifestResourceStream(resourceName);
            }
            catch { return null; }
        }
    }
    public class Product
    {
        //Workspace ws = new Workspace();

    }
    
}
