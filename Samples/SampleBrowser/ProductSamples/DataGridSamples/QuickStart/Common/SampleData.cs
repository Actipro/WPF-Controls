using System;
using System.Collections.Generic;

namespace ActiproSoftware.ProductSamples.DataGridSamples.Common {

	/// <summary>
	/// Represents a simple set of data for demonstration purposes.
	/// </summary>
	public class SampleData {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets a boolean value.
		/// </summary>
		/// <value>A boolean value.</value>
		public bool Boolean { get; set; }

		/// <summary>
		/// Gets or sets a date/time value.
		/// </summary>
		/// <value>A date/time value.</value>
		public DateTime DateTime { get; set; }

		/// <summary>
		/// Gets or sets a integer value.
		/// </summary>
		/// <value>A integer value.</value>
		public int Integer { get; set; }

		/// <summary>
		/// Gets or sets a long string value.
		/// </summary>
		/// <value>A long string value.</value>
		public string LongString { get; set; }

		/// <summary>
		/// Gets or sets a string value.
		/// </summary>
		/// <value>A string value.</value>
		public string String { get; set; }

		/// <summary>
		/// Gets the fixed static values.
		/// </summary>
		/// <value>The fixed static values.</value>
		public static List<SampleData> Values {
			get {
				DateTime now = DateTime.Now;

				List<SampleData> values = new List<SampleData>();
				for (int x = 1; x <= 10; x++) {
					SampleData data = new SampleData();
					data.Boolean = (0 == (x % 2));
					data.DateTime = new DateTime(now.Year, now.Month, x);
					data.Integer = x;
					data.String = string.Format("String {0}", x);
					data.LongString = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In dui nunc, feugiat a vestibulum eget, interdum quis nunc. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Sed non turpis erat, eu placerat lorem. Cras quis enim eget eros malesuada sagittis nec vel diam. Integer scelerisque fringilla sapien ac condimentum. Integer consequat libero sed tortor venenatis dapibus. Duis ultricies molestie ligula, quis tristique justo egestas sit amet. Duis fringilla velit a sem rhoncus aliquam.";
					values.Add(data);
				}
				return values;
			}
		}

		#endregion // PUBLIC PROCEDURES
	}
}
