using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using System.Windows.Media;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.ComboBoxAndEditors {

	/// <summary>
	/// Represents a simple view model for a gallery item to be displayed in a combobox.
	/// </summary>
	public class SimpleComboBoxGalleryItem : BarGalleryItemViewModelBase {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <see cref="SimpleComboBoxGalleryItem"/> class.
		/// </summary>
		/// <param name="label">The item's label.</param>
		/// <param name="category">The item's category.</param>
		public SimpleComboBoxGalleryItem(string label, string category) : base(category) {
			this.Label = label;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets an <see cref="System.Windows.Media.ImageSource"/> to be associated with the item.
		/// </summary>
		/// <value>An <see cref="System.Windows.Media.ImageSource"/>.</value>
		public ImageSource ImageSource { get; set; }

		/// <summary>
		/// Gets whether the <see cref="Label"/> is visible.
		/// </summary>
		/// <value>
		/// <c>true</c> if the <see cref="Label"/> is visible; otherwise, <c>false</c>.
		/// </value>
		public override bool IsLabelVisible => true;

	}

}
