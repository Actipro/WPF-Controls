using System;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using Prism.Regions;
using Prism.Regions.Behaviors;
using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Docking;
using ActiproSoftware.Windows.PrismIntegration.ViewModels;

namespace ActiproSoftware.Windows.PrismIntegration.Regions.Behaviors {

	/// <summary>
	/// Defines the attached behavior that keeps the items of the <see cref="DockSite"/> control in synchronization with the <see cref="IRegion"/>.
	/// </summary>
	/// <remarks>
	/// This behavior also makes sure that if a view is in the region is activated, then it's associated window in the <see cref="DockSite"/> is activated.
	/// In addition, if you activate a window in the <see cref="DockSite"/>, then it's associated view is activated in the region.
	/// </remarks>
	public class DockSiteRegionBehavior : RegionBehavior, IHostAwareRegionBehavior {

		private DockSite dockSite;
		
		/// <summary>
		/// Gets the name that identifies the <see cref="DockSiteRegionBehavior"/> behavior in a collection of regions behaviors.
		/// </summary>
		public static readonly string BehaviorKey = "DockSiteRegionBehavior";

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the <see cref="DockSite"/> that the <see cref="IRegion"/> is attached to.
		/// </summary>
		/// <value>A <see cref="DockSite"/> that the <see cref="IRegion"/> is attached to.</value>
		DependencyObject IHostAwareRegionBehavior.HostControl {
			get {
				return dockSite;
			}
			set {
				dockSite = value as DockSite;
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Ensures the <see cref="DockingWindow"/> in the event args exists and is a generated container for an item in this region.
		/// </summary>
		/// <param name="e">The <see cref="DockingWindowEventArgs"/> to examine.</param>
		/// <returns>
		/// <c>true</c> if the <see cref="DockingWindow"/> in the event args is a valid container; otherwise, <c>false</c>.
		/// </returns>
		private bool IsDockingWindowAValidContainer(DockingWindowEventArgs e) {
			return ((e != null) && (e.Window != null) && (e.Window.IsContainerForItem) && (dockSite != null) && (e.OriginalSource == dockSite));
		}

		/// <summary>
		/// Occurs after a <see cref="DockingWindow"/> has been activated.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="DockingWindowEventArgs"/> that contains the event data.</param>
		private void OnDockSiteWindowActivated(object sender, DockingWindowEventArgs e) {
			if (!this.IsDockingWindowAValidContainer(e))
				return;

			// Get the region
			var region = this.Region;
			if (region == null)
				return;

			// Deactivate all inactive views
			foreach (var activeView in region.ActiveViews) {
				if ((e.Window != activeView) && (activeView != e.Window.DataContext))
					region.Deactivate(activeView);
			}

			// Ensure the view is flagged active
			if (region.Views.Contains(e.Window))
				region.Activate(e.Window);
			else if (region.Views.Contains(e.Window.DataContext))
				region.Activate(e.Window.DataContext);
		}

		/// <summary>
		/// Occurs after a <see cref="DockingWindow"/> has been deactivated.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="DockingWindowEventArgs"/> that contains the event data.</param>
		private void OnDockSiteWindowDeactivated(object sender, DockingWindowEventArgs e) {
			if (!this.IsDockingWindowAValidContainer(e))
				return;

			// Get the region
			var region = this.Region;
			if (region == null)
				return;

			// Ensure the view is flagged inactive
			if (region.Views.Contains(e.Window))
				region.Deactivate(e.Window);
			else if (region.Views.Contains(e.Window.DataContext))
				region.Deactivate(e.Window.DataContext);
		}
		
		/// <summary>
		/// Occurs when a docking window's default location is requested.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowDefaultLocationEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteWindowDefaultLocationRequested(object sender, DockingWindowDefaultLocationEventArgs e) {
			if (!this.IsDockingWindowAValidContainer(e))
				return;

			// Get the region
			var region = this.Region;
			if (region == null)
				return;

			var viewModel = e.Window.DataContext as ToolItemViewModel;
			if (viewModel != null) {
				// Get the dock state
				var dockState = (ToolItemState)new ToolItemStateConverter().Convert(e.State, typeof(ToolItemState), null, null);

				// Query the tool view-model for default locations relative to other tool view-models
				var defaultLocations = viewModel.GetDefaultLocations(dockState);
				if (defaultLocations != null) {
					foreach (var defaultLocation in defaultLocations) {
						if ((defaultLocation != null) && (!string.IsNullOrEmpty(defaultLocation.TargetSerializationId))) {
							var targetViewModel = region.Views.OfType<ToolItemViewModel>().FirstOrDefault(vm => vm.SerializationId == defaultLocation.TargetSerializationId);
							if ((targetViewModel != null) && (targetViewModel.IsOpen)) {
								// Another open tool view-model was located
								var targetWindow = dockSite.ContainerFromItem(targetViewModel);
								if (targetWindow != null) {
									e.Target = targetWindow;
									e.Side = (Side)new ToolItemDockSideConverter().ConvertBack(defaultLocation.DockSide, typeof(Side), null, null);
									return;
								}
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Occurs when a docking window is registered.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteWindowRegistered(object sender, DockingWindowEventArgs e) {
			if (!this.IsDockingWindowAValidContainer(e))
				return;

			// Bind to the view model and open the window
			var viewModel = e.Window.DataContext as DockingItemViewModelBase;
			if (viewModel != null)
				e.Window.PrepareContainerBindings(viewModel);
		}

		/// <summary>
		/// Occurs when a docking window is unregistered.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteWindowUnregistered(object sender, DockingWindowEventArgs e) {
			if (!this.IsDockingWindowAValidContainer(e))
				return;

			// Clear the view model bindings
			e.Window.ClearContainerBindings();

			// Ensure the view is removed from the region
			var region = this.Region;
			if (region != null) {
				if (region.Views.Contains(e.Window))
					region.Remove(e.Window);
				else if (region.Views.Contains(e.Window.DataContext))
					region.Remove(e.Window.DataContext);
			}
		}

		/// <summary>
		/// Occurs when the <c>Region.ActiveViews</c> collection changes.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
		private void OnRegionActiveViewsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
			if ((dockSite == null) || (e == null) || (e.NewItems == null))
				return;

			foreach (var item in e.NewItems) {
				// Get the docking window container for the item and quit if it's already active
				var window = dockSite.ContainerFromItem(item);
				if ((window == null) || (window.IsActive))
					continue;

				// Activate the window
				window.Activate(e.NewItems.Count == 1);
			}
		}

		/// <summary>
		/// Occurs when the <c>Region.Views</c> collection changes.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
		private void OnRegionViewsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
			if ((dockSite == null) || (e == null) || (e.NewItems == null))
				return;

			// This code needs to be dispatched since the Region.Views is updated in the midst of the DockSite items source 
			//   properties being updated when initialized
			dockSite.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, (Action)(() => {
				foreach (var item in e.NewItems) {
					// Get the docking window container for the item
					var window = dockSite.ContainerFromItem(item);
					if (window == null)
						continue;

					// Open the docking window in its default location
					window.IsOpen = true;
				}
			}));
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the <see cref="DockSite"/> that the <see cref="IRegion"/> is attached to.
		/// </summary>
		/// <value>A <see cref="DockSite"/> that the <see cref="IRegion"/> is attached to.</value>
		public DockSite DockSite {
			get {
				return dockSite;
			}
			set {
				dockSite = value;
			}
		}

		/// <summary>
		/// Performs logic after the behavior has been attached.
		/// </summary>
		protected override void OnAttach() {
			var region = this.Region;
			if (region != null) {
				region.ActiveViews.CollectionChanged += this.OnRegionActiveViewsCollectionChanged;
				region.Views.CollectionChanged += this.OnRegionViewsCollectionChanged;
			}

			if (dockSite != null) {
				dockSite.WindowActivated += this.OnDockSiteWindowActivated;
				dockSite.WindowDeactivated += this.OnDockSiteWindowDeactivated;
				dockSite.WindowDefaultLocationRequested += OnDockSiteWindowDefaultLocationRequested;
				dockSite.WindowRegistered += this.OnDockSiteWindowRegistered;
				dockSite.WindowUnregistered += this.OnDockSiteWindowUnregistered;
			}
		}

	}

}
