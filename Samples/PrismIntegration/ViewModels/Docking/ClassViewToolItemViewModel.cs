using Prism.Regions;
using System.Windows.Media.Imaging;

namespace ActiproSoftware.Windows.PrismIntegration.ViewModels;

/// <summary>
/// Represents a tool view-model for the sample.
/// </summary>
/// <remarks>
/// This view-model derives from a base class that initializes the <c>ToolWindow</c> from instance properties.
/// </remarks>
public class ClassViewToolItemViewModel : ToolItemViewModel {

	private readonly IRegionManager _regionManager;

	public const string SerializationIdText = "ClassViewToolWindow";

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="regionManager">The region manager.</param>
	public ClassViewToolItemViewModel(IRegionManager regionManager) {
		_regionManager = regionManager ?? throw new ArgumentNullException(nameof(regionManager));

		DefaultDockSide = ToolItemDockSide.Right;
		ImageSource = new BitmapImage(new Uri("/Resources/Images/ClassView16.png", UriKind.Relative));
		SerializationId = SerializationIdText;
		Title = "Class View";
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override IEnumerable<ToolItemDefaultLocation>? GetDefaultLocations(ToolItemState state) {
		if (state == ToolItemState.Docked) {
			var region = _regionManager.Regions[ShellViewModel.MainRegionName];
			if (region is not null) {
				var targetViewModel = region.Views.OfType<ToolItemViewModel>().FirstOrDefault(vm => vm.SerializationId == SolutionExplorerToolItemViewModel.SerializationIdText);
				if (targetViewModel is not null) {
					// Dock below the Solution Explorer
					return [
						new ToolItemDefaultLocation() {
							TargetSerializationId = targetViewModel.SerializationId,
							DockSide = ToolItemDockSide.Bottom
						}
					];
				}
			}
		}

		return base.GetDefaultLocations(state);
	}

}
