using System.Collections.Generic;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridDynamicProperties {
	
	/// <summary>
	/// Represents the minimum requirements to allow an object to determine if its properties are read-only and visible.
	/// </summary>
	public interface IDynamicPropertyStateProvider {
		
		/// <summary>
		/// Gets whether the property supports dynamic <see cref="StandardValues"/>.
		/// </summary>
		/// <param name="propertyName">The name of the property to examine.</param>
		/// <returns>
		/// <c>true</c> if the property supports dynamic <see cref="StandardValues"/>; otherwise, <c>false</c>.
		/// </returns>
		bool GetPropertyHasStandardValues(string propertyName);

		/// <summary>
		/// Gets a value indicating whether the specified property is read-only.
		/// </summary>
		/// <param name="propertyName">The name of the property to examine.</param>
		/// <returns>
		/// <c>true</c> if the specified property is read-only; otherwise, <c>false</c>.
		/// </returns>
		bool GetPropertyReadOnly(string propertyName);
		
		/// <summary>
		/// Gets the standard list of values for the <see cref="Value"/> property.
		/// </summary>
		/// <param name="propertyName">The name of the property to examine.</param>
		/// <returns>
		/// The standard list of values for the <see cref="Value"/> property.
		/// </returns>
		IEnumerable<object> GetPropertyStandardValues(string propertyName);

		/// <summary>
		/// Gets a value indicating whether the specified property is visible.
		/// </summary>
		/// <param name="propertyName">The name of the property to examine.</param>
		/// <returns>
		/// <c>true</c> if the specified property is visible; otherwise, <c>false</c>.
		/// </returns>
		bool GetPropertyVisibility(string propertyName);

	}

}
