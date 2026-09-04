namespace ActiproSoftware.ProductSamples.WizardSamples.Demo.Themes;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl : UserControl {

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

}
