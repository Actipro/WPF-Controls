using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using ActiproSoftware.Windows.Controls.Views;
using ActiproSoftware.Windows.Data;
using ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.Common;
using ActiproSoftware.SampleBrowser;

namespace ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.AnimatedCanvasIntro {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private Random random = new Random(Environment.TickCount);

		private Point? dragPoint = null;
		private double dragLeft = 0;
		private double dragTop = 0;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();
			this.Loaded += new RoutedEventHandler(this.OnLoaded);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a new <see cref="ProductListBoxItem"/> instance.
		/// </summary>
		/// <returns>A new <see cref="ProductListBoxItem"/> instance.</returns>
		private ProductListBoxItem CreateItem() {
			ProductListBoxItem item = new ProductListBoxItem();

			// Randomly place the item
			double left = random.NextDouble() * (this.listBox.ActualWidth - item.MinWidth);
			double top = random.NextDouble() * (this.listBox.ActualHeight - item.MinHeight);
			AnimatedCanvas.SetLeft(item, left);
			AnimatedCanvas.SetTop(item, top);

			// Hook up dragging
			item.AddHandler(ProductListBoxItem.MouseLeftButtonDownEvent, (MouseButtonEventHandler)this.OnProductListBoxItemMouseLeftButtonDown, true);
			item.MouseMove += this.OnProductListBoxItemMouseMove;
			item.MouseLeftButtonUp += this.OnProductListBoxItemMouseLeftButtonUp;

			return item;
		}

		/// <summary>
		/// Handles the <c>Click</c> event of the add button.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnAddItemClick(object sender, RoutedEventArgs e) {
			this.listBox.Items.Add(CreateItem());
		}

		/// <summary>
		/// Handles the <c>Click</c> event of the arrange items button.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnArrangeItemsClick(object sender, RoutedEventArgs e) {
			double rowHeight = 0;
			double x = 0;
			double y = 0;

			foreach (ProductListBoxItem item in this.listBox.Items) {
				// Perform wrap-panel type layout
				if (x + item.MinWidth > this.listBox.ActualWidth) {
					// Start a new row
					x = 0;
					y += rowHeight;
					rowHeight = 0;
				}

				AnimatedCanvas.SetLeft(item, x);
				AnimatedCanvas.SetTop(item, y);

				x += item.MinWidth;
				rowHeight = Math.Max(rowHeight, item.MinHeight);
			}
		}

		/// <summary>
		/// Handles the <c>Click</c> event of the clear button.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnClearAllClick(object sender, RoutedEventArgs e) {
			this.listBox.Items.Clear();
		}

		/// <summary>
		/// Handles the <c>Click</c> event of the insert button.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnInsertItemClick(object sender, RoutedEventArgs e) {
			int index = MathHelper.Range(this.listBox.SelectedIndex + 1, 0, this.listBox.Items.Count);
			this.listBox.Items.Insert(index, CreateItem());
		}

		/// <summary>
		/// Handles the <c>Loaded</c> event of the MainControl control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnLoaded(object sender, RoutedEventArgs e) {
			var data = this.FindResource("ProductData") as ProductData;
			if (data != null) {
				for (int i = 0; i < data.ProductFamilies.Count; i++)
					this.OnAddItemClick(null, null);
				this.listBox.SelectedIndex = 0;
			}
		}

		/// <summary>
		/// Handles the <c>MouseLeftButtonDown</c> event of a <see cref="ProductListBoxItem"/>control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
		private void OnProductListBoxItemMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
			// Don't start dragging if a button has the capture, since it's probably being pressed
			if (!(Mouse.Captured is ButtonBase)) {
				ProductListBoxItem item = (ProductListBoxItem)sender;
				if (item.CaptureMouse()) {
					this.dragPoint = e.GetPosition(item.Parent as Panel);
					this.dragLeft = AnimatedCanvas.GetLeft(item);
					this.dragTop = AnimatedCanvas.GetTop(item);
					e.Handled = true;
				}
			}
		}

		/// <summary>
		/// Handles the <c>MouseLeftButtonUp</c> event of a <see cref="ProductListBoxItem"/>control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
		private void OnProductListBoxItemMouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			ProductListBoxItem item = (ProductListBoxItem)sender;
			if (item.IsMouseCaptured)
				item.ReleaseMouseCapture();

			if (this.dragPoint != null) {
				this.dragPoint = null;
				this.dragLeft = 0;
				this.dragTop = 0;
				e.Handled = true;
			}
		}

		/// <summary>
		/// Handles the <c>MouseMove</c> event of a <see cref="ProductListBoxItem"/>control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
		private void OnProductListBoxItemMouseMove(object sender, MouseEventArgs e) {
			ProductListBoxItem item = (ProductListBoxItem)sender;
			if (this.dragPoint != null && item.IsMouseCaptured) {
				// Get the current point and the difference with the drag point
				Point currentPoint = e.GetPosition(item.Parent as Panel);
				double diffX = currentPoint.X - this.dragPoint.Value.X;
				double diffY = currentPoint.Y - this.dragPoint.Value.Y;

				// Ensure the mouse has moved a minimum distance
				if (Math.Abs(diffX) >= 3 || Math.Abs(diffY) >= 3) {
					AnimatedCanvas.SetLeft(item, this.dragLeft + diffX);
					AnimatedCanvas.SetTop(item, this.dragTop + diffY);
					e.Handled = true;
				}
			}
		}

		/// <summary>
		/// Handles the <c>Click</c> event of the scatter items button.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnScatterItemsClick(object sender, RoutedEventArgs e) {
			foreach (ProductListBoxItem item in this.listBox.Items) {
				// Randomly place the item
				double left = random.NextDouble() * (this.listBox.ActualWidth - item.MinWidth);
				double top = random.NextDouble() * (this.listBox.ActualHeight - item.MinHeight);
				AnimatedCanvas.SetLeft(item, left);
				AnimatedCanvas.SetTop(item, top);
			}
		}
	}
}