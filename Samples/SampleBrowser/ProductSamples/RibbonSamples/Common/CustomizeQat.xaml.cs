using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Controls.Ribbon.Customization;
using ActiproSoftware.Windows.Controls.Ribbon.UI;
using ActiproSoftware.Windows.Media;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.RibbonSamples.Common;

/// <summary>
/// Provides the options window for this sample.
/// </summary>
public partial class CustomizeQat : UserControl {

	private DeferrableObservableCollection<RibbonControlReference>? _qatItems;

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="Ribbon"/> property.
	/// </summary>
	public static readonly DependencyProperty RibbonProperty
		= DependencyProperty.Register(nameof(Ribbon), typeof(Windows.Controls.Ribbon.Ribbon), typeof(CustomizeQat), new FrameworkPropertyMetadata(defaultValue: null, OnRibbonPropertyValueChanged));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public CustomizeQat() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the window.
	/// </summary>
	private void Initialize() {
		// Get the list of customization categories
		DeferrableObservableCollection<RibbonControlCustomizationCategory> categories = RibbonControlCustomizationCategory.GetCategories(Ribbon);
		availableQatCategoriesComboBox.ItemsSource = categories;
		if (categories.Count > 1)
			availableQatCategoriesComboBox.SelectedIndex = 1;

		// Get the list of items already on the QAT
		_qatItems = RibbonControlCustomizationCategory.GetQuickAccessToolBarItems(Ribbon);
		qatItemsListBox.ItemsSource = _qatItems;
	}

	private void OnAddToQatButtonClick(object sender, RoutedEventArgs e) {
		// Ensure that the control has not already been added
		var controlRef = (RibbonControlReference)availableQatItemsListBox.SelectedItem;
		if (controlRef.IsAlreadyAdded(_qatItems)) {
			MessageBox.Show("The selected command is already on the Quick Access Toolbar.", "Quick Access Toolbar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			return;
		}

		// Clone the selected control and add it to the QAT items list
		UIElement clonedControl = controlRef.Clone();
		_qatItems?.Insert(qatItemsListBox.SelectedIndex + 1, new RibbonControlReference(clonedControl));
		qatItemsListBox.SelectedIndex = qatItemsListBox.SelectedIndex + 1;
	}

	private void OnAvailableQatCategoriesListBoxSelectionChanged(object sender, SelectionChangedEventArgs e) {
		var category = availableQatCategoriesComboBox.SelectedItem as RibbonControlCustomizationCategory;
		if (category is not null) {
			DeferrableObservableCollection<RibbonControlReference> items = category.GetControls();
			availableQatItemsListBox.ItemsSource = items;
		}
		else
			availableQatItemsListBox.ItemsSource = null;
	}

	private void OnAvailableQatItemsListBoxMouseDoubleClick(object sender, MouseButtonEventArgs e) {
		var mouseOver = e.OriginalSource as DependencyObject;
		if (mouseOver is not null) {
			var item = VisualTreeHelperExtended.GetCurrentOrAncestor<ListBoxItem>(mouseOver);
			if ((item is not null) && (addToQatButton.IsEnabled))
				addToQatButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
		}
	}

	private void OnAvailableQatItemsListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
		=> UpdateButtons();

	private void OnMoveQatItemDownButtonClick(object sender, RoutedEventArgs e) {
		int selectedIndex = qatItemsListBox.SelectedIndex;
		_qatItems?.Move(selectedIndex, selectedIndex + 1);
		qatItemsListBox.SelectedIndex = selectedIndex + 1;

		UpdateButtons();
	}

	private void OnMoveQatItemUpButtonClick(object sender, RoutedEventArgs e) {
		int selectedIndex = qatItemsListBox.SelectedIndex;
		_qatItems?.Move(selectedIndex, selectedIndex - 1);
		qatItemsListBox.SelectedIndex = selectedIndex - 1;

		UpdateButtons();
	}

	private void OnQatItemsListBoxMouseDoubleClick(object sender, MouseButtonEventArgs e) {
		var mouseOver = e.OriginalSource as DependencyObject;
		if (mouseOver is not null) {
			var item = VisualTreeHelperExtended.GetCurrentOrAncestor<ListBoxItem>(mouseOver);
			if ((item is not null) && (removeFromQatButton.IsEnabled))
				removeFromQatButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
		}
	}

	private void OnQatItemsListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
		=> UpdateButtons();

	private void OnRemoveFromQatButtonClick(object sender, RoutedEventArgs e) {
		// Remove the item from the list
		int selectedIndex = qatItemsListBox.SelectedIndex;
		var controlRef = (RibbonControlReference)qatItemsListBox.SelectedItem;
		_qatItems?.RemoveAt(selectedIndex);

		// Dispose the cloned item if it is not already in the real QAT
		if ((Ribbon is { } ribbon) && (!ribbon.QuickAccessToolBarItems.Contains(controlRef.Control)))
			CloneService.DisposeClone(controlRef.Control);

		UpdateButtons();
	}

	/// <summary>
	/// Occurs when the <see cref="RibbonProperty"/> value is changed.
	/// </summary>
	/// <param name="obj">The <see cref="DependencyObject"/> whose property is changed.</param>
	/// <param name="e">The event data.</param>
	private static void OnRibbonPropertyValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
		var control = (CustomizeQat)obj;
		if (control.Ribbon is not null)
			control.Initialize();
	}

	/// <summary>
	/// Updates the enabled states of buttons.
	/// </summary>
	private void UpdateButtons() {
		addToQatButton.IsEnabled = (availableQatItemsListBox.SelectedIndex != -1);
		removeFromQatButton.IsEnabled = (qatItemsListBox.SelectedIndex != -1);
		moveQatItemUpButton.IsEnabled = (qatItemsListBox.SelectedIndex > 0);
		moveQatItemDownButton.IsEnabled = (qatItemsListBox.SelectedIndex != -1) && (qatItemsListBox.SelectedIndex < (_qatItems?.Count ?? 0) - 1);
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Cancels any changes that have been made.
	/// </summary>
	public void Cancel() {
		// Dispose cloned items that are in the list of QAT items on this Window but will not be added to the real QAT
		RibbonControlCustomizationCategory.DisposeUnusedClones(Ribbon, _qatItems);
	}

	/// <summary>
	/// The ribbon that is being customized.
	/// </summary>
	public Windows.Controls.Ribbon.Ribbon? Ribbon {
		get => (Windows.Controls.Ribbon.Ribbon)GetValue(RibbonProperty);
		set => SetValue(RibbonProperty, value);
	}

	/// <summary>
	/// Saves any changes that have been made.
	/// </summary>
	public void Save() {
		// Update the QAT items per the updated list on this window
		RibbonControlCustomizationCategory.SetQuickAccessToolBarItems(Ribbon, _qatItems);
	}

}
