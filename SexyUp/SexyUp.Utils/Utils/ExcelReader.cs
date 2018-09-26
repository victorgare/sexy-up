using System.Data;
using System.IO;
using ExcelDataReader;

namespace SexyUp.Utils.Utils
{
    public static class ExcelReader
    {
        public static DataTable ConvertExcelToDataTable(string filePath)
        {
            var dataTable = new DataTable();
            var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            var excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            var result = excelReader.AsDataSet();


            var rows = result.Tables[0].Rows;

            for (var i = 0; i < rows.Count; i++)
            {
                var dataRow = dataTable.NewRow();

                for (var j = 0; j < rows[i].ItemArray.Length; j++)
                {
                    if (i == 0)
                    {
                        dataTable.Columns.Add(rows[i].ItemArray[j].ToString());
                    }
                    else
                    {
                        dataRow[j] = rows[i].ItemArray[j];
                    }
                }

                if (i != 0)
                {
                    dataTable.Rows.Add(dataRow);
                }
            }

            excelReader.Close();

            return dataTable;
        }
    }
}
