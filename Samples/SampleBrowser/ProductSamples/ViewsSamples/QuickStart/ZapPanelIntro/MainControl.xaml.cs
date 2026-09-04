using ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.Common;
using ActiproSoftware.Windows.Controls.Views;
using ActiproSoftware.Windows.Data;

namespace ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.ZapPanelIntro;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="AreChildrenWrapped"/> property.
	/// </summary>
	public static readonly DependencyProperty AreChildrenWrappedProperty
		= DependencyProperty.Register(nameof(AreChildrenWrapped), typeof(bool), typeof(MainControl), new FrameworkPropertyMetadata(defaultValue: false));

	/// <summary>
	/// Defines the <see cref="Orientation"/> property.
	/// </summary>
	public static readonly DependencyProperty OrientationProperty
		= DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(MainControl), new FrameworkPropertyMetadata(defaultValue: Orientation.Vertical, OnOrientationPropertyValueChanged));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		for (var i = 0; i < 3; i++)
			AddNewItem();
		listBox.SelectedIndex = 0;

		UpdateListBox();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void AddNewItem()
		=> listBox.Items.Add(CreateItem());

	private static ProductListBoxItem CreateItem()
		=> new() { IsMovable = true };

	private void OnClearAllClick(object sender, RoutedEventArgs e)
		=> listBox.Items.Clear();

	private void OnAddItemClick(object sender, RoutedEventArgs e)
		=> AddNewItem();

	private void OnInsertItemClick(object sender, RoutedEventArgs e) {
		var index = MathHelper.Range(listBox.SelectedIndex + 1, 0, listBox.Items.Count);
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
				listBox.SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Auto);
				listBox.SetValue(ScrollViewer.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);
			}
			else {
				listBox.SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);
				listBox.SetValue(ScrollViewer.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Auto);
			}
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Indicates whether the child items are wrapped around the item currently centered in the <see cref="ZapPanel"/>.
	/// </summary>
	/// <value>
	/// The default value is <c>false</c>.
	/// </value>
	public bool AreChildrenWrapped {
		get => (bool)GetValue(AreChildrenWrappedProperty);
		set => SetValue(AreChildrenWrappedProperty, value);
	}

	/// <summary>
	/// The orientation of the <see cref="ZapPanel"/>.
	/// </summary>
	/// <value>
	/// The default value is <see cref="Orientation.Vertical"/>.
	/// </value>
	public Orientation Orientation {
		get => (Orientation)GetValue(OrientationProperty);
		set => SetValue(OrientationProperty, value);
	}

}
