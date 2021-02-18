namespace ActiproSoftware.ProductSamples.EditorsSamples.Common {

	/// <summary>
	/// Represents a predefined format.
	/// </summary>
	public class PredefinedFormat {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="PredefinedFormat"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="format">The format.</param>
		public PredefinedFormat(string name, string format) {
			this.Name = name;
			this.Format = format;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the predefined format.
		/// </summary>
		/// <value>The predefined format.</value>
		public string Format { get; private set; }
		
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; private set; }

		/// <summary>
		/// Returns a <see cref="String"/> that represents the current <see cref="Object"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="String"/> that represents the current <see cref="Object"/>.
		/// </returns>
		public override string ToString() {
			return this.Format ?? string.Empty;
		}

	}

}
