using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;
using DataTable = System.Data.DataTable;
using Excel = Microsoft.Office.Interop.Excel;
using XlHAlign = Microsoft.Office.Core.XlHAlign;

namespace Common
{
    public class ExcelService
    {
        public Application ExcelApp = new Excel.Application();
        public Excel._Worksheet WorkSheet;
        public ExcelService()
        {
            ExcelApp.Workbooks.Add();
            WorkSheet = ExcelApp.ActiveSheet;
        }
        public  void ExportDtataTableToExcel( DataTable tbl,int rowToStart,int cellStart,bool headerBold)
        {
            try
            {
                if (tbl == null || tbl.Columns.Count == 0)
                    throw new Exception("ExportToExcel: Null or empty input table!\n");

                for (int i = 0; i < tbl.Columns.Count; i++)
                {
                    if (headerBold)
                    {
                        WorkSheet.Cells[rowToStart, (i + 1) + cellStart] = tbl.Columns[i].ColumnName; //1
                        WorkSheet.Cells[rowToStart, (i + 1) + cellStart].Font.Bold = true;
                
                    }
                    else
                    {
                        WorkSheet.Cells[rowToStart, (i + 1) + cellStart] = tbl.Columns[i].ColumnName; //1
                    }
                }
                // rows
                for (int i = rowToStart - 1; i < tbl.Rows.Count + rowToStart - 1; i++) //0
                {
                    // to do: format datetime values before printing
                    for (int j = 0; j < tbl.Columns.Count; j++)
                    {
                        var value = tbl.Rows[i - (rowToStart - 1)][j];

                        WorkSheet.Cells[(i + 2), (j + 1) + cellStart] = value;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ExportToExcel: \n" + ex.Message);
            }
        }
        public void ExportListOfDtataTableToExcel(List<DataTable> listtable,string fullAddress,string fax,string phone,string clinetName,string email ,int rowToStart, int cellStart, bool headerBold,string path,DateTime from,DateTime to)
        {
            var ii = 0;
            var workbook = ExcelApp.Workbooks.Add(Missing.Value);


            WorkSheet = (Worksheet) workbook.Worksheets.Item[ii + 1];
            WorkSheet.Name = string.Format(clinetName, ii + 1);
            ii++;
            foreach (var table in listtable)
            {
                try
                {
                    if (table == null || table.Columns.Count == 0)
                        throw new Exception("ExportToExcel: Null or empty input table!\n");

                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        if (headerBold)
                        {
                            WorkSheet.Cells[rowToStart, (i + 1) + cellStart] = table.Columns[i].ColumnName; //1
                            WorkSheet.Cells[rowToStart, (i + 1) + cellStart].Font.Bold = true;
                            //SetStyleToCell(((i + 1) + cellStart).ToString(), rowToStart.ToString(), true, "Arial", 11, false,
                            //               false);
                        }
                        else
                        {
                            WorkSheet.Cells[rowToStart, (i + 1) + cellStart] = table.Columns[i].ColumnName; //1
                        }
                    }
                    // rows
                    for (int i = rowToStart - 1; i < table.Rows.Count + rowToStart - 1; i++) //0
                    {
                        // to do: format datetime values before printing
                        for (int j = 0; j < table.Columns.Count; j++)
                        {
                            WorkSheet.Cells[(i + 2), (j + 1) + cellStart] = table.Rows[i - (rowToStart - 1)][j];
                        }
                    }


                    AddFiledToSpecificPlace(2, 5, ": שם לקוח");
                    SetStyleToCell("B5", "B5", true, "Arial", 11, false, false);
                    AddFiledToSpecificPlace(3, 5, clinetName);
                    SetStyleToCell("C5", "C5", false, "Arial", 11, false, false, (XlHAlign)Excel.XlHAlign.xlHAlignCenter);

                    AddFiledToSpecificPlace(2, 6, ": הזמנות בין תאריכים");
                    SetStyleToCell("B6", "B6", true, "Arial", 11, false, false);
                    AddFiledToSpecificPlace(3, 6,
                                            from.ToString("dd/MM/yyyy") + " - " + to.ToString("dd/MM/yyyy"));
                    SetStyleToCell("C6", "C6", false, "Arial", 11, false, false, (XlHAlign)Excel.XlHAlign.xlHAlignCenter);

                    AddFiledToSpecificPlace(2, 8, ": אימייל");
                    SetStyleToCell("B8", "B8", true, "Arial", 11, false, false);
                    AddFiledToSpecificPlace(3, 8, email);
                    SetStyleToCell("C8", "C8", false, "Arial", 11, false, false, (XlHAlign)Excel.XlHAlign.xlHAlignCenter);

                    AddFiledToSpecificPlace(2, 10, ": טלפון");
                    SetStyleToCell("B10", "B10", true, "Arial", 11, false, false);
                    AddFiledToSpecificPlace(3, 10, phone);
                    SetStyleToCell("C10", "C10", false, "Arial", 11, false, false, (XlHAlign)Excel.XlHAlign.xlHAlignCenter);

                    AddFiledToSpecificPlace(2, 9, ": פקס");
                    SetStyleToCell("B9", "B9", true, "Arial", 11, false, false);
                    AddFiledToSpecificPlace(3, 9, fax);
                    SetStyleToCell("C9", "C9", false, "Arial", 11, false, false, (XlHAlign)Excel.XlHAlign.xlHAlignCenter);


                    AddFiledToSpecificPlace(2, 11, ": כתובת");
                    SetStyleToCell("B11", "B11", true, "Arial", 11, false, false);
                    AddFiledToSpecificPlace(3, 11, fullAddress);
                    SetStyleToCell("C11", "C11", false, "Arial", 11, false, false, (XlHAlign)Excel.XlHAlign.xlHAlignCenter);

                    //if (radCheckBoxAproveOnly.Checked)
                    //{
                    //   AddFiledToSpecificPlace(0, 4, "הזמנות מאושרות בלבד");
                    //   SetStyleToCell("D7", "D7", false, "Arial", 8, false, false);
                    //}

                    //PhraseHeader phraseHeader = dal.GetPhraseByName("Location folders");
                    //var firstOrDefault = phraseHeader.PhraseEntries.FirstOrDefault(pe => pe.PhraseDescription == "excel pic");
                    string save = path;

                    if (File.Exists(save))
                    {
                        AddPictureToFile(save, 200, 0, 150, 50);
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("ExportToExcel: \n" + ex.Message);
                }
            }
            


        }
        public void SaveFile(string excelFilePath)
        {
            if (!string.IsNullOrEmpty(excelFilePath))
            {
                try
                {
                    WorkSheet.SaveAs(excelFilePath);
                    ExcelApp.Quit();
                   // MessageBox.Show("Excel file saved!");
                }
                catch (Exception ex)
                {
                    throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
                                        + ex.Message);
                }
            }
        }
        public void Close ()
        {
            Marshal.ReleaseComObject(ExcelApp);
        }
        public void AddFiledToSpecificPlace(int cell,int row,string text)
        {
            WorkSheet.Cells[row, cell] = text;
        }
        public void AddPictureToFile(string picPath,float left,float top,float width,float high )
        {
            WorkSheet.Shapes.AddPicture(picPath, MsoTriState.msoFalse, MsoTriState.msoCTrue, left, top, width, high);
        }
        public void SetStyleToCell(string fromCell, string toCell, bool bold, string fontName, int size, bool italic, bool underline, XlHAlign centerText=XlHAlign.xlHAlignGeneral)
        {
            WorkSheet.Range[fromCell,toCell].Font.Bold = bold;
            WorkSheet.Range[fromCell,toCell].Font.Name = fontName;
            WorkSheet.Range[fromCell,toCell].Font.Size = size;
            WorkSheet.Range[fromCell,toCell].Font.Italic = italic;
            WorkSheet.Range[fromCell,toCell].Font.Underline = underline;
          //  if (centerText)
          //  {
            WorkSheet.Range[fromCell, toCell].HorizontalAlignment = centerText;
          //  }
        }
        public void AutoFit()
        {
            var selectedRange = ExcelApp.Range["A1", "N30"];
            selectedRange.Columns.AutoFit();
        }
    }
}
