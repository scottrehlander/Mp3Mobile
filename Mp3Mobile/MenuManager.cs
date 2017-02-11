using System;
using System.Collections.Generic;
using System.Text;
using MediaLibrary;

namespace MediaMobile
{
    
    enum CursorDirection { Down, Up };
    public enum MenuNodeType { Unknown, Root, Music, Video, Settings, Artists, Playlists, Playlist, Albums, Songs, PlaylistSongs, SongDetail }

    class MenuManager
    {
        public delegate void MenuTimeoutHandler();
        public event MenuTimeoutHandler OnMenuTimeout;
        public delegate void MediaSelectedHandler(List<string> media);
        public event MediaSelectedHandler OnMediaSelected;

        private int menuTimeout = 5;
        DateTime lastAction = DateTime.Now;

        SerialLCDMp3 lcdScreen;
        MediaLibrary.MediaLibrary mediaLib;
        MenuNode rootNode;
        MenuNode currentMenuNode;
        int topMostChildNodeIndex;
        int cursorPosition;

        private const string ARTIST_TITLE = "{Artists}";
        private const string PLAYLISTS_TITLE = "{Playlists}";
        private const string ALBUM_TITLE = "{Albums}";
        private const string SONG_TITLE = "{Songs}";
        private const string SONG_DATA_TITLE = "{Song Data}";

        public int MenuTimeout { set { menuTimeout = value; } get { return menuTimeout; } }

        public MenuManager(SerialLCDMp3 serialInterface, MediaLibrary.MediaLibrary medLib)
        {
            lcdScreen = serialInterface;
            mediaLib = medLib;

            SerialLCDInterface.SerialLCDInterface.ClearScreen();
            InitializeMenuTree();
            currentMenuNode = rootNode;
            cursorPosition = 0;
            ShowMenu(currentMenuNode, 0);

            new System.Threading.Thread(new System.Threading.ThreadStart(MenuTimeoutWatcher)).Start();
        }

        private void MenuTimeoutWatcher()
        {
            while (1 == 1)
            {
                if ((DateTime.Now - lastAction).Seconds > menuTimeout)
                {
                    if (OnMenuTimeout != null)
                        OnMenuTimeout();
                    return;
                }
            }
        }

        DateTime nextAllowed = DateTime.Now;
        public void KeyPressed(System.Windows.Forms.Keys key)
        {
            lastAction = DateTime.Now;
            if (DateTime.Now <= nextAllowed) return;
            nextAllowed = DateTime.Now + new TimeSpan(1000000);
            Console.Out.WriteLine("menu caught key " + key.ToString());
            if (key == System.Windows.Forms.Keys.Down)
                MoveCursor(CursorDirection.Down);
            else if (key == System.Windows.Forms.Keys.Up)
                MoveCursor(CursorDirection.Up);
            else if (key == System.Windows.Forms.Keys.Right)
            {
                DisplayNextMenu();
            }
            else if (key == System.Windows.Forms.Keys.Left)
            {
                DisplayPrevMenu();
            }
            else if (key == System.Windows.Forms.Keys.Enter)
            {
                List<string> mediaList;
                MenuNode selectedNode = currentMenuNode.ChildNodes[topMostChildNodeIndex + cursorPosition];
                if (selectedNode.Name.StartsWith("{") && selectedNode.Name.EndsWith("}"))
                    mediaList = GetSelectedMedia(currentMenuNode);
                else
                    mediaList = GetSelectedMedia(currentMenuNode.ChildNodes[topMostChildNodeIndex + cursorPosition]);
                if (OnMediaSelected != null)
                    OnMediaSelected(mediaList);
            }
        }

        private void InitializeMenuTree()
        {
            rootNode = new MenuNode(null, "Menu", MenuNodeType.Root);
            MenuNode musicNode = new MenuNode(rootNode, MenuNodeType.Music.ToString(), MenuNodeType.Music);
            rootNode.ChildNodes.Add(musicNode);
            MenuNode artistsNode = new MenuNode(musicNode, MenuNodeType.Artists.ToString(), MenuNodeType.Artists);
            MenuNode playlistsNode = new MenuNode(musicNode, MenuNodeType.Playlists.ToString(), MenuNodeType.Playlists);
            PopulateArtistMenu(artistsNode);
            musicNode.ChildNodes.Add(artistsNode);
            musicNode.ChildNodes.Add(playlistsNode);
            MenuNode videoNode = new MenuNode(rootNode, MenuNodeType.Video.ToString(), MenuNodeType.Video);
            rootNode.ChildNodes.Add(videoNode);
            MenuNode settingsNode = new MenuNode(rootNode, MenuNodeType.Settings.ToString(), MenuNodeType.Settings);
            rootNode.ChildNodes.Add(settingsNode);
        }

        private void PopulateArtistMenu(MenuNode musicNode)
        {
            SortedList<string, SortedList<string, List<MediaLibrary.MediaObject>>> artists = mediaLib.GetMediaByArtist();
            if(artists.Count > 0)
                musicNode.ChildNodes.Add(new MenuNode(musicNode, ARTIST_TITLE, MenuNodeType.Artists));
            foreach (string artist in artists.Keys)
            {
                MenuNode artistNode = new MenuNode(musicNode, artist, MenuNodeType.Artists);
                musicNode.ChildNodes.Add(artistNode);
            }
        }

        private void PopulateChildNodes(MenuNode selectedNode)
        {
            if (selectedNode.NodeType == MenuNodeType.Artists)
            {
                SortedList<string, List<MediaLibrary.MediaObject>> albums = mediaLib.GetMediaByArtist()[selectedNode.Name];
                if(albums.Count > 0)
                    selectedNode.ChildNodes.Add(new MenuNode(selectedNode, ALBUM_TITLE, MenuNodeType.Albums));

                foreach (string albumName in albums.Keys)
                    selectedNode.ChildNodes.Add(new MenuNode(selectedNode, (albumName.Equals("") ? "Untitled Album" : albumName), MenuNodeType.Albums));
            }
            else if (selectedNode.NodeType == MenuNodeType.Playlists)
            {
                if (mediaLib.Playlists.Count > 0)
                    selectedNode.ChildNodes.Add(new MenuNode(selectedNode, PLAYLISTS_TITLE, MenuNodeType.Playlist));

                foreach(MediaLibrary.MediaPlaylist playlist in mediaLib.Playlists)
                    selectedNode.ChildNodes.Add(new MenuNode(selectedNode, playlist.Name, MenuNodeType.Playlist));
            }
            else if (selectedNode.NodeType == MenuNodeType.Playlist)
            {
                MediaLibrary.MediaPlaylist selectedPlaylist = null;
                foreach (MediaLibrary.MediaPlaylist playlist in mediaLib.Playlists)
                {
                    if (playlist.Name.Equals(selectedNode.Name))
                    {
                        selectedPlaylist = playlist;
                        break;
                    }
                }
                if (selectedPlaylist == null) return;

                foreach (MediaLibrary.MediaObject song in selectedPlaylist.MediaList)
                {
                    if (song is MediaLibrary.Mp3)
                    {
                        MenuNode songNode = new MenuNode(selectedNode, (song as MediaLibrary.Mp3).TrackTitle, MenuNodeType.PlaylistSongs);
                        songNode.Data = (song as MediaLibrary.Mp3).FileName;
                        selectedNode.ChildNodes.Add(songNode);
                    }
                }
            }
            else if (selectedNode.NodeType == MenuNodeType.Albums)
            {
                string album = selectedNode.Name.Equals("Untitled Album") ? "" : selectedNode.Name;
                List<MediaLibrary.MediaObject> songs = mediaLib.GetMediaByArtist()[selectedNode.ParentNode.Name][album];
                if (songs.Count > 0)
                    selectedNode.ChildNodes.Add(new MenuNode(selectedNode, SONG_TITLE, MenuNodeType.Songs));
                foreach (MediaLibrary.MediaObject song in songs)
                    if (song is MediaLibrary.Mp3)
                    {
                        MenuNode songNode = new MenuNode(selectedNode, (song as MediaLibrary.Mp3).TrackTitle, MenuNodeType.Songs);
                        songNode.Data = (song as MediaLibrary.Mp3).FileName;
                        selectedNode.ChildNodes.Add(songNode);
                    }
            }
            else if (selectedNode.NodeType == MenuNodeType.Songs)
            {
                List<MediaLibrary.MediaObject> songs = mediaLib.GetMediaByArtist()[selectedNode.ParentNode.ParentNode.Name][selectedNode.ParentNode.Name];
                foreach (MediaLibrary.MediaObject song in songs)
                {
                    if (song is MediaLibrary.Mp3)
                    {
                        if ((song as MediaLibrary.Mp3).TrackTitle.Equals(selectedNode.Name))
                        {
                            selectedNode.ChildNodes.Add(new MenuNode(selectedNode, SONG_DATA_TITLE, MenuNodeType.SongDetail));
                            selectedNode.ChildNodes.Add(new MenuNode(selectedNode, (song as MediaLibrary.Mp3).Artist, MenuNodeType.SongDetail));
                            selectedNode.ChildNodes.Add(new MenuNode(selectedNode, (song as MediaLibrary.Mp3).Album, MenuNodeType.SongDetail));
                            selectedNode.ChildNodes.Add(new MenuNode(selectedNode, "Year: " + (song as MediaLibrary.Mp3).Year.ToString(), MenuNodeType.SongDetail));
                            selectedNode.ChildNodes.Add(new MenuNode(selectedNode, (song as MediaLibrary.Mp3).Genre, MenuNodeType.SongDetail));
                        }
                    }
                }
            }
        }

        private void DisplayNextMenu()
        {
            // We will try and populate the child nodes for the currently selected child node.
            if (currentMenuNode.ChildNodes[topMostChildNodeIndex + cursorPosition].ChildNodes.Count == 0)
                PopulateChildNodes(currentMenuNode.ChildNodes[topMostChildNodeIndex + cursorPosition]);

            if (currentMenuNode.ChildNodes[topMostChildNodeIndex + cursorPosition].ChildNodes.Count > 0)
            {
                MenuNode nodeToShow = currentMenuNode.ChildNodes[topMostChildNodeIndex + cursorPosition];
                cursorPosition = 0;
                ShowMenu(nodeToShow, 0);
                currentMenuNode = nodeToShow;
            }
        }

        private void DisplayPrevMenu()
        {
            if (currentMenuNode.ParentNode == null) return;

            cursorPosition = 0;
            ShowMenu(currentMenuNode.ParentNode, 0);
            currentMenuNode = currentMenuNode.ParentNode;
        }

        private void ShowMenu(MenuNode rootNode, int startNode)
        {
            //SerialLCDInterface.SerialLCDInterface.ClearScreen();

            int screenDisplayPos = 0;
            for (int i = startNode; i < startNode + SerialLCDInterface.SerialLCDInterface.ScreenRows; i++)
            {
                if (i >= rootNode.ChildNodes.Count)
                {
                    SerialLCDInterface.SerialLCDInterface.ClearLine(GetRowMemoryAddress(i));
                    continue;
                }

                string displayString = (screenDisplayPos == cursorPosition ? ">" : " ") + rootNode.ChildNodes[i].Name;
                if (displayString.Length > 20)
                    displayString = displayString.Substring(0, 20);
                else if (displayString.Length < 20)
                    for (int j = displayString.Length; j < 20; j++)
                        displayString += " ";
                SerialLCDInterface.SerialLCDInterface.DisplayStringAtLocation(GetRowMemoryAddress(screenDisplayPos),
                    displayString, 0, screenDisplayPos);

                if (screenDisplayPos == 0)
                    topMostChildNodeIndex = i;

                rootNode.ChildNodes[i].ScreenPosition = screenDisplayPos++;
            }
        }

        public void DisplayMessage(string message)
        {
            SerialLCDInterface.SerialLCDInterface.ClearScreen();

            if (message.Length > 20)
                message = message.Substring(0, 20);

            SerialLCDInterface.SerialLCDInterface.DisplayStringAtLocation(GetRowMemoryAddress(1), message, 0, 0);
        }

        private void MoveCursor(CursorDirection direction)
        {
            // If one position in the direct we are going puts us past the screen limits, handle it specially
            if (CheckPastLimits(direction))
                return;

            // Transforms the node indicies based on direction
            int modifier = 1;
            if (direction == CursorDirection.Up)
            {
                // If the direction is up, but we are at our first child node, return
                if (cursorPosition == 0) return;
                modifier = -1;
            }
            else // If the direction is down, but we are at our last child node, return
                if (currentMenuNode.ChildNodes.Count <= cursorPosition + modifier)
                    return;   

            cursorPosition += modifier;
            for (int i = topMostChildNodeIndex; i < topMostChildNodeIndex + SerialLCDInterface.SerialLCDInterface.ScreenRows; i++)
            { // From our topmost child node, display the nodes that fit on the screen
                if (i >= currentMenuNode.ChildNodes.Count) return;

                MenuNode node = currentMenuNode.ChildNodes[i];
                if (node.ScreenPosition == cursorPosition || node.ScreenPosition == cursorPosition - modifier)
                { // If this is the the node we need to move the cursor to or delete the cursor from, update that line
                    string displayString = ">";
                    int positionToModify = cursorPosition;
                    if (node.ScreenPosition == cursorPosition - modifier)
                    {
                        displayString = " ";
                        positionToModify = cursorPosition - modifier;
                    }
                    SerialLCDInterface.SerialLCDInterface.DisplayStringAtLocation(GetRowMemoryAddress(positionToModify), displayString,
                        0, positionToModify);
                }
            }
        }

        private List<string> GetSelectedMedia(MenuNode node)
        {
            List<string> media = new List<string>();

            if (node.Name.StartsWith("{") && node.Name.EndsWith("}"))
                return media;

            if (node.ChildNodes.Count == 0)
                PopulateChildNodes(node);

            for(int i = 0; i < node.ChildNodes.Count; i++)
            {
                MenuNode childNode = node.ChildNodes[i];

                // If this childNode is a song, add it - else find all the song child nodes of this node
                if((childNode.NodeType != MenuNodeType.Songs) && (childNode.NodeType != MenuNodeType.PlaylistSongs))
                    media.AddRange(GetSelectedMedia(childNode));

                if (childNode.NodeType == MenuNodeType.Songs || childNode.NodeType == MenuNodeType.PlaylistSongs)
                {
                    if (childNode.Name.StartsWith("{") && childNode.Name.EndsWith("}"))
                        continue;
                    media.Add(childNode.Data);
                }
            }

            return media;
        }

        private bool CheckPastLimits(CursorDirection direction)
        {
            if (cursorPosition == 3 && direction == CursorDirection.Down)
            {
                if (topMostChildNodeIndex >= currentMenuNode.ChildNodes.Count - SerialLCDInterface.SerialLCDInterface.ScreenRows) return true;
                topMostChildNodeIndex++;
                ShowMenu(currentMenuNode, topMostChildNodeIndex);
                return true;
            }
            else if (cursorPosition == 0 && direction == CursorDirection.Up)
            {
                if (topMostChildNodeIndex == 0) return true;
                topMostChildNodeIndex--;
                ShowMenu(currentMenuNode, topMostChildNodeIndex);
                return true;
            }

            return false;
        }

        private SerialLCDInterface.SerialLCDInterface.RowMemoryAddress GetRowMemoryAddress(int lineNum)
        {
            switch (lineNum)
            {
                case 0:
                    return SerialLCDInterface.SerialLCDInterface.RowMemoryAddress.Line1;
                case 1:
                    return SerialLCDInterface.SerialLCDInterface.RowMemoryAddress.Line2;
                case 2:
                    return SerialLCDInterface.SerialLCDInterface.RowMemoryAddress.Line3;
                case 3:
                    return SerialLCDInterface.SerialLCDInterface.RowMemoryAddress.Line4;
                default:
                    throw new Exception("GetRowMemoryAddress(): Invalid line number: " + lineNum);
            }
        }
    }

    public class MenuNode
    {
        string name;
        MenuNode parentNode;
        List<MenuNode> childNodes = new List<MenuNode>();
        int screenPosition;
        private MenuNodeType nodeType;
        private string data;

        public MenuNode ParentNode { get { return parentNode; } }
        public List<MenuNode> ChildNodes { get { return childNodes; } set { childNodes = value; } }
        public string Name { get { return name; } }
        public int ScreenPosition { get { return screenPosition; } set { screenPosition = value; } }
        public MenuNodeType NodeType { get { return nodeType; } }
        public string Data { get { return data; } set { data = value; } }

        public MenuNode(MenuNode parentalNode, string menuName, MenuNodeType menuNodeType)
        {
            name = menuName;
            parentNode = parentalNode;
            nodeType = menuNodeType;
        }
    }

}
