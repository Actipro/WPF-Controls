using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Docking;
using ActiproSoftware.Windows.PrismIntegration.ViewModels;
using Prism.Regions;
using Prism.Regions.Behaviors;
using System.Collections.Specialized;
using System.Windows.Threading;

namespace ActiproSoftware.Windows.PrismIntegration.Regions.Behaviors;

/// <summary>
/// Defines the attached behavior that keeps the items of the <see cref="DockSite"/> control in synchronization with the <see cref="IRegion"/>.
/// </summary>
/// <remarks>
/// This behavior also makes sure that if a view is in the region is activated, then it's associated window in the <see cref="DockSite"/> is activated.
/// In addition, if you activate a window in the <see cref="DockSite"/>, then it's associated view is activated in the region.
/// </remarks>
public class DockSiteRegionBehavior : RegionBehavior, IHostAwareRegionBehavior {

	/// <summary>
	/// The name that identifies the <see cref="DockSiteRegionBehavior"/> behavior in a collection of regions behaviors.
	/// </summary>
	public static readonly string BehaviorKey = nameof(DockSiteRegionBehavior);

	// --------------------------------------------------------------------------------------------------
	// INTERFACE IMPLEMENTATION
	// --------------------------------------------------------------------------------------------------

	DependencyObject? IHostAwareRegionBehavior.HostControl {
		get => DockSite;
		set => DockSite = value as DockSite;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Ensures the <see cref="DockingWindow"/> in the event args exists and is a generated container for an item in this region.
	/// </summary>
	/// <param name="e">The <see cref="DockingWindowEventArgs"/> to examine.</param>
	/// <returns>
	/// <c>true</c> if the <see cref="DockingWindow"/> in the event args is a valid container; otherwise, <c>false</c>.
	/// </returns>
	private bool IsDockingWindowAValidContainer(DockingWindowEventArgs e) {
		return e.Window is { IsContainerForItem: true }
			&& DockSite is { } dockSite
			&& e.OriginalSource == dockSite;
	}

	/// <summary>
	/// Occurs after a <see cref="DockingWindow"/> has been activated.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnDockSiteWindowActivated(object? sender, DockingWindowEventArgs e) {
		if (
			IsDockingWindowAValidContainer(e)
			&& e.Window is { } window
			&& Region is { } region
		) {
			// Deactivate all inactive views
			foreach (var activeView in region.ActiveViews) {
				if ((activeView != window) && (activeView != window.DataContext))
					region.Deactivate(activeView);
			}

			// Ensure the view is flagged active
			if (region.Views.Contains(window))
				region.Activate(window);
			else if (region.Views.Contains(window.DataContext))
				region.Activate(window.DataContext);
		}
	}

	/// <summary>
	/// Occurs after a <see cref="DockingWindow"/> has been deactivated.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnDockSiteWindowDeactivated(object? sender, DockingWindowEventArgs e) {
		if (
			IsDockingWindowAValidContainer(e)
			&& e.Window is { } window
			&& Region is { } region
		) {
			// Ensure the view is flagged inactive
			if (region.Views.Contains(window))
				region.Deactivate(window);
			else if (region.Views.Contains(window.DataContext))
				region.Deactivate(window.DataContext);
		}
	}

	/// <summary>
	/// Occurs when a docking window's default location is requested.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnDockSiteWindowDefaultLocationRequested(object? sender, DockingWindowDefaultLocationEventArgs e) {
		if (
			DockSite is { } dockSite
			&& IsDockingWindowAValidContainer(e)
			&& Region is { } region
			&& e.Window is { DataContext: ToolItemViewModel viewModel } window
		) {
			// Get the dock state
			var dockState = (ToolItemState)new ToolItemStateConverter().Convert(e.State, typeof(ToolItemState), parameter: null, culture: null)!;

			// Query the tool view-model for default locations relative to other tool view-models
			var defaultLocations = viewModel.GetDefaultLocations(dockState);
			if (defaultLocations is not null) {
				foreach (var defaultLocation in defaultLocations) {
					if (defaultLocation?.TargetSerializationId is { Length: > 0 } targetSerializationId) {
						var targetViewModel = region.Views.OfType<ToolItemViewModel>().FirstOrDefault(vm => vm.SerializationId == targetSerializationId);
						if (targetViewModel is { IsOpen: true }) {
							// Another open tool view-model was located
							var targetWindow = dockSite.ContainerFromItem(targetViewModel);
							if (targetWindow is not null) {
								e.Target = targetWindow;
								e.Side = (Side)new ToolItemDockSideConverter().Convert(defaultLocation.DockSide, typeof(Side), parameter: null, culture: null)!;
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
	/// <param name="e">The event data.</param>
	private void OnDockSiteWindowRegistered(object? sender, DockingWindowEventArgs e) {
		if (IsDockingWindowAValidContainer(e)) {
			// Bind to the view model and open the window
			if (e.Window?.DataContext is DockingItemViewModelBase viewModel)
				e.Window.PrepareContainerBindings(viewModel);
		}
	}

	/// <summary>
	/// Occurs when a docking window is unregistered.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnDockSiteWindowUnregistered(object? sender, DockingWindowEventArgs e) {
		if (
			IsDockingWindowAValidContainer(e)
			&& e.Window is { } window
		) {
			// Clear the view model bindings
			window.ClearContainerBindings();

			// Ensure the view is removed from the region
			if (Region is { } region) {
				if (region.Views.Contains(window))
					region.Remove(window);
				else if (region.Views.Contains(window.DataContext))
					region.Remove(window.DataContext);
			}
		}
	}

	/// <summary>
	/// Occurs when the <c>Region.ActiveViews</c> collection changes.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
	private void OnRegionActiveViewsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		if (
			DockSite is { } dockSite
			&& e.NewItems is not null
		) {
			foreach (var item in e.NewItems) {
				// Get the docking window container for the item and quit if it's already active
				var window = dockSite.ContainerFromItem(item);
				if ((window is null) || (window.IsActive))
					continue;

				// Activate the window
				window.Activate(e.NewItems.Count == 1);
			}
		}
	}

	/// <summary>
	/// Occurs when the <c>Region.Views</c> collection changes.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
	private void OnRegionViewsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		if (
			DockSite is { } dockSite
			&& e.NewItems is not null
		) {
			// This code needs to be dispatched since the Region.Views is updated in the midst of the DockSite items source 
			//   properties being updated when initialized
			dockSite.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, () => {
				foreach (var item in e.NewItems) {
					// Get the docking window container for the item
					var window = dockSite.ContainerFromItem(item);
					if (window is null)
						continue;

					// Open the docking window in its default location
					window.IsOpen = true;
				}
			});
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="DockSite"/> that the <see cref="IRegion"/> is attached to.
	/// </summary>
	public DockSite? DockSite { get; set; }

	/// <summary>
	/// Performs logic after the behavior has been attached.
	/// </summary>
	protected override void OnAttach() {
		if (Region is { } region) {
			region.ActiveViews.CollectionChanged += OnRegionActiveViewsCollectionChanged;
			region.Views.CollectionChanged += OnRegionViewsCollectionChanged;
		}

		if (DockSite is { } dockSite) {
			dockSite.WindowActivated += OnDockSiteWindowActivated;
			dockSite.WindowDeactivated += OnDockSiteWindowDeactivated;
			dockSite.WindowDefaultLocationRequested += OnDockSiteWindowDefaultLocationRequested;
			dockSite.WindowRegistered += OnDockSiteWindowRegistered;
			dockSite.WindowUnregistered += OnDockSiteWindowUnregistered;
		}
	}

}
