using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CollapsedRegionsAdvanced {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl : System.Windows.Controls.UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnCollapseButtonClick(object sender, RoutedEventArgs e) {
			if (editor.ActiveView.Selection.IsZeroLength) {
				MessageBox.Show("Please select at least one character to collapse.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				return;
			}

			// Get the tagger that was created by the language and has been persisted in the document's properties
			//   while the language is active on the document
			CollapsedRegionTagger tagger = null;
			if (editor.Document.Properties.TryGetValue(typeof(CollapsedRegionTagger), out tagger)) {
				// Create a version range
				ITextVersionRange versionRange = editor.ActiveView.Selection.SnapshotRange.ToVersionRange(TextRangeTrackingModes.DeleteWhenZeroLength);

				// Create a formatted text
				FormattedText formattedText = new FormattedText("...", CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
					new Typeface(editor.FontFamily, editor.FontStyle, editor.FontWeight, editor.FontStretch),
					editor.FontSize, Brushes.Gray, VisualTreeHelper.GetDpi(editor).PixelsPerDip);
				
				// Create a tag that will be used to both collapse text and drive an intra-text placeholder...
				//   Since the tags in this sample are persisted in a collection while active, 
				//   we can use the tag itself as the key... the key is used to retrieve
				//   the bounds of the spacer later on so adornments can be rendered in it, thus is must be unique
				CollapsedRegionTag tag = new CollapsedRegionTag();
				tag.Key = tag;
				tag.Text = formattedText.Text;
				tag.Baseline = formattedText.Baseline;
				tag.Size = new Size(formattedText.WidthIncludingTrailingWhitespace, formattedText.Height);

				// Add the tag to the tagger
				tagger.Add(new TagVersionRange<ICollapsedRegionTag>(versionRange, tag));
			}

			// Focus the editor
			editor.Focus();
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnUncollapseAllButtonClick(object sender, RoutedEventArgs e) {
			// Get the tagger that was created by the language and has been persisted in the document's properties
			//   while the language is active on the document
			CollapsedRegionTagger tagger = null;
			if (editor.Document.Properties.TryGetValue(typeof(CollapsedRegionTagger), out tagger)) {
				// Clear all tags from the tagger
				tagger.Clear();
			}
		}

	}

}