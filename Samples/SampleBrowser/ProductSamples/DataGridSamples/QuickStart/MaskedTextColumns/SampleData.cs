using System;
using System.Collections.Generic;

namespace ActiproSoftware.ProductSamples.DataGridSamples.QuickStart.MaskedTextColumns {

	/// <summary>
	/// Represents a simple set of data for demonstration purposes.
	/// </summary>
	public class SampleData {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets a identifier.
		/// </summary>
		/// <value>A identifier.</value>
		public int Id { get; set; }
	
		/// <summary>
		/// Gets or sets a SSN.
		/// </summary>
		/// <value>A SSN.</value>
		public string SocialSecurity { get; set; }

		/// <summary>
		/// Gets or sets a phone number.
		/// </summary>
		/// <value>A phone number.</value>
		public string Phone { get; set; }

		/// <summary>
		/// Gets the fixed static values.
		/// </summary>
		/// <value>The fixed static values.</value>
		public static List<SampleData> Values {
			get {
				List<SampleData> values = new List<SampleData>();
				values.Add(new SampleData() { Id = 1, SocialSecurity = "123-45-6789", Phone = "1-703-555-1212" });
				values.Add(new SampleData() { Id = 2, SocialSecurity = "234-56-7890", Phone = "(571) 555-1212" });
				values.Add(new SampleData() { Id = 3, SocialSecurity = "345-67-8901", Phone = "212-555-1212" });
				values.Add(new SampleData() { Id = 4, SocialSecurity = "456-78-9012", Phone = "555-1212" });
				values.Add(new SampleData() { Id = 5, SocialSecurity = "567-89-0123", Phone = "(202) 555-1212" });
				return values;
			}
		}

		#endregion // PUBLIC PROCEDURES
	}
}
