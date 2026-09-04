using System.Windows.Media.Imaging;
using ActiproSoftware.Windows.Controls.Docking;
using ActiproSoftware.Windows.Controls.Docking.Serialization;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.LayoutSerialization;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	private static DockSiteSerializationBehavior _layoutSerializationBehavior = DockSiteSerializationBehavior.ToolWindowsOnly;
	private static string _layoutXml = string.Empty;
	private static DockingWindowDeserializationBehavior _windowDeserializationBehavior = DockingWindowDeserializationBehavior.Discard;

	private string _defaultLayoutXml = string.Empty;
	private readonly DockSiteLayoutSerializer _layoutSerializer;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		// Create a layout serialization and attach a DockingWindowDeserializing event handler,
		//   which is called when deserializing all windows (including ones that may not exist yet)
		_layoutSerializer = new DockSiteLayoutSerializer {
			DocumentWindowDeserializationBehavior = _windowDeserializationBehavior,
			SerializationBehavior = _layoutSerializationBehavior,
			ToolWindowDeserializationBehavior = _windowDeserializationBehavior
		};
		_layoutSerializer.DockingWindowDeserializing += OnLayoutSerializerDockingWindowDeserializing;

		InitializeComponent();

		// Update the UI
		saveToolWindowLayoutOnlyMenuItem.IsChecked = (_layoutSerializationBehavior == DockSiteSerializationBehavior.ToolWindowsOnly);
		xmlDataEditor.Text = _layoutXml;
		UpdateWindowDeserializationBehavior();

		// Save the default layout
		SaveLayout(saveDefaultLayout: true);

		// Load or save the normal layout depending on if this sample has already been opened
		if (!string.IsNullOrEmpty(_layoutXml))
			LoadLayout(loadDefaultLayout: false);
		else
			SaveLayout(saveDefaultLayout: false);
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the "Programmatic Tool Window 1" tool window.
	/// </summary>
	/// <param name="toolWindow">The tool window.</param>
	private static void InitializeProgrammaticToolWindow1(ToolWindow toolWindow) {
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
		var layout = (loadDefaultLayout ? _defaultLayoutXml : _layoutXml);
		if (!string.IsNullOrEmpty(layout))
			_layoutSerializer.LoadFromString(layout, dockSite);
	}

	/// <summary>
	/// Occurs when the <c>Activate.Programmatic ToolWindow 1</c> menu item is clicked.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnActivateProgrammaticToolWindow1Click(object sender, RoutedEventArgs e) {
		var toolWindow = dockSite.ToolWindows["programmaticToolWindow1"];
		if (toolWindow is null) {
			// Create, initialize, and register the tool window
			toolWindow = new ToolWindow();
			InitializeProgrammaticToolWindow1(toolWindow);
			dockSite.ToolWindows.Add(toolWindow);

			// Change the menu item's header
			activeProgrammaticToolWindow1.Header = "Activate Programmatic ToolWindow 1";
		}

		toolWindow.Activate();
	}

	/// <summary>
	/// Occurs when the <c>Activate.Programmatic ToolWindow 2</c> menu item is clicked.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnActivateProgrammaticToolWindow2Click(object sender, RoutedEventArgs e) {
		var toolWindow = dockSite.ToolWindows["programmaticToolWindow2"];
		if (toolWindow is null) {
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
	/// Handles the <see cref="DockSiteLayoutSerializer.DockingWindowDeserializing"/> event.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnLayoutSerializerDockingWindowDeserializing(object? sender, DockingWindowDeserializingEventArgs e) {
		// If windows are auto-creating...
		if ((_windowDeserializationBehavior == DockingWindowDeserializationBehavior.AutoCreate) && (e.Window is not null)) {
			// The e.Node property contains the XML data and the e.Window property contains the associated DocumentWindow or ToolWindow, if any...
			//   The window may have been retrieved from the DockSite, or automatically created (when using DockingWindowDeserializationBehavior.AutoCreate)
			if (e.Node.Name == "programmaticToolWindow1") {
				InitializeProgrammaticToolWindow1((ToolWindow)e.Window);

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
	/// <param name="e">The event data.</param>
	private void OnWindowDeserializationOptionMenuItemClick(object sender, RoutedEventArgs e) {
		if (sender == discardMenuItem)
			_windowDeserializationBehavior = DockingWindowDeserializationBehavior.Discard;
		else if (sender == autoCreateMenuItem)
			_windowDeserializationBehavior = DockingWindowDeserializationBehavior.AutoCreate;
		else if (sender == lazyLoadMenuItem)
			_windowDeserializationBehavior = DockingWindowDeserializationBehavior.LazyLoad;

		UpdateWindowDeserializationBehavior();
	}

	/// <summary>
	/// Occurs when the menu item is clicked.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnLoadDefaultLayoutMenuItemClick(object sender, RoutedEventArgs e) {
		LoadLayout(loadDefaultLayout: true);
		MessageBox.Show("Default layout XML loaded.", "Default Layout Load");
	}

	/// <summary>
	/// Occurs when the menu item is clicked.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnLoadLayoutMenuItemClick(object sender, RoutedEventArgs e) {
		LoadLayout(loadDefaultLayout: false);
		MessageBox.Show("Layout XML loaded from static member variable.", "Layout Load");
	}

	/// <summary>
	/// Occurs when the menu item is clicked.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnSaveLayoutMenuItemClick(object sender, RoutedEventArgs e) {
		SaveLayout(saveDefaultLayout: false);
		MessageBox.Show("Layout XML saved to static member variable and displayed in document.", "Layout Save");
	}

	/// <summary>
	/// Saves the layout to a <see cref="TextBox"/>.
	/// </summary>
	/// <param name="saveDefaultLayout">Whether to save the default layout.</param>
	private void SaveLayout(bool saveDefaultLayout) {
		_layoutSerializationBehavior = (saveToolWindowLayoutOnlyMenuItem.IsChecked ? DockSiteSerializationBehavior.ToolWindowsOnly : DockSiteSerializationBehavior.All);
		_layoutSerializer.SerializationBehavior = _layoutSerializationBehavior;

		var layout = _layoutSerializer.SaveToString(dockSite);
		if (saveDefaultLayout)
			_defaultLayoutXml = layout;
		else {
			_layoutXml = layout;
			xmlDataEditor.Text = _layoutXml;
		}
	}

	/// <summary>
	/// Updates the UI of window deserialization behavior.
	/// </summary>
	private void UpdateWindowDeserializationBehavior() {
		discardMenuItem.IsChecked = (_windowDeserializationBehavior == DockingWindowDeserializationBehavior.Discard);
		autoCreateMenuItem.IsChecked = (_windowDeserializationBehavior == DockingWindowDeserializationBehavior.AutoCreate);
		lazyLoadMenuItem.IsChecked = (_windowDeserializationBehavior == DockingWindowDeserializationBehavior.LazyLoad);

		_layoutSerializer.DocumentWindowDeserializationBehavior = _windowDeserializationBehavior;
		_layoutSerializer.ToolWindowDeserializationBehavior = _windowDeserializationBehavior;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override void NotifyUnloaded()
		=> SaveLayout(saveDefaultLayout: false);

}
