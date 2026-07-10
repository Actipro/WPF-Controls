using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Docking;
using System.Windows.Media.Imaging;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.ProgrammaticLayout;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	private int _windowIndex;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		Loaded += (sender, e) => {
			if (dockSite.ToolWindows.Count == 0)
				CreateDockSite();
		};
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates the <see cref="DockSite"/>.
	/// </summary>
	private void CreateDockSite() {
		// Make sure tabs are programmatically added to the end
		dockSite.AreNewTabsInsertedBeforeExistingTabs = false;

		// Add a Workspace
		dockSite.Child = new Workspace();

		// Add a TabbedMdiHost
		var mdiHost = new TabbedMdiHost();
		dockSite.PrimaryDockHost!.Workspace!.Content = mdiHost;

		// Add a couple tool windows attached to each other on the right that are 300px wide
		var toolWindowR1 = CreateToolWindow("DockedRight-1");
		toolWindowR1.ContainerDockedSize = new Size(300, 200);
		var toolWindowR2 = CreateToolWindow("DockedRight-2");
		toolWindowR1.Dock(dockSite, Side.Right);
		toolWindowR2.Attach(toolWindowR1);

		// Dock bottom
		var toolWindowB = CreateToolWindow("DockedBottom");
		toolWindowB.Dock(dockSite.PrimaryDockHost.Workspace, Side.Bottom);

		// Auto hide left
		var toolWindowAH = CreateToolWindow("Auto-Hidden");
		toolWindowAH.AutoHide(Side.Left);

		// Floating
		var toolWindowU = CreateToolWindow("Floating");
		toolWindowU.ContainerDockedSize = new Size(400, 200);
		toolWindowU.Float(new Point(400, 300));

		// Add three documents
		var documentWindow1 = CreateDocumentWindow("Upper-1");
		documentWindow1.Open();
		var documentWindow2 = CreateDocumentWindow("Upper-2");
		documentWindow2.Open();
		var documentWindow3 = CreateDocumentWindow("Lower");
		documentWindow3.Open();
		documentWindow3.MoveToNewHorizontalContainer();

		// Make sure new tabs are inserted before existing tabs again
		dockSite.AreNewTabsInsertedBeforeExistingTabs = true;
	}

	/// <summary>
	/// Creates a new <see cref="DocumentWindow"/>.
	/// </summary>
	/// <param name="title">The title to use.</param>
	private DocumentWindow CreateDocumentWindow(string title) {
		// Create a TextBox
		var textBox = new TextBox {
			BorderThickness = new Thickness(),
			TextWrapping = TextWrapping.Wrap,
			VerticalScrollBarVisibility = ScrollBarVisibility.Auto,

			// Initialize
			Text = string.Format("Document window {0} created at {1}.", ++_windowIndex, DateTime.Now)
		};
		var name = string.Format("DocumentWindow{0}", _windowIndex);

		// Create the window (using this constructor registers the document window with the DockSite)
		var window = new DocumentWindow(dockSite, name, title,
			new BitmapImage(new Uri("/Images/Icons/TextDocument16.png", UriKind.Relative)), textBox);

		return window;
	}

	/// <summary>
	/// Creates a new <see cref="ToolWindow"/>.
	/// </summary>
	/// <param name="title">The title to use.</param>
	private ToolWindow CreateToolWindow(string title) {
		// Create a TextBox
		var textBox = new TextBox {
			BorderThickness = new Thickness(),
			TextWrapping = TextWrapping.Wrap,
			VerticalScrollBarVisibility = ScrollBarVisibility.Auto,

			// Initialize
			Text = string.Format("Tool window {0} created at {1}.", ++_windowIndex, DateTime.Now)
		};
		var name = string.Format("ToolWindow{0}", _windowIndex);

		// Create the window (using this constructor registers the tool window with the DockSite)
		var window = new ToolWindow(dockSite, name, title,
			new BitmapImage(new Uri("/Images/Icons/TextDocument16.png", UriKind.Relative)), textBox);

		return window;
	}

}
