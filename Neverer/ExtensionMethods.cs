using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text.RegularExpressions;

namespace Neverer
{
    public static class ExtensionMethods
    {
        public static int ToOle(this System.Drawing.Color c)
        {
            return System.Drawing.ColorTranslator.ToOle(c);
        }

        public static void AllBorders(this Excel.Range r)
        {
            Excel.Borders _borders = r.Borders;
            _borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
            _borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
            _borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
            _borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
            _borders.Color = System.Drawing.Color.Black.ToOle();
        }

        public static String ToExcelCol(this int i)
        {
            int dividend = i;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }

        public static void AddIfNotExists<T, U>(this SerializableDictionary<T, U> dict, T key, U valueIfNotExists) where U : class
        {
            if (dict.ContainsKey(key))
            {
                return;
            }
            dict.Add(key, valueIfNotExists);
        }
        public static U RetrieveIfKeyExists<T, U>(this SerializableDictionary<T, U> dict, T key, U valueIfNotExists = null) where U : class
        {
            if (dict.ContainsKey(key))
            {
                return dict[key];
            }
            else
            {
                return valueIfNotExists;
            }
        }

        public static string SerializeObject<T>(this T toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }

        public static T DeserializeObject<T>(this String source)
        {
            T returnObject;
            using (StringReader read = new StringReader(source))
            {
                Type outType = typeof(T);

                XmlSerializer serializer = new XmlSerializer(outType);
                using (XmlReader reader = new XmlTextReader(read))
                {
                    returnObject = (T)serializer.Deserialize(reader);
                    reader.Close();
                }

                read.Close();
            }
            return returnObject;
        }

        public static T LoadFromXML<T>(String filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                Exception ex = new Exception(String.Format("Error opening file {0}: file is empty or missing.", filePath));
                throw ex;
            }

            T returnObject;

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filePath);
                string xmlString = xmlDoc.OuterXml;

                using (StringReader read = new StringReader(xmlString))
                {
                    Type outType = typeof(T);

                    XmlSerializer serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        returnObject = (T)serializer.Deserialize(reader);
                        reader.Close();
                    }

                    read.Close();
                }
                return returnObject;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error opening file: {0}", ex.Message), ex);
            }
        }

        public static void SaveToXML<T>(this T sourceObject, String filePath)
        {
            try
            {
                String pathOnly = Path.GetDirectoryName(filePath);
                Directory.CreateDirectory(pathOnly);
                XmlSerializer serial = new System.Xml.Serialization.XmlSerializer(typeof(T));
                XmlDocument xmlDoc = new XmlDocument();
                using (MemoryStream stream = new MemoryStream())
                {
                    serial.Serialize(stream, sourceObject);
                    stream.Position = 0;
                    xmlDoc.Load(stream);
                    xmlDoc.Save(filePath);
                    stream.Close();
                }
                return;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error saving: {0}", ex.Message), ex);
            }
        }

        public static bool MatchesRegex(this String haystack, Regex needle)
        {
            return needle.IsMatch(haystack);
        }
        public static bool MatchesRegex(this String haystack, String needle)
        {
            return needle.MatchesRegex(new Regex(haystack));
        }
        public static bool MatchesRegex(this String haystack, String needle, RegexOptions options)
        {
            return needle.MatchesRegex(new Regex(haystack, options));
        }
    }
}
