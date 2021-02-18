using ActiproSoftware.Products;
using System;
using System.Reflection;

namespace ActiproSoftware.SampleBrowser.Utilities.StringResourceBrowser {

	/// <summary>
	/// Stores information about string resource data.
	/// </summary>
	public class ResourceData {

		private Type enumType;
		private string name;
		private Type srType;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>ResourceData</c> class.
		/// </summary>
		/// <param name="srType">The <see cref="SRBase"/> <see cref="Type"/>.</param>
		/// <param name="enumType">The enumeration <see cref="Type"/>.</param>
		/// <param name="name">The name of the resource.</param>
		public ResourceData(Type srType, Type enumType, string name) {
			// Initialize
			this.srType = srType;
			this.enumType = enumType;
			this.name = name;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the enumeration <see cref="Type"/>.
		/// </summary>
		/// <value>The enumeration <see cref="Type"/>.</value>
		public Type EnumType {
			get {
				return enumType;
			}
		}

		/// <summary>
		/// Gets the name of the resource.
		/// </summary>
		/// <value>The name of the resource.</value>
		public string Name {
			get {
				return name;
			}
		}

		/// <summary>
		/// Gets the value of the resource.
		/// </summary>
		/// <value>The value of the resource.</value>
		public string Value {
			get {
				return srType.InvokeMember("GetString", BindingFlags.Public | BindingFlags.Static | BindingFlags.InvokeMethod, null, null, new object[] { name }) as string;
			}
		}

	}

}