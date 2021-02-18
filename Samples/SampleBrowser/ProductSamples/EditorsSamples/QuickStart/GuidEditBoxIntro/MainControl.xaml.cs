using System;
using System.Collections.Generic;
using ActiproSoftware.ProductSamples.EditorsSamples.Common;
using ActiproSoftware.SampleBrowser;

namespace ActiproSoftware.ProductSamples.EditorsSamples.QuickStart.GuidEditBoxIntro {

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

			this.CurrentValue = Guid.NewGuid();
			this.Formats = PredefinedFormats.Guid;

			this.DataContext = this;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the current value.
		/// </summary>
		/// <value>The current value.</value>
		public Guid? CurrentValue { get; set; }
	
		/// <summary>
		/// Gets or sets the predefined formats.
		/// </summary>
		/// <value>The predefined formats.</value>
		public IEnumerable<PredefinedFormat> Formats { get; set; }

	}
}