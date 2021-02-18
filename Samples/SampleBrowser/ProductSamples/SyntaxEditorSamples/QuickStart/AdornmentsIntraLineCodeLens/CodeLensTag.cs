using ActiproSoftware.Text.Tagging;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsIntraLineCodeLens {
    
	/// <summary>
	/// Provides an <see cref="IIntraLineSpacerTag"/> implementation that reserves intra-line space for an element.
	/// </summary>
	public class CodeLensTag : IIntraLineSpacerTag {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the bottom margin of the spacer, which the amount of additional vertical space needed below the view line.
		/// </summary>
		/// <value>The bottom margin of the spacer, which the amount of additional vertical space needed below the view line.</value>
		public double BottomMargin { get; set; }
		
		/// <summary>
		/// Gets or sets the <see cref="CodeLensDeclaration"/> related to this tag.
		/// </summary>
		/// <value>The <see cref="CodeLensDeclaration"/> related to this tag.</value>
		public CodeLensDeclaration Declaration { get; set; }

		/// <summary>
		/// Gets an object that can be used to uniquely identify the spacer, so that an adornment can be rendered within its bounds.
		/// </summary>
		/// <value>An object that can be used to uniquely identify the spacer, so that an adornment can be rendered within its bounds.</value>
		public object Key {
			get {
				return (this.Declaration != null ? this.Declaration.Key : null);
			}
		}
		
		/// <summary>
		/// Gets or sets the top margin of the spacer, which the amount of additional vertical space needed above the view line.
		/// </summary>
		/// <value>The top margin of the spacer, which the amount of additional vertical space needed above the view line.</value>
		public double TopMargin { get; set; }

	}
	
}
