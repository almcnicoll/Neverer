using Neverer.UtilityClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Excel = Microsoft.Office.Interop.Excel;

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
        /*
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
        */

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

        public static String Replace(this String original, IEnumerable<String> oldStrings, String newString)
        {
            String output = original;
            foreach (String oldString in oldStrings)
            {
                if (oldString != null)
                {
                    output = output.Replace(oldString, newString);
                }
            }
            return output;
        }

        public static void OpenTag(this HtmlTextWriter writer, HtmlTag tag)
        {
            foreach (KeyValuePair<HtmlTextWriterAttribute, string> attr in tag.Attributes)
            {
                writer.AddAttribute(attr.Key, attr.Value);
            }
            foreach (KeyValuePair<HtmlTextWriterStyle, string> style in tag.StyleAttributes)
            {
                writer.AddStyleAttribute(style.Key, style.Value);
            }
            writer.RenderBeginTag(tag.TagName);
        }
        public static void RenderTagContents(this HtmlTextWriter writer, HtmlTag tag)
        {
            if (tag.InnerTag != null) { writer.RenderWholeTag(tag.InnerTag); }
            if (tag.InnerText != null) { writer.Write(tag.InnerText); }
        }
        public static void CloseTag(this HtmlTextWriter writer, HtmlTag tag)
        {
            writer.RenderEndTag();
        }
        public static void RenderWholeTag(this HtmlTextWriter writer, HtmlTag tag)
        {
            writer.OpenTag(tag);
            writer.RenderTagContents(tag);
            writer.CloseTag(tag);
        }

        /// <summary>
        /// Updates the dictionary entry if key exists, otherwise adds it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>true if key added, false if key updated</returns>
        public static Boolean AddOrUpdate<T, U>(this Dictionary<T, U> dict, T key, U value)
        {
            if (dict.ContainsKey(key))
            {
                dict[key] = value;
                return false;
            }
            else
            {
                dict.Add(key, value);
                return true;
            }
        }
    }

    /// <summary>
    /// A set of methods that are much easier to use than the usual <see cref="Control.Invoke"/> versions, since they
    /// accept the standard delegate types as arguments.
    /// </summary>
    /// <remarks>
    /// Instead of using the version of <see cref="Control.Invoke"/> that allows you to pass parameters to your
    /// delegate, just capture them in a closure when using one of these.
    /// </remarks>
    public static class WindowsFormsControlInvoke
    {
        /// <summary>
        /// Executes the specified delegate on the thread that owns the control's underlying window handle.
        /// </summary>
        /// <param name="control">The control whose window handle the delegate should be invoked on.</param>
        /// <param name="method">A delegate that contains a method to be called in the control's thread context.</param>
        public static void Invoke(this System.Windows.Forms.Control control, Action method)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(method);
            }
            else
            {
                method();
            }
        }

        /// <summary>
        /// Executes the specified delegate on the thread that owns the control's underlying window handle, returning a
        /// value.
        /// </summary>
        /// <param name="control">The control whose window handle the delegate should be invoked on.</param>
        /// <param name="method">A delegate that contains a method to be called in the control's thread context and
        /// that returns a value.</param>
        /// <returns>The return value from the delegate being invoked.</returns>
        public static TResult Invoke<TResult>(this System.Windows.Forms.Control control, Func<TResult> method)
        {
            if (control.InvokeRequired)
            {
                return (TResult)control.Invoke(method);
            }
            else
            {
                return method();
            }
        }
    }
}