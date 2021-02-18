using System.Collections.Generic;
using System.ComponentModel;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCollectionNewItems {
	
	/// <summary>
	/// Represents a parent object which has several collections of child objects.
	/// </summary>
	public class TestObject {

		private List<string> strings1;
		private List<string> strings2;
		private List<string> strings3;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>TestObject</c> class.
		/// </summary>
		public TestObject() {
			this.strings1 = new List<string>();
			this.strings1.Add("One");
			this.strings1.Add("Two");
			this.strings1.Add("Three");

			this.strings2 = new List<string>();
			this.strings2.Add("One");
			this.strings2.Add("Two");
			this.strings2.Add("Three");

			this.strings3 = new List<string>();
			this.strings3.Add("One");
			this.strings3.Add("Two");
			this.strings3.Add("Three");
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets a list of strings.
		/// </summary>
		/// <value>A list of strings.</value>
		[Description("A list of strings (i.e. List<string>), which uses the default type converter that allows empty strings to be added.")]
		public List<string> Strings1 {
			get {
				return this.strings1;
			}
		}

		/// <summary>
		/// Gets a list of strings.
		/// </summary>
		/// <value>A list of strings.</value>
		[Description("A list of strings (i.e. List<string>), which uses a custom type converter that allows null values to be added.")]
		[TypeConverter(typeof(NullStringListConverter))]
		public List<string> Strings2 {
			get {
				return this.strings2;
			}
		}

		/// <summary>
		/// Gets a list of strings.
		/// </summary>
		/// <value>A list of strings.</value>
		[Description("A list of strings (i.e. List<string>), which uses a custom type converter that allows custom strings to be added.")]
		[TypeConverter(typeof(CustomStringListConverter))]
		public List<string> Strings3 {
			get {
				return this.strings3;
			}
		}

	}

}
