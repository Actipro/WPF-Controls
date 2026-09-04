using ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.Common;
using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.Controls.Views;
using ActiproSoftware.Windows.Data;

namespace ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.AnimatedWrapPanelIntro;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="IsEmptySpaceEvenlyDistributed"/> property.
	/// </summary>
	public static readonly DependencyProperty IsEmptySpaceEvenlyDistributedProperty
		= DependencyProperty.Register(nameof(IsEmptySpaceEvenlyDistributed), typeof(bool), typeof(MainControl), new FrameworkPropertyMetadata(defaultValue: false));

	/// <summary>
	/// Defines the <see cref="Orientation"/> property.
	/// </summary>
	public static readonly DependencyProperty OrientationProperty
		= DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(MainControl), new FrameworkPropertyMetadata(defaultValue: Orientation.Horizontal, OnOrientationPropertyValueChanged));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		var data = FindResource("ProductData") as ProductData;
		if (data is not null) {
			for (var i = 0; i < data.ProductFamilies.Count; i++)
				AddNewItem();
			listBox.SelectedIndex = 0;
		}

		UpdateListBox();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void AddNewItem()
		=> listBox.Items.Add(CreateItem());

	/// <summary>
	/// Creates a new <see cref="ProductListBoxItem"/> instance.
	/// </summary>
	private static ProductListBoxItem CreateItem()
		=> new() { IsMovable = true };

	private void OnClearAllClick(object sender, RoutedEventArgs e)
		=> listBox.Items.Clear();

	private void OnAddItemClick(object sender, RoutedEventArgs e)
		=> AddNewItem();

	private void OnInsertItemClick(object sender, RoutedEventArgs e) {
		int index = MathHelper.Range(listBox.SelectedIndex + 1, 0, listBox.Items.Count);
		listBox.Items.Insert(index, CreateItem());
	}

	private static void OnOrientationPropertyValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
		var control = d as MainControl;
		control?.UpdateListBox();
	}

	/// <summary>
	/// Updates the list box based on the current options.
	/// </summary>
	private void UpdateListBox() {
		if (listBox is not null) {
			if (Orientation == Orientation.Horizontal) {
				listBox.SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);
				listBox.SetValue(ScrollViewer.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Auto);
			}
			else {
				listBox.SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Auto);
				listBox.SetValue(ScrollViewer.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);
			}
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Indicates whether any empty space in a row/column will be evenly distributed around the elements.
	/// </summary>
	/// <value>
	/// The default value is <c>false</c>.
	/// </value>
	public bool IsEmptySpaceEvenlyDistributed {
		get => (bool)GetValue(IsEmptySpaceEvenlyDistributedProperty);
		set => SetValue(IsEmptySpaceEvenlyDistributedProperty, value);
	}

	/// <summary>
	/// The orientation of the <see cref="AnimatedWrapPanel"/>.
	/// </summary>
	/// <value>
	/// The default value is <see cref="Orientation.Horizontal"/>.
	/// </value>
	public Orientation Orientation {
		get => (Orientation)GetValue(OrientationProperty);
		set => SetValue(OrientationProperty, value);
	}

}
