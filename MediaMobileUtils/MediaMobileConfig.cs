using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace MediaMobile.MediaMobileUtils
{
    /// <summary>
    /// This class loads and stores all configuration information everytime the
    /// software starts.
    /// </summary>
    public static class MediaMobileConfig
    {
        private static readonly string CONFIG_FILE_LOC = @"c:\!mypage\Mp3Mobile\MediaMobileUtils\bin\Debug\mmconfig.xml";

        private static SortedList<string, string> knownExtensions = new SortedList<string, string>();

        /// <summary>
        /// This is a collection of known media extensions (mp3, avi, mpg, etc.)
        /// The Key is the extension.
        /// The Value is a description of the file type that it denotes.
        /// </summary>
        public static SortedList<string, string> KnownExtensions { get { return knownExtensions; } }

        static MediaMobileConfig()
        {
            LoadConfiguration();
        }

        private static void LoadConfiguration()
        {
            XmlDocument configDoc = new XmlDocument();
            configDoc.Load(CONFIG_FILE_LOC);

            XmlNode rootNode = configDoc.SelectSingleNode("MediaMobileConfig");

            LoadKnownFileExtensions(rootNode);

        }

        private static void LoadKnownFileExtensions(XmlNode rootNode)
        {
            XmlNodeList knownExtensionsList = rootNode.SelectNodes("KnownExtensionsList/KnownExtension");
            if (knownExtensions == null) return;

            foreach (XmlNode knownExtension in knownExtensionsList)
            {
                string extension = knownExtension.SelectSingleNode("Extension").InnerText;
                string description = knownExtension.SelectSingleNode("MediaDescription").InnerText;

                knownExtensions.Add(extension, description);
            }
        }

    }
}
