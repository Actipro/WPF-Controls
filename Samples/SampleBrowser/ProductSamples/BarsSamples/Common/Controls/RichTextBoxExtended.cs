using ActiproSoftware.Extensions;
using ActiproSoftware.Windows.Themes;
using System.Windows.Documents;
using System.Xml;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common;

/// <summary>
/// Represents an extended <see cref="RichTextBox"/> control.
/// </summary>
public class RichTextBoxExtended : RichTextBox {

	private MemoryStream? _previewStream;

	// --------------------------------------------------------------------------------------------------
	// NESTED TYPES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Defines the current preview mode state.
	/// </summary>
	public enum PreviewModeState {

		/// <summary>Preview mode is not active.</summary>
		None,

		/// <summary>Preview mode is active and selection is tracked.</summary>
		ActiveWithSelection,

		/// <summary>Preview mode is active, but selection is not tracked.</summary>
		ActiveWithoutSelection,

	}

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the class.
	/// </summary>
	static RichTextBoxExtended() {
		AcceptsReturnProperty.OverrideMetadata(typeof(RichTextBoxExtended), new FrameworkPropertyMetadata(defaultValue: true));
		AcceptsTabProperty.OverrideMetadata(typeof(RichTextBoxExtended), new FrameworkPropertyMetadata(defaultValue: true));
		HorizontalScrollBarVisibilityProperty.OverrideMetadata(typeof(RichTextBoxExtended), new FrameworkPropertyMetadata(defaultValue: ScrollBarVisibility.Hidden));
		PaddingProperty.OverrideMetadata(typeof(RichTextBoxExtended), new FrameworkPropertyMetadata(defaultValue: new Thickness(32.0)));
		VerticalScrollBarVisibilityProperty.OverrideMetadata(typeof(RichTextBoxExtended), new FrameworkPropertyMetadata(defaultValue: ScrollBarVisibility.Hidden));
	}

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public RichTextBoxExtended() {
		// Set appearance
		Background = Brushes.White;
		BorderBrush = Brushes.Black;
		BorderThickness = new Thickness(0);
		Foreground = Brushes.Black;
		Document.Background = Brushes.White;
		Document.Foreground = Foreground;

		// Force Ideal formatting because Display formatting at mixed DPI (e.g. 100% primary monitor, 150% secondary monitor)
		//   could cause RichTextBox to crash after switching monitors and scrolling documents with wrapped lines; especially if a MaxWidth
		//   was assigned to the RichTextBox or one of its parent containers
		TextOptions.SetTextFormattingMode(this, TextFormattingMode.Ideal);

		ThemeProperties.SetUseBackgroundStates(this, false);
		ThemeProperties.SetUseBorderStates(this, false);
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates a <see cref="TextDecorationCollection"/> with one or more <see cref="TextDecoration"/> values.
	/// </summary>
	/// <param name="underline"><c>true</c> to include a <see cref="TextDecoration"/> for underline; otherwise <c>false</c>.</param>
	/// <param name="strikeThrough"><c>true</c> to include a <see cref="TextDecoration"/> for strikethrough; otherwise <c>false</c>.</param>
	private static TextDecorationCollection? CreateTextDecorationCollection(bool underline, bool strikeThrough) {
		// Use a pre-defined TextDecoration if only a single decoration is necessary
		if (underline && !strikeThrough)
			return TextDecorations.Underline;
		else if (strikeThrough && !underline)
			return TextDecorations.Strikethrough;
		else if (!(underline || strikeThrough))
			return null;

		// Combine multiple text decorations into a single collection
		var textDecorations = new TextDecorationCollection();
		if (underline)
			textDecorations.AddRange(TextDecorations.Underline);
		if (strikeThrough)
			textDecorations.AddRange(TextDecorations.Strikethrough);
		return textDecorations;
	}

	/// <summary>
	/// Serializes the current selection.
	/// </summary>
	/// <param name="stream">The stream which defines the serialized data.</param>
	private void DeserializeSelection(Stream stream) {
		// The serialized data is stored as XAML with a root <Span> tag that has one
		//   or more <Run> children. The <Span> tag defines several attributes that
		//   should be applied to all <Run> tags, and each <Run> tag should only need
		//   to define the attributes that are different than the parent <Span>.
		//
		// There is an issue with RichTextBox inserting XAML if the <Run> tags do
		//   not directly define the necessary attributes, which should only apply
		//   when there is a single <Run> tag.
		//
		// This issue has primarily been observed when the only formatting applied
		//   to a selection is the foreground or background colors.
		//
		// Before deserializing, transfer relevant attributes from the <Span> tag
		//   to the <Run> tag.
		stream.Position = 0;
		var dom = new XmlDocument();
		dom.Load(stream);
		bool isDirty = false;
		if (dom.DocumentElement is { Name: "Span", ChildNodes.Count: 1, FirstChild: { Name: "Run" } run } span) {
			var attributesToTransfer = new string[] { "Foreground", "Background" };
			foreach (var attributeName in attributesToTransfer) {
				if (run.Attributes?.GetNamedItem(attributeName) is null) {
					var value = span.Attributes.GetNamedItem(attributeName)?.InnerText;
					if (!string.IsNullOrEmpty(value)) {
						var attr = dom.CreateAttribute(attributeName);
						attr.Value = value;
						run.Attributes?.SetNamedItem(attr);
						isDirty = true;
					}
				}
			}
		}
		if (isDirty) {
			// Write the modified data back to the stream
			using (var writer = XmlWriter.Create(stream, new XmlWriterSettings() { Indent = false, CloseOutput = false })) {
				stream.Position = 0;
				dom.Save(writer);
				stream.Flush();
				stream.Position = 0;
			}
		}

		// Load the selection
		Selection.Load(stream, DataFormats.Xaml);
	}

	/// <summary>
	/// Returns the text range for the word located at the given position; otherwise <c>null</c> if a word is not detected.
	/// </summary>
	/// <param name="position">The position to examine.</param>
	private static TextRange? GetWordRange(TextPointer position) {
		if (position is null)
			throw new ArgumentNullException(nameof(position));

		// Define which characters will be used as word breaks
		var wordBreakChars = " .?,;:!\"?";

		// Look at the text before the position and stop at the first word break (or beginning of the text)
		TextPointer wordStart;
		var beforeText = position.GetTextInRun(LogicalDirection.Backward);
		if (!string.IsNullOrEmpty(beforeText)) {
			var offset = 0;
			for (var i = beforeText.Length - 1; i >= 0; i--, offset--) {
				if (wordBreakChars.Contains(beforeText[i]))
					break;
			}
			wordStart = position.GetPositionAtOffset(offset, LogicalDirection.Forward);
		}
		else {
			wordStart = position;
		}

		// Look at the text after the position and stop at the first word break (or end of the text)
		TextPointer wordEnd;
		var afterText = position.GetTextInRun(LogicalDirection.Forward);
		if (!string.IsNullOrEmpty(afterText)) {
			var offset = 0;
			for (var i = 0; i < afterText.Length; i++, offset++) {
				if (wordBreakChars.Contains(afterText[i]))
					break;
			}
			wordEnd = position.GetPositionAtOffset(offset, LogicalDirection.Forward);
		}
		else {
			wordEnd = position;
		}

		// Create the TextRange if a word is detected
		if (!wordStart.Equals(wordEnd)) {
			var wordRange = new TextRange(wordStart, wordEnd);
			//Debug.WriteLine("Word Text = \"" + wordRange.Text + "\"");
			return wordRange;
		}

		return null;
	}

	/// <summary>
	/// Serializes the current selection.
	/// </summary>
	/// <param name="stream">The stream to write the serialized data.</param>
	private void SerializeSelection(Stream stream) {
		// Save the current selection as XAML
		Selection.Save(stream, DataFormats.Xaml);
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Activates preview mode.
	/// </summary>
	public void ActivatePreviewMode() {
		if (IsPreviewModeActive)
			return;

		try {
			if (_previewStream is null) {
				if (Selection.IsEmpty) {
					// When the selection is empty, we need to select something for the preview stream functionality to work correctly
					var wordRange = GetWordRange(CaretPosition);
					if (wordRange is null) {
						// Nothing to select, so selection will not be available to modify in preview mode. This can happen
						//   if the caret is positioned on a non-word element like a image.
						return;
					}
					Selection.Select(wordRange.Start, wordRange.End);
				}

				// Serialize the current settings so they can be restored when preview is deactivated
				if (!Selection.IsEmpty) {
					_previewStream = new MemoryStream();
					SerializeSelection(_previewStream);
				}
			}
		}
		finally {
			IsPreviewModeActive = true;
		}
	}

	/// <summary>
	/// Clears all text highlights.
	/// </summary>
	public void ClearAllTextHighlights() {
		if (IsPreviewModeActive)
			return;

		var range = new TextRange(Document.ContentStart, Document.ContentEnd);
		range.ApplyPropertyValue(TextElement.BackgroundProperty, null);
	}

	/// <summary>
	/// Deactivates preview mode.
	/// </summary>
	/// <param name="restoreOldSettings">Whether to restore the old settings.</param>
	public void DeactivatePreviewMode(bool restoreOldSettings) {
		if (!IsPreviewModeActive)
			return;

		try {
			if (_previewStream is not null) {
				if (restoreOldSettings)
					DeserializeSelection(_previewStream);
				_previewStream.Dispose();
				_previewStream = null;
			}
		}
		finally {
			IsPreviewModeActive = false;
		}
	}

	/// <summary>
	/// Indicates whether preview mode is active.
	/// </summary>
	protected bool IsPreviewModeActive { get; private set; }

	/// <inheritdoc/>
	protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo) {
		base.OnRenderSizeChanged(sizeInfo);

		// Adjust the document's page width (since there is a WPF bug when used within a parent ScrollViewer with horizontal scroll capabilities)
		if (Document is { } document)
			document.PageWidth = ActualWidth - BorderThickness.Left - Padding.Left - BorderThickness.Right - Padding.Right;
	}

	/// <summary>
	/// The current state of preview mode.
	/// </summary>
	public PreviewModeState PreviewMode {
		get {
			if (IsPreviewModeActive) {
				return (_previewStream is null)
					? PreviewModeState.ActiveWithoutSelection
					: PreviewModeState.ActiveWithSelection;
			}
			return PreviewModeState.None;
		}
	}

	/// <summary>
	/// Resets the current selection to a zero-width range at the start of the document.
	/// </summary>
	public void ResetSelection() {
		if (Document is { } document) {
			var startPosition = document.ContentStart.GetPositionAtOffset(0);

			// Advance to the first text context
			while (
				startPosition is not null
				&& startPosition.GetPointerContext(LogicalDirection.Forward) != TextPointerContext.Text
			) {
				// Next context
				startPosition = startPosition.GetNextContextPosition(LogicalDirection.Forward);
			}

			if (startPosition is not null)
				Selection.Select(startPosition, startPosition);
		}
	}

	/// <summary>
	/// Indicates whether the selected text is bold.
	/// </summary>
	public bool SelectionBold {
		get => FontWeights.Bold.Equals(Selection.GetPropertyValue(TextElement.FontWeightProperty));
		set {
			if (SelectionBold != value)
				Selection.ApplyPropertyValue(TextElement.FontWeightProperty, (value != false ? FontWeights.Bold : FontWeights.Normal));
		}
	}

	/// <summary>
	/// The selection's font color.
	/// </summary>
	public Color SelectionFontColor {
		get {
			return (Selection.GetPropertyValue(TextElement.ForegroundProperty) is SolidColorBrush brush)
				? brush.Color
				: Colors.Black;
		}
		set {
			if (SelectionFontColor != value)
				Selection.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(value));
		}
	}

	/// <summary>
	/// The selection's font family name.
	/// </summary>
	public string? SelectionFontFamilyName {
		get {
			return (Selection.GetPropertyValue(TextElement.FontFamilyProperty) is FontFamily fontFamily)
				? fontFamily.Source
				: null;
		}
		set {
			if (!(string.IsNullOrEmpty(value)) && (SelectionFontFamilyName != value))
				Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, new FontFamily(value));
		}
	}

	/// <summary>
	/// The selection's font size.
	/// </summary>
	public double SelectionFontSize {
		get {
			return (Selection.GetPropertyValue(TextElement.FontSizeProperty) is double fontSize)
				? fontSize
				: double.NaN;
		}
		set {
			if (!(double.IsNaN(value)) && (SelectionFontSize != value))
				Selection.ApplyPropertyValue(TextElement.FontSizeProperty, Math.Max(1.0, value));
		}
	}

	/// <summary>
	/// Indicates whether the selected text is italic.
	/// </summary>
	public bool SelectionItalic {
		get => FontStyles.Italic.Equals(Selection.GetPropertyValue(TextElement.FontStyleProperty));
		set {
			if (SelectionItalic != value)
				Selection.ApplyPropertyValue(TextElement.FontStyleProperty, (value != false ? FontStyles.Italic : FontStyles.Normal));
		}
	}

	/// <summary>
	/// Indicates whether the selected text has a strike-through.
	/// </summary>
	public bool SelectionStrikethrough {
		get {
			if (Selection.GetPropertyValue(TextBlock.TextDecorationsProperty) is TextDecorationCollection textDecorations)
				return textDecorations.Any(d => d.Location == TextDecorationLocation.Strikethrough);
			return false;
		}
		set {
			if (SelectionStrikethrough != value)
				Selection.ApplyPropertyValue(TextBlock.TextDecorationsProperty, CreateTextDecorationCollection(underline: SelectionUnderline, strikeThrough: value));
		}
	}

	/// <summary>
	/// The selection's text alignment.
	/// </summary>
	public TextAlignment SelectionTextAlignment {
		get {
			return (Selection.GetPropertyValue(TextBlock.TextAlignmentProperty) is TextAlignment textAlignment)
				? textAlignment
				: TextAlignment.Left;
		}
		set {
			if (SelectionTextAlignment != value)
				Selection.ApplyPropertyValue(TextBlock.TextAlignmentProperty, value);
		}
	}

	/// <summary>
	/// The selection's text highlight color.
	/// </summary>
	public Color SelectionTextHighlightColor {
		get {
			return (Selection.GetPropertyValue(TextElement.BackgroundProperty) is SolidColorBrush brush)
				? brush.Color
				: Colors.White;
		}
		set {
			if (SelectionTextHighlightColor != value)
				Selection.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(value));
		}
	}

	/// <summary>
	/// Indicates whether the selected text is underlined.
	/// </summary>
	public bool SelectionUnderline {
		get {
			if (Selection.GetPropertyValue(TextBlock.TextDecorationsProperty) is TextDecorationCollection textDecorations)
				return textDecorations.Any(d => d.Location == TextDecorationLocation.Underline);
			return false;
		}
		set {
			if (SelectionUnderline != value)
				Selection.ApplyPropertyValue(TextBlock.TextDecorationsProperty, CreateTextDecorationCollection(underline: value, strikeThrough: SelectionStrikethrough));
		}
	}

}
