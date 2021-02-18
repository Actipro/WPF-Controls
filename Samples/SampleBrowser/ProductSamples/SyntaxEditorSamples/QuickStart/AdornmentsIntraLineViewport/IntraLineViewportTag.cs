using System;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Windows.Controls.SyntaxEditor;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsIntraLineViewport {
    
	/// <summary>
	/// Provides an <see cref="IIntraLineSpacerTag"/> implementation that reserves intra-line space for an element.
	/// </summary>
	public class IntraLineViewportTag : IIntraLineSpacerTag {
		
		private const double MaxAdornmentHeight = 300.0;
		private const double MinAdornmentHeight = 90.0;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the bottom margin of the spacer, which the amount of additional vertical space needed below the text line.
		/// </summary>
		/// <value>The baseline of the spacer, which the amount of additional vertical space needed below the text line.</value>
		public double BottomMargin { get; set; }
		
		/// <summary>
		/// Gets or sets an object that can be used to uniquely identify the spacer, so that an adornment can be rendered within its bounds.
		/// </summary>
		/// <value>An object that can be used to uniquely identify the spacer, so that an adornment can be rendered within its bounds.</value>
		public object Key { get; set; }

		/// <summary>
		/// Gets or sets the top margin of the spacer, which the amount of additional vertical space needed above the text line.
		/// </summary>
		/// <value>The top of the spacer, which the amount of additional vertical space needed above the text line.</value>
		public double TopMargin { get; set; }
		
		/// <summary>
		/// Updates the bottom margin based on the view height.
		/// </summary>
		/// <param name="view">The <see cref="IEditorView"/> to examine.</param>
		public void UpdateBottomMargin(IEditorView view) {
			var height = Math.Round(view.TextAreaViewportBounds.Height / 3.0);
			this.BottomMargin = Math.Max(MinAdornmentHeight, Math.Min(MaxAdornmentHeight, height));
		}
		
	}
	
}
