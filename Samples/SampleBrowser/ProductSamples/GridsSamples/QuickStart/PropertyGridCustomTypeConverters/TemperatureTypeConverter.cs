using System;
using System.ComponentModel;
using System.Globalization;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCustomTypeConverters {
	
	/// <summary>
	/// Represents a <see cref="DoubleConverter"/> that presents a value in Fahrenheit, but can accept values in
	/// Fahrenheit or Celsius.
	/// </summary>
	public class TemperatureTypeConverter : DoubleConverter {

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
			if (null != valueString) {
				valueString = valueString.Trim().ToUpperInvariant();

				// Determine if Fahrenheit or Celsius
				bool fahrenheit = true;
				if (valueString.Length > 0) {
					string scale = valueString.Substring(valueString.Length - 1);
					bool removeLastChar = false;
					if ("C" == scale) {
						fahrenheit = false;
						removeLastChar = true;
					}
					else if ("F" == scale) {
						removeLastChar = true;
					}

					if (removeLastChar) {
						if (valueString.Length > 1)
							valueString = valueString.Substring(0, valueString.Length - 1).Trim();
						else
							valueString = string.Empty;
					}
				}

				// Remove degree symbol, if it's there
				if (valueString.Length > 0) {
					string scale = valueString.Substring(valueString.Length - 1);
					if ("°" == scale) {
						if (valueString.Length > 1)
							valueString = valueString.Substring(0, valueString.Length - 1).Trim();
						else
							valueString = string.Empty;
					}
				}

				// Convert degrees portion using base class
				double degrees = 0.0;
				if (0 != valueString.Length)
					degrees = (double)base.ConvertFrom(context, culture, valueString);

				// Converter if needed
				if (!fahrenheit)
					degrees = degrees * 9 / 5 + 32.0;

				return degrees;
			}

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
				return string.Format("{0:#######0.0}° F", value);

			return base.ConvertTo(context, culture, value, destinationType);
		}

	}

}
