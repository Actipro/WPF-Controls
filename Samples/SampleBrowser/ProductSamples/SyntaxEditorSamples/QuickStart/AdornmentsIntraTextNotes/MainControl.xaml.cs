using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Tagging;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsIntraTextNotes {

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
		private void OnAddNoteButtonClick(object sender, RoutedEventArgs e) {
			// Get the tagger that was created by the language and has been persisted in the document's properties
			//   while the language is active on the document
			IntraTextNoteTagger tagger = null;
			if (editor.Document.Properties.TryGetValue(typeof(IntraTextNoteTagger), out tagger)) {
				// Create a version range
				ITextVersionRange versionRange = editor.ActiveView.Selection.SnapshotRange.ToVersionRange(TextRangeTrackingModes.ExpandFirstEdge | TextRangeTrackingModes.DeleteWhenZeroLength);
				
				// Create a tag that will be used to reserve space between text characters...
				//   Since the tags in this sample are persisted in a collection while active, 
				//   we can use the tag itself as the key... the key is used to retrieve
				//   the bounds of the spacer later on so adornments can be rendered in it, thus is must be unique
				IntraTextNoteTag tag = new IntraTextNoteTag();
				tag.Key = tag;  
				tag.Size = new Size(30, 18);
				tag.Baseline = 14;
				tag.Author = "Actipro Customer";
				tag.Created = DateTime.Now;
				tag.Message = noteText.Text.Trim();
				tag.Status = ReviewStatus.Pending;

				// Add the tag to the tagger
				tagger.Add(new TagVersionRange<IIntraTextSpacerTag>(versionRange, tag));
			}

			// Focus the editor
			editor.Focus();
		}

	}

}