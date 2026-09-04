using ActiproSoftware.Windows.Controls.Editors;

namespace ActiproSoftware.ProductSamples.EditorsSamples.QuickStart.CountryComboBoxIntro;

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
		InitializeFilteredCountries();

		InitializeComponent();

		DataContext = this;
		UpdateCurrencies();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the filtered country list.
	/// </summary>
	private void InitializeFilteredCountries() {
		var countries = new string[] { "PT", "ES", "GB", "FR", "DE" };
		FilteredCountries = Country.Countries.Where(c => countries.Contains(c.Alpha2Code));
		SelectedFilteredCountry = "GB";
	}

	/// <summary>
	/// Occurs when the selection is changed.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
		=> UpdateCurrencies();

	/// <summary>
	/// Updates the currency types used by the selected <see cref="Country"/>.
	/// </summary>
	private void UpdateCurrencies() {
		if (currenciesItemsControl is null)
			return;

		var countryCode = comboBox.SelectedValue as string;
		if (countryCode is not null) {
			currenciesItemsControl.ItemsSource =
				from mapping in CountryCurrencyMapping.Mappings
				join currency in Currency.Currencies on mapping.CurrencyCode equals currency.Code
				where mapping.CountryCode == countryCode
				orderby currency.Code
				select currency;
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The collection of filtered countries.
	/// </summary>
	public IEnumerable<Country>? FilteredCountries { get; private set; }

	/// <summary>
	/// The selected filtered country code.
	/// </summary>
	public string? SelectedFilteredCountry { get; set; }

}
