using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid.Primitives;

namespace ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid {

	/// <summary>
	/// Represents a data-bound column for use in a <c>DataGrid</c> that utilizes the <see cref="TimeSpanEditBox"/> control.
	/// </summary>
	public class DataGridTimeSpanColumn : DataGridPartEditBoxColumnBase<TimeSpan?> {
	
		#region Dependency Properties
			
		/// <summary>
		/// Identifies the <see cref="DefaultValue"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="DefaultValue"/> dependency property.</value>
		public static readonly DependencyProperty DefaultValueProperty = DependencyProperty.Register("DefaultValue", typeof(TimeSpan), typeof(DataGridTimeSpanColumn), new PropertyMetadata(TimeSpan.Zero));
			
		/// <summary>
		/// Identifies the <see cref="Format"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="Format"/> dependency property.</value>
		public static readonly DependencyProperty FormatProperty = DependencyProperty.Register("Format", typeof(string), typeof(DataGridTimeSpanColumn), new PropertyMetadata("g"));
		
		/// <summary>
		/// Identifies the <see cref="LargeChange"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="LargeChange"/> dependency property.</value>
		public static readonly DependencyProperty LargeChangeProperty = DependencyProperty.Register("LargeChange", typeof(TimeSpan), typeof(DataGridTimeSpanColumn), new PropertyMetadata(new TimeSpan(7, 3, 5, 5, 50)));
		
		/// <summary>
		/// Identifies the <see cref="Maximum"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="Maximum"/> dependency property.</value>
		public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof(TimeSpan), typeof(DataGridTimeSpanColumn), new PropertyMetadata(TimeSpan.MaxValue));
		
		/// <summary>
		/// Identifies the <see cref="Minimum"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="Minimum"/> dependency property.</value>
		public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof(TimeSpan), typeof(DataGridTimeSpanColumn), new PropertyMetadata(TimeSpan.MinValue));
		
		/// <summary>
		/// Identifies the <see cref="SmallChange"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="SmallChange"/> dependency property.</value>
		public static readonly DependencyProperty SmallChangeProperty = DependencyProperty.Register("SmallChange", typeof(TimeSpan), typeof(DataGridTimeSpanColumn), new PropertyMetadata(new TimeSpan(1, 1, 1, 1, 1)));
		
		#endregion

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the <see cref="DataGridTimeSpanColumn"/> class.
		/// </summary>
		public DataGridTimeSpanColumn() {
			this.HasPopup = false;
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
			if (targetElement is TimeSpanEditBox) {
				this.ApplyValue(DefaultValueProperty, targetElement, TimeSpanEditBox.DefaultValueProperty);
				this.ApplyValue(FormatProperty, targetElement, TimeSpanEditBox.FormatProperty);
				this.ApplyValue(LargeChangeProperty, targetElement, TimeSpanEditBox.LargeChangeProperty);
				this.ApplyValue(MaximumProperty, targetElement, TimeSpanEditBox.MaximumProperty);
				this.ApplyValue(MinimumProperty, targetElement, TimeSpanEditBox.MinimumProperty);
				this.ApplyValue(SmallChangeProperty, targetElement, TimeSpanEditBox.SmallChangeProperty);
			}
		}
		
		/// <summary>
		/// Gets or sets the value to set when incrementing/decrementing from a null value.
		/// </summary>
		/// <value>
		/// The value to set when incrementing/decrementing from a null value.
		/// The default value is <c>0</c>.
		/// </value>
		public TimeSpan DefaultValue {
			get {
				return (TimeSpan)this.GetValue(DefaultValueProperty);
			}
			set {
				this.SetValue(DefaultValueProperty, value);
			}
		}
		
		/// <summary>
		/// Gets or sets the timespan format string.
		/// </summary>
		/// <value>
		/// The timespan format string.
		/// The default value is <c>"g"</c>.
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
			return typeof(TimeSpanEditBox);
		}
		
		/// <summary>
		/// Gets or sets the large change value.
		/// </summary>
		/// <value>
		/// The large change value.
		/// The default value is <c>5</c>.
		/// </value>
		public TimeSpan LargeChange {
			get {
				return (TimeSpan)this.GetValue(LargeChangeProperty);
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
		public TimeSpan Maximum {
			get {
				return (TimeSpan)this.GetValue(MaximumProperty);
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
		public TimeSpan Minimum {
			get {
				return (TimeSpan)this.GetValue(MinimumProperty);
			}
			set {
				this.SetValue(MinimumProperty, value);
			}
		}
		
		/// <summary>
		/// Gets or sets the small change value.
		/// </summary>
		/// <value>
		/// The small change value.
		/// The default value is <c>1</c>.
		/// </value>
		public TimeSpan SmallChange {
			get {
				return (TimeSpan)this.GetValue(SmallChangeProperty);
			}
			set {
				this.SetValue(SmallChangeProperty, value);
			}
		}
		
	}

}
