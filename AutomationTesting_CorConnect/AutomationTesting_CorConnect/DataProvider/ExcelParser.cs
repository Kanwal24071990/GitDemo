using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using NLog;

namespace AutomationTesting_CorConnect.DataProvider
{
    internal class ExcelParser
    {
        internal Data Read(string FileName, Logger logger)
        {
            var testInputData = new Data();
            testInputData.TestData = new Dictionary<string, Dictionary<string, List<Dictionary<string, dynamic>>>>();
            string connectionString = string.Empty;

            string fileExtension = Path.GetExtension(FileName);
            logger.Debug(string.Format(LoggerMesages.ExcelFileExtension, fileExtension));

            if (fileExtension == ".xls")
            {
                connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileName + ";" + "Extended Properties='Excel 8.0;HDR=NO;'";
            }
            else if (fileExtension == ".xlsx")
            {
                connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";" + "Extended Properties='Excel 12.0 Xml;IMEX=1;HDR=NO;'";
            }

            using (OleDbConnection connExcel = new(connectionString))
            {
                DataTable dt = new();

                OleDbCommand cmdExcel = new()
                {
                    Connection = connExcel
                };

                logger.Debug(LoggerMesages.OpenningExcelconnection);
                connExcel.Open();

                DataTable dtExcelSchema;

                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                logger.Debug(LoggerMesages.ClosingExcelConnection);
                connExcel.Close();

                var Sheets = dtExcelSchema.Rows;
                try
                {
                    for (int i = 0; i < dtExcelSchema.Rows.Count; i++)
                    {
                        var TestDataInput = new Dictionary<string, List<Dictionary<string, dynamic>>>();
                        string sheetName = dtExcelSchema.Rows[i]["TABLE_NAME"].ToString();
                        logger.Debug(string.Format(LoggerMesages.ReadingSheet, sheetName));

                        cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";

                        OleDbDataAdapter da = new()
                        {
                            SelectCommand = cmdExcel
                        };

                        DataSet ds = new();

                        da.Fill(ds);

                        var dataTable = ds.Tables[0];
                        var rowCount = dataTable.Rows.Count;
                        var firstRow = dataTable.Rows[0];
                        var header = new List<string>();

                        for (int j = 0; j < dataTable.Columns.Count; j++)
                        {
                            header.Add(firstRow[j].ToString());
                        }

                        var DataRoww = new List<List<dynamic>>();
                        List<dynamic> data;

                        for (int k = 1; k < rowCount; k++)
                        {
                            data = new List<dynamic>();

                            for (int j = 0; j < dataTable.Columns.Count; j++)
                            {
                                data.Add(ds.Tables[0].Rows[k][j]);
                            }

                            DataRoww.Add(data);
                        }


                        foreach (var row in DataRoww)
                        {
                            logger.Debug(string.Format(LoggerMesages.AddingDataForTestCase, row[2]));

                            if (TestDataInput.ContainsKey(row[2]))
                            {
                                TestDataInput[row[2]].AddRange(new List<Dictionary<string, dynamic>>() { GetObject(header, row) });
                            }

                            else
                            {
                                TestDataInput.Add(row[2], new List<Dictionary<string, dynamic>>() { GetObject(header, row) });
                            }
                        }

                        testInputData.TestData.Add(sheetName.Trim('\'').TrimEnd('$').Replace(" ", "").Replace("-", ""), TestDataInput);
                    }
                }
                catch (Exception e)
                {
                    logger.Debug(e.Message);
                    return null;
                }

            }

            return testInputData;
        }

        internal DataTable ReadData(string FileName, string sheetName)
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";" + "Extended Properties='Excel 12.0 Xml;IMEX=1;HDR=NO;'";

            using (OleDbConnection connExcel = new(connectionString))
            {
                DataTable dt = new();

                OleDbCommand cmdExcel = new()
                {
                    Connection = connExcel
                };

                connExcel.Open();

                DataTable dtExcelSchema;

                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                var sheets = dtExcelSchema.AsEnumerable().Where(s => s.Field<string>("Table_Name").EndsWith("$")).Select(r => r.Field<string>("Table_Name")).ToList();

                connExcel.Close();

                try
                {

                    cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";

                    OleDbDataAdapter da = new()
                    {
                        SelectCommand = cmdExcel
                    };

                    DataSet ds = new();

                    da.Fill(ds);

                    return ds.Tables[0];
                }
                catch (Exception e)
                {
                }
            }
            return null;

        }

        private Dictionary<string, dynamic> GetObject(List<string> header, List<dynamic> data)
        {
            var dictionary = new Dictionary<string, dynamic>();

            for (int i = 0; i < header.Count; i++)
            {
                if (!string.IsNullOrWhiteSpace(header[i]))
                {
                    dictionary.Add(header[i], data[i]);
                }
            }

            return dictionary;
        }

        internal Dictionary<string, List<string>> GetPageHeadersDictionaryByClient(string fileName, string clientName)
        {
            Dictionary<string, List<string>> pageHeadersDic = new Dictionary<string, List<string>>();
            DataTable dataTable = ReadData(fileName, clientName + "$");
            if (dataTable != null)
            {
                int rowCount = dataTable.Rows.Count;
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    List<string> headers = new List<string>();
                    for (int j = 1; j < rowCount; j++)
                    {
                        headers.Add(dataTable.Rows[j][i].ToString().Trim());
                    }
                    headers = headers.Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
                    string columnName = dataTable.Rows[0][i].ToString();
                    if (!string.IsNullOrEmpty(columnName))
                    {
                        pageHeadersDic.Add(CommonUtils.RemoveNonAlphanumericChars(columnName).ToLower(), headers);
                    }
                }
            }
            return pageHeadersDic;
        }
    }
}
