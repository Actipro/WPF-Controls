using System;

namespace ActiproSoftware.ProductSamples.EditorsSamples.QuickStart.MonthCalendarIntro {

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

			disabledDaysCalendar.IsDateDisabledFunc = d => (d.DayOfWeek == DayOfWeek.Tuesday) && (d.Day >= 8) && (d.Day <= 14);
		}
		
	}
}