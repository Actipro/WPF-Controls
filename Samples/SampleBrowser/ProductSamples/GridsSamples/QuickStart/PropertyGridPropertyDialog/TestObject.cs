namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridPropertyDialog;

/// <summary>
/// Represents a test object for demonstration purposes.
/// </summary>
public class TestObject : ObservableObjectBase {

	private string _editablePath = @"C:\Documents\Foo.html";
	private string _name = "Foo";
	private string _uneditablePath = @"C:\Documents\Foo.css";

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The editable path.
	/// </summary>
	[Description("A file path that can be typed in or selected via the ellipses button.")]
	public string EditablePath {
		get => _editablePath;
		set => SetProperty(ref _editablePath, value);
	}

	/// <summary>
	/// The name.
	/// </summary>
	[Description("The name of the item.")]
	public string Name {
		get => _name;
		set => SetProperty(ref _name, value);
	}

	/// <summary>
	/// Gets a read-only path.
	/// </summary>
	[Description("A file path whose property is read-only, but keeps the ellipses button enabled for full display.  This concept is useful for getter-only collection properties.")]
	public string ReadOnlyPath
		=> @"C:\Documents\Foo.js";

	/// <summary>
	/// The uneditable path.
	/// </summary>
	[Description("Another file path but one that can't be directly typed in, only selected via the ellipses button.")]
	public string UneditablePath {
		get => _uneditablePath;
		set => SetProperty(ref _uneditablePath, value);
	}

}
