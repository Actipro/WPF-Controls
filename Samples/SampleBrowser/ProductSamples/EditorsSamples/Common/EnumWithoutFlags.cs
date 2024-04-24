using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ActiproSoftware.ProductSamples.EditorsSamples.Common {
	
	/// <summary>
	/// Represents an enumeration without the flags attribute.
	/// </summary>
	public enum EnumWithoutFlags {

		/// <summary>
		/// Indicates no value.
		/// </summary>
		[Display(Order = 0, Name = "Nada")]
		None = 0,

		/// <summary>
		/// Indicates the value 1.
		/// </summary>
		[Display(Order = 1, Name = "Uno")]
		One = 0x01,

		/// <summary>
		/// Indicates the value 2.
		/// </summary>
		[Display(Order = 2, Name = "One and One")]
		Two = 0x02,

		/// <summary>
		/// Indicates the value 4.
		/// </summary>
		[Display(Order = 4, Name = "Square root of 16")]
		Four = 0x04,

		/// <summary>
		/// Indicates the value 8.
		/// </summary>
		[Display(Order = 8, Name = "2 to the power of 3")]
		Eight = 0x08,

		/// <summary>
		/// Indicates the union of One and Two.
		/// </summary>
		[Display(Order = 3, Name = "An odd number")]
		Three = One | Two,

		/// <summary>
		/// Indicates the union of One, Two, Four, and Eight.
		/// </summary>
		[Display(Name = "Everything")]
		All = One | Two | Four | Eight

	}

}
