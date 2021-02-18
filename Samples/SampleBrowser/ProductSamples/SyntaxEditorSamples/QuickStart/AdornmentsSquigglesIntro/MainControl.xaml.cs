using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;
using System;
using System.Text.RegularExpressions;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsSquigglesIntro {

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

			this.RefreshSquiggleTags();
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Rescan the document and look for instances of the word 'Actipro' to mark with squiggle tags.
		/// </summary>
		private void RefreshSquiggleTags() {
			// Get the tagger that was created by the language and has been persisted in the document's properties
			//   while the language is active on the document
			CustomSquiggleTagger tagger = null;
			if (editor.Document.Properties.TryGetValue(typeof(CustomSquiggleTagger), out tagger)) {
				using (var batch = tagger.CreateBatch()) {
					// Clear existing tags
					tagger.Clear();

					// In this example we are going to construct the full snapshot text string... this is not generally a good
					//   idea for a production application since doing so for a large document in the UI thread can negatively affect performance...
					//   But this example shows how any text range results (such as those from an external error scan) can be used to generate squiggle tags
					var snapshot = editor.ActiveView.CurrentSnapshot;
					var snapshotText = snapshot.GetText(LineTerminator.Newline);

					// Look for regex pattern matches
					var matches = Regex.Matches(snapshotText, @"\bActipro\b", RegexOptions.IgnoreCase);
					for (var matchIndex = 0; matchIndex < matches.Count; matchIndex++) {
						var match = matches[matchIndex];

						// Create a version range for the match
						var snapshotRange = new TextSnapshotRange(snapshot, TextRange.FromSpan(match.Index, match.Length));
						var versionRange = snapshotRange.ToVersionRange(TextRangeTrackingModes.DeleteWhenZeroLength);
				
						// Create a tag, and include a quick info tip if specified
						var tag = new SquiggleTag();
						tag.ClassificationType = ClassificationTypes.Warning;  // This classification type is mapped in the tagger to a Green color
						tag.ContentProvider = new PlainTextContentProvider(String.Format("Instance number {0}", matchIndex + 1));

						// Add the tag to the tagger
						tagger.Add(new TagVersionRange<ISquiggleTag>(versionRange, tag));
					}
				}
			}

		}

	}

}