using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CollapsedRegionsAdvanced {

    /// <summary>
	/// Represents an adornment manager for a view that renders intra-text placeholders for collapsed regions.
    /// </summary>
    public class CollapsedRegionAdornmentManager : IntraTextAdornmentManagerBase<IEditorView, CollapsedRegionTag> {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>CollapsedRegionAdornmentManager</c> class.
		/// </summary>
		/// <param name="view">The view to which this manager is attached.</param>
		public CollapsedRegionAdornmentManager(IEditorView view) : base(view, AdornmentLayerDefinitions.CollapsedRegion) {}

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
		protected override void AddAdornment(AdornmentChangeReason reason, ITextViewLine viewLine, TagSnapshotRange<CollapsedRegionTag> tagRange, TextBounds bounds) {
			// Create a border
			Border outerBorder = new Border();
			outerBorder.Background = Brushes.Transparent;
			outerBorder.BorderBrush = Brushes.Gray;
			outerBorder.BorderThickness = new Thickness(1.0);
			outerBorder.CornerRadius = new CornerRadius(2.0);
			outerBorder.Cursor = Cursors.Arrow;
			outerBorder.SnapsToDevicePixels = true;
			outerBorder.Width = bounds.Width;
			outerBorder.Height = bounds.Height;
			this.AdornmentLayer.AddAdornment(reason, outerBorder, new Point(Math.Round(bounds.Left), Math.Round(bounds.Top)), tagRange.Tag.Key, null);

			// Create the text adornment
			TextBlock element = new TextBlock();
			element.IsHitTestVisible = false;
			element.Text = tagRange.Tag.Text;
			element.FontFamily = this.View.SyntaxEditor.FontFamily;
			element.FontSize = this.View.SyntaxEditor.FontSize;
			element.Foreground = Brushes.Gray;
			element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

			// Get the location
			Point location = new Point(Math.Round(bounds.Left), Math.Round(bounds.Top + (bounds.Height - element.DesiredSize.Height) / 2));

			// Add the text adornment to the layer
			this.AdornmentLayer.AddAdornment(reason, element, location, tagRange.Tag.Key, null);
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
