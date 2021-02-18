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
	public class ClassViewToolItemViewModel : ToolItemViewModel {

		private IRegionManager regionManager;
		
		public const string SerializationIdText = "ClassViewToolWindow";

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="ClassViewToolItemViewModel"/> class.
		/// </summary>
		/// <param name="regionManager">The region manager.</param>
		public ClassViewToolItemViewModel(IRegionManager regionManager) {
			this.regionManager = regionManager;

			this.DefaultDockSide = ToolItemDockSide.Right;
			this.ImageSource = new BitmapImage(new Uri("/Resources/Images/ClassView16.png", UriKind.Relative));
			this.SerializationId = SerializationIdText;
			this.Title = "Class View";
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
					var targetViewModel = region.Views.OfType<ToolItemViewModel>().FirstOrDefault(vm => vm.SerializationId == SolutionExplorerToolItemViewModel.SerializationIdText);
					if (targetViewModel != null) {
						// Dock below the Solution Explorer
						return new ToolItemDefaultLocation[] {
							new ToolItemDefaultLocation() {
								TargetSerializationId = targetViewModel.SerializationId,
								DockSide = ToolItemDockSide.Bottom
							}
						};
					}
				}
			}

			return base.GetDefaultLocations(state);
		}
		
	}

}
