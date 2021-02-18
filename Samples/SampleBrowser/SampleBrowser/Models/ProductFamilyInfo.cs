using ActiproSoftware.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Windows.Markup;
using System.Windows.Media;

namespace ActiproSoftware.SampleBrowser {

    /// <summary>
    /// Provides information about a product family.
    /// </summary>
	[ContentProperty("Items")]
    public class ProductFamilyInfo : ObservableObjectBase {

		private IEnumerable<IGrouping<string, ProductItemInfo>>	groupedItems;
		private ProductItemInfo									overviewItem;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <see cref="ProductFamilyInfo"/> class.
		/// </summary>
		public ProductFamilyInfo() {
			Items.CollectionChanged += OnItemsCollectionChanged;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the items collection has changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="NotifyCollectionChangedEventArgs"/> that contains the event data.</param>
		private void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
			// Clear the cached collection
			groupedItems = null;

			// Wire up the parent product family references
			if (e.NewItems != null) {
				foreach (var itemInfoObj in e.NewItems) {
					var itemInfo = itemInfoObj as ProductItemInfo;
					if (itemInfo != null)
						itemInfo.ProductFamily = this;
				}
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the blurb text.
		/// </summary>
		/// <value>The blurb text.</value>
		public string BlurbText { get; set; }

		/// <summary>
		/// Gets the online feature summary URL.
		/// </summary>
		/// <value>The online feature summary URL.</value>
		public string FeatureSummaryUrl {
			get {
				return string.Format(CultureInfo.InvariantCulture, "https://www.actiprosoftware.com/products/controls/wpf/{0}", this.ShortTitle.Replace(" ", string.Empty).ToLowerInvariant());
			}
		}

		/// <summary>
		/// Gets the first <see cref="ProductItemInfo"/> object.
		/// </summary>
		/// <value>The first <see cref="ProductItemInfo"/> object.</value>
		public ProductItemInfo FirstItem {
			get {
				return this.OverviewItem ?? Items.FirstOrDefault();
			}
		}
		
		/// <summary>
		/// Gets the collection of <see cref="ProductItemInfo"/> objects for all items.
		/// </summary>
		/// <value>The collection of <see cref="ProductItemInfo"/> objects for all items.</value>
		public IEnumerable<IGrouping<string, ProductItemInfo>> GroupedItems {
			get {
				if (groupedItems == null) {
					groupedItems = from i in Items
								   where i != this.OverviewItem
								   group i by i.Category;
				}

				return groupedItems;
			}
		}

		/// <summary>
		/// Gets whether there is any blurb text.
		/// </summary>
		/// <value>
		/// <c>true</c> if there is any blurb text; otherwise, <c>false</c>.
		/// </value>
		public bool HasBlurbText {
			get {
				return !string.IsNullOrEmpty(this.BlurbText);
			}
		}

		/// <summary>
		/// Gets the collection of items.
		/// </summary>
		/// <value>The collection of items.</value>
		public ObservableCollection<ProductItemInfo> Items { get; } = new ObservableCollection<ProductItemInfo>();

        /// <summary>
        /// Gets or sets the logo <see cref="ImageSource"/>.
        /// </summary>
        /// <value>The logo <see cref="ImageSource"/>.</value>
        public ImageSource LogoImageSource { get; set; }

		/// <summary>
		/// Gets the collection of news items.
		/// </summary>
		/// <value>The collection of news items.</value>
		public ObservableCollection<ListItemInfo> News { get; } = new ObservableCollection<ListItemInfo>();

		/// <summary>
		/// Gets or sets the news sort order.
		/// </summary>
		/// <value>The news sort order.</value>
		public int NewsSortOrder { get; set; }

		/// <summary>
		/// Gets the <see cref="ProductItemInfo"/> object for an overview.
		/// </summary>
		/// <value>The <see cref="ProductItemInfo"/> object for an overview.</value>
		public ProductItemInfo OverviewItem {
			get {
				return overviewItem;
			}
			set {
				if (overviewItem != value) {
					overviewItem = value;
					overviewItem.ProductFamily = this;
				}
			}
		}
		
        /// <summary>
        /// Gets or sets the short title.
        /// </summary>
        /// <value>The short title.</value>
        public string ShortTitle { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        /// <value>The summary.</value>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

    }

}
