using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Animation;
using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Controls.Docking;
using System.Text;
using ActiproSoftware.Windows.Extensions;
using System.Windows.Data;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.WindowControlIntro {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {
		
		#region Dependency Properties
		
		/// <summary>
		/// Identifies the <see cref="IsLocationSizeEventOutputEnabled"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="IsLocationSizeEventOutputEnabled"/> dependency property.</value>
		public static readonly DependencyProperty IsLocationSizeEventOutputEnabledProperty = DependencyProperty.Register("IsLocationSizeEventOutputEnabled", typeof(bool), typeof(MainControl), new PropertyMetadata(false));
		
		#endregion

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
		/// Appends a message to the events <see cref="ListBox"/>.
		/// </summary>
		/// <param name="text">The text to append.</param>
		private void AppendMessage(string text) {
			var item = new ListBoxItem();
			item.Content = text;
			eventsListBox.Items.Add(item);
			eventsListBox.SelectedItem = item;
			eventsListBox.ScrollIntoView(item);
		}
		
		/// <summary>
		/// Gets or sets whether location/size event output is enabled.
		/// </summary>
		/// <value>
		/// <c>true</c> if location/size event output is enabled; otherwise, <c>false</c>.
		/// </value>
		public bool IsLocationSizeEventOutputEnabled {
			get {
				return (bool)this.GetValue(IsLocationSizeEventOutputEnabledProperty);
			}
			set {
				this.SetValue(IsLocationSizeEventOutputEnabledProperty, value);
			}
		}
		
		/// <summary>
		/// Occurs when a docking-related context menu is opening, allowing for customization before it is displayed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingMenuEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteMenuOpening(object sender, DockingMenuEventArgs e) {
			var sb = new StringBuilder(String.Format("WindowContextMenu: Kind={0}", e.Kind));

			if (e.Window != null) {
				sb.AppendFormat(", Title={0} ", e.Window.Title);

				if (e.Window == outputToolWindow) {
					e.Menu.Items.Add(new Separator());

					var menuItem = new MenuItem() { Header = "Location/Size Events", IsCheckable = true };
					menuItem.BindToProperty(MenuItem.IsCheckedProperty, this, "IsLocationSizeEventOutputEnabled", BindingMode.TwoWay);
					e.Menu.Items.Add(menuItem);
				}
			}

			this.AppendMessage(sb.ToString());
		}

		/// <summary>
		/// Occurs when a <c>Button</c> is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnOpenWindowButtonClick(object sender, RoutedEventArgs e) {
			if (window.Visibility != Visibility.Visible) {
				Storyboard swoopIn = (Storyboard)this.FindResource("SwoopIn");
				swoopIn.Begin(window);
				window.Show();
			}
			window.Activate();
		}
		
		/// <summary>
		/// Occurs when the <c>Activated</c> event occurs.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnWindowActivated(object sender, RoutedEventArgs e) {
			this.AppendMessage("Activated");
		}

		/// <summary>
		/// Occurs when the <c>Closed</c> event occurs.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnWindowClosed(object sender, RoutedEventArgs e) {
			this.AppendMessage("Closed");
			window.Visibility = Visibility.Collapsed;
		}

		/// <summary>
		/// Occurs when the <c>Closing</c> event occurs.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="CancelRoutedEventArgs"/> that contains the event data.</param>
		private void OnWindowClosing(object sender, CancelRoutedEventArgs e) {
			this.AppendMessage("Closing");
		}

		/// <summary>
		/// Occurs when the <c>Deactivated</c> event occurs.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnWindowDeactivated(object sender, RoutedEventArgs e) {
			this.AppendMessage("Deactivated");
		}
		
		/// <summary>
		/// Occurs when the <c>DragMoved</c> event occurs.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnWindowDragMoved(object sender, RoutedEventArgs e) {
			this.AppendMessage("DragMoved");
		}

		/// <summary>
		/// Occurs when the <c>DragMoving</c> event occurs.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="CancelRoutedEventArgs"/> that contains the event data.</param>
		private void OnWindowDragMoving(object sender, CancelRoutedEventArgs e) {
			this.AppendMessage("DragMoving");
		}

		/// <summary>
		/// Occurs when the <c>LocationChanged</c> event occurs.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnWindowLocationChanged(object sender, RoutedEventArgs e) {
			if (this.IsLocationSizeEventOutputEnabled)
				this.AppendMessage(String.Format("LocationChanged: {0},{1}", Math.Round(window.Left), Math.Round(window.Top)));
		}

		/// <summary>
		/// Occurs when the <c>Opened</c> event occurs.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnWindowOpened(object sender, RoutedEventArgs e) {
			this.AppendMessage("Opened");
			window.Visibility = Visibility.Visible;
		}

		/// <summary>
		/// Occurs when the <c>SizeChanged</c> event occurs.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnWindowSizeChanged(object sender, RoutedEventArgs e) {
			if (this.IsLocationSizeEventOutputEnabled)
				this.AppendMessage(String.Format("SizeChanged: {0},{1}", Math.Round(window.ActualWidth), Math.Round(window.ActualHeight)));
		}

		/// <summary>
		/// Occurs when the <c>StateChanged</c> event occurs.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnWindowStateChanged(object sender, RoutedEventArgs e) {
			this.AppendMessage(String.Format("StateChanged: {0}", window.WindowState));
		}

		/// <summary>
		/// Occurs when the <c>TitleBarContextMenuOpening</c> event occurs.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="DockingMenuEventArgs"/> that contains the event data.</param>
		private void OnWindowTitleBarMenuOpening(object sender, DockingMenuEventArgs e) {
			this.AppendMessage("TitleBarMenuOpening");
		}

		/// <summary>
		/// Occurs when the <c>TitleBarDoubleTapped</c> event occurs.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="CancelRoutedEventArgs"/> that contains the event data.</param>
		private void OnWindowTitleBarDoubleTapped(object sender, CancelRoutedEventArgs e) {
			this.AppendMessage("TitleBarDoubleTapped");
		}

	}

}
