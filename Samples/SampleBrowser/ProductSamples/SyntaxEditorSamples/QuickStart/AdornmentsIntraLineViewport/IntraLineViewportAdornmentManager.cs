using System;
using System.Collections.Generic;
using System.Windows;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Utility;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsIntraLineViewport {

    /// <summary>
	/// Represents an adornment manager for a view that renders intra-text notes.
    /// </summary>
    public class IntraLineViewportAdornmentManager : IntraLineAdornmentManagerBase<IEditorView, IntraLineViewportTag> {

		private static AdornmentLayerDefinition layerDefinition = 
			new AdornmentLayerDefinition("IntraLineViewport", new Ordering(AdornmentLayerDefinitions.Caret.Key, OrderPlacement.Before));

		private List<Tuple<WeakReference, AdornmentElement>> cachedElements = new List<Tuple<WeakReference, AdornmentElement>>();

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>IntraLineViewportAdornmentManager</c> class.
		/// </summary>
		/// <param name="view">The view to which this manager is attached.</param>
		public IntraLineViewportAdornmentManager(IEditorView view) : base(view, layerDefinition) {
			// Attach to events
			view.TextAreaLayout += OnViewTextAreaLayout;
			view.VisualElement.SizeChanged += OnViewSizeChanged;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Examines the cached elements, prunes ones that no longer have valid tags, and returns a match if found.
		/// </summary>
		/// <param name="tag">The target tag for which to search.</param>
		/// <returns>A cached element, if found.</returns>
		private AdornmentElement GetCachedElement(object tag) {
			AdornmentElement targetElement = null;

			if (tag != null) {
				for (var index = cachedElements.Count - 1; index >= 0; index--) {
					var tagRef = cachedElements[index].Item1;
					if (tagRef.IsAlive) {
						// If there is a tag match, use the cached element and remove the entry
						if (tagRef.Target == tag) {
							targetElement = cachedElements[index].Item2;
							cachedElements.RemoveAt(index);
						}
					}
					else {
						// Remove the entry
						cachedElements.RemoveAt(index);
					}
				}
			}

			return targetElement;
		}

		/// <summary>
		/// Returns the bounds for the specified <see cref="IntraLineViewportTag"/> to keep it fully visible and stretched across the text area width.
		/// </summary>
		/// <param name="tag">The <see cref="IntraLineViewportTag"/> to examine.</param>
		/// <returns>The bounds of the specified <see cref="IntraLineViewportTag"/>.</returns>
		private Rect GetAdornmentBounds(IntraLineViewportTag tag) {
			var viewportBounds = this.View.TransformToTextArea(this.View.TextAreaViewportBounds);
			var y = 0.0;
			return new Rect(this.View.ScrollState.HorizontalAmount, y, viewportBounds.Width, tag.BottomMargin);
		}

		/// <summary>
		/// Occurs when an adornment is removed.
		/// </summary>
		/// <param name="adornment">The <see cref="IAdornment"/> that is removed.</param>
		private void OnAdornmentRemoved(IAdornment adornment) {
			if (adornment != null) {
				var element = adornment.VisualElement as AdornmentElement;
				if (element != null) {
					cachedElements.Add(Tuple.Create(new WeakReference(adornment.Tag), element));
					element.Tag = null;
				}
			}
		}
		
		/// <summary>
		/// Occurs when the size of the element changes.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="SizeChangedEventArgs"/> that contains the event data.</param>
		private void OnViewSizeChanged(object sender, SizeChangedEventArgs e) {
			this.UpdateAdornmentBounds();
		}

		/// <summary>
		/// Occurs when a view text area layout occurs.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="TextViewTextAreaLayoutEventArgs"/> that contains the event data.</param>
		private void OnViewTextAreaLayout(object sender, TextViewTextAreaLayoutEventArgs e) {
			this.UpdateAdornmentBounds();
		}

		/// <summary>
		/// Adjusts each adornment's X-coordinate and width so it fills the text area.
		/// </summary>
		private void UpdateAdornmentBounds() {
			if (this.AdornmentLayer.Adornments.Count > 0) {
				var viewportBounds = this.View.TransformToTextArea(this.View.TextAreaViewportBounds);

				foreach (var adornment in this.AdornmentLayer.Adornments) {
					if (adornment != null) {
						adornment.Location = new Point(this.View.ScrollState.HorizontalAmount, adornment.Location.Y);

						var visualElement = adornment.VisualElement as FrameworkElement;
						if (visualElement != null)
							visualElement.Width = viewportBounds.Width;
					}
				}
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Adds an adornment to the <see cref="AdornmentLayer"/>.
		/// </summary>
		/// <param name="viewLine">The current <see cref="ITextViewLine"/> being examined.</param>
		/// <param name="tagRange">The <see cref="ITag"/> and the range it covers.</param>
		protected override void AddAdornment(ITextViewLine viewLine, TagSnapshotRange<IntraLineViewportTag> tagRange) {
			// Determine the bounds
			var bounds = this.GetAdornmentBounds(tagRange.Tag);
			var charBounds = viewLine.GetCharacterBounds(tagRange.SnapshotRange.StartOffset);
			bounds.Y = charBounds.Bottom;

			// See if a cached version of the element for the tag is available
			var element = this.GetCachedElement(tagRange.Tag);
			if (element == null) {
				// Create the element
				element = new AdornmentElement();
			}

			// Update the size
			element.Width = bounds.Width;
			element.Height = bounds.Height;
			element.Tag = tagRange.Tag;
			
			// Add the adornment
			this.AdornmentLayer.AddAdornment(AdornmentChangeReason.Other, element, bounds.Location, tagRange.Tag.Key, OnAdornmentRemoved);
		}

		/// <summary>
		/// Occurs when the manager is closed and detached from the view.
		/// </summary>
		/// <remarks>
		/// Overrides should release any event handlers set up in the manager's constructor.
		/// </remarks>
		protected override void OnClosed() {
			// Detach from events
			this.View.TextAreaLayout -= OnViewTextAreaLayout;
			this.View.VisualElement.SizeChanged -= OnViewSizeChanged;

			// Remove any remaining adornments
			this.AdornmentLayer.RemoveAllAdornments(AdornmentChangeReason.ManagerClosed);

			// Call the base method
			base.OnClosed();
		}

    }
	
}
