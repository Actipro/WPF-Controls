using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid.Primitives;

namespace ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid {

	/// <summary>
	/// Represents a data-bound column for use in a <c>DataGrid</c> that utilizes the <see cref="DateTimeEditBox"/> control.
	/// </summary>
	public class DataGridDateTimeColumn : DataGridPartEditBoxColumnBase<DateTime?> {
		
		#region Dependency Properties

		/// <summary>
		/// Identifies the <see cref="Format"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="Format"/> dependency property.</value>
		public static readonly DependencyProperty FormatProperty = DependencyProperty.Register("Format", typeof(string), typeof(DataGridDateTimeColumn), new PropertyMetadata("g", NotifyPropertyChangeForRefreshContent));
		
		/// <summary>
		/// Identifies the <see cref="Maximum"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="Maximum"/> dependency property.</value>
		public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof(DateTime), typeof(DataGridDateTimeColumn), new PropertyMetadata(new DateTime(9998, 12, 31), NotifyPropertyChangeForRefreshContent));
		
		/// <summary>
		/// Identifies the <see cref="Minimum"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="Minimum"/> dependency property.</value>
		public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof(DateTime), typeof(DataGridDateTimeColumn), new PropertyMetadata(new DateTime(1753, 1, 1), NotifyPropertyChangeForRefreshContent));
		
		#endregion

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the <see cref="DataGridDateTimeColumn"/> class.
		/// </summary>
		public DataGridDateTimeColumn() {
			this.SpinWrapping = SpinWrapping.Wrap;
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
			if (targetElement is DateTimeEditBox) {
				this.ApplyValue(FormatProperty, targetElement, DateTimeEditBox.FormatProperty);
				this.ApplyValue(MaximumProperty, targetElement, DateTimeEditBox.MaximumProperty);
				this.ApplyValue(MinimumProperty, targetElement, DateTimeEditBox.MinimumProperty);
			}
		}

		/// <summary>
		/// Gets or sets the date/time format string.
		/// </summary>
		/// <value>
		/// The date/time format string.
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
			return typeof(DateTimeEditBox);
		}
		
		/// <summary>
		/// Gets or sets the highest possible value.
		/// </summary>
		/// <value>
		/// The highest possible value.
		/// </value>
		public DateTime Maximum {
			get {
				return (DateTime)this.GetValue(MaximumProperty);
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
		public DateTime Minimum {
			get {
				return (DateTime)this.GetValue(MinimumProperty);
			}
			set {
				this.SetValue(MinimumProperty, value);
			}
		}
		
	}

}
