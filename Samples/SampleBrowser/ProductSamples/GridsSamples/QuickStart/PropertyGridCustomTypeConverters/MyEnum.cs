using System.ComponentModel;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCustomTypeConverters {
	
	/// <summary>
	/// Represents a custom enumeration.
	/// </summary>
	public enum MyEnum {

		/// <summary>
		/// The first value.
		/// </summary>
		[Description("1st of 3")]
		FirstEnum,

		/// <summary>
		/// The second value.
		/// </summary>
		[Description("2nd of 3")]
		SecondEnum,

		/// <summary>
		/// The third value.
		/// </summary>
		[Description("3rd of 3")]
		ThirdEnum

	}

}