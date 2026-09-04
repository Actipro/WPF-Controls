using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.BarCode;

namespace ActiproSoftware.ProductSamples.BarCodeSamples.QuickStart.DrawingContext;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		RenderToDrawingContext();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnRenderCustomDrawElement(object? sender, CustomDrawElementCustomDrawEventArgs e) {
		var symbology = customDrawElement.Tag as Code39ExtendedSymbology;
		symbology?.Render(e.DrawingContext, new Point(0, 0), customDrawElement.RenderSize);
	}

	private void OnRenderToDrawingContextButtonClick(object? sender, RoutedEventArgs e)
		=> RenderToDrawingContext();

	/// <summary>
	/// Renders the bar code.
	/// </summary>
	private void RenderToDrawingContext() {
		var symbology = new Code39ExtendedSymbology {
			ValueDisplayStyle = ValueDisplayStyle,
			Value = Value
		};
		customDrawElement.Tag = symbology;

		var desiredSize = symbology.MeasureDesiredSize(new Size(double.PositiveInfinity, double.PositiveInfinity));
		customDrawElement.Width = desiredSize.Width;
		customDrawElement.Height = desiredSize.Height;
		customDrawElement.InvalidateVisual();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The value.
	/// </summary>
	public string Value { get; set; } = "ABC-123";

	/// <summary>
	/// The display style.
	/// </summary>
	public LinearBarCodeValueDisplayStyle ValueDisplayStyle { get; set; } = LinearBarCodeValueDisplayStyle.Centered;

}
