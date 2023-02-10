using System;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace ActiproSoftware.Products.Bars.Mvvm {

	/// <summary>
	/// Provides access to the string resources of this assembly, also allowing for their customization.
	/// </summary>
	/// <remarks>
	/// Call the <see cref="GetString(string)"/> method to return a resolved resource string.
	/// If a custom string has been set for a specified string resource name, it will be returned.
	/// Otherwise, the default string resource value is returned.
	/// <para>
	/// If any of the resource strings are customized via a call to <see cref="SetCustomString"/>,
	/// it is best to do so before any other classes in this assembly are referenced,
	/// such as in the application startup.
	/// </para>
	/// </remarks>
	public sealed class SR : SRBase {

		private static volatile SR	instance;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>SR</c> class.
		/// </summary>
		/// <remarks>
		/// The default constructor initializes all fields to their default values.
		/// </remarks>
		private SR() {}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the <see cref="SR"/> instance.
		/// </summary>
		/// <value>The <see cref="SR"/> instance.</value>
		private static SR Instance {
			get {
				if (instance == null)
					instance = new SR();
				return instance;
			}
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Removes all custom strings.
		/// </summary>
		public static void ClearCustomStrings() {
			SR.Instance.ClearCustomStringsCore();
		}

		/// <summary>
		/// Returns whether a custom string is defined for the specified string resource.
		/// </summary>
		/// <param name="name">The name of the resource for which to search.</param>
		/// <returns>
		/// <c>true</c> if a custom string is defined for the specified string resource; otherwise, <c>false</c>.
		/// </returns>
		public static bool ContainsCustomString(string name) {
			return SR.Instance.ContainsCustomStringCore(name);
		}

		/// <summary>
		/// Returns custom string that is stored for the specified string resource, if any.
		/// </summary>
		/// <param name="name">The name of the resource to get.</param>
		/// <returns>The custom string that is stored for the specified string resource, if any.</returns>
		public static string GetCustomString(string name) {
			return SR.Instance.GetCustomStringCore(name);
		}

		/// <summary>
		/// Returns the resolved value of the specified string resource.
		/// </summary>
		/// <param name="name">The name of the resource to get.</param>
		/// <returns>
		/// The value of the resource localized for the caller's current culture settings. 
		/// If a best match is not possible, <c>null</c> is returned.
		/// </returns>
		public static string GetString(string name) {
			return SR.Instance.GetStringCore(name, null);
		}
		
		/// <summary>
		/// Returns the resolved value of the specified string resource.
		/// </summary>
		/// <param name="name">The name of the resource to get.</param>
		/// <returns>
		/// The value of the resource localized for the caller's current culture settings. 
		/// If a best match is not possible, <c>null</c> is returned.
		/// </returns>
		public static string GetString(SRName name) {
			return SR.GetString(name.ToString());
		}
		
		/// <summary>
		/// Returns the resolved value of the specified string resource, by calling <c>String.Format</c> using supplied arguments.
		/// </summary>
		/// <param name="name">The name of the resource to get.</param>
		/// <param name="args">The arguments to pass to <c>String.Format</c>.</param>
		/// <returns>
		/// The value of the resource localized for the caller's current culture settings. 
		/// If a best match is not possible, <c>null</c> is returned.
		/// </returns>
		public static string GetString(string name, params object[] args) {
			return SR.Instance.GetStringCore(name, args);
		}
		
		/// <summary>
		/// Returns the resolved value of the specified string resource, by calling <c>String.Format</c> using supplied arguments.
		/// </summary>
		/// <param name="name">The name of the resource to get.</param>
		/// <param name="args">The arguments to pass to <c>String.Format</c>.</param>
		/// <returns>
		/// The value of the resource localized for the caller's current culture settings. 
		/// If a best match is not possible, <c>null</c> is returned.
		/// </returns>
		public static string GetString(SRName name, params object[] args) {
			return SR.GetString(name.ToString(), args);
		}
		
		/// <summary>
		/// Removes any custom string that is defined for the specified string resource.
		/// </summary>
		/// <param name="name">The name of the resource to remove.</param>
		public static void RemoveCustomString(string name) {
			SR.Instance.RemoveCustomStringCore(name);
		}
		
        /// <summary>
        /// Gets the <see cref="ResourceManager"/> that provides the default resources. 
        /// </summary>
		/// <value>The <see cref="ResourceManager"/> that provides the default resources.</value>
        public override ResourceManager ResourceManager {
            get { 
				return Resources.ResourceManager; 
			}
        }
		
		/// <summary>
		/// Sets a custom string value for the specified string resource.
		/// </summary>
		/// <param name="name">The name of the resource to set.</param>
		/// <param name="value">The value of the resource to set.</param>
		public static void SetCustomString(string name, string value) {
			SR.Instance.SetCustomStringCore(name, value);
		}

	}

}
