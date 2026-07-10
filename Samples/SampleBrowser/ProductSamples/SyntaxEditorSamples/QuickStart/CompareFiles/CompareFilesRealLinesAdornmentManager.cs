using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CompareFiles.DifferenceEngine;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Utility;

#if WINFORMS
using ActiproSoftware.UI.WinForms.Controls.SyntaxEditor;
using ActiproSoftware.UI.WinForms.Controls.SyntaxEditor.Adornments;
using ActiproSoftware.UI.WinForms.Controls.SyntaxEditor.Adornments.Implementation;
using ActiproSoftware.UI.WinForms.Controls.SyntaxEditor.Highlighting;
using ActiproSoftware.UI.WinForms.Controls.SyntaxEditor.Implementation;
using Rect = System.Drawing.Rectangle;
#elif WPF
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Implementation;
#endif

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CompareFiles;

/// <summary>
/// Represents an adornment manager for a view that renders a adornments for line and character differences when comparing files.
/// </summary>
/// <param name="view">The view to which this manager is attached.</param>
public class CompareFilesRealLinesAdornmentManager(IEditorView view) : DecorationAdornmentManagerBase<IEditorView, RealDifferenceTag>(view, _layerDefinition) {

	private static readonly AdornmentLayerDefinition _layerDefinition =
		new("CompareFilesRealLines", new Ordering(AdornmentLayerDefinitions.TextBackground.Key, OrderPlacement.After));

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the class.
	/// </summary>
	static CompareFilesRealLinesAdornmentManager() {
		// NOTE: The IClassificationType must be registered and associated with an
		//   IHighlightingStyle so the editor's view can determine the format to be applied for the
		//   adornment. Each editor is associated with an IHighlightingStyleRegistry which defines
		//   the styles to use for each IClassificationType. The AmbientHighlightingStyleRegistry
		//   is a global IHighlightingStyleRegistry which is used by default. If you choose to use
		//   a different IHighlightingStyleRegistry for your editor, the IClassificationType will
		//   also need to be registered there.

		// Make sure the classification types are registered with a default style
		if (
			AmbientHighlightingStyleRegistry.Instance.GetClassificationType(CompareFilesClassificationTypes.DifferenceAdded.Key) is null
			|| AmbientHighlightingStyleRegistry.Instance.GetClassificationType(CompareFilesClassificationTypes.DifferenceModifiedNew.Key) is null
			|| AmbientHighlightingStyleRegistry.Instance.GetClassificationType(CompareFilesClassificationTypes.DifferenceModifiedOld.Key) is null
			|| AmbientHighlightingStyleRegistry.Instance.GetClassificationType(CompareFilesClassificationTypes.DifferenceRemoved.Key) is null
		) {
			new CompareFilesClassificationTypeProvider(AmbientHighlightingStyleRegistry.Instance).RegisterAll();
		}
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Returns the <see cref="IHighlightingStyle"/> associated with a tag.
	/// </summary>
	/// <param name="tag">The tag to examine.</param>
	private IHighlightingStyle? GetHighlightingStyle(RealDifferenceTag tag) {
		// NOTE: A view can have its own IHighlightingStyleRegistry that is different
		//   than the globally available AmbientHighlightingStyleRegistry
		var highlightingStyleRegistry = View.HighlightingStyleRegistry
			?? AmbientHighlightingStyleRegistry.Instance;

		IClassificationType classificationType;
		switch (tag.Kind) {
			case DifferenceKind.Added:
				classificationType = CompareFilesClassificationTypes.DifferenceAdded;
				break;
			case DifferenceKind.Modified:
				if (tag.IsForLine) {
					// Modified lines have a different style for the oldest and latest versions
					classificationType = tag.IsLatest
						? CompareFilesClassificationTypes.DifferenceModifiedNew
						: CompareFilesClassificationTypes.DifferenceModifiedOld;
				}
				else {
					// Modified words/characters are "removed" from oldest version and "added" to latest version.
					classificationType = tag.IsLatest
						? CompareFilesClassificationTypes.DifferenceAdded
						: CompareFilesClassificationTypes.DifferenceRemoved;
				}
				break;
			case DifferenceKind.Removed:
				classificationType = CompareFilesClassificationTypes.DifferenceRemoved;
				break;
			default:
				return null;
		}

		return highlightingStyleRegistry[classificationType];
	}

	/// <summary>
	/// Occurs when the highlight adornment for a line or character needs to be drawn.
	/// </summary>
	/// <param name="context">The <see cref="TextViewDrawContext"/> to use for rendering.</param>
	/// <param name="adornment">The <see cref="IAdornment"/> to draw.</param>
	private void OnDrawAdornment(TextViewDrawContext context, IAdornment adornment) {
		if (adornment.Tag is RealDifferenceTag { Kind: not DifferenceKind.Imaginary } tag) {
			// Get the highlighting style to be used, quitting if one is not defined
			if (GetHighlightingStyle(tag) is not { } highlightingStyle)
				return;

			// Get the adornment bounds within the text area, accounting for scroll state
			var bounds = new Rect(
				context.TextAreaBounds.X + adornment.Location.X - context.View.ScrollState.HorizontalAmount,
				context.TextAreaBounds.Y + adornment.Location.Y,
				adornment.Size.Width,
				adornment.Size.Height
			);

			// Render the background
			if (highlightingStyle.HasBackground)
				context.FillRectangle(bounds, highlightingStyle.Background!.Value);
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void AddAdornment(AdornmentChangeReason reason, ITextViewLine viewLine, TagSnapshotRange<RealDifferenceTag> tagRange, TextBounds bounds) {
		if (tagRange.Tag is RealDifferenceTag { Kind: not DifferenceKind.Imaginary } tag) {
			if (tag.IsForLine) {
				// Define the adornment bounds to cover the text portion of the line (excluding top/bottom adornments)
				var viewLineBounds = viewLine.TextBounds;
				viewLineBounds.Width = 1000000;

				// Add the adornment to the layer
				AdornmentLayer.AddAdornment(reason, OnDrawAdornment, viewLineBounds, tag, viewLine, tagRange.SnapshotRange, TextRangeTrackingModes.ExpandBothEdges, removedCallback: null);
			}
			else {
				// Add the character adornment to the layer
				AdornmentLayer.AddAdornment(reason, OnDrawAdornment, bounds.Rect, tag, viewLine, tagRange.SnapshotRange, TextRangeTrackingModes.ExpandBothEdges, removedCallback: null);
			}
		}
	}

	/// <inheritdoc/>
	protected override void OnClosed() {
		// Remove any remaining adornments
		AdornmentLayer.RemoveAllAdornments(AdornmentChangeReason.ManagerClosed);

		base.OnClosed();
	}

}
