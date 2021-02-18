using System;
using System.ComponentModel;
using System.Reflection;

#if WINRT
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using ActiproSoftware.UI.Xaml.Controls.Editors.Interop.Grids.PropertyEditors;
using ActiproSoftware.UI.Xaml.Controls.Grids.PropertyEditors;
#else
using System.Windows;
using System.Windows.Media;
using ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors;
using ActiproSoftware.Windows.Controls.Grids.PropertyEditors;
#endif

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridEditorsIntegration {
	
	/// <summary>
	/// Represents a test object for demonstration purposes.
	/// </summary>
	public class TestObject {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>TestObject</c> class.
		/// </summary>
		public TestObject() {
			this.ColorValue = Colors.Red;
			this.DateLongValue = DateTime.Now;
			this.DateValue = DateTime.Now;
			this.DateTimeValue = DateTime.Now;
			this.TimeValue = DateTime.Now;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// SYSTEM PROPERTIES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		#if !WINRT
		[Category("System Properties")]
		[DisplayName("Byte")]
		public byte ByteValue { get; set; }

		[Category("System Properties")]
		[DisplayName("Byte?")]
		public byte? ByteNullableValue { get; set; }
		#endif

		// This property's editor is set with EditorAttribute
		[Category("System Properties")]
		[DisplayName("DateTime (date-only)")]
		[Editor(typeof(DatePropertyEditor), typeof(PropertyEditor))]
		public DateTime DateValue { get; set; }

		// This property's editor is set with EditorAttribute
		[Category("System Properties")]
		[DisplayName("DateTime? (date-only)")]
		[Editor(typeof(NullableDatePropertyEditor), typeof(PropertyEditor))]
		public DateTime? DateNullableValue { get; set; }
		
		// This property's editor is set in XAML
		[Category("System Properties")]
		[DisplayName("DateTime (date-only, long format)")]
		public DateTime DateLongValue { get; set; }
		
		[Category("System Properties")]
		[DisplayName("DateTime")]
		public DateTime DateTimeValue { get; set; }

		[Category("System Properties")]
		[DisplayName("DateTime?")]
		public DateTime? DateTimeNullableValue { get; set; }

		[Category("System Properties")]
		[DisplayName("Double")]
		public Double DoubleValue { get; set; }

		[Category("System Properties")]
		[DisplayName("Double?")]
		public Double? DoubleNullableValue { get; set; }
		
		[Category("System Properties")]
		[DisplayName("Enum")]
		public ProcessorArchitecture EnumValue { get; set; }

		[Category("System Properties")]
		[DisplayName("Enum (flags)")]
		public BindingFlags FlagsEnumValue { get; set; }

		[Category("System Properties")]
		[DisplayName("Guid")]
		public Guid GuidValue { get; set; }

		[Category("System Properties")]
		[DisplayName("Guid?")]
		public Guid? GuidNullableValue { get; set; }

		[Category("System Properties")]
		[DisplayName("Int16")]
		public Int16 Int16Value { get; set; }

		[Category("System Properties")]
		[DisplayName("Int16?")]
		public Int16? Int16NullableValue { get; set; }

		[Category("System Properties")]
		[DisplayName("Int32")]
		public Int32 Int32Value { get; set; }

		[Category("System Properties")]
		[DisplayName("Int32?")]
		public Int32? Int32NullableValue { get; set; }

		[Category("System Properties")]
		[DisplayName("Int64")]
		public Int64 Int64Value { get; set; }

		[Category("System Properties")]
		[DisplayName("Int64?")]
		public Int64? Int64NullableValue { get; set; }

		[Category("System Properties")]
		[DisplayName("Masked String")]
		public string MaskedString { get; set; }

		[Category("System Properties")]
		[DisplayName("Single")]
		public Single SingleValue { get; set; }

		[Category("System Properties")]
		[DisplayName("Single?")]
		public Single? SingleNullableValue { get; set; }

		// This property's editor is set with EditorAttribute
		[Category("System Properties")]
		[DisplayName("DateTime (time-only)")]
		[Editor(typeof(TimePropertyEditor), typeof(PropertyEditor))]
		public DateTime TimeValue { get; set; }

		// This property's editor is set with EditorAttribute
		[Category("System Properties")]
		[DisplayName("DateTime? (time-only)")]
		[Editor(typeof(NullableTimePropertyEditor), typeof(PropertyEditor))]
		public DateTime? TimeNullableValue { get; set; }

		[Category("System Properties")]
		[DisplayName("TimeSpan")]
		public TimeSpan TimeSpanValue { get; set; }

		[Category("System Properties")]
		[DisplayName("TimeSpan?")]
		public TimeSpan? TimeSpanNullableValue { get; set; }
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PLATFORM PROPERTIES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		[Category("Platform Properties")]
		[DisplayName("Brush")]
		public Brush BrushValue { get; set; }

		[Category("Platform Properties")]
		[DisplayName("Color")]
		public Color ColorValue { get; set; }

		[Category("Platform Properties")]
		[DisplayName("Color?")]
		public Color? ColorNullableValue { get; set; }

		[Category("Platform Properties")]
		[DisplayName("CornerRadius")]
		public CornerRadius CornerRadiusValue { get; set; }

		[Category("Platform Properties")]
		[DisplayName("CornerRadius?")]
		public CornerRadius? CornerRadiusNullableValue { get; set; }
		
		#if !WINRT
		[Category("Platform Properties")]
		[DisplayName("Int32Rect")]
		public Int32Rect Int32RectValue { get; set; }

		[Category("Platform Properties")]
		[DisplayName("Int32Rect?")]
		public Int32Rect? Int32RectNullableValue { get; set; }
		#endif
		
		[Category("Platform Properties")]
		[DisplayName("Point")]
		public Point PointValue { get; set; }

		[Category("Platform Properties")]
		[DisplayName("Point?")]
		public Point? PointNullableValue { get; set; }

		[Category("Platform Properties")]
		[DisplayName("Rect")]
		public Rect RectValue { get; set; }

		[Category("Platform Properties")]
		[DisplayName("Rect?")]
		public Rect? RectNullableValue { get; set; }

		[Category("Platform Properties")]
		[DisplayName("Size")]
		public Size SizeValue { get; set; }

		[Category("Platform Properties")]
		[DisplayName("Size?")]
		public Size? SizeNullableValue { get; set; }

		[Category("Platform Properties")]
		[DisplayName("Thickness")]
		public Thickness ThicknessValue { get; set; }

		[Category("Platform Properties")]
		[DisplayName("Thickness?")]
		public Thickness? ThicknessNullableValue { get; set; }
		
		#if !WINRT
		[Category("Platform Properties")]
		[DisplayName("Vector")]
		public Vector VectorValue { get; set; }

		[Category("Platform Properties")]
		[DisplayName("Vector?")]
		public Vector? VectorNullableValue { get; set; }
		#endif
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-CLS COMPLIANT SYSTEM PROPERTIES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		#if !WINRT
		[Category("System Properties (Non-CLS Compliant)")]
		[DisplayName("SByte")]
		public SByte SByteValue { get; set; }

		[Category("System Properties (Non-CLS Compliant)")]
		[DisplayName("UInt16")]
		public UInt16 UInt16Value { get; set; }

		[Category("System Properties (Non-CLS Compliant)")]
		[DisplayName("UInt32")]
		public UInt32 UInt32Value { get; set; }
		#endif

	}

}
