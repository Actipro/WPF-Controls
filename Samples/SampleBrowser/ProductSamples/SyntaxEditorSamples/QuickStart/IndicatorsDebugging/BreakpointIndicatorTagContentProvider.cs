using System;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IndicatorsDebugging {
	
	/// <summary>
	/// Provides IntelliPrompt popup content for a breakpoint indicator tag.
	/// </summary>
	internal class BreakpointIndicatorTagContentProvider : IContentProvider {

		private TagVersionRange<BreakpointIndicatorTag> tagRange;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>BreakpointIndicatorTagContentProvider</c> class.
		/// </summary>
		/// <param name="tagRange">The tag range.</param>
		public BreakpointIndicatorTagContentProvider(TagVersionRange<BreakpointIndicatorTag> tagRange) {
			if (tagRange == null)
				throw new ArgumentNullException("tagRange");

			// Initialize
			this.tagRange = tagRange;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Returns the content to use in various IntelliPrompt popups.
		/// </summary>
		/// <returns>The content to use in various IntelliPrompt popups.</returns>
		public object GetContent() {
			// Get the snapshot range relative to the current snapshot (in case the document changed since the provider was created)
			var snapshotRange = tagRange.VersionRange.Translate(tagRange.VersionRange.Document.CurrentSnapshot);

			var htmlSnippet = String.Format("At line <b>{0}</b>, character <b>{1}</b>{2}",
				snapshotRange.StartPosition.DisplayLine, snapshotRange.StartPosition.DisplayCharacter,
				(tagRange.Tag.IsEnabled ? String.Empty : " <i>(disabled)</i>"));
			return new HtmlContentProvider(htmlSnippet).GetContent();
		}

	}

}

