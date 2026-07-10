using ActiproSoftware.Windows.Controls.Navigation.Serialization;
using ActiproSoftware.Windows.Serialization;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.NavigationBarLayout;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	private NavigationBarLayoutSerializer? _layoutSerializer;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		Loaded += OnLoaded;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The layout serializer.
	/// </summary>
	private NavigationBarLayoutSerializer LayoutSerializer {
		get {
			// Create a layout serializer that supports custom data
			if (_layoutSerializer is null) {
				_layoutSerializer = new NavigationBarLayoutSerializer();
				_layoutSerializer.ObjectSerialized += OnObjectSerialized;
				_layoutSerializer.CustomTypes.Add(typeof(CustomData));  // Register the custom data type
			}
			return _layoutSerializer;
		}
	}

	private void OnLoaded(object sender, RoutedEventArgs e) {
		// Initialize the layout textbox
		SaveLayout();
	}

	private void OnLoadLayoutButtonClick(object sender, RoutedEventArgs e) {
		LayoutSerializer.LoadFromString(layoutTextBox.Text, navBar);

		// If you wish to read the custom data from the layout, attach to the NavigationBarLayoutSerializer.ObjectDeserialized event
		//   using a handler like OnObjectSerialized but one that has code to read the custom data instead of write it
	}

	/// <summary>
	/// Occurs when an object is serialized into the layout.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnObjectSerialized(object? sender, ItemSerializationEventArgs e) {
		// Store some custom data in the layout... this is called every time an object is serialized
		if (e.Node is XmlNavigationBarLayout) {
			var data = new CustomData {
				StringValue = "Custom data object in root layout element, injected in this sample's code-behind"
			};
			e.Node.Tag = data;
		}
		else if (e.Item == mailPane)
			e.Node.Tag = "Custom data for Mail pane only";
	}

	private void OnSaveLayoutButtonClick(object sender, RoutedEventArgs e)
		=> SaveLayout();

	/// <summary>
	/// Saves the layout to a <see cref="TextBox"/>.
	/// </summary>
	private void SaveLayout()
		=> layoutTextBox.Text = LayoutSerializer.SaveToString(navBar);

}
