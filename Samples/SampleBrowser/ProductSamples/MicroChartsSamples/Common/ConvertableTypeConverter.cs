using System;
using System.ComponentModel;
using System.Globalization;

namespace ActiproSoftware.ProductSamples.MicroChartsSamples.Common {

	/// <summary>
	/// Implements a <see cref="TypeConverter"/> that allows any <see cref="IConvertable"/> type to be converted to a <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">The target type.</typeparam>
	public class ConvertibleTypeConverter<T> : TypeConverter where T : IConvertible {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Returns whether this converter can convert an object of the given type to the type of this converter, using the
		/// specified context.
		/// </summary>
		/// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
		/// <param name="sourceType">A <see cref="Type"/> that represents the type you want to convert from.</param>
		/// <returns>
		/// <c>true</c> if this converter can perform the conversion; otherwise, <c>false</c>.
		/// </returns>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) {
			return sourceType.GetInterface("IConvertible", false) != null;
		}

		/// <summary>
		/// Returns whether this converter can convert the object to the specified type, using the specified context.
		/// </summary>
		/// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
		/// <param name="destinationType">A <see cref="Type"/> that represents the type you want to convert to.</param>
		/// <returns>
		/// <c>true</c> if this converter can perform the conversion; otherwise, <c>false</c>.
		/// </returns>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) {
			return destinationType.GetInterface("IConvertible", false) != null;
		}

		/// <summary>
		/// Converts the given object to the type of this converter, using the specified context and culture information.
		/// </summary>
		/// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
		/// <param name="culture">The <see cref="CultureInfo"/> to use as the current culture.</param>
		/// <param name="value">The <see cref="Object"/> to convert.</param>
		/// <returns>
		/// An <see cref="Object"/> that represents the converted value.
		/// </returns>
		/// <exception cref="NotSupportedException">The conversion cannot be performed. </exception>
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) {
			return ((IConvertible)value).ToType(typeof(T), culture);
		}

		/// <summary>
		/// Converts the given value object to the specified type, using the specified context and culture information.
		/// </summary>
		/// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
		/// <param name="culture">A <see cref="CultureInfo"/>. If null is passed, the current culture is assumed.</param>
		/// <param name="value">The <see cref="Object"/> to convert.</param>
		/// <param name="destinationType">The <see cref="Type"/> to convert the <paramref name="value"/> parameter to.</param>
		/// <returns>
		/// An <see cref="Object"/> that represents the converted value.
		/// </returns>
		/// <exception cref="ArgumentNullException">The <paramref name="destinationType"/> parameter is null. </exception>
		/// <exception cref="NotSupportedException">The conversion cannot be performed. </exception>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) {
			return ((IConvertible)value).ToType(destinationType, culture);
		}

	}

}
