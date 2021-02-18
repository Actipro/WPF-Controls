using System;

namespace ActiproSoftware.ProductSamples.GridsSamples.Demo.Toolbox {

	/// <summary>
	/// Provides control data information.
	/// </summary>
	public class ControlData {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>ControlData</c> class.
		/// </summary>
		/// <param name="fullName">The full name of the control (e.g. CompanyName.Namespace.ControlName).</param>
		/// <param name="category">The category for the control.</param>
		public ControlData(string fullName, string category) {
			this.FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
			this.Category = category ?? throw new ArgumentNullException(nameof(category));
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the full name of the control (e.g. CompanyName.Namespace.ControlName).
		/// </summary>
		public string FullName { get; }

		/// <summary>
		/// Gets the category for the control.
		/// </summary>
		public string Category { get; }

	}

}
