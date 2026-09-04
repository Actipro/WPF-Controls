namespace ActiproSoftware.ProductSamples.EditorsSamples.Common;

/// <summary>
/// Represents a predefined format.
/// </summary>
public static class PredefinedFormats {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <c>Int16</c> predefined formats.
	/// </summary>
	public static IEnumerable<PredefinedFormat> Byte {
		get => [
			new("Decimal", "D"),
			new("Decimal (2 digit minimum)", "D2"),
			new("Decimal (4 digit minimum)", "D3"),
			new("General", "G"),
			new("Hexadecimal (uppercase)", @"X"),
			new("Hexadecimal (uppercase, 2 digit minimum)", @"X2"),
			new("Hexadecimal (lowercase)", @"x"),
			new("Hexadecimal (lowercase, 2 digit minimum)", @"x2"),
			new("Number", "N0"),
		];
	}

	/// <summary>
	/// The <c>CornerRadius</c> predefined formats.
	/// </summary>
	public static IEnumerable<PredefinedFormat> CornerRadius
		=> DoubleBase;

	/// <summary>
	/// The <c>Date</c> predefined formats.
	/// </summary>
	public static IEnumerable<PredefinedFormat> Date {
		get => [
			new("Short date", "d"),
			new("Long date", "D"),
			new("Month/day", "m"),
			new("Year/month", "y"),
			new("Custom (MM/dd/yyyy)", "MM/dd/yyyy"),
			new("Custom (MM/dd/yy)", "MM/dd/yy"),
			new("Custom (yyyy-MM-dd)", @"yyyy-MM-dd"),
			new("Custom (d MMMM yyyy)", @"d MMMM yyyy"),
			new("Custom (d MMM yyyy)", @"d MMM yyyy"),
			new("Custom (dd.MM.yyyy)", @"dd.MM.yyyy"),
			new("Custom (d.M.yyyy)", "d.M.yyyy"),
		];
	}

	/// <summary>
	/// The <c>DateTime</c> predefined formats.
	/// </summary>
	public static IEnumerable<PredefinedFormat> DateTime {
		get => [
			new("General (short time)", "g"),
			new("General (long time)", "G"),
			new("Full (short time)", "f"),
			new("Full (long time)", "F"),
			new("Custom (MM/dd/yyyy hh:mm tt)", "MM/dd/yyyy hh:mm tt"),
			new("Custom (MM/dd/yy h:mm:ss)", "MM/dd/yy h:mm:ss"),
			new("Custom (yyyy-MM-dd HH:mm:ss)", @"yyyy-MM-dd HH:mm:ss"),
			new("Custom (d MMMM yyyy HH:mm)", @"d MMMM yyyy HH:mm"),
			new("Custom (d MMM yyyy HH:mm)", @"d MMM yyyy HH:mm"),
			new("Custom (dd.MM.yyyy HH:mm)", @"dd.MM.yyyy HH:mm"),
			new("Custom (d.M.yyyy HH:mm)", "d.M.yyyy HH:mm"),
		];
	}

	/// <summary>
	/// The <c>Decimal</c> predefined formats.
	/// </summary>
	public static IEnumerable<PredefinedFormat> Decimal
		=> Double;

	/// <summary>
	/// The <c>Double</c> predefined formats.
	/// </summary>
	public static IEnumerable<PredefinedFormat> Double {
		get => [
			new("Currency", "C"),
			new("Fixed-point", "F"),
			new("Fixed-point (1 decimal digit)", "F1"),
			new("Fixed-point (2 decimal digits)", "F2"),
			new("Fixed-point (4 decimal digits)", "F4"),
			new("General", "G"),
			new("Number", @"N"),
			new("Number (1 decimal digit)", @"N1"),
			new("Number (2 decimal digits)", @"N2"),
			new("Number (4 decimal digits)", @"N4"),
			new("Percent", "P0"),
			new("Percent (2 decimal digits)", "P"),
		];
	}

	/// <summary>
	/// The <c>DoubleBase</c> predefined formats.
	/// </summary>
	public static IEnumerable<PredefinedFormat> DoubleBase {
		get => [
			new("Fixed-point", "F"),
			new("Fixed-point (1 decimal digit)", "F1"),
			new("Fixed-point (2 decimal digits)", "F2"),
			new("Fixed-point (4 decimal digits)", "F4"),
			new("General", "G"),
		];
	}

	/// <summary>
	/// The <c>Guid</c> predefined formats.
	/// </summary>
	public static IEnumerable<PredefinedFormat> Guid {
		get => [
			new("32 digits (uppercase)", "N"),
			new("32 digits (lowercase)", "n"),
			new("32 digits separated by hyphens (uppercase)", "D"),
			new("32 digits separated by hyphens (lowercase)", "d"),
			new("32 digits separated by hyphens, enclosed in braces (uppercase)", "B"),
			new("32 digits separated by hyphens, enclosed in braces (lowercase)", "b"),
			new("32 digits separated by hyphens, enclosed in parentheses (uppercase)", "P"),
			new("32 digits separated by hyphens, enclosed in parentheses (lowercase)", "p"),
		];
	}

	/// <summary>
	/// The <c>Int16</c> predefined formats.
	/// </summary>
	public static IEnumerable<PredefinedFormat> Int16
		=> Int32;

	/// <summary>
	/// The <c>Int32</c> predefined formats.
	/// </summary>
	/// <value>The predefined formats.</value>
	public static IEnumerable<PredefinedFormat> Int32 {
		get => [
			new("Currency", "C0"),
			new("Decimal", "D"),
			new("Decimal (2 digit minimum)", "D2"),
			new("Decimal (4 digit minimum)", "D4"),
			new("Decimal (6 digit minimum)", "D6"),
			new("General", "G"),
			new("Hexadecimal (uppercase)", @"X"),
			new("Hexadecimal (uppercase, 4 digit minimum)", @"X4"),
			new("Hexadecimal (lowercase)", @"x"),
			new("Hexadecimal (lowercase, 4 digit minimum)", @"x4"),
			new("Number", "N0"),
			new("Conditional Formatting ('##;(##);Zero')", "##;(##);Zero"),
		];
	}

	/// <summary>
	/// The <c>Int32Rect</c> predefined formats.
	/// </summary>
	public static IEnumerable<PredefinedFormat> Int32Rect {
		get => [
			new("Decimal", "D"),
			new("Decimal (2 digit minimum)", "D2"),
			new("Decimal (4 digit minimum)", "D4"),
			new("Decimal (6 digit minimum)", "D6"),
			new("General", "G"),
		];
	}

	/// <summary>
	/// The <c>Int32</c> predefined formats.
	/// </summary>
	public static IEnumerable<PredefinedFormat> Int64
		=> Int32;

	/// <summary>
	/// The <c>Point</c> predefined formats.
	/// </summary>
	public static IEnumerable<PredefinedFormat> Point
		=> DoubleBase;

	/// <summary>
	/// The <c>Rect</c> predefined formats.
	/// </summary>
	public static IEnumerable<PredefinedFormat> Rect
		=> DoubleBase;

	/// <summary>
	/// The <c>Single</c> predefined formats.
	/// </summary>
	public static IEnumerable<PredefinedFormat> Single
		=> Double;

	/// <summary>
	/// The <c>Size</c> predefined formats.
	/// </summary>
	public static IEnumerable<PredefinedFormat> Size
		=> DoubleBase;

	/// <summary>
	/// The <c>Thickness</c> predefined formats.
	/// </summary>
	public static IEnumerable<PredefinedFormat> Thickness
		=> DoubleBase;

	/// <summary>
	/// The <c>Time</c> predefined formats.
	/// </summary>
	public static IEnumerable<PredefinedFormat> Time {
		get => [
			new("Short time", "t"),
			new("Long time", "T"),
			new("Custom (hh:mm tt)", "hh:mm tt"),
			new("Custom (h:mm:ss)", "h:mm:ss"),
			new("Custom (HH:mm:ss)", @"HH:mm:ss"),
			new("Custom (HH:mm)", @"HH:mm"),
		];
	}

	/// <summary>
	/// The <c>TimeSpan</c> predefined formats.
	/// </summary>
	public static IEnumerable<PredefinedFormat> TimeSpan {
		get => [
			new("Constant (invariant)", "c"),
			new("General (short)", "g"),
			new("General (long)", "G"),
			new(@"Custom (hh:mm)", @"hh:mm"),
			new(@"Custom (+hh:mm)", @"+hh:mm"),
			new(@"Custom (hh\h mm\m ss\s)", @"hh\h mm\m ss\s"),
			new(@"Custom (+dd:hh:mm:ss.fff)", @"+dd:hh:mm:ss.fff"),
		];
	}

	/// <summary>
	/// The <c>Vector</c> predefined formats.
	/// </summary>
	public static IEnumerable<PredefinedFormat> Vector
		=> DoubleBase;

}
