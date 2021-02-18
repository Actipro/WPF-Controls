using System;
using System.Collections.Generic;
using System.Windows;
using ActiproSoftware.Windows.Controls.Views;
using ActiproSoftware.Windows.Controls.Views.Primitives;

namespace ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.CustomPanelRandom {

	/// <summary>
	/// Represents a custom panel that randomly arranges it's child elements.
	/// </summary>
	public class RandomPanel : PanelBase {

		private Random random = new Random(Environment.TickCount);

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Positions the specified elements and determines a size for a <see cref="FrameworkElement"/>-derived class.
		/// </summary>
		/// <param name="elements">The elements to be arranged.</param>
		/// <param name="finalSize">
		/// The final area within the parent that this element should use to arrange itself and the specified elements.
		/// </param>
		/// <returns>The actual size used.</returns>
		public override Size ArrangeElements(IList<UIElement> elements, Size finalSize) {
			// Cache and reset layout pending flag
			bool isLayoutUpdatePending = this.IsLayoutUpdatePending;
			this.IsLayoutUpdatePending = false;

			// Iterate over the elements and arrange
			foreach (UIElement element in elements) {
				if (element != null) {
					Size desiredSize = element.DesiredSize;

					// Calculate a random x/y position that keeps the element in the view
					double x = Math.Max(random.NextDouble() * (finalSize.Width - desiredSize.Width), 0);
					double y = Math.Max(random.NextDouble() * (finalSize.Height - desiredSize.Height), 0);

					// Update the arrange state with the new arrange rect, but if there are leaving elements then don't move
					//   the element
					ArrangeState state = new ArrangeState(element, false, isLayoutUpdatePending);
					if (!this.HasLeavingChildren)
						state.ArrangeRect = new Rect(x, y, desiredSize.Width, desiredSize.Height);
					else
						state.ArrangeRect = state.PreviousArrangeRect;

					PanelBase.SetArrangeState(element, state);
				}
			}

			return finalSize;
		}

		/// <summary>
		/// Measures the size in layout required for the specified elements and determines a size for the <see cref="FrameworkElement"/>-derived class.
		/// </summary>
		/// <param name="elements">The elements to be measured.</param>
		/// <param name="availableSize">The available size that this element can give to the specified elements. Infinity can be specified as a value to indicate that
		/// the element will size to whatever content is available.</param>
		/// <returns>
		/// The size that this element determines it needs during layout, based on its calculations of the specified elements.
		/// </returns>
		public override Size MeasureElements(IList<UIElement> elements, Size availableSize) {
			// Measure each element, and return the largest width and height needed
			Size desiredSize = new Size();
			foreach (UIElement element in elements) {
				if (null != element) {
					element.Measure(availableSize);

					desiredSize.Width = Math.Max(element.DesiredSize.Width, desiredSize.Width);
					desiredSize.Height = Math.Max(element.DesiredSize.Height, desiredSize.Height);
				}
			}
			return desiredSize;
		}
	}
}
