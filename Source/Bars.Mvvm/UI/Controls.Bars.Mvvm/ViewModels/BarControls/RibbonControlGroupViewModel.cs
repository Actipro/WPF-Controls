using System.Collections.ObjectModel;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a control group control within a ribbon group.
	/// </summary>
	public class RibbonControlGroupViewModel : ObservableObjectBase {

		private ItemVariantBehavior itemVariantBehavior = ItemVariantBehavior.Default;
		private RibbonControlGroupSeparatorMode separatorMode = RibbonControlGroupSeparatorMode.Default;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the collection of items in the control.
		/// </summary>
		/// <value>The collection of items in the control.</value>
		public ObservableCollection<object> Items { get; } = new ObservableCollection<object>();
		
		/// <summary>
		/// Gets or sets an <see cref="itemVariantBehavior"/> that indicators how variant sizes should be applied to items.
		/// </summary>
		/// <value>
		/// An <see cref="itemVariantBehavior"/> that indicators how variant sizes should be applied to items.
		/// The default value is <see cref="ItemVariantBehavior.Default"/>.
		/// </value>
		public ItemVariantBehavior ItemVariantBehavior {
			get {
				return itemVariantBehavior;
			}
			set {
				if (itemVariantBehavior != value) {
					itemVariantBehavior = value;
					this.NotifyPropertyChanged(nameof(ItemVariantBehavior));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets a <see cref="RibbonControlGroupSeparatorMode"/> indicating how separators should be positioned around the control.
		/// </summary>
		/// <value>
		/// A <see cref="RibbonControlGroupSeparatorMode"/> indicating how separators should be positioned around the control.
		/// The default value is <see cref="RibbonControlGroupSeparatorMode.Default"/>.
		/// </value>
		public RibbonControlGroupSeparatorMode SeparatorMode {
			get {
				return separatorMode;
			}
			set {
				if (separatorMode != value) {
					separatorMode = value;
					this.NotifyPropertyChanged(nameof(SeparatorMode));
				}
			}
		}

		/// <inheritdoc/>
		public override string ToString() {
			return $"{this.GetType().FullName}[{this.Items.Count} item(s)']";
		}

	}

}
