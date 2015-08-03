using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestProject
{
    public partial class WebFormTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //DatatableToExcel();
            ExcelToDataTable();

        }


        private void DownloadExcel()
        {


            SpreadsheetGear.IWorkbook workbook = SpreadsheetGear.Factory.GetWorkbook();
            SpreadsheetGear.IWorksheet worksheet = workbook.Worksheets["Sheet1"];
            SpreadsheetGear.IRange cells = worksheet.Cells;

            // Set the worksheet name.
            worksheet.Name = "2005 Sales";

            // Load column titles and center.
            cells["B1"].Formula = "North";
            cells["C1"].Formula = "South";
            cells["D1"].Formula = "East";
            cells["E1"].Formula = "West";
            cells["B1:E1"].HorizontalAlignment = SpreadsheetGear.HAlign.Right;

            // Load row titles using multiple cell text reference and iteration.
            int quarter = 1;
            foreach (SpreadsheetGear.IRange cell in cells["A2:A5"])
                cell.Formula = "Q" + quarter++;

            // Load random data and format as $ using a multiple cell range.

            SpreadsheetGear.IRange body = cells[1, 1, 4, 4];

            body.Formula = "=RAND() * 10000";
            body.NumberFormat = "$#,##0_);($#,##0)";

            cells[0, 0].Value = "abc";


            // Stream the Excel spreadsheet to the client in a format
            // compatible with Excel 97/2000/XP/2003/2007/2010.
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment; filename=report4.xls");
            workbook.SaveToStream(Response.OutputStream, SpreadsheetGear.FileFormat.Excel8);
            Response.End();
        }


        private void ExcelToDataTable()
        {

            String ssFile = Server.MapPath("files/spiceorder.xls");
            SpreadsheetGear.IWorkbook workbook = SpreadsheetGear.Factory.GetWorkbook(ssFile);

            


            // Get a DataSet from an existing defined name
            DataSet dataSet = workbook.GetDataSet("orderrange", SpreadsheetGear.Data.GetDataFlags.FormattedText);
            
        }



        private void DatatableToExcel()
        {
            // any DataSet such as one returned from a database query.
            String xmlfile = Server.MapPath("files/spiceorder.xml");
            System.Data.DataSet dataset = new System.Data.DataSet();
            
            dataset.ReadXml(xmlfile);
            System.Data.DataTable datatable = dataset.Tables["OrderItems"];

            // Create a new workbook and worksheet.
            SpreadsheetGear.IWorkbook workbook = SpreadsheetGear.Factory.GetWorkbook();
            SpreadsheetGear.IWorksheet worksheet = workbook.Worksheets["Sheet1"];
            worksheet.Name = "Spice Order";

            // Get the top left cell for the DataTable.
            SpreadsheetGear.IRange range = worksheet.Cells["A1"];

            // Copy the DataTable to the worksheet range.
            range.CopyFromDataTable(datatable, SpreadsheetGear.Data.SetDataFlags.None);

            // Auto size all worksheet columns which contain data
            worksheet.UsedRange.Columns.AutoFit();

            // Stream the Excel spreadsheet to the client in a format
            // compatible with Excel 97/2000/XP/2003/2007/2010.
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment; filename=report.xls");
            workbook.SaveToStream(Response.OutputStream, SpreadsheetGear.FileFormat.Excel8);
            Response.End();


        }
    }
}