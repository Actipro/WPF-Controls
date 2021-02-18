using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Extensions;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Provides an implementation of a <see cref="Panel"/> that measures a drop-down glyph (the last element) before filling the first element.
	/// </summary>
	public class DropDownGlyphPanel : Panel {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// When overridden in a derived class, positions child elements and determines a size for a <see cref="FrameworkElement"/> derived class. 
		/// </summary>
		/// <param name="finalSize">
		/// The final area within the parent that this element should use to arrange itself and its children.
		/// </param>
		/// <returns>The actual size used.</returns>
		protected override Size ArrangeOverride(Size finalSize) {
			var internalChildren = base.InternalChildren;
			if (internalChildren.Count == 2) {
				var x = Math.Max(0, Math.Min(internalChildren[0].DesiredSize.Width, finalSize.Width - internalChildren[1].DesiredSize.Width));
				internalChildren[1].Arrange(new Rect(x, 0, internalChildren[1].DesiredSize.Width, finalSize.Height));
				internalChildren[0].Arrange(new Rect(0, 0, x, finalSize.Height));
			}

			return finalSize;
		}

		/// <summary>
		/// Measures all children given an available size and returns the element's desired size based on the size of its children.
		/// </summary>
		/// <param name="availableSize">The size the element is suggested to fit inside.</param>
		/// <returns>The desired size of the element.</returns>
		protected override Size MeasureOverride(Size availableSize) {
			var desiredWidth = 0.0;
			var desiredHeight = 0.0;

			var availableWidth = availableSize.Width;
			var availableHeight = availableSize.Height;

			var internalChildren = base.InternalChildren;
			if (internalChildren.Count == 2) {
				// Glyph
				internalChildren[1].Measure(new Size(availableWidth, availableHeight));
				availableWidth = Math.Max(0, availableWidth - internalChildren[1].DesiredSize.Width);

				// Text
				internalChildren[0].Measure(new Size(availableWidth, availableHeight));
				
				// Calculate size
				desiredWidth = Math.Ceiling(internalChildren[0].DesiredSize.Width + internalChildren[1].DesiredSize.Width);
				desiredHeight = Math.Max(internalChildren[0].DesiredSize.Height, internalChildren[1].DesiredSize.Height).Round(RoundMode.CeilingToEven);
			}

			return new Size(desiredWidth, desiredHeight);
		}

	}

}
