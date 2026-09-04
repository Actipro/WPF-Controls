using ActiproSoftware.Text;
using ActiproSoftware.Text.Parsing;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsIntraLineCodeLens;

/// <summary>
/// Provides <see cref="CodeLensTag"/> objects over text ranges.
/// </summary>
public class CodeLensTagger : TaggerBase<IIntraLineSpacerTag> {

	private readonly List<CodeLensDeclaration> _cachedDeclarations = [];

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="document">The document to which this tagger is attached.</param>
	public CodeLensTagger(ICodeDocument document) : base(nameof(CodeLensTagger), orderings: null, document, isForLanguage: true) {
		// Initialize declarations and tags from the current document
		CacheDeclarationsAndInvalidateTags();

		// Watch for parse data changes
		document.ParseDataChanged += OnDocumentParseDataChanged;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Performs a binary search for the declaration that starts at or after the specified snapshot offset.
	/// </summary>
	/// <param name="snapshotRange">The target snapshot range.</param>
	/// <returns>
	/// The index of the specified value in the specified array, if value is found.
	/// If value is not found and value is less than one or more elements in array,
	/// a negative number which is the bitwise complement of the index of the first element that is larger than value.
	/// If value is not found and value is greater than any of the elements in array,
	/// a negative number which is the bitwise complement of (the index of the last element plus 1).
	/// </returns>
	private int BinarySearchDeclarations(TextSnapshotRange snapshotRange) {
		var lowerIndex = 0;
		var upperIndex = _cachedDeclarations.Count - 1;
		int index;

		while (lowerIndex <= upperIndex) {
			index = (lowerIndex + upperIndex) / 2;

			var translatedSnapshotRange = _cachedDeclarations[index].VersionRange.Translate(snapshotRange.Snapshot);
			if (!translatedSnapshotRange.HasValue)
				return -1;
			else if (translatedSnapshotRange.Value.StartOffset == snapshotRange.StartOffset)
				return index;
			else if (translatedSnapshotRange.Value.StartOffset > snapshotRange.StartOffset)
				upperIndex = index - 1;
			else
				lowerIndex = index + 1;
		}

		if (upperIndex >= 0) {
			var translatedSnapshotRange = _cachedDeclarations[upperIndex].VersionRange.Translate(snapshotRange.Snapshot);
			if (!translatedSnapshotRange.HasValue)
				return -1;
			else if (translatedSnapshotRange.Value.StartOffset > snapshotRange.StartOffset)
				return ~upperIndex;
			else
				return ~(upperIndex + 1);
		}
		else
			return -1;
	}

	/// <summary>
	/// Caches parsed declarations and invalidates tags.
	/// </summary>
	private void CacheDeclarationsAndInvalidateTags() {
		if (Document is null)
			return;

		var snapshot = Document.CurrentSnapshot;

		if (Document.ParseData is CodeLensParseData parseData) {
			int? invalidStartOffset = null;
			int? invalidEndOffset = null;

			var cacheIndex = 0;
			foreach (var declaration in parseData.Declarations) {
				var translatedSnapshotRange = declaration.VersionRange.Translate(snapshot);
				if (!translatedSnapshotRange.HasValue)
					continue;

				var declarationStartOffset = translatedSnapshotRange.Value.StartOffset;

				// Remove old cached declarations that no longer apply and are before the current declaration
				var currentCachedDeclarationMatchesOffset = false;
				while (cacheIndex < _cachedDeclarations.Count) {
					translatedSnapshotRange = _cachedDeclarations[cacheIndex].VersionRange.Translate(snapshot);
					if (!translatedSnapshotRange.HasValue)
						continue;

					var cachedDeclarationStartOffset = translatedSnapshotRange.Value.StartOffset;

					if (cachedDeclarationStartOffset < declarationStartOffset) {
						_cachedDeclarations.RemoveAt(cacheIndex);

						if (!invalidStartOffset.HasValue)
							invalidStartOffset = cachedDeclarationStartOffset;
						invalidEndOffset = cachedDeclarationStartOffset;
					}
					else {
						currentCachedDeclarationMatchesOffset = (cachedDeclarationStartOffset == declarationStartOffset);
						break;
					}
				}

				// If the current cached declaration matches the current declaration's offset...
				if (currentCachedDeclarationMatchesOffset) {
					// If there is a key match...
					if (declaration.Key == _cachedDeclarations[cacheIndex].Key) {
						// Keep using the same declaration as before
						cacheIndex++;
						continue;
					}
					else {
						// Since the key has changed, remove the old cached declaration
						_cachedDeclarations.RemoveAt(cacheIndex);
					}
				}

				// Add a new declaration
				_cachedDeclarations.Insert(cacheIndex++, declaration);

				if (!invalidStartOffset.HasValue)
					invalidStartOffset = declarationStartOffset;
				invalidEndOffset = declarationStartOffset;
			}

			// Remove remaining old cached declarations
			while (cacheIndex < _cachedDeclarations.Count) {
				var translatedSnapshotRange = _cachedDeclarations[cacheIndex].VersionRange.Translate(snapshot);

				_cachedDeclarations.RemoveAt(cacheIndex);

				if (translatedSnapshotRange.HasValue) {
					var cachedDeclarationStartOffset = translatedSnapshotRange.Value.StartOffset;

					if (!invalidStartOffset.HasValue)
						invalidStartOffset = cachedDeclarationStartOffset;
					invalidEndOffset = cachedDeclarationStartOffset;
				}
			}

			// Invalidate any affected range
			if ((invalidStartOffset.HasValue) && (invalidEndOffset.HasValue))
				OnTagsChanged(new TagsChangedEventArgs(new TextSnapshotRange(snapshot, invalidStartOffset.Value, invalidEndOffset.Value)));
		}
		else if (_cachedDeclarations.Count > 0) {
			var snapshotRange = new TextSnapshotRange(
				snapshot,
				startOffset: _cachedDeclarations[0].VersionRange.Translate(snapshot)?.StartOffset ?? 0,
				endOffset: _cachedDeclarations[_cachedDeclarations.Count - 1].VersionRange.Translate(snapshot)?.StartOffset ?? snapshot.Length
			);

			_cachedDeclarations.Clear();

			OnTagsChanged(new TagsChangedEventArgs(snapshotRange));
		}
	}

	private void OnDocumentParseDataChanged(object? sender, ParseDataPropertyChangedEventArgs e) {
		// Refresh declarations and tags after parse
		CacheDeclarationsAndInvalidateTags();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override IEnumerable<TagSnapshotRange<IIntraLineSpacerTag>> GetTags(NormalizedTextSnapshotRangeCollection snapshotRanges, object? parameter) {
		if (snapshotRanges is not null) {
			foreach (var snapshotRange in snapshotRanges) {
				var index = BinarySearchDeclarations(snapshotRange);
				if (index < 0)
					index = ~index;

				while (index < _cachedDeclarations.Count) {
					var declaration = _cachedDeclarations[index++];

					var translatedSnapshotRange = declaration.VersionRange.Translate(snapshotRange.Snapshot);
					if (translatedSnapshotRange.HasValue) {
						var startOffset = translatedSnapshotRange.Value.StartOffset;
						if (snapshotRange.Contains(startOffset)) {
							yield return new TagSnapshotRange<IIntraLineSpacerTag>(
								new TextSnapshotRange(snapshotRange.Snapshot, startOffset),
								new CodeLensTag(declaration) { TopMargin = 12 }
							);
						}
					}
				}
			}
		}
	}

	/// <inheritdoc/>
	protected override void OnClosed() {
		base.OnClosed();

		// Stop watching for parse data changes
		if (Document is not null)
			Document.ParseDataChanged -= OnDocumentParseDataChanged;
	}

}
