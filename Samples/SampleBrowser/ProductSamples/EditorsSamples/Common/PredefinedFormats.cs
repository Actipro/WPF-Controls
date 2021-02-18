using System.Collections.Generic;

namespace ActiproSoftware.ProductSamples.EditorsSamples.Common {

	/// <summary>
	/// Represents a predefined format.
	/// </summary>
	public static class PredefinedFormats {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the <c>Int16</c> predefined formats.
		/// </summary>
		/// <value>The predefined formats.</value>
		public static IEnumerable<PredefinedFormat> Byte {
			get {
				return new PredefinedFormat[] {
					new PredefinedFormat("Decimal", "D"),
					new PredefinedFormat("Decimal (2 digit minimum)", "D2"),
					new PredefinedFormat("Decimal (4 digit minimum)", "D3"),
					new PredefinedFormat("General", "G"),
					new PredefinedFormat("Hexadecimal (uppercase)", @"X"),
					new PredefinedFormat("Hexadecimal (uppercase, 2 digit minimum)", @"X2"),
					new PredefinedFormat("Hexadecimal (lowercase)", @"x"),
					new PredefinedFormat("Hexadecimal (lowercase, 2 digit minimum)", @"x2"),
					new PredefinedFormat("Number", "N0"),
				};
			}
		}
		
		/// <summary>
		/// Gets the <c>CornerRadius</c> predefined formats.
		/// </summary>
		/// <value>The predefined formats.</value>
		public static IEnumerable<PredefinedFormat> CornerRadius {
			get {
				return DoubleBase;
			}
		}
		
		/// <summary>
		/// Gets the <c>Date</c> predefined formats.
		/// </summary>
		/// <value>The predefined formats.</value>
		public static IEnumerable<PredefinedFormat> Date {
			get {
				return new PredefinedFormat[] {
					new PredefinedFormat("Short date", "d"),
					new PredefinedFormat("Long date", "D"),
					new PredefinedFormat("Month/day", "m"),
					new PredefinedFormat("Year/month", "y"),
					new PredefinedFormat("Custom (MM/dd/yyyy)", "MM/dd/yyyy"),
					new PredefinedFormat("Custom (MM/dd/yy)", "MM/dd/yy"),
					new PredefinedFormat("Custom (yyyy-MM-dd)", @"yyyy-MM-dd"),
					new PredefinedFormat("Custom (d MMMM yyyy)", @"d MMMM yyyy"),
					new PredefinedFormat("Custom (d MMM yyyy)", @"d MMM yyyy"),
					new PredefinedFormat("Custom (dd.MM.yyyy)", @"dd.MM.yyyy"),
					new PredefinedFormat("Custom (d.M.yyyy)", "d.M.yyyy"),
				};
			}
		}

		/// <summary>
		/// Gets the <c>DateTime</c> predefined formats.
		/// </summary>
		/// <value>The predefined formats.</value>
		public static IEnumerable<PredefinedFormat> DateTime {
			get {
				return new PredefinedFormat[] {
					new PredefinedFormat("General (short time)", "g"),
					new PredefinedFormat("General (long time)", "G"),
					new PredefinedFormat("Full (short time)", "f"),
					new PredefinedFormat("Full (long time)", "F"),
					new PredefinedFormat("Custom (MM/dd/yyyy hh:mm tt)", "MM/dd/yyyy hh:mm tt"),
					new PredefinedFormat("Custom (MM/dd/yy h:mm:ss)", "MM/dd/yy h:mm:ss"),
					new PredefinedFormat("Custom (yyyy-MM-dd HH:mm:ss)", @"yyyy-MM-dd HH:mm:ss"),
					new PredefinedFormat("Custom (d MMMM yyyy HH:mm)", @"d MMMM yyyy HH:mm"),
					new PredefinedFormat("Custom (d MMM yyyy HH:mm)", @"d MMM yyyy HH:mm"),
					new PredefinedFormat("Custom (dd.MM.yyyy HH:mm)", @"dd.MM.yyyy HH:mm"),
					new PredefinedFormat("Custom (d.M.yyyy HH:mm)", "d.M.yyyy HH:mm"),
				};
			}
		}
		
		/// <summary>
		/// Gets the <c>Double</c> predefined formats.
		/// </summary>
		/// <value>The predefined formats.</value>
		public static IEnumerable<PredefinedFormat> Double {
			get {
				return new PredefinedFormat[] {
					new PredefinedFormat("Currency", "C"),
					new PredefinedFormat("Fixed-point", "F"),
					new PredefinedFormat("Fixed-point (1 decimal digit)", "F1"),
					new PredefinedFormat("Fixed-point (2 decimal digits)", "F2"),
					new PredefinedFormat("Fixed-point (4 decimal digits)", "F4"),
					new PredefinedFormat("General", "G"),
					new PredefinedFormat("Number", @"N"),
					new PredefinedFormat("Number (1 decimal digit)", @"N1"),
					new PredefinedFormat("Number (2 decimal digits)", @"N2"),
					new PredefinedFormat("Number (4 decimal digits)", @"N4"),
					new PredefinedFormat("Percent", "P0"),
					new PredefinedFormat("Percent (2 decimal digits)", "P"),
				};
			}
		}
		
		/// <summary>
		/// Gets the <c>DoubleBase</c> predefined formats.
		/// </summary>
		/// <value>The predefined formats.</value>
		public static IEnumerable<PredefinedFormat> DoubleBase {
			get {
				return new PredefinedFormat[] {
					new PredefinedFormat("Fixed-point", "F"),
					new PredefinedFormat("Fixed-point (1 decimal digit)", "F1"),
					new PredefinedFormat("Fixed-point (2 decimal digits)", "F2"),
					new PredefinedFormat("Fixed-point (4 decimal digits)", "F4"),
					new PredefinedFormat("General", "G"),
				};
			}
		}

		/// <summary>
		/// Gets the <c>Guid</c> predefined formats.
		/// </summary>
		/// <value>The predefined formats.</value>
		public static IEnumerable<PredefinedFormat> Guid {
			get {
				return new PredefinedFormat[] {
					new PredefinedFormat("32 digits (uppercase)", "N"),
					new PredefinedFormat("32 digits (lowercase)", "n"),
					new PredefinedFormat("32 digits separated by hyphens (uppercase)", "D"),
					new PredefinedFormat("32 digits separated by hyphens (lowercase)", "d"),
					new PredefinedFormat("32 digits separated by hyphens, enclosed in braces (uppercase)", "B"),
					new PredefinedFormat("32 digits separated by hyphens, enclosed in braces (lowercase)", "b"),
					new PredefinedFormat("32 digits separated by hyphens, enclosed in parentheses (uppercase)", "P"),
					new PredefinedFormat("32 digits separated by hyphens, enclosed in parentheses (lowercase)", "p"),
				};
			}
		}
		
		/// <summary>
		/// Gets the <c>Int16</c> predefined formats.
		/// </summary>
		/// <value>The predefined formats.</value>
		public static IEnumerable<PredefinedFormat> Int16 {
			get {
				return Int32;
			}
		}
		
		/// <summary>
		/// Gets the <c>Int32</c> predefined formats.
		/// </summary>
		/// <value>The predefined formats.</value>
		public static IEnumerable<PredefinedFormat> Int32 {
			get {
				return new PredefinedFormat[] {
					new PredefinedFormat("Currency", "C0"),
					new PredefinedFormat("Decimal", "D"),
					new PredefinedFormat("Decimal (2 digit minimum)", "D2"),
					new PredefinedFormat("Decimal (4 digit minimum)", "D4"),
					new PredefinedFormat("Decimal (6 digit minimum)", "D6"),
					new PredefinedFormat("General", "G"),
					new PredefinedFormat("Hexadecimal (uppercase)", @"X"),
					new PredefinedFormat("Hexadecimal (uppercase, 4 digit minimum)", @"X4"),
					new PredefinedFormat("Hexadecimal (lowercase)", @"x"),
					new PredefinedFormat("Hexadecimal (lowercase, 4 digit minimum)", @"x4"),
					new PredefinedFormat("Number", "N0"),
					new PredefinedFormat("Conditional Formatting ('##;(##);Zero')", "##;(##);Zero"),
				};
			}
		}
		
		/// <summary>
		/// Gets the <c>Int32Rect</c> predefined formats.
		/// </summary>
		/// <value>The predefined formats.</value>
		public static IEnumerable<PredefinedFormat> Int32Rect {
			get {
				return new PredefinedFormat[] {
					new PredefinedFormat("Decimal", "D"),
					new PredefinedFormat("Decimal (2 digit minimum)", "D2"),
					new PredefinedFormat("Decimal (4 digit minimum)", "D4"),
					new PredefinedFormat("Decimal (6 digit minimum)", "D6"),
					new PredefinedFormat("General", "G"),
				};
			}
		}
		
		/// <summary>
		/// Gets the <c>Int32</c> predefined formats.
		/// </summary>
		/// <value>The predefined formats.</value>
		public static IEnumerable<PredefinedFormat> Int64 {
			get {
				return Int32;
			}
		}
		
		/// <summary>
		/// Gets the <c>Point</c> predefined formats.
		/// </summary>
		/// <value>The predefined formats.</value>
		public static IEnumerable<PredefinedFormat> Point {
			get {
				return DoubleBase;
			}
		}

		/// <summary>
		/// Gets the <c>Rect</c> predefined formats.
		/// </summary>
		/// <value>The predefined formats.</value>
		public static IEnumerable<PredefinedFormat> Rect {
			get {
				return DoubleBase;
			}
		}
		
		/// <summary>
		/// Gets the <c>Single</c> predefined formats.
		/// </summary>
		/// <value>The predefined formats.</value>
		public static IEnumerable<PredefinedFormat> Single {
			get {
				return Double;
			}
		}
		
		/// <summary>
		/// Gets the <c>Size</c> predefined formats.
		/// </summary>
		/// <value>The predefined formats.</value>
		public static IEnumerable<PredefinedFormat> Size {
			get {
				return DoubleBase;
			}
		}

		/// <summary>
		/// Gets the <c>Thickness</c> predefined formats.
		/// </summary>
		/// <value>The predefined formats.</value>
		public static IEnumerable<PredefinedFormat> Thickness {
			get {
				return DoubleBase;
			}
		}
		
		/// <summary>
		/// Gets the <c>Time</c> predefined formats.
		/// </summary>
		/// <value>The predefined formats.</value>
		public static IEnumerable<PredefinedFormat> Time {
			get {
				return new PredefinedFormat[] {
					new PredefinedFormat("Short time", "t"),
					new PredefinedFormat("Long time", "T"),
					new PredefinedFormat("Custom (hh:mm tt)", "hh:mm tt"),
					new PredefinedFormat("Custom (h:mm:ss)", "h:mm:ss"),
					new PredefinedFormat("Custom (HH:mm:ss)", @"HH:mm:ss"),
					new PredefinedFormat("Custom (HH:mm)", @"HH:mm"),
				};
			}
		}
		
		/// <summary>
		/// Gets the <c>Time</c> predefined formats.
		/// </summary>
		/// <value>The predefined formats.</value>
		public static IEnumerable<PredefinedFormat> TimeSpan {
			get {
				return new PredefinedFormat[] {
					new PredefinedFormat("Constant (invariant)", "c"),
					new PredefinedFormat("General (short)", "g"),
					new PredefinedFormat("General (long)", "G"),
					new PredefinedFormat(@"Custom (hh\h mm\m ss\s)", @"hh\h mm\m ss\s"),
				};
			}
		}
		
		/// <summary>
		/// Gets the <c>Vector</c> predefined formats.
		/// </summary>
		/// <value>The predefined formats.</value>
		public static IEnumerable<PredefinedFormat> Vector {
			get {
				return DoubleBase;
			}
		}

	}

}
