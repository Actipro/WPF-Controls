using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Utility;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments.Implementation;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IndicatorsDebugging {

	/// <summary>
	/// Represents an adornment manager for a view that renders ealpsed times.
	/// </summary>
	public class ElapsedTimeAdornmentManager : IntraTextAdornmentManagerBase<IEditorView, ElapsedTimeTag> {

		private static AdornmentLayerDefinition layerDefinition = 
			new AdornmentLayerDefinition("ElapsedTime", new Ordering(AdornmentLayerDefinitions.TextForeground.Key, OrderPlacement.Before));

		private const double FontSizeAdjustment = 0.9;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>IntraTextNoteAdornmentManager</c> class.
		/// </summary>
		/// <param name="view">The view to which this manager is attached.</param>
		public ElapsedTimeAdornmentManager(IEditorView view) : base(view, layerDefinition) {}
		
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
		protected override void AddAdornment(AdornmentChangeReason reason, ITextViewLine viewLine, TagSnapshotRange<ElapsedTimeTag> tagRange, TextBounds bounds) {
			var boundsList = viewLine.GetTextBounds(new TextRange(tagRange.SnapshotRange.StartOffset)).ToArray();
			if ((boundsList != null) && (boundsList.Length == 1)) {
				// Create a text block
				var adornment = new TextBlock() {
					FontFamily = SystemFonts.MessageFontFamily,
					FontSize = Math.Round(this.View.DefaultFontSize * FontSizeAdjustment, MidpointRounding.AwayFromZero),
					Opacity = 0.6,
					Text = tagRange.Tag.TimeSpanText
				};

				// Measure the adornment and determine its display location
				adornment.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
				var adornmentLocation = new Point(boundsList[0].Left + this.View.DefaultCharacterWidth, boundsList[0].Top + ((bounds.Height - adornment.DesiredSize.Height) / 2.0));

				// Add the adornment to the layer
				this.AdornmentLayer.AddAdornment(reason, adornment, adornmentLocation, tagRange.Tag.Key, removedCallback: null);
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
