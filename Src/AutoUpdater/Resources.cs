using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace AutoUpdate
{
    public class Resources
    {
        public static string Run
        {
            get { return ConfigurationManager.AppSettings["L10N.Run"]; }
        }
        public static string Cancel
        {
            get { return ConfigurationManager.AppSettings["L10N.Cancel"]; }
        }
        public static string CheckingUpdate
        {
            get { return ConfigurationManager.AppSettings["L10N.CheckingUpdate"]; }
        }
        public static string CopyingFiles
        {
            get { return ConfigurationManager.AppSettings["L10N.CopyingFiles"]; }
        }
        public static string DeleteFileFailure
        {
            get { return ConfigurationManager.AppSettings["L10N.DeleteFileFailure"]; }
        }
        public static string DeletingTempFiles
        {
            get { return ConfigurationManager.AppSettings["L10N.DeletingTempFiles"]; }
        }
        public static string Downloading
        {
            get { return ConfigurationManager.AppSettings["L10N.Downloading"]; }
        }
        public static string ExtractedFile
        {
            get { return ConfigurationManager.AppSettings["L10N.ExtractedFile"]; }
        }
        public static string KillProcessConfirmation
        {
            get { return ConfigurationManager.AppSettings["L10N.KillProcessConfirmation"]; }
        }
        public static string Updating
        {
            get { return ConfigurationManager.AppSettings["L10N.Updating"]; }
        }
    }
}
