using ActiproSoftware.Windows.Controls.Editors;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.ComboBoxAndEditors;

/// <summary>
/// Provides the user control for this sample that uses a XAML-based ribbon configuration.
/// </summary>
public partial class SampleXamlControl : SampleControlBase {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public SampleXamlControl() {
		InitializeComponent();

		// Configure this code-behind to be the view model for this sample
		DataContext = this;

		// Additional configuration for other editors
		countryBox.ItemsSource = Country.Countries;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override string? GetTextBoxCommitCommandText(object? commandParameter) {
		// In the XAML sample the text is passed as the command parameter
		return commandParameter as string;
	}

}
