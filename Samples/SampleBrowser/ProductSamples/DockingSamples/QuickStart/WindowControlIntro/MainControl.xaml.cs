using ActiproSoftware.Extensions;
using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Controls.Docking;
using ActiproSoftware.Windows.Extensions;
using System.Windows.Media.Animation;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.WindowControlIntro;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="IsLocationSizeEventOutputEnabled"/> property.
	/// </summary>
	public static readonly DependencyProperty IsLocationSizeEventOutputEnabledProperty
		= DependencyProperty.Register(nameof(IsLocationSizeEventOutputEnabled), typeof(bool), typeof(MainControl), new PropertyMetadata(defaultValue: false));

	#endregion

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
	/// Appends a message to the events <see cref="ListBox"/>.
	/// </summary>
	/// <param name="text">The text to append.</param>
	private void AppendMessage(string text) {
		var item = new ListBoxItem {
			Content = text
		};
		eventsListBox.Items.Add(item);
		eventsListBox.SelectedItem = item;
		eventsListBox.ScrollIntoView(item);
	}

	/// <summary>
	/// Indicates whether location/size event output is enabled.
	/// </summary>
	public bool IsLocationSizeEventOutputEnabled {
		get => (bool)GetValue(IsLocationSizeEventOutputEnabledProperty);
		set => SetValue(IsLocationSizeEventOutputEnabledProperty, value);
	}

	/// <summary>
	/// Occurs when a docking-related context menu is opening, allowing for customization before it is displayed.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnDockSiteMenuOpening(object sender, DockingMenuEventArgs e) {
		var sb = new StringBuilder(string.Format("WindowContextMenu: Kind={0}", e.Kind));

		if ((e.Window is { } window) && (e.Menu is { } menu)) {
			sb.AppendFormat(", Title={0} ", window.Title);

			if (window == outputToolWindow) {
				menu.Items.Add(new Separator());

				var menuItem = new MenuItem() { Header = "Location/Size Events", IsCheckable = true };
				menuItem.BindToProperty(MenuItem.IsCheckedProperty, this, nameof(IsLocationSizeEventOutputEnabled), BindingMode.TwoWay);
				menu.Items.Add(menuItem);
			}
		}

		AppendMessage(sb.ToString());
	}

	/// <summary>
	/// Occurs when a <c>Button</c> is clicked.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnOpenWindowButtonClick(object sender, RoutedEventArgs e) {
		if (window.Visibility != Visibility.Visible) {
			var swoopIn = (Storyboard)FindResource("SwoopIn");
			swoopIn.Begin(window);
			window.Show();
		}
		window.Activate();
	}

	/// <summary>
	/// Occurs when the <c>Activated</c> event is raised.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnWindowActivated(object sender, RoutedEventArgs e)
		=> AppendMessage("Activated");

	/// <summary>
	/// Occurs when the <c>Closed</c> event is raised.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnWindowClosed(object sender, RoutedEventArgs e) {
		AppendMessage("Closed");
		window.Visibility = Visibility.Collapsed;
	}

	/// <summary>
	/// Occurs when the <c>Closing</c> event is raised.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnWindowClosing(object sender, CancelRoutedEventArgs e)
		=> AppendMessage("Closing");

	/// <summary>
	/// Occurs when the <c>Deactivated</c> event is raised.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnWindowDeactivated(object sender, RoutedEventArgs e)
		=> AppendMessage("Deactivated");

	/// <summary>
	/// Occurs when the <c>DragMoved</c> event is raised.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnWindowDragMoved(object sender, RoutedEventArgs e)
		=> AppendMessage("DragMoved");

	/// <summary>
	/// Occurs when the <c>DragMoving</c> event is raised.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnWindowDragMoving(object sender, CancelRoutedEventArgs e)
		=> AppendMessage("DragMoving");

	/// <summary>
	/// Occurs when the <c>LocationChanged</c> event is raised.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnWindowLocationChanged(object sender, RoutedEventArgs e) {
		if (IsLocationSizeEventOutputEnabled)
			AppendMessage(string.Format("LocationChanged: {0},{1}", window.Left.Round(), window.Top.Round()));
	}

	/// <summary>
	/// Occurs when the <c>Opened</c> event is raised.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnWindowOpened(object sender, RoutedEventArgs e) {
		AppendMessage("Opened");
		window.Visibility = Visibility.Visible;
	}

	/// <summary>
	/// Occurs when the <c>SizeChanged</c> event is raised.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnWindowSizeChanged(object sender, RoutedEventArgs e) {
		if (IsLocationSizeEventOutputEnabled)
			AppendMessage(string.Format("SizeChanged: {0},{1}", window.ActualWidth.Round(), window.ActualHeight.Round()));
	}

	/// <summary>
	/// Occurs when the <c>StateChanged</c> event is raised.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnWindowStateChanged(object sender, RoutedEventArgs e)
		=> AppendMessage(string.Format("StateChanged: {0}", window.WindowState));

	/// <summary>
	/// Occurs when the <c>TitleBarContextMenuOpening</c> event is raised.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnWindowTitleBarMenuOpening(object sender, DockingMenuEventArgs e)
		=> AppendMessage("TitleBarMenuOpening");

	/// <summary>
	/// Occurs when the <c>TitleBarDoubleTapped</c> event is raised.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnWindowTitleBarDoubleTapped(object sender, CancelRoutedEventArgs e)
		=> AppendMessage("TitleBarDoubleTapped");

}
