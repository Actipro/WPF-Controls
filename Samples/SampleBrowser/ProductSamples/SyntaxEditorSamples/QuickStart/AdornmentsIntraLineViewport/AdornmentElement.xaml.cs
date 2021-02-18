using ActiproSoftware.Windows.Controls.Docking;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Primitives;
using ActiproSoftware.Windows.Media;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsIntraLineViewport {

	/// <summary>
	/// Represents the adornment element.
	/// </summary>
	public partial class AdornmentElement {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the <c>AdornmentElement</c> class.
		/// </summary>
		public AdornmentElement() {
			InitializeComponent();
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when a tab is closing.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="AdvancedTabItemEventArgs"/> that contains the event data.</param>
		private void OnTabControlTabClosing(object sender, AdvancedTabItemEventArgs e) {
			var view = VisualTreeHelperExtended.GetAncestor<EditorView>(this);
			if (view == null)
				return;

			var tag = this.Tag as IntraLineViewportTag;
			if (tag == null)
				return;

			// Remove the tag
			IntraLineViewportTagger tagger = null;
			if (view.Properties.TryGetValue(typeof(IntraLineViewportTagger), out tagger))
				tagger.Remove(tag);
		}

	}

}
