using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Excel;

namespace AutomationFramework.UITests.Framework.Helpers
{
    public static class DataColletionListExtensions
    {
        public static string ReadData(this List<Datacollection> list, int rowNumber, string columnName)
        {
            //Retriving Data using LINQ to reduce much of iterations
            var data = (from colData in list
                where colData.ColName == columnName && colData.RowNumber == rowNumber
                select colData.ColValue).SingleOrDefault();
            //var datas = dataCol.Where(x => x.colName == columnName && x.rowNumber == rowNumber).SingleOrDefault().colValue;
            return data;
        }
    }

    public class ExcelHelpers
    {
        /// <summary>
        ///     Storing all the excel values in to the in-memory collections
        /// </summary>
        /// <param name="fileName"></param>
        public static List<Datacollection> PopulateInCollection(string fileName)
        {
            var dataCollectionList = new List<Datacollection>();
            var table = ExcelToDataTable(fileName);

            //Iterate through the rows and columns of the Table
            for (var row = 1; row <= table.Rows.Count; row++)
            for (var col = 0; col < table.Columns.Count; col++)
            {
                var dtTable = new Datacollection
                {
                    RowNumber = row,
                    ColName = table.Columns[col].ColumnName,
                    ColValue = table.Rows[row - 1][col].ToString()
                };
                //Add all the details for each row
                dataCollectionList.Add(dtTable);
            }
            return dataCollectionList;
        }

        /// <summary>
        ///     Reading all the datas from Excelsheet
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static DataTable ExcelToDataTable(string fileName)
        {
            //open file and returns as Stream
            var stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
            //Createopenxmlreader via ExcelReaderFactory
            var excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream); //.xlsx
            //Set the First Row as Column Name
            excelReader.IsFirstRowAsColumnNames = true;
            //Return as DataSet
            var result = excelReader.AsDataSet();
            //Get all the Tables
            var table = result.Tables;
            //Store it in DataTable
            var resultTable = table["Blad1"];
            //return
            return resultTable;
        }
    }
}