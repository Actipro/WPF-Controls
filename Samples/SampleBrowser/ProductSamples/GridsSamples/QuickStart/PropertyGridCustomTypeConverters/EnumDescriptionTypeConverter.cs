using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCustomTypeConverters {
	
	/// <summary>
	/// Represents a <see cref="EnumConverter"/> that uses the <see cref="DescriptionAttribute"/> for the
	/// string representation of the enumeration values, when available.
	/// </summary>
	public class EnumDescriptionTypeConverter : EnumConverter {

		private Type type;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="EnumDescriptionTypeConverter"/> class.
		/// </summary>
		/// <param name="type">A <see cref="Type"/> that represents the type of enumeration to associate with this enumeration converter.</param>
		public EnumDescriptionTypeConverter(Type type)
			: base(type) {
			this.type = type;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Converts the specified value object to an enumeration object.
		/// </summary>
		/// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
		/// <param name="culture">An optional <see cref="CultureInfo"/>. If not supplied, the current culture is assumed.</param>
		/// <param name="value">The <see cref="Object"/> to convert.</param>
		/// <returns>
		/// An <see cref="Object"/> that represents the converted <paramref name="value"/>.
		/// </returns>
		/// <exception cref="FormatException"><paramref name="value"/> is not a valid value for the target type. </exception>
		/// <exception cref="NotSupportedException">The conversion cannot be performed. </exception>
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) {
			string valueString = value as string;
			if (null != valueString)
				return GetValue(this.type, valueString);

			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>
		/// Converts the given value object to the specified destination type.
		/// </summary>
		/// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
		/// <param name="culture">An optional <see cref="CultureInfo"/>. If not supplied, the current culture is assumed.</param>
		/// <param name="value">The <see cref="Object"/> to convert.</param>
		/// <param name="destinationType">The <see cref="Type"/> to convert the value to.</param>
		/// <returns>
		/// An <see cref="Object"/> that represents the converted <paramref name="value"/>.
		/// </returns>
		/// <exception cref="ArgumentNullException"><paramref name="destinationType"/> is null. </exception>
		/// <exception cref="ArgumentException"><paramref name="value"/> is not a valid value for the enumeration. </exception>
		/// <exception cref="NotSupportedException">The conversion cannot be performed. </exception>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) {
			if (destinationType == null)
				throw new ArgumentNullException("destinationType");

			if (null != value && destinationType == typeof(string))
				return GetDescription(this.type, value.ToString());

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
		public static String GetDescription(Type type, string fieldName) {
			if (null != type && null != fieldName) {
				FieldInfo fieldInfo = type.GetField(fieldName);
				if (null != fieldInfo) {
					DescriptionAttribute[] attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
					if (null != attributes && 0 != attributes.Length)
						return attributes[0].Description;
				}

				return fieldName;
			}

			return string.Empty;
		}

		/// <summary>
		/// Gets the value of an Enum, based on it's DescriptionAttribute or named value.
		/// </summary>
		/// <param name="type">The enumeration type to get the value for.</param>
		/// <param name="description">The description or name of the element.</param>
		/// <returns>The value, or the passed in description, if it was not found.</returns>
		public static Object GetValue(Type type, String description) {
			FieldInfo[] fieldInfos = type.GetFields();
			foreach (FieldInfo fieldInfo in fieldInfos) {
				DescriptionAttribute[] attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
				if (null != attributes && 0 != attributes.Length) {
					if (attributes[0].Description == description)
						return fieldInfo.GetValue(fieldInfo.Name);
				}

				if (fieldInfo.Name == description)
					return fieldInfo.GetValue(fieldInfo.Name);
			}

			return description;
		}

	}

}
