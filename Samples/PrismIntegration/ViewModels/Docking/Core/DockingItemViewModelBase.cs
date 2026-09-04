namespace ActiproSoftware.Windows.PrismIntegration.ViewModels;

/// <summary>
/// Represents a base class for all docking item view-models.
/// </summary>
public abstract class DockingItemViewModelBase : ObservableObjectBase {

	private string? _description;
	private ImageSource? _imageSource;
	private bool _isActive;
	private bool _isOpen;
	private bool _isSelected;
	private string? _serializationId;
	private string? _title;
	private string? _windowGroupName;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The description associated with the view-model.
	/// </summary>
	public string? Description {
		get => _description;
		set => SetProperty(ref _description, value);
	}

	/// <summary>
	/// The image associated with the view-model.
	/// </summary>
	public ImageSource? ImageSource {
		get => _imageSource;
		set => SetProperty(ref _imageSource, value);
	}

	/// <summary>
	/// Indicates whether the view is currently active.
	/// </summary>
	public bool IsActive {
		get => _isActive;
		set => SetProperty(ref _isActive, value);
	}

	/// <summary>
	/// Indicates whether the view is currently open.
	/// </summary>
	public bool IsOpen {
		get => _isOpen;
		set => SetProperty(ref _isOpen, value);
	}

	/// <summary>
	/// Indicates whether the view is currently selected in its parent container.
	/// </summary>
	public bool IsSelected {
		get => _isSelected;
		set => SetProperty(ref _isSelected, value);
	}

	/// <summary>
	/// Indicates whether the container generated for this view model should be a tool window.
	/// </summary>
	public abstract bool IsTool { get; }

	/// <summary>
	/// The name that uniquely identifies the view-model for layout serialization.
	/// </summary>
	public string? SerializationId {
		get => _serializationId;
		set => SetProperty(ref _serializationId, value);
	}

	/// <summary>
	/// The title associated with the view-model.
	/// </summary>
	public string? Title {
		get => _title;
		set => SetProperty(ref _title, value);
	}

	/// <summary>
	/// The window group name associated with the view-model.
	/// </summary>
	public string? WindowGroupName {
		get => _windowGroupName;
		set => SetProperty(ref _windowGroupName, value);
	}

}
