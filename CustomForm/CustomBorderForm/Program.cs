#region Using directives

using System;
using System.Collections;
using System.Windows.Forms;

#endregion

namespace Kobush.Windows.Forms
{
	public class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.DoEvents();

			Application.Run(new DemoForm());
		}
	}
}

