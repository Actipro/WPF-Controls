using Prism.Regions;
using System.Windows.Media.Imaging;

namespace ActiproSoftware.Windows.PrismIntegration.ViewModels {

	/// <summary>
	/// Represents a tool view-model for the sample.
	/// </summary>
	/// <remarks>
	/// This view-model derives from a base class that initializes the <c>ToolWindow</c> from instance properties.
	/// </remarks>
	public class SolutionExplorerToolItemViewModel : ToolItemViewModel {
		
		private readonly IRegionManager _regionManager;

		public const string SerializationIdText = "SolutionExplorerToolWindow";

		// --------------------------------------------------------------------------------------------------
		// OBJECT
		// --------------------------------------------------------------------------------------------------

		/// <summary>
		/// Initializes an instance of the class.
		/// </summary>
		/// <param name="regionManager">The region manager.</param>
		public SolutionExplorerToolItemViewModel(IRegionManager regionManager) {
			_regionManager = regionManager ?? throw new ArgumentNullException(nameof(regionManager));

			DefaultDockSide = ToolItemDockSide.Right;
			ImageSource = new BitmapImage(new Uri("/Resources/Images/SolutionExplorer16.png", UriKind.Relative));
			SerializationId = SerializationIdText;
			Title = "Solution Explorer";
		}
		
		// --------------------------------------------------------------------------------------------------
		// PUBLIC PROCEDURES
		// --------------------------------------------------------------------------------------------------
		
		/// <inheritdoc/>
		public override IEnumerable<ToolItemDefaultLocation>? GetDefaultLocations(ToolItemState state) {
			if (state == ToolItemState.Docked) {
				var region = _regionManager.Regions[ShellViewModel.MainRegionName];
				if (region is not null) {
					var targetViewModel = region.Views.OfType<ToolItemViewModel>().FirstOrDefault(vm => vm.SerializationId == ClassViewToolItemViewModel.SerializationIdText);
					if (targetViewModel is not null) {
						// Dock above the Class Library
						return [
							new ToolItemDefaultLocation() {
								TargetSerializationId = targetViewModel.SerializationId,
								DockSide = ToolItemDockSide.Top
							}
						];
					}
				}
			}

			return base.GetDefaultLocations(state);
		}
		
	}

}
