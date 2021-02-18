using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using ActiproSoftware.Windows.Controls.BarCode;

namespace ActiproSoftware.ProductSamples.BarCodeSamples.Demo.ScreenTest {

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
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Generates a new value for the symbology.
		/// </summary>
		private void GenerateNewValue() {
			int minChars = 6;
			int maxChars = 12;
			string value = "";

			// Set min/max chars
			if (barCode.Symbology is Ean13Symbology) {
				value = "2";
				minChars = 11;
				maxChars = 11;
			}
			else if (barCode.Symbology is Ean8Symbology) {
				minChars = 7;
				maxChars = 7;
			}
			else if ((barCode.Symbology is PostnetSymbology) || (barCode.Symbology is UpcASymbology)) {
				minChars = 11;
				maxChars = 11;
			}
			else if (barCode.Symbology is UpcESymbology) {
				minChars = 7;
				maxChars = 7;
			}

			// Randomly generate a value
			DateTime dateTime = DateTime.Now;
			Random random = new Random(dateTime.Millisecond);

			int count = (int)Math.Max(minChars, Math.Min(maxChars, minChars + (random.NextDouble() * (maxChars - minChars))));
			for (int index = 0; index < count; index++)
				value += "0123456789"[(int)Math.Min(9, random.NextDouble() * 10)];

			// Append any necessary pre/post text
			if (barCode.Symbology is CodabarSymbology)
				value = String.Format("A{0}A", value);
			else if (barCode.Symbology is UpcESymbology)
				value = String.Format("0{0}", value);

			// Set the value
			barCode.Symbology.Value = value;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when a key is pressed while the control has focus.
		/// </summary>
		/// <param name="e">A <see cref="KeyEventArgs"/> that contains the event data.</param>
		protected override void OnKeyDown(KeyEventArgs e) {
			// Call the base method
			base.OnKeyDown(e);

			if (e.Key == Key.Return) {
				// Get the value to compare to
				string value = (barCode.Symbology is LinearBarCodeSymbology ? ((LinearBarCodeSymbology)barCode.Symbology).DisplayValue : barCode.Symbology.Value);

				// See if the test value matches
				ListBoxItem item = new ListBoxItem();
				if (inputTextBox.Text.Length == 0) {
					item.Content = "Empty read, wand may be configuring... please try again";
				}
				else if (inputTextBox.Text == value) {
					item.Content = String.Format("{0} Success: {1}", barCode.Symbology.DisplayName, inputTextBox.Text);
					item.Foreground = Brushes.Green;
					this.GenerateNewValue();
				}
				else {
					item.Content = String.Format("{0} Incorrect Read: {1} (should have been {2})", barCode.Symbology.DisplayName, inputTextBox.Text, value);
					item.Foreground = Brushes.Maroon;
				}
				resultsListBox.Items.Insert(0, item);
				resultsListBox.SelectedIndex = 0;

				inputTextBox.Text = String.Empty;
				inputTextBox.Focus();
			}
		}

	}
}