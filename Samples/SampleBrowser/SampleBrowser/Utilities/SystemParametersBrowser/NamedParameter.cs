using System.Collections.Generic;
using System.Reflection;
using System.Windows;

namespace ActiproSoftware.SampleBrowser.Utilities.SystemParametersBrowser {

	/// <summary>
	/// Stores information about resource data.
	/// </summary>
	public class NamedParameter {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="NamedParameter"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="value">The value.</param>
		protected NamedParameter(string name, object value) {
			this.Name = name;
			this.Value = value;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; private set; }

		/// <summary>
		/// Gets the parameters from <see cref="SystemParameters"/>.
		/// </summary>
		public static IEnumerable<NamedParameter> SystemParameters {
			get {
				foreach (PropertyInfo p in typeof(SystemParameters).GetProperties(BindingFlags.Public | BindingFlags.Static)) {
					if (p.PropertyType != typeof(ResourceKey))
						yield return new NamedParameter(p.Name, p.GetValue(null, null));
				}
			}
		}

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>The value.</value>
		public object Value { get; private set; }

	}

}