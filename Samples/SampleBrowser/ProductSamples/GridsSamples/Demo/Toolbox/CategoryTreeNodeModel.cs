using ActiproSoftware.ProductSamples.GridsSamples.Common;
using System.Collections.Specialized;

namespace ActiproSoftware.ProductSamples.GridsSamples.Demo.Toolbox;

/// <summary>
/// Provides a tree node model implementation for a toolbox category.
/// </summary>
public class CategoryTreeNodeModel : ToolboxTreeNodeModel {

	private EmptyPlaceholderTreeNodeModel? _emptyPlaceholderModel = null;
	private bool _ignoreChildrenCollectionChanged = false;
	private bool _isEmptyPlaceholderDirty = true;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public CategoryTreeNodeModel() {
		SyncEmptyPlaceholder();

		// Listen for changes in the children
		Children.CollectionChanged += OnChildrenCollectionChanged;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Adds the placeholder to an empty category.
	/// </summary>
	private void AddEmptyPlaceholder() {
		// Quit if the placeholder already exists
		if ((_emptyPlaceholderModel is not null) && Children.Contains(_emptyPlaceholderModel))
			return;

		_emptyPlaceholderModel ??= CreateEmptyPlaceholderTreeNodeModel();

		// Ignore changes to the children collection caused by the addition of the placeholder
		_ignoreChildrenCollectionChanged = true;
		try {
			Children.Add(_emptyPlaceholderModel);
		}
		finally {
			_ignoreChildrenCollectionChanged = false;
		}
	}

	/// <summary>
	/// Tests if the category is empty (i.e., contains no controls).
	/// </summary>
	private bool IsEmpty {
		get {
			// Look for any child that is not an empty placeholder
			if (Children.Any(m => !IsEmptyPlaceholder(m)))
				return false;
			return true;

			// Tests if a model is an empty placeholder
			static bool IsEmptyPlaceholder(TreeNodeModel model)
				=> model is EmptyPlaceholderTreeNodeModel;
		}
	}

	/// <summary>
	/// This method is called when the <see cref="TreeNodeModel.Children"/> collection changes.
	/// </summary>
	/// <param name="sender">A references to the collection.</param>
	/// <param name="e">The event args.</param>
	private void OnChildrenCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		// Ignore if change processing is suppressed
		if (_ignoreChildrenCollectionChanged)
			return;

		// Synchronize the empty placeholder in response to changes in the child collection.
		//   An exception will be thrown if children are modified from the CollectionChanged event,
		//   so this call must be invoked asynchronously.
		_isEmptyPlaceholderDirty = true;
		DispatcherBeginInvoke(SyncEmptyPlaceholder);
	}

	/// <summary>
	/// Removes the placeholder from an empty category.
	/// </summary>
	private void RemoveEmptyPlaceholder() {
		// Quit if the placeholder is not present
		if (_emptyPlaceholderModel is null)
			return;

		// Ignore changes to the children collection caused by the removal of the placeholder
		_ignoreChildrenCollectionChanged = true;
		try {
			Children.Remove(_emptyPlaceholderModel);
			_emptyPlaceholderModel = null;
		}
		finally {
			_ignoreChildrenCollectionChanged = false;
		}
	}

	/// <summary>
	/// Synchronizes the presence of the empty placeholder based on the current
	/// collection of children.
	/// </summary>
	private void SyncEmptyPlaceholder() {
		// Ignore if no change is necessary
		if (!_isEmptyPlaceholderDirty)
			return;

		// Only show the empty placeholder when the category is empty
		if (IsEmpty)
			AddEmptyPlaceholder();
		else
			RemoveEmptyPlaceholder();

		_isEmptyPlaceholderDirty = false;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates a new instance of the model used for the placeholder of a category when it has no controls as children.
	/// </summary>
	protected virtual EmptyPlaceholderTreeNodeModel CreateEmptyPlaceholderTreeNodeModel()
		=> new();

}
