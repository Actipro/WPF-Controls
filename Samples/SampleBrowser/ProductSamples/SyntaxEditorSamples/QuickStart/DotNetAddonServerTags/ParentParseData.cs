using System;
using System.Collections.Generic;
using System.Linq;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Languages.DotNet;
using ActiproSoftware.Text.Languages.DotNet.Ast.Implementation;
using ActiproSoftware.Text.Parsing;
using ActiproSoftware.Text.Parsing.Implementation;
using ActiproSoftware.Text.Parsing.LLParser;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.DotNetAddOnServerTags {
	
	/// <summary>
	/// Provides the result of a parsing operation.
	/// </summary>
	public class ParentParseData : IDotNetParseData {

		private List<IParseError>					errors;
		private ILLParseData						generatedParseData;
		private ITextSnapshot						snapshot;
		private List<Tuple<TextRange, TextRange>>	textRangeMappings		= new List<Tuple<TextRange,TextRange>>();
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the object that contains the abstract syntax tree result.
		/// </summary>
		/// <value>The object that contains the abstract syntax tree result.</value>
		public IAstNode Ast {
			get { 
				return (generatedParseData != null ? generatedParseData.Ast : null); 
			}
		}
		
		/// <summary>
		/// Gets the collection of <see cref="IParseError"/> objects that specify parse errors.
		/// </summary>
		/// <value>The collection of <see cref="IParseError"/> objects that specify parse errors.</value>
		public IEnumerable<IParseError> Errors { 
			get {
				if (errors == null) {
					errors = new List<IParseError>();

					if ((generatedParseData != null) && (generatedParseData.Errors != null) && (generatedParseData.Snapshot != null)) {
						// Loop through errors and translate to the editor snapshot
						foreach (var parseError in generatedParseData.Errors) {
							if (parseError != null) {
								var generatedSnapshotRange = new TextSnapshotRange(generatedParseData.Snapshot, generatedParseData.Snapshot.PositionRangeToTextRange(parseError.PositionRange));
								var editorSnapshotOffset = this.TranslateGeneratedToEditor(new TextSnapshotOffset(generatedParseData.Snapshot, generatedSnapshotRange.StartOffset));
								if (editorSnapshotOffset.HasValue) {
									// Add the error
									var positionRange = snapshot.TextRangeToPositionRange(TextRange.FromSpan(editorSnapshotOffset.Value, Math.Max(1, generatedSnapshotRange.AbsoluteLength)));
									errors.Add(new ParseError(parseError.Level, parseError.Description, positionRange));
								}
							}
						}
					}
				}

				return errors;
			}
		}

		/// <summary>
		/// Gets or sets the generated parse data.
		/// </summary>
		/// <value>The generated parse data.</value>
		public ILLParseData GeneratedParseData { 
			get {
				return generatedParseData;
			}
			set {
				generatedParseData = value;
			}
		}
	
		/// <summary>
		/// Gets the collection of preprocessor directives.
		/// </summary>
		/// <value>The collection of preprocessor directives.</value>
		public IList<PreprocessorDirective> PreprocessorDirectives {
			get { 
				return null; 
			}
		}
		
		/// <summary>
		/// Gets or sets the <see cref="ITextSnapshot"/>, if known, from which the parse errors were created.
		/// </summary>
		/// <value>The <see cref="ITextSnapshot"/>, if known, from which the parse errors were created.</value>
		public ITextSnapshot Snapshot { 
			get {
				return snapshot;
			}
			set {
				snapshot = value;
			}
		}
		
		/// <summary>
		/// Gets the collection of text range mappings from the editor snapshot to the generated snapshots.
		/// </summary>
		/// <value>The collection of text range mappings from the editor snapshot to the generated snapshots.</value>
		public IList<Tuple<TextRange, TextRange>> TextRangeMappings { 
			get {
				return textRangeMappings;
			}
		}
		
		/// <summary>
		/// Translates from an editor snapshot to a generated snapshot.
		/// </summary>
		/// <param name="snapshotOffset">The snapshot offset.</param>
		/// <returns>The translated snapshot, if within a child language section.</returns>
		public TextSnapshotOffset? TranslateEditorToGenerated(TextSnapshotOffset snapshotOffset) {
			if ((generatedParseData != null) && (generatedParseData.Snapshot != null)) {
				// Translate back to the editor snapshot
				snapshotOffset = snapshotOffset.TranslateTo(snapshot, TextOffsetTrackingMode.Negative);

				foreach (var mapping in textRangeMappings) {
					if (mapping.Item1.IntersectsWith(snapshotOffset.Offset)) {
						var generatedSnapshotOffset = new TextSnapshotOffset(generatedParseData.Snapshot, mapping.Item2.StartOffset + (snapshotOffset.Offset - mapping.Item1.StartOffset));
						return generatedSnapshotOffset;
					}
				}
			}

			return null;
		}
		
		/// <summary>
		/// Translates from a generated snapshot to an editor snapshot.
		/// </summary>
		/// <param name="snapshotOffset">The snapshot offset.</param>
		/// <returns>The translated snapshot, if within a child language section.</returns>
		public TextSnapshotOffset? TranslateGeneratedToEditor(TextSnapshotOffset snapshotOffset) {
			if ((generatedParseData != null) && (generatedParseData.Snapshot != null)) {
				// Translate back to the generated parse data's snapshot
				snapshotOffset = snapshotOffset.TranslateTo(generatedParseData.Snapshot, TextOffsetTrackingMode.Negative);

				foreach (var mapping in textRangeMappings) {
					if (mapping.Item2.IntersectsWith(snapshotOffset.Offset)) {
						var editorSnapshotOffset = new TextSnapshotOffset(snapshot, mapping.Item1.StartOffset + (snapshotOffset.Offset - mapping.Item2.StartOffset));
						return editorSnapshotOffset;
					}
				}
			}

			return null;
		}

	}

}
