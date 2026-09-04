using System.Windows.Media.Imaging;
using ActiproSoftware.SampleBrowser.SampleData;
using ActiproSoftware.Windows.Controls.Editors;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.EditorsSamples.QuickStart.AutoCompleteBoxIntro;

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

		// Initialize data
		textBox.ItemsSource = People.All;
		countryBox.ItemsSource = Country.Countries;
		UpdateQuickLaunchItemsSource();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Occurs when the end user submits a text entry, generally by pressing <c>Enter</c>.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnQuickLaunchBoxSubmitted(object sender, AutoCompleteBoxEventArgs e) {
		MessageBox.Show(string.Format("\"{0}\" was submitted.", e.Text), "Text Submitted");
		quickLaunchBox.Text = string.Empty;
	}

	/// <summary>
	/// Occurs when the <c>Text</c> is changed.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnQuickLaunchBoxTextChanged(object sender, AutoCompleteBoxEventArgs e)
		=> UpdateQuickLaunchItemsSource();

	/// <summary>
	/// Occurs when the end user submits a text entry, generally by pressing <c>Enter</c>.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnTextBoxSubmitted(object sender, AutoCompleteBoxEventArgs e) {
		MessageBox.Show(string.Format("\"{0}\" was submitted.", e.Text), "Text Submitted");
		textBox.Text = string.Empty;
	}

	/// <summary>
	/// Updates the items source of the quick launch example.
	/// </summary>
	private void UpdateQuickLaunchItemsSource() {
		var options = new List<object> {
			new QuickLaunchItem() { Text = "New Document", ImageSource = new BitmapImage(new Uri("/Images/Icons/New16.png", UriKind.Relative)) },
			new QuickLaunchItem() { Text = "Open Document", ImageSource = new BitmapImage(new Uri("/Images/Icons/Open16.png", UriKind.Relative)) },
			new QuickLaunchItem() { Text = "Save Document", ImageSource = new BitmapImage(new Uri("/Images/Icons/Save16.png", UriKind.Relative)) },
			new QuickLaunchItem() { Text = "Save All Documents" },
			new QuickLaunchItem() { Text = "Help", ImageSource = new BitmapImage(new Uri("/Images/Icons/Help16.png", UriKind.Relative)) }
		};

		var text = quickLaunchBox.Text;
		if (!string.IsNullOrEmpty(text)) {
			for (var index = options.Count - 1; index >= 0; index--) {
				if ((options[index] is QuickLaunchItem { Text: { } itemText }) && (itemText.IndexOf(text, 0, StringComparison.OrdinalIgnoreCase) == -1))
					options.RemoveAt(index);
			}

			if (options.Count > 0)
				options.Add(new QuickLaunchSeparator());

			options.Add(new QuickLaunchItem() { Text = string.Format("Get Help on \"{0}\"", text), ImageSource = new BitmapImage(new Uri("/Images/Icons/Help16.png", UriKind.Relative)) });
			options.Add(new QuickLaunchItem() { Text = string.Format("Smart Lookup on \"{0}\"", text), ImageSource = new BitmapImage(new Uri("/Images/Icons/Find16.png", UriKind.Relative)) });
		}

		quickLaunchBox.ItemsSource = options;
	}

}
