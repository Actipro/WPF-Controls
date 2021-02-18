using System;
using System.ComponentModel;

namespace ActiproSoftware.ProductSamples.EditorsSamples.Common {
	
	/// <summary>
	/// Represents an enumeration without the flags attribute.
	/// </summary>
	public enum EnumWithoutFlags {

		/// <summary>
		/// Indicates no value.
		/// </summary>
		[Description("Nada")]
		None = 0,

		/// <summary>
		/// Indicates the value 1.
		/// </summary>
		[Description("Uno")]
		One = 0x01,

		/// <summary>
		/// Indicates the value 2.
		/// </summary>
		[Description("One and One")]
		Two = 0x02,

		/// <summary>
		/// Indicates the value 4.
		/// </summary>
		[Description("Square root of 16")]
		Four = 0x04,

		/// <summary>
		/// Indicates the value 8.
		/// </summary>
		[Description("2 to the power of 3")]
		Eight = 0x08,

		/// <summary>
		/// Indicates the union of One and Two.
		/// </summary>
		[Description("An odd number")]
		Three = One | Two,

		/// <summary>
		/// Indicates the union of One, Two, Four, and Eight.
		/// </summary>
		[Description("Everything")]
		All = One | Two | Four | Eight

	}

}
