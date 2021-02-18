using System;
using System.Windows;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsIntraLineViewport {
    
	/// <summary>
	/// Provides <see cref="IntraLineViewportTag"/> objects over text ranges.
	/// </summary>
	public class IntraLineViewportTagger : CollectionTagger<IIntraLineSpacerTag> {

		private IEditorView			view;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>IntraLineViewportTagger</c> class.
		/// </summary>
		/// <param name="view">The view to which this manager is attached.</param>
		public IntraLineViewportTagger(IEditorView view) : base("IntraLineViewportTagger", null, (view != null ? view.SyntaxEditor.Document : null), true) {
			if (view == null)
				throw new ArgumentNullException("view");

			// Initialize
			this.view = view;
			this.view.VisualElement.SizeChanged += OnViewSizeChanged;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the size of the element changes.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="SizeChangedEventArgs"/> that contains the event data.</param>
		private void OnViewSizeChanged(object sender, SizeChangedEventArgs e) {
			foreach (var tagRange in this) {
				var tag = (IntraLineViewportTag)tagRange.Tag;
				var oldBottomMargin = tag.BottomMargin;

				tag.UpdateBottomMargin(view);

				if (oldBottomMargin != tag.BottomMargin)
					this.RaiseTagsChanged(new TagsChangedEventArgs(tagRange.VersionRange.Translate(view.CurrentSnapshot)));
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the tagger is closed.
		/// </summary>
		protected override void OnClosed() {
			// Detach from the view
			if (view != null) {
				view.VisualElement.SizeChanged -= OnViewSizeChanged;
				view = null;
			}

			// Call the base method
			base.OnClosed();
		}
		
		/// <summary>
		/// Raises the <see cref="TagsChanged"/> event.
		/// </summary>
		/// <param name="e">A <c>TagsChangedEventArgs</c> that contains the event data.</param>
		public void RaiseTagsChanged(TagsChangedEventArgs e) {
			this.OnTagsChanged(e);
		}

	}

}
