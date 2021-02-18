using ActiproSoftware.Text.Utility;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Implementation;
using System;
using System.Windows;
using System.Windows.Media;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsAlternatingRows {

	/// <summary>
	/// Represents an adornment manager for a view that displays a background over alternating rows.
	/// </summary>
	public class AlternatingRowsAdornmentManager : AdornmentManagerBase<IEditorView> {

		private static AdornmentLayerDefinition layerDefinition = 
			new AdornmentLayerDefinition("AlternatingRows", new Ordering(AdornmentLayerDefinitions.TextBackground.Key, OrderPlacement.After));

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>AlternatingRowsAdornmentManager</c> class.
		/// </summary>
		/// <param name="view">The view to which this manager is attached.</param>
		public AlternatingRowsAdornmentManager(IEditorView view) : base(view, layerDefinition, false) {
			// Attach to events
			view.TextAreaLayout += new EventHandler<TextViewTextAreaLayoutEventArgs>(OnViewTextAreaLayout);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Adds an adornment to the <see cref="AdornmentLayer"/>.
		/// </summary>
		/// <param name="viewLine">The current <see cref="ITextViewLine"/> being examined.</param>
		private void AddAdornment(ITextViewLine viewLine) {
			// Get the adornment bounds
			var bounds = viewLine.Bounds;
			bounds.Width = 1000000;

			// Add the adornment to the layer
			this.AdornmentLayer.AddAdornment(AdornmentChangeReason.Other, OnDrawAdornment, bounds, viewLine, null);
		}

		/// <summary>
		/// Returns the adornments with the specified tag.
		/// </summary>
		/// <param name="tag">The tag for which to search.</param>
		/// <returns>The adornments with the specified tag.</returns>
		private IAdornment[] GetAdornmentsWithTag(object tag) {
			return this.AdornmentLayer.FindAdornments(tag);
		}
		
		/// <summary>
		/// Occurs when the adornment needs to be drawn.
		/// </summary>
		/// <param name="context">The <see cref="TextViewDrawContext"/> to use for rendering.</param>
		/// <param name="adornment">The <see cref="IAdornment"/> to draw.</param>
		private void OnDrawAdornment(TextViewDrawContext context, IAdornment adornment) {
			var color = Color.FromArgb(0x20, 0x80, 0x80, 0x80);
			context.FillRectangle(new Rect(adornment.Location, adornment.Size), color);
		}

		/// <summary>
		/// Occurs when a view text area layout occurs.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="TextViewTextAreaLayoutEventArgs"/> that contains the event data.</param>
		private void OnViewTextAreaLayout(object sender, TextViewTextAreaLayoutEventArgs e) {
			// Loop through the added/updated lines
			foreach (ITextViewLine viewLine in e.AddedOrUpdatedViewLines) {
				// If an even document line (odd-indexed), add an adornment
				if (viewLine.StartPosition.Line % 2 == 1)
					this.AddAdornment(viewLine);
			}
			
			// Loop through the translated lines
			foreach (ITextViewLine viewLine in e.TranslatedViewLines) {
				// If an even document line (odd-indexed)...
				if (viewLine.StartPosition.Line % 2 == 1) {
					// Get any existing adornments
					IAdornment[] adornments = this.GetAdornmentsWithTag(viewLine);
					if (adornments.Length > 0) {
						// Translate existing adornments
						foreach (IAdornment adornment in adornments)
							adornment.Translate(0, viewLine.TranslationY);
					}
					else {
						// There are no existing adornments but this line needs one now
						this.AddAdornment(viewLine);
					}
				}
				else {
					// Odd line (even-indexed) so remove any existing adornments
					this.AdornmentLayer.RemoveAdornments(AdornmentChangeReason.Other, this.GetAdornmentsWithTag(viewLine));
				}
			}

			// Loop through the removed lines
			foreach (ITextViewLine viewLine in e.RemovedViewLines)
				this.AdornmentLayer.RemoveAdornments(AdornmentChangeReason.ViewLineRemoved, this.GetAdornmentsWithTag(viewLine));
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
			this.View.TextAreaLayout -= new EventHandler<TextViewTextAreaLayoutEventArgs>(OnViewTextAreaLayout);

			// Remove any remaining adornments
			this.AdornmentLayer.RemoveAllAdornments(AdornmentChangeReason.ManagerClosed);
		
			// Call the base method
			base.OnClosed();
		}
		
    }
	
}
