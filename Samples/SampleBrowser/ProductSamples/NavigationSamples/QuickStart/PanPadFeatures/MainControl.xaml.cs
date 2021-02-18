using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using ActiproSoftware.Windows.Controls.Navigation.Primitives;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.PanPadFeatures {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			this.panPad.Pan += new EventHandler<PanEventArgs>(this.OnPanPadPan);
		}

		#endregion // OBJECT

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Appends a message to the events <see cref="ListBox"/>.
		/// </summary>
		/// <param name="text">The text to append.</param>
		private void AppendMessage(string text) {
			if (null == this.eventsListBox)
				return;

			ListBoxItem item = new ListBoxItem() { Content = text };
			this.eventsListBox.Items.Add(item);
			this.eventsListBox.SelectedItem = item;
			this.eventsListBox.ScrollIntoView(item);
		}

		/// <summary>
		/// Handles the Click event of the clear Button control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnClearButtonClick(object sender, RoutedEventArgs e) {
			if (null != this.eventsListBox)
				this.eventsListBox.Items.Clear();
		}

		/// <summary>
		/// Handles the <c>Pan</c> event of the <c>panPad</c> control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="PanEventArgs"/> instance containing the event data.</param>
		private void OnPanPadPan(object sender, PanEventArgs e) {
			this.AppendMessage(string.Format("Pan X={0}, Y={1}", e.XOffset, e.YOffset));
		}

		#endregion // NON-PUBLIC PROCEDURES
	}
}