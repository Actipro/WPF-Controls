using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.GettingStarted {

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

			// Define the steps in the series
			itemsControl.ItemsSource = new List<GettingStartedItemInfo>() {
				new GettingStartedItemInfo(1, "Step01/MainWindow", "Create a RibbonWindow configured with an empty Ribbon."),
				new GettingStartedItemInfo(2, "Step02/MainWindow", "Create SampleApplicationViewModel and RibbonViewModel that will be bound to the sample."),
				new GettingStartedItemInfo(3, "Step03/MainWindow", "Create SampleBarManager to manage working with view models for controls within the Ribbon."),
				new GettingStartedItemInfo(4, "Step04/MainWindow", "Add the first Tab to the Ribbon."),
				new GettingStartedItemInfo(5, "Step05/MainWindow", "Expand the current sample to include a RichTextBox with a more diverse set of commands in the Ribbon."),
				new GettingStartedItemInfo(6, "Step06/MainWindow", "Replace a default ContextMenu with one based on Ribbon controls."),
				new GettingStartedItemInfo(7, "Step07/MainWindow", "Add the Quick Access Toolbar."),
				new GettingStartedItemInfo(8, "Step08/MainWindow", "Add the Backstage with buttons."),
				new GettingStartedItemInfo(9, "Step09/MainWindow", "Expand the Backstage to include Tabs."),
			};

		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
			
		/// <summary>
		/// Occurs when button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnLaunchButtonClick(object sender, RoutedEventArgs e) {
			if ((this.DataContext is ApplicationViewModel viewModel)
				&& (sender is Button button)
				&& (button.DataContext is GettingStartedItemInfo itemInfo)) {

				viewModel.OpenExternalSample(itemInfo.Path);
			}
		}

	}

}
