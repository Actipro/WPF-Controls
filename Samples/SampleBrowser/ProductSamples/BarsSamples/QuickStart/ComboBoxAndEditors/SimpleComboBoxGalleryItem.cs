using ActiproSoftware.Windows.Controls.Bars.Mvvm;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.ComboBoxAndEditors {

	/// <summary>
	/// Represents a simple view model for a gallery item to be displayed in a combobox.
	/// </summary>
	public class SimpleComboBoxGalleryItem : BarGalleryItemViewModel<string> {

		// NOTE: This class is used to demonstrate how to wrap any value type in a view model for
		//		 use with a combobox. For string values, the existing TextBarGalleryItemViewModel
		//		 class in the MVVM Library can be used instead.

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="SimpleComboBoxGalleryItem"/> class.
		/// </summary>
		/// <param name="text">The item's value and label.</param>
		/// <param name="category">The item's category.</param>
		public SimpleComboBoxGalleryItem(string text, string category)
			: base(text, category) { }
		
	}

}
