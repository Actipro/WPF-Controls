using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CollapsedRegionsAdvanced;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl : UserControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnCollapseButtonClick(object sender, RoutedEventArgs e) {
		if (editor.ActiveView.Selection.IsZeroLength) {
			MessageBox.Show("Please select at least one character to collapse.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			return;
		}

		// Get the tagger that was created by the language and has been persisted in the document's properties
		//   while the language is active on the document
		if (editor.Document.Properties.TryGetValue<CollapsedRegionTagger>(out var tagger)) {
			// Create a version range
			var versionRange = editor.ActiveView.Selection.SnapshotRange.ToVersionRange(TextRangeTrackingModes.DeleteWhenZeroLength);

			// Create a formatted text
			var formattedText = new FormattedText("...", CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
				new Typeface(editor.FontFamily, editor.FontStyle, editor.FontWeight, editor.FontStretch),
				editor.FontSize, Brushes.Gray, VisualTreeHelper.GetDpi(editor).PixelsPerDip
			);

			// Create a tag that will be used to both collapse text and drive an intra-text placeholder...
			//   Since the tags in this sample are persisted in a collection while active,
			//   we can use the tag itself as the key... the key is used to retrieve
			//   the bounds of the spacer later on so adornments can be rendered in it, thus is must be unique
			var tag = new CollapsedRegionTag();
			tag.Key = tag;
			tag.Text = formattedText.Text;
			tag.Baseline = formattedText.Baseline;
			tag.Size = new Size(formattedText.WidthIncludingTrailingWhitespace, formattedText.Height);

			// Add the tag to the tagger
			tagger!.Add(new TagVersionRange<ICollapsedRegionTag>(versionRange, tag));
		}

		// Focus the editor
		editor.Focus();
	}

	private void OnUncollapseAllButtonClick(object sender, RoutedEventArgs e) {
		// Get the tagger that was created by the language and has been persisted in the document's properties
		//   while the language is active on the document
		if (editor.Document.Properties.TryGetValue<CollapsedRegionTagger>(out var tagger)) {
			// Clear all tags from the tagger
			tagger!.Clear();
		}
	}

}
