using System;
using System.Windows;
using System.Windows.Media;
using ActiproSoftware.Windows.Controls.Docking;
using ActiproSoftware.Windows.Media;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.ProgrammaticSizing {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the <c>Layout.EvenlyDistribute</c> menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnLayoutEvenlyDistributeMenuItemClick(object sender, RoutedEventArgs e) {
			var descendants = VisualTreeHelperExtended.GetAllDescendants<SplitContainer>(dockSite);
			if (descendants != null) {
				foreach (var splitContainer in descendants)
					splitContainer.ResizeSlots();
			}
		}
		
		/// <summary>
		/// Occurs when the <c>Layout.EvenlyDistributeDocumentsOnly</c> menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnLayoutEvenlyDistributeDocumentsOnlyMenuItemClick(object sender, RoutedEventArgs e) {
			var workspace = dockSite.PrimaryDockHost.Workspace;
			if (workspace != null) {
				var descendants = VisualTreeHelperExtended.GetAllDescendants<SplitContainer>(workspace);
				if (descendants != null) {
					foreach (var splitContainer in descendants)
						splitContainer.ResizeSlots();
				}
			}
		}
		
		/// <summary>
		/// Occurs when the <c>Layout.EvenlyDistributeFavorFocused</c> menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnLayoutEvenlyDistributeFavorFocusedMenuItemClick(object sender, RoutedEventArgs e) {
			var descendants = VisualTreeHelperExtended.GetAllDescendants<SplitContainer>(dockSite);
			if (descendants != null) {
				foreach (var splitContainer in descendants) {
					// Look for SplitContainers that contain the focused element, and increase the ratios for that slot
					var visualCount = splitContainer.Children.Count;
					var desiredRatios = new double[visualCount];
					for (int i = 0, visibleChildCount = 0; i < visualCount; i++) {
						// Default ratio, must also ensure that we don't pass a ratio that is less than or equal to 0
						desiredRatios[i] = 1;

						// Get the child and verify that it is visible
						var child = splitContainer.Children[i] as FrameworkElement;
						if ((child != null) && (child.Visibility == Visibility.Visible)) {
							// If the child has the keyboard focus, then increase it's ratio
							if (child.IsKeyboardFocusWithin)
								desiredRatios[visibleChildCount] = 3;
							visibleChildCount++;
						}
					}

					splitContainer.ResizeSlots(desiredRatios);
				}
			}
		}
		
		/// <summary>
		/// Occurs when the <c>Layout.EvenlyDistributeFavorWorkspace</c> menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnLayoutEvenlyDistributeFavorWorkspaceMenuItemClick(object sender, RoutedEventArgs e) {
			var descendants = VisualTreeHelperExtended.GetAllDescendants<SplitContainer>(dockSite);
			if (descendants != null) {
				foreach (var splitContainer in descendants) {
					// Look for SplitContainers that contain the Workspace, and increase the ratios for that slot
					var visualCount = splitContainer.Children.Count;
					var desiredRatios = new double[visualCount];
					for (int i = 0, visibleChildCount = 0; i < visualCount; i++) {
						// Default ratio, must also ensure that we don't pass a ratio that is less than or equal to 0
						desiredRatios[i] = 1;

						// Get the child and verify that it is visible
						var child = splitContainer.Children[i] as FrameworkElement;
						if ((child != null) && (child.Visibility == Visibility.Visible)) {
							// If the child is a Workspace, or contains the Workspace, then increase it's ratio
							if ((child is Workspace) || (VisualTreeHelperExtended.GetFirstDescendant(child, typeof(Workspace)) != null))
								desiredRatios[visibleChildCount] = 3;
							visibleChildCount++;
						}
					}

					splitContainer.ResizeSlots(desiredRatios);
				}
			}
		}

		/// <summary>
		/// Occurs when the <c>Layout.EvenlyDistributeToolsOnly</c> menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnLayoutEvenlyDistributeToolsOnlyMenuItemClick(object sender, RoutedEventArgs e) {
			var descendants = VisualTreeHelperExtended.GetAllDescendants<SplitContainer>(dockSite);
			if (descendants != null) {
				foreach (var splitContainer in descendants) {
					if (VisualTreeHelperExtended.GetAncestor<Workspace>(splitContainer) != null)
						continue;

					splitContainer.ResizeSlots();
				}
			}
		}

		/// <summary>
		/// Occurs when the <c>Layout.RandomlyDistribute</c> menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnLayoutRandomlyDistributeMenuItemClick(object sender, RoutedEventArgs e) {
			var descendants = VisualTreeHelperExtended.GetAllDescendants<SplitContainer>(dockSite);
			if (descendants != null) {
				var random = new Random();
				foreach (var splitContainer in descendants) {
					splitContainer.ResizeSlots(
						random.NextDouble() * 8 + 1,
						random.NextDouble() * 6 + 1,
						random.NextDouble() * 4 + 1,
						random.NextDouble() * 2 + 1);
				}
			}
		}
		
		/// <summary>
		/// Occurs when the <c>Layout.ReverseAll</c> menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnLayoutReverseAllMenuItemClick(object sender, RoutedEventArgs e) {
			var descendants = VisualTreeHelperExtended.GetAllDescendants<SplitContainer>(dockSite);
			if (descendants != null) {
				foreach (var splitContainer in descendants)
					splitContainer.ReverseSlots();
			}
		}

	}

}
