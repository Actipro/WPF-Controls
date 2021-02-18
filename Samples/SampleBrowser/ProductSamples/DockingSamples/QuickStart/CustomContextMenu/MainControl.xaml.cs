using System;
using System.Text;
using System.Windows.Controls;
using ActiproSoftware.Windows.Controls.Docking;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.CustomContextMenu {

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
		/// Occurs when a docking-related context menu is opening, allowing for customization before it is displayed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingMenuEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteMenuOpening(object sender, DockingMenuEventArgs e) {
			var sb = new StringBuilder(String.Format("WindowContextMenu: Kind={0}", e.Kind));

			if (e.Window != null) {
				sb.AppendFormat(", Title={0} ", e.Window.Title);

				if (e.Window == customizedDocumentWindow) {
					e.Menu.Items.Add(new Separator());

					var menuItem = new MenuItem() { Header = "Custom Menu Item Added at " + DateTime.Now.ToShortTimeString() };
					e.Menu.Items.Add(menuItem);
				}
				else if (e.Window == customizedToolWindow) {
					e.Menu.Items.Clear();

					var menuItem = new MenuItem() { Header = "Custom Menu Item Added at " + DateTime.Now.ToShortTimeString() };
					e.Menu.Items.Add(menuItem);
				}
			}

			this.AppendMessage(sb.ToString());
		}

	}

}
