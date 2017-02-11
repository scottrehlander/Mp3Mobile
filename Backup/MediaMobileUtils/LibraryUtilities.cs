using System;
using System.Collections.Generic;
using System.Text;

namespace MediaMobile.MediaMobileUtils
{
    public static class LibraryUtilities
    {
        private static string libraryFilename = "Library.wii";
        public static string LibraryFilename { get { return libraryFilename; } }

        public static bool EndsWithKnownExtension(string fileName)
        {
            foreach (string extension in MediaMobileConfig.KnownExtensions.Keys)
                if (fileName.EndsWith(extension))
                    return true;

            return false;
        }

        public static string GetLibraryPlaylistLocation()
        {
            if (Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"Software\SRT\Wii3P0\") == null)
                Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"Software\SRT\Wii3P0");
            
            Microsoft.Win32.RegistryKey currentOrderKey =
                Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"Software\SRT\Wii3P0", true);

            if(currentOrderKey.GetValue("LibraryPlaylist", "").ToString().Equals(""))
                currentOrderKey.SetValue("LibraryPlaylist", @"C:\Program Files\SRT\Wii3P0" + "\\" + libraryFilename,
                Microsoft.Win32.RegistryValueKind.String);

            return currentOrderKey.GetValue("LibraryPlaylist", "").ToString();
        }

        public static string GetPlaylistFileExtension()
        {
            return ".wi3";
        }
    }
}
