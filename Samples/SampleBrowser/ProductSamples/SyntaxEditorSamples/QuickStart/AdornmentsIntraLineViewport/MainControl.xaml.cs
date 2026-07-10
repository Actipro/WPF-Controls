using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsIntraLineViewport;

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

		AddAdornment(offset: 399);
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Adds an adornment.
	/// </summary>
	/// <param name="offset">The offset.</param>
	private void AddAdornment(int offset) {
		// Get the tagger that was created by the language and has been persisted in the document's properties
		//   while the language is active on the document
		if (editor.ActiveView.Properties.TryGetValue<IntraLineViewportTagger>(out var tagger)) {
			// Create a version range
			var versionRange = new TextSnapshotRange(editor.ActiveView.CurrentSnapshot, offset).ToVersionRange(TextRangeTrackingModes.Default);

			// Create a tag that will be used to reserve space between view lines...
			//   Since the tags in this sample are persisted in a collection while active, 
			//   we can use the tag itself as the key... the key is used to retrieve
			//   the bounds of the spacer later on so adornments can be rendered in it, thus is must be unique
			var tag = new IntraLineViewportTag();
			tag.UpdateBottomMargin(editor.ActiveView);
			tag.Key = tag;

			// Add the tag to the tagger
			tagger!.Add(new TagVersionRange<IIntraLineSpacerTag>(versionRange, tag));
		}

		// Focus the editor
		editor.Focus();
	}

}
