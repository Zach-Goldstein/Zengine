using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Engine
{
    public static class Extensions
    {
        public static string Attr(this XmlElement xml, string name, string onFail)
        {
            XmlAttribute xmlList;

            if ((xmlList = xml.Attributes[name]) is null)
                return onFail;

            return xmlList.InnerText;
        }
    }
}
