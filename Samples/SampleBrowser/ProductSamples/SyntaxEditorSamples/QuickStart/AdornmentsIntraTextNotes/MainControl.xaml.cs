using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsIntraTextNotes;

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

	private void OnAddNoteButtonClick(object sender, RoutedEventArgs e) {
		// Get the tagger that was created by the language and has been persisted in the document's properties
		//   while the language is active on the document
		if (editor.Document.Properties.TryGetValue<IntraTextNoteTagger>(out var tagger)) {
			// Create a version range
			var versionRange = editor.ActiveView.Selection.SnapshotRange.ToVersionRange(TextRangeTrackingModes.ExpandFirstEdge | TextRangeTrackingModes.DeleteWhenZeroLength);

			// Create a tag that will be used to reserve space between text characters...
			//   Since the tags in this sample are persisted in a collection while active,
			//   we can use the tag itself as the key... the key is used to retrieve
			//   the bounds of the spacer later on so adornments can be rendered in it, thus is must be unique
			var tag = new IntraTextNoteTag();
			tag.Key = tag;
			tag.Size = new Size(30, 18);
			tag.Baseline = 14;
			tag.Author = "Actipro Customer";
			tag.Created = DateTime.Now;
			tag.Message = noteText.Text.Trim();
			tag.Status = ReviewStatus.Pending;

			// Add the tag to the tagger
			tagger!.Add(new TagVersionRange<IIntraTextSpacerTag>(versionRange, tag));
		}

		// Focus the editor
		editor.Focus();
	}

}
