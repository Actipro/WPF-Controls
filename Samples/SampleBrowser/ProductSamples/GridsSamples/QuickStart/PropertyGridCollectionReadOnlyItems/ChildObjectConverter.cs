using System;
using System.ComponentModel;
using System.Globalization;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCollectionReadOnlyItems {
	
	/// <summary>
	/// Represents a type converter for <see cref="ChildObject"/> that derives from <c>TypeConverter</c>.
	/// </summary>
	public class ChildObjectConverter : ExpandableObjectConverter {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.
		/// </summary>
		/// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
		/// <param name="sourceType">A <see cref="Type"/> that represents the type you want to convert from.</param>
		/// <returns>
		/// <c>true</c> if this converter can perform the conversion; otherwise, <c>false</c>.
		/// </returns>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) {
			if (sourceType == typeof(string))
				return true;
			return base.CanConvertFrom(context, sourceType);
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
		/// <exception cref="NotSupportedException">
		/// The conversion cannot be performed.
		/// </exception>
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) {
			string str = value as string;
			if (null != str)
				return new ChildObject() { Name = str };

			return base.ConvertFrom(context, culture, value);
		}

	}

}
