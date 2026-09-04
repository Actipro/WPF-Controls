using ActiproSoftware.Text.Utility;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsWatermark;

/// <summary>
/// Represents an adornment manager for a view that makes a watermark effect on the text area.
/// </summary>
public class WatermarkAdornmentManager : AdornmentManagerBase<IEditorView> {

	private static readonly AdornmentLayerDefinition _layerDefinition = new("Watermark", new Ordering(AdornmentLayerDefinitions.Selection.Key, OrderPlacement.After));
	private readonly IAdornment _watermarkAdornment;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="view">The view to which this manager is attached.</param>
	public WatermarkAdornmentManager(IEditorView view) : base(view, _layerDefinition) {
		// Only let this manager be active when the view has focus
		IsActive = view.HasFocus;

		// Create the watermark adornment
		_watermarkAdornment = CreateWatermarkAdornment();

		// Attach to events
		view.TextAreaLayout += OnViewTextAreaLayout;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates the watermark adornment.
	/// </summary>
	private IAdornment CreateWatermarkAdornment() {
		// Create a UIElement for the watermark
		var textBlock = new TextBlock() {
			Text = "Watermark",
			FontSize = 72,
			IsHitTestVisible = false,
			Opacity = 0.1,
			RenderTransform = new ScaleTransform(),
			RenderTransformOrigin = new Point(0.5, 0.5)
		};

		// Add the UIElement to the adornment layer as an adornment
		return AdornmentLayer.AddAdornment(AdornmentChangeReason.Other, textBlock, new Point(), tag: null, removedCallback: null);
	}

	private void OnViewTextAreaLayout(object? sender, TextViewTextAreaLayoutEventArgs e) {
		// Determine the center of the watermark element
		if (_watermarkAdornment.VisualElement is not TextBlock textBlock)
			return;
		var textBlockCenter = new Point(textBlock.ActualWidth / 2, textBlock.ActualHeight / 2);

		// Get the horizontal scroll
		var view = e.View as IEditorView;
		var firstVisibleX = (view is not null ? view.ScrollState.HorizontalAmount : 0.0);

		// Determine the center of the text area viewport
		var textAreaViewportBounds = e.View.TextAreaViewportBounds;
		var center = new Point(
			firstVisibleX + textAreaViewportBounds.Width / 2 / e.View.SyntaxEditor.ZoomLevelAnimated,
			textAreaViewportBounds.Height / 2 / e.View.SyntaxEditor.ZoomLevelAnimated
		);

		// Adjust scale
		var scaleTrans = (ScaleTransform)textBlock.RenderTransform;
		scaleTrans.ScaleX = 1 / e.View.SyntaxEditor.ZoomLevelAnimated;
		scaleTrans.ScaleY = 1 / e.View.SyntaxEditor.ZoomLevelAnimated;

		// Determine the watermark location
		var watermarkLocation = new Point(center.X - textBlockCenter.X, center.Y - textBlockCenter.Y);

		// Set the watermark location
		_watermarkAdornment.Location = watermarkLocation;
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
