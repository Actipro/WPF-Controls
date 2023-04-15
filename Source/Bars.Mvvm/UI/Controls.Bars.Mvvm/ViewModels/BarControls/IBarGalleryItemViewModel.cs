using System;
using System.ComponentModel;
using System.Windows.Media;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a gallery item within a bar gallery control.
	/// </summary>
	public interface IBarGalleryItemViewModel : INotifyPropertyChanged, IEquatable<IBarGalleryItemViewModel> {

		/// <summary>
		/// Gets or sets the item's category.
		/// </summary>
		/// <value>The item's category.</value>
		string Category { get; set; }

		/// <summary>
		/// Gets or sets the text description to display in screen tips.
		/// </summary>
		/// <value>The text description to display in screen tips.</value>
		string Description { get; set; }
		
		/// <summary>
		/// Gets or sets the image to display, if the item's template supports an image.
		/// </summary>
		/// <value>An <see cref="System.Windows.Media.ImageSource"/> for the image.</value>
		ImageSource ImageSource { get; set; }

		/// <summary>
		/// Gets whether the <see cref="Label"/> is visible.
		/// </summary>
		/// <value>
		/// <c>true</c> if the <see cref="Label"/> is visible; otherwise, <c>false</c>.
		/// </value>
		bool IsLabelVisible { get; }

		/// <summary>
		/// Gets or sets the key tip text used to access the control.
		/// </summary>
		/// <value>The key tip text used to access the control.</value>
		string KeyTipText { get; set; }

		/// <summary>
		/// Gets or sets the text label to display.
		/// </summary>
		/// <value>The text label to display.</value>
		string Label { get; set; }

		/// <summary>
		/// Gets or sets a <see cref="BarGalleryItemLayoutBehavior"/> indicating how the gallery item should be visually displayed.
		/// </summary>
		/// <value>
		/// A <see cref="BarGalleryItemLayoutBehavior"/> indicating how the gallery item should be visually displayed.
		/// The default value is <see cref="BarGalleryItemLayoutBehavior.Default"/>.
		/// </value>
		BarGalleryItemLayoutBehavior LayoutBehavior { get; }
		
		/// <summary>
		/// Gets or sets the value associated with this view model.
		/// </summary>
		/// <value>
		/// An object.
		/// </value>
		object Value { get; set; }

	}

}
