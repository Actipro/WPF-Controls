using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.BarCode;
using System.Windows;

namespace ActiproSoftware.ProductSamples.BarCodeSamples.QuickStart.DrawingContext {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			this.RenderToDrawingContext();
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Renders the bar code.
		/// </summary>
		private void RenderToDrawingContext() {
			Code39ExtendedSymbology symbology = new Code39ExtendedSymbology();
			symbology.ValueDisplayStyle = this.ValueDisplayStyle;
			symbology.Value = this.Value;
			customDrawElement.Tag = symbology;

			Size desiredSize = symbology.MeasureDesiredSize(new Size(double.PositiveInfinity, double.PositiveInfinity));
			customDrawElement.Width = desiredSize.Width;
			customDrawElement.Height = desiredSize.Height;
			customDrawElement.InvalidateVisual();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when the element is rendered.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="CustomDrawElementCustomDrawEventArgs"/> that contains the event data.</param>
		public void OnRenderCustomDrawElement(object sender, CustomDrawElementCustomDrawEventArgs e) {
			Code39ExtendedSymbology symbology = customDrawElement.Tag as Code39ExtendedSymbology;
			if (symbology != null)
				symbology.Render(e.DrawingContext, new Point(0, 0), customDrawElement.RenderSize);
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		public void OnRenderToDrawingContextButtonClick(object sender, RoutedEventArgs e) {
			this.RenderToDrawingContext();
		}
		
		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>The value.</value>
		public string Value { get; set; } = "ABC-123";

		/// <summary>
		/// Gets or sets the display style.
		/// </summary>
		/// <value>The display style.</value>
		public LinearBarCodeValueDisplayStyle ValueDisplayStyle { get; set; } = LinearBarCodeValueDisplayStyle.Centered;

	}

}