using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsIntraLineViewport;

/// <summary>
/// Provides <see cref="IntraLineViewportTag"/> objects over text ranges.
/// </summary>
public class IntraLineViewportTagger : CollectionTagger<IIntraLineSpacerTag> {

	private IEditorView? _view;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="view">The view to which this manager is attached.</param>
	public IntraLineViewportTagger(IEditorView view) : base("IntraLineViewportTagger", orderings: null, view.SyntaxEditor.Document, isForLanguage: true) {
		_view = view ?? throw new ArgumentNullException(nameof(view));
		_view.VisualElement.SizeChanged += OnViewSizeChanged;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnViewSizeChanged(object sender, SizeChangedEventArgs e) {
		if (_view is not null) {
			foreach (var tagRange in this) {
				var tag = (IntraLineViewportTag)tagRange.Tag;
				var oldBottomMargin = tag.BottomMargin;

				tag.UpdateBottomMargin(_view);

				if (oldBottomMargin != tag.BottomMargin) {
					var changedSnapshotRange = tagRange.VersionRange.Translate(_view.CurrentSnapshot);
					if (changedSnapshotRange.HasValue)
						RaiseTagsChanged(new TagsChangedEventArgs(changedSnapshotRange.Value));
				}
			}
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void OnClosed() {
		// Detach from the view
		if (_view is not null) {
			_view.VisualElement.SizeChanged -= OnViewSizeChanged;
			_view = null;
		}

		base.OnClosed();
	}

	/// <summary>
	/// Raises the <see cref="TagsChanged"/> event.
	/// </summary>
	/// <param name="e">The event data.</param>
	public void RaiseTagsChanged(TagsChangedEventArgs e)
		=> OnTagsChanged(e);

}
