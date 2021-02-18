using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ActiproSoftware.Windows.Controls.Ribbon.Input;

namespace ActiproSoftware.ProductSamples.RibbonSamples.QuickStart.GettingStarted {

	/// <summary>
	/// Represents a sample QuickStart for getting started with the ribbon control.
	/// </summary>
	public partial class Step14Window : ActiproSoftware.Windows.Controls.Ribbon.RibbonWindow {

		/// <summary>
		/// Initializes an instance of the <c>Step14Window</c> class.
		/// </summary>
        public Step14Window() {
			// Register UI providers for existing built-in commands
			RibbonCommandUIManager.Register(System.Windows.Input.ApplicationCommands.Copy, 
				new RibbonCommandUIProvider("Copy", null, "pack://application:,,,/SampleBrowser;component/Images/Icons/Copy16.png", "Copy the selection and put it on the Clipboard."));
			RibbonCommandUIManager.Register(System.Windows.Input.ApplicationCommands.Cut, 
				new RibbonCommandUIProvider("Cut", null, "pack://application:,,,/SampleBrowser;component/Images/Icons/Cut16.png", "Cut the selection from the document and put it on the Clipboard."));
			RibbonCommandUIManager.Register(System.Windows.Input.ApplicationCommands.Paste, 
				new RibbonCommandUIProvider("Paste", "pack://application:,,,/SampleBrowser;component/Images/Icons/Paste32.png", "pack://application:,,,/SampleBrowser;component/Images/Icons/Paste16.png", "Paste the contents of the Clipboard."));

            InitializeComponent();
        }
	}
}