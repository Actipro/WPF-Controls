using ActiproSoftware.Windows.Extensions;
using ActiproSoftware.Windows.Themes;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Xml;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {

	/// <summary>
	/// Represents an extended <see cref="RichTextBox"/> control.
	/// </summary>
	public class RichTextBoxExtended : RichTextBox {

		private MemoryStream	previewStream;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes the <c>RichTextBoxExtended</c> class.
		/// </summary>
		static RichTextBoxExtended() {
			AcceptsReturnProperty.OverrideMetadata(typeof(RichTextBoxExtended), new FrameworkPropertyMetadata(true));
			AcceptsTabProperty.OverrideMetadata(typeof(RichTextBoxExtended), new FrameworkPropertyMetadata(true));
			HorizontalScrollBarVisibilityProperty.OverrideMetadata(typeof(RichTextBoxExtended), new FrameworkPropertyMetadata(ScrollBarVisibility.Hidden));
			PaddingProperty.OverrideMetadata(typeof(RichTextBoxExtended), new FrameworkPropertyMetadata(new Thickness(32.0)));
			VerticalScrollBarVisibilityProperty.OverrideMetadata(typeof(RichTextBoxExtended), new FrameworkPropertyMetadata(ScrollBarVisibility.Hidden));
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RichTextBoxExtended"/> class.
		/// </summary>
		public RichTextBoxExtended() {
			// Set appearance
			this.Background = Brushes.White;
			this.BorderBrush = Brushes.Black;
			this.BorderThickness = new Thickness(0);
			this.Foreground = Brushes.Black;
			this.Document.Background = Brushes.White;
			this.Document.Foreground = this.Foreground;

			// Force Ideal formatting because Display formatting at mixed DPI (e.g. 100% primary monitor, 150% secondary monitor)
			// could cause RichTextBox to crash after switching monitors and scrolling documents with wrapped lines; especially if a MaxWidth
			// was assigned to the RichTextBox or one of its parent containers
			TextOptions.SetTextFormattingMode(this, TextFormattingMode.Ideal);

			ThemeProperties.SetUseBackgroundStates(this, false);
			ThemeProperties.SetUseBorderStates(this, false);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a <see cref="TextDecorationCollection"/> with one or more <see cref="TextDecoration"/> values.
		/// </summary>
		/// <param name="underline"><c>true</c> to include a <see cref="TextDecoration"/> for underline; otherwise <c>false</c>.</param>
		/// <param name="strikeThrough"><c>true</c> to include a <see cref="TextDecoration"/> for strikethrough; otherwise <c>false</c>.</param>
		/// <returns>A <see cref="TextDecorationCollection"/>.</returns>
		private static TextDecorationCollection CreateTextDecorationCollection(bool underline, bool strikeThrough) {
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
			// or more <Run> children. The <Span> tag defines several attributes that
			// should be applied to all <Run> tags, and each <Run> tag should only need
			// to define the attributes that are different than the parent <Span>.
			//
			// There is an issue with RichTextBox inserting XAML if the <Run> tags do
			// not directly define the necessary attributes, which should only apply
			// when there is a single <Run> tag.
			//
			// This issue has primarily been observed when the only formatting applied
			// to a selection is the foreground or background colors.
			//
			// Before deserializing, transfer relevant attributes from the <Span> tag
			// to the <Run> tag.
			stream.Position = 0;
			var dom = new XmlDocument();
			dom.Load(stream);
			bool isDirty = false;
			if (dom.DocumentElement.Name == "Span") {
				var span = dom.DocumentElement;
				if (span.ChildNodes.Count == 1) {
					var run = span.FirstChild;
					if (run.Name == "Run") {
						var attributesToTransfer = new string[] { "Foreground", "Background" };
						foreach (var attributeName in attributesToTransfer) {
							if (run.Attributes.GetNamedItem(attributeName) is null) {
								var value = span.Attributes.GetNamedItem(attributeName)?.InnerText;
								if (!string.IsNullOrEmpty(value)) {
									var attr = dom.CreateAttribute(attributeName);
									attr.Value = value;
									run.Attributes.SetNamedItem(attr);
									isDirty = true;
								}
							}
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
			this.Selection.Load(stream, DataFormats.Xaml);
		}

		/// <summary>
		/// Gets the text range for the word located at the given position.
		/// </summary>
		/// <param name="position">The position to examine.</param>
		/// <returns>A <see cref="TextRange"/> of the current word if one is detected; otherwise <c>null</c>.</returns>
		/// <exception cref="ArgumentNullException"></exception>
		private static TextRange GetWordRange(TextPointer position) {
			if (position == null)
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
			else{
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
				//System.Diagnostics.Debug.WriteLine("Word Text = \"" + wordRange.Text + "\"");
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
			this.Selection.Save(stream, DataFormats.Xaml);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Activates preview mode.
		/// </summary>
		public void ActivatePreviewMode() {
			if (previewStream == null) {
				if (this.Selection.IsEmpty) {
					// When the selection is empty, we need to select something for the preview stream functionality to work correctly
					var wordRange = GetWordRange(this.CaretPosition);
					if (wordRange != null)
						this.Selection.Select(wordRange.Start, wordRange.End);
				}

				// Serialize the current settings so they can be restored when preview is deactivated
				previewStream = new MemoryStream();
				SerializeSelection(previewStream);
			}
		}

		/// <summary>
		/// Clears all text highlights.
		/// </summary>
		public void ClearAllTextHighlights() {
			var range = new TextRange(this.Document.ContentStart, this.Document.ContentEnd);
			range.ApplyPropertyValue(TextElement.BackgroundProperty, null);
		}

		/// <summary>
		/// Deactivates preview mode.
		/// </summary>
		/// <param name="restoreOldSettings">Whether to restore the old settings.</param>
		public void DeactivatePreviewMode(bool restoreOldSettings) {
			if (previewStream != null) {
				if (restoreOldSettings)
					DeserializeSelection(previewStream);
				previewStream.Dispose();
				previewStream = null;
			}
		}
		
		/// <summary>
		/// Gets whether preview mode is active.
		/// </summary>
		/// <value>
		/// <c>true</c> if preview mode is active; otherwise, <c>false</c>.
		/// </value>
		public bool IsPreviewModeActive => (previewStream != null);
		
		/// <summary>
		/// Called when the rendered size of a control changes. 
		/// </summary>
		/// <param name="sizeInfo">Specifies the size changes.</param>
		protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo) {
			// Call the base method
			base.OnRenderSizeChanged(sizeInfo);

			// Adjust the document's page width (since there is a WPF bug when used within a parent ScrollViewer with horizontal scroll capabilities)
			if (this.Document != null)
				this.Document.PageWidth = this.ActualWidth - this.BorderThickness.Left - this.Padding.Left - this.BorderThickness.Right - this.Padding.Right;
		}
		
		/// <summary>
		/// Resets the current selection to a zero-width range at the start of the document.
		/// </summary>
		public void ResetSelection() {
			if (this.Document != null) {
				var startPosition = Document.ContentStart.GetPositionAtOffset(0);
				if (startPosition != null)
					Selection.Select(startPosition, startPosition);
			}
		}
		
		/// <summary>
		/// Gets or sets whether the selected text is bold.
		/// </summary>
		/// <value>
		/// <c>true</c> if the selected text is bold; otherwise, <c>false</c>.
		/// </value>
		public bool SelectionBold {
			get {
				return FontWeights.Bold.Equals(this.Selection.GetPropertyValue(TextElement.FontWeightProperty));
			}
			set {
				if (this.SelectionBold != value)
					this.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, (value != false ? FontWeights.Bold : FontWeights.Normal));
			}
		}
		
		/// <summary>
		/// Gets or sets the selection's font color.
		/// </summary>
		/// <value>The selection's font color.</value>
		public Color SelectionFontColor {
			get {
				if (this.Selection.GetPropertyValue(TextElement.ForegroundProperty) is SolidColorBrush brush)
					return brush.Color;
				else
					return Colors.Black;
			}
			set {
				if (this.SelectionFontColor != value)
					this.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(value));
			}
		}
		
		/// <summary>
		/// Gets or sets the selection's font family name.
		/// </summary>
		/// <value>The selection's font family name.</value>
		public string SelectionFontFamilyName {
			get {
				if (this.Selection.GetPropertyValue(TextElement.FontFamilyProperty) is FontFamily fontFamily)
					return fontFamily.Source;
				else
					return null;
			}
			set {
				if (!(string.IsNullOrEmpty(value)) && (this.SelectionFontFamilyName != value)) 
					this.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, new FontFamily(value));
			}
		}
		
		/// <summary>
		/// Gets or sets the selection's font size.
		/// </summary>
		/// <value>The selection's font size.</value>
		public double SelectionFontSize {
			get {
				if (this.Selection.GetPropertyValue(TextElement.FontSizeProperty) is double fontSize)
					return fontSize;
				else
					return double.NaN;
			}
			set {
				if (!(double.IsNaN(value)) && (this.SelectionFontSize != value))
					this.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, Math.Max(1.0, value));
			}
		}
		
		/// <summary>
		/// Gets or sets whether the selected text is italic.
		/// </summary>
		/// <value>
		/// <c>true</c> if the selected text is italic; otherwise, <c>false</c>.
		/// </value>
		public bool SelectionItalic {
			get {
				return FontStyles.Italic.Equals(this.Selection.GetPropertyValue(TextElement.FontStyleProperty));
			}
			set {
				if (this.SelectionItalic != value)
					this.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, (value != false ? FontStyles.Italic : FontStyles.Normal));
			}
		}
		
		/// <summary>
		/// Gets or sets whether the selected text has a strike-through.
		/// </summary>
		/// <value>
		/// <c>true</c> if the selected text has a strike-through; otherwise, <c>false</c>.
		/// </value>
		public bool SelectionStrikethrough {
			get {
				var textDecorations = (TextDecorationCollection)this.Selection.GetPropertyValue(TextBlock.TextDecorationsProperty);
				return textDecorations?.Any(d => d.Location == TextDecorationLocation.Strikethrough) == true;
			}
			set {
				if (this.SelectionStrikethrough != value)
					this.Selection.ApplyPropertyValue(TextBlock.TextDecorationsProperty, CreateTextDecorationCollection(underline: SelectionUnderline, strikeThrough: value));
			}
		}
		
		/// <summary>
		/// Gets or sets the selection's text alignment.
		/// </summary>
		/// <value>The selection's text alignment.</value>
		public TextAlignment SelectionTextAlignment {
			get {
				if (this.Selection.GetPropertyValue(TextBlock.TextAlignmentProperty) is TextAlignment textAlignment)
					return textAlignment;
				else
					return TextAlignment.Left;
			}
			set {
				if (this.SelectionTextAlignment != value)
					this.Selection.ApplyPropertyValue(TextBlock.TextAlignmentProperty, value);
			}
		}
		
		/// <summary>
		/// Gets or sets the selection's text highlight color.
		/// </summary>
		/// <value>The selection's text highlight color.</value>
		public Color SelectionTextHighlightColor {
			get {
				if (this.Selection.GetPropertyValue(TextElement.BackgroundProperty) is SolidColorBrush brush)
					return brush.Color;
				else
					return Colors.White;
			}
			set {
				if (this.SelectionTextHighlightColor != value)
					this.Selection.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(value));
			}
		}
		
		/// <summary>
		/// Gets or sets whether the selected text is underlined.
		/// </summary>
		/// <value>
		/// <c>true</c> if the selected text is underlined; otherwise, <c>false</c>.
		/// </value>
		public bool SelectionUnderline {
			get {
				var textDecorations = (TextDecorationCollection)this.Selection.GetPropertyValue(TextBlock.TextDecorationsProperty);
				return textDecorations?.Any(d => d.Location == TextDecorationLocation.Underline) == true;
			}
			set {
				if (this.SelectionUnderline != value)
					this.Selection.ApplyPropertyValue(TextBlock.TextDecorationsProperty, CreateTextDecorationCollection(underline: value, strikeThrough: SelectionStrikethrough));
			}
		}

	}

}
