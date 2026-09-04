using ActiproSoftware.Windows.Controls.Editors;

namespace ActiproSoftware.ProductSamples.EditorsSamples.QuickStart.CurrencyComboBoxIntro;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		UpdateCountries();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Occurs when the selection is changed.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
		=> UpdateCountries();

	/// <summary>
	/// Updates the countries which use the selected <see cref="Currency"/>.
	/// </summary>
	private void UpdateCountries() {
		if (countriesItemsControl is null)
			return;

		var currencyCode = comboBox.SelectedValue as string;
		if (currencyCode is not null) {
			countriesItemsControl.ItemsSource =
				from mapping in CountryCurrencyMapping.Mappings
				join country in Country.Countries on mapping.CountryCode equals country.Alpha2Code
				where mapping.CurrencyCode == currencyCode
				orderby country.Name
				select country;
		}
	}

}
