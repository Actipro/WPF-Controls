using ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.Common;
using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.Controls.Views;
using ActiproSoftware.Windows.Data;

namespace ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.AnimatedCanvasIntro;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	private readonly Random _random = new(Environment.TickCount);

	private Point? _dragPoint = null;
	private double _dragLeft = 0;
	private double _dragTop = 0;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();
		Loaded += OnLoaded;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates a new <see cref="ProductListBoxItem"/> instance.
	/// </summary>
	private ProductListBoxItem CreateItem() {
		var item = new ProductListBoxItem();

		// Randomly place the item
		var left = _random.NextDouble() * (listBox.ActualWidth - item.MinWidth);
		var top = _random.NextDouble() * (listBox.ActualHeight - item.MinHeight);
		AnimatedCanvas.SetLeft(item, left);
		AnimatedCanvas.SetTop(item, top);

		// Hook up dragging
		item.AddHandler(MouseLeftButtonDownEvent, (MouseButtonEventHandler)OnProductListBoxItemMouseLeftButtonDown, handledEventsToo: true);
		item.MouseMove += OnProductListBoxItemMouseMove;
		item.MouseLeftButtonUp += OnProductListBoxItemMouseLeftButtonUp;

		return item;
	}

	private void OnAddItemClick(object? sender, RoutedEventArgs? e)
		=> listBox.Items.Add(CreateItem());

	private void OnArrangeItemsClick(object sender, RoutedEventArgs e) {
		var rowHeight = 0.0;
		var x = 0.0;
		var y = 0.0;

		foreach (var item in listBox.Items.OfType<ProductListBoxItem>()) {
			// Perform wrap-panel type layout
			if (x + item.MinWidth > listBox.ActualWidth) {
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

	private void OnClearAllClick(object sender, RoutedEventArgs e)
		=> listBox.Items.Clear();

	private void OnInsertItemClick(object sender, RoutedEventArgs e) {
		var index = MathHelper.Range(listBox.SelectedIndex + 1, 0, listBox.Items.Count);
		listBox.Items.Insert(index, CreateItem());
	}

	private void OnLoaded(object sender, RoutedEventArgs e) {
		var data = FindResource("ProductData") as ProductData;
		if (data is not null) {
			for (var i = 0; i < data.ProductFamilies.Count; i++)
				OnAddItemClick(sender: null, e: null);
			listBox.SelectedIndex = 0;
		}
	}

	private void OnProductListBoxItemMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
		// Don't start dragging if a button has the capture, since it's probably being pressed
		if (Mouse.Captured is not ButtonBase) {
			var item = (ProductListBoxItem)sender;
			if (item.CaptureMouse()) {
				_dragPoint = e.GetPosition(item.Parent as Panel);
				_dragLeft = AnimatedCanvas.GetLeft(item);
				_dragTop = AnimatedCanvas.GetTop(item);
				e.Handled = true;
			}
		}
	}

	private void OnProductListBoxItemMouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
		var item = (ProductListBoxItem)sender;
		if (item.IsMouseCaptured)
			item.ReleaseMouseCapture();

		if (_dragPoint is not null) {
			_dragPoint = null;
			_dragLeft = 0;
			_dragTop = 0;
			e.Handled = true;
		}
	}

	private void OnProductListBoxItemMouseMove(object sender, MouseEventArgs e) {
		var item = (ProductListBoxItem)sender;
		if ((_dragPoint is not null) && (item.IsMouseCaptured)) {
			// Get the current point and the difference with the drag point
			var currentPoint = e.GetPosition(item.Parent as Panel);
			var diffX = currentPoint.X - _dragPoint.Value.X;
			var diffY = currentPoint.Y - _dragPoint.Value.Y;

			// Ensure the mouse has moved a minimum distance
			if ((Math.Abs(diffX) >= 3) || (Math.Abs(diffY) >= 3)) {
				AnimatedCanvas.SetLeft(item, _dragLeft + diffX);
				AnimatedCanvas.SetTop(item, _dragTop + diffY);
				e.Handled = true;
			}
		}
	}

	private void OnScatterItemsClick(object sender, RoutedEventArgs e) {
		foreach (var item in listBox.Items.OfType<ProductListBoxItem>()) {
			// Randomly place the item
			var left = _random.NextDouble() * (listBox.ActualWidth - item.MinWidth);
			var top = _random.NextDouble() * (listBox.ActualHeight - item.MinHeight);
			AnimatedCanvas.SetLeft(item, left);
			AnimatedCanvas.SetTop(item, top);
		}
	}

}
