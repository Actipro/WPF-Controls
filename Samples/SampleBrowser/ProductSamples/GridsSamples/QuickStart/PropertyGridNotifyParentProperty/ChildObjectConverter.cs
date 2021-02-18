using System;
using System.ComponentModel;
using System.Globalization;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridNotifyParentProperty {

	/// <summary>
	/// Represents a <c>TypeConverter</c> for the <see cref="ChildObject"/> type.
	/// </summary>
	public class ChildObjectConverter : ExpandableObjectConverter {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

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
			if (destinationType == typeof(string)) {
				ChildObject child = (ChildObject)value;
				return string.Format("Will={0}, WillNot={1}", child.WillNotify, child.WillNotNotify);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

	}

}
