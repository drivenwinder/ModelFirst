using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace EAP.Utils
{
    public static class ExcelHelper
    {
        /// <summary>
        /// 从Stream中读取数据到DataSet
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="stratRowIndex"></param>
        /// <param name="startColumnIndex"></param>
        /// <returns></returns>
        public static DataSet Read(Stream stream, int stratRowIndex = 1, int startColumnIndex = 0)
        {
            DataSet ds = new DataSet();

            HSSFWorkbook book = new HSSFWorkbook(stream);
            // Read Data
            // 连续3行为空，将终止读取
            for (int i = 0; i < book.NumberOfSheets; i++)
            {
                ISheet workSheet = book.GetSheetAt(i);
                DataTable dt = ReadSheet(workSheet, stratRowIndex, startColumnIndex);
                ds.Tables.Add(dt);
            }
            return ds;
        }

        /// <summary>
        /// 把DataTable的数据写入Stream.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="dt"></param>
        public static void Write(Stream stream, DataTable dt)
        {
            HSSFWorkbook book = new HSSFWorkbook();
            string sheetName = dt.TableName.IsNotEmpty() ? dt.TableName : "sheet1";
            ISheet sheet = book.CreateSheet(sheetName);
            Write(sheet, dt);
            book.Write(stream);
        }

        /// <summary>
        /// 按模板把DataTable的数据写入Stream.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="dt"></param>
        /// <param name="template"></param>
        public static void Write(Stream stream, DataTable dt, string template)
        {
            using (Stream fileStream = File.OpenRead(template))
            {
                HSSFWorkbook book = new HSSFWorkbook(fileStream);
                ISheet sheet = book.GetSheetAt(0);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    IRow row = InsertRow(sheet, i + 1);
                    Write(row, dr);
                }
                book.Write(stream);
            }
        }

        /// <summary>
        /// 把DataSet的数据写入Stream.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="ds"></param>
        public static void Write(Stream stream, DataSet ds)
        {
            HSSFWorkbook book = new HSSFWorkbook();
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                DataTable dt = ds.Tables[i];
                string sheetName = dt.TableName.IsNotEmpty() ? dt.TableName : "sheet" + (i + 1);
                ISheet sheet = book.CreateSheet(sheetName);
                Write(sheet, dt);
                book.Write(stream);
            }
        }

        /// <summary>
        /// 按模板把DataSet的数据写入Stream.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="ds"></param>
        /// <param name="template"></param>
        public static void Write(Stream stream, DataSet ds, string template)
        {
            using (Stream fileStream = File.OpenRead(template))
            {
                HSSFWorkbook book = new HSSFWorkbook(fileStream);
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    DataTable dt = ds.Tables[i];
                    ISheet sheet = book.GetSheetAt(i);
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        DataRow dr = dt.Rows[j];
                        IRow row = InsertRow(sheet, j + 1);
                        Write(row, dr);
                    }
                }
                book.Write(stream);
            }
        }

        static DataTable ReadSheet(ISheet workSheet, int startRowIndex = 1, int startColumnIndex = 0)
        {
            int columnIndexClone = startColumnIndex;
            int rowIndexClone = startRowIndex;
            DataTable dt = new DataTable(workSheet.SheetName);

            #region Add Column to DataTable

            int blankHeadCol = 0;
            if (rowIndexClone > 0)
            {
                do
                {
                    IRow sheetHeaderRow = workSheet.GetRow(rowIndexClone - 1);
                    if (rowIndexClone > 0 && sheetHeaderRow != null)
                    {
                        ICell dataColumnCell = sheetHeaderRow.GetCell(dt.Columns.Count);
                        string colName = GetCellString(dataColumnCell);
                        if (!string.IsNullOrWhiteSpace(colName))
                        {
                            string temp = GetCellString(dataColumnCell);
                            dt.Columns.Add(GetColumnName(dt.Columns, temp));
                        }
                        else
                        {
                            dt.Columns.Add("Column" + dt.Columns.Count);
                            blankHeadCol++;
                        }
                    }
                    else
                    {
                        blankHeadCol++;
                    }
                } while (blankHeadCol < 5);

                int tem = dt.Columns.Count - 1;
                for (int i = tem; i >= tem - 4; i--)
                {
                    if (dt.Columns[i].ColumnName.ToUpper().StartsWith("COLUMN"))
                        dt.Columns.RemoveAt(i);
                }
            }


            #endregion

            int blankRow = 0;
            while (blankRow < 3)
            {
                #region 过滤空行

                // 过滤空行
                IRow sheetRow = workSheet.GetRow(startRowIndex);
                startRowIndex++;
                if (sheetRow == null)
                {
                    blankRow++;
                    continue;
                }
                else
                {
                    bool blankRowCol = true;

                    for (int rowColIndex = 0; rowColIndex < dt.Columns.Count + 10; rowColIndex++)
                    {
                        if (GetCellString(GetCell(sheetRow, rowColIndex)).IsNotEmpty())
                        {
                            blankRowCol = false;
                            break;
                        }
                    }
                    if (blankRowCol)
                    {
                        blankRow++;
                        continue;
                    }
                }
                #endregion

                // 读取数据
                DataRow dataRow = dt.NewRow();

                int blankCol = 0;
                int colIndex = 0;
                startColumnIndex = columnIndexClone;

                while (blankCol < 10)
                {
                    ICell dataCell = sheetRow.GetCell(startColumnIndex);
                    startColumnIndex++;
                    colIndex++;

                    if (GetCellString(dataCell).IsNullOrWhiteSpace())
                    {
                        blankCol++;
                        continue;
                    }

                    #region Add Column to DataTable
                    while (dt.Columns.Count < colIndex)
                    {
                        IRow sheetHeaderRow = workSheet.GetRow(rowIndexClone - 1);
                        if (rowIndexClone > 0 && sheetHeaderRow != null)
                        {
                            ICell dataColumnCell = sheetHeaderRow.GetCell(dt.Columns.Count);

                            string colName = GetCellString(dataColumnCell);
                            if (!string.IsNullOrWhiteSpace(colName))
                                dt.Columns.Add(GetCellString(dataColumnCell));
                            else
                                dt.Columns.Add("Column" + dt.Columns.Count);

                        }
                        else
                            dt.Columns.Add("F" + dt.Columns.Count);
                    }
                    #endregion

                    dataRow[colIndex - 1] = GetCellString(dataCell);

                    blankCol = 0;
                }

                dt.Rows.Add(dataRow);
                blankRow = 0;

            }
            return dt;
        }

        static IRow InsertRow(ISheet sheet, int index)
        {
            if (sheet.LastRowNum > index)
                sheet.ShiftRows(index, sheet.LastRowNum, 1, true, false);
            IRow row = sheet.GetRow(index);
            if (row == null)
                row = sheet.CreateRow(index);
            return row;
        }

        static void Write(ISheet sheet, DataTable dt)
        {
            for (int rowIndex = 0; rowIndex < dt.Rows.Count; rowIndex++)
            {
                DataRow dr = dt.Rows[rowIndex];
                IRow row = GetRow(sheet, rowIndex);
                Write(row, dr);
            }
        }

        static void Write(IRow row, DataRow dr)
        {
            for (int i = 0; i < dr.ItemArray.Length; i++)
            {
                ICell cell = GetCell(row, i);
                string value = dr[i].ToSafeString();
                if (value.IsNotEmpty())
                {
                    cell.SetCellValue(dr[i]);
                    cell.CellStyle.VerticalAlignment = VerticalAlignment.CENTER;
                }
            }
        }

        static String GetColumnName(DataColumnCollection cols, string colName, int deep = 0)
        {
            string tem = colName;
            tem = colName + (deep == 0 ? "" : deep.ToString());
            if (cols.Contains(tem))
            {
                return GetColumnName(cols, colName, ++deep);
            }
            return tem;
        }

        static string GetCellString(ICell cell)
        {
            if (cell == null)
                return String.Empty;

            switch (cell.CellType)
            {
                case CellType.STRING:
                    return cell.StringCellValue;
                case CellType.NUMERIC:
                    return cell.NumericCellValue.ToSafeString();
                case CellType.BOOLEAN:
                    return cell.BooleanCellValue.ToSafeString();
                case CellType.FORMULA:
                    if (cell.CachedFormulaResultType == CellType.NUMERIC)
                        return cell.NumericCellValue.ToSafeString();
                    return cell.StringCellValue;
                default:
                    return cell.ToSafeString();
            }
        }

        public static IRow GetRow(ISheet workSheet, int rowIndex)
        {
            IRow sheetRow = workSheet.GetRow(rowIndex);
            if (sheetRow == null)
                sheetRow = workSheet.CreateRow(rowIndex);
            return sheetRow;
        }

        public static ICell GetCell(IRow sheetRow, int colIndex)
        {
            ICell cell = sheetRow.GetCell(colIndex);
            if (cell == null)
                cell = sheetRow.CreateCell(colIndex);
            return cell;
        }

        public static void SetCellValue(this ICell cell, object value)
        {
            if (value is bool)
                cell.SetCellValue((bool)value);
            else if (value is DateTime)
                cell.SetCellValue((DateTime)value);
            else if (IsNumber(value))
                cell.SetCellValue(Convert.ToDouble(value));
            else
                cell.SetCellValue(value.ToSafeString());
        }

        static bool IsNumber(object value)
        {
            return value is double
                || value is float
                || value is int
                || value is short
                || value is long
                || value is decimal
                || value is byte;
        }
    }
}
