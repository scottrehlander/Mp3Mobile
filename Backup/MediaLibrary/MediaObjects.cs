using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using HundredMilesSoftware.UltraID3Lib;

namespace MediaMobile.MediaLibrary
{
    /// <summary>
    /// This class is the base class for all types of media that can be streamed
    /// through MediaMobile.
    /// </summary>
    public abstract class MediaObject
    {
    }

    public class Mp3 : MediaObject
    {
        private UltraID3 ultraID3;
        private string problemString = "";

        public string TrackTitle { get { return ultraID3.Title; } }
        public short TrackNumber { get { return ultraID3.TrackNum.Value; } }
        public string Artist { get { return ultraID3.Artist; } }
        public string Album { get { return ultraID3.Album; } }
        public string Genre { get { return ultraID3.Genre; } }
        public string FileName { get { return ultraID3.FileName; } }
        public string Comments { get { return ultraID3.Comments; } }
        public TimeSpan Duration { get { return ultraID3.Duration; } }
        public long Size { get { return ultraID3.Size.Value; } }
        public short Year { get { return ultraID3.Year.Value; } }
        public string ProblemString { get { return problemString; } set { problemString = value; } }
        

        public Mp3(string fileName)
        {
            ultraID3 = new UltraID3();

            ExtractMp3Info(fileName);
        }

        private void ExtractMp3Info(string fileName)
        {
            ultraID3.Read(fileName);
        }

        public void SetArtist(string newArtist)
        {
            ultraID3.Artist = newArtist;
        }
    }
}