// This implementation of IDocumentDifferenceBuilder is provided as an example of how to adapt the
//   popular DiffPlex library for use with the SyntaxEditor QuickStart that demonstrates comparing two
//   files.
//
// DiffPLex on GitHub: https://github.com/mmanela/diffplex
//
// To use the DiffPlex-based engine, define the DIFFPLEX constant for the project and reference the
//   DiffPlex NuGet package.

#if DIFFPLEX

using DiffPlex.DiffBuilder.Model;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CompareFiles.DifferenceEngine.Implementation;

/// <summary>
/// Defines an implementation of <see cref="IDocumentDifferenceBuilder"/> for use with DiffPlex.
/// </summary>
public class DiffPlexDocumentDifferenceBuilder : IDocumentDifferenceBuilder {

	// --------------------------------------------------------------------------------------------------
	// NESTED TYPES
	// --------------------------------------------------------------------------------------------------

	#region DiffPlexDifference

	/// <summary>
	/// Defines a <see cref="Difference"/> that is based on a DiffPlex <see cref="DiffPiece"/>.
	/// </summary>
	private class DiffPlexDifference : Difference {

		/// <summary>
		/// Initializes an instance of the class.
		/// </summary>
		/// <param name="diffPiece">The <see cref="DiffPiece"/> represented by this difference.</param>
		public DiffPlexDifference(DiffPiece diffPiece) : base(ConvertDifferenceKind(diffPiece?.Type)) {
			#if NET
			ArgumentNullException.ThrowIfNull(diffPiece);
			#else
			if (diffPiece is null)
				throw new ArgumentNullException(nameof(diffPiece));
			#endif

			// Convert sub-pieces to children
			if (diffPiece.SubPieces is not null) {
				DiffPlexDifference? prevCharDiff = null;
				foreach (var subDiffPiece in diffPiece.SubPieces) {
					var charDiffLength = subDiffPiece.Text?.Length ?? 0;
					var charDiff = new DiffPlexDifference(subDiffPiece) {
						Length = charDiffLength,
						Position = subDiffPiece.Position.HasValue
							? subDiffPiece.Position.Value - 1
							: null
					};
					if (
						prevCharDiff?.Kind == charDiff.Kind
						&& (prevCharDiff.Position + prevCharDiff.Length) == charDiff.Position
					) {
						// Extend the previous difference that is adjacent to the new difference
						prevCharDiff.Length += charDiff.Length;
					}
					else {
						// Add the new difference
						Children.Add(charDiff);
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
		/// Initializes an instance of the class.
		/// </summary>
		/// <param name="diffPaneModel">The <see cref="DiffPaneModel"/> represented by this collection.</param>
		public DiffPlexDifferenceCollection(DiffPaneModel diffPaneModel) {
			#if NET
			ArgumentNullException.ThrowIfNull(diffPaneModel);
			#else
			if (diffPaneModel is null)
				throw new ArgumentNullException(nameof(diffPaneModel));
			#endif

			// Convert each line of the model
			foreach (var line in diffPaneModel.Lines) {
				Add(new DiffPlexDifference(line) {
					// Translate the 1-based line position to a 0-based line index
					Position = (line.Position.HasValue ? line.Position.Value - 1 : null)
				});
			}
		}

	}

	#endregion DiffPlexDifferenceCollection

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Converts a DiffPlex <see cref="ChangeType"/> into the equivalent <see cref="DifferenceKind"/>.
	/// </summary>
	/// <param name="diffPiece">The <see cref="DiffPiece"/> to examine.</param>
	private static DifferenceKind ConvertDifferenceKind(ChangeType? changeType) {
		if (!changeType.HasValue)
			return DifferenceKind.None;

		return changeType.Value switch {
			ChangeType.Inserted => DifferenceKind.Added,
			ChangeType.Deleted => DifferenceKind.Removed,
			ChangeType.Modified => DifferenceKind.Modified,
			ChangeType.Unchanged => DifferenceKind.None,
			ChangeType.Imaginary => DifferenceKind.Imaginary,
			_ => throw new NotImplementedException()
		};
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="IDocumentDifferenceBuilder.Compare"/>
	public void Compare(string oldText, string newText, bool ignoreWhiteSpace) {
		// Build a side-by-side difference of the old and new text
		var sideBySideModel = new DiffPlex.DiffBuilder.SideBySideDiffBuilder()
			.BuildDiffModel(oldText, newText, ignoreWhiteSpace);

		// Convert the DiffPlex model into a difference collection
		OldDifferences = new DiffPlexDifferenceCollection(sideBySideModel.OldText);
		NewDifferences = new DiffPlexDifferenceCollection(sideBySideModel.NewText);
	}

	/// <inheritdoc cref="IDocumentDifferenceBuilder.NewDifferences"/>
	public IDifferenceCollection? OldDifferences { get; private set; }

	/// <inheritdoc cref="IDocumentDifferenceBuilder.OldDifferences"/>
	public IDifferenceCollection? NewDifferences { get; private set; }

}
#endif
