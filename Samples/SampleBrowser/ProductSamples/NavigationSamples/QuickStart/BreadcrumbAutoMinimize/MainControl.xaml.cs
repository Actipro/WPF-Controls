using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Threading;

using ActiproSoftware.Windows.Controls.Navigation;
using ActiproSoftware.ProductSamples.NavigationSamples.Common.Breadcrumb.ShellItem;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.BreadcrumbAutoMinimize {

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
		/// Handles the ConvertItem event of the breadcrumb control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="ActiproSoftware.Windows.Controls.Navigation.BreadcrumbConvertItemEventArgs"/> instance containing the event data.</param>
		private void OnBreadcrumbConvertItem(object sender, BreadcrumbConvertItemEventArgs e) {
			ConvertItemHelper.HandleConvertItem(sender, e);
		}

		/// <summary>
		/// Handles the SelectionChanged event of the RadioButtonList used for the minimized item display styles.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
		private void OnDisplayStyleListSelectionChanged(object sender, SelectionChangedEventArgs e) {
			ComboBox comboBox = sender as ComboBox;
			Style style = null;
			switch (comboBox.SelectedIndex) {
				case 1:
					style = (Style)FindResource("BreadcrumbItemStyleEllipsis");
					break;
				case 2:
					style = (Style)FindResource("BreadcrumbItemStyleImageOnly");
					break;
				case 3:
					style = (Style)FindResource("BreadcrumbItemStyleNormal");
					break;
				default:
					style = (Style)FindResource("BreadcrumbItemStylePopupOnly");
					break;
			}

			if (null != style) {
				// In order to dynamically switch the style of items already created, we need to recreate them. To do this we will
				//   state the current state of the Breadcrumb, clear it's selected and root items, then restore the state. Keep
				//   in mind that this process is only needed because we are trying to demonstrate different styles that can be
				//   applied during runtime. Typically, a style would be applied up front and not changed during runtime.
				//

				// Save the current state
				int maxTailItemCount = this.breadcrumb.MaxTailItemCount;
				object rootItem = this.breadcrumb.RootItem;
				object selectedItem = this.breadcrumb.SelectedItem;
				object tailItem = null;
				if (null != this.breadcrumb.SelectedContainer) {
					BreadcrumbItem container = this.breadcrumb.SelectedContainer.ExpandedContainer;
					while (null != container) {
						tailItem = container.DataContext;
						container = container.ExpandedContainer;
					}
				}

				// Clear out the Breadcrumb
				this.breadcrumb.MaxTailItemCount = 0;
				this.breadcrumb.RootItem = null;
				this.breadcrumb.UpdateLayout();

				// Set the new style
				this.breadcrumb.ItemContainerStyle = style;

				// Restore the state
				this.breadcrumb.RootItem = rootItem;
				this.breadcrumb.MaxTailItemCount = maxTailItemCount;
				this.breadcrumb.UpdateLayout();
				if (null != tailItem) {
					this.breadcrumb.SelectedItem = tailItem;
					this.breadcrumb.UpdateLayout();
				}
				if (null != selectedItem) {
					this.breadcrumb.SelectedItem = selectedItem;
					this.breadcrumb.UpdateLayout();
				}
			}
		}

		/// <summary>
		/// Handles the Click event of the select leaf item Button.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
		private void OnSelectLeafItemClick(object sender, RoutedEventArgs e) {
			this.breadcrumb.SelectedPath = "Desktop\\Computer\\Local Disk (C:)\\Program Files\\Actipro Software\\WPFStudio";
		}
	}
}