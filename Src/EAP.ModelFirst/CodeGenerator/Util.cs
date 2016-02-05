using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Core.Project.Members;
using System.Data;

namespace EAP.ModelFirst.CodeGenerator
{
    public static class Util
    {
        public static string ToPascal(string name)
        {
            string result = name.Replace(" ", "").Replace(".", "_");
            if (result.IndexOf('_') > 0)
            {
                char[] arr = result.ToCharArray();
                if (char.IsLower(arr[0]))
                    arr[0] = char.ToUpper(arr[0]);
                for (int i = 1; i < arr.Length; i++)
                {
                    if (arr[i] == '_' && i + 1 < arr.Length)
                    {
                        i++;
                        if (char.IsLower(arr[i]))
                            arr[i] = char.ToUpper(arr[i]);

                    }
                    else
                    {
                        if (char.IsUpper(arr[i]))
                            arr[i] = char.ToLower(arr[i]);
                    }
                }
                result = new string(arr).Replace("_", "");
            }
            else
            {
                char[] arr = result.ToCharArray();
                bool hasLower = false;
                bool hasUpper = false;
                for (int i = 0; i < arr.Length; i++)
                {
                    if (char.IsLower(arr[i]))
                    {
                        hasLower = true;
                        break;
                    }
                }
                for (int i = 0; i < arr.Length; i++)
                {
                    if (char.IsUpper(arr[i]))
                    {
                        hasUpper = true;
                        break;
                    }
                }
                if (!hasLower)
                {
                    for (int i = 1; i < arr.Length; i++)
                    {
                        arr[i] = char.ToLower(arr[i]);
                    }
                    result = new string(arr);
                }
                if (!hasUpper)
                {
                    arr[0] = char.ToUpper(arr[0]);
                    result = new string(arr);
                }
            }
            return result;
        }

        public static string GetTableName(this SingleInharitanceType t)
        {
            if (t is ClassType)
            {
                var type = t as ClassType;
                if (type.InheritanceStrategy == InheritanceStrategy.Subclass)
                    return type.GetBaseRoot().TableName;
            }
            return t.TableName;
        }

        public static Field FindField(this SingleInharitanceType t, string field)
        {
            foreach (var f in t.Fields)
                if (f.Name == field)
                    return f;
            return null;
        }

        public static DbType GetDbType(string typeName)
        {
            TypeCode c;
            if (Enum.TryParse(typeName, true, out c))
            {
                switch (c)
                {
                    case TypeCode.Boolean:
                        return DbType.Boolean;
                    case TypeCode.Byte:
                        return DbType.Byte;
                    case TypeCode.DateTime:
                        return DbType.DateTime;
                    case TypeCode.Double:
                        return DbType.Double;
                    case TypeCode.Int16:
                        return DbType.Int16;
                    case TypeCode.Int32:
                        return DbType.Int32;
                    case TypeCode.Int64:
                        return DbType.Int64;
                    case TypeCode.SByte:
                        return DbType.SByte;
                    case TypeCode.Single:
                        return DbType.Single;
                    case TypeCode.String:
                        return DbType.String;
                    case TypeCode.Char:
                    case TypeCode.UInt16:
                        return DbType.UInt16;
                    case TypeCode.UInt32:
                        return DbType.UInt32;
                    case TypeCode.UInt64:
                        return DbType.UInt64;
                    case TypeCode.Decimal:
                        return DbType.Decimal;
                    case TypeCode.DBNull:
                    case TypeCode.Empty:
                    case TypeCode.Object:
                        return DbType.Object;
                }
            }
            switch (typeName)
            {
                case "int":
                    return DbType.Int32;
                case "short":
                    return DbType.Int16;
                case "long":
                    return DbType.Int64;
                case "bool":
                    return DbType.Boolean;
            }
            return DbType.AnsiString;
        }
    }
}
