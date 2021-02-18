using System.Linq;
using System.Windows.Controls;
using ActiproSoftware.Windows.Controls.Editors;

namespace ActiproSoftware.ProductSamples.EditorsSamples.QuickStart.CurrencyComboBoxIntro {

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

			this.UpdateCountries();
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when the selection is changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="SelectionChangedEventArgs"/> that contains data related to the event.</param>
		private void OnComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e) {
			this.UpdateCountries();
		}
		
		/// <summary>
		/// Updates the countries which use the selected <see cref="Currency"/>.
		/// </summary>
		private void UpdateCountries() {
			if (countriesItemsControl == null)
				return;

			var currencyCode = comboBox.SelectedValue as string;
			if (currencyCode != null) {
				countriesItemsControl.ItemsSource =
					from mapping in CountryCurrencyMapping.Mappings
					join country in Country.Countries on mapping.CountryCode equals country.Alpha2Code
					where mapping.CurrencyCode == currencyCode
					orderby country.Name
					select country;
			}
		}

	}
}