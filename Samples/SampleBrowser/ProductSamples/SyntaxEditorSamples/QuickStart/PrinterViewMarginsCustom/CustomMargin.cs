using ActiproSoftware.Text.Utility;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Margins;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.PrinterViewMarginsCustom;

/// <summary>
/// Represents an implementation of a custom margin for an <see cref="IPrinterView"/>.
/// </summary>
public class CustomMargin : Control, IPrinterViewMargin {

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="DocumentTitle"/> property.
	/// </summary>
	public static readonly DependencyProperty DocumentTitleProperty
		= DependencyProperty.Register(nameof(DocumentTitle), typeof(string), typeof(CustomMargin), new FrameworkPropertyMetadata(defaultValue: null));

	#endregion

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
	/// <param name="view">The <see cref="IPrinterView"/> that will host the margin.</param>
	public CustomMargin(IPrinterView view) {
		// Store the document title
		DocumentTitle = view.SyntaxEditor.PrintSettings?.DocumentTitle;

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

	/// <summary>
	/// The document title to display.
	/// </summary>
	public string? DocumentTitle {
		get => (string)GetValue(DocumentTitleProperty);
		set => SetValue(DocumentTitleProperty, value);
	}

	/// <inheritdoc cref="ITextViewMargin.Draw"/>
	public void Draw(TextViewDrawContext context) {
		// NOTE: This margin is rendered via XAML but could be drawn here instead if desired
	}

	/// <inheritdoc cref="IOrderable.Orderings"/>
	public IEnumerable<Ordering> Orderings {
		// Make this custom margin appear "inside" of all the built-in margins
		get => [new Ordering(PrinterViewMarginKeys.DocumentTitle, OrderPlacement.Before)];
	}

	/// <inheritdoc cref="IPrinterViewMargin.Placement"/>
	public PrinterViewMarginPlacement Placement
		=> PrinterViewMarginPlacement.Top;

	/// <inheritdoc cref="ITextViewMargin.VisualElement"/>
	public FrameworkElement VisualElement
		=> this;

}
