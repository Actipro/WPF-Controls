using ActiproSoftware.Windows.Controls.Wizard;
using System.Threading;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.WizardSamples.Demo.Features;

/// <summary>
/// Provides the demo user control for this sample.
/// </summary>
public partial class MainWindow {

	private BackgroundWorker? _simpleProcessingBackgroundWorker;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainWindow() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnProgrammaticSelectedPageNavigationSampleHyperlinkClick(object sender, RoutedEventArgs e)
		=> wizard.SelectedPage = transitionEffectsPage;

	private void OnProgrammaticSelectedIndexNavigationSampleHyperlinkClick(object sender, RoutedEventArgs e)
		=> wizard.SelectedIndex = 1;

	private void OnStartProcessingButtonClick(object sender, RoutedEventArgs e) {
		// Disable the buttons while processing occurs
		startProcessingButton.IsEnabled = false;
		processingPage.CancelButtonEnabled = false;
		processingPage.BackButtonEnabled = false;
		processingPage.NextButtonEnabled = false;

		// Initialize the background worker
		if (_simpleProcessingBackgroundWorker is null) {
			_simpleProcessingBackgroundWorker = new BackgroundWorker {
				WorkerReportsProgress = true
			};
			_simpleProcessingBackgroundWorker.DoWork += (_, _) => {
				// Simply sleep for 100ms to simulate processing
				for (var index = 0; index <= 10; index++) {
					Thread.Sleep(100);
					_simpleProcessingBackgroundWorker.ReportProgress(index * 10);
				}
			};
			_simpleProcessingBackgroundWorker.ProgressChanged += (_, e) => {
				progressTextBlock.Text = (e.ProgressPercentage < 100 ? e.ProgressPercentage + "% complete" : "Processing completed");
				progressBar.Value = e.ProgressPercentage;
			};
			_simpleProcessingBackgroundWorker.RunWorkerCompleted += (_, _) => {
				// Re-enable the buttons now that the processing is complete
				startProcessingButton.IsEnabled = true;
				processingPage.CancelButtonEnabled = null;
				processingPage.BackButtonEnabled = null;
				processingPage.NextButtonEnabled = null;
			};
		}

		// Start the background work
		_simpleProcessingBackgroundWorker.RunWorkerAsync();
	}

	private void OnThemeListBoxSelectionChanged(object sender, RoutedEventArgs e) {
		if (themeListBox.SelectedItem is ListBoxItem { Content: not null } selectedItem) {
			// Load the proper resources
			bool isAlternateButtonLayout = false;
			var themeName = selectedItem.Tag as string;
			if (themeName?.EndsWith(".xaml") == true) {
				// Load a theme from a resource dictionary in the sample project
				Resources = (ResourceDictionary)Application.LoadComponent(new Uri("/ProductSamples/WizardSamples/Demo/Features/Themes/" + themeName, UriKind.Relative));
				isAlternateButtonLayout = themeName.StartsWith("Alternate");
			}
			else {
				// Clear any loaded theme resources
				Resources = null;
			}

			// Change the button visibilities if using the alternate layout for the button container
			wizard.FinishButtonVisible = !isAlternateButtonLayout;
			finishPage.NextButtonVisible = !isAlternateButtonLayout;

			// Due what seems to be a bug in VisualBrush where style changes don't update the rendered control,
			//   this will force the brush to repaint the sample exterior page Wizard that is on the Themes page
			sampleExteriorPageWizard.Resources = Resources;
		}
	}

	private void OnWizardCancel(object sender, RoutedEventArgs e) {
		if (!wizard.CancelButtonClosesWindow)
			MessageBox.Show($"You clicked the Cancel button while on the '{wizard.SelectedPage?.Caption}' page.", "Wizard Sample");
	}

	private void OnWizardFinish(object sender, RoutedEventArgs e) {
		if (!wizard.FinishButtonClosesWindow)
			MessageBox.Show($"You clicked the Finish button while on the '{wizard.SelectedPage?.Caption}' page.", "Wizard Sample");
	}

	private void OnWizardHelp(object sender, RoutedEventArgs e)
		=> MessageBox.Show($"You clicked the Help button while on the '{wizard.SelectedPage?.Caption}' page.", "Wizard Sample");

	private void OnWizardSelectedPageChanged(object sender, WizardSelectedPageChangeEventArgs e) {
		if (e.NewSelectedPage == processingPage) {
			// Clear the processing amount
			progressBar.Value = 0;
		}
		if (e.NewSelectedPage == cancelSelectionChangePage) {
			// Update the selection flags TextBlock to indicate what flags were used in selecting this page
			selectionFlagsTextBlock.Text = e.SelectionFlags.ToString();
		}
	}

	private void OnWizardSelectedPageChanging(object sender, WizardSelectedPageChangeEventArgs e) {
		if (e.OldSelectedPage == cancelSelectionChangePage) {
			// If the cancel selection change CheckBox is checked, cancel the selection change
			if (cancelSelectionChangeCheckBox.IsChecked == true) {
				MessageBox.Show("The selected page change is cancelled because you have the CheckBox set.  Clear the CheckBox to be able to navigate through the wizard again.", "Wizard Sample");
				e.Cancel = true;
			}
		}
	}

}
