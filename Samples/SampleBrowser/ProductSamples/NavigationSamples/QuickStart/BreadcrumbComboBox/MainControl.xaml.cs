using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Threading;

using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Controls.Navigation;
using ActiproSoftware.ProductSamples.NavigationSamples.Common.Breadcrumb.ShellItem;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.BreadcrumbComboBox {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private bool includeFavorites = true;
		private bool includeRecentHistory = true;

		/// <summary>
		/// Holds a combined collection of favorites and recent items.
		/// </summary>
		private CompositeCollection comboBoxItems = new CompositeCollection();

		/// <summary>
		/// Holds the favorite items shown in the ComboBox in the Breadcrumb.
		/// </summary>
		private DeferrableObservableCollection<object> favoriteItems = new DeferrableObservableCollection<object>();

		/// <summary>
		/// Holds the recent items shown in the ComboBox in the Breadcrumb.
		/// </summary>
		private DeferrableObservableCollection<object> recentItems = new DeferrableObservableCollection<object>();

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();
			OnUpdateComboItems(null, null);
			this.AddHandler(FrameworkElement.LoadedEvent, new RoutedEventHandler(OnLoaded));
		}


		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Handles the ConvertItem event of the breadcrumb control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="ActiproSoftware.Windows.Controls.Navigation.BreadcrumbConvertItemEventArgs"/> instance containing the event data.</param>
		private void OnBreadcrumbConvertItem(object sender, BreadcrumbConvertItemEventArgs e) {
			ConvertItemHelper.HandleConvertItem(sender, e);
		}

		/// <summary>
		/// Handles the SelectedItemChanged event of the breadcrumb control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="PropertyChangedRoutedEventArgs{object}"/> instance containing the event data.</param>
		private void OnBreadcrumbSelectedItemChanged(object sender, ObjectPropertyChangedRoutedEventArgs e) {
			UpdateRecentItems();
		}

		/// <summary>
		/// Occurs when the element is loaded
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnLoaded(object sender, RoutedEventArgs e) {
			this.favoriteItems.BeginUpdate();
			try {
				this.favoriteItems.Clear();
				object rootItem = this.breadcrumb.RootItem;
				this.favoriteItems.Add(ConvertItemHelper.GetItem(rootItem, "Desktop\\Control Panel\\Security"));
				this.favoriteItems.Add(ConvertItemHelper.GetItem(rootItem, "Desktop\\Recycle Bin"));
				this.favoriteItems.Add(ConvertItemHelper.GetItem(rootItem, "Desktop\\Computer\\Local Disk (C:)\\Temp"));
			}
			finally {
				this.favoriteItems.EndUpdate();
			}
		}

		/// <summary>
		/// Updates the <see cref="ComboBoxItems"/> collection.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnUpdateComboItems(object sender, RoutedEventArgs e) {
			if (!this.IsInitialized)
				return;

			this.comboBoxItems.Clear();

			if (this.IncludeFavorites) {
				Separator sep = new Separator();
				sep.Style = (Style)FindResource("FavoritesSeparatorStyle");
				this.comboBoxItems.Add(sep);

				CollectionContainer collectionContainer = new CollectionContainer();
				collectionContainer.Collection = this.favoriteItems;
				this.comboBoxItems.Add(collectionContainer);
			}

			if (this.IncludeRecentHistory) {
				Separator sep = new Separator();
				sep.Style = (Style)FindResource("RecentSeparatorStyle");
				this.comboBoxItems.Add(sep);

				CollectionContainer collectionContainer = new CollectionContainer();
				collectionContainer.Collection = this.recentItems;
				this.comboBoxItems.Add(collectionContainer);
			}

			if (0 == this.comboBoxItems.Count) {
				Separator sep = new Separator();
				sep.Style = (Style)FindResource("EmptyListSeparatorStyle");
				this.comboBoxItems.Add(sep);
			}
		}

		/// <summary>
		/// Updates the <see cref="RecentItems"/>.
		/// </summary>
		private void UpdateRecentItems() {
			if (null != this.breadcrumb.SelectedItem) {
				this.recentItems.BeginUpdate();
				try {
					// Make sure item doesn't already exist in the list
					while (this.recentItems.Remove(this.breadcrumb.SelectedItem))
						; // No-op

					// Insert it at the beginning
					this.recentItems.Insert(0, this.breadcrumb.SelectedItem);

					// Cap the size of the list
					while (this.recentItems.Count > 15)
						this.recentItems.RemoveAt(15);
				}
				finally {
					this.recentItems.EndUpdate();
				}
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the combo box items.
		/// </summary>
		/// <value>The combo box items.</value>
		public CompositeCollection ComboBoxItems {
			get {
				return this.comboBoxItems;
			}
		}
		
		/// <summary>
		/// Gets or sets whether to include favorites.
		/// </summary>
		/// <value>
		/// <c>true</c> if favorites should be included; otherwise, <c>false</c>.
		/// </value>
		public bool IncludeFavorites {
			get {
				return includeFavorites;
			}
			set {
				if (includeFavorites != value) {
					includeFavorites = value;
					OnUpdateComboItems(null, null);
				}
			}
		}

		/// <summary>
		/// Gets or sets whether to include recent history.
		/// </summary>
		/// <value>
		/// <c>true</c> if recent history should be included; otherwise, <c>false</c>.
		/// </value>
		public bool IncludeRecentHistory {
			get {
				return includeRecentHistory;
			}
			set {
				if (includeRecentHistory != value) {
					includeRecentHistory = value;
					OnUpdateComboItems(null, null);
				}
			}
		}


	}
}