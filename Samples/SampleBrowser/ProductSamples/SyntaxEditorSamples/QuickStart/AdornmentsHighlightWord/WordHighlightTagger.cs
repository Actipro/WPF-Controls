using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Media;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.Text.Utility;
using ActiproSoftware.Windows.Controls.Rendering;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsHighlightWord {

	/// <summary>
	/// Provides a custom implementation of a view-based classification tagger that tags the word that that view's caret is in.
	/// </summary>
	public class WordHighlightTagger : TaggerBase<IClassificationTag> {

		private string		currentWord		= String.Empty;
		private Regex		wordCheck		= new Regex(@"[A-Za-z_]\w*", RegexOptions.Compiled);
		private IEditorView view;

		private static IClassificationType wordHighlightClassificationType = new ClassificationType("WordHighlight", "Word Highlight");

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes the <c>WordHighlightTagger</c> class.
		/// </summary>
		static WordHighlightTagger() {
			IHighlightingStyle style = new HighlightingStyle(null, Color.FromArgb(0x40, 0xC0, 0xC0, 0xC0));
			style.BorderColor = Color.FromArgb(0xFF, 0xC0, 0xC0, 0xC0);
			style.BorderCornerKind = HighlightingStyleBorderCornerKind.Rounded;
			style.BorderKind = LineKind.Solid;
			AmbientHighlightingStyleRegistry.Instance.Register(wordHighlightClassificationType, style);
		}

		/// <summary>
		/// Initializes a new instance of the <c>WordHighlightTagger</c> class.
		/// </summary>
		/// <param name="view">The view to which this manager is attached.</param>
		public WordHighlightTagger(IEditorView view) : base("Custom",
			new Ordering[] { new Ordering(TaggerKeys.Token, OrderPlacement.Before) }, view.SyntaxEditor.Document) {
			
			// Initialize
			this.view = view;
			this.view.SelectionChanged += new EventHandler<EditorViewSelectionEventArgs>(OnViewSelectionChanged);

			// Update current word
			this.UpdateCurrentWord();
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the view's selection is changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="EditorViewSelectionEventArgs"/> that contains data related to this event.</param>
		private void OnViewSelectionChanged(object sender, EditorViewSelectionEventArgs e) {
			if (view == null)
				return;
			
			// Update the current word
			this.UpdateCurrentWord();
		}

		/// <summary>
		/// Updates the current word.
		/// </summary>
		private void UpdateCurrentWord() {
			if ((view == null) || (view.Selection == null))
				return;

			// Save the old current word
			string oldCurrentWord = currentWord;

			// Get the current word and ensure it has only letter or number characters
			currentWord = view.GetCurrentWordText().Trim();
			Match match = wordCheck.Match(currentWord);
			if ((match == null) || (match.Index != 0) || (match.Length != currentWord.Length))
				currentWord = String.Empty;

			// If the current word changed...
			if (oldCurrentWord != currentWord) {
				// Notify that tags changed
				this.OnTagsChanged(new TagsChangedEventArgs(new TextSnapshotRange(view.SyntaxEditor.Document.CurrentSnapshot, view.SyntaxEditor.Document.CurrentSnapshot.TextRange)));
			}
		}
	
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Returns the tag ranges that intersect with the specified normalized snapshot ranges.
		/// </summary>
		/// <param name="snapshotRanges">The collection of normalized snapshot ranges.</param>
		/// <param name="parameter">An optional parameter that provides contextual information about the tag request.</param>
		/// <returns>The tag ranges that intersect with the specified normalized snapshot ranges.</returns>
		public override IEnumerable<TagSnapshotRange<IClassificationTag>> GetTags(NormalizedTextSnapshotRangeCollection snapshotRanges, object parameter) {
			if (String.IsNullOrEmpty(currentWord))
				yield break;

			// Get a regex of the current word
			Regex search = new Regex(String.Format(@"\b{0}\b", currentWord), RegexOptions.Singleline);

			// Loop through the requested snapshot ranges...
			foreach (TextSnapshotRange snapshotRange in snapshotRanges) {
				// If the snapshot range is not zero-length...
				if (!snapshotRange.IsZeroLength) {
					// Look for current word matches
					foreach (Match match in search.Matches(snapshotRange.Text)) {
						// Add a highlighted range
						yield return new TagSnapshotRange<IClassificationTag>(
							new TextSnapshotRange(snapshotRange.Snapshot, TextRange.FromSpan(snapshotRange.StartOffset + match.Index, match.Length)),
							new ClassificationTag(wordHighlightClassificationType)
							);
					}
				}
			}
		}
		
		/// <summary>
		/// Occurs when the manager is closed and detached from the view.
		/// </summary>
		/// <remarks>
		/// Overrides should release any event handlers set up in the manager's constructor.
		/// </remarks>
		protected override void OnClosed() {
			// Detach from the view
			if (view != null) {
				view.SelectionChanged -= new EventHandler<EditorViewSelectionEventArgs>(OnViewSelectionChanged);
				view = null;
			}

			// Call the base method
			base.OnClosed();
		}
		
	}
}