using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

namespace UpperComputer
{
    public class ClassXMLGET
    {
        public static T xmlget<T>(string Path) where T : new()
        {
            try
            {
                XmlSerializer d = new XmlSerializer(typeof(T));
                using (FileStream stream = new FileStream(Path, FileMode.OpenOrCreate))
                {
                    try
                    {
                        object obj = d.Deserialize(stream);
                        if (obj is T)
                        {
                            return (T)obj;
                        }
                        else
                        {
                            return new T();
                        }
                    }
                    catch
                    {

                        return new T();
                    }
                }
            }
            catch (Exception ex)
            {
                return new T();
            }
        }

        public static void Copy<T>(T source, ref T destination) where T : class, new()
        {
            if (source == null) source = new T();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            binaryFormatter.Serialize(stream, source);
            stream.Seek(0, SeekOrigin.Begin);
            destination = (T)binaryFormatter.Deserialize(stream);
            stream.Close();
            stream.Dispose();
        }


        public static bool xmlset<T>(string Path, T S)
        {
            try
            {
                using (FileStream stream = new FileStream(Path, FileMode.Create))
                {
                    XmlSerializer s = new XmlSerializer(typeof(T));
                    s.Serialize(stream, S);
                }
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }

}
