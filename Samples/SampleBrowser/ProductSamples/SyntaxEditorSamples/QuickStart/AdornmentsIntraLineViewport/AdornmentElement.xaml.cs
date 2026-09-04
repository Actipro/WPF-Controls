using ActiproSoftware.Windows.Controls.Docking;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Primitives;
using ActiproSoftware.Windows.Extensions;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsIntraLineViewport;

/// <summary>
/// Represents the adornment element.
/// </summary>
public partial class AdornmentElement {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public AdornmentElement() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnTabControlTabClosing(object sender, AdvancedTabItemEventArgs e) {
		if (
			this.FindAncestorOfType<EditorView>() is { } view
			&& Tag is IntraLineViewportTag tag
		) {
			// Remove the tag
			if (view.Properties.TryGetValue<IntraLineViewportTagger>(out var tagger))
				tagger!.Remove(tag);
		}
	}

}
