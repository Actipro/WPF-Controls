using System.Reflection;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCustomTypeConverters;

/// <summary>
/// Represents a <see cref="EnumConverter"/> that uses the <see cref="DescriptionAttribute"/> for the
/// string representation of the enumeration values, when available.
/// </summary>
/// <param name="type">A <see cref="Type"/> that represents the type of enumeration to associate with this enumeration converter.</param>
public class EnumDescriptionTypeConverter(Type type) : EnumConverter(type) {

	private readonly Type _type = type;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value) {
		if (value is string stringValue)
			return GetValue(_type, stringValue);

		return base.ConvertFrom(context, culture, value);
	}

	/// <inheritdoc/>
	public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType) {
		if (destinationType is null)
			throw new ArgumentNullException(nameof(destinationType));

		if ((value is not null) && (destinationType == typeof(string)))
			return GetDescription(_type, value.ToString());

		return base.ConvertTo(context, culture, value, destinationType);
	}

	/// <summary>
	/// This method will get the "description" of the given enumeration field name for
	/// the given type (set by using the DescriptionAttribute). If there is no
	/// description then it will simply return the given field name.
	/// </summary>
	/// <param name="type">The enumeration type to get the description for.</param>
	/// <param name="fieldName">The enumeration fieldName to get the description for.</param>
	/// <returns>
	/// The description of the given enumeration field name for the given type, the given
	/// field name, or string.Empty if all else fails.
	/// </returns>
	public static string GetDescription(Type type, string? fieldName) {
		if (fieldName is not null) {
			var fieldInfo = type.GetField(fieldName);
			var attribute = fieldInfo?.GetCustomAttribute<DescriptionAttribute>();
			return attribute?.Description
				?? fieldName;
		}

		return string.Empty;
	}

	/// <summary>
	/// Returns the value of an Enum, based on it's DescriptionAttribute or named value.
	/// </summary>
	/// <param name="type">The enumeration type to get the value for.</param>
	/// <param name="description">The description or name of the element.</param>
	public static object? GetValue(Type type, String description) {
		foreach (var fieldInfo in type.GetFields()) {
			var attribute = fieldInfo.GetCustomAttribute<DescriptionAttribute>(inherit: false);
			if ((attribute?.Description is { } attributeDescription) && (attributeDescription == description))
				return fieldInfo.GetValue(fieldInfo.Name);

			if (fieldInfo.Name == description)
				return fieldInfo.GetValue(fieldInfo.Name);
		}

		return description;
	}

}
