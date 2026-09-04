using ActiproSoftware.Text.Utility;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments.Implementation;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsSmokeText;

/// <summary>
/// Represents an adornment manager for a view that makes a smoke text effect when text is changed.
/// </summary>
public class SmokeTextAdornmentManager : AdornmentManagerBase<IEditorView> {

	private static readonly AdornmentLayerDefinition _layerDefinition = new("SmokeText", new Ordering(AdornmentLayerDefinitions.Selection.Key, OrderPlacement.After));

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="view">The view to which this manager is attached.</param>
	public SmokeTextAdornmentManager(IEditorView view) : base(view, _layerDefinition) {
		// Only let this manager be active when the view has focus
		IsActive = view.HasFocus;

		// Attach to events
		view.HasFocusChanged += OnViewHasFocusChanged;
		view.SyntaxEditor.DocumentTextChanged += OnSyntaxEditorDocumentTextChanged;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnStoryboardCompleted(object? sender, EventArgs e) {
		// This event assumes a ClockGroup is passed with the Storyboard as its Timeline
		if (sender is ClockGroup { Timeline: Storyboard storyboard }) {
			// Clean up and remove any adornments that are tagged with the storyboard's name
			AdornmentLayer.RemoveAdornments(AdornmentChangeReason.Other, AdornmentLayer.FindAdornments(storyboard.Name));
			return;
		}
	}

	private void OnSyntaxEditorDocumentTextChanged(object? sender, EditorSnapshotChangedEventArgs e) {
		// Don't add effects if the view doesn't have focus
		if (!IsActive)
			return;

		// Get the caret bounds in the view
		var caretBounds = View.GetCharacterBounds(View.Selection.EndPosition);
		if (caretBounds.HasValue) {
			// Render the smoke using adornments
			PuffSmoke(new Point(
				caretBounds.Value.Left,
				caretBounds.Value.Top + (caretBounds.Value.Height / 2)
			));
		}

	}

	private void OnViewHasFocusChanged(object sender, EventArgs e) {
		// Only let this manager be active when the view has focus
		IsActive = View.HasFocus;
	}

	/// <summary>
	/// Renders an smoke text effect.
	/// </summary>
	/// <param name="location">The location of the effect.</param>
	private void PuffSmoke(Point location) {
		var random = new Random();

		var smokeClouds = new List<Ellipse>();
		var smokeCloudCount = 4 + random.Next(2);

		for (var index = 0; index < smokeCloudCount; index++) {
			var smokeCloud = new Ellipse {
				Fill = Brushes.Silver,
				Stroke = Brushes.Gray,
				StrokeThickness = 1.0,
				Opacity = 0.3,

				Width = 10 + random.Next(10),
				Height = 10 + random.Next(10)
			};

			var smokeCloudLocation = new Point(
				location.X - (smokeCloud.Width / 2),
				location.Y - (smokeCloud.Height / 2)
			);

			var group = new TransformGroup();
			smokeCloud.RenderTransform = group;

			var storyboard = new Storyboard {
				Duration = new Duration(TimeSpan.FromSeconds(2.7))
			};
			storyboard.Completed += OnStoryboardCompleted;
			storyboard.Name = string.Format("SC{0}", Guid.NewGuid().ToString().Replace('-', '_'));
			DoubleAnimation? animation;

			AdornmentLayer.AddAdornment(AdornmentChangeReason.Other, smokeCloud, smokeCloudLocation, storyboard.Name, removedCallback: null);

			var scale = new ScaleTransform {
				CenterX = smokeCloud.Width / 2,
				CenterY = smokeCloud.Height / 2
			};
			group.Children.Add(scale);
			var targetScaleFactor = 2 + random.NextDouble();

			animation = new DoubleAnimation {
				To = targetScaleFactor
			};
			Storyboard.SetTargetProperty(animation, new PropertyPath("(0).(1)[0].(2)", UIElement.RenderTransformProperty, TransformGroup.ChildrenProperty, ScaleTransform.ScaleXProperty));
			storyboard.Children.Add(animation);

			animation = new DoubleAnimation {
				To = targetScaleFactor
			};
			Storyboard.SetTargetProperty(animation, new PropertyPath("(0).(1)[0].(2)", UIElement.RenderTransformProperty, TransformGroup.ChildrenProperty, ScaleTransform.ScaleYProperty));
			storyboard.Children.Add(animation);

			var translate = new TranslateTransform();
			group.Children.Add(translate);

			animation = new DoubleAnimation {
				To = 20 - 40 * random.NextDouble()
			};
			Storyboard.SetTargetProperty(animation, new PropertyPath("(0).(1)[1].(2)", UIElement.RenderTransformProperty, TransformGroup.ChildrenProperty, TranslateTransform.XProperty));
			storyboard.Children.Add(animation);

			animation = new DoubleAnimation {
				To = 20 - 40 * random.NextDouble()
			};
			Storyboard.SetTargetProperty(animation, new PropertyPath("(0).(1)[1].(2)", UIElement.RenderTransformProperty, TransformGroup.ChildrenProperty, TranslateTransform.YProperty));
			storyboard.Children.Add(animation);

			animation = new DoubleAnimation {
				To = 0.0
			};
			Storyboard.SetTargetProperty(animation, new PropertyPath(UIElement.OpacityProperty));
			storyboard.Children.Add(animation);

			storyboard.Begin(smokeCloud);
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void OnClosed() {
		// Detach from events
		View.HasFocusChanged -= OnViewHasFocusChanged;
		View.SyntaxEditor.DocumentTextChanged -= OnSyntaxEditorDocumentTextChanged;

		// Remove any remaining adornments
		AdornmentLayer.RemoveAllAdornments(AdornmentChangeReason.ManagerClosed);

		base.OnClosed();
	}

}
