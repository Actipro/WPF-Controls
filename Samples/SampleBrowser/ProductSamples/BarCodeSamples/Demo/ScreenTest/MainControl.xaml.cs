using ActiproSoftware.Extensions;
using ActiproSoftware.Windows.Controls.BarCode;
using ActiproSoftware.Windows.Data;

namespace ActiproSoftware.ProductSamples.BarCodeSamples.Demo.ScreenTest;

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
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Generates a new value for the symbology.
	/// </summary>
	private void GenerateNewValue() {
		// Ignore if symbology is not set
		if (barCode.Symbology is null)
			return;

		// Set min/max chars
		int minChars;
		int maxChars;
		var value = string.Empty;
		switch (barCode.Symbology) {
			case Ean13Symbology:
				value = "2";
				minChars = 11;
				maxChars = 11;
				break;
			case Ean8Symbology:
				minChars = 7;
				maxChars = 7;
				break;
			case PostnetSymbology:
			case UpcASymbology:
				minChars = 11;
				maxChars = 11;
				break;
			case UpcESymbology:
				minChars = 7;
				maxChars = 7;
				break;
			default:
				minChars = 6;
				maxChars = 12;
				break;
		}

		// Randomly generate a value
		var dateTime = DateTime.Now;
		var random = new Random(dateTime.Millisecond);

		var count = (random.NextDouble() * (maxChars - minChars)).ClampToRange(minChars, maxChars);
		for (var index = 0; index < count; index++)
			value += "0123456789"[(int)Math.Min(9, random.NextDouble() * 10)];

		// Append any necessary pre/post text
		value = barCode.Symbology switch {
			CodabarSymbology => string.Format("A{0}A", value),
			UpcESymbology => string.Format("0{0}", value),
			_ => value
		};

		// Set the value
		barCode.Symbology.Value = value;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void OnKeyDown(KeyEventArgs e) {
		base.OnKeyDown(e);

		// Ignore if symbology is not set
		if (barCode.Symbology is null)
			return;

		if (e.Key == Key.Return) {
			// Get the value to compare to
			var value = (barCode.Symbology as LinearBarCodeSymbology)?.DisplayValue
				?? barCode.Symbology.Value;

			// See if the test value matches
			var item = new ListBoxItem();
			if (string.IsNullOrEmpty(inputTextBox.Text)) {
				item.Content = "Empty read, wand may be configuring... please try again";
			}
			else if (inputTextBox.Text == value) {
				item.Content = string.Format("{0} Success: {1}", barCode.Symbology.DisplayName, inputTextBox.Text);
				item.Foreground = Brushes.Green;
				GenerateNewValue();
			}
			else {
				item.Content = string.Format("{0} Incorrect Read: {1} (should have been {2})", barCode.Symbology.DisplayName, inputTextBox.Text, value);
				item.Foreground = Brushes.Maroon;
			}
			resultsListBox.Items.Insert(0, item);
			resultsListBox.SelectedIndex = 0;

			inputTextBox.Text = string.Empty;
			inputTextBox.Focus();
		}
	}

}
