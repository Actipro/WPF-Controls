using System.Collections.Specialized;
using System.ComponentModel;

namespace ActiproSoftware.ProductSamples.GridsSamples.Common {
	
	/// <summary>
	/// Provides a common implementation of a tree node model that supports three-state checking.
	/// </summary>
	public class ThreeStateCheckableTreeNodeModel : CheckableTreeNodeModel {
		
		private bool								isUpdatingIsChecked;
		private ThreeStateCheckableTreeNodeModel	parent;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <see cref="ThreeStateCheckableTreeNodeModel"/> class.
		/// </summary>
		public ThreeStateCheckableTreeNodeModel() {
			this.Children.CollectionChanged += OnChildrenCollectionChanged;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the collection is changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> containing data related to this event.</param>
		private void OnChildrenCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
			if (e != null) {
				if (e.OldItems != null) {
					foreach (var oldItem in e.OldItems) {
						var oldChild = oldItem as ThreeStateCheckableTreeNodeModel;
						if (oldChild != null)
							oldChild.Parent = null;
					}
				}

				if (e.NewItems != null) {
					foreach (var newItem in e.NewItems) {
						var newChild = newItem as ThreeStateCheckableTreeNodeModel;
						if (newChild != null)
							newChild.Parent = this;
					}
				}
			}
		}

		/// <summary>
		/// Applies recursive updates based on the checked state.
		/// </summary>
		private void ApplyRecursiveUpdates() {
			if (!this.IsChecked.HasValue)
				return;

			foreach (var child in this.Children) {
				var checkableChild = child as ThreeStateCheckableTreeNodeModel;
				if (checkableChild != null) 
					this.ApplyRecursiveUpdates(checkableChild, this.IsChecked);
			}

			var ancestor = parent;
			while (ancestor != null) {
				var allChildrenChecked = true;
				var allChildrenUnchecked = true;

				foreach (var child in ancestor.Children) {
					var checkableChild = child as ThreeStateCheckableTreeNodeModel;
					if (checkableChild != null) {
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
		private void ApplyRecursiveUpdates(ThreeStateCheckableTreeNodeModel node, bool? newValue) { 
			if ((node != null) && (node.IsChecked != newValue)) {
				node.SetIsCheckedWithoutRecursion(newValue);

				foreach (var child in node.Children) {
					var checkableChild = child as ThreeStateCheckableTreeNodeModel;
					if (checkableChild != null) 
						this.ApplyRecursiveUpdates(checkableChild, newValue);
				}
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Raises the <see cref="PropertyChanged"/> event.
		/// </summary>
		/// <param name="e">The <see cref="PropertyChangedEventArgs"/> that contains the event data.</param>
		protected override void OnPropertyChanged(PropertyChangedEventArgs e) {
			// Call the base method
			base.OnPropertyChanged(e);

			if (e != null) {
				switch (e.PropertyName) {
					case "IsChecked":
						if (!isUpdatingIsChecked)
							this.ApplyRecursiveUpdates();
						break;
				}
			}
		}

		/// <summary>
		/// Gets the parent node, whose reference is needed for check updates that affect the ancestor nodes.
		/// </summary>
		/// <value>The parent node, whose reference is needed for check updates that affect the ancestor nodes.</value>
		public ThreeStateCheckableTreeNodeModel Parent {
			get {
				return parent;
			}
			private set {
				if (parent == value)
					return;

				parent = value;
				this.NotifyPropertyChanged("Parent");
			}
		}
		
		/// <summary>
		/// Sets the <c>IsChecked</c> property without recursion.
		/// </summary>
		/// <param name="newValue">The new value.</param>
		public void SetIsCheckedWithoutRecursion(bool? newValue) {
			try {
				isUpdatingIsChecked = true;
				this.IsChecked = newValue;
			}
			finally {
				isUpdatingIsChecked = false;
			}
		}
		
	}

}
