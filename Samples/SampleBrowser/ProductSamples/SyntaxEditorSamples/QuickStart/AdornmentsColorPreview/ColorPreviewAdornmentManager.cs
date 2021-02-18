using System;
using System.Windows;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Utility;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsColorPreview {

    /// <summary>
	/// Represents an adornment manager for a view that makes a color preview box under colors.
    /// </summary>
    public class ColorPreviewAdornmentManager : DecorationAdornmentManagerBase<IEditorView, ColorPreviewTag> {

		private static AdornmentLayerDefinition layerDefinition = 
			new AdornmentLayerDefinition("ColorPreview", new Ordering(AdornmentLayerDefinitions.TextForeground.Key, OrderPlacement.After));

		private const double ColorPreviewHeight = 2;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>ColorPreviewAdornmentManager</c> class.
		/// </summary>
		/// <param name="view">The view to which this manager is attached.</param>
		public ColorPreviewAdornmentManager(IEditorView view) : base(view, layerDefinition) {}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when the highlight adornment needs to be drawn.
		/// </summary>
		/// <param name="context">The <see cref="TextViewDrawContext"/> to use for rendering.</param>
		/// <param name="adornment">The <see cref="IAdornment"/> to draw.</param>
		private void OnDrawHighlightAdornment(TextViewDrawContext context, IAdornment adornment) {
			var tag = adornment.Tag as ColorPreviewTag;
			if (tag != null) {
				// Get the adornment bounds within the text area, accounting for scroll state
				var bounds = new Rect(
					context.TextAreaBounds.X + adornment.Location.X - context.View.ScrollState.HorizontalAmount, 
					context.TextAreaBounds.Y + adornment.Location.Y + adornment.Size.Height - ColorPreviewHeight,
					adornment.Size.Width,
					ColorPreviewHeight
				);

				context.FillRectangle(bounds, tag.Color);
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
		protected override void AddAdornment(AdornmentChangeReason reason, ITextViewLine viewLine, TagSnapshotRange<ColorPreviewTag> tagRange, TextBounds bounds) {
			// Round off the bounds to integers to help ensure crispness
			var adornmentBounds = new Rect(Math.Round(bounds.Left, MidpointRounding.AwayFromZero), Math.Round(bounds.Top, MidpointRounding.AwayFromZero),
				Math.Round(bounds.Width, MidpointRounding.AwayFromZero), Math.Round(bounds.Height, MidpointRounding.AwayFromZero));

			// Add the adornment to the layer
			this.AdornmentLayer.AddAdornment(reason, OnDrawHighlightAdornment, adornmentBounds, tagRange.Tag, viewLine, tagRange.SnapshotRange, TextRangeTrackingModes.ExpandBothEdges, null);
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
