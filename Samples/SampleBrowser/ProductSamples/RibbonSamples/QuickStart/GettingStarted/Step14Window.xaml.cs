using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.Controls.Ribbon.Input;
using System.Windows.Input;

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
			RibbonCommandUIManager.Register(ApplicationCommands.Copy, 
				new RibbonCommandUIProvider("Copy", imageSourceLarge: null, ImageLoader.GetIcon("Copy16.png"), "Copy the selection and put it on the Clipboard."));
			RibbonCommandUIManager.Register(ApplicationCommands.Cut, 
				new RibbonCommandUIProvider("Cut", imageSourceLarge: null, ImageLoader.GetIcon("Cut16.png"), "Cut the selection from the document and put it on the Clipboard."));
			RibbonCommandUIManager.Register(ApplicationCommands.Paste, 
				new RibbonCommandUIProvider("Paste", ImageLoader.GetIcon("Paste32.png"), ImageLoader.GetIcon("Paste16.png"), "Paste the contents of the Clipboard."));

            InitializeComponent();
        }
	}
}