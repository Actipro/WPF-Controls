using ActiproSoftware.Text.Utility;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsAlternatingRows;

/// <summary>
/// Represents an adornment manager for a view that displays a background over alternating rows.
/// </summary>
public class AlternatingRowsAdornmentManager : AdornmentManagerBase<IEditorView> {

	private static readonly AdornmentLayerDefinition _layerDefinition = new("AlternatingRows", new Ordering(AdornmentLayerDefinitions.TextBackground.Key, OrderPlacement.After));

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="view">The view to which this manager is attached.</param>
	public AlternatingRowsAdornmentManager(IEditorView view) : base(view, _layerDefinition, false) {
		// Attach to events
		view.TextAreaLayout += OnViewTextAreaLayout;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Adds an adornment to the <see cref="AdornmentLayer"/>.
	/// </summary>
	/// <param name="viewLine">The current <see cref="ITextViewLine"/> being examined.</param>
	private void AddAdornment(ITextViewLine viewLine) {
		// Get the adornment bounds
		var bounds = viewLine.Bounds;
		bounds.Width = 1000000;

		// Add the adornment to the layer
		AdornmentLayer.AddAdornment(AdornmentChangeReason.Other, OnDrawAdornment, bounds, viewLine, removedCallback: null);
	}

	/// <summary>
	/// Returns the adornments with the specified tag.
	/// </summary>
	/// <param name="tag">The tag for which to search.</param>
	private IAdornment[] GetAdornmentsWithTag(object tag)
		=> AdornmentLayer.FindAdornments(tag);

	/// <summary>
	/// Occurs when the adornment needs to be drawn.
	/// </summary>
	/// <param name="context">The <see cref="TextViewDrawContext"/> to use for rendering.</param>
	/// <param name="adornment">The <see cref="IAdornment"/> to draw.</param>
	private void OnDrawAdornment(TextViewDrawContext context, IAdornment adornment) {
		var color = Color.FromArgb(0x20, 0x80, 0x80, 0x80);
		context.FillRectangle(new Rect(adornment.Location, adornment.Size), color);
	}

	private void OnViewTextAreaLayout(object? sender, TextViewTextAreaLayoutEventArgs e) {
		// Loop through the added/updated lines
		foreach (var viewLine in e.AddedOrUpdatedViewLines) {
			// If an even document line (odd-indexed), add an adornment
			if (viewLine.StartPosition.Line % 2 == 1)
				AddAdornment(viewLine);
		}

		// Loop through the translated lines
		foreach (var viewLine in e.TranslatedViewLines) {
			// If an even document line (odd-indexed)...
			if (viewLine.StartPosition.Line % 2 == 1) {
				// Get any existing adornments
				var adornments = GetAdornmentsWithTag(viewLine);
				if (adornments.Length > 0) {
					// Translate existing adornments
					foreach (var adornment in adornments)
						adornment.Translate(deltaX: 0, deltaY: viewLine.TranslationY);
				}
				else {
					// There are no existing adornments but this line needs one now
					AddAdornment(viewLine);
				}
			}
			else {
				// Odd line (even-indexed) so remove any existing adornments
				AdornmentLayer.RemoveAdornments(AdornmentChangeReason.Other, GetAdornmentsWithTag(viewLine));
			}
		}

		// Loop through the removed lines
		foreach (var viewLine in e.RemovedViewLines)
			AdornmentLayer.RemoveAdornments(AdornmentChangeReason.ViewLineRemoved, GetAdornmentsWithTag(viewLine));
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void OnClosed() {
		// Detach from events
		View.TextAreaLayout -= OnViewTextAreaLayout;

		// Remove any remaining adornments
		AdornmentLayer.RemoveAllAdornments(AdornmentChangeReason.ManagerClosed);

		base.OnClosed();
	}

}
