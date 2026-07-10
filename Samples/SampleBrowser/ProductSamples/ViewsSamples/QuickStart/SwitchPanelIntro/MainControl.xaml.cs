using ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.Common;
using ActiproSoftware.Windows.Controls.Views;
using ActiproSoftware.Windows.Data;

namespace ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.SwitchPanelIntro;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	private readonly Random _random = new(Environment.TickCount);

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="ActiveIndex"/> property.
	/// </summary>
	public static readonly DependencyProperty ActiveIndexProperty
		= DependencyProperty.Register(nameof(ActiveIndex), typeof(int), typeof(MainControl), new FrameworkPropertyMetadata(defaultValue: 0, OnActiveIndexPropertyValueChanged));

	#endregion

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

	private void AddNewItem()
		=> listBox.Items.Add(CreateItem());

	/// <summary>
	/// Creates a new <see cref="ProductListBoxItem"/> instance.
	/// </summary>
	private ProductListBoxItem CreateItem() {
		var isDock = (ActiveIndex == 1);

		var item = new ProductListBoxItem() { IsDockable = isDock, IsMovable = !isDock };

		// Randomly place the item in Canvas
		var left = _random.NextDouble() * (listBox.ActualWidth - item.MinWidth);
		var top = _random.NextDouble() * (listBox.ActualHeight - item.MinHeight);
		AnimatedCanvas.SetLeft(item, left);
		AnimatedCanvas.SetTop(item, top);

		return item;
	}

	private static void OnActiveIndexPropertyValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
		var control = d as MainControl;
		control?.UpdateListBox();
	}

	private void OnAddItemClick(object sender, RoutedEventArgs e)
		=> AddNewItem();

	private void OnClearAllClick(object sender, RoutedEventArgs e)
		=> listBox.Items.Clear();

	private void OnInsertItemClick(object sender, RoutedEventArgs e) {
		int index = MathHelper.Range(listBox.SelectedIndex + 1, 0, listBox.Items.Count);
		listBox.Items.Insert(index, CreateItem());
	}

	private void OnLoaded(object sender, RoutedEventArgs e) {
		for (var i = 0; i < 3; i++)
			AddNewItem();
		listBox.SelectedIndex = 0;

		UpdateListBox();
	}

	/// <summary>
	/// Updates the list box based on the current options.
	/// </summary>
	private void UpdateListBox() {
		if (listBox is not null) {
			// Update the scroll bars
			switch (ActiveIndex) {
				case 0: // AnimatedCanvas
				case 1: // AnimatedDockPanel
					listBox.SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);
					listBox.SetValue(ScrollViewer.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);
					break;

				case 2: // AnimatedStackPanel (Horizontal)
				case 6: // AnimatedWrapPanel (Vertical)
				case 7: // AnimatedWrapPanel (Vertical + Evenly Spaced)
					listBox.SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Auto);
					listBox.SetValue(ScrollViewer.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);
					break;

				case 3: // AnimatedStackPanel (Vertical)
				case 4: // AnimatedWrapPanel (Horizontal)
				case 5: // AnimatedWrapPanel (Horizontal + Evenly Spaced)
					listBox.SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);
					listBox.SetValue(ScrollViewer.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Auto);
					break;
			}

			// Update the item states
			var isDock = (1 == ActiveIndex);
			foreach (var element in listBox.Items.OfType<ProductListBoxItem>()) {
				element.IsDockable = isDock;
				element.IsMovable = !isDock;
			}
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The active index of the <see cref="SwitchPanel"/>.
	/// </summary>
	/// <value>
	/// The default value is <c>0</c>.
	/// </value>
	public int ActiveIndex {
		get => (int)GetValue(ActiveIndexProperty);
		set => SetValue(ActiveIndexProperty, value);
	}

}
