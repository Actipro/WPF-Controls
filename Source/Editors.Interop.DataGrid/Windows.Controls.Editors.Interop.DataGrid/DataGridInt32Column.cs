using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid.Primitives;

namespace ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid {

	/// <summary>
	/// Represents a data-bound column for use in a <c>DataGrid</c> that utilizes the <see cref="Int32EditBox"/> control.
	/// </summary>
	public class DataGridInt32Column : DataGridPartEditBoxColumnBase<Int32?> {
	
		#region Dependency Properties
		
		/// <summary>
		/// Identifies the <see cref="DefaultValue"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="DefaultValue"/> dependency property.</value>
		public static readonly DependencyProperty DefaultValueProperty = DependencyProperty.Register("DefaultValue", typeof(Int32), typeof(DataGridInt32Column), new PropertyMetadata((Int32)0, NotifyPropertyChangeForRefreshContent));
		
		/// <summary>
		/// Identifies the <see cref="Format"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="Format"/> dependency property.</value>
		public static readonly DependencyProperty FormatProperty = DependencyProperty.Register("Format", typeof(string), typeof(DataGridInt32Column), new PropertyMetadata("D", NotifyPropertyChangeForRefreshContent));
		
		/// <summary>
		/// Identifies the <see cref="LargeChange"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="LargeChange"/> dependency property.</value>
		public static readonly DependencyProperty LargeChangeProperty = DependencyProperty.Register("LargeChange", typeof(Int32), typeof(DataGridInt32Column), new PropertyMetadata((Int32)5, NotifyPropertyChangeForRefreshContent));
		
		/// <summary>
		/// Identifies the <see cref="Maximum"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="Maximum"/> dependency property.</value>
		public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof(Int32), typeof(DataGridInt32Column), new PropertyMetadata(Int32.MaxValue, NotifyPropertyChangeForRefreshContent));
		
		/// <summary>
		/// Identifies the <see cref="Minimum"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="Minimum"/> dependency property.</value>
		public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof(Int32), typeof(DataGridInt32Column), new PropertyMetadata(Int32.MinValue, NotifyPropertyChangeForRefreshContent));
		
		/// <summary>
		/// Identifies the <see cref="PickerKind"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="PickerKind"/> dependency property.</value>
		public static readonly DependencyProperty PickerKindProperty = DependencyProperty.Register("PickerKind", typeof(Int32EditBoxPickerKind), typeof(DataGridInt32Column), new PropertyMetadata(Int32EditBoxPickerKind.Default, NotifyPropertyChangeForRefreshContent));
		
		/// <summary>
		/// Identifies the <see cref="SmallChange"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="SmallChange"/> dependency property.</value>
		public static readonly DependencyProperty SmallChangeProperty = DependencyProperty.Register("SmallChange", typeof(Int32), typeof(DataGridInt32Column), new PropertyMetadata((Int32)1, NotifyPropertyChangeForRefreshContent));
		
		#endregion

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the <see cref="DataGridInt32Column"/> class.
		/// </summary>
		public DataGridInt32Column() {
			this.HasPopup = false;
			this.IsArrowKeyPartNavigationEnabled = false;
			this.PickerKind = Int32EditBoxPickerKind.Calculator;
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
			if (targetElement is Int32EditBox) {
				this.ApplyValue(DefaultValueProperty, targetElement, Int32EditBox.DefaultValueProperty);
				this.ApplyValue(FormatProperty, targetElement, Int32EditBox.FormatProperty);
				this.ApplyValue(LargeChangeProperty, targetElement, Int32EditBox.LargeChangeProperty);
				this.ApplyValue(MaximumProperty, targetElement, Int32EditBox.MaximumProperty);
				this.ApplyValue(MinimumProperty, targetElement, Int32EditBox.MinimumProperty);
				this.ApplyValue(PickerKindProperty, targetElement, Int32EditBox.PickerKindProperty);
				this.ApplyValue(SmallChangeProperty, targetElement, Int32EditBox.SmallChangeProperty);
			}
		}
		
		/// <summary>
		/// Gets or sets the value to set when incrementing/decrementing from a null value.
		/// </summary>
		/// <value>
		/// The value to set when incrementing/decrementing from a null value.
		/// The default value is <c>0</c>.
		/// </value>
		public Int32 DefaultValue {
			get {
				return (Int32)this.GetValue(DefaultValueProperty);
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
			return typeof(Int32EditBox);
		}
		
		/// <summary>
		/// Gets or sets the large change value.
		/// </summary>
		/// <value>
		/// The large change value.
		/// The default value is <c>5</c>.
		/// </value>
		public Int32 LargeChange {
			get {
				return (Int32)this.GetValue(LargeChangeProperty);
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
		public Int32 Maximum {
			get {
				return (Int32)this.GetValue(MaximumProperty);
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
		public Int32 Minimum {
			get {
				return (Int32)this.GetValue(MinimumProperty);
			}
			set {
				this.SetValue(MinimumProperty, value);
			}
		}
		
		/// <summary>
		/// Gets or sets an <see cref="Int32EditBoxPickerKind"/> indicating the pre-defined <c>Style</c> to apply to the picker used within the popup.
		/// </summary>
		/// <value>
		/// An <see cref="Int32EditBoxPickerKind"/> indicating the pre-defined <c>Style</c> to apply to the picker used within the popup.
		/// The default value is <c>Calculator</c> in WPF and <c>Default</c> in UWP.
		/// </value>
		public Int32EditBoxPickerKind PickerKind {
			get {
				return (Int32EditBoxPickerKind)this.GetValue(PickerKindProperty);
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
		public Int32 SmallChange {
			get {
				return (Int32)this.GetValue(SmallChangeProperty);
			}
			set {
				this.SetValue(SmallChangeProperty, value);
			}
		}
		
	}

}
