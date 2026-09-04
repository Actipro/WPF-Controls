namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridDynamicProperties;

/// <summary>
/// Represents the minimum requirements to allow an object to determine if its properties are read-only and visible.
/// </summary>
public interface IDynamicPropertyStateProvider {

	/// <summary>
	/// Returns whether the property supports dynamic <see cref="StandardValues"/>.
	/// </summary>
	/// <param name="propertyName">The name of the property to examine.</param>
	bool GetPropertyHasStandardValues(string propertyName);

	/// <summary>
	/// Returns whether the specified property is read-only.
	/// </summary>
	/// <param name="propertyName">The name of the property to examine.</param>
	bool GetPropertyReadOnly(string propertyName);

	/// <summary>
	/// Returns the standard list of values for the <see cref="Value"/> property.
	/// </summary>
	/// <param name="propertyName">The name of the property to examine.</param>
	IEnumerable<object>? GetPropertyStandardValues(string propertyName);

	/// <summary>
	/// Returns whether the specified property is visible.
	/// </summary>
	/// <param name="propertyName">The name of the property to examine.</param>
	bool GetPropertyVisibility(string propertyName);

}
