using ActiproSoftware.Windows.Controls.Docking;
using ActiproSoftware.Windows.PrismIntegration.Regions.Behaviors;
using ActiproSoftware.Windows.PrismIntegration.ViewModels;
using Prism.Regions;

namespace ActiproSoftware.Windows.PrismIntegration.Regions;

/// <summary>
/// Adapter that creates a new <see cref="Region"/> and binds all the views to the adapted <see cref="DockSite"/>.
/// </summary>
public class DockSiteRegionAdapter : RegionAdapterBase<DockSite> {

	private readonly DockSiteRegionViewKinds _supportedViewKinds;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes a new instance of the class that supports document and tool views.
	/// </summary>
	/// <param name="regionBehaviorFactory">The factory used to create the region behaviors to attach to the created regions.</param>
	public DockSiteRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
		: this(regionBehaviorFactory, DockSiteRegionViewKinds.Both) { }

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="regionBehaviorFactory">The factory used to create the region behaviors to attach to the created regions.</param>
	/// <param name="supportedViewKinds">The view kinds that are supported by the adapter.</param>
	private DockSiteRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory, DockSiteRegionViewKinds supportedViewKinds)
		: base(regionBehaviorFactory) {

		if (supportedViewKinds == DockSiteRegionViewKinds.None)
			throw new ArgumentException("At least one view kind must be specified.", nameof(supportedViewKinds));

		// Initialize
		_supportedViewKinds = supportedViewKinds;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Indicates whether the region can support document views.
	/// </summary>
	private bool CanSupportDocumentViews
		=> (_supportedViewKinds & DockSiteRegionViewKinds.Document) == DockSiteRegionViewKinds.Document;

	/// <summary>
	/// Indicates whether the region can support tool views.
	/// </summary>
	private bool CanSupportToolViews
		=> (_supportedViewKinds & DockSiteRegionViewKinds.Tool) == DockSiteRegionViewKinds.Tool;

	/// <summary>
	/// The <see cref="DockingWindowItemKind"/> associated with the specified item, if any.
	/// </summary>
	/// <param name="item">The item whose docking type should be returned.</param>
	private static DockingWindowItemKind GetDockingWindowItemKind(object item)
		=> item switch {
			DocumentWindow => DockingWindowItemKind.Document,
			ToolWindow => DockingWindowItemKind.Tool,
			DockingItemViewModelBase viewModel => viewModel.IsTool ? DockingWindowItemKind.Tool : DockingWindowItemKind.Document,
			_ => DockingWindowItemKind.Document
		};

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Adapts a <see cref="DockSite"/> to an <see cref="IRegion"/>.
	/// </summary>
	/// <param name="region">The new region being used.</param>
	/// <param name="regionTarget">The <see cref="DockSite"/> to adapt.</param>
	protected override void Adapt(IRegion region, DockSite regionTarget) {
		ArgumentNullException.ThrowIfNull(region);
		ArgumentNullException.ThrowIfNull(regionTarget);

		// If supporting document views, ensure that DockSite.DocumentItemsSource hasn't been set
		if (CanSupportDocumentViews
			&& (
				regionTarget.DocumentItemsSource is not null
				|| BindingOperations.GetBinding(regionTarget, DockSite.DocumentItemsSourceProperty) is not null
			)
		) {
			throw new InvalidOperationException("The DockSite.DocumentItemsSource property is not empty.");
		}

		// If supporting tool views, ensure that DockSite.ToolItemsSource hasn't been set
		if (CanSupportToolViews
			&& (
				regionTarget.ToolItemsSource is not null
				|| BindingOperations.GetBinding(regionTarget, DockSite.ToolItemsSourceProperty) is not null
			)
		) {
			throw new InvalidOperationException("The DockSite.ToolItemsSource property is not empty.");
		}

		// Set the items source properties appropriately
		switch (_supportedViewKinds) {
			case DockSiteRegionViewKinds.Document:
				regionTarget.DocumentItemsSource = region.Views;
				break;
			case DockSiteRegionViewKinds.Tool:
				regionTarget.ToolItemsSource = region.Views;
				break;
			case DockSiteRegionViewKinds.Both:
				regionTarget.DocumentItemsSource = new EnumerableView<object>(
					region.Views,
					o => GetDockingWindowItemKind(o) != DockingWindowItemKind.Document
				);
				regionTarget.ToolItemsSource = new EnumerableView<object>(
					region.Views,
					o => GetDockingWindowItemKind(o) != DockingWindowItemKind.Tool
				);
				break;
		}
	}

	/// <summary>
	/// Attaches behaviors to the region.
	/// </summary>
	/// <param name="region">The region.</param>
	/// <param name="regionTarget">The region target.</param>
	/// <remarks>
	/// This class attaches the base behaviors and an instance of <see cref="DockSiteRegionBehavior"/>.
	/// </remarks>
	protected override void AttachBehaviors(IRegion region, DockSite regionTarget) {
		ArgumentNullException.ThrowIfNull(region);
		ArgumentNullException.ThrowIfNull(regionTarget);

		// Add the behavior that syncs the items source items with the rest of the items
		region.Behaviors.Add(
			DockSiteRegionBehavior.BehaviorKey,
			new DockSiteRegionBehavior() { DockSite = regionTarget }
		);

		// Call the base method
		base.AttachBehaviors(region, regionTarget);
	}

	/// <summary>
	/// Creates a new instance of a <see cref="Region"/>.
	/// </summary>
	/// <returns>The <see cref="Region"/> that was created.</returns>
	protected override IRegion CreateRegion() {
		// Clear the SortComparison or else any time a region is added, the Prism items source will tell DockSite
		//   to reset and windows won't remain open properly
		return new Region {
			SortComparison = null
		};
	}

}
