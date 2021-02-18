using System.Collections.Generic;
using Prism.Regions;

namespace ActiproSoftware.Windows.PrismIntegration.ViewModels {

	//
	// NOTE: The DefaultDockSide and State properties in this VM class return ToolItemDockSide and ToolItemState enums, 
	//       which allows for an abstraction layer between them and the Side/DockingWindowState enum values they represent... 
	//       This is useful in scenarios where you don't wish to have your models directly reference types in the Docking/MDI assembly... 
	//       If that is not a factor, there is nothing wrong with changing the properties to directly return the two Actipro types instead
	//

	/// <summary>
	/// Represents a tool item view-model.
	/// </summary>
	public class ToolItemViewModel : DockingItemViewModelBase {
		
		private ToolItemDockSide defaultDockSide = ToolItemDockSide.Right;
		private ToolItemState state = ToolItemState.Docked;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the default side that the tool window will dock towards when no prior location is known.
		/// </summary>
		/// <value>The default side that the tool window will dock towards when no prior location is known.</value>
		public ToolItemDockSide DefaultDockSide {
			get {
				return this.defaultDockSide;
			}
			set {
				if (this.defaultDockSide != value) {
					this.defaultDockSide = value;
					this.NotifyPropertyChanged("DefaultDockSide");
				}
			}
		}

		/// <summary>
		/// Returns the default locations for the specified state.
		/// </summary>
		/// <param name="state">The target state.</param>
		/// <returns>The default locations for the specified state.</returns>
		public virtual IEnumerable<ToolItemDefaultLocation> GetDefaultLocations(ToolItemState state) {
			return null;
		}
		
		/// <summary>
		/// Gets whether the container generated for this view model should be a tool window.
		/// </summary>
		/// <value>
		/// <c>true</c> if the container generated for this view model should be a tool window; otherwise, <c>false</c>.
		/// </value>
		public override bool IsTool {
			get {
				return true;
			}
		}
		
		/// <summary>
		/// Gets or sets the current state of the view.
		/// </summary>
		/// <value>The current state of the view.</value>
		public ToolItemState State {
			get {
				return this.state;
			}
			set {
				if (this.state != value) {
					this.state = value;
					this.NotifyPropertyChanged("State");
				}
			}
		}

	}

}
