using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.Controls.Editors;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.ComboBoxAndEditors;

/// <summary>
/// Represents an abstract base view model for an editbox control within a bar control.
/// </summary>
public abstract class PartEditBoxViewModelBase<T> : BarKeyedObjectViewModelBase {

	private string? _description;
	private T? _editorValue;
	private string? _label;
	private double _requestedWidth = 110.0;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	protected PartEditBoxViewModelBase()  // Parameterless constructor required for XAML support
		: this(key: null) { }

	/// <summary>
	/// Initializes a new instance of the class with the specified key.  The label is auto-generated.
	/// </summary>
	/// <param name="key">A string that uniquely identifies the control.</param>
	protected PartEditBoxViewModelBase(string? key)
		: this(key, label: null) { }

	/// <summary>
	/// Initializes a new instance of the class with the specified key and label.
	/// </summary>
	/// <param name="key">A string that uniquely identifies the control.</param>
	/// <param name="label">The text label to display, which is auto-generated from the <paramref name="key"/> if <c>null</c>.</param>
	protected PartEditBoxViewModelBase(string? key, string? label)
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
	/// The text description to display in screen tips.
	/// </summary>
	public T? Value {
		get => _editorValue;
		set => SetProperty(ref _editorValue, value);
	}

}

/// <summary>
/// Represents a view model for a <see cref="ColorEditBox"/> control within a bar control.
/// </summary>
public class ColorEditBoxViewModel : PartEditBoxViewModelBase<Color?> {

	/// <summary>
	/// Initializes a new instance of the class with the specified key.  The label is auto-generated.
	/// </summary>
	/// <param name="key">A string that uniquely identifies the control.</param>
	public ColorEditBoxViewModel(string key) : base(key) { }

}

/// <summary>
/// Represents a view model for a <see cref="DateEditBox"/> control within a bar control.
/// </summary>
public class DateEditBoxViewModel : PartEditBoxViewModelBase<DateTime?> {

	/// <summary>
	/// Initializes a new instance of the class with the specified key.  The label is auto-generated.
	/// </summary>
	/// <param name="key">A string that uniquely identifies the control.</param>
	public DateEditBoxViewModel(string key) : base(key) { }

}

/// <summary>
/// Represents a view model for a <see cref="Int32EditBox"/> control within a bar control.
/// </summary>
public class Int32EditBoxViewModel : PartEditBoxViewModelBase<int?> {

	/// <summary>
	/// Initializes a new instance of the class with the specified key.  The label is auto-generated.
	/// </summary>
	/// <param name="key">A string that uniquely identifies the control.</param>
	public Int32EditBoxViewModel(string key) : base(key) { }

}
