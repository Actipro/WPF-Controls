using System;

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.WallCalendar {

	/// <summary>
	/// A structure that holds data about a <see cref="Calendar"/> holiday.
	/// </summary>
	public class Holiday {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the day.
		/// </summary>
		/// <value>The day.</value>
		public int Day { get; set; }

		/// <summary>
		/// Gets or sets the name of the holiday.
		/// </summary>
		/// <value>The name of the holiday.</value>
		public string HolidayName { get; set; }

	}
}
