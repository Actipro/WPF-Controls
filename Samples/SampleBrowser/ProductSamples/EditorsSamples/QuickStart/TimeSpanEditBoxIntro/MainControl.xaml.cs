using System;
using System.Collections.Generic;
using ActiproSoftware.ProductSamples.EditorsSamples.Common;

namespace ActiproSoftware.ProductSamples.EditorsSamples.QuickStart.TimeSpanEditBoxIntro {

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

			this.Formats = PredefinedFormats.TimeSpan;
			this.CurrentValue = new TimeSpan(3, 15, 0);
			this.MinimumValue = TimeSpan.MinValue;
			this.MaximumValue = TimeSpan.MaxValue;

			this.DataContext = this;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the current value.
		/// </summary>
		/// <value>The current value.</value>
		public TimeSpan? CurrentValue { get; set; }
		
		/// <summary>
		/// Gets or sets the predefined formats.
		/// </summary>
		/// <value>The predefined formats.</value>
		public IEnumerable<PredefinedFormat> Formats { get; set; }
		
		/// <summary>
		/// Gets or sets the maximum value.
		/// </summary>
		/// <value>The maximum value.</value>
		public TimeSpan MaximumValue { get; set; }

		/// <summary>
		/// Gets or sets the minimum value.
		/// </summary>
		/// <value>The minimum value.</value>
		public TimeSpan MinimumValue { get; set; }

	}

}