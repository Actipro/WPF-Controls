#if DEBUG
//#define DEBUG_ADORNMENTS
#if DEBUG_ADORNMENTS
using System.Diagnostics;
#endif
#endif

using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Utility;
using System;
using System.Linq;

#if WINFORMS
using ActiproSoftware.UI.WinForms.Controls.SyntaxEditor;
using ActiproSoftware.UI.WinForms.Controls.SyntaxEditor.Adornments;
using ActiproSoftware.UI.WinForms.Controls.SyntaxEditor.Adornments.Implementation;
using ActiproSoftware.UI.WinForms.Controls.SyntaxEditor.Highlighting;
using ActiproSoftware.UI.WinForms.Controls.SyntaxEditor.Implementation;
using System.Drawing;
using System.Drawing.Drawing2D;
using Rect = System.Drawing.Rectangle;
#elif WPF
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Implementation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
#endif

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CompareFiles {

	/// <summary>
	/// Represents an adornment manager for a view that renders a placeholder for imaginary lines in a file comparison.
	/// </summary>
	public class CompareFilesImaginaryLinesAdornmentManager : IntraLineAdornmentManagerBase<IEditorView, ImaginaryLineDifferenceTag> {

		private const int StandardAdornmentWidth = 1000000;

		private static readonly AdornmentLayerDefinition layerDefinition = 
			new AdornmentLayerDefinition("CompareFilesImaginaryLines", new Ordering(AdornmentLayerDefinitions.TextBackground.Key, OrderPlacement.After));

		#if WINFORMS
		private HatchBrush		hatchBrush;
		#elif WPF
		private SolidColorBrush	foregroundBrush;
		private VisualBrush		hatchBrush;
		#endif

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes the <see cref="CompareFilesImaginaryLinesAdornmentManager"/> class.
		/// </summary>
		static CompareFilesImaginaryLinesAdornmentManager() {
			// Implementation Note: The IClassificationType must be registered and associated with an
			// IHighlightingStyle so the editor's view can determine the format to be applied for the
			// adornment. Each editor is associated with an IHighlightingStyleRegistry which defines
			// the styles to use for each IClassificationType. The AmbientHighlightingStyleRegistry
			// is a global IHighlightingStyleRegistry which is used by default. If you choose to use
			// a different IHighlightingStyleRegistry for your editor, the IClassificationType will
			// also need to be registered there.

			// Make sure the classification types are registered with a default style
			if (AmbientHighlightingStyleRegistry.Instance.GetClassificationType(CompareFilesClassificationTypes.DifferenceImaginary.Key) is null)
				new CompareFilesClassificationTypeProvider(AmbientHighlightingStyleRegistry.Instance).RegisterAll();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CompareFilesImaginaryLinesAdornmentManager"/> class.
		/// </summary>
		/// <param name="view">The view to which this manager is attached.</param>
		public CompareFilesImaginaryLinesAdornmentManager(IEditorView view) : base(view, layerDefinition) { }

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the background brush used for adornments.
		/// </summary>
		/// <returns>A brush.</returns>
		private Brush GetBackgroundBrush() {
			// Get the highlighting style for the imaginary line
			var highlightingStyle = GetHighlightingStyle();

			// Determine the foreground color
			#if WINFORMS
			var foregroundColor = Color.LightGray;
			#elif WPF
			var foregroundColor = Colors.LightGray;
			#endif
			if (highlightingStyle != null)
				foregroundColor = highlightingStyle.Foreground.GetValueOrDefault(foregroundColor);

			// Create the brush if never created or the color has changed
			#if WINFORMS
			if ((hatchBrush is null) || (hatchBrush.ForegroundColor != foregroundColor))
				hatchBrush = new HatchBrush(HatchStyle.BackwardDiagonal, foregroundColor, Color.Transparent);
			#elif WPF
			if ((foregroundBrush == null) || (foregroundBrush.Color != foregroundColor)) {
				foregroundBrush = new SolidColorBrush(foregroundColor);
				foregroundBrush.Freeze();

				// The hatch pattern is achieved by drawing within n a square and repeating that square as a tile.
				// A single diagonal from bottom-left corner to top-right corner cannot be drawn because the line will
				// be clipped by the corner of the square and the adjacent corners will not have had a line
				// drawn through them. To solve this issue, lines cannot be drawn through the corners. Instead
				// two lines are drawn. The first line starts in the middle of the left edge to the middle
				// of the top edge. The second line is drawn from the middle of the bottom edge to the middle
				// of the right edge. To make sure line end caps are not an issue at the edges of the square,
				// each line is extended half a pixel outside the bounds of the square.
				//		Square: 8 x 8
				//		Line 1: [-0.5, 4.5] to [4.5, -0.5]
				//		Line 2: [3.5, 8.5] to [8.5, 3.5]
				hatchBrush = new VisualBrush() {
					TileMode = TileMode.Tile,
					Viewport = new Rect(0, 0, 8, 8),
					ViewportUnits = BrushMappingMode.Absolute,
					Viewbox = new Rect(0, 0, 8, 8),
					ViewboxUnits = BrushMappingMode.Absolute,
					Visual = new Canvas() {
						Children = {
							new System.Windows.Shapes.Path() {
								Stroke = foregroundBrush,
								Data = Geometry.Parse("M -0.5 4.5 L 4.5 -0.5 M 3.5 8.5 L 8.5 3.5"),
							}
						}
					}
				};
			}
			#endif

			return hatchBrush;
		}

		/// <summary>
		/// Gets the <see cref="IHighlightingStyle"/> associated with imaginary lines.
		/// </summary>
		/// <returns>The <see cref="IHighlightingStyle"/> associated with imaginary lines.</returns>
		private IHighlightingStyle GetHighlightingStyle() {
			// Implementation Note: A view can have its own IHighlightingStyleRegistry that is different
			// than the globally available AmbientHighlightingStyleRegistry
			var highlightingStyleRegistry = View.HighlightingStyleRegistry
				?? AmbientHighlightingStyleRegistry.Instance;

			return highlightingStyleRegistry[CompareFilesClassificationTypes.DifferenceImaginary];
		}

		/// <summary>
		/// Gets the <see cref="ImaginaryLineDifferenceTag"/> associated with the given <see cref="IAdornment"/>.
		/// </summary>
		/// <param name="adornment">The adornment to examine.</param>
		/// <returns>The associated <see cref="ImaginaryLineDifferenceTag"/> or <c>null</c> if one could not be determined.</returns>
		private static ImaginaryLineDifferenceTag GetImaginaryLineDifferenceTag(IAdornment adornment) {
			// The IAdornment.Tag property (not to be confused with an ITag) is set to
			// the same value as ITag.Key when the adornment is added and is used to
			// find the ITag associated with the IAdornment.
			return adornment?.ViewLine
				?.GetIntraLineSpacerTags<ImaginaryLineDifferenceTag>()
				.FirstOrDefault(tagSnapshotRange => tagSnapshotRange.Tag.Key == adornment.Tag)
				?.Tag;
		}

		/// <summary>
		/// Occurs when the highlight adornment for a line or character needs to be drawn.
		/// </summary>
		/// <param name="context">The <see cref="TextViewDrawContext"/> to use for rendering.</param>
		/// <param name="adornment">The <see cref="IAdornment"/> to draw.</param>
		private void OnDrawAdornment(TextViewDrawContext context, IAdornment adornment) {
			// Get the spacer tag for the adornment, quitting if one is not found
			var imaginaryLineDifferenceTag = GetImaginaryLineDifferenceTag(adornment);
			if (imaginaryLineDifferenceTag is null)
				return;

			// Get the highlighting style to be used, quitting if a color is not available
			IHighlightingStyle highlightingStyle = GetHighlightingStyle();
			if ((highlightingStyle is null) || !(highlightingStyle.Foreground.HasValue))
				return;

			// The TextViewDrawContext performs drawing operations relative to the entire
			// visible text area, so the current location of the view line is used to
			// determine where the adornment should be rendered relative to that line

			var left = adornment.ViewLine.TextBounds.Left;

			var top = imaginaryLineDifferenceTag.IsBottomPosition
					? adornment.ViewLine.TextBounds.Bottom  // Render below the text of the line
					: adornment.ViewLine.Bounds.Top;		// Render in the reserved space above the line

			var height = imaginaryLineDifferenceTag.IsBottomPosition
				? imaginaryLineDifferenceTag.BottomMargin
				: imaginaryLineDifferenceTag.TopMargin;

			// Render the background
			var bounds = new Rect(left, top, StandardAdornmentWidth, height);
			context.FillRectangle(bounds, GetBackgroundBrush());
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Adds an adornment to the <see cref="AdornmentLayer"/>.
		/// </summary>
		/// <param name="viewLine">The current <see cref="ITextViewLine"/> being examined.</param>
		/// <param name="tagRange">The <see cref="ITag"/> and the range it covers.</param>
		protected override void AddAdornment(ITextViewLine viewLine, TagSnapshotRange<ImaginaryLineDifferenceTag> tagRange) {
			#if DEBUG_ADORNMENTS
			Debug.WriteLine($"{this.GetType().Name}.{nameof(AddAdornment)} :: Adding imaginary line adornment for on document line index {viewLine.StartPosition.Line}");
			#endif

			if (tagRange.Tag.DefaultLineHeight != View.DefaultLineHeight) {
				// The tag's TopMargin and BottomMargin are used to reserve space for the adornment, and the DefaultLineHeight
				// used to calculate the margins has changed. Invalidate all tags so the margins can be recalculated to match
				// the new DefaultLineHeight.
				if (View.Properties[typeof(CompareFilesImaginaryLinesTagger)] is CompareFilesImaginaryLinesTagger tagger)
					tagger.InvalidateAllTags();

				// Quit processing since all adornments will be re-added after invalidation
				return;
			}
			
			// Bounds are determined dynamically when the adornment is rendered
			#if WINFORMS
			var bounds = Rect.Empty;
			#elif WPF
			var bounds = new Rect(0d, 0d, 0d, 0d);
			#endif

			// The IAdornment.Tag property (not to be confused with an ITag) is set to
			// the same value as ITag.Key when the adornment is added and is used to
			// find the ITag associated with the IAdornment.
			var adornmentTag = tagRange.Tag.Key;

			// Add the adornment
			this.AdornmentLayer.AddAdornment(AdornmentChangeReason.Other, OnDrawAdornment, bounds, adornmentTag, viewLine, tagRange.SnapshotRange, Text.TextRangeTrackingModes.ExpandBothEdges, null);			
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
