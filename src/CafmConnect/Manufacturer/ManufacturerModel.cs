using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafmConnect.Manufacturer
{
    public class CcManufacturerProduct
    {
        List<CcManufacturerProductDetail> m_attributes;
        string m_Code;
        string m_Description;
        string m_Name;
        string m_name = "temp";

        public CcManufacturerProduct(string classificationCode)
        {
            m_attributes = new List<CcManufacturerProductDetail>();
            m_Code = classificationCode;
        }

        public List<CcManufacturerProductDetail> Attributes
        {
            get
            {
                return m_attributes;
            }

            set
            {
                m_attributes = value;
            }
        }

        public string Code
        {
            get
            {
                return m_Code;
            }
        }

        public string Description { get { return m_Description; } set { m_Description = value; } }

        public string Name { get { return m_name; } set { m_name = value; } }
    }

    public class CcManufacturerProductDetail
    {
        string m_attributeName;
        string m_attributeDescription;
        string m_attributeCcName;
        string m_attributeUnit;
        string m_attributeValue;

        public CcManufacturerProductDetail(string attributeName,string attributeCcName, string attributeValue, string attributeUnit="")
        {
            m_attributeName = attributeName;
            m_attributeCcName = attributeCcName;
            m_attributeUnit = attributeUnit;
            m_attributeValue = attributeValue;
        }

        public string AttributeName
        {
            get
            {
                return m_attributeName;
            }

            set
            {
                m_attributeName = value;
            }
        }

        public string AttributeDescription
        {
            get
            {
                return m_attributeDescription;
            }

            set
            {
                m_attributeDescription = value;
            }
        }

        public string AttributeCcName
        {
            get
            {
                return m_attributeCcName;
            }

            set
            {
                m_attributeCcName = value;
            }
        }

        public string AttributeUnit
        {
            get
            {
                return m_attributeUnit;
            }

            set
            {
                m_attributeUnit = value;
            }
        }

        public string AttributeValue
        {
            get
            {
                return m_attributeValue;
            }

            set
            {
                m_attributeValue = value;
            }
        }
    }

}
