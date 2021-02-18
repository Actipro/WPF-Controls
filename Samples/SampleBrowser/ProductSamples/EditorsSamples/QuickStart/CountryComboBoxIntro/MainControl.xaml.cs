using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using ActiproSoftware.Windows.Controls.Editors;

namespace ActiproSoftware.ProductSamples.EditorsSamples.QuickStart.CountryComboBoxIntro {

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
			this.InitializeFilteredCountries();

			InitializeComponent();
			
			this.DataContext = this;
			this.UpdateCurrencies();
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes the filtered country list.
		/// </summary>
		private void InitializeFilteredCountries() {
			var countries = new string[] { "PT", "ES", "GB", "FR", "DE" };
			this.FilteredCountries = from c in Country.Countries where countries.Contains(c.Alpha2Code) select c;
			this.SelectedFilteredCountry = "GB";
		}

		/// <summary>
		/// Occurs when the selection is changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="SelectionChangedEventArgs"/> that contains data related to the event.</param>
		private void OnComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e) {
			this.UpdateCurrencies();
		}
		
		/// <summary>
		/// Updates the currency types used by the selected <see cref="Country"/>.
		/// </summary>
		private void UpdateCurrencies() {
			if (currenciesItemsControl == null)
				return;

			var countryCode = comboBox.SelectedValue as string;
			if (countryCode != null) {
				currenciesItemsControl.ItemsSource =
					from mapping in CountryCurrencyMapping.Mappings
					join currency in Currency.Currencies on mapping.CurrencyCode equals currency.Code
					where mapping.CountryCode == countryCode
					orderby currency.Code
					select currency;
			}
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the collection of filtered countries.
		/// </summary>
		/// <value>The collection of filtered countries.</value>
		public IEnumerable<Country> FilteredCountries { get; private set; }

		/// <summary>
		/// Gets or sets the selected filtered country code.
		/// </summary>
		/// <value>The selected filtered country code.</value>
		public string SelectedFilteredCountry { get; set; }

	}

}