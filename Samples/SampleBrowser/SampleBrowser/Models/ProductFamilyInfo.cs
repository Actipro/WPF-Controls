using System.Collections.Specialized;

namespace ActiproSoftware.SampleBrowser;

/// <summary>
/// Provides information about a product family.
/// </summary>
[ContentProperty(nameof(Items))]
public class ProductFamilyInfo : ObservableObjectBase {

	private IEnumerable<IGrouping<string?, ProductItemInfo>>? _groupedItems;
	private ProductItemInfo? _overviewItem;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public ProductFamilyInfo() {
		Items.CollectionChanged += OnItemsCollectionChanged;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnItemsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		// Clear the cached collection
		_groupedItems = null;

		// Wire up the parent product family references
		if (e.NewItems is not null) {
			foreach (var itemInfo in e.NewItems.OfType<ProductItemInfo>())
				itemInfo.ProductFamily = this;
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The blurb text.
	/// </summary>
	public string? BlurbText { get; set; }

	/// <summary>
	/// The online feature summary URL.
	/// </summary>
	public string FeatureSummaryUrl
		=> string.Format(CultureInfo.InvariantCulture, "https://www.actiprosoftware.com/products/controls/wpf/{0}", ShortTitle?.Replace(" ", string.Empty).ToLowerInvariant());

	/// <summary>
	/// The first <see cref="ProductItemInfo"/> object.
	/// </summary>
	public ProductItemInfo? FirstItem
		=> OverviewItem ?? Items.FirstOrDefault();

	/// <summary>
	/// The collection of <see cref="ProductItemInfo"/> objects for all items.
	/// </summary>
	public IEnumerable<IGrouping<string?, ProductItemInfo>> GroupedItems
		=> _groupedItems ??= Items.Where(i => i != OverviewItem).GroupBy(i => i.Category);

	/// <summary>
	/// Indicates whether there is any blurb text.
	/// </summary>
	public bool HasBlurbText
		=> !string.IsNullOrEmpty(BlurbText);

	/// <summary>
	/// The collection of items.
	/// </summary>
	public ObservableCollection<ProductItemInfo> Items { get; } = [];

	/// <summary>
	/// The logo <see cref="ImageSource"/>.
	/// </summary>
	public ImageSource? LogoImageSource { get; set; }

	/// <summary>
	/// The collection of news items.
	/// </summary>
	public ObservableCollection<ListItemInfo> News { get; } = [];

	/// <summary>
	/// The news sort order.
	/// </summary>
	public int NewsSortOrder { get; set; }

	/// <summary>
	/// The <see cref="ProductItemInfo"/> object for an overview.
	/// </summary>
	public ProductItemInfo? OverviewItem {
		get => _overviewItem;
		set {
			if (_overviewItem != value) {
				_overviewItem = value;
				if (_overviewItem is not null)
					_overviewItem.ProductFamily = this;
			}
		}
	}

	/// <summary>
	/// The short title.
	/// </summary>
	public string? ShortTitle { get; set; }

	/// <summary>
	/// The summary.
	/// </summary>
	public string? Summary { get; set; }

	/// <summary>
	/// The title.
	/// </summary>
	public string? Title { get; set; }

}
