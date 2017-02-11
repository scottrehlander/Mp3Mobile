using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using WMPLib;
using SerialLCDInterface;

namespace MediaMobile
{
    public partial class Mp3Player : UserControl
    {
        protected WindowsMediaPlayerClass mp3Player;
        protected Thread timeThread;
        protected bool stopped = true;
        protected string currentMedia = "";
        protected string currentPositionTimecode = "";
        protected PlayStateEnum playState = PlayStateEnum.Stopped;

        public PlayStateEnum PlayState { get { return playState; } }
        public string CurrentMedia { get { return currentMedia; } }

        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        static extern int SetErrorMode(int wMode);

        public enum PlayStateEnum
        {
            Playing = 0,
            Stopped,
            Paused,
            Transitioning
        }

        public Mp3Player()
        {
            InitializeComponent();

            mp3Player = new WindowsMediaPlayerClass();
            mp3Player.autoStart = false;
            mp3Player.StatusChange += new _WMPOCXEvents_StatusChangeEventHandler(mp3Player_StatusChange);
        }

        public virtual void Play(string fileName)
        {
            currentMedia = fileName;
            mp3Player.URL = fileName;
            stopped = false;
            timeThread = new Thread(new ThreadStart(timeTracker));
            timeThread.Start();
            Play();
        }

        public virtual void Play()
        {
            SetErrorMode(1);
            mp3Player.controls.play();
        }

        public delegate void MediaFinishedHandler(string currentMedia);
        public event MediaFinishedHandler OnMediaFinished;
        public delegate void PlayStateChangedHandler(PlayStateEnum playState);
        public event PlayStateChangedHandler OnPlayStateChanged;
        void mp3Player_StatusChange()
        {
            if (mp3Player.playState == WMPPlayState.wmppsMediaEnded)
            {
                if (OnMediaFinished != null)
                    OnMediaFinished(currentMedia);
                currentMedia = "";
            }
            else if(OnPlayStateChanged != null)
            {
                if (mp3Player.playState == WMPPlayState.wmppsTransitioning)
                {
                    playState = PlayStateEnum.Transitioning;
                    OnPlayStateChanged(PlayStateEnum.Transitioning);
                }
                if (mp3Player.playState == WMPPlayState.wmppsPlaying)
                {
                    playState = PlayStateEnum.Playing;
                    OnPlayStateChanged(PlayStateEnum.Playing);
                }
                else if (mp3Player.playState == WMPPlayState.wmppsStopped)
                {
                    playState = PlayStateEnum.Stopped;
                    OnPlayStateChanged(PlayStateEnum.Stopped);
                }
                else if (mp3Player.playState == WMPPlayState.wmppsPaused)
                {
                    playState = PlayStateEnum.Paused;
                    OnPlayStateChanged(PlayStateEnum.Paused);
                }
            }
            Console.Out.WriteLine("State has been changed: " + mp3Player.playState.ToString());
        }

        public delegate void TimeElapsedHandler(string timeElapsed);
        public event TimeElapsedHandler TimeElapsed;
        protected void timeTracker()
        {
            while (!stopped)
            {
                Thread.Sleep(1000);
                try
                {
                    if (TimeElapsed != null)
                    {
                        if (mp3Player.currentPositionString != currentPositionTimecode)
                        {
                            currentPositionTimecode = (mp3Player.controls.currentPositionString.Equals("") ? "00:00" : mp3Player.controls.currentPositionString)
                                + "/" + mp3Player.currentItem.durationString;
                            TimeElapsed(currentPositionTimecode);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine("EXECPTION: " + e.Message);
                }
            }
        }
    }

    class SerialLCDMp3 : Mp3Player
    {
        const string SPLASH_STRING = "       Wii3P0               2008          SRT Technologies";
        string lastTimeRecorded = "";
        string currentTitle = "";
        string currentArtist = "";

        public string CurrentTitle { get { return currentTitle; } set { currentTitle = value; } }
        public string CurrentArtist { get { return currentArtist; } set { currentArtist = value; } }

        public SerialLCDMp3()
        {
            SerialLCDInterface.SerialLCDInterface.InitializeDevice(1);
            //Simluation string
            //serialLcd = new SerialLCDInterface.SerialLCDInterface(true);

            SerialLCDInterface.SerialLCDInterface.SetBacklightBrightness(255);
            this.TimeElapsed += new TimeElapsedHandler(SerialLCDMp3_TimeElapsed);
            // Write the splash
            SerialLCDInterface.SerialLCDInterface.WriteString(SerialLCDInterface.SerialLCDInterface.RowMemoryAddress.Splash, SPLASH_STRING);
            SerialLCDInterface.SerialLCDInterface.MoveCursorToPos(0, 0);
            ClearScreen();
            SerialLCDInterface.SerialLCDInterface.DisplayString(SerialLCDInterface.SerialLCDInterface.RowMemoryAddress.Splash);
        }

        void SerialLCDMp3_TimeElapsed(string timeElapsed)
        {
            if (!timeElapsed.Equals(lastTimeRecorded))
            {
                //{
                //    int i = 0;
                //    while (serialLcd.ThreadBlock)
                //    {
                //        Thread.Sleep(5);
                //        Console.Out.WriteLine("Thread is blocked out");
                //        if (i == 20) return;
                //    }
                //}
                //serialLcd.ThreadBlock = true;
                //serialLcd.WriteString(SerialLCDInterface.SerialLCDInterface.RowMemoryAddress.Line3, timeElapsed);
                //serialLcd.MoveCursorToPos(2, 0);
                //serialLcd.DisplayString(SerialLCDInterface.SerialLCDInterface.RowMemoryAddress.Line3);
                //serialLcd.ThreadBlock = false;
                SerialLCDInterface.SerialLCDInterface.DisplayStringAtLocation(SerialLCDInterface.SerialLCDInterface.RowMemoryAddress.Line3, timeElapsed, 0, 2);
                lastTimeRecorded = timeElapsed;
            }
        }

        public void Play(string fileName, string trackTitle, string artist)
        {
            currentMedia = fileName;
            CurrentArtist = artist;
            currentTitle = trackTitle;
            mp3Player.URL = fileName;
            stopped = false;
            // Lets not keep track of time, and see if we get fucked up
            timeThread = new Thread(new ThreadStart(timeTracker));
            timeThread.Start();
            Play();
        }

        public override void Play()
        {
            if (currentArtist.Equals(""))
                currentArtist = "Unknown Artist";
            SerialLCDInterface.SerialLCDInterface.UpdateLineText(SerialLCDInterface.SerialLCDInterface.RowMemoryAddress.Line1, currentArtist);
            Thread.Sleep(100);
            if (currentTitle.Equals(""))
                currentTitle = "Unknown Title";
            SerialLCDInterface.SerialLCDInterface.UpdateLineText(SerialLCDInterface.SerialLCDInterface.RowMemoryAddress.Line2, currentTitle);

            base.Play();
        }

        public void ClearScreen()
        {
            SerialLCDInterface.SerialLCDInterface.ClearScreen();
        }

        public void SetBacklightBrightness(int brightness)
        {
            SerialLCDInterface.SerialLCDInterface.SetBacklightBrightness(brightness);
        }
    }
}
