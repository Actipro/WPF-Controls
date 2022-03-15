#if DEBUG
//#define DEBUG_DIFF
#endif

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CompareFiles.DifferenceEngine.Implementation {

	/// <summary>
	/// An object that can build a collection of differences between two documents.
	/// </summary>
	public class DocumentDifferenceBuilder : IDocumentDifferenceBuilder {

		private const			int		LookAheadThreshold	= 100;
		private static readonly Regex	WhiteSpaceRegex		= new Regex(@"\s+", RegexOptions.Compiled);

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NESTED TYPES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Defines and item that can be compared (e.g. a document line or individual text within a line).
		/// </summary>
		[DebuggerDisplay("CompariableItem[{Index}, {Hash}]; {Text}")]
		private struct ComparableItem {

			/// <summary>
			/// Initializes a new instance of the <see cref="ComparableItem"/> struct.
			/// </summary>
			/// <param name="index">The zero-based position of the item within a collection.</param>
			/// <param name="text">The text associated with the item.</param>
			/// <param name="ignoreWhiteSpace"><c>true</c> if insigificant differences in white space should be ignored.</param>
			public ComparableItem(int index, string text, bool ignoreWhiteSpace) {
				this.Hash = CalculateHash(text, ignoreWhiteSpace);
				this.Index = index;
				this.Text = text;
			}

			/////////////////////////////////////////////////////////////////////////////////////////////////////
			// NON-PUBLIC PROCEDURES
			/////////////////////////////////////////////////////////////////////////////////////////////////////

			/// <summary>
			/// Calculates the hash used to represent the item.
			/// </summary>
			/// <param name="text">The text associated with the item.</param>
			/// <param name="ignoreWhiteSpace"><c>true</c> if insigificant differences in white space should be ignored.</param>
			/// <returns>The hash code.</returns>
			private static int CalculateHash(string text, bool ignoreWhiteSpace) {
				if (text is null)
					return -1;
				if (ignoreWhiteSpace) {
					// Collapse all groups of white space to a single space, trim start/end
					text = WhiteSpaceRegex.Replace(text, " ")
						.Trim();
				}
				return text.GetHashCode();
			}

			/////////////////////////////////////////////////////////////////////////////////////////////////////
			// PUBLIC PROCEDURES
			/////////////////////////////////////////////////////////////////////////////////////////////////////

			/// <summary>
			/// Gets the calculated hash code for the item.
			/// </summary>
			public int Hash { get; private set; }
			
			/// <summary>
			/// Gets the zero-based position of the item within a collection.
			/// </summary>
			public int Index { get; private set; }

			/// <summary>
			/// Gets the length of the item's text.
			/// </summary>
			public int Length => Text?.Length ?? 0;

			/// <summary>
			/// Gets the text associated with the item.
			/// </summary>
			public string Text { get; private set; }

		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		IDifferenceCollection IDocumentDifferenceBuilder.OldDifferences => this.OldDifferences;

		IDifferenceCollection IDocumentDifferenceBuilder.NewDifferences => this.NewDifferences;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Performs the core operations to compare two collections of items.
		/// </summary>
		/// <param name="oldItems">The comparable items for the old version.</param>
		/// <param name="newItems">The comparable items for the new version.</param>
		/// <param name="oldDifferences">The collection where old differences will be added.</param>
		/// <param name="newDifferences">The collection where new differences will be added.</param>
		/// <param name="ignoreWhiteSpace"><c>true</c> to ignore insignificant white space.</param>
		/// <param name="canCompareSubItems"><c>true</c> if sub-items should be compared for detected modifications (e.g. compare the content of a modified line).</param>
		private static void CompareCore(Dictionary<int, ComparableItem> oldItems, Dictionary<int, ComparableItem> newItems, DifferenceCollection oldDifferences, DifferenceCollection newDifferences, bool ignoreWhiteSpace, bool canCompareSubItems) {
			var oldItemIndex = 0;
			var newItemIndex = 0;

			while ((oldItemIndex < oldItems.Count) && (newItemIndex < newItems.Count)) {
				if (oldItems[oldItemIndex].Hash == newItems[newItemIndex].Hash) {
					// No change in items
					oldDifferences.Add(CreateRealDifference(DifferenceKind.None, oldItems[oldItemIndex]));
					newDifferences.Add(CreateRealDifference(DifferenceKind.None, newItems[newItemIndex]));

					// Advance the old and new items
					oldItemIndex++;
					newItemIndex++;
				}
				else {
					// Items don't match, so try to find the next closest match

					var nextNewItemMatchIndex = FindMatchingItem(newItems, newItemIndex + 1, oldItems[oldItemIndex]);
					var nextOldItemMatchIndex = FindMatchingItem(oldItems, oldItemIndex + 1, newItems[newItemIndex]);
					if (nextNewItemMatchIndex.HasValue && nextOldItemMatchIndex.HasValue) {
						// Both sides match an item further ahead in the collection, so use the one with the cloest match
						// and a preference for non-white space matches
						var newItemDelta = nextNewItemMatchIndex.Value - newItemIndex;
						var oldItemDelta = nextOldItemMatchIndex.Value - oldItemIndex;

						int targetOldItemIndex;
						int targetNewItemIndex;

						if ((oldItemDelta <= newItemDelta)
							|| (string.IsNullOrWhiteSpace(newItems[newItemIndex].Text) && !string.IsNullOrWhiteSpace(oldItems[oldItemIndex].Text))) {
							// The current old item will be matched to new item further ahead in the collection
							targetOldItemIndex = oldItemIndex;
							targetNewItemIndex = nextNewItemMatchIndex.Value;
						}
						else {
							// The current new item will be matched to old item further ahead in the collection
							targetOldItemIndex = nextOldItemMatchIndex.Value;
							targetNewItemIndex = newItemIndex;
						}

						while ((oldItemIndex < targetOldItemIndex) && (newItemIndex < targetNewItemIndex)) {
							// Each unmatched item leading up to the matched item will be considered a change
							var oldDifference = CreateRealDifference(DifferenceKind.Modified, oldItems[oldItemIndex]);
							var newDifference = CreateRealDifference(DifferenceKind.Modified, newItems[newItemIndex]);
							oldDifferences.Add(oldDifference);
							newDifferences.Add(newDifference);

							// Optionally compare the sub items
							if (canCompareSubItems)
								CompareSubItems(oldItems[oldItemIndex], newItems[newItemIndex], ignoreWhiteSpace, oldDifference.Children, newDifference.Children);

							// Advance the old and new items
							oldItemIndex++;
							newItemIndex++;
						}

						while (oldItemIndex < targetOldItemIndex) {
							// Any remaining old items were removed from the new collection
							oldDifferences.Add(CreateRealDifference(DifferenceKind.Removed, oldItems[oldItemIndex]));
							newDifferences.Add(new Difference(DifferenceKind.Imaginary));

							// Advance the old item only
							oldItemIndex++;
						}

						while (newItemIndex < targetNewItemIndex) {
							// Any remaining new items were added to the new collection
							oldDifferences.Add(new Difference(DifferenceKind.Imaginary));
							newDifferences.Add(CreateRealDifference(DifferenceKind.Added, newItems[newItemIndex]));

							// Advance the new item only
							newItemIndex++;
						}
					}
					else if (nextNewItemMatchIndex.HasValue) {
						// Old item matched a new item further ahead in the collection, so items were added to the new collection
						var addedItemCount = nextNewItemMatchIndex.Value - newItemIndex;

						// Indicate each item that was added to the new collection with matching imaginary items in the old collection
						for (var i = 0; i < addedItemCount; i++) {
							oldDifferences.Add(new Difference(DifferenceKind.Imaginary));
							newDifferences.Add(CreateRealDifference(DifferenceKind.Added, newItems[newItemIndex]));

							// Advance the new item only
							newItemIndex++;
						}

						// Add the matching items
						oldDifferences.Add(CreateRealDifference(DifferenceKind.None, oldItems[oldItemIndex]));
						newDifferences.Add(CreateRealDifference(DifferenceKind.None, newItems[nextNewItemMatchIndex.Value]));

						// Advance the old and new items
						oldItemIndex++;
						newItemIndex = nextNewItemMatchIndex.Value + 1;
					}
					else if (nextOldItemMatchIndex.HasValue) {
						// New item matched an old item further ahead in the collection, so items were removed from the old collection
						var removedItemCount = nextOldItemMatchIndex.Value - oldItemIndex;

						// Indicate each item that was removed from the old collection with matching imaginary items in the new collection
						for (var i = 0; i < removedItemCount; i++) {
							oldDifferences.Add(CreateRealDifference(DifferenceKind.Removed, oldItems[oldItemIndex]));
							newDifferences.Add(new Difference(DifferenceKind.Imaginary));

							// Advance the old item only
							oldItemIndex++;
						}

						// Add the matching items
						oldDifferences.Add(CreateRealDifference(DifferenceKind.None, oldItems[nextOldItemMatchIndex.Value]));
						newDifferences.Add(CreateRealDifference(DifferenceKind.None, newItems[newItemIndex]));

						// Advance the old and new items
						oldItemIndex = nextOldItemMatchIndex.Value + 1;
						newItemIndex++;
					}
					else {
						// The old and new items did not match any other items, so the same item was modified
						var oldDifference = CreateRealDifference(DifferenceKind.Modified, oldItems[oldItemIndex]);
						var newDifference = CreateRealDifference(DifferenceKind.Modified, newItems[newItemIndex]);
						oldDifferences.Add(oldDifference);
						newDifferences.Add(newDifference);

						// Optionally compare the sub items
						if (canCompareSubItems)
							CompareSubItems(oldItems[oldItemIndex], newItems[newItemIndex], ignoreWhiteSpace, oldDifference.Children, newDifference.Children);

						// Advance the old and new items
						oldItemIndex++;
						newItemIndex++;
					}
				}
			}

			if (oldItemIndex < oldItems.Count) {
				Debug.Assert(newItemIndex >= newItems.Count);

				// Old collection had more items than new collection, so extra items were removed from old collection
				while (oldItemIndex < oldItems.Count) {
					oldDifferences.Add(CreateRealDifference(DifferenceKind.Removed, oldItems[oldItemIndex]));
					newDifferences.Add(new Difference(DifferenceKind.Imaginary));

					// Advance the old item only
					oldItemIndex++;
				}
			}
			else if (newItemIndex < newItems.Count) {
				Debug.Assert(oldItemIndex >= oldItems.Count);

				// New collection had more items than old collection, so extra items were added to new collection
				while (newItemIndex < newItems.Count) {
					oldDifferences.Add(new Difference(DifferenceKind.Imaginary));
					newDifferences.Add(CreateRealDifference(DifferenceKind.Added, newItems[newItemIndex]));

					// Advance the new item only
					newItemIndex++;
				}
			}

			#if DEBUG_DIFF
			if (canCompareSubItems) {
				// At the document level, output the results of the comparison
				OutputDifferences("OLD", oldItems, oldDifferences);
				OutputDifferences("NEW", newItems, newDifferences);
			}
			#endif
		}

		/// <summary>
		/// Compares the sub-items of two items.
		/// </summary>
		/// <param name="oldItem">The old item whose sub-items will be examined.</param>
		/// <param name="newItem">The new item whose sub-items will be examined.</param>
		/// <param name="ignoreWhiteSpace"><c>true</c> to ignore insignificant white space.</param>
		/// <param name="oldDifferences">The collection where old differences will be added.</param>
		/// <param name="newDifferences">The collection where new differences will be added.</param>
		private static void CompareSubItems(ComparableItem oldItem, ComparableItem newItem, bool ignoreWhiteSpace, DifferenceCollection oldDifferences, DifferenceCollection newDifferences) {
			// Break item item down into sub-items
			var oldSubItems = ConvertComparableItemsToDictionary(ItemizeLine(oldItem.Text, ignoreWhiteSpace));
			var newSubItems = ConvertComparableItemsToDictionary(ItemizeLine(newItem.Text, ignoreWhiteSpace));

			// Perform the comparison
			CompareCore(oldSubItems, newSubItems, oldDifferences, newDifferences, ignoreWhiteSpace, canCompareSubItems: false);
		}

		/// <summary>
		/// Converts an <see cref="IEnumerable{T}"/> of <see cref="ComparableItem"/> into a dictionary keyed from <see cref="ComparableItem.Index"/>.
		/// </summary>
		/// <param name="items">The items to be converted.</param>
		/// <returns>A new dictionary.</returns>
		private static Dictionary<int, ComparableItem> ConvertComparableItemsToDictionary(IEnumerable<ComparableItem> items) {
			var dictionary = new Dictionary<int, ComparableItem>();
			foreach (var item in items) {
				dictionary[item.Index] = item;
				Debug.Assert(item.Index == dictionary.Count - 1, "The index of items should be contiguous values starting at zero.");
			}
			return dictionary;
		}

		/// <summary>
		/// Creates a real (i.e. non-imaginary) difference for an item.
		/// </summary>
		/// <param name="kind">The difference kind.</param>
		/// <param name="item">The item associated with the difference.</param>
		/// <returns>A new instance of <see cref="Difference"/>.</returns>
		private static Difference CreateRealDifference(DifferenceKind kind, ComparableItem item) {
			if (kind == DifferenceKind.Imaginary)
				throw new ArgumentOutOfRangeException(nameof(kind));
			return new Difference(kind, item.Index) {
				Length = item.Length
			};
		}

		/// <summary>
		/// Itemizes a document into individual lines that can be compared.
		/// </summary>
		/// <param name="documentText">The document text.</param>
		/// <param name="ignoreWhiteSpace"><c>true</c> to ignore insignificant white space.</param>
		/// <returns>An <see cref="IEnumerable{T}"/> of <see cref="ComparableItem"/> for each line.</returns>
		private static IEnumerable<ComparableItem> ItemizeDocument(string documentText, bool ignoreWhiteSpace) {
			var nextIndex = 0;
			using (var reader = new StringReader(documentText)) {
				string line;
				while ((line = reader.ReadLine()) != null)
					yield return new ComparableItem(nextIndex++, line, ignoreWhiteSpace);
			}

			// If a document ends with a line terminator, StringReader will not pick up the blank line at the end of the document
			if (documentText.EndsWith("\n") || documentText.EndsWith("\r"))
				yield return new ComparableItem(nextIndex++, string.Empty, ignoreWhiteSpace);
		}

		/// <summary>
		/// Itemizes a document line into individual text pieces that can be compared.
		/// </summary>
		/// <param name="lineText">The document line text.</param>
		/// <param name="ignoreWhiteSpace"><c>true</c> to ignore insignificant white space.</param>
		/// <returns>An <see cref="IEnumerable{T}"/> of <see cref="ComparableItem"/> for sub-text piece of the line.</returns>
		private static IEnumerable<ComparableItem> ItemizeLine(string lineText, bool ignoreWhiteSpace) {
			var nextIndex = 0;
			if (!string.IsNullOrEmpty(lineText)) {
				// Break the line into pieces at every group of white space
				var nextPieceStartIndex = 0;
				var match = WhiteSpaceRegex.Match(lineText);
				while (match.Success) {
					// Yield a piece for the text, if any, before the white space
					var previousPieceLength = match.Index - nextPieceStartIndex;
					if (previousPieceLength > 0)
						yield return new ComparableItem(nextIndex++, lineText.Substring(nextPieceStartIndex, previousPieceLength), ignoreWhiteSpace);

					// Yield a piece for the white space itself
					yield return new ComparableItem(nextIndex++, lineText.Substring(match.Index, match.Length), ignoreWhiteSpace);

					// Mark the beginning of the next non-white space piece
					nextPieceStartIndex = match.Index + match.Length;

					// Move to the next white space delimiter
					match = match.NextMatch();
				}

				// Yield a piece for text after the last white space
				yield return new ComparableItem(nextIndex++, lineText.Substring(nextPieceStartIndex), ignoreWhiteSpace);
			}
		}

		/// <summary>
		/// Finds the next matching item.
		/// </summary>
		/// <param name="lines">The lines to examine.</param>
		/// <param name="startIndex">The index of the first item to search.</param>
		/// <param name="comparisonItem">The item to compare when determining a match.</param>
		/// <returns>The index of the match if found; otherwise <c>null</c>.</returns>
		private static int? FindMatchingItem(Dictionary<int, ComparableItem> lines, int startIndex, ComparableItem comparisonItem) {
			var endIndex = Math.Min(startIndex + LookAheadThreshold, lines.Count - 1);
			for (int i = startIndex; i <= endIndex; i++) {
				if (lines[i].Hash == comparisonItem.Hash)
					return i;
			}
			return null;
		}

		#if DEBUG_DIFF
		/// <summary>
		/// Outputs the differences detected in a document.
		/// </summary>
		/// <param name="name">A name to identify the differences.</param>
		/// <param name="lines">The lines that were compared.</param>
		/// <param name="differences">The detected differences.</param>
		private static void OutputDifferences(string name, Dictionary<int, ComparableItem> lines, IDifferenceCollection differences) {
			Debug.WriteLine("");
			Debug.WriteLine($"--------------------[{name}]--------------------");
			foreach (var diff in differences) {
				string lineText = null;
				if ((diff.Position.HasValue) && (lines.TryGetValue(diff.Position.Value, out var line)))
					lineText = line.Text;
				Debug.WriteLine($"{diff.Position,3} [{diff.Kind,-9}]: {lineText}");
				if (diff.Kind == DifferenceKind.Modified) {
					string charText = string.Empty;
					foreach (var subDiff in diff.Children) {
						switch (subDiff.Kind) {
							case DifferenceKind.Modified:
								charText += new string('~', subDiff.Length);
								break;
							case DifferenceKind.Removed:
								charText += new string('-', subDiff.Length);
								break;
							case DifferenceKind.Added:
								charText += new string('+', subDiff.Length);
								break;
							case DifferenceKind.Imaginary:
								//charText += new string('?', subDiff.Length);
								break;
							case DifferenceKind.None:
								charText += new string(' ', subDiff.Length);
								break;
						}
					}
					if (!string.IsNullOrWhiteSpace(charText))
						Debug.WriteLine("                 " + charText);
				}
			}
			Debug.WriteLine("---------------------------------------------");
			Debug.WriteLine("");
			Debug.WriteLine("");
		}
		#endif

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="IDocumentDifferenceBuilder.Compare(string, string, bool)"/>
		public void Compare(string oldText, string newText, bool ignoreWhiteSpace) {
			// Break each document down into lines
			var oldLines = ConvertComparableItemsToDictionary(ItemizeDocument(oldText, ignoreWhiteSpace));
			var newLines = ConvertComparableItemsToDictionary(ItemizeDocument(newText, ignoreWhiteSpace));

			// Initialize the differences
			OldDifferences = new DifferenceCollection();
			NewDifferences = new DifferenceCollection();

			// Perform the comparison
			CompareCore(oldLines, newLines, OldDifferences, NewDifferences, ignoreWhiteSpace, canCompareSubItems: true);
		}

		/// <inheritdoc cref="IDocumentDifferenceBuilder.NewDifferences"/>
		public DifferenceCollection NewDifferences { get; private set; }

		/// <inheritdoc cref="IDocumentDifferenceBuilder.OldDifferences"/>
		public DifferenceCollection OldDifferences { get; private set; }

	}

}
