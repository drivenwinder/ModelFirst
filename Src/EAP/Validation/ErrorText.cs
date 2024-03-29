﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EAP.Validation
{
    [Serializable]
    public class ErrorTextCollection : List<ErrorText>
    {
        public override string ToString()
        {
            StringBuilder text = new StringBuilder();
            foreach (ErrorText t in this)
            {
                text.Append(t);
                text.Append(",");
            }
            if (text.Length > 0)
                text.Remove(text.Length - 1, 1);
            return text.ToString();
        }
    }

    /// <summary>
    /// 错误文本
    /// </summary>
    [Serializable]
    public class ErrorText
    {
        public ErrorText() { Args = new object[0]; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="res">资源键</param>
        /// <param name="text">找不到键对应的资源时显示的文本</param>
        /// <param name="args">格式化参数</param>
        public ErrorText(string res, string text, params object[] args)
        {
            ResKey = res;
            Text = text;
            Args = args;
        }

        /// <summary>
        /// 资源键
        /// </summary>
        public string ResKey { get; set; }

        /// <summary>
        /// 找不到键对应的资源时显示的文本
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 格式化参数
        /// </summary>
        public object[] Args { get; set; }

        public override string ToString()
        {
            return Text.FormatArgs(Args);
        }

        #region ErrorText

        /// <summary>
        /// Is required
        /// </summary>
        public static ErrorText Require = new ErrorText("Require", "is required");
        /// <summary>
        /// Is invalid
        /// </summary>
        public static ErrorText Invalid = new ErrorText("Invalid", "is invalid");
        /// <summary>
        /// Exists
        /// </summary>
        public static ErrorText Exists = new ErrorText("Exists", "already exists");
        /// <summary>
        /// Is not found
        /// </summary>
        public static ErrorText NotFound = new ErrorText("NotFound", "is not found");
        /// <summary>
        /// Maximum length is {0}, current is {1}
        /// </summary>
        public static ErrorText MaxLength(int max, int current)
        {
            return new ErrorText("MaxLength", "maximum length is {0}, current is {1}", max, current);
        }
        /// <summary>
        /// Minimum length is {0}, current is {1}
        /// </summary>
        public static ErrorText MinLength(int min, int current)
        {
            return new ErrorText("MinLength", "minimum length is {0}, current is {1}", min, current);
        }
        /// <summary>
        /// Maximum value is {0}, current is {1}
        /// </summary>
        public static ErrorText MaxValue(object max, object current)
        {
            return new ErrorText("MaxValue", "maximum value is {0}, current is {1}", max, current);
        }
        /// <summary>
        /// Minimum value is {0}, current is {1}
        /// </summary>
        public static ErrorText MinValue(object min, object current)
        {
            return new ErrorText("MinValue", "minimum value is {0}, current is {1}", min, current);
        }
        /// <summary>
        /// invalid scope
        /// </summary>
        public static ErrorText InvalidScope = new ErrorText("InvalidScope", "invalid scope");

        #endregion
    }
}
