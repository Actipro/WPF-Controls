using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.Controls.Views;

namespace ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.Common;

/// <summary>
/// Represents a product item for displaying in the various panels.
/// </summary>
public partial class ProductListBoxItem : ListBoxItem {

	private static int _counter = 0;

	#region Dependency Property Keys

	/// <summary>
	/// Defines the <see cref="ProductFamily"/> property key.
	/// </summary>
	private static readonly DependencyPropertyKey ProductFamilyPropertyKey
		= DependencyProperty.RegisterReadOnly(nameof(ProductFamily), typeof(ProductFamilyInfo), typeof(ProductListBoxItem), new FrameworkPropertyMetadata(defaultValue: null));

	#endregion

	#region Dependency Properties

	/// <summary>
	/// Defines the <c>IsDockable</c> attached property.
	/// </summary>
	public static readonly DependencyProperty IsDockableProperty
		= DependencyProperty.RegisterAttached(nameof(IsDockable), typeof(bool), typeof(ProductListBoxItem), new FrameworkPropertyMetadata(defaultValue: false));

	/// <summary>
	/// Defines the <c>IsMovable</c> attached property.
	/// </summary>
	public static readonly DependencyProperty IsMovableProperty
		= DependencyProperty.RegisterAttached(nameof(IsMovable), typeof(bool), typeof(ProductListBoxItem), new FrameworkPropertyMetadata(defaultValue: false));

	/// <summary>
	/// Defines the <see cref="ProductFamily"/> property.
	/// </summary>
	public static readonly DependencyProperty ProductFamilyProperty
		= ProductFamilyPropertyKey.DependencyProperty;

	#endregion

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public ProductListBoxItem() {
		Id = _counter++;

		var data = FindResource("ProductData") as ProductData;
		if (data is not null)
			ProductFamily = data.ProductFamilies[Id % data.ProductFamilies.Count];

		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnDeleteButtonClick(object sender, RoutedEventArgs e) {
		var parent = Parent as ListBox;
		parent?.Items.Remove(this);
	}

	private void OnDockBottomButtonClick(object sender, RoutedEventArgs e)
		=> AnimatedDockPanel.SetDock(this, Dock.Bottom);

	private void OnDockLeftButtonClick(object sender, RoutedEventArgs e)
		=> AnimatedDockPanel.SetDock(this, Dock.Left);

	private void OnDockRightButtonClick(object sender, RoutedEventArgs e)
		=> AnimatedDockPanel.SetDock(this, Dock.Right);

	private void OnDockTopButtonClick(object sender, RoutedEventArgs e)
		=> AnimatedDockPanel.SetDock(this, Dock.Top);

	private void OnMoveLeftButtonClick(object sender, RoutedEventArgs e) {
		if (Parent is ListBox parent) {
			var index = parent.Items.IndexOf(this);
			if (index > 0) {
				var selectedIndex = parent.SelectedIndex;
				parent.Items.RemoveAt(index);
				parent.Items.Insert(index - 1, this);

				if (selectedIndex == index)
					parent.SelectedIndex = index - 1;
			}
		}
	}

	private void OnMoveRightButtonClick(object sender, RoutedEventArgs e) {
		if (Parent is ListBox parent) {
			var index = parent.Items.IndexOf(this);
			if (index < parent.Items.Count - 1) {
				var selectedIndex = parent.SelectedIndex;
				parent.Items.RemoveAt(index);
				parent.Items.Insert(index + 1, this);

				if (selectedIndex == index)
					parent.SelectedIndex = index + 1;
			}
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The product item identifier.
	/// </summary>
	public int Id { get; }

	/// <summary>
	/// Indicates whether this instance is dockable.
	/// </summary>
	/// <value>
	/// The default value is <c>false</c>.
	/// </value>
	public bool IsDockable {
		get => (bool)GetValue(IsDockableProperty);
		set => SetValue(IsDockableProperty, value);
	}

	/// <summary>
	/// Indicates whether this instance is movable.
	/// </summary>
	/// <value>
	/// The default value is <c>false</c>.
	/// </value>
	public bool IsMovable {
		get => (bool)GetValue(IsMovableProperty);
		set => SetValue(IsMovableProperty, value);
	}

	/// <summary>
	/// The product family.
	/// </summary>
	/// <value>
	/// The default value is <c>null</c>.
	/// </value>
	public ProductFamilyInfo? ProductFamily {
		get => (ProductFamilyInfo)GetValue(ProductFamilyProperty);
		private set => SetValue(ProductListBoxItem.ProductFamilyPropertyKey, value);
	}

}
