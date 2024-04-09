using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Telerik.WinControls.UI;

namespace Common
{
    public static class UiHelperMethods
    {
        public static string ConvertGridDesignToString(RadGridView gridView)
        {
            string myStr = "";
            using (var stream = new MemoryStream())
            {
                gridView.SaveLayout(stream);
                stream.Position = 0;
                var sr = new StreamReader(stream);
                myStr = sr.ReadToEnd();
            }
            return myStr;

        }

        public static byte[] ConvertGridToByteArrray(RadGridView gridView)
        {
            using (var ms = new MemoryStream())
            {
                gridView.SaveLayout(ms);
                ms.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public static void LoadGridLayout(byte[] aBytes, RadGridView gridView)
        {
            Stream stream = new MemoryStream(aBytes);
            gridView.LoadLayout(stream);

        }

        public static void LoadGridLayout(string xml, RadGridView gridView)
        {
            var settings = new XmlReaderSettings
                               {
                                   ConformanceLevel = ConformanceLevel.Fragment,
                                   IgnoreWhitespace = true,
                                   IgnoreComments = true
                               };
            var xmlReader = XmlReader.Create(new StringReader(xml), settings);
            xmlReader.Read();
            gridView.LoadLayout(xmlReader);

        }

        public static List<string> GetColumnsFromXmlGridView(string contentXml, string column)
        {
            var reader = contentXml.ToXmlReader();

            var columns = new List<string>();

            while (reader.Read())
            {

                if (reader.NodeType == XmlNodeType.Element)
                {
                    var z = reader.Name;
                    if (z.StartsWith("Telerik.WinControls.UI.GridView"))
                    {


                        var columnV = reader[column];
                        columns.Add(columnV);
                    }
                }
            }
            return columns;


        }



        public static void CopyPasteRow(RadGridView radGrid)
        {
            var cb = CopyRows(radGrid);
            PasteRows(radGrid, cb);
        }





        private static object[,] CopyRows(RadGridView radGridView)
        {
            object[,] clipboard = new object[radGridView.SelectedRows.Count, radGridView.SelectedRows[0].Cells.Count];

            for (int i = 0; i < radGridView.SelectedRows.Count; ++i)
            {
                for (int j = 0; j < radGridView.SelectedRows[i].Cells.Count; ++j)
                {
                    var value = radGridView.SelectedRows[i].Cells[j].Value;
                    clipboard[i, j] = value != null ? value.ToString() : null;
                }

            }


            return clipboard;
        }

        private static void PasteRows(RadGridView radGridView, object[,] clipboard)
        {
            //radGridView.GridElement.BeginUpdate();
            radGridView.TableElement.BeginUpdate();
            try
            {


                for (int i = 0; i < clipboard.GetLength(0); ++i)
                {
                    var row = radGridView.Rows.AddNew();

                    for (int j = 0; j < clipboard.GetLength(1); ++j)
                    {
                        row.Cells[j].Value = clipboard[i, j];
                    }

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Incompatible DataSources");
            }
            //radGridView.GridElement.EndUpdate();
            radGridView.TableElement.EndUpdate();

        }

    }
}
