using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Media;
using ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid.Primitives;

namespace ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid {

	/// <summary>
	/// Represents a data-bound column for use in a <c>DataGrid</c> that utilizes the <see cref="ColorEditBox"/> control.
	/// </summary>
	public class DataGridColorColumn : DataGridPartEditBoxColumnBase<Color?> {
	
		#region Dependency Properties
		
		/// <summary>
		/// Identifies the <see cref="DefaultValue"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="DefaultValue"/> dependency property.</value>
		public static readonly DependencyProperty DefaultValueProperty = DependencyProperty.Register("DefaultValue", typeof(Color), typeof(DataGridColorColumn), new PropertyMetadata(Colors.Red));
		
		/// <summary>
		/// Identifies the <see cref="HasSwatch"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="HasSwatch"/> dependency property.</value>
		public static readonly DependencyProperty HasSwatchProperty = DependencyProperty.Register("HasSwatch", typeof(bool), typeof(DataGridColorColumn), new PropertyMetadata(true));
		
		/// <summary>
		/// Identifies the <see cref="HasText"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="HasText"/> dependency property.</value>
		public static readonly DependencyProperty HasTextProperty = DependencyProperty.Register("HasText", typeof(bool), typeof(DataGridBrushColumn), new PropertyMetadata(true));

		/// <summary>
		/// Identifies the <see cref="IsAlphaEnabled"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="IsAlphaEnabled"/> dependency property.</value>
		public static readonly DependencyProperty IsAlphaEnabledProperty = DependencyProperty.Register("IsAlphaEnabled", typeof(bool), typeof(DataGridColorColumn), new PropertyMetadata(true));

		#endregion

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the <see cref="DataGridColorColumn"/> class.
		/// </summary>
		public DataGridColorColumn() {
			this.IsArrowKeyPartNavigationEnabled = false;
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
			if (targetElement is ColorEditBox) {
				this.ApplyValue(DefaultValueProperty, targetElement, ColorEditBox.DefaultValueProperty);
				this.ApplyValue(HasSwatchProperty, targetElement, ColorEditBox.HasSwatchProperty);
				this.ApplyValue(HasTextProperty, targetElement, ColorEditBox.HasTextProperty);
				this.ApplyValue(IsAlphaEnabledProperty, targetElement, ColorEditBox.IsAlphaEnabledProperty);
			}
		}
		
		/// <summary>
		/// Gets or sets the value to set when incrementing/decrementing from a null value.
		/// </summary>
		/// <value>
		/// The value to set when incrementing/decrementing from a null value.
		/// The default value is <c>Red</c>.
		/// </value>
		public Color DefaultValue {
			get {
				return (Color)this.GetValue(DefaultValueProperty);
			}
			set {
				this.SetValue(DefaultValueProperty, value);
			}
		}
		
		/// <summary>
		/// Gets the type of the associated <c>PartEditBoxBase</c>-derived control.
		/// </summary>
		/// <returns>The type of the associated <c>PartEditBoxBase</c>-derived control.</returns>
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		protected override Type GetEditBoxType() {
			return typeof(ColorEditBox);
		}
		
		/// <summary>
		/// Gets or sets whether the edit box should display a swatch that previews the <c>Value</c>.
		/// </summary>
		/// <value>
		/// <c>true</c> if the edit box should display a swatch that previews the <c>Value</c>; otherwise, <c>false</c>.
		/// The default value is <c>true</c>.
		/// </value>
		public bool HasSwatch {
			get {
				return (bool)this.GetValue(HasSwatchProperty);
			}
			set {
				this.SetValue(HasSwatchProperty, value);
			}
		}
		
		/// <summary>
		/// Gets or sets whether the edit box should display a text representation of the <c>Value</c>.
		/// </summary>
		/// <value>
		/// <c>true</c> if the edit box should display a text representation of the <c>Value</c>; otherwise, <c>false</c>.
		/// The default value is <c>true</c>.
		/// </value>
		public bool HasText {
			get {
				return (bool)this.GetValue(HasTextProperty);
			}
			set {
				this.SetValue(HasTextProperty, value);
			}
		}

		/// <summary>
		/// Gets or sets whether the alpha channel (transparency) of the color value is enabled.
		/// </summary>
		/// <value>
		/// <c>true</c> if the alpha channel (transparency) of the color value is enabled; otherwise, <c>false</c>.
		/// The default value is <c>true</c>.
		/// </value>
		/// <remarks>
		/// When disabled, no transparency is supported.
		/// </remarks>
		public bool IsAlphaEnabled {
			get {
				return (bool)this.GetValue(IsAlphaEnabledProperty);
			}
			set {
				this.SetValue(IsAlphaEnabledProperty, value);
			}
		}
		
	}

}
