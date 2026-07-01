using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;



namespace UpperComputer
{
    public static class BasicArithmetic
    {

        /// <summary>
        /// 保存数据到本地XML文件中
        /// </summary>
        /// <param name="s_FilePath">XML文件路径</param>
        /// <param name="s_SourceObj">资源对象</param>
        /// <param name="type">类型</param>
        public static void SaveToXml(string s_FilePath, object s_SourceObj, Type type)
        {
            if (!string.IsNullOrWhiteSpace(s_FilePath) && s_SourceObj != null)
            {
                type = ((type != (Type)null) ? type : s_SourceObj.GetType());
                using (StreamWriter textWriter = new StreamWriter(s_FilePath))
                {
                    System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(type);
                    XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
                    xmlSerializerNamespaces.Add("", "");
                    xmlSerializer.Serialize(textWriter, s_SourceObj, xmlSerializerNamespaces);
                }
            }
        }
        /// <summary>
        /// 从本地XML文件中获取数据
        /// </summary>
        /// <param name="s_FilePath">XML文件路径</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static object LoadFromXml(string s_FilePath, Type type)
        {
            object result = null;
            if (File.Exists(s_FilePath))
            {
                using (StreamReader textReader = new StreamReader(s_FilePath))
                {
                    System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(type);
                    result = xmlSerializer.Deserialize(textReader);
                }
            }
            return result;
        }
    }
}
