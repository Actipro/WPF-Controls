namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.ScrollWheel;

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

		// Year
		var currentYear = DateTime.Now.Year;
		for (var year = currentYear; year < currentYear + 10; year++)
			yearListBox.Items.Add(year);
		yearListBox.SelectedItem = currentYear;

		// Month
		for (var month = 1; month <= 12; month++)
			monthListBox.Items.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month));
		monthListBox.SelectedIndex = DateTime.Now.Month - 1;

		// Day
		for (var day = 1; day <= 31; day++)
			dayListBox.Items.Add(day);
		dayListBox.SelectedItem = DateTime.Now.Day;
	}

}
