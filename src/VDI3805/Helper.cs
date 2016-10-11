using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace VDI3805
{
    public class LocalizedDescriptionAttribute : DescriptionAttribute
    {
        static string Localize(string key)
        {
            return MyResources.ResourceManager.GetString(key);
        }

        public LocalizedDescriptionAttribute(string key)
            : base(Localize(key))
        {
        }
    }
}
