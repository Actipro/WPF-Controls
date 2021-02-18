using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.Text.Lexing;
using ActiproSoftware.Text.Utility;
using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.ClassificationLayered {

	/// <summary>
	/// Provides a custom implementation of a tagger that can classify ranges of text within a text buffer.
	/// </summary>
	public class CustomClassificationTagger : TaggerBase<IClassificationTag> {

		private bool						highlightDocumentationComments	= true;
		private bool						highlightIdentifiers;

		private ITagAggregator<ITokenTag>	tokenTagAggregator;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes the <c>CustomClassificationTagger</c> class.
		/// </summary>
		static CustomClassificationTagger() {
			// Register the classification type for a warning that will render the squiggle in red
			AmbientHighlightingStyleRegistry.Instance.Register(ClassificationTypes.SyntaxError, new HighlightingStyle(Colors.Red));
		}
			
		/// <summary>
		/// Initializes a new instance of the <c>CustomClassificationTagger</c> class.
		/// </summary>
		/// <param name="document">The document to which this manager is attached.</param>
		public CustomClassificationTagger(ICodeDocument document) : base("Custom",
			new Ordering[] { new Ordering(TaggerKeys.Token, OrderPlacement.Before) }, document) {

			// Get a token tag aggregator
			tokenTagAggregator = document.CreateTagAggregator<ITokenTag>();
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
			// Loop through the requested snapshot ranges...
			foreach (TextSnapshotRange snapshotRange in snapshotRanges) {
				// If the snapshot range is not zero-length...
				if (!snapshotRange.IsZeroLength) {
					IEnumerable<TagSnapshotRange<ITokenTag>> tokenTagRanges = tokenTagAggregator.GetTags(snapshotRange);
					if (tokenTagRanges != null) {
						foreach (TagSnapshotRange<ITokenTag> tokenTagRange in tokenTagRanges) {
							if (tokenTagRange.Tag.Token != null) {
								switch (tokenTagRange.Tag.Token.Key) {
									case "XmlCommentText": {
										if (highlightDocumentationComments) {
											// Get the text of the token
											string text = tokenTagRange.SnapshotRange.Text;

											// Look for the text "Actipro"
											int index = text.IndexOf("Actipro");
											while (index != -1) {
												// Add a highlighted range
												yield return new TagSnapshotRange<IClassificationTag>(
													new TextSnapshotRange(snapshotRange.Snapshot, TextRange.FromSpan(tokenTagRange.SnapshotRange.StartOffset + index, 7)),
													new ClassificationTag(ClassificationTypes.SyntaxError)
													);

												// Look for another match
												index = text.IndexOf("Actipro", index + 7);
											}
										}
										break;
									}
									case "Identifier": {
										if (highlightIdentifiers) {
											// Get the text of the token
											string text = tokenTagRange.SnapshotRange.Text;

											// If the text is "Actipro"...
											if (text == "Actipro") {
												// Add a highlighted range
												yield return new TagSnapshotRange<IClassificationTag>(
													new TextSnapshotRange(snapshotRange.Snapshot, tokenTagRange.SnapshotRange.TextRange),
													new ClassificationTag(ClassificationTypes.SyntaxError)
													);
											}
										}
										break;
									}
								}
							}
						}
					}
				}
			}
		}
		
		/// <summary>
		/// Gets or sets whether to highlight 'Actipro' in documentation comments.
		/// </summary>
		/// <value>
		/// <c>true</c> if 'Actipro' in documentation comments should be highlighted.
		/// </value>
		public bool HighlightDocumentationComments { 
			get {
				return highlightDocumentationComments;
			}
			set {
				if (highlightDocumentationComments == value)
					return;

				highlightDocumentationComments = value;

				// Raise an event so that the entire document is reclassified
				ITextSnapshot snapshot = this.Document.CurrentSnapshot;
				this.OnTagsChanged(new TagsChangedEventArgs(new TextSnapshotRange(snapshot, snapshot.TextRange)));
			}
		}

		/// <summary>
		/// Gets or sets whether to highlight 'Actipro' in identifiers.
		/// </summary>
		/// <value>
		/// <c>true</c> if 'Actipro' in identifiers should be highlighted.
		/// </value>
		public bool HighlightIdentifiers { 
			get {
				return highlightIdentifiers;
			}
			set {
				if (highlightIdentifiers == value)
					return;

				highlightIdentifiers = value;

				// Raise an event so that the entire document is reclassified
				ITextSnapshot snapshot = this.Document.CurrentSnapshot;
				this.OnTagsChanged(new TagsChangedEventArgs(new TextSnapshotRange(snapshot, snapshot.TextRange)));
			}
		}
		
	}
}