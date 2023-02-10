using System.Windows.Media;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Stores options data for an image used in a bar control.
	/// </summary>
	public struct BarImageOptions {

		/// <summary>
		/// Gets the default image options instance.
		/// </summary>
		public static BarImageOptions Default { get; } = new BarImageOptions(BarImageSize.Small);
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <see cref="BarImageOptions"/> class.
		/// </summary>
		/// <param name="size">A <see cref="BarImageSize"/> indicating the image size.</param>
		public BarImageOptions(BarImageSize size) {
			this.ContextualColor = null;
			this.Size = size;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the optional contextual <see cref="Color"/>.
		/// </summary>
		/// <value>The optional contextual <see cref="Color"/>.</value>
		public Color? ContextualColor { get; set; }

		/// <summary>
		/// Gets or sets a <see cref="BarImageSize"/> indicating the image size.
		/// </summary>
		/// <value>A <see cref="BarImageSize"/> indicating the image size.</value>
		public BarImageSize Size { get; set; }

	}

}
