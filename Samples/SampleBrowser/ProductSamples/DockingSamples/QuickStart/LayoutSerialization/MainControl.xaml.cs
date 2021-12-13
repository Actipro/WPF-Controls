using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ActiproSoftware.Windows.Controls.Docking;
using ActiproSoftware.Windows.Controls.Docking.Serialization;

#if WPF
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;
#endif

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.LayoutSerialization {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private static DockSiteSerializationBehavior layoutSerializationBehavior = DockSiteSerializationBehavior.ToolWindowsOnly;
		private static string layoutXml = string.Empty;
		private static DockingWindowDeserializationBehavior windowDeserializationBehavior = DockingWindowDeserializationBehavior.Discard;

		private string defaultLayoutXml = string.Empty;
		private DockSiteLayoutSerializer layoutSerializer;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			// Create a layout serialization and attach a DockingWindowDeserializing event handler, which is called when deserializing all windows (including ones that may not exist yet)
			layoutSerializer = new DockSiteLayoutSerializer();
			layoutSerializer.DocumentWindowDeserializationBehavior = windowDeserializationBehavior;
			layoutSerializer.SerializationBehavior = layoutSerializationBehavior;
			layoutSerializer.ToolWindowDeserializationBehavior = windowDeserializationBehavior;
			layoutSerializer.DockingWindowDeserializing += this.OnLayoutSerializerDockingWindowDeserializing;

			InitializeComponent();

			// Update the UI
			saveToolWindowLayoutOnlyMenuItem.IsChecked = (layoutSerializationBehavior == DockSiteSerializationBehavior.ToolWindowsOnly);
			layoutXmlTextBox.Text = layoutXml;
			this.UpdateWindowDeserializationBehavior();

			// Save the default layout
			this.SaveLayout(true);

			// Load or save the normal layout depending on if this sample has already been opened
			if (!string.IsNullOrEmpty(layoutXml))
				this.LoadLayout(false);
			else
				this.SaveLayout(false);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes the "Programmatic Tool Window 1" tool window.
		/// </summary>
		/// <param name="toolWindow">The tool window.</param>
		private void InitializeProgrammaticToolWindow1(ToolWindow toolWindow) {
			if (toolWindow == null)
				throw new ArgumentNullException("toolWindow");

			// Create the tool window content
			var textBox = new TextBox() {
				BorderThickness = new Thickness(),
				IsReadOnly = true,
				Text = "This ToolWindow was programmatically created in the code-behind.",
				TextWrapping = TextWrapping.Wrap
			};
			
			toolWindow.Name = "programmaticToolWindow1";
			toolWindow.Title = "Programmatic ToolWindow 1";
			toolWindow.ImageSource = new BitmapImage(new Uri("/Images/Icons/Properties16.png", UriKind.Relative));
			toolWindow.Content = textBox;
		}

		/// <summary>
		/// Loads the layout from a <see cref="TextBox"/>.
		/// </summary>
		/// <param name="loadDefaultLayout">Whether to load the default layout.</param>
		private void LoadLayout(bool loadDefaultLayout) {
			if (loadDefaultLayout) {
				if (!string.IsNullOrEmpty(defaultLayoutXml))
					layoutSerializer.LoadFromString(defaultLayoutXml, dockSite);
			}
			else if (!string.IsNullOrEmpty(layoutXml))
				layoutSerializer.LoadFromString(layoutXml, dockSite);
		}
		
		/// <summary>
		/// Occurs when the <c>Activate.Programmatic ToolWindow 1</c> menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnActivateProgrammaticToolWindow1Click(object sender, RoutedEventArgs e) {
			var toolWindow = dockSite.ToolWindows["programmaticToolWindow1"];
			if (toolWindow == null) {
				// Create, initialize, and register the tool window
				toolWindow = new ToolWindow();
				this.InitializeProgrammaticToolWindow1(toolWindow);
				this.dockSite.ToolWindows.Add(toolWindow);

				// Change the menu item's header
				activeProgrammaticToolWindow1.Header = "Activate Programmatic ToolWindow 1";
			}

			toolWindow.Activate();
		}

		/// <summary>
		/// Occurs when the <c>Activate.Programmatic ToolWindow 2</c> menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnActivateProgrammaticToolWindow2Click(object sender, RoutedEventArgs e) {
			var toolWindow = dockSite.ToolWindows["programmaticToolWindow2"];
			if (toolWindow == null) {
				// Create and register the tool window
				toolWindow = new CustomToolWindow() {
					Name = "programmaticToolWindow2"
				};
				dockSite.ToolWindows.Add(toolWindow);

				// Change the menu item's header
				activeProgrammaticToolWindow2.Header = "Activate Programmatic ToolWindow 2";
			}

			toolWindow.Activate();
		}

		/// <summary>
		/// Handles the <c>DockingWindowDeserializing</c> event of the <see cref="layoutSerializer"/> control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="DockingWindowDeserializingEventArgs"/> instance containing the event data.</param>
		private void OnLayoutSerializerDockingWindowDeserializing(object sender, DockingWindowDeserializingEventArgs e) {
			// If windows are auto-creating...
			if ((windowDeserializationBehavior == DockingWindowDeserializationBehavior.AutoCreate) && (e.Window != null)) {
				// The e.Node property contains the XML data and the e.Window property contains the associated DocumentWindow or ToolWindow, if any...
				//   The window may have been retrieved from the DockSite, or automatically created (when using DockingWindowDeserializationBehavior.AutoCreate)
				if (e.Node.Name == "programmaticToolWindow1") {
					this.InitializeProgrammaticToolWindow1(e.Window as ToolWindow);

					// Change the menu item's header
					activeProgrammaticToolWindow1.Header = "Activate Programmatic ToolWindow 1";
				}
				else if (e.Node.Name == "programmaticToolWindow2") {
					// NOTE: We don't need to initialize "programmaticToolWindow2", because it is a custom ToolWindow that sets the appropriate properties when constructed.

					// Change the menu item's header
					activeProgrammaticToolWindow2.Header = "Activate Programmatic ToolWindow 2";
				}
			}
		}

		/// <summary>
		/// Occurs when the menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnWindowDeserializationOptionMenuItemClick(object sender, RoutedEventArgs e) {
			if (sender == discardMenuItem)
				windowDeserializationBehavior = DockingWindowDeserializationBehavior.Discard;
			else if (sender == autoCreateMenuItem)
				windowDeserializationBehavior = DockingWindowDeserializationBehavior.AutoCreate;
			else if (sender == lazyLoadMenuItem)
				windowDeserializationBehavior = DockingWindowDeserializationBehavior.LazyLoad;

			this.UpdateWindowDeserializationBehavior();
		}
		
		/// <summary>
		/// Occurs when the menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnLoadDefaultLayoutMenuItemClick(object sender, RoutedEventArgs e) {
			this.LoadLayout(true);
			MessageBox.Show("Default layout XML loaded.", "Default Layout Load");
		}

		/// <summary>
		/// Occurs when the menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnLoadLayoutMenuItemClick(object sender, RoutedEventArgs e) {
			this.LoadLayout(false);
			MessageBox.Show("Layout XML loaded from static member variable.", "Layout Load");
		}

		/// <summary>
		/// Occurs when the menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnSaveLayoutMenuItemClick(object sender, RoutedEventArgs e) {
			this.SaveLayout(false);
			MessageBox.Show("Layout XML saved to static member variable and displayed in document.", "Layout Save");
		}
		
		/// <summary>
		/// Saves the layout to a <see cref="TextBox"/>.
		/// </summary>
		/// <param name="saveDefaultLayout">Whether to save the default layout.</param>
		private void SaveLayout(bool saveDefaultLayout) {
			layoutSerializationBehavior = (saveToolWindowLayoutOnlyMenuItem.IsChecked ? DockSiteSerializationBehavior.ToolWindowsOnly : DockSiteSerializationBehavior.All);
			layoutSerializer.SerializationBehavior = layoutSerializationBehavior;
			
			if (saveDefaultLayout)
				defaultLayoutXml = layoutSerializer.SaveToString(dockSite);
			else {
				layoutXml = layoutSerializer.SaveToString(dockSite);
				layoutXmlTextBox.Text = layoutXml;
			}
		}

		/// <summary>
		/// Updates the UI of window deserialization behavior.
		/// </summary>
		private void UpdateWindowDeserializationBehavior() {
			discardMenuItem.IsChecked = (windowDeserializationBehavior == DockingWindowDeserializationBehavior.Discard);
			autoCreateMenuItem.IsChecked = (windowDeserializationBehavior == DockingWindowDeserializationBehavior.AutoCreate);
			lazyLoadMenuItem.IsChecked = (windowDeserializationBehavior == DockingWindowDeserializationBehavior.LazyLoad);

			layoutSerializer.DocumentWindowDeserializationBehavior = windowDeserializationBehavior;
			layoutSerializer.ToolWindowDeserializationBehavior = windowDeserializationBehavior;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Notifies the UI that it has been unloaded.
		/// </summary>
		public override void NotifyUnloaded() {
			this.SaveLayout(false);
		}
		
	}

}
