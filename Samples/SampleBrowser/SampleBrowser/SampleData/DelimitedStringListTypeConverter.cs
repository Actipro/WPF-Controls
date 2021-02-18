using System;
using System.ComponentModel;
using System.Globalization;

namespace ActiproSoftware.SampleBrowser.SampleData {

	/// <summary>
	/// Converts a delimited string to a list of strings.
	/// </summary>
	public class DelimitedStringListTypeConverter : TypeConverter {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Converts a value.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <param name="culture">The culture.</param>
		/// <param name="value">The value to convert.</param>
		/// <returns>The converted result.</returns>
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) {
			var delimitedString = value as string;
			if (!string.IsNullOrEmpty(delimitedString))
				return delimitedString.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>
		/// Determines whether a conversion from a type can occur.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <param name="sourceType">The source <see cref="Type"/>.</param>
		/// <returns>
		/// <c>true</c> if a conversion can occur; otherwise, <c>false</c>.
		/// </returns>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) {
			if (sourceType == typeof(string))
				return true;

			return base.CanConvertFrom(context, sourceType);
		}

	}

}
