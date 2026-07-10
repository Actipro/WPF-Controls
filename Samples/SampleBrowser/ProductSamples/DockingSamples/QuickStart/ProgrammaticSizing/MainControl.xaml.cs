using ActiproSoftware.Windows.Controls.Docking;
using ActiproSoftware.Windows.Extensions;
using ActiproSoftware.Windows.Media;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.ProgrammaticSizing;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Occurs when the <c>Layout.EvenlyDistribute</c> menu item is clicked.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnLayoutEvenlyDistributeMenuItemClick(object sender, RoutedEventArgs e) {
		foreach (var splitContainer in dockSite.GetVisualDescendants().OfType<SplitContainer>().ToArray())
			splitContainer.ResizeSlots();
	}

	/// <summary>
	/// Occurs when the <c>Layout.EvenlyDistributeDocumentsOnly</c> menu item is clicked.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnLayoutEvenlyDistributeDocumentsOnlyMenuItemClick(object sender, RoutedEventArgs e) {
		var workspace = dockSite.PrimaryDockHost?.Workspace;
		if (workspace is not null) {
			foreach (var splitContainer in workspace.GetVisualDescendants().OfType<SplitContainer>().ToArray())
				splitContainer.ResizeSlots();
		}
	}

	/// <summary>
	/// Occurs when the <c>Layout.EvenlyDistributeFavorFocused</c> menu item is clicked.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnLayoutEvenlyDistributeFavorFocusedMenuItemClick(object sender, RoutedEventArgs e) {
		foreach (var splitContainer in dockSite.GetVisualDescendants().OfType<SplitContainer>().ToArray()) {
			// Look for SplitContainers that contain the focused element, and increase the ratios for that slot
			var visualCount = splitContainer.Children.Count;
			var desiredRatios = new double[visualCount];
			for (int i = 0, visibleChildCount = 0; i < visualCount; i++) {
				// Default ratio, must also ensure that we don't pass a ratio that is less than or equal to 0
				desiredRatios[i] = 1;

				// Get the child and verify that it is visible
				var child = splitContainer.Children[i];
				if (child?.Visibility == Visibility.Visible) {
					// If the child has the keyboard focus, then increase it's ratio
					if (child.IsKeyboardFocusWithin)
						desiredRatios[visibleChildCount] = 3;
					visibleChildCount++;
				}
			}

			splitContainer.ResizeSlots(desiredRatios);
		}
	}

	/// <summary>
	/// Occurs when the <c>Layout.EvenlyDistributeFavorWorkspace</c> menu item is clicked.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnLayoutEvenlyDistributeFavorWorkspaceMenuItemClick(object sender, RoutedEventArgs e) {
		foreach (var splitContainer in dockSite.GetVisualDescendants().OfType<SplitContainer>().ToArray()) {
			// Look for SplitContainers that contain the Workspace, and increase the ratios for that slot
			var visualCount = splitContainer.Children.Count;
			var desiredRatios = new double[visualCount];
			for (int i = 0, visibleChildCount = 0; i < visualCount; i++) {
				// Default ratio, must also ensure that we don't pass a ratio that is less than or equal to 0
				desiredRatios[i] = 1;

				// Get the child and verify that it is visible
				var child = splitContainer.Children[i];
				if (child?.Visibility == Visibility.Visible) {
					// If the child is a Workspace, or contains the Workspace, then increase it's ratio
					if (child.FindDescendantOfType<Workspace>(includeSelf: true) is not null)
						desiredRatios[visibleChildCount] = 3;
					visibleChildCount++;
				}
			}

			splitContainer.ResizeSlots(desiredRatios);
		}
	}

	/// <summary>
	/// Occurs when the <c>Layout.EvenlyDistributeToolsOnly</c> menu item is clicked.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnLayoutEvenlyDistributeToolsOnlyMenuItemClick(object sender, RoutedEventArgs e) {
		foreach (var splitContainer in dockSite.GetVisualDescendants().OfType<SplitContainer>().ToArray()) {
			if (splitContainer.FindAncestorOfType<Workspace>() is not null)
				continue;

			splitContainer.ResizeSlots();
		}
	}

	/// <summary>
	/// Occurs when the <c>Layout.RandomlyDistribute</c> menu item is clicked.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnLayoutRandomlyDistributeMenuItemClick(object sender, RoutedEventArgs e) {
		var random = new Random();
		foreach (var splitContainer in dockSite.GetVisualDescendants().OfType<SplitContainer>().ToArray()) {
			splitContainer.ResizeSlots(
				random.NextDouble() * 8 + 1,
				random.NextDouble() * 6 + 1,
				random.NextDouble() * 4 + 1,
				random.NextDouble() * 2 + 1
			);
		}
	}

	/// <summary>
	/// Occurs when the <c>Layout.ReverseAll</c> menu item is clicked.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnLayoutReverseAllMenuItemClick(object sender, RoutedEventArgs e) {
		foreach (var splitContainer in dockSite.GetVisualDescendants().OfType<SplitContainer>().ToArray())
			splitContainer.ReverseSlots();
	}

}
