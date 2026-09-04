using ActiproSoftware.Text.Utility;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Margins;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.EditorViewMarginsCustom;

/// <summary>
/// Represents an implementation of a custom margin for an <see cref="IEditorView"/>.
/// </summary>
public class CustomMargin : Control, IEditorViewMargin {

	private readonly IEditorView _view;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the class.
	/// </summary>
	static CustomMargin() {
		// Override property defaults
		IsTabStopProperty.OverrideMetadata(typeof(CustomMargin), new FrameworkPropertyMetadata(false));
		PaddingProperty.OverrideMetadata(typeof(CustomMargin), new FrameworkPropertyMetadata(new Thickness(5)));
		DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomMargin), new FrameworkPropertyMetadata(typeof(CustomMargin)));
		FocusableProperty.OverrideMetadata(typeof(CustomMargin), new FrameworkPropertyMetadata(false));
	}

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="view">The <see cref="IEditorView"/> that will host the margin.</param>
	public CustomMargin(IEditorView view) {
		// Initialize
		_view = view;

		// Attach to events
		view.MarginsDestroyed += OnViewMarginsDestroyed;
		view.TextAreaLayout += OnViewTextAreaLayout;
	}

	// --------------------------------------------------------------------------------------------------
	// INTERFACE IMPLEMENTATION
	// --------------------------------------------------------------------------------------------------

	string IKeyedObject.Key
		=> "Custom";

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnViewMarginsDestroyed(object sender, EventArgs e) {
		// Detach from events
		_view.MarginsDestroyed -= OnViewMarginsDestroyed;
		_view.TextAreaLayout -= OnViewTextAreaLayout;
	}

	private void OnViewTextAreaLayout(object? sender, TextViewTextAreaLayoutEventArgs e) {
		if (Visibility == Visibility.Visible) {
			// Determine min width
			var digitCount = _view.CurrentSnapshot.Length.ToString(CultureInfo.CurrentCulture).Length;

			// Get typeface
			var typeface = new Typeface(FontFamily, FontStyle, FontWeight, FontStretch);
			var fontSize = FontSize;

			// Get the formatted text
			var text = new FormattedText(new string('0', digitCount) + " chars", CultureInfo.CurrentCulture,
				FlowDirection.LeftToRight, typeface, fontSize, Foreground, VisualTreeHelper.GetDpi(_view.VisualElement).PixelsPerDip);

			// Update the min width to ensure all digits will fit when on the last line
			var minWidth = Math.Max(42, Math.Ceiling(text.WidthIncludingTrailingWhitespace) + Padding.Left + Padding.Right);
			if (MinWidth != minWidth)
				MinWidth = minWidth;
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="ITextViewMargin.Draw"/>
	public void Draw(TextViewDrawContext context) {
		var marginBounds = context.Bounds;

		// Loop through all the view lines
		var visibleLines = _view.VisibleViewLines;
		foreach (var viewLine in visibleLines) {
			// Get the number of characters on the line
			string characterCount = viewLine.CharacterCount.ToString(CultureInfo.CurrentCulture) + " chars";

			// Get the foreground
			var foreground = viewLine.CharacterCount switch {
				> 60 => Colors.Red,
				> 40 => Colors.DarkGoldenrod,
				> 20 => Colors.DarkGreen,
				_ => Colors.Black
			};

			// Get the line layout
			var firstLayoutLine = context.Canvas.CreateTextLayout(characterCount, 0, FontFamily.Source, (float)FontSize, foreground).Lines[0];

			// Get x/y
			var x = marginBounds.Right - firstLayoutLine.Width - Padding.Right;
			var y = marginBounds.Y + viewLine.TextBounds.Y + (int)Math.Round(viewLine.Baseline - firstLayoutLine.Baseline, MidpointRounding.AwayFromZero);

			// Draw the text
			context.DrawText(new Point(x, y), firstLayoutLine);
		}
	}

	/// <inheritdoc cref="IOrderable.Orderings"/>
	public IEnumerable<Ordering> Orderings {
		// Make this custom margin appear "outside" of all the built-in margins
		get => [
			new Ordering(EditorViewMarginKeys.Indicator, OrderPlacement.After),
			new Ordering(EditorViewMarginKeys.LineNumber, OrderPlacement.After),
			new Ordering(EditorViewMarginKeys.Selection, OrderPlacement.After),
		];
	}

	/// <inheritdoc cref="IEditorViewMargin.Placement"/>
	public EditorViewMarginPlacement Placement
		=> EditorViewMarginPlacement.ScrollableLeft;

	/// <inheritdoc cref="ITextViewMargin.VisualElement"/>
	public FrameworkElement VisualElement
		=> this;

}
