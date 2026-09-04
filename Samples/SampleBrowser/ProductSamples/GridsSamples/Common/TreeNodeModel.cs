namespace ActiproSoftware.ProductSamples.GridsSamples.Common;

/// <summary>
/// Provides a common implementation of a tree node model.
/// </summary>
[ContentProperty(nameof(Children))]
public class TreeNodeModel : ObservableObjectBase {

	private ICommand? _defaultActionCommand;
	private ImageSource? _imageSource;
	private bool _isDraggable = true;
	private bool _isEditable;
	private bool _isEditing;
	private bool _isExpanded;
	private bool _isLoading;
	private bool _isSelectable = true;
	private bool _isSelected;
	private string _name = string.Empty;
	private object? _tag;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The collection of child nodes.
	/// </summary>
	public ObservableCollection<TreeNodeModel> Children { get; } = [];

	/// <summary>
	/// The default action command.
	/// </summary>
	public ICommand? DefaultActionCommand {
		get => _defaultActionCommand;
		set => SetProperty(ref _defaultActionCommand, value);
	}

	/// <summary>
	/// The <see cref="ImageSource"/> for an image to display on the node.
	/// </summary>
	public ImageSource? ImageSource {
		get => _imageSource;
		set => SetProperty(ref _imageSource, value);
	}

	/// <summary>
	/// Indicates whether the node is draggable.
	/// </summary>
	public bool IsDraggable {
		get => _isDraggable;
		set => SetProperty(ref _isDraggable, value);
	}

	/// <summary>
	/// Indicates whether the node is editable.
	/// </summary>
	public bool IsEditable {
		get => _isEditable;
		set => SetProperty(ref _isEditable, value);
	}

	/// <summary>
	/// Indicates whether the node is currently being edited.
	/// </summary>
	public bool IsEditing {
		get => _isEditing;
		set => SetProperty(ref _isEditing, value);
	}

	/// <summary>
	/// Indicates whether the node is expanded.
	/// </summary>
	public bool IsExpanded {
		get => _isExpanded;
		set => SetProperty(ref _isExpanded, value);
	}

	/// <summary>
	/// Indicates whether the node is currently loading children asynchronously.
	/// </summary>
	public bool IsLoading {
		get => _isLoading;
		set => SetProperty(ref _isLoading, value);
	}

	/// <summary>
	/// Indicates whether the node is capable of being selected.
	/// </summary>
	public bool IsSelectable {
		get => _isSelectable;
		set => SetProperty(ref _isSelectable, value);
	}

	/// <summary>
	/// Indicates whether the node is selected.
	/// </summary>
	public bool IsSelected {
		get => _isSelected;
		set => SetProperty(ref _isSelected, value);
	}

	/// <summary>
	/// The name of the node.
	/// </summary>
	public string Name {
		get => _name;
		set {
			// Prevent the name from being cleared
			if (!string.IsNullOrEmpty(value))
				SetProperty(ref _name, value);
		}
	}

	/// <summary>
	/// Custom data for the node.
	/// </summary>
	public object? Tag {
		get => _tag;
		set => SetProperty(ref _tag, value);
	}

	/// <inheritdoc/>
	public override string ToString()
		=> string.Format("{0}[Name={1}]", GetType().Name, Name);

}
