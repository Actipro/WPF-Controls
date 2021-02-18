using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Utility;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsCustomDecorator {

    /// <summary>
	/// Represents an adornment manager for a view that makes a custom decorator under text.
    /// </summary>
    public class CustomAdornmentManager : DecorationAdornmentManagerBase<IEditorView, CustomTag> {

		private static AdornmentLayerDefinition layerDefinition = 
			new AdornmentLayerDefinition("Custom", new Ordering(AdornmentLayerDefinitions.TextForeground.Key, OrderPlacement.After));

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>CustomAdornmentManager</c> class.
		/// </summary>
		/// <param name="view">The view to which this manager is attached.</param>
		public CustomAdornmentManager(IEditorView view) : base(view, layerDefinition) {}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Returns a <see cref="UIElement"/> for a custom decorator.
		/// </summary>
		/// <param name="width">The width of the line.</param>
		/// <returns>The <see cref="UIElement"/> that was created.</returns>
		private static UIElement CreateDecorator(double width) {
			// Create a rectangle
			Rectangle rect = new Rectangle();
			rect.Width = width;
			rect.Height = 2;

			// Add a brush
			LinearGradientBrush brush = new LinearGradientBrush();
			brush.GradientStops.Add(new GradientStop(Colors.Transparent, -0.5));
			brush.GradientStops.Add(new GradientStop(Colors.Red, 0.0));
			brush.GradientStops.Add(new GradientStop(Colors.Transparent, 0.01));
			rect.Fill = brush;

			Storyboard sb = new Storyboard();
			sb.Duration = new Duration(TimeSpan.FromSeconds(4));
			sb.RepeatBehavior = RepeatBehavior.Forever;
			DoubleAnimation anim;
			
			anim = new DoubleAnimation(0.01, 1.51, new Duration(TimeSpan.FromSeconds(2)));
			Storyboard.SetTargetProperty(anim, new PropertyPath("(0).(1)[2].(2)", Rectangle.FillProperty, LinearGradientBrush.GradientStopsProperty, GradientStop.OffsetProperty));
			sb.Children.Add(anim);
			
			anim = new DoubleAnimation(0.99, -0.51, new Duration(TimeSpan.FromSeconds(2)));
			anim.BeginTime = TimeSpan.FromSeconds(2);
			Storyboard.SetTargetProperty(anim, new PropertyPath("(0).(1)[2].(2)", Rectangle.FillProperty, LinearGradientBrush.GradientStopsProperty, GradientStop.OffsetProperty));
			sb.Children.Add(anim);
			
			anim = new DoubleAnimation(0.0, 1.5, new Duration(TimeSpan.FromSeconds(2)));
			Storyboard.SetTargetProperty(anim, new PropertyPath("(0).(1)[1].(2)", Rectangle.FillProperty, LinearGradientBrush.GradientStopsProperty, GradientStop.OffsetProperty));
			sb.Children.Add(anim);
			
			anim = new DoubleAnimation(1.0, -0.5, new Duration(TimeSpan.FromSeconds(2)));
			anim.BeginTime = TimeSpan.FromSeconds(2);
			Storyboard.SetTargetProperty(anim, new PropertyPath("(0).(1)[1].(2)", Rectangle.FillProperty, LinearGradientBrush.GradientStopsProperty, GradientStop.OffsetProperty));
			sb.Children.Add(anim);
			
			anim = new DoubleAnimation(-0.5, 1.0, new Duration(TimeSpan.FromSeconds(2)));
			Storyboard.SetTargetProperty(anim, new PropertyPath("(0).(1)[0].(2)", Rectangle.FillProperty, LinearGradientBrush.GradientStopsProperty, GradientStop.OffsetProperty));
			sb.Children.Add(anim);
				
			anim = new DoubleAnimation(1.5, 0.0, new Duration(TimeSpan.FromSeconds(2)));
			anim.BeginTime = TimeSpan.FromSeconds(2);
			Storyboard.SetTargetProperty(anim, new PropertyPath("(0).(1)[0].(2)", Rectangle.FillProperty, LinearGradientBrush.GradientStopsProperty, GradientStop.OffsetProperty));
			sb.Children.Add(anim);
			
			sb.Begin(rect);

			return rect;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Adds an adornment to the <see cref="AdornmentLayer"/>.
		/// </summary>
		/// <param name="reason">An <see cref="AdornmentChangeReason"/> indicating the add reason.</param>
		/// <param name="viewLine">The current <see cref="ITextViewLine"/> being examined.</param>
		/// <param name="tagRange">The <see cref="ITag"/> and the range it covers.</param>
		/// <param name="bounds">The text bounds in which to render an adornment.</param>
		protected override void AddAdornment(AdornmentChangeReason reason, ITextViewLine viewLine, TagSnapshotRange<CustomTag> tagRange, TextBounds bounds) {
			// Create the adornment
			UIElement element = CustomAdornmentManager.CreateDecorator(bounds.Width);
			Point location = new Point(Math.Round(bounds.Left), bounds.Bottom - 2);

			// Add the adornment to the layer
			this.AdornmentLayer.AddAdornment(reason, element, location, null, viewLine, tagRange.SnapshotRange, TextRangeTrackingModes.ExpandBothEdges, null);
		}

		/// <summary>
		/// Occurs when the manager is closed and detached from the view.
		/// </summary>
		/// <remarks>
		/// Overrides should release any event handlers set up in the manager's constructor.
		/// </remarks>
		protected override void OnClosed() {
			// Remove any remaining adornments
			this.AdornmentLayer.RemoveAllAdornments(AdornmentChangeReason.ManagerClosed);
		
			// Call the base method
			base.OnClosed();
		}

    }
	
}
