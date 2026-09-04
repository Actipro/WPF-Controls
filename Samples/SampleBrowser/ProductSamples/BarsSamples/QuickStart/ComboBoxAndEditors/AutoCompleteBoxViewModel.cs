using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.ComboBoxAndEditors;

/// <summary>
/// Represents a view model for an auto-complete box control within a bar control.
/// </summary>
public class AutoCompleteBoxViewModel : BarKeyedObjectViewModelBase {

	private string? _description;
	private bool _hasClearButton;
	private IEnumerable? _itemsSource;
	private string? _itemsSourceDisplayMemberPath;
	private string? _itemsSourceTextMemberPath;
	private string? _label;
	private string? _popupHeader;
	private double _requestedWidth = 110.0;
	private object? _selectedItem;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public AutoCompleteBoxViewModel()  // Parameterless constructor required for XAML support
		: this(key: null) { }

	/// <summary>
	/// Initializes a new instance of the class with the specified key.  The label is auto-generated.
	/// </summary>
	/// <param name="key">A string that uniquely identifies the control.</param>
	public AutoCompleteBoxViewModel(string? key)
		: this(key, label: null) { }

	/// <summary>
	/// Initializes a new instance of the class with the specified key and label.
	/// </summary>
	/// <param name="key">A string that uniquely identifies the control.</param>
	/// <param name="label">The text label to display, which is auto-generated from the <paramref name="key"/> if <c>null</c>.</param>
	public AutoCompleteBoxViewModel(string? key, string? label)
		: base(key) {

		_label = label ?? BarControlService.LabelGenerator.FromKey(key);
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The text label to display.
	/// </summary>
	public string? Label {
		get => _label;
		set => SetProperty(ref _label, value);
	}

	/// <summary>
	/// The text description to display in screen tips.
	/// </summary>
	public string? Description {
		get => _description;
		set => SetProperty(ref _description, value);
	}

	/// <summary>
	/// Indicates if the clear button is displayed.
	/// </summary>
	public bool HasClearButton {
		get => _hasClearButton;
		set => SetProperty(ref _hasClearButton, value);
	}

	/// <summary>
	/// The source of items to be auto-completed.
	/// </summary>
	public IEnumerable? ItemsSource {
		get => _itemsSource;
		set => SetProperty(ref _itemsSource, value);
	}

	/// <summary>
	/// A path to a value on the source object to serve as the visual representation of the object.
	/// </summary>
	public string? ItemsSourceDisplayMemberPath {
		get => _itemsSourceDisplayMemberPath;
		set => SetProperty(ref _itemsSourceDisplayMemberPath, value);
	}

	/// <summary>
	/// The property path that is used to get the value for display in the text box portion of the control, when an item is selected.
	/// </summary>
	public string? ItemsSourceTextMemberPath {
		get => _itemsSourceTextMemberPath;
		set => SetProperty(ref _itemsSourceTextMemberPath, value);
	}

	/// <summary>
	/// The header to be displayed in the popup.
	/// </summary>
	public string? PopupHeader {
		get => _popupHeader;
		set => SetProperty(ref _popupHeader, value);
	}

	/// <summary>
	/// The requested width of the control.
	/// </summary>
	/// <value>
	/// The default value is <c>110</c>.
	/// </value>
	public double RequestedWidth {
		get => _requestedWidth;
		set => SetProperty(ref _requestedWidth, value);
	}

	/// <summary>
	/// The currently selected item.
	/// </summary>
	public object? SelectedItem {
		get => _selectedItem;
		set => SetProperty(ref _selectedItem, value);
	}

}
