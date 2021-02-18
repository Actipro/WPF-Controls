using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;
using Prism.Regions;

namespace ActiproSoftware.Windows.PrismIntegration.ViewModels {

	/// <summary>
	/// Represents a tool view-model for the sample.
	/// </summary>
	/// <remarks>
	/// This view-model derives from a base class that initializes the <c>ToolWindow</c> from instance properties.
	/// </remarks>
	public class SolutionExplorerToolItemViewModel : ToolItemViewModel {
		
		private IRegionManager regionManager;

		public const string SerializationIdText = "SolutionExplorerToolWindow";

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="SolutionExplorerToolItemViewModel"/> class.
		/// </summary>
		/// <param name="regionManager">The region manager.</param>
		public SolutionExplorerToolItemViewModel(IRegionManager regionManager) {
			this.regionManager = regionManager;

			this.DefaultDockSide = ToolItemDockSide.Right;
			this.ImageSource = new BitmapImage(new Uri("/Resources/Images/SolutionExplorer16.png", UriKind.Relative));
			this.SerializationId = SerializationIdText;
			this.Title = "Solution Explorer";
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Returns the default locations for the specified state.
		/// </summary>
		/// <param name="state">The target state.</param>
		/// <returns>The default locations for the specified state.</returns>
		public override IEnumerable<ToolItemDefaultLocation> GetDefaultLocations(ToolItemState state) {
			if (state == ToolItemState.Docked) {
				var region = regionManager.Regions[ShellViewModel.MainRegionName];
				if (region != null) {
					var targetViewModel = region.Views.OfType<ToolItemViewModel>().FirstOrDefault(vm => vm.SerializationId == ClassViewToolItemViewModel.SerializationIdText);
					if (targetViewModel != null) {
						// Dock above the Class Library
						return new ToolItemDefaultLocation[] {
							new ToolItemDefaultLocation() {
								TargetSerializationId = targetViewModel.SerializationId,
								DockSide = ToolItemDockSide.Top
							}
						};
					}
				}
			}

			return base.GetDefaultLocations(state);
		}
		
	}

}
