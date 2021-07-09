using System;
using System.Windows.Media;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsHighlightRange {

	/// <summary>
	/// Provides an <see cref="IClassificationTag"/> implementation that defines a highlight to be applied to a range of text.
	/// </summary>
	public class HighlightRangeTag : IClassificationTag {

		/// <summary>
		/// The key which uniquely identifies the <see cref="IClassificationType"/> to be used by this <see cref="IClassificationTag"/>.
		/// </summary>
		private const string HighlightRangeClassificationTypeKey = "HighlightRange";

		// Implementation Note:
		//
		// This custom IClassificationTag defines a custom IClassificationType which will be used to find the IHighlightingStyle
		// whose formatting will be used when rendering this tag in the editor's view.

		private static readonly IClassificationType highlightRangeClassificationType = new ClassificationType(HighlightRangeClassificationTypeKey, "Highlight Range");

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes the <c>HighlightRangeTag</c> class.
		/// </summary>
		static HighlightRangeTag() {

			// Implementation Note:
			//
			// The IClassificationType must be registered and associated with an IHighlightingStyle
			// so the editor's view can determine the format to be applied for the highlighted ranged.
			// Each editor is associated with an IHighlightingStyleRegistry which defines the styles
			// to use for each IClassificationType. The AmbientHighlightingStyleRegistry is a global
			// IHighlightingStyleRegistry which is used by default. If you choose to use a different
			// IHighlightingStyleRegistry for your editor, the IClassificationType will also need to
			// be registered there.

			// Make sure the classification type is registered with a default style
			AmbientHighlightingStyleRegistry.Instance.Register(highlightRangeClassificationType,
				new HighlightingStyle(foreground: null, background: Color.FromArgb(0x40, 0xFF, 0xF2, 0x0)));
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the <see cref="IClassificationType"/> associated with this tag.
		/// </summary>
		/// <value>The <see cref="IClassificationType"/> associated with this tag.</value>
		public IClassificationType ClassificationType {

			// Implementation Note:
			//
			// This IClassificationTag defines its own custom IClassificationType, but you could also
			// choose to use any registered IClassificationType. Even though we could return the
			// static instance of the IClassificationType created by this class, the following shows
			// how any IClassificationType could be returned from the default registry. The static
			// instance will still be returned as a backup if the IHighlightingStyleRegistry could not
			// find an already registered IClassificationType of the same key.

			get => AmbientHighlightingStyleRegistry.Instance.GetClassificationType(HighlightRangeClassificationTypeKey)
				?? highlightRangeClassificationType;
		}

	}
	
}
