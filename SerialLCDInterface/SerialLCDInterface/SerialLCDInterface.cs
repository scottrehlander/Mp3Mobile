using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.IO;
using System.Threading;

namespace SerialLCDInterface
{
    public static class SerialLCDInterface
    {
        static SerialPort LCDport;
        private static byte[,] positionMap;
        private static SortedList<int, string> displayText = new SortedList<int, string>();
        private static RowMemoryAddress currentScrollLine;
        private static bool threadBlock = false;
        //public bool ThreadBlock { get { return threadBlock; } set { threadBlock = value; } }
        private static bool isSimulated = false;
        static SimulationObject simObject;
        static bool alreadyInitialized = false;
        static bool pauseScroll = false;

        private const int DEFULT_BAUD_RATE = 9600;
        private const int MAX_COL = 20;
        private const int MAX_ROWS = 4;
        private static string POSITION_MAP_LOC = "PositionDefinition.ini";

        public static SortedList<int, string> DisplayText { get { return displayText; } set { displayText = value; } }
        public static SortedList<RowMemoryAddress, Thread> lineThreads = new SortedList<RowMemoryAddress, Thread>();
        public static bool PauseScroll { get { return pauseScroll; } set { pauseScroll = value; } }
        public static int ScreenRows { get { return MAX_ROWS; } }
        public static int ScreenCols { get { return MAX_COL; } }

        public enum RowMemoryAddress
        {
            Splash = 0,
            Line1 = 90,
            Line2 = 130,
            Line3 = 170,
            Line4 = 210
        }

        //public static SerialLCDInterface(int portNumber) : this(portNumber, DEFULT_BAUD_RATE) { }
        //public static SerialLCDInterface(bool simulate)
        //{
        //    if (simulate == false)
        //        throw new Exception("Call actual constructor to not-simulate.");
        //    else
        //    {
        //        isSimulated = true;
        //        simObject = new SimulationObject();
        //    }
        //}
        //public SerialLCDInterface(int portNumber, int baudRate)
        //{
        //    LCDport = new SerialPort("COM" + portNumber.ToString(), baudRate);
        //    LCDport.StopBits = StopBits.One;
        //    LCDport.DataBits = 8;
        //    LCDport.Parity = Parity.None;
        //    LCDport.RtsEnable = true;
        //    LCDport.DtrEnable = false;
        //    LCDport.Open();

        //    InitPositionMap();
        //}

        public static void InitializeDevice(int portNumber) { InitializeDevice(portNumber, DEFULT_BAUD_RATE); }
        public static void InitializeDevice(bool simulate)
        {
               if (simulate == false)
                   throw new Exception("Call actual constructor to not-simulate.");
               else
               {
                   isSimulated = true;
                   simObject = new SimulationObject();
               }
        }
        public static void InitializeDevice(int portNumber, int baudRate)
        {
            if(alreadyInitialized) return;

            LCDport = new SerialPort("COM" + portNumber.ToString(), baudRate);
            LCDport.StopBits = StopBits.One;
            LCDport.DataBits = 8;
            LCDport.Parity = Parity.None;
            LCDport.RtsEnable = true;
            LCDport.DtrEnable = false;
            LCDport.Open();

            InitPositionMap();
            alreadyInitialized = true;
        }

        #region LCD Commands

        public static void DisplayStringAtLocation(RowMemoryAddress rowAddr, string displayString, int col, int row)
        {
            while (threadBlock) { }
            try
            {
                threadBlock = true;
                lock (new object())
                {
                    WriteString(rowAddr, displayString);
                    MoveCursorToPos(row, col);
                    DisplayString(rowAddr);
                }
            }
            finally { threadBlock = false; }
        }

        /// <summary>
        /// Stores a string to the memory of the LCD unit.
        /// </summary>
        /// <param name="stringAddr">An integer representing the address of the string that will be stored.</param>
        /// <param name="displayString">A string to be stored in the LCD's memory.</param>
        public static void WriteString(RowMemoryAddress rowAddr, string displayString)
        {
            if (isSimulated)
            {
                simObject.WriteString(rowAddr, displayString);
                return;
            }
                
            byte[] msg = Encoding.ASCII.GetBytes(displayString);

            byte[] fullMsg = new byte[4 + msg.Length];
            fullMsg[0] = 0x1B;
            fullMsg[1] = 0x77;
            fullMsg[2] = byte.Parse(((int)rowAddr).ToString());
            for (int i = 0; i < msg.Length; i++)
                fullMsg[i + 3] = msg[i];
            fullMsg[fullMsg.Length - 1] = 0x00;

            Console.Out.WriteLine("Writing string to: " + rowAddr.ToString() + " String: " + displayString);
            SendMessage(fullMsg);
        }

        public static void WriteCopyrightString(int stringAddr, string displayString)
        {
            if (isSimulated) return;

            byte[] msg = Encoding.ASCII.GetBytes(displayString);

            byte[] fullMsg = new byte[5 + msg.Length];
            fullMsg[0] = 0x1B;
            fullMsg[1] = 0x77;
            fullMsg[2] = byte.Parse(stringAddr.ToString());
            fullMsg[3] = 0x04;
            for (int i = 0; i < msg.Length; i++)
                fullMsg[i + 4] = msg[i];
            fullMsg[fullMsg.Length - 1] = 0x00;

            SendMessage(fullMsg);
        }

        /// <summary>
        /// Displays a string that has been previous stored using WriteString().
        /// </summary>
        /// <param name="stringAddr">The address of the string that is to be displayed.</param>
        public static void DisplayString(RowMemoryAddress stringAddr)
        {
            if (isSimulated)
            {
                simObject.DisplayString(stringAddr);
                return;
            }

            Console.Out.WriteLine("Displaying string from: " + stringAddr.ToString());
            SendMessage(new byte[] {
                0x1B,
                0x53,
                byte.Parse(((int)stringAddr).ToString())
            });
        }

        /// <summary>
        /// Sets the backlight brightness of the LCD display
        /// </summary>
        /// <param name="brightness">Valid values are 0-255</param>
        public static void SetBacklightBrightness(int brightness)
        {
            SendMessage(new byte[] { 
                0x1B, 
                0x2A, 
                byte.Parse(brightness.ToString())
            });
        }

        public static void ClearLine(RowMemoryAddress lineNumber)
        {
                ////WriteString(lineNumber, " ");
                //for (int i = 0; i < 20; i++)
                //{
                //    //ThreadBlock = true;
                //    //MoveCursorToPos(int.Parse(lineNumber.ToString().Substring(4)) - 1, i);
                //    //DisplayString(lineNumber);
                //    //ThreadBlock = false;
                //    DisplayStringAtLocation(lineNumber, " ", i, int.Parse(lineNumber.ToString().Substring(4)) - 1);
                //}
            string twentyChars = "";
            for (int i = 0; i < MAX_COL; i++)
                twentyChars += " ";
            DisplayStringAtLocation(lineNumber, "                    ", 0, int.Parse(lineNumber.ToString().Substring(4)) - 1);
        }

        public static void ClearLine(RowMemoryAddress lineNumber, int fromCol)
        {
            lock (new object())
            {
                //WriteString(lineNumber, " ");
                for (int i = fromCol; i < MAX_COL; i++)
                {
                    //ThreadBlock = true;
                    //MoveCursorToPos(int.Parse(lineNumber.ToString().Substring(4)) - 1, i);
                    //DisplayString(lineNumber);
                    //ThreadBlock = false;
                    DisplayStringAtLocation(lineNumber, " ", i, int.Parse(lineNumber.ToString().Substring(4)) - 1);
                }
            }
        }

        public static void ClearScreen()
        {
            SendMessage(new byte[] { 
                0xFE, 
                0x01 
            });
        }

        public static void MoveCursorToPos(int rowNumber, int colNumber)
        {
            if (isSimulated)
            {
                simObject.MoveCursorToPos(rowNumber, colNumber);
                return;
            }

            Console.Out.WriteLine("Moving Cursor to row: " + rowNumber.ToString() + " col: " + colNumber);
            SendMessage(new byte[] { 
                0xFE, 
                positionMap[rowNumber, colNumber] 
            });
        }

        private static void SendMessage(byte[] message)
        {
            if (isSimulated) return;
            
            LCDport.Write(message, 0, message.Length);
        }
        private static void SendMessage(string message)
        {
            try
            {
                LCDport.Write(message);
            }
            catch (Exception e)
            {
                throw new Exception("Error attempting to send Serial Message", e);
            }
        }

        #endregion


        #region Functional Methods

        private static void InitPositionMap()
        {
            try
            {
                // Look in parent directories for the position map
                int tries = 0;
                while (!File.Exists(POSITION_MAP_LOC))
                {
                    POSITION_MAP_LOC = "..\\" + POSITION_MAP_LOC;
                    if (tries++ > 20)
                        throw new Exception("Serial LCD Position Map not found.");
                }

                using (StreamReader sr = new StreamReader(POSITION_MAP_LOC))
                {
                    int rows = int.Parse(sr.ReadLine().Split('=')[1]);
                    int cols = int.Parse(sr.ReadLine().Split('=')[1]);
                    positionMap = new byte[rows, cols];

                    for (int i = 0; i < rows; i++)
                    {
                        string[] row = sr.ReadLine().Split(' ');
                        for (int j = 0; j < cols; j++)
                            positionMap[i, j] = byte.Parse(row[j], System.Globalization.NumberStyles.AllowHexSpecifier);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error occured while initializing LCD Position Map", e);
            }
        }

        public static int AddressToLineNum(RowMemoryAddress line)
        {
            return int.Parse(line.ToString().Substring(4));
        }

        #endregion


        #region Line Management

        public static void UpdateLineText(RowMemoryAddress line, string text)
        {
            if(displayText.ContainsKey(AddressToLineNum(line)))
                displayText[AddressToLineNum(line)] = text;
            else
                displayText.Add(AddressToLineNum(line), text);

            //while (ThreadBlock)
            //    Console.Out.WriteLine("Thread is blocked out");

            //ThreadBlock = true;
            if (lineThreads.ContainsKey(line))
            {
                lineThreads[line].Abort();
                ClearLine(line);
                lineThreads.Remove(line);
            }
            //ThreadBlock = false;

            currentScrollLine = line;
            lineThreads.Add(line, new Thread(new ThreadStart(ScrollLine)));
            lineThreads[line].Start();
        }

        private static void ScrollLine()
        {
            RowMemoryAddress thisLine = currentScrollLine;

            int curChar = 0;
            string dispString = displayText[AddressToLineNum(thisLine)];

            if (dispString.Length <= MAX_COL)
            {
                //while (ThreadBlock)
                //    Console.Out.WriteLine("Thread is blocked out") ;
                
                //ThreadBlock = true;
                //WriteString(thisLine, dispString);
                //MoveCursorToPos(AddressToLineNum(thisLine) - 1, 0);
                //DisplayString(thisLine);
                //ThreadBlock = false;
                DisplayStringAtLocation(thisLine, dispString, 0, AddressToLineNum(thisLine) - 1);
                Thread.CurrentThread.Abort();
            }

            bool right = true;

            while (1 == 1)
            {
                if (pauseScroll)
                {
                    System.Threading.Thread.Sleep(100);
                    continue;
                }
                int lengthTillEnd = dispString.Length - curChar >= MAX_COL ? MAX_COL : dispString.Length - curChar;

                if (curChar == dispString.Length - 18)
                    right = false;
                if (curChar == 0)
                    right = true;

                if (curChar == 1 && right)
                    Thread.Sleep(2000);

                //while (ThreadBlock)
                //    Console.Out.WriteLine("Thread is blocked out");

                //ThreadBlock = true;
                //WriteString(thisLine, dispString.Substring(curChar, lengthTillEnd));
                //MoveCursorToPos(AddressToLineNum(thisLine) - 1, 0);
                //DisplayString(thisLine);
                DisplayStringAtLocation(thisLine, dispString.Substring(curChar, lengthTillEnd), 0, AddressToLineNum(thisLine) - 1);

                if (lengthTillEnd < MAX_COL)
                    ClearLine(thisLine, lengthTillEnd);
                //ThreadBlock = false;

                if (right)
                    curChar++;
                else
                    curChar--;

                Thread.Sleep(500);
            }
        }

        #endregion


        #region Clock code

        public static void DisplayTime()
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(TimeThread));
            t.Start();
        }

        static RowMemoryAddress timeLine = RowMemoryAddress.Line3;
        public static void TimeThread()
        {
            DateTime displayed = DateTime.Now;
            string timeString = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + (DateTime.Now.Second.ToString().Length == 1 ? "0" + DateTime.Now.Second : DateTime.Now.Second.ToString());
            WriteString(timeLine, timeString);
            MoveCursorToPos(int.Parse(timeLine.ToString().Substring(4)) - 1, 6);
            DisplayString(timeLine);

            while(1 == 1)
            {
                System.Threading.Thread.Sleep(1000);
                DateTime now = DateTime.Now;
                if (displayed.Second != now.Second)
                {
                    timeString = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + (DateTime.Now.Second.ToString().Length == 1 ? "0" + DateTime.Now.Second : DateTime.Now.Second.ToString());
                    WriteString(timeLine, timeString);
                    MoveCursorToPos(int.Parse(timeLine.ToString().Substring(4)) - 1, 6);
                    DisplayString(timeLine);
                    displayed = now;
                }
            }
        }

        #endregion


        #region Testing

        public static void ILoveXtine()
        {
            while (1 == 1)
            {
                ClearScreen();
                MoveCursorToPos(0, 0);
                WriteString(RowMemoryAddress.Line1, "I love Christine");
                DisplayString(RowMemoryAddress.Line1);
                System.Threading.Thread.Sleep(1000);

                ClearScreen();
                MoveCursorToPos(1, 0);
                WriteString(RowMemoryAddress.Line1, "I love Christine");
                DisplayString(RowMemoryAddress.Line1);
                System.Threading.Thread.Sleep(1000);

                ClearScreen();
                MoveCursorToPos(2, 0);
                WriteString(RowMemoryAddress.Line1, "I love Christine");
                DisplayString(RowMemoryAddress.Line1);
                System.Threading.Thread.Sleep(1000);

                ClearScreen();
                MoveCursorToPos(3, 0);
                WriteString(RowMemoryAddress.Line1, "I love Christine");
                DisplayString(RowMemoryAddress.Line1);
                System.Threading.Thread.Sleep(1000);
            }
        }

        class SimulationObject
        {
            public string line1 = "";
            public string line2 = "";
            public string line3 = "";
            public string line4 = "";

            private int row = 0;
            private int col = 0;

            SimulationGUI gui = new SimulationGUI();

            public SimulationObject()
            {
                gui.Show();
            }

            public void WriteString(RowMemoryAddress rowAddr, string displayString)
            {
                switch (rowAddr)
                {
                    case RowMemoryAddress.Line1:
                        line1 = displayString;
                        break;
                    case RowMemoryAddress.Line2:
                        line2 = displayString;
                        break;
                    case RowMemoryAddress.Line3:
                        line3 = displayString;
                        break;
                    case RowMemoryAddress.Line4:
                        line4 = displayString;
                        break;
                }
            }

            delegate void ListInvoker(int lineNum, string displayString, int colNum);
            public void DisplayString(RowMemoryAddress stringAddr)
            {
                switch (row)
                {
                    case 0:
                        if (gui.lstDisplay.InvokeRequired)
                        {
                            ListInvoker li = new ListInvoker(gui.DisplayString);
                            gui.Invoke(li, new object[] { 0, line1, col });
                        }
                        break;
                    case 1:
                        if (gui.lstDisplay.InvokeRequired)
                        {
                            ListInvoker li = new ListInvoker(gui.DisplayString);
                            gui.Invoke(li, new object[] { 1, line2, col });
                        }
                        break;
                    case 2:
                        if (gui.lstDisplay.InvokeRequired)
                        {
                            ListInvoker li = new ListInvoker(gui.DisplayString);
                            gui.Invoke(li, new object[] { 2, line3, col });
                        }
                        break;
                    case 3:
                        if (gui.lstDisplay.InvokeRequired)
                        {
                            ListInvoker li = new ListInvoker(gui.DisplayString);
                            gui.Invoke(li, new object[] { 3, line4, col });
                        }
                        break;
                }
            }

            public void MoveCursorToPos(int rowNumber, int colNumber)
            {
                row = rowNumber;
                col = colNumber;
            }
        }

        #endregion

    }
}
