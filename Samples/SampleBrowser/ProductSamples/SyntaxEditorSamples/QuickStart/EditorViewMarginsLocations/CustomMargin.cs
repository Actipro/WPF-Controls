using ActiproSoftware.Text.Utility;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Margins;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.EditorViewMarginsLocations;

/// <summary>
/// Represents an implementation of a custom margin for an <see cref="IEditorView"/>.
/// </summary>
public class CustomMargin : Control, IEditorViewMargin {

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
	/// <param name="placement">A <see cref="EditorViewMarginPlacement"/> indicating the placement of the margin within its parent <see cref="IEditorView"/>.</param>
	public CustomMargin(EditorViewMarginPlacement placement) : this() {
		Placement = placement;
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
		// Make this custom margin appear "outside" of all the built-in margins
		get => [
			new Ordering(EditorViewMarginKeys.Indicator, OrderPlacement.After),
			new Ordering(EditorViewMarginKeys.LineNumber, OrderPlacement.After),
			new Ordering(EditorViewMarginKeys.Selection, OrderPlacement.After),
			new Ordering(EditorViewMarginKeys.Ruler, OrderPlacement.After),
			new Ordering(EditorViewMarginKeys.WordWrapGlyph, OrderPlacement.After),
		];
	}

	/// <inheritdoc cref="IEditorViewMargin.Placement"/>
	public EditorViewMarginPlacement Placement { get; set; }

	/// <inheritdoc cref="ITextViewMargin.VisualElement"/>
	public FrameworkElement VisualElement
		=> this;

}
