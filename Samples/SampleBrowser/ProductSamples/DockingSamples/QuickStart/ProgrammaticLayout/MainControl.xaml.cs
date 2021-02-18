using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Docking;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.ProgrammaticLayout {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private int windowIndex;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			this.Loaded += (sender, e) => {
				if (dockSite.ToolWindows.Count == 0)
					this.CreateDockSite();
			};
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
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
			dockSite.PrimaryDockHost.Workspace.Content = mdiHost;

			// Add a couple tool windows attached to each other on the right that are 300px wide
			var toolWindowR1 = this.CreateToolWindow("DockedRight-1");
			toolWindowR1.ContainerDockedSize = new Size(300, 200);
			var toolWindowR2 = this.CreateToolWindow("DockedRight-2");
			toolWindowR1.Dock(dockSite, Side.Right);
			toolWindowR2.Attach(toolWindowR1);

			// Dock bottom
			var toolWindowB = this.CreateToolWindow("DockedBottom");
			toolWindowB.Dock(dockSite.PrimaryDockHost.Workspace, Side.Bottom);

			// Auto hide left
			var toolWindowAH = this.CreateToolWindow("Auto-Hidden");
			toolWindowAH.AutoHide(Side.Left);

			// Floating
			var toolWindowU = this.CreateToolWindow("Floating");
			toolWindowU.ContainerDockedSize = new Size(400, 200);
			toolWindowU.Float(new Point(400, 300));

			// Add three documents
			var documentWindow1 = this.CreateDocumentWindow("Upper-1");
			documentWindow1.Open();
			var documentWindow2 = this.CreateDocumentWindow("Upper-2");
			documentWindow2.Open();
			var documentWindow3 = this.CreateDocumentWindow("Lower");
			documentWindow3.Open();
			documentWindow3.MoveToNewHorizontalContainer();

			// Make sure new tabs are inserted before existing tabs again
			dockSite.AreNewTabsInsertedBeforeExistingTabs = true;
		}
		
		/// <summary>
		/// Creates a new <see cref="DocumentWindow"/>.
		/// </summary>
		/// <param name="title">The title to use.</param>
		/// <returns>The <see cref="DocumentWindow"/> that was created.</returns>
		private DocumentWindow CreateDocumentWindow(string title) {
			// Create a TextBox
			var textBox = new TextBox();
			textBox.BorderThickness = new Thickness();
			textBox.TextWrapping = TextWrapping.Wrap;
			textBox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

			// Initialize
			textBox.Text = String.Format("Document window {0} created at {1}.", ++windowIndex, DateTime.Now);
			var name = String.Format("DocumentWindow{0}", windowIndex);

			// Create the window (using this constructor registers the document window with the DockSite)
			var window = new DocumentWindow(dockSite, name, title, 
				new BitmapImage(new Uri("/Images/Icons/TextDocument16.png", UriKind.Relative)), textBox);

			return window;
		}
		
		/// <summary>
		/// Creates a new <see cref="ToolWindow"/>.
		/// </summary>
		/// <param name="title">The title to use.</param>
		/// <returns>The <see cref="ToolWindow"/> that was created.</returns>
		private ToolWindow CreateToolWindow(string title) {
			// Create a TextBox
			var textBox = new TextBox();
			textBox.BorderThickness = new Thickness();
			textBox.TextWrapping = TextWrapping.Wrap;
			textBox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

			// Initialize
			textBox.Text = String.Format("Tool window {0} created at {1}.", ++windowIndex, DateTime.Now);
			var name = String.Format("ToolWindow{0}", windowIndex);

			// Create the window (using this constructor registers the tool window with the DockSite)
			var window = new ToolWindow(dockSite, name, title, 
				new BitmapImage(new Uri("/Images/Icons/TextDocument16.png", UriKind.Relative)), textBox);

			return window;
		}
		
	}

}
