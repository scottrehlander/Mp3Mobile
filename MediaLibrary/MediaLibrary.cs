using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using MediaMobile.MediaMobileUtils;

namespace MediaMobile.MediaLibrary
{
    /// <summary>
    /// This class maintains the collection of all media and corresponding playlists.
    /// It also provides features to import/remove media from the library.
    /// Anything that the MediaMobile is aware of will have a reference to it in thiq
    /// class.
    /// </summary>
    public class MediaLibrary
    {
        List<MediaPlaylist> playlists = new List<MediaPlaylist>();
        List<MediaObject> allMedia = new List<MediaObject>();
        SortedList<string, SortedList<string, List<MediaObject>>> mediaByArtist;

        // I guess I don't really need all MediaObjects in memory. :(  Strings will suffice - or will it...
        public List<MediaPlaylist> Playlists { get { return playlists; } }
        public List<MediaObject> AllMedia { get { return allMedia; } }

        public MediaLibrary()
        {
            LoadLibrary();
        }

        private void LoadLibrary()
        {
                string libraryBase = MediaMobile.MediaMobileUtils.LibraryUtilities.GetLibraryPlaylistLocation();

                System.Xml.Serialization.XmlSerializer xml = new System.Xml.Serialization.XmlSerializer(typeof(PlaylistElement));
                if (!File.Exists(libraryBase)) return;

                using (StreamReader sr = new StreamReader(libraryBase))
                {
                    PlaylistElement playlist = xml.Deserialize(sr) as PlaylistElement;
                    foreach (MediaObjectElement mediaObject in playlist.MediaObjectElement)
                    {
                        try
                        {
                            Mp3 media = new Mp3(mediaObject.Filename);
                            if(!allMedia.Contains(media))
                                allMedia.Add(media);
                        }
                        catch (Exception e)
                        {
                            Console.Out.WriteLine("LoadLibrary(): Error creating MP3: " + e.Message);
                        }
                    }

                    foreach (EmbeddedPlaylistElement embeddedPlaylist in playlist.EmbeddedPlaylistElement)
                    {
                        MediaPlaylist playlistToAdd = null;
                        List<string> deadlockChecker = new List<string>();
                        deadlockChecker.Add(libraryBase);
                        playlistToAdd = new MediaPlaylist(embeddedPlaylist.PlaylistFilename, deadlockChecker);
                        playlists.Add(playlistToAdd);
                    }
                }
        }

        public void SaveLibrary()
        {
            string libraryBase = MediaMobile.MediaMobileUtils.LibraryUtilities.GetLibraryPlaylistLocation();

            List<MediaObjectElement> moeArray = new List<MediaObjectElement>();
            for(int i = 0; i < allMedia.Count; i++)
                moeArray.Add(new MediaObjectElement((allMedia[i] as Mp3).FileName));

            List<EmbeddedPlaylistElement> embeddedPlaylists = new List<EmbeddedPlaylistElement>();
            for (int i = 0; i < playlists.Count; i++)
                embeddedPlaylists.Add(new EmbeddedPlaylistElement(playlists[i].Filename));

            PlaylistElement playlist = new PlaylistElement("Library", moeArray, embeddedPlaylists);

            using (StreamWriter sw = new StreamWriter(libraryBase))
            {
                System.Xml.Serialization.XmlSerializer xml = new System.Xml.Serialization.XmlSerializer(typeof(PlaylistElement));
                xml.Serialize(sw, playlist);
            }
        }

        public void SavePlaylists()
        {
            string libraryBase = MediaMobile.MediaMobileUtils.LibraryUtilities.GetLibraryPlaylistLocation();

            PlaylistElement library;
            using (StreamReader sr = new StreamReader(libraryBase))
            {
                System.Xml.Serialization.XmlSerializer xml = new System.Xml.Serialization.XmlSerializer(typeof(PlaylistElement));
                library = xml.Deserialize(sr) as PlaylistElement;
            }
            if (library == null) throw new Exception("SavePlaylists(): Library could not be loaded.");

            List<EmbeddedPlaylistElement> newEmbeddedPlaylists = new List<EmbeddedPlaylistElement>();
            foreach (MediaPlaylist playlist in playlists)
                newEmbeddedPlaylists.Add(new EmbeddedPlaylistElement(playlist.Filename));

            library.EmbeddedPlaylistElement = newEmbeddedPlaylists;
            using (StreamWriter sw = new StreamWriter(libraryBase, false))
            {
                System.Xml.Serialization.XmlSerializer xml = new System.Xml.Serialization.XmlSerializer(typeof(PlaylistElement));
                xml.Serialize(sw, library);
            }
        }


        public delegate void OnImportMediaDirectoryCheckedHandler(string directory);
        public event OnImportMediaDirectoryCheckedHandler OnImportMediaDirectoryChecked;
        public void ImportMedia(object directoryInfo)
        {
            DirectoryInfo[] directories = directoryInfo as DirectoryInfo[];
            // If this is not recursive, just call ImportMedia once - else 
            //   loop through all directories that are in this directory
            foreach (DirectoryInfo directory in directories)
            {
                if (OnImportMediaDirectoryChecked != null)
                    OnImportMediaDirectoryChecked(directory.FullName);

                //if (!recursive)
                //{
                //    FileInfo[] files = directory.GetFiles();
                //    if (files.Length > 0)
                //    {
                //        foreach (FileInfo file in files)
                //            if (LibraryUtilities.EndsWithKnownExtension(file.FullName))
                //                AddMedia(file.FullName);
                //    }
                //}
                //else
                    ImportMediaRecursive(directory);
            }
        }

        public void ImportMediaRecursive(DirectoryInfo directory)
        {
            try
            {
                if (directory.FullName.ToUpper().Contains("$RECYCLE.BIN"))
                    return;
                if (OnImportMediaDirectoryChecked != null)
                    OnImportMediaDirectoryChecked(directory.FullName);

                DirectoryInfo[] diArray = directory.GetDirectories();
                foreach (DirectoryInfo di in diArray)
                    ImportMediaRecursive(di);

                FileInfo[] files = directory.GetFiles();
                if (files.Length > 0)
                {
                    foreach (FileInfo file in files)
                        if (LibraryUtilities.EndsWithKnownExtension(file.FullName))
                            AddMedia(file.FullName);
                }
            }
            catch (Exception e) { Console.Out.WriteLine(e.Message); }
        }

        public void AddMedia(string fileName)
        {
            // THIS IS SLOW!!!
            if (fileName.EndsWith("mp3"))
            {
                Mp3 mp3 = new Mp3(fileName);
                foreach (MediaObject media in allMedia)
                    if ((media as Mp3).FileName.Equals(mp3.FileName))
                        return;
                allMedia.Add(mp3);
            }
        }

        public string Next(string fileName)
        {
            bool returnNext = false;
            foreach (MediaObject media in AllMedia)
            {
                if (returnNext)
                    return (media as Mp3).FileName;
                if ((media as Mp3).FileName.Equals(fileName))
                {
                    returnNext = true;
                }
            }
            return "";
        }

        public string Random()
        {
            return (allMedia[new Random().Next(allMedia.Count - 1)] as Mp3).FileName;
        }

        public string Prev(string fileName)
        {
            string lastMp3 = "";
            foreach (MediaObject media in AllMedia)
            {
                if ((media as Mp3).FileName.Equals(fileName))
                    return lastMp3;
                lastMp3 = (media as Mp3).FileName;
            }
            return lastMp3;
        }

        public MediaPlaylist GetPlaylist(string playlistFilename)
        {
            foreach (MediaPlaylist playlist in playlists)
                if (playlist.Filename.Equals(playlistFilename))
                    return playlist;
            return null;
        }

        public MediaPlaylist GetPlaylistByName(string playlistName)
        {
            foreach (MediaPlaylist playlist in playlists)
                if (playlist.Name.Equals(playlistName))
                    return playlist;
            return null;
        }

        public MediaObject GetMedia(string fileName)
        {
            foreach (MediaObject media in allMedia)
                if ((media as Mp3).FileName.ToUpper().Equals(fileName.ToUpper()))
                    return media;
            return null;
        }

        public SortedList<string, SortedList<string, List<MediaObject>>> GetMediaByArtist()
        {
            // If we already are sorted by artist...
            if (mediaByArtist != null)
                return mediaByArtist;
            
            mediaByArtist = new SortedList<string, SortedList<string, List<MediaObject>>>();
            foreach (MediaObject media in allMedia)
            {
                if (media is Mp3)
                {
                    Mp3 mp3 = media as Mp3;
                    if (mp3.Artist.Equals(""))
                        mp3.SetArtist("Unknown Artist");

                    if (!mediaByArtist.ContainsKey(mp3.Artist))
                        mediaByArtist.Add(mp3.Artist, new SortedList<string, List<MediaObject>>());
                    if (!mediaByArtist[mp3.Artist].ContainsKey(mp3.Album))
                        mediaByArtist[mp3.Artist].Add(mp3.Album, new List<MediaObject>());
                    mediaByArtist[mp3.Artist][mp3.Album].Add(media);
                }
            }
            return mediaByArtist;
        }

        public void AddPlaylist(string playlistName, string filepath)
        {
            MediaPlaylist playlist = new MediaPlaylist(filepath, playlistName);
            playlists.Add(playlist);
        }
        public void AddPlaylist()
        {
            string path = MediaMobileUtils.LibraryUtilities.GetLibraryPlaylistLocation();
            path = path.Substring(0, path.LastIndexOf("\\"));
            path += "\\New Playlist.wi3";

            // Find a unique name for this playlist
            int i = 1;
            while(File.Exists(path))
            {  
                path = path.Substring(0, path.LastIndexOf("\\")) + "\\New Playlist " + i++.ToString() + ".wi3"; 
            }

            string name = path.Substring(path.LastIndexOf("\\") + 1);
            name = name.Substring(0, name.Length - 4);
            MediaPlaylist playlist = new MediaPlaylist(path.Substring(0, path.LastIndexOf("\\")), name);
            playlists.Add(playlist);
            SaveLibrary();
        }

        public void RemovePlaylist(string filename)
        {
            if (File.Exists(filename))
                File.Delete(filename);

            for(int i = 0; i < playlists.Count; i++)
                if (playlists[i].Filename.Equals(filename))
                {
                    playlists.Remove(playlists[i]);
                    SaveLibrary();
                }


        }
    }
}
