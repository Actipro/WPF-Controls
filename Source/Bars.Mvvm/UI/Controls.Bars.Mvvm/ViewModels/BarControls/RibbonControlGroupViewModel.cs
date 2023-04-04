using System.Collections.ObjectModel;
using System.Windows;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a control group control within a ribbon group.
	/// </summary>
	public class RibbonControlGroupViewModel : ObservableObjectBase {

		private HorizontalAlignment horizontalContentAlignment = HorizontalAlignment.Left;
		private ItemVariantBehavior itemVariantBehavior = ItemVariantBehavior.All;
		private RibbonControlGroupSeparatorMode separatorMode = RibbonControlGroupSeparatorMode.Default;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets a <see cref="HorizontalAlignment"/> that indicates how items stacked vertically should be aligned horizontally.
		/// </summary>
		/// <value>
		/// An <see cref="itemVariantBehavior"/> that indicates how items stacked vertically should be aligned horizontally.
		/// The default value is <see cref="HorizontalAlignment.Left"/>.
		/// </value>
		public HorizontalAlignment HorizontalContentAlignment {
			get {
				return horizontalContentAlignment;
			}
			set {
				if (horizontalContentAlignment != value) {
					horizontalContentAlignment = value;
					this.NotifyPropertyChanged(nameof(HorizontalContentAlignment));
				}
			}
		}
		
		/// <summary>
		/// Gets the collection of items in the control.
		/// </summary>
		/// <value>The collection of items in the control.</value>
		public ObservableCollection<object> Items { get; } = new ObservableCollection<object>();
		
		/// <summary>
		/// Gets or sets an <see cref="itemVariantBehavior"/> that indicates how variant sizes should be applied to items.
		/// </summary>
		/// <value>
		/// An <see cref="itemVariantBehavior"/> that indicates how variant sizes should be applied to items.
		/// The default value is <see cref="ItemVariantBehavior.All"/>.
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
