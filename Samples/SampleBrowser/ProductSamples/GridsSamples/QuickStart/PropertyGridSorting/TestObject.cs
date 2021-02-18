using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridSorting {
	
	/// <summary>
	/// Represents a test object for demonstration purposes.
	/// </summary>
	public class TestObject {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		// A Properties
		[Category("A")]
		[DisplayName("1")]
		[Description("The category for this property will be sorted below the Z category, but alphabetically with other categories. Its properties will be sorted in reverse numerical order due to the DisplayAttribute.Order specifications on each property.")]
		[Display(Order = 4)]
		public string AProperty1 { get; set; }

		[Category("A")]
		[DisplayName("2")]
		[Description("The category for this property will be sorted below the Z category, but alphabetically with other categories. Its properties will be sorted in reverse numerical order due to the DisplayAttribute.Order specifications on each property.")]
		[Display(Order = 3)]
		public string AProperty2 { get; set; }

		[Category("A")]
		[DisplayName("10")]
		[Description("The category for this property will be sorted below the Z category, but alphabetically with other categories. Its properties will be sorted in reverse numerical order due to the DisplayAttribute.Order specifications on each property.")]
		[Display(Order = 2)]
		public string AProperty10 { get; set; }

		[Category("A")]
		[DisplayName("20")]
		[Description("The category for this property will be sorted below the Z category, but alphabetically with other categories. Its properties will be sorted in reverse numerical order due to the DisplayAttribute.Order specifications on each property.")]
		[Display(Order = 1)]
		public string AProperty20 { get; set; }

		// B Properties
		[Category("B")]
		[DisplayName("1")]
		[Description("The category for this property will be sorted below the Z category, but alphabetically with other categories. Its properties will be sorted based on the display name's numeric value.")]
		public string BProperty1 { get; set; }

		[Category("B")]
		[DisplayName("2")]
		[Description("The category for this property will be sorted below the Z category, but alphabetically with other categories. Its properties will be sorted based on the display name's numeric value.")]
		public string BProperty2 { get; set; }

		[Category("B")]
		[DisplayName("10")]
		[Description("The category for this property will be sorted below the Z category, but alphabetically with other categories. Its properties will be sorted based on the display name's numeric value.")]
		public string BProperty10 { get; set; }

		[Category("B")]
		[DisplayName("20")]
		[Description("The category for this property will be sorted below the Z category, but alphabetically with other categories. Its properties will be sorted based on the display name's numeric value.")]
		public string BProperty20 { get; set; }

		// Z Properties
		[Category("Z")]
		[DisplayName("1")]
		[Description("The category for this property will be sorted before all others, and the properties in this category will be sorted alphabetically.")]
		public string ZProperty1 { get; set; }

		[Category("Z")]
		[DisplayName("2")]
		[Description("The category for this property will be sorted before all others, and the properties in this category will be sorted alphabetically.")]
		public string ZProperty2 { get; set; }

		[Category("Z")]
		[DisplayName("10")]
		[Description("The category for this property will be sorted before all others, and the properties in this category will be sorted alphabetically.")]
		public string ZProperty10 { get; set; }

		[Category("Z")]
		[DisplayName("20")]
		[Description("The category for this property will be sorted before all others, and the properties in this category will be sorted alphabetically.")]
		public string ZProperty20 { get; set; }

	}

}
