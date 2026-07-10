using ActiproSoftware.SampleBrowser.SampleData;

namespace ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.DetailsView;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	private readonly ObservableCollection<Person> _people;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		// Set up the items source
		_people = new ObservableCollection<Person>(People.All);
		listBox.ItemsSource = _people;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnClearAllClick(object sender, RoutedEventArgs e)
		=> _people.Clear();

	private void OnRemoveItemsClick(object sender, RoutedEventArgs e) {
		for (int i = listBox.SelectedItems.Count - 1; i >= 0; i--)
			_people.Remove((Person)listBox.SelectedItems[i]!);
	}

	private void OnResetClick(object sender, RoutedEventArgs e) {
		_people.Clear();

		foreach (var person in People.All)
			_people.Add(person);
	}

}
