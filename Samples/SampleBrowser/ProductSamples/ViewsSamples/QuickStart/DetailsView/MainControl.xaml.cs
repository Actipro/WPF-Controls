using ActiproSoftware.SampleBrowser.SampleData;
using System.Collections.ObjectModel;
using System.Windows;

namespace ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.DetailsView {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private ObservableCollection<Person> people;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			// Set up the items source
			people = new ObservableCollection<Person>(People.All);
			listBox.ItemsSource = people;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Handles the <c>Click</c> event of the clear button.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnClearAllClick(object sender, RoutedEventArgs e) {
			people.Clear();
		}

		/// <summary>
		/// Handles the <c>Click</c> event of the remove button.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnRemoveItemsClick(object sender, RoutedEventArgs e) {
			for (int i = this.listBox.SelectedItems.Count - 1; i >= 0; i--)
				people.Remove((Person)this.listBox.SelectedItems[i]);
		}
		
		/// <summary>
		/// Handles the <c>Click</c> event of the reset button.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnResetClick(object sender, RoutedEventArgs e) {
			people.Clear();

			foreach (var person in People.All)
				people.Add(person);
		}

	}
}