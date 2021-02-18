using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ActiproSoftware.Windows.Controls.Views;
using ActiproSoftware.Windows.Media;
using ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.Common;
using ActiproSoftware.Compatibility;

namespace ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.AnimatedDockPanelIntro {

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

			this.listBox.Items.Add(new ListBoxItem() { Style = this.Resources["CenterContentListBoxItemStyle"] as Style }); // Placeholder for center
			this.OnAddItemTopClick(null, null);
			this.OnAddItemBottomClick(null, null);
			this.OnAddItemLeftClick(null, null);
			this.OnAddItemRightClick(null, null);
			this.listBox.SelectedIndex = 0;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a new <see cref="ProductListBoxItem"/> instance.
		/// </summary>
		/// <returns>A new <see cref="ProductListBoxItem"/> instance.</returns>
		private ProductListBoxItem CreateItem() {
			return new ProductListBoxItem() { IsDockable = true };
		}

		/// <summary>
		/// Handles the <c>Click</c> event of the clear button.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnClearAllClick(object sender, RoutedEventArgs e) {
			for (int i = this.listBox.Items.Count - 2; i >= 0; i--)
				this.listBox.Items.RemoveAt(i);
		}

		/// <summary>
		/// Handles the <c>Click</c> event of the add-bottom button.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnAddItemBottomClick(object sender, RoutedEventArgs e) {
			ProductListBoxItem item = CreateItem();
			AnimatedDockPanel.SetDock(item, Dock.Bottom);
			this.listBox.Items.Insert(Math.Max(0, this.listBox.Items.Count - 1), item);
		}

		/// <summary>
		/// Handles the <c>Click</c> event of the add-left button.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnAddItemLeftClick(object sender, RoutedEventArgs e) {
			ProductListBoxItem item = CreateItem();
			AnimatedDockPanel.SetDock(item, Dock.Left);
			this.listBox.Items.Insert(Math.Max(0, this.listBox.Items.Count - 1), item);
		}

		/// <summary>
		/// Handles the <c>Click</c> event of the add-right button.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnAddItemRightClick(object sender, RoutedEventArgs e) {
			ProductListBoxItem item = CreateItem();
			AnimatedDockPanel.SetDock(item, Dock.Right);
			this.listBox.Items.Insert(Math.Max(0, this.listBox.Items.Count - 1), item);
		}

		/// <summary>
		/// Handles the <c>Click</c> event of the add-top button.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnAddItemTopClick(object sender, RoutedEventArgs e) {
			ProductListBoxItem item = CreateItem();
			AnimatedDockPanel.SetDock(item, Dock.Top);
			this.listBox.Items.Insert(Math.Max(0, this.listBox.Items.Count - 1), item);
		}
	}
}