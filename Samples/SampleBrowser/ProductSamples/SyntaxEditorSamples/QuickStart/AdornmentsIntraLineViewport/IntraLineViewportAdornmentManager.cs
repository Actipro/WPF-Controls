using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Utility;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsIntraLineViewport;

/// <summary>
/// Represents an adornment manager for a view that renders intra-text notes.
/// </summary>
public class IntraLineViewportAdornmentManager : IntraLineAdornmentManagerBase<IEditorView, IntraLineViewportTag> {

	private static readonly AdornmentLayerDefinition _layerDefinition = new("IntraLineViewport", new Ordering(AdornmentLayerDefinitions.Caret.Key, OrderPlacement.Before));

	private readonly List<Tuple<WeakReference, AdornmentElement>> _cachedElements = [];

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="view">The view to which this manager is attached.</param>
	public IntraLineViewportAdornmentManager(IEditorView view) : base(view, _layerDefinition) {
		// Attach to events
		view.TextAreaLayout += OnViewTextAreaLayout;
		view.VisualElement.SizeChanged += OnViewSizeChanged;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Examines the cached elements, prunes ones that no longer have valid tags, and returns a match if found.
	/// </summary>
	/// <param name="tag">The target tag for which to search.</param>
	private AdornmentElement? GetCachedElement(object? tag) {
		AdornmentElement? targetElement = null;

		if (tag is not null) {
			for (var index = _cachedElements.Count - 1; index >= 0; index--) {
				var tagRef = _cachedElements[index].Item1;
				if (tagRef.IsAlive) {
					// If there is a tag match, use the cached element and remove the entry
					if (tagRef.Target == tag) {
						targetElement = _cachedElements[index].Item2;
						_cachedElements.RemoveAt(index);
					}
				}
				else {
					// Remove the entry
					_cachedElements.RemoveAt(index);
				}
			}
		}

		return targetElement;
	}

	/// <summary>
	/// Returns the bounds for the specified <see cref="IntraLineViewportTag"/> to keep it fully visible and stretched across the text area width.
	/// </summary>
	/// <param name="tag">The <see cref="IntraLineViewportTag"/> to examine.</param>
	private Rect GetAdornmentBounds(IntraLineViewportTag tag) {
		var viewportBounds = View.TransformToTextArea(View.TextAreaViewportBounds);
		var y = 0.0;
		return new Rect(View.ScrollState.HorizontalAmount, y, viewportBounds.Width, tag.BottomMargin);
	}

	/// <summary>
	/// Occurs when an adornment is removed.
	/// </summary>
	/// <param name="adornment">The <see cref="IAdornment"/> that is removed.</param>
	private void OnAdornmentRemoved(IAdornment adornment) {
		if (adornment.VisualElement is AdornmentElement element) {
			_cachedElements.Add(Tuple.Create(new WeakReference(adornment.Tag), element));
			element.Tag = null;
		}
	}

	private void OnViewSizeChanged(object sender, SizeChangedEventArgs e)
		=> UpdateAdornmentBounds();

	private void OnViewTextAreaLayout(object? sender, TextViewTextAreaLayoutEventArgs e)
		=> UpdateAdornmentBounds();

	/// <summary>
	/// Adjusts each adornment's X-coordinate and width so it fills the text area.
	/// </summary>
	private void UpdateAdornmentBounds() {
		if (AdornmentLayer.Adornments.Count > 0) {
			var viewportBounds = View.TransformToTextArea(View.TextAreaViewportBounds);

			foreach (var adornment in AdornmentLayer.Adornments) {
				if (adornment is not null) {
					adornment.Location = new Point(View.ScrollState.HorizontalAmount, adornment.Location.Y);

					if (adornment.VisualElement is FrameworkElement visualElement)
						visualElement.Width = viewportBounds.Width;
				}
			}
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void AddAdornment(ITextViewLine viewLine, TagSnapshotRange<IntraLineViewportTag> tagRange) {
		// Determine the bounds
		var bounds = GetAdornmentBounds(tagRange.Tag);
		var charBounds = viewLine.GetCharacterBounds(tagRange.SnapshotRange.StartOffset);
		if (charBounds.HasValue)
			bounds.Y = charBounds.Value.Bottom;

		// See if a cached version of the element for the tag is available, and create a new one if needed
		var element = GetCachedElement(tagRange.Tag)
			?? new AdornmentElement();

		// Update the size
		element.Width = bounds.Width;
		element.Height = bounds.Height;
		element.Tag = tagRange.Tag;

		// Add the adornment
		AdornmentLayer.AddAdornment(AdornmentChangeReason.Other, element, bounds.Location, tagRange.Tag.Key, OnAdornmentRemoved);
	}

	/// <inheritdoc/>
	protected override void OnClosed() {
		// Detach from events
		View.TextAreaLayout -= OnViewTextAreaLayout;
		View.VisualElement.SizeChanged -= OnViewSizeChanged;

		// Remove any remaining adornments
		AdornmentLayer.RemoveAllAdornments(AdornmentChangeReason.ManagerClosed);

		base.OnClosed();
	}

}
