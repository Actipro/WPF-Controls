using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ActiproSoftware.Windows.Controls.Views;
using ActiproSoftware.Windows.Media;
using ActiproSoftware.SampleBrowser;

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.ScrollWheel {

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
			
			// Year
			int currentYear = DateTime.Now.Year;
			for (int year = currentYear; year < currentYear + 10; year++)
				yearListBox.Items.Add(year);
			yearListBox.SelectedItem = currentYear;

			// Month
			for (int month = 1; month <= 12; month++)
				monthListBox.Items.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month));
			monthListBox.SelectedIndex = DateTime.Now.Month - 1;
			
			// Day
			for (int day = 1; day <= 31; day++)
				dayListBox.Items.Add(day);
			dayListBox.SelectedItem = DateTime.Now.Day;
		}

	}
}