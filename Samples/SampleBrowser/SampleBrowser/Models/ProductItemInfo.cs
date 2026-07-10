
namespace ActiproSoftware.SampleBrowser;

/// <summary>
/// Provides information about a product item.
/// </summary>
public class ProductItemInfo {

	private string? _blurbText;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The blurb text.
	/// </summary>
	public string? BlurbText {
		get => _blurbText ?? (IsPrivate ? "Private!" : null);
		set => _blurbText = value;
	}

	/// <summary>
	/// Indicates whether the sample can be auto-focused following load.
	/// </summary>
	public bool CanFocusOnLoad { get; set; } = true;

	/// <summary>
	/// The category.
	/// </summary>
	public string? Category { get; set; }

	/// <summary>
	/// The path to the folder containing the product item.
	/// </summary>
	public string? FolderPath {
		get {
			var path = Path;
			if (!string.IsNullOrEmpty(path)) {
				var lastSlashIndex = path!.LastIndexOf('/');
				if (lastSlashIndex != -1)
					path = path.Substring(0, lastSlashIndex);
			}

			return path;
		}
	}

	/// <summary>
	/// Indicates whether there is any blurb text.
	/// </summary>
	public bool HasBlurbText
		=> !string.IsNullOrEmpty(BlurbText);

	/// <summary>
	/// Indicates whether the item has a custom status bar.
	/// </summary>
	public bool HasCustomStatusBar { get; set; }

	/// <summary>
	/// Indicates whether the item has any interop controls that may cause airspace issues with Backstage overlays.
	/// </summary>
	public bool HasInterop { get; set; }

	/// <summary>
	/// Indicates whether this item is a private item not intended for inclusion in public projects.
	/// </summary>
	public bool IsPrivate { get; set; }

	/// <summary>
	/// Indicates whether this item is a product overview document.
	/// </summary>
	public bool IsProductOverview
		=> ProductFamily?.OverviewItem == this;

	/// <summary>
	/// Indicates whether this item is a release history.
	/// </summary>
	public bool IsReleaseHistory
		=> Category == "Release History";

	/// <summary>
	/// Indicates whether this item is a utility.
	/// </summary>
	public bool IsUtility
		=> Category == "Utilities";

	/// <summary>
	/// The next <see cref="ProductItemInfo"/>, if any.
	/// </summary>
	public ProductItemInfo? NextItem {
		get {
			if (ProductFamily is { } productFamily) {
				var index = productFamily.Items.IndexOf(this);
				if (index < productFamily.Items.Count - 1)
					return productFamily.Items[index + 1];
			}

			return null;
		}
	}

	/// <summary>
	/// The file path to the sample.
	/// </summary>
	public string? Path { get; set; }

	/// <summary>
	/// The previous <see cref="ProductItemInfo"/>, if any.
	/// </summary>
	public ProductItemInfo? PreviousItem {
		get {
			if (ProductFamily is { } productFamily) {
				var index = productFamily.Items.IndexOf(this);
				if (index > 0)
					return productFamily.Items[index - 1];
				else if ((index == 0) && (productFamily.OverviewItem is not null))
					return ProductFamily.OverviewItem;
			}

			return null;
		}
	}

	/// <summary>
	/// The <see cref="ProductFamilyInfo"/> that owns this item.
	/// </summary>
	public ProductFamilyInfo? ProductFamily { get; set; }

	/// <summary>
	/// The search score.
	/// </summary>
	public int SearchScore { get; set; }

	/// <summary>
	/// The sidebar width.
	/// </summary>
	public PredefinedSideBarWidth SideBarWidth { get; set; } = PredefinedSideBarWidth.Wide;

	/// <summary>
	/// The title.
	/// </summary>
	public string? Title { get; set; }

}
