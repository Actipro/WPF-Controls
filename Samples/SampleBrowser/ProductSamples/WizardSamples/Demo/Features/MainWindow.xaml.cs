using ActiproSoftware.Windows.Controls.Wizard;
using ActiproSoftware.Windows.Themes;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;

namespace ActiproSoftware.ProductSamples.WizardSamples.Demo.Features {

	/// <summary>
	/// Provides the demo user control for this sample.
	/// </summary>
	public partial class MainWindow {

		private BackgroundWorker simpleProcessingBackgroundWorker;

		/// <summary>
		/// Initializes an instance of the <c>MainWindow</c> class.
		/// </summary>
		public MainWindow() {
			InitializeComponent();
		}

		/// <summary>
		/// Occurs when the control is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">Event arguments.</param>
		private void programmaticSelectedPageNavigationSampleHyperlink_Click(object sender, RoutedEventArgs e) {
			wizard.SelectedPage = transitionEffectsPage;
		}

		/// <summary>
		/// Occurs when the control is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">Event arguments.</param>
		private void programmaticSelectedIndexNavigationSampleHyperlink_Click(object sender, RoutedEventArgs e) {
			wizard.SelectedIndex = 1;
		}
		
		/// <summary>
		/// Occurs when the control is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">Event arguments.</param>
		private void startProcessingButton_Click(object sender, RoutedEventArgs e) {
			// Disable the buttons while processing occurs
			startProcessingButton.IsEnabled = false;
			processingPage.CancelButtonEnabled = false;
			processingPage.BackButtonEnabled = false;
			processingPage.NextButtonEnabled = false;

			// Initialize the background worker
			if (simpleProcessingBackgroundWorker == null) {
				simpleProcessingBackgroundWorker = new BackgroundWorker();
				simpleProcessingBackgroundWorker.WorkerReportsProgress = true;
				simpleProcessingBackgroundWorker.DoWork += delegate(object sndr, DoWorkEventArgs eventArgs) {
					// Simply sleep for 100ms to simulate processing
					for (int index = 0; index <= 10; index++) {
						Thread.Sleep(100);
						simpleProcessingBackgroundWorker.ReportProgress(index * 10);
					}
				};
				simpleProcessingBackgroundWorker.ProgressChanged += delegate(object sndr, ProgressChangedEventArgs eventArgs) {
					progressTextBlock.Text = (eventArgs.ProgressPercentage < 100 ? eventArgs.ProgressPercentage + "% complete" : "Processing completed");
					progressBar.Value = eventArgs.ProgressPercentage; 
				};
				simpleProcessingBackgroundWorker.RunWorkerCompleted += delegate(object sndr, RunWorkerCompletedEventArgs eventArgs) {
					// Re-enable the buttons now that the processing is complete
					startProcessingButton.IsEnabled = true;
					processingPage.CancelButtonEnabled = null;
					processingPage.BackButtonEnabled = null;
					processingPage.NextButtonEnabled = null;
				};
			}

			// Start the background work
			simpleProcessingBackgroundWorker.RunWorkerAsync();
		}

		/// <summary>
		/// Occurs when the selection is changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">Event arguments.</param>
        private void themeListBox_SelectionChanged(object sender, RoutedEventArgs e) {
			if ((themeListBox.SelectedItem != null) && (((ListBoxItem)themeListBox.SelectedItem).Content != null)) {
				// Load the proper resources
				bool isAlternateButtonLayout = false;
				string themeName = ((ListBoxItem)themeListBox.SelectedItem).Tag as string;
				if ((themeName != null) && (themeName.EndsWith(".xaml"))) {
					// Load a theme from a resource dictionary in the sample project
					this.Resources = (ResourceDictionary)System.Windows.Application.LoadComponent(
						new Uri("/ProductSamples/WizardSamples/Demo/Features/Themes/" + themeName, UriKind.Relative));
					isAlternateButtonLayout = themeName.StartsWith("Alternate");
				}
				else {
					// Clear any loaded theme resources
					this.Resources = null;
				}

				// Change the button visibilities if using the alternate layout for the button container 
				wizard.FinishButtonVisible = !isAlternateButtonLayout;
				finishPage.NextButtonVisible = !isAlternateButtonLayout;

				// Due what seems to be a bug in VisualBrush where style changes don't update the rendered control, 
				//   this will force the brush to repaint the sample exterior page Wizard that is on the Themes page
				sampleExteriorPageWizard.Resources = this.Resources;
			}
		}
		
		/// <summary>
		/// Occurs when the wizard's Cancel button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">Event arguments.</param>
        private void wizard_Cancel(object sender, RoutedEventArgs e) {
			if ((BrowserInteropHelper.IsBrowserHosted) || (!wizard.CancelButtonClosesWindow))
				MessageBox.Show("You clicked the Cancel button while on the '" + wizard.SelectedPage.Caption + "' page.", "Wizard Sample");
		}

		/// <summary>
		/// Occurs when the wizard's Finish button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">Event arguments.</param>
        private void wizard_Finish(object sender, RoutedEventArgs e) {
			if ((BrowserInteropHelper.IsBrowserHosted) || (!wizard.FinishButtonClosesWindow))
				MessageBox.Show("You clicked the Finish button while on the '" + wizard.SelectedPage.Caption + "' page.", "Wizard Sample");
		}

		/// <summary>
		/// Occurs when the wizard's Help button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">Event arguments.</param>
        private void wizard_Help(object sender, RoutedEventArgs e) {
			MessageBox.Show("You clicked the Help button while on the '" + wizard.SelectedPage.Caption + "' page.", "Wizard Sample");
		}

		/// <summary>
		/// Occurs after the wizard's selected page has changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">Event arguments.</param>
		private void wizard_SelectedPageChanged(object sender, WizardSelectedPageChangeEventArgs e) {
			if (e.NewSelectedPage == processingPage) {
				// Clear the processing amount
				progressBar.Value = 0;
			}
			if (e.NewSelectedPage == cancelSelectionChangePage) {
				// Update the selection flags TextBlock to indicate what flags were used in selecting this page
				selectionFlagsTextBlock.Text = e.SelectionFlags.ToString();
			}
		}

		/// <summary>
		/// Occurs before the wizard's selected page has changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">Event arguments.</param>
		private void wizard_SelectedPageChanging(object sender, WizardSelectedPageChangeEventArgs e) {
			if (e.OldSelectedPage == cancelSelectionChangePage) {
				// If the cancel selection change CheckBox is checked, cancel the selection change
				if (cancelSelectionChangeCheckBox.IsChecked == true) {
					MessageBox.Show("The selected page change is cancelled because you have the CheckBox set.  Clear the CheckBox to be able to navigate through the wizard again.", "Wizard Sample");
					e.Cancel = true;
				}
			}
		}

	}
}