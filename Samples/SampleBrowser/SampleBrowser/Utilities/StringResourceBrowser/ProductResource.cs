using System;
using System.Reflection;

namespace ActiproSoftware.SampleBrowser.Utilities.StringResourceBrowser {

	/// <summary>
	/// Stores information about a product with string resources.
	/// </summary>
	public class ProductResource {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>ProductResource</c> class.
		/// </summary>
		/// <param name="assembly">The assembly.</param>
		public ProductResource(Assembly assembly) {
			this.Assembly = assembly;

			var name = this.Assembly.GetName().Name;
			if (name.StartsWith("ActiproSoftware.", StringComparison.OrdinalIgnoreCase))
				name = name.Substring("ActiproSoftware.".Length);
			if (name.EndsWith(".Wpf", StringComparison.OrdinalIgnoreCase))
				name = name.Substring(0, name.Length - ".Wpf".Length);
			this.Name = name;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the assembly.
		/// </summary>
		/// <value>The assembly.</value>
		public Assembly Assembly { get; private set; }

		/// <summary>
		/// Gets whether assembly is valid for customizing string resources.
		/// </summary>
		/// <value>
		/// <c>true</c> if the assembly is valid for customizing string resources; otherwise, <c>false</c>.
		/// </value>
		public bool IsValid {
			get {
				return (this.SRType != null) && (this.SRNameType != null);
			}
		}

		/// <summary>
		/// Gets the name of the resource.
		/// </summary>
		/// <value>The name of the resource.</value>
		public string Name { get; private set; }
		
		/// <summary>
		/// Gets the string resource name type.
		/// </summary>
		/// <value>The string resource name type.</value>
		public Type SRNameType {
			get {
				return Type.GetType(String.Format("ActiproSoftware.Products.{0}.SRName, {1}", this.Name, this.Assembly.FullName));
			}
		}

		/// <summary>
		/// Gets the string resource type.
		/// </summary>
		/// <value>The string resource type.</value>
		public Type SRType {
			get {
				return Type.GetType(String.Format("ActiproSoftware.Products.{0}.SR, {1}", this.Name, this.Assembly.FullName));
			}
		}

	}

}