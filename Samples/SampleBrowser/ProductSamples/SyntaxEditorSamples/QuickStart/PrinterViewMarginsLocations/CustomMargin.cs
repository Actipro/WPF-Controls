using ActiproSoftware.Text.Utility;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Margins;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.PrinterViewMarginsLocations;

/// <summary>
/// Represents an implementation of a custom margin for an <see cref="IPrinterView"/>.
/// </summary>
public class CustomMargin : Control, IPrinterViewMargin {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the class.
	/// </summary>
	static CustomMargin() {
		// Override property defaults
		DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomMargin), new FrameworkPropertyMetadata(typeof(CustomMargin)));
	}

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public CustomMargin() { }

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="view">The <see cref="IPrinterView"/> that will host the margins.</param>
	/// <param name="placement">A <see cref="PrinterViewMarginPlacement"/> indicating the placement of the margin within its parent <see cref="IPrinterView"/>.</param>
	public CustomMargin(IPrinterView view, PrinterViewMarginPlacement placement) : this() {
		Placement = placement;

		// Get the style manually from the resources above the SyntaxEditor since this margin will be used
		//   outside the resource scope of the SyntaxEditor when displayed in a print preview...
		//   Alternatively, define a global style for this type in the application resources
		Style = view.SyntaxEditor.FindResource(typeof(CustomMargin)) as Style;
	}

	// --------------------------------------------------------------------------------------------------
	// INTERFACE IMPLEMENTATION
	// --------------------------------------------------------------------------------------------------

	string IKeyedObject.Key
		=> "Custom";

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="ITextViewMargin.Draw"/>
	public void Draw(TextViewDrawContext context) {
		// NOTE: This margin is rendered via XAML but could be drawn here instead if desired
	}

	/// <inheritdoc cref="IOrderable.Orderings"/>
	public IEnumerable<Ordering> Orderings {
		get => [
			new Ordering(PrinterViewMarginKeys.DocumentTitle, OrderPlacement.Before),
			new Ordering(PrinterViewMarginKeys.LineNumber, OrderPlacement.Before),
			new Ordering(PrinterViewMarginKeys.PageNumber, OrderPlacement.Before),
			new Ordering(PrinterViewMarginKeys.WordWrapGlyph, OrderPlacement.After),
		];
	}

	/// <inheritdoc cref="IPrinterViewMargin.Placement"/>
	public PrinterViewMarginPlacement Placement { get; set; }

	/// <inheritdoc cref="ITextViewMargin.VisualElement"/>
	public FrameworkElement VisualElement
		=> this;

}
