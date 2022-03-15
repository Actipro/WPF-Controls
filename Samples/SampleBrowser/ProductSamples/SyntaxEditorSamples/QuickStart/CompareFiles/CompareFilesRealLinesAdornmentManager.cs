using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CompareFiles.DifferenceEngine;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Utility;
using System;

#if WINFORMS
using ActiproSoftware.UI.WinForms.Controls.SyntaxEditor;
using ActiproSoftware.UI.WinForms.Controls.SyntaxEditor.Adornments;
using ActiproSoftware.UI.WinForms.Controls.SyntaxEditor.Adornments.Implementation;
using ActiproSoftware.UI.WinForms.Controls.SyntaxEditor.Highlighting;
using ActiproSoftware.UI.WinForms.Controls.SyntaxEditor.Implementation;
using System.Drawing;
using Rect = System.Drawing.Rectangle;
#elif WPF
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Implementation;
using System.Windows;
#endif

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CompareFiles {

	/// <summary>
	/// Represents an adornment manager for a view that renders a adornments for line and character differences when comparing files.
	/// </summary>
	public class CompareFilesRealLinesAdornmentManager : DecorationAdornmentManagerBase<IEditorView, RealDifferenceTag> {

		private static readonly AdornmentLayerDefinition layerDefinition = 
			new AdornmentLayerDefinition("CompareFilesRealLines", new Ordering(AdornmentLayerDefinitions.TextBackground.Key, OrderPlacement.After));

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes the <see cref="CompareFilesRealLinesAdornmentManager"/> class.
		/// </summary>
		static CompareFilesRealLinesAdornmentManager() {
			// Implementation Note: The IClassificationType must be registered and associated with an
			// IHighlightingStyle so the editor's view can determine the format to be applied for the
			// adornment. Each editor is associated with an IHighlightingStyleRegistry which defines
			// the styles to use for each IClassificationType. The AmbientHighlightingStyleRegistry
			// is a global IHighlightingStyleRegistry which is used by default. If you choose to use
			// a different IHighlightingStyleRegistry for your editor, the IClassificationType will
			// also need to be registered there.

			// Make sure the classification types are registered with a default style
			if ((AmbientHighlightingStyleRegistry.Instance.GetClassificationType(CompareFilesClassificationTypes.DifferenceAdded.Key) is null)
				|| (AmbientHighlightingStyleRegistry.Instance.GetClassificationType(CompareFilesClassificationTypes.DifferenceModifiedNew.Key) is null)
				|| (AmbientHighlightingStyleRegistry.Instance.GetClassificationType(CompareFilesClassificationTypes.DifferenceModifiedOld.Key) is null)
				|| (AmbientHighlightingStyleRegistry.Instance.GetClassificationType(CompareFilesClassificationTypes.DifferenceRemoved.Key) is null)) {
				new CompareFilesClassificationTypeProvider(AmbientHighlightingStyleRegistry.Instance).RegisterAll();
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CompareFilesRealLinesAdornmentManager"/> class.
		/// </summary>
		/// <param name="view">The view to which this manager is attached.</param>
		public CompareFilesRealLinesAdornmentManager(IEditorView view) : base(view, layerDefinition) { }

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the <see cref="IHighlightingStyle"/> associated with a tag.
		/// </summary>
		/// <param name="tag">The tag to examine.</param>
		/// <returns>The <see cref="IHighlightingStyle"/> associated with a tag.</returns>
		private IHighlightingStyle GetHighlightingStyle(RealDifferenceTag tag) {
			// Implementation Note: A view can have its own IHighlightingStyleRegistry that is different
			// than the globally available AmbientHighlightingStyleRegistry
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
			if ((adornment.Tag is RealDifferenceTag tag) && (tag.Kind != DifferenceKind.Imaginary)) {
				// Get the highlighting style to be used, quitting if one is not defined
				IHighlightingStyle highlightingStyle = GetHighlightingStyle(tag);
				if (highlightingStyle is null)
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
					context.FillRectangle(bounds, highlightingStyle.Background.Value);

				#if USE_BORDERS_FOR_INLINE_DIFF
				// Render the border (for character differences within a line only)
				if (!tag.IsForLine && highlightingStyle.HasBorder)
					context.DrawRectangle(bounds, highlightingStyle.BorderColor.Value, highlightingStyle.BorderKind, strokeWidth: 2.0f);
				#endif
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Adds an adornment to the <see cref="AdornmentLayer"/>.
		/// </summary>
		/// <param name="reason">An <see cref="AdornmentChangeReason"/> indicating the add reason.</param>
		/// <param name="viewLine">The current <see cref="ITextViewLine"/> being examined.</param>
		/// <param name="tagRange">The <see cref="ITag"/> and the range it covers.</param>
		/// <param name="bounds">The text bounds in which to render an adornment.</param>
		protected override void AddAdornment(AdornmentChangeReason reason, ITextViewLine viewLine, TagSnapshotRange<RealDifferenceTag> tagRange, TextBounds bounds) {
			if ((tagRange.Tag is RealDifferenceTag tag) && (tag.Kind != DifferenceKind.Imaginary)) {
				if (tag.IsForLine) {
					// Define the adornment bounds to cover the text portion of the line (excluding top/bottom adornments)
					var viewLineBounds = viewLine.TextBounds;
					viewLineBounds.Width = 1000000;

					// Add the adornment to the layer
					this.AdornmentLayer.AddAdornment(reason, OnDrawAdornment, viewLineBounds, tag, viewLine, tagRange.SnapshotRange, TextRangeTrackingModes.ExpandBothEdges, null);
				}
				else {
					// Add the character adornment to the layer
					this.AdornmentLayer.AddAdornment(reason, OnDrawAdornment, bounds.Rect, tag, viewLine, tagRange.SnapshotRange, TextRangeTrackingModes.ExpandBothEdges, null);
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
			// Remove any remaining adornments
			this.AdornmentLayer.RemoveAllAdornments(AdornmentChangeReason.ManagerClosed);
		
			// Call the base method
			base.OnClosed();
		}

    }
	
}
