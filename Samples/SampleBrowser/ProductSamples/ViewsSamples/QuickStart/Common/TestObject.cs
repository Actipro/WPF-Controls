using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using ActiproSoftware.SampleBrowser;
using System.Windows.Media;

namespace ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.Common {

	/// <summary>
	/// Represents a test object for various Views samples.
	/// </summary>
	public class TestObject {

		private static Random random = new Random(Environment.TickCount);
		private static int counter = 0;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="TestObject"/> class.
		/// </summary>
		public TestObject() {
			this.Id = ++counter;

			byte r = (byte)(random.NextDouble() * 255);
			byte g = (byte)(random.NextDouble() * 255);
			byte b = (byte)(random.NextDouble() * 255);
			this.Brush = new SolidColorBrush(Color.FromArgb(0x88, r, g, b));
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the associated brush.
		/// </summary>
		/// <value>The associated brush.</value>
		public Brush Brush {
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		public int Id {
			get;
			private set;
		}

		/// <summary>
		/// Resets the counter.
		/// </summary>
		public static void ResetCounter() {
			counter = 0;
		}
	}
}
