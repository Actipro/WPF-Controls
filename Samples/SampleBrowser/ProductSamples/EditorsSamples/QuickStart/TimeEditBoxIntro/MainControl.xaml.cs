using System;
using System.Collections.Generic;
using ActiproSoftware.ProductSamples.EditorsSamples.Common;

namespace ActiproSoftware.ProductSamples.EditorsSamples.QuickStart.TimeEditBoxIntro {

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

			this.Formats = PredefinedFormats.Time;
			this.CurrentValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 15, 35, 0);
			this.MinimumValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0);
			this.MaximumValue = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59);

			this.DataContext = this;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the current value.
		/// </summary>
		/// <value>The current value.</value>
		public DateTime? CurrentValue { get; set; }
		
		/// <summary>
		/// Gets or sets the predefined formats.
		/// </summary>
		/// <value>The predefined formats.</value>
		public IEnumerable<PredefinedFormat> Formats { get; set; }
		
		/// <summary>
		/// Gets or sets the maximum value.
		/// </summary>
		/// <value>The maximum value.</value>
		public DateTime MaximumValue { get; set; }

		/// <summary>
		/// Gets or sets the minimum value.
		/// </summary>
		/// <value>The minimum value.</value>
		public DateTime MinimumValue { get; set; }

	}

}