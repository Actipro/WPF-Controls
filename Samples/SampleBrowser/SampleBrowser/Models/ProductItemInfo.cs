using System;

namespace ActiproSoftware.SampleBrowser {

    /// <summary>
    /// Provides information about a product item.
    /// </summary>
    public class ProductItemInfo {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the blurb text.
		/// </summary>
		/// <value>The blurb text.</value>
		public string BlurbText { get; set; }

		/// <summary>
		/// Gets or sets whether the sample can be auto-focused following load.
		/// </summary>
		/// <value>
		/// <c>true</c> if the sample can be auto-focused following load; otherwise, <c>false</c>.
		/// </value>
		public bool CanFocusOnLoad { get; set; } = true;

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>The category.</value>
        public string Category { get; set; }

		/// <summary>
		/// Gets the path to the folder containing the product item.
		/// </summary>
		/// <value>The path to the folder containing the product item.</value>
		public string FolderPath {
			get {
				var path = this.Path;
				if (!string.IsNullOrEmpty(path)) {
					var lastSlashIndex = path.LastIndexOf('/');
					if (lastSlashIndex != -1)
						path = path.Substring(0, lastSlashIndex);
				}

				return path;
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
        /// Gets or sets whether the item has a custom status bar.
        /// </summary>
        /// <value>
		/// <c>true</c> if the item has a custom status bar; otherwise, <c>false</c>.
		/// </value>
        public bool HasCustomStatusBar { get; set; }
		
        /// <summary>
        /// Gets or sets whether the item has any interop controls that may cause airspace issues with Backstage overlays.
        /// </summary>
        /// <value>
		/// <c>true</c> if the item has any interop controls that may cause airspace issues with Backstage overlays; otherwise, <c>false</c>.
		/// </value>
        public bool HasInterop { get; set; }
		
		/// <summary>
		/// Gets whether this item is a product overview document.
		/// </summary>
		/// <value>
		/// <c>true</c> if this item is a product overview document; otherwise, <c>false</c>.
		/// </value>
		public bool IsProductOverview {
			get {
				return (this.ProductFamily?.OverviewItem == this);
			}
		}
		
		/// <summary>
		/// Gets whether this item is a release history.
		/// </summary>
		/// <value>
		/// <c>true</c> if this item is a release history; otherwise, <c>false</c>.
		/// </value>
		public bool IsReleaseHistory {
			get {
				return (this.Category == "Release History");
			}
		}
		
		/// <summary>
		/// Gets whether this item is a utility.
		/// </summary>
		/// <value>
		/// <c>true</c> if this item is a utility; otherwise, <c>false</c>.
		/// </value>
		public bool IsUtility {
			get {
				return (this.Category == "Utilities");
			}
		}
		
		/// <summary>
		/// Gets the next <see cref="ProductItemInfo"/>, if any.
		/// </summary>
		/// <value>The next <see cref="ProductItemInfo"/>, if any.</value>
		public ProductItemInfo NextItem {
			get {
				if (this.ProductFamily != null) {
					var index = this.ProductFamily.Items.IndexOf(this);
					if (index < this.ProductFamily.Items.Count - 1)
						return this.ProductFamily.Items[index + 1];
				}

				return null;
			}
		}

		/// <summary>
		/// Gets or sets the file path to the sample.
		/// </summary>
		/// <value>The file path to the sample.</value>
		public string Path { get; set; }
		
		/// <summary>
		/// Gets the previous <see cref="ProductItemInfo"/>, if any.
		/// </summary>
		/// <value>The previous <see cref="ProductItemInfo"/>, if any.</value>
		public ProductItemInfo PreviousItem {
			get {
				if (this.ProductFamily != null) {
					var index = this.ProductFamily.Items.IndexOf(this);
					if (index > 0)
						return this.ProductFamily.Items[index - 1];
					else if ((index == 0) && (this.ProductFamily.OverviewItem != null))
						return this.ProductFamily.OverviewItem;
				}

				return null;
			}
		}
		
		/// <summary>
		/// Gets or sets the <see cref="ProductFamilyInfo"/> that owns this item.
		/// </summary>
		/// <value>The <see cref="ProductFamilyInfo"/> that owns this item.</value>
		public ProductFamilyInfo ProductFamily { get; set; }

		/// <summary>
		/// Gets or sets the search score.
		/// </summary>
		/// <value>The search score.</value>
		public int SearchScore { get; set; }

        /// <summary>
        /// Gets or sets the sidebar width.
        /// </summary>
        /// <value>The sidebar width.</value>
        public PredefinedSideBarWidth SideBarWidth { get; set; } = PredefinedSideBarWidth.Wide;

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

    }

}
