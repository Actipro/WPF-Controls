using System;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Lexing;
using ActiproSoftware.Text.Parsing;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CodeOutliningRangeBased {

	/// <summary>
	/// Represents a <c>Javascript</c> language range-based outlining source.
	/// </summary>
	public class JavascriptOutliningSource : RangeOutliningSourceBase, IParseData {

		private static OutliningNodeDefinition curlyBraceDefinition;
		private static OutliningNodeDefinition multiLineCommentDefinition;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes the <c>JavascriptOutliningSource</c> class.
		/// </summary>
		static JavascriptOutliningSource() {
			// Create the outlining node definitions that will be used by this outlining source to
			//   tell the document's outlining manager how to create new outlining nodes...
			//   Each definition can indicate options such as: 
			//   1) Whether the node is an implementation and will be collapsed when "Collapse to Definitions" is clicked
			//   2) The default collapsed content for the node that appears in the in-line collapsed node box
			//   3) If the node should be collapsed by default when loading a file, such as for #region type nodes
			//   4) If the node is collapsible... when false, no UI appears for the node in the margin

			curlyBraceDefinition = new OutliningNodeDefinition("CurlyBrace");
			curlyBraceDefinition.IsImplementation = true;
			
			multiLineCommentDefinition = new OutliningNodeDefinition("MultiLineComment");
			multiLineCommentDefinition.DefaultCollapsedContent = "/**/";
			multiLineCommentDefinition.IsImplementation = true;
		}
		
		/// <summary>
		/// Initializes a new instance of the <c>JavascriptOutliningSource</c> class.
		/// </summary>
		/// <param name="snapshot">The <see cref="ITextSnapshot"/> to use for this outlining source.</param>
		public JavascriptOutliningSource(ITextSnapshot snapshot) : base(snapshot) {
			int curlyBraceStartOffset = -1;
			int curlyBraceLevel = 0;
			int commentStartOffset = -1;
			IToken token;

			// Get a text snapshot reader so that we can read tokens
			ITextSnapshotReader reader = snapshot.GetReader(0);

			// Read through the entire snapshot
			while (!reader.IsAtSnapshotEnd) {
				// Get the next token
				token = reader.ReadToken();
				if (token != null) {
					switch (token.Key) {
						case "MultiLineCommentStartDelimiter":
							// A multi-line comment is starting... save its start offset
							if (commentStartOffset == -1)
								commentStartOffset = token.StartOffset;
							break;
						case "MultiLineCommentEndDelimiter":
							// A multi-line comment is ending... add its range to the outlining source
							if (commentStartOffset != -1) {
								this.AddNode(new TextRange(commentStartOffset, token.EndOffset), multiLineCommentDefinition);
								commentStartOffset = -1;
							}
							break;
						case "OpenCurlyBrace":
							// An open curly brace... save its start offset if it's a top-level brace
							if (curlyBraceLevel++ == 0) {
								if (curlyBraceStartOffset == -1)
									curlyBraceStartOffset = token.StartOffset;
							}
							break;
						case "CloseCurlyBrace":
							// A close curly brace... add its range to the outlining source if it's a top-level brace
							if (curlyBraceLevel > 0) {
								curlyBraceLevel--;
								if (curlyBraceLevel == 0) {
									this.AddNode(new TextRange(curlyBraceStartOffset, token.EndOffset), curlyBraceDefinition);
									curlyBraceStartOffset = -1;
								}
							}
							break;
					}
				}
				else
					break;
			}

			// If there are any "open" nodes (never found a matching end), add them too
			if (commentStartOffset != -1)
				this.AddOpenNode(commentStartOffset, multiLineCommentDefinition);
			if (curlyBraceStartOffset != -1)
				this.AddOpenNode(curlyBraceStartOffset, curlyBraceDefinition);
		}
	
	}

}
