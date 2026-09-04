using ActiproSoftware.ProductSamples.NavigationSamples.Common.Breadcrumb.ShellItem;
using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Controls.Navigation;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.BreadcrumbComboBox;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	private bool _includeFavorites = true;
	private bool _includeRecentHistory = true;

	/// <summary>
	/// Holds the favorite items shown in the ComboBox in the Breadcrumb.
	/// </summary>
	private readonly DeferrableObservableCollection<object> _favoriteItems = [];

	/// <summary>
	/// Holds the recent items shown in the ComboBox in the Breadcrumb.
	/// </summary>
	private readonly DeferrableObservableCollection<object> _recentItems = [];

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();
		OnUpdateComboItems(sender: null, e: null);
		AddHandler(LoadedEvent, new RoutedEventHandler(OnLoaded));
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Handles the <see cref="Breadcrumb.ConvertItem"/> event.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnBreadcrumbConvertItem(object? sender, BreadcrumbConvertItemEventArgs e)
		=> ConvertItemHelper.HandleConvertItem(sender, e);

	/// <summary>
	/// Handles the <see cref="Breadcrumb.SelectedItemChanged"/> event.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnBreadcrumbSelectedItemChanged(object? sender, ObjectPropertyChangedRoutedEventArgs e)
		=> UpdateRecentItems();

	private void OnLoaded(object sender, RoutedEventArgs e) {
		_favoriteItems.BeginUpdate();
		try {
			_favoriteItems.Clear();
			if (breadcrumb.RootItem is { } rootItem) {
				_favoriteItems.Add(ConvertItemHelper.GetItem(rootItem, @"Desktop\Control Panel\Security")!);
				_favoriteItems.Add(ConvertItemHelper.GetItem(rootItem, @"Desktop\Recycle Bin")!);
				_favoriteItems.Add(ConvertItemHelper.GetItem(rootItem, @"Desktop\Computer\Local Disk (C:)\Temp")!);
			}
		}
		finally {
			_favoriteItems.EndUpdate();
		}
	}

	/// <summary>
	/// Updates the <see cref="ComboBoxItems"/> collection.
	/// </summary>
	/// <param name="sender">The sender.</param>
	/// <param name="e">The event data.</param>
	private void OnUpdateComboItems(object? sender, RoutedEventArgs? e) {
		if (!IsInitialized)
			return;

		ComboBoxItems.Clear();

		if (IncludeFavorites) {
			var separator = new Separator {
				Style = (Style)FindResource("FavoritesSeparatorStyle")
			};
			ComboBoxItems.Add(separator);

			var collectionContainer = new CollectionContainer {
				Collection = _favoriteItems
			};
			ComboBoxItems.Add(collectionContainer);
		}

		if (IncludeRecentHistory) {
			var separator = new Separator {
				Style = (Style)FindResource("RecentSeparatorStyle")
			};
			ComboBoxItems.Add(separator);

			var collectionContainer = new CollectionContainer {
				Collection = _recentItems
			};
			ComboBoxItems.Add(collectionContainer);
		}

		if (ComboBoxItems.Count == 0) {
			var separator = new Separator {
				Style = (Style)FindResource("EmptyListSeparatorStyle")
			};
			ComboBoxItems.Add(separator);
		}
	}

	/// <summary>
	/// Updates the <see cref="RecentItems"/>.
	/// </summary>
	private void UpdateRecentItems() {
		if (breadcrumb.SelectedItem is { } selectedItem) {
			_recentItems.BeginUpdate();
			try {
				// Make sure item doesn't already exist in the list
				while (_recentItems.Remove(selectedItem)) { /* no-op */ }

				// Insert it at the beginning
				_recentItems.Insert(0, selectedItem);

				// Cap the size of the list
				while (_recentItems.Count > 15)
					_recentItems.RemoveAt(15);
			}
			finally {
				_recentItems.EndUpdate();
			}
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// A combined collection of favorites and recent items.
	/// </summary>
	public CompositeCollection ComboBoxItems { get; } = [];

	/// <summary>
	/// Indicates whether to include favorites.
	/// </summary>
	public bool IncludeFavorites {
		get => _includeFavorites;
		set {
			if (_includeFavorites != value) {
				_includeFavorites = value;
				OnUpdateComboItems(sender: null, e: null);
			}
		}
	}

	/// <summary>
	/// Indicates whether to include recent history.
	/// </summary>
	public bool IncludeRecentHistory {
		get => _includeRecentHistory;
		set {
			if (_includeRecentHistory != value) {
				_includeRecentHistory = value;
				OnUpdateComboItems(sender: null, e: null);
			}
		}
	}

}
