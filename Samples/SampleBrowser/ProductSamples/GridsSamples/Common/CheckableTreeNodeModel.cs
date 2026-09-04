namespace ActiproSoftware.ProductSamples.GridsSamples.Common;

/// <summary>
/// Provides a common implementation of a tree node model that supports checking.
/// </summary>
public class CheckableTreeNodeModel : TreeNodeModel {

	private bool _isCheckable;
	private bool? _isChecked = false;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Indicates whether the node is checkable.
	/// </summary>
	public bool IsCheckable {
		get => _isCheckable;
		set => SetProperty(ref _isCheckable, value);
	}

	/// <summary>
	/// Indicates whether the node is checked.
	/// </summary>
	public bool? IsChecked {
		get => _isChecked;
		set => SetProperty(ref _isChecked, value);
	}

}
