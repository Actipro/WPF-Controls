using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid.Primitives;

namespace ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid {

	/// <summary>
	/// Represents a data-bound column for use in a <c>DataGrid</c> that utilizes the <see cref="Int16EditBox"/> control.
	/// </summary>
	public class DataGridInt16Column : DataGridPartEditBoxColumnBase<Int16?> {
	
		#region Dependency Properties
		
		/// <summary>
		/// Identifies the <see cref="DefaultValue"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="DefaultValue"/> dependency property.</value>
		public static readonly DependencyProperty DefaultValueProperty = DependencyProperty.Register("DefaultValue", typeof(Int16), typeof(DataGridInt16Column), new PropertyMetadata((Int16)0));
		
		/// <summary>
		/// Identifies the <see cref="Format"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="Format"/> dependency property.</value>
		public static readonly DependencyProperty FormatProperty = DependencyProperty.Register("Format", typeof(string), typeof(DataGridInt16Column), new PropertyMetadata("D"));
		
		/// <summary>
		/// Identifies the <see cref="LargeChange"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="LargeChange"/> dependency property.</value>
		public static readonly DependencyProperty LargeChangeProperty = DependencyProperty.Register("LargeChange", typeof(Int16), typeof(DataGridInt16Column), new PropertyMetadata((Int16)5));
		
		/// <summary>
		/// Identifies the <see cref="Maximum"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="Maximum"/> dependency property.</value>
		public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof(Int16), typeof(DataGridInt16Column), new PropertyMetadata(Int16.MaxValue));
		
		/// <summary>
		/// Identifies the <see cref="Minimum"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="Minimum"/> dependency property.</value>
		public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof(Int16), typeof(DataGridInt16Column), new PropertyMetadata(Int16.MinValue));
		
		/// <summary>
		/// Identifies the <see cref="PickerKind"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="PickerKind"/> dependency property.</value>
		public static readonly DependencyProperty PickerKindProperty = DependencyProperty.Register("PickerKind", typeof(Int16EditBoxPickerKind), typeof(DataGridInt16Column), new PropertyMetadata(Int16EditBoxPickerKind.Default));
		
		/// <summary>
		/// Identifies the <see cref="SmallChange"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="SmallChange"/> dependency property.</value>
		public static readonly DependencyProperty SmallChangeProperty = DependencyProperty.Register("SmallChange", typeof(Int16), typeof(DataGridInt16Column), new PropertyMetadata((Int16)1));
		
		#endregion

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the <see cref="DataGridInt16Column"/> class.
		/// </summary>
		public DataGridInt16Column() {
			this.HasPopup = false;
			this.IsArrowKeyPartNavigationEnabled = false;
			this.PickerKind = Int16EditBoxPickerKind.Calculator;
			this.SpinnerVisibility = SpinnerVisibility.VisibleWhenActive;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Applies standard values to the specified target element.
		/// </summary>
		/// <param name="targetElement">The target element.</param>
		protected override void ApplyStandardValues(FrameworkElement targetElement) {
			base.ApplyStandardValues(targetElement);
			if (targetElement is Int16EditBox) {
				this.ApplyValue(DefaultValueProperty, targetElement, Int16EditBox.DefaultValueProperty);
				this.ApplyValue(FormatProperty, targetElement, Int16EditBox.FormatProperty);
				this.ApplyValue(LargeChangeProperty, targetElement, Int16EditBox.LargeChangeProperty);
				this.ApplyValue(MaximumProperty, targetElement, Int16EditBox.MaximumProperty);
				this.ApplyValue(MinimumProperty, targetElement, Int16EditBox.MinimumProperty);
				this.ApplyValue(PickerKindProperty, targetElement, Int16EditBox.PickerKindProperty);
				this.ApplyValue(SmallChangeProperty, targetElement, Int16EditBox.SmallChangeProperty);
			}
		}
		
		/// <summary>
		/// Gets or sets the value to set when incrementing/decrementing from a null value.
		/// </summary>
		/// <value>
		/// The value to set when incrementing/decrementing from a null value.
		/// The default value is <c>0</c>.
		/// </value>
		public Int16 DefaultValue {
			get {
				return (Int16)this.GetValue(DefaultValueProperty);
			}
			set {
				this.SetValue(DefaultValueProperty, value);
			}
		}
		
		/// <summary>
		/// Gets or sets the number format string.
		/// </summary>
		/// <value>
		/// The number format string.
		/// The default value is <c>"D"</c>.
		/// </value>
		public string Format {
			get {
				return (string)this.GetValue(FormatProperty);
			}
			set {
				this.SetValue(FormatProperty, value);
			}
		}
		
		/// <summary>
		/// Gets the type of the associated <c>PartEditBoxBase</c>-derived control.
		/// </summary>
		/// <returns>The type of the associated <c>PartEditBoxBase</c>-derived control.</returns>
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		protected override Type GetEditBoxType() {
			return typeof(Int16EditBox);
		}
		
		/// <summary>
		/// Gets or sets the large change value.
		/// </summary>
		/// <value>
		/// The large change value.
		/// The default value is <c>5</c>.
		/// </value>
		public Int16 LargeChange {
			get {
				return (Int16)this.GetValue(LargeChangeProperty);
			}
			set {
				this.SetValue(LargeChangeProperty, value);
			}
		}
		
		/// <summary>
		/// Gets or sets the highest possible value.
		/// </summary>
		/// <value>
		/// The highest possible value.
		/// </value>
		public Int16 Maximum {
			get {
				return (Int16)this.GetValue(MaximumProperty);
			}
			set {
				this.SetValue(MaximumProperty, value);
			}
		}
		
		/// <summary>
		/// Gets or sets the lowest possible value.
		/// </summary>
		/// <value>
		/// The lowest possible value.
		/// </value>
		public Int16 Minimum {
			get {
				return (Int16)this.GetValue(MinimumProperty);
			}
			set {
				this.SetValue(MinimumProperty, value);
			}
		}
		
		/// <summary>
		/// Gets or sets an <see cref="Int16EditBoxPickerKind"/> indicating the pre-defined <c>Style</c> to apply to the picker used within the popup.
		/// </summary>
		/// <value>
		/// An <see cref="Int16EditBoxPickerKind"/> indicating the pre-defined <c>Style</c> to apply to the picker used within the popup.
		/// The default value is <c>Calculator</c> in WPF and <c>Default</c> in UWP.
		/// </value>
		public Int16EditBoxPickerKind PickerKind {
			get {
				return (Int16EditBoxPickerKind)this.GetValue(PickerKindProperty);
			}
			set {
				this.SetValue(PickerKindProperty, value);
			}
		}
		
		/// <summary>
		/// Gets or sets the small change value.
		/// </summary>
		/// <value>
		/// The small change value.
		/// The default value is <c>1</c>.
		/// </value>
		public Int16 SmallChange {
			get {
				return (Int16)this.GetValue(SmallChangeProperty);
			}
			set {
				this.SetValue(SmallChangeProperty, value);
			}
		}
		
	}

}
