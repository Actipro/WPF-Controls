using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using RibbonControls = ActiproSoftware.Windows.Controls.Ribbon.Controls;
using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Controls.Ribbon.Customization;
using ActiproSoftware.Windows.Controls.Ribbon.UI;
using ActiproSoftware.Windows.Media;

namespace ActiproSoftware.ProductSamples.RibbonSamples.Common {

	/// <summary>
	/// Provides the options window for this sample.
	/// </summary>
	public partial class CustomizeQat : UserControl {

		private DeferrableObservableCollection<RibbonControlReference> qatItems;
		
		#region Dependency Properties
		
		/// <summary>
		/// Identifies the <see cref="Ribbon"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="Ribbon"/> dependency property.</value>
		public static readonly DependencyProperty RibbonProperty = DependencyProperty.Register("Ribbon", typeof(ActiproSoftware.Windows.Controls.Ribbon.Ribbon), typeof(CustomizeQat), new FrameworkPropertyMetadata(null, OnRibbonPropertyValueChanged));
		
		#endregion

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>CustomizeQat</c> class.
		/// </summary>
		public CustomizeQat() {
			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes the window.
		/// </summary>
		private void Initialize() {
			// Get the list of customization categories
			DeferrableObservableCollection<RibbonControlCustomizationCategory> categories = RibbonControlCustomizationCategory.GetCategories(this.Ribbon);
			availableQatCategoriesComboBox.ItemsSource = categories;
			if (categories.Count > 1)
				availableQatCategoriesComboBox.SelectedIndex = 1;

			// Get the list of items already on the QAT
			qatItems = RibbonControlCustomizationCategory.GetQuickAccessToolBarItems(this.Ribbon);
			qatItemsListBox.ItemsSource = qatItems;
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnAddToQatButtonClick(object sender, RoutedEventArgs e) {
			// Ensure that the control has not already been added
			RibbonControlReference controlRef = (RibbonControlReference)availableQatItemsListBox.SelectedItem;
			if (controlRef.IsAlreadyAdded(qatItems)) {
				MessageBox.Show("The selected command is already on the Quick Access Toolbar.", "Quick Access Toolbar", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				return;
			}

			// Clone the selected control and add it to the QAT items list
			UIElement clonedControl = controlRef.Clone();
			qatItems.Insert(qatItemsListBox.SelectedIndex + 1, new RibbonControlReference(clonedControl));
			qatItemsListBox.SelectedIndex = qatItemsListBox.SelectedIndex + 1;
		}

		/// <summary>
		/// Occurs when the selection changes.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="SelectionChangedEventArgs"/> that contains the event data.</param>
		private void OnAvailableQatCategoriesListBoxSelectionChanged(object sender, SelectionChangedEventArgs e) {
			RibbonControlCustomizationCategory category = availableQatCategoriesComboBox.SelectedItem as RibbonControlCustomizationCategory;
			if (category != null) {
				DeferrableObservableCollection<RibbonControlReference> items = category.GetControls();
				availableQatItemsListBox.ItemsSource = items;
			}
			else
				availableQatItemsListBox.ItemsSource = null;
		}
		
		/// <summary>
		/// Occurs when the list is double-clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="SelectionChangedEventArgs"/> that contains the event data.</param>
		private void OnAvailableQatItemsListBoxMouseDoubleClick(object sender, MouseButtonEventArgs e) {
			DependencyObject mouseOver = e.OriginalSource as DependencyObject;
			if (mouseOver != null) {
				ListBoxItem item = (ListBoxItem)VisualTreeHelperExtended.GetCurrentOrAncestor(mouseOver, typeof(ListBoxItem));
				if ((item != null) && (addToQatButton.IsEnabled))
					addToQatButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
			}
		}
		
		/// <summary>
		/// Occurs when the selection changes.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="SelectionChangedEventArgs"/> that contains the event data.</param>
		private void OnAvailableQatItemsListBoxSelectionChanged(object sender, SelectionChangedEventArgs e) {
			this.UpdateButtons();
		}
		
		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnMoveQatItemDownButtonClick(object sender, RoutedEventArgs e) {
			int selectedIndex = qatItemsListBox.SelectedIndex;
			qatItems.Move(selectedIndex, selectedIndex + 1);
			qatItemsListBox.SelectedIndex = selectedIndex + 1;

			this.UpdateButtons();
		}
		
		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnMoveQatItemUpButtonClick(object sender, RoutedEventArgs e) {
			int selectedIndex = qatItemsListBox.SelectedIndex;
			qatItems.Move(selectedIndex, selectedIndex - 1);
			qatItemsListBox.SelectedIndex = selectedIndex - 1;

			this.UpdateButtons();
		}
		
		/// <summary>
		/// Occurs when the list is double-clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="MouseButtonEventArgs"/> that contains the event data.</param>
		private void OnQatItemsListBoxMouseDoubleClick(object sender, MouseButtonEventArgs e) {
			DependencyObject mouseOver = e.OriginalSource as DependencyObject;
			if (mouseOver != null) {
				ListBoxItem item = (ListBoxItem)VisualTreeHelperExtended.GetCurrentOrAncestor(mouseOver, typeof(ListBoxItem));
				if ((item != null) && (removeFromQatButton.IsEnabled))
					removeFromQatButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
			}
		}
		
		/// <summary>
		/// Occurs when the selection changes.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="SelectionChangedEventArgs"/> that contains the event data.</param>
		private void OnQatItemsListBoxSelectionChanged(object sender, SelectionChangedEventArgs e) {
			this.UpdateButtons();
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnRemoveFromQatButtonClick(object sender, RoutedEventArgs e) {
			// Remove the item from the list
			int selectedIndex = qatItemsListBox.SelectedIndex;
			RibbonControlReference controlRef = (RibbonControlReference)qatItemsListBox.SelectedItem;
			qatItems.RemoveAt(selectedIndex);

			// Dispose the cloned item if it is not already in the real QAT
			if (!this.Ribbon.QuickAccessToolBarItems.Contains(controlRef.Control))
				CloneService.DisposeClone(controlRef.Control);

			this.UpdateButtons();
		}
		
		/// <summary>
		/// Occurs when the <see cref="RibbonProperty"/> value is changed.
		/// </summary>
		/// <param name="obj">The <see cref="DependencyObject"/> whose property is changed.</param>
		/// <param name="e">A <see cref="DependencyPropertyChangedEventArgs"/> that contains the event data.</param>
		private static void OnRibbonPropertyValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
			CustomizeQat control = (CustomizeQat)obj;
			if (control.Ribbon != null)
				control.Initialize();
		}
		
		/// <summary>
		/// Updates the enabled states of buttons.
		/// </summary>
		private void UpdateButtons() {
			addToQatButton.IsEnabled = (availableQatItemsListBox.SelectedIndex != -1);
			removeFromQatButton.IsEnabled = (qatItemsListBox.SelectedIndex != -1);
			moveQatItemUpButton.IsEnabled = (qatItemsListBox.SelectedIndex > 0);
			moveQatItemDownButton.IsEnabled = (qatItemsListBox.SelectedIndex != -1) && (qatItemsListBox.SelectedIndex < qatItems.Count - 1);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Cancels any changes that have been made.
		/// </summary>
		public void Cancel() {
			// Dispose cloned items that are in the list of QAT items on this Window but will not be added to the real QAT
			RibbonControlCustomizationCategory.DisposeUnusedClones(this.Ribbon, qatItems);
		}
		
		/// <summary>
		/// Gets or sets the ribbon that is being customized.
		/// </summary>
		/// <value>The ribbon that is being customized.</value>
		public ActiproSoftware.Windows.Controls.Ribbon.Ribbon Ribbon {
			get {
				return (ActiproSoftware.Windows.Controls.Ribbon.Ribbon)this.GetValue(CustomizeQat.RibbonProperty);
			}
			set {
				this.SetValue(CustomizeQat.RibbonProperty, value);
			}
		}
		
		/// <summary>
		/// Saves any changes that have been made.
		/// </summary>
		public void Save() {
			// Update the QAT items per the updated list on this window
			RibbonControlCustomizationCategory.SetQuickAccessToolBarItems(this.Ribbon, qatItems);
		}

	}
}