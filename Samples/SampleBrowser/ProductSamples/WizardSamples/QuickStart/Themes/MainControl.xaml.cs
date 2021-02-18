using System;
using System.Windows;
using System.Windows.Controls;

namespace ActiproSoftware.ProductSamples.WizardSamples.Demo.Themes {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl : System.Windows.Controls.UserControl {
		
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
		
	}
}