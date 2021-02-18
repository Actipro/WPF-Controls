using System;
using System.Windows.Media;

namespace ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.BookTransparency {

	/// <summary>
	/// Represents a test object for this sample.
	/// </summary>
	public class TestObject {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="TestObject"/> class.
		/// </summary>
		public TestObject() {
			this.Header = " ";
			this.Footer = " ";
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the footer.
		/// </summary>
		/// <value>The footer.</value>
		public string Footer { get; set; }

		/// <summary>
		/// Gets or sets the header.
		/// </summary>
		/// <value>The header.</value>
		public string Header { get; set; }

		/// <summary>
		/// Gets or sets the content.
		/// </summary>
		/// <value>The content.</value>
		public ImageSource ImageSource { get; set; }

	}
}
