namespace ActiproSoftware.SampleBrowser;

/// <summary>
/// Provides product data information.
/// </summary>
[ContentProperty(nameof(ProductFamilies))]
public class ProductData {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The collection of featured sample items.
	/// </summary>
	public ObservableCollection<ListItemInfo> FeaturedSamples { get; } = [];

	/// <summary>
	/// The collection of product families.
	/// </summary>
	public ObservableCollection<ProductFamilyInfo> ProductFamilies { get; } = [];

	/// <summary>
	/// The collection of product families with news.
	/// </summary>
	public IEnumerable<ProductFamilyInfo> ProductFamiliesWithNews {
		get => ProductFamilies.OfType<ProductFamilyInfo>()
			.Where(pf => pf.News.Any())
			.OrderBy(pf => pf.NewsSortOrder)
			.ThenByDescending(pf => pf.News.Count);
	}

	#pragma warning disable CA1822 // Mark members as static
	/// <summary>
	/// The product version text.
	/// </summary>
	public string ProductVersionText
		=> "v" + ActiproSoftware.Properties.Shared.AssemblyInfo.Instance.VersionText.Substring(0, 4);
	#pragma warning restore CA1822

	#pragma warning disable CA1822 // Mark members as static
	/// <summary>
	/// The product version with build text.
	/// </summary>
	public string ProductVersionWithBuildText
		=> "v" + ActiproSoftware.Properties.Shared.AssemblyInfo.Instance.InformationalVersionText;
	#pragma warning restore CA1822

	/// <summary>
	/// The <see cref="ProductFamilyInfo"/> that contains release histories.
	/// </summary>
	public ProductFamilyInfo? ReleaseHistory { get; set; }

	/// <summary>
	/// The <see cref="ProductFamilyInfo"/> that contains utilities.
	/// </summary>
	public ProductFamilyInfo? Utilities { get; set; }

}
