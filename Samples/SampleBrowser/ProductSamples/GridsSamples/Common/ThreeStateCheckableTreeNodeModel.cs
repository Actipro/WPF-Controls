using System.Collections.Specialized;

namespace ActiproSoftware.ProductSamples.GridsSamples.Common;

/// <summary>
/// Provides a common implementation of a tree node model that supports three-state checking.
/// </summary>
public class ThreeStateCheckableTreeNodeModel : CheckableTreeNodeModel {

	private bool _isUpdatingIsChecked;
	private ThreeStateCheckableTreeNodeModel? _parent;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public ThreeStateCheckableTreeNodeModel() {
		Children.CollectionChanged += OnChildrenCollectionChanged;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnChildrenCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		if (e.OldItems is { } oldItems) {
			foreach (var oldChild in oldItems.OfType<ThreeStateCheckableTreeNodeModel>())
				oldChild.Parent = null;
		}

		if (e.NewItems is { } newItems) {
			foreach (var newChild in newItems.OfType<ThreeStateCheckableTreeNodeModel>())
				newChild.Parent = this;
		}
	}

	/// <summary>
	/// Applies recursive updates based on the checked state.
	/// </summary>
	private void ApplyRecursiveUpdates() {
		if (!IsChecked.HasValue)
			return;

		foreach (var checkableChild in Children.OfType<ThreeStateCheckableTreeNodeModel>())
			ApplyRecursiveUpdates(checkableChild, IsChecked);

		var ancestor = _parent;
		while (ancestor is not null) {
			var allChildrenChecked = true;
			var allChildrenUnchecked = true;

			foreach (var checkableChild in ancestor.Children.OfType<ThreeStateCheckableTreeNodeModel>()) {
				switch (checkableChild.IsChecked) {
					case true:
						allChildrenUnchecked = false;
						break;
					case false:
						allChildrenChecked = false;
						break;
					default:
						allChildrenChecked = false;
						allChildrenUnchecked = false;
						break;
				}
			}

			if (allChildrenChecked)
				ancestor.SetIsCheckedWithoutRecursion(true);
			else if (allChildrenUnchecked)
				ancestor.SetIsCheckedWithoutRecursion(false);
			else
				ancestor.SetIsCheckedWithoutRecursion(null);

			ancestor = ancestor.Parent;
		}
	}

	/// <summary>
	/// Applies recursive updates based on the checked state.
	/// </summary>
	/// <param name="node">The node to update.</param>
	/// <param name="newValue">The new value.</param>
	private static void ApplyRecursiveUpdates(ThreeStateCheckableTreeNodeModel node, bool? newValue) {
		if ((node is not null) && (node.IsChecked != newValue)) {
			node.SetIsCheckedWithoutRecursion(newValue);

			foreach (var checkableChild in node.Children.OfType<ThreeStateCheckableTreeNodeModel>())
				ApplyRecursiveUpdates(checkableChild, newValue);
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void OnPropertyChanged(PropertyChangedEventArgs e) {
		base.OnPropertyChanged(e);

		switch (e.PropertyName) {
			case nameof(IsChecked):
				if (!_isUpdatingIsChecked)
					ApplyRecursiveUpdates();
				break;
		}
	}

	/// <summary>
	/// The parent node, whose reference is needed for check updates that affect the ancestor nodes.
	/// </summary>
	public ThreeStateCheckableTreeNodeModel? Parent {
		get => _parent;
		private set => SetProperty(ref _parent, value);
	}

	/// <summary>
	/// Sets the <see cref="CheckableTreeNodeModel.IsChecked"/> property without recursion.
	/// </summary>
	/// <param name="newValue">The new value.</param>
	public void SetIsCheckedWithoutRecursion(bool? newValue) {
		try {
			_isUpdatingIsChecked = true;
			IsChecked = newValue;
		}
		finally {
			_isUpdatingIsChecked = false;
		}
	}

}
