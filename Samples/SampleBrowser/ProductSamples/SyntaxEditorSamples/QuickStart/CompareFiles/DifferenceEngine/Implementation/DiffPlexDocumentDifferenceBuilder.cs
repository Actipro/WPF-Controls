// This implementation of IDocumentDifferenceBuilder is provided as an example of how to adapt the
// popular DiffPlex library for use with the SyntaxEditor QuickStart that demonstrates comparing two
// files.
//
// DiffPLex on GitHub: https://github.com/mmanela/diffplex
//
// To use the DiffPlex-based engine, define the DIFFPLEX constant for the project and reference the
// DiffPlex NuGet package.

#if DIFFPLEX

using DiffPlex.DiffBuilder.Model;
using System;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CompareFiles.DifferenceEngine.Implementation {

	/// <summary>
	/// Defines an implementation of <see cref="IDocumentDifferenceBuilder"/> for use with DiffPlex.
	/// </summary>
	public class DiffPlexDocumentDifferenceBuilder : IDocumentDifferenceBuilder {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NESTED TYPES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		#region DiffPlexDifference

		/// <summary>
		/// Defines a <see cref="Difference"/> that is based on a DiffPlex <see cref="DiffPiece"/>.
		/// </summary>
		private class DiffPlexDifference : Difference {

			/// <summary>
			/// Initializes a new instance of the <see cref="DiffPlexDifference"/> class.
			/// </summary>
			/// <param name="diffPiece">The <see cref="DiffPiece"/> represented by this difference.</param>
			public DiffPlexDifference(DiffPiece diffPiece) : base(ConvertDifferenceKind(diffPiece?.Type)) {
				if (diffPiece is null)
					throw new ArgumentNullException(nameof(diffPiece));

				// Convert sub-pieces to children
				if (diffPiece.SubPieces != null) {
					DiffPlexDifference prevCharDiff = null;
					foreach (var subDiffPiece in diffPiece.SubPieces) {
						var charDiffLength = ((subDiffPiece.Text is null) ? 0 : subDiffPiece.Text.Length);
						var charDiff = new DiffPlexDifference(subDiffPiece) {
							Length = charDiffLength,
							Position = subDiffPiece.Position.HasValue
								? subDiffPiece.Position.Value - 1
								: (int?)null
						};
						if ((prevCharDiff != null)
							&& (prevCharDiff.Kind == charDiff.Kind)
							&& ((prevCharDiff.Position + prevCharDiff.Length) == charDiff.Position)) {
							// Extend the previous difference that is adjacent to the new difference
							prevCharDiff.Length += charDiff.Length;
						}
						else {
							// Add the new difference
							this.Children.Add(charDiff);
							prevCharDiff = charDiff;
						}
					}
				}
			}

		}

		#endregion DiffPlexDifference

		#region DiffPlexDifferenceCollection

		/// <summary>
		/// Defines a <see cref="DifferenceCollection"/> that is based on DiffPlex <see cref="DiffPaneModel"/>.
		/// </summary>
		private class DiffPlexDifferenceCollection : DifferenceCollection {

			/// <summary>
			/// Initializes a new instance of the <see cref="DiffPlexDifferenceCollection"/> class.
			/// </summary>
			/// <param name="diffPaneModel">The <see cref="DiffPaneModel"/> represented by this collection.</param>
			public DiffPlexDifferenceCollection(DiffPaneModel diffPaneModel) {
				if (diffPaneModel is null)
					throw new ArgumentNullException(nameof(diffPaneModel));

				// Convert each line of the model
				foreach (var line in diffPaneModel.Lines) {
					this.Add(new DiffPlexDifference(line) {
						// Translate the 1-based line position to a 0-based line index
						Position = (line.Position.HasValue ? line.Position.Value - 1 : (int?)null)
					});
				}
			}

		}

		#endregion DiffPlexDifferenceCollection

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Converts a DiffPlex <see cref="ChangeType"/> into the equivalent <see cref="DifferenceKind"/>.
		/// </summary>
		/// <param name="diffPiece">The <see cref="DiffPiece"/> to examine.</param>
		/// <returns>One of the <see cref="DifferenceKind"/> values.</returns>
		private static DifferenceKind ConvertDifferenceKind(ChangeType? changeType) {
			if (!changeType.HasValue)
				return DifferenceKind.None;

			switch (changeType.Value) {
				case ChangeType.Inserted:
					return DifferenceKind.Added;
				case ChangeType.Deleted:
					return DifferenceKind.Removed;
				case ChangeType.Modified:
					return DifferenceKind.Modified;
				case ChangeType.Unchanged:
					return DifferenceKind.None;
				case ChangeType.Imaginary:
					return DifferenceKind.Imaginary;
				default:
					throw new NotImplementedException();
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="IDocumentDifferenceBuilder.Compare(string, string, bool)"/>
		public void Compare(string oldText, string newText, bool ignoreWhiteSpace) {
			// Build a side-by-side difference of the old and new text
			var sideBySideModel = new DiffPlex.DiffBuilder.SideBySideDiffBuilder()
				.BuildDiffModel(oldText, newText, ignoreWhiteSpace);

			// Convert the DiffPlex model into a difference collection
			this.OldDifferences = new DiffPlexDifferenceCollection(sideBySideModel.OldText);
			this.NewDifferences = new DiffPlexDifferenceCollection(sideBySideModel.NewText);
		}

		/// <summary>
		/// Gets the collection of differences in the oldest document from the last compare operation.
		/// </summary>
		public IDifferenceCollection OldDifferences { get; private set; }

		/// <summary>
		/// Gets the collection of differences in the lastest document from the last compare operation.
		/// </summary>
		public IDifferenceCollection NewDifferences { get; private set; }

	}

}

#endif