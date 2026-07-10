using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Utility;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments.Implementation;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsCustomDecorator;

/// <summary>
/// Represents an adornment manager for a view that makes a custom decorator under text.
/// </summary>
/// <param name="view">The view to which this manager is attached.</param>
public class CustomAdornmentManager(IEditorView view) : DecorationAdornmentManagerBase<IEditorView, CustomTag>(view, _layerDefinition) {

	private static readonly AdornmentLayerDefinition _layerDefinition = new("Custom", new Ordering(AdornmentLayerDefinitions.TextForeground.Key, OrderPlacement.After));

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Returns a <see cref="Rectangle"/> for a custom decorator.
	/// </summary>
	/// <param name="width">The width of the line.</param>
	private static Rectangle CreateDecorator(double width) {
		// Create a rectangle
		var rect = new Rectangle {
			Width = width,
			Height = 2
		};

		// Add a brush
		var brush = new LinearGradientBrush();
		brush.GradientStops.Add(new GradientStop(Colors.Transparent, -0.5));
		brush.GradientStops.Add(new GradientStop(Colors.Red, 0.0));
		brush.GradientStops.Add(new GradientStop(Colors.Transparent, 0.01));
		rect.Fill = brush;

		var sb = new Storyboard {
			Duration = new Duration(TimeSpan.FromSeconds(4)),
			RepeatBehavior = RepeatBehavior.Forever
		};

		DoubleAnimation animation;

		animation = new DoubleAnimation(0.01, 1.51, new Duration(TimeSpan.FromSeconds(2)));
		Storyboard.SetTargetProperty(animation, new PropertyPath("(0).(1)[2].(2)", Shape.FillProperty, GradientBrush.GradientStopsProperty, GradientStop.OffsetProperty));
		sb.Children.Add(animation);

		animation = new DoubleAnimation(0.99, -0.51, new Duration(TimeSpan.FromSeconds(2))) {
			BeginTime = TimeSpan.FromSeconds(2)
		};
		Storyboard.SetTargetProperty(animation, new PropertyPath("(0).(1)[2].(2)", Shape.FillProperty, GradientBrush.GradientStopsProperty, GradientStop.OffsetProperty));
		sb.Children.Add(animation);

		animation = new DoubleAnimation(0.0, 1.5, new Duration(TimeSpan.FromSeconds(2)));
		Storyboard.SetTargetProperty(animation, new PropertyPath("(0).(1)[1].(2)", Shape.FillProperty, GradientBrush.GradientStopsProperty, GradientStop.OffsetProperty));
		sb.Children.Add(animation);

		animation = new DoubleAnimation(1.0, -0.5, new Duration(TimeSpan.FromSeconds(2))) {
			BeginTime = TimeSpan.FromSeconds(2)
		};
		Storyboard.SetTargetProperty(animation, new PropertyPath("(0).(1)[1].(2)", Shape.FillProperty, GradientBrush.GradientStopsProperty, GradientStop.OffsetProperty));
		sb.Children.Add(animation);

		animation = new DoubleAnimation(-0.5, 1.0, new Duration(TimeSpan.FromSeconds(2)));
		Storyboard.SetTargetProperty(animation, new PropertyPath("(0).(1)[0].(2)", Shape.FillProperty, GradientBrush.GradientStopsProperty, GradientStop.OffsetProperty));
		sb.Children.Add(animation);

		animation = new DoubleAnimation(1.5, 0.0, new Duration(TimeSpan.FromSeconds(2))) {
			BeginTime = TimeSpan.FromSeconds(2)
		};
		Storyboard.SetTargetProperty(animation, new PropertyPath("(0).(1)[0].(2)", Shape.FillProperty, GradientBrush.GradientStopsProperty, GradientStop.OffsetProperty));
		sb.Children.Add(animation);

		sb.Begin(rect);

		return rect;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void AddAdornment(AdornmentChangeReason reason, ITextViewLine viewLine, TagSnapshotRange<CustomTag> tagRange, TextBounds bounds) {
		// Create the adornment
		var element = CreateDecorator(bounds.Width);
		var location = new Point(Math.Round(bounds.Left), bounds.Bottom - 2);

		// Add the adornment to the layer
		AdornmentLayer.AddAdornment(reason, element, location, tag: null, viewLine, tagRange.SnapshotRange, TextRangeTrackingModes.ExpandBothEdges, removedCallback: null);
	}

	/// <inheritdoc/>
	protected override void OnClosed() {
		// Remove any remaining adornments
		AdornmentLayer.RemoveAllAdornments(AdornmentChangeReason.ManagerClosed);

		base.OnClosed();
	}

}
