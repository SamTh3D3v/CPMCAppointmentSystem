using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace CPMCAppointmentSystem.Helpers
{
    public static class ApplicationSettings
    {
        #region Const
        private const string FileName = "Settings.xml";
        #endregion
        #region Fields

        #endregion

        #region Properties
        public static ChosenTheme ChosenTheme { get; private set; }
        public static string SettingsFilePath
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    FileName);
            }
        }
        public static XDocument SettingsXmlDocument
        {
            get;
            private set;
        }
        #endregion
        #region Ctors and Methods
        static ApplicationSettings()
        {
            if (IsSettingsFileExisting())
            {
                var isSettingsFileValide = false;
                try
                {
                    SettingsXmlDocument = XDocument.Load(SettingsFilePath);
                    isSettingsFileValide = IsSettingsFileValide();
                }
                catch (XmlException xmlException)
                {
                    isSettingsFileValide = false;
                }
                if (!isSettingsFileValide)
                {
                    CreateSettingsFile();
                }
                else
                {
                    if (SettingsXmlDocument.Descendants().First(element => element.Name == "Theme").Value == "FirstTheme")
                    {
                        ChosenTheme = ChosenTheme.FirstTheme;
                    }
                    else
                    {
                        if (SettingsXmlDocument.Descendants().First(element => element.Name == "Theme").Value == "SecondTheme")
                        {
                            ChosenTheme = ChosenTheme.SecondTheme;
                        }
                    }
                }
            }
            else
            {
                CreateSettingsFile();
                UpdateTheme(ChosenTheme.FirstTheme);
            }

        }
        private static bool IsSettingsFileExisting()
        {
            return System.IO.File.Exists(SettingsFilePath);
        }
        private static bool IsSettingsFileValide()
        {
            var schemas = new XmlSchemaSet();
            var reader = XmlReader.Create(@".\SettingsFileSchema.xsd");
            using (reader)
            {
                var myschema = XmlSchema.Read(reader, (sender, eventArgs) =>
                {
                    if (eventArgs.Severity == XmlSeverityType.Warning)
                        Debug.WriteLine("WARNING: ");
                    else if (eventArgs.Severity == XmlSeverityType.Error)
                        Debug.WriteLine("ERROR: ");

                });
                schemas.Add(myschema);
            }

            var msg = "";
            SettingsXmlDocument.Validate(schemas, (sender, eventArgs) => msg += eventArgs.Message);
            return msg == "";
        }
        private static void CreateSettingsFile()
        {
            SettingsXmlDocument = new XDocument(new XElement("Root", new XElement("Settings")));
            var themeXElement = new XElement("Theme");
            SettingsXmlDocument.Descendants().First(element => element.Name == "Settings").Add(themeXElement);

            SaveXDocumentToFile();
        }
        public static void UpdateTheme(ChosenTheme theme)
        {
            ChosenTheme = theme;
            SettingsXmlDocument.Descendants().First(element => element.Name == "Theme").SetValue(theme);
            SaveXDocumentToFile();
        }
        private static void SaveXDocumentToFile()
        {
            var xmlStreamWriter = new XmlTextWriter(SettingsFilePath, Encoding.UTF8);
            using (xmlStreamWriter)
            {
                SettingsXmlDocument.Save(xmlStreamWriter);
            }
        }
        #endregion
    }

    public enum ChosenTheme
    {
        FirstTheme,
        SecondTheme
    }

}
