using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using ActiproSoftware.Text.Utility;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsSmokeText {
    
    /// <summary>
	/// Represents an adornment manager for a view that makes a smoke text effect when text is changed.
    /// </summary>
    public class SmokeTextAdornmentManager : AdornmentManagerBase<IEditorView> {

		private static AdornmentLayerDefinition layerDefinition = new AdornmentLayerDefinition("SmokeText", new Ordering(AdornmentLayerDefinitions.Selection.Key, OrderPlacement.After));

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>SmokeTextAdornmentManager</c> class.
		/// </summary>
		/// <param name="view">The view to which this manager is attached.</param>
		public SmokeTextAdornmentManager(IEditorView view) : base(view, layerDefinition) {
			// Only let this manager be active when the view has focus
			this.IsActive = view.HasFocus;

			// Attach to events
			view.HasFocusChanged += OnViewHasFocusChanged;
			view.SyntaxEditor.DocumentTextChanged += OnSyntaxEditorDocumentTextChanged;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the storyboard completes.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
		private void OnStoryboardCompleted(object sender, EventArgs e) {
			// This event assumes a ClockGroup is passed with the Storyboard as its Timeline
			ClockGroup cg = sender as ClockGroup;
			if (cg != null) {
				Storyboard sb = cg.Timeline as Storyboard;
				if (sb != null) {
					// Clean up and remove any adornments that are tagged with the storyboard's name
					this.AdornmentLayer.RemoveAdornments(AdornmentChangeReason.Other, this.AdornmentLayer.FindAdornments(sb.Name));
					return;
				}
			}
		}
		
		/// <summary>
		/// Occurs after the document text is changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="EditorSnapshotChangedEventArgs"/> that contains the event data.</param>
		private void OnSyntaxEditorDocumentTextChanged(object sender, EditorSnapshotChangedEventArgs e) {
			// Don't add effects if the view doesn't have focus
			if (!this.IsActive)
				return;

			// Get the caret bounds in the view
			TextBounds caretBounds = this.View.GetCharacterBounds(this.View.Selection.EndPosition);

			// Render the smoke using adornments
			this.PuffSmoke(new Point(caretBounds.Left, caretBounds.Top + (caretBounds.Height / 2)));
		}
		
		/// <summary>
		/// Occurs when the view gains or loses focus.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnViewHasFocusChanged(object sender, EventArgs e) {
			// Only let this manager be active when the view has focus
			this.IsActive = this.View.HasFocus;
		}
		
		/// <summary>
		/// Renders an smoke text effect.
		/// </summary>
		/// <param name="location">The location of the effect.</param>
		private void PuffSmoke(Point location) {
			Random rand = new Random();

			List<Ellipse> smokeClouds = new List<Ellipse>();
			int smokeCloudCount = 4 + rand.Next(2);

			for (int index = 0; index < smokeCloudCount; index++) {
				Ellipse smokeCloud = new Ellipse();
				smokeCloud.Fill = Brushes.Silver;
				smokeCloud.Stroke = Brushes.Gray;
				smokeCloud.StrokeThickness = 1.0;
				smokeCloud.Opacity = 0.3;

				smokeCloud.Width = 10 + rand.Next(10);
				smokeCloud.Height = 10 + rand.Next(10);

				Point smokeCloudLocation = new Point(location.X - (smokeCloud.Width / 2), location.Y - (smokeCloud.Height / 2));

				TransformGroup group = new TransformGroup();
				smokeCloud.RenderTransform = group;

				Storyboard sb = new Storyboard();
				sb.Duration = new Duration(TimeSpan.FromSeconds(2.7));
				sb.Completed += new EventHandler(OnStoryboardCompleted);
				sb.Name = String.Format("SC{0}", Guid.NewGuid().ToString().Replace('-', '_'));
				DoubleAnimation anim;

				this.AdornmentLayer.AddAdornment(AdornmentChangeReason.Other, smokeCloud, smokeCloudLocation, sb.Name, null);

				ScaleTransform scale = new ScaleTransform();
				scale.CenterX = smokeCloud.Width / 2;
				scale.CenterY = smokeCloud.Height / 2;
				group.Children.Add(scale);
				double targetScaleFactor = 2 + rand.NextDouble();

				anim = new DoubleAnimation();
				anim.To = targetScaleFactor;
				Storyboard.SetTargetProperty(anim, new PropertyPath("(0).(1)[0].(2)", UIElement.RenderTransformProperty, TransformGroup.ChildrenProperty, ScaleTransform.ScaleXProperty));
				sb.Children.Add(anim);
				
				anim = new DoubleAnimation();
				anim.To = targetScaleFactor;
				Storyboard.SetTargetProperty(anim, new PropertyPath("(0).(1)[0].(2)", UIElement.RenderTransformProperty, TransformGroup.ChildrenProperty, ScaleTransform.ScaleYProperty));
				sb.Children.Add(anim);
				
				TranslateTransform translate = new TranslateTransform();
				group.Children.Add(translate);
				
				anim = new DoubleAnimation();
				anim.To = 20 - 40 * rand.NextDouble();
				Storyboard.SetTargetProperty(anim, new PropertyPath("(0).(1)[1].(2)", UIElement.RenderTransformProperty, TransformGroup.ChildrenProperty, TranslateTransform.XProperty));
				sb.Children.Add(anim);
				
				anim = new DoubleAnimation();
				anim.To = 20 - 40 * rand.NextDouble();
				Storyboard.SetTargetProperty(anim, new PropertyPath("(0).(1)[1].(2)", UIElement.RenderTransformProperty, TransformGroup.ChildrenProperty, TranslateTransform.YProperty));
				sb.Children.Add(anim);
				
				anim = new DoubleAnimation();
				anim.To = 0.0;
				Storyboard.SetTargetProperty(anim, new PropertyPath(UIElement.OpacityProperty));
				sb.Children.Add(anim);

				sb.Begin(smokeCloud);
			}			
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the manager is closed and detached from the view.
		/// </summary>
		/// <remarks>
		/// Overrides should release any event handlers set up in the manager's constructor.
		/// </remarks>
		protected override void OnClosed() {
			// Detach from events
			this.View.HasFocusChanged -= OnViewHasFocusChanged;
			this.View.SyntaxEditor.DocumentTextChanged -= OnSyntaxEditorDocumentTextChanged;

			// Remove any remaining adornments
			this.AdornmentLayer.RemoveAllAdornments(AdornmentChangeReason.ManagerClosed);
		
			// Call the base method
			base.OnClosed();
		}
		
    }
	
}
