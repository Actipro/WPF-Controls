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

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsWatermark {
    
    /// <summary>
	/// Represents an adornment manager for a view that makes a watermark effect on the text area.
    /// </summary>
    public class WatermarkAdornmentManager : AdornmentManagerBase<IEditorView> {

		private static AdornmentLayerDefinition layerDefinition = new AdornmentLayerDefinition("Watermark", new Ordering(AdornmentLayerDefinitions.Selection.Key, OrderPlacement.After));
		private IAdornment watermarkAdornment;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>WatermarkAdornmentManager</c> class.
		/// </summary>
		/// <param name="view">The view to which this manager is attached.</param>
		public WatermarkAdornmentManager(IEditorView view) : base(view, layerDefinition) {
			// Only let this manager be active when the view has focus
			this.IsActive = view.HasFocus;

			// Create the watermark adornment
			this.watermarkAdornment = this.CreateWatermarkAdornment();

			// Attach to events
			view.TextAreaLayout += OnViewTextAreaLayout;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
						
		/// <summary>
		/// Called when the <c>ViewTextAreaLayout</c> event occurs.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="ActiproSoftware.Windows.Controls.SyntaxEditor.TextViewTextAreaLayoutEventArgs"/> 
		/// instance containing the event data.</param>
		private void OnViewTextAreaLayout(object sender, TextViewTextAreaLayoutEventArgs e) {
			// Get the horizontal scroll
			IEditorView view = e.View as IEditorView;
			double firstVisibleX = (view != null ? view.ScrollState.HorizontalAmount : 0.0);

			// Determine the center of the text area viewport
			Rect textAreaViewportBounds = e.View.TextAreaViewportBounds;
			Point center = new Point(firstVisibleX + textAreaViewportBounds.Width / 2 / e.View.SyntaxEditor.ZoomLevelAnimated, textAreaViewportBounds.Height / 2 / e.View.SyntaxEditor.ZoomLevelAnimated);
			
			// Determine the center of the watermark element
			UIElement element = this.watermarkAdornment.VisualElement;
			TextBlock textBlock = element as TextBlock;
			if (textBlock == null)
				return;
			Point textBlockCenter = new Point(textBlock.ActualWidth / 2, textBlock.ActualHeight / 2);

			// Adjust scale
			var scaleTrans = (ScaleTransform)textBlock.RenderTransform;
			scaleTrans.ScaleX = 1 / e.View.SyntaxEditor.ZoomLevelAnimated;
			scaleTrans.ScaleY = 1 / e.View.SyntaxEditor.ZoomLevelAnimated;
			
			// Determine the watermark location
			Point watermarkLocation = new Point(center.X - textBlockCenter.X, center.Y - textBlockCenter.Y);

			// Set the watermark location
			watermarkAdornment.Location = watermarkLocation;
		}

		/// <summary>
		/// Creates the watermark adornment.
		/// </summary>
		/// <returns>The watermark adornment.</returns>
		private IAdornment CreateWatermarkAdornment() {
			// Create a UIElement for the watermark
			TextBlock textBlock = new TextBlock() {
				Text = "Watermark",
				FontSize = 72,
				IsHitTestVisible = false,
				Opacity = 0.1,
				RenderTransform = new ScaleTransform(),
				RenderTransformOrigin = new Point(0.5, 0.5)
			};

			// Add the UIElement to the adornment layer as an adornment
			return this.AdornmentLayer.AddAdornment(AdornmentChangeReason.Other, textBlock, new Point(), null, null);
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
			this.View.TextAreaLayout -= OnViewTextAreaLayout;

			// Remove any remaining adornments
			this.AdornmentLayer.RemoveAllAdornments(AdornmentChangeReason.ManagerClosed);
		
			// Call the base method
			base.OnClosed();
		}
		
    }
	
}
