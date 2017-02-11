using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MediaMobile.MediaLibrary
{
    /// <summary>
    /// This class holds a collection of MediaObjects and all neccessary
    /// information to effectively play any collection of media.
    /// </summary>
    public class MediaPlaylist
    {
        string name;
        string filename;
        List<MediaObject> mediaList = new List<MediaObject>();
        List<MediaPlaylist> embeddedLists = new List<MediaPlaylist>();

        public List<MediaObject> MediaList { get { return mediaList; } }
        public List<MediaPlaylist> EmbeddedLists { get { return embeddedLists; } }
        public string Name { get { return name; } set { name = value; } }
        public string Filename { get { return filename; } set { filename = value; } }

        public MediaPlaylist(string playlistPath, string playlistName)
        {
            if (File.Exists(playlistPath + "\\" + playlistName + MediaMobileUtils.LibraryUtilities.GetPlaylistFileExtension()))
                throw new Exception("Cannot overwrite existing playlist.  Use alternate constructor to load existing playlists.");

            filename = playlistPath + "\\" + playlistName + MediaMobileUtils.LibraryUtilities.GetPlaylistFileExtension();
            name = playlistName;

            if (!Directory.Exists(playlistPath))
                Directory.CreateDirectory(playlistPath);

            using (StreamWriter sw = new StreamWriter(filename))
            {
                PlaylistElement newEmptyPlaylist = new PlaylistElement();
                newEmptyPlaylist.PlaylistName = name;
                System.Xml.Serialization.XmlSerializer xml = new System.Xml.Serialization.XmlSerializer(typeof(PlaylistElement));
                xml.Serialize(sw, newEmptyPlaylist);
            }
        }

        public MediaPlaylist(string existingPlaylistPath, List<string> deadlockChecker)
        {
            try
            {
                PlaylistElement playlist;
                using (StreamReader sr = new StreamReader(existingPlaylistPath))
                {
                    System.Xml.Serialization.XmlSerializer xml = new System.Xml.Serialization.XmlSerializer(typeof(PlaylistElement));
                    playlist = xml.Deserialize(sr) as PlaylistElement;
                }
                name = playlist.PlaylistName;
                filename = existingPlaylistPath;

                deadlockChecker.Add(filename);

                // Recursively embed playlists
                foreach (EmbeddedPlaylistElement embeddedPlaylist in playlist.EmbeddedPlaylistElement)
                {
                    try
                    {
                        if (deadlockChecker.Contains(embeddedPlaylist.PlaylistFilename))
                            throw new CircularReferenceException("Circular Playlist Reference encountered.  Playlist will remain unusable until this is resolved.");
                        embeddedLists.Add(new MediaPlaylist(embeddedPlaylist.PlaylistFilename, deadlockChecker));
                    }
                    catch (CircularReferenceException e)
                    {
                        // Try to mark this playlist and continue
                    }
                }

                foreach (MediaObjectElement mediaObject in playlist.MediaObjectElement)
                    mediaList.Add(new Mp3(mediaObject.Filename));
            }
            catch (Exception ex)
            {
                throw new Exception("Could not create playlist: " + ex.Message);
            }
        }

        public void AddMedia(MediaObject media)
        {
            mediaList.Add(media);
        }

        public void Save()
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {
                PlaylistElement newPlaylist = new PlaylistElement();
                
                List<MediaObjectElement> mediaElements = new List<MediaObjectElement>();
                foreach (MediaObject mo in mediaList)
                    mediaElements.Add(new MediaObjectElement((mo as Mp3).FileName));

                List<EmbeddedPlaylistElement> embeddedPlaylists = new List<EmbeddedPlaylistElement>();
                foreach(MediaPlaylist embeddedPlaylist in embeddedLists)
                    embeddedPlaylists.Add(new EmbeddedPlaylistElement(embeddedPlaylist.Filename));

                newPlaylist.MediaObjectElement = mediaElements;
                newPlaylist.EmbeddedPlaylistElement = embeddedPlaylists;
                newPlaylist.PlaylistName = name;

                System.Xml.Serialization.XmlSerializer xml = new System.Xml.Serialization.XmlSerializer(typeof(PlaylistElement));
                xml.Serialize(sw, newPlaylist);
            }
        }

        public class CircularReferenceException : Exception
        {
            string playlist = "";
            string message = "You referenced a playlist which then referenced this playlist, you idiot.";
            public new string Message { get { return message; } }
            public string Playlist
            {
                get
                {
                    if (playlist.Equals(""))
                        return "Error playlist was not set by MediaMobile.";
                    else
                        return playlist;
                }
            }

            public CircularReferenceException(string exceptionMessage, string errorPlaylist) : 
                this(exceptionMessage)
            {
                playlist = errorPlaylist;
            }
            public CircularReferenceException(string exceptionMessage)
            {
                message = exceptionMessage;
            }
        }
    }

}
