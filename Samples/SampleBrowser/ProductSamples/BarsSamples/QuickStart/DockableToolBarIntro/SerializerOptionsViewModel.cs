using ActiproSoftware.Windows.Controls.Bars;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.DockableToolBarIntro;

/// <summary>
/// Defines a view model for turning options on or off that will be used during dockable toolbar serialization.
/// </summary>
public class SerializerOptionsViewModel : ObservableObjectBase {

	private bool _floatingLocation = true;
	private bool _isFloating = true;
	private bool _isVisible = true;
	private bool _lineIndex = true;
	private bool _offset = true;
	private bool _placement = true;
	private bool _sortOrder = true;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="DockableToolBarSerializerOptions"/> that are reflected by the current configuration.
	/// </summary>
	public DockableToolBarSerializerOptions CreateOptions() {
		// Start with no options enabled
		var options = DockableToolBarSerializerOptions.None;

		// Add each option that is currently turned on
		if (FloatingLocation)
			options |= DockableToolBarSerializerOptions.FloatingLocation;
		if (IsFloating)
			options |= DockableToolBarSerializerOptions.IsFloating;
		if (IsVisible)
			options |= DockableToolBarSerializerOptions.IsVisible;
		if (LineIndex)
			options |= DockableToolBarSerializerOptions.LineIndex;
		if (Offset)
			options |= DockableToolBarSerializerOptions.Offset;
		if (Placement)
			options |= DockableToolBarSerializerOptions.Placement;
		if (SortOrder)
			options |= DockableToolBarSerializerOptions.SortOrder;

		return options;
	}

	/// <summary>
	/// Indicates if the option will be processed when serializing or deserializing.
	/// </summary>
	public bool FloatingLocation {
		get => _floatingLocation;
		set => SetProperty(ref _floatingLocation, value);
	}

	/// <summary>
	/// Indicates if the option will be processed when serializing or deserializing.
	/// </summary>
	public bool IsFloating {
		get => _isFloating;
		set => SetProperty(ref _isFloating, value);
	}

	/// <summary>
	/// Indicates if the option will be processed when serializing or deserializing.
	/// </summary>
	public bool IsVisible {
		get => _isVisible;
		set => SetProperty(ref _isVisible, value);
	}

	/// <summary>
	/// Indicates if the option will be processed when serializing or deserializing.
	/// </summary>
	public bool LineIndex {
		get => _lineIndex;
		set => SetProperty(ref _lineIndex, value);
	}

	/// <summary>
	/// Indicates if the option will be processed when serializing or deserializing.
	/// </summary>
	public bool Offset {
		get => _offset;
		set => SetProperty(ref _offset, value);
	}

	/// <summary>
	/// Indicates if the option will be processed when serializing or deserializing.
	/// </summary>
	public bool Placement {
		get => _placement;
		set => SetProperty(ref _placement, value);
	}

	/// <summary>
	/// Indicates if the option will be processed when serializing or deserializing.
	/// </summary>
	public bool SortOrder {
		get => _sortOrder;
		set => SetProperty(ref _sortOrder, value);
	}

}
