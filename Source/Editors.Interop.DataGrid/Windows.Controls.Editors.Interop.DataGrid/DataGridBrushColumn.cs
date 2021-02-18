using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Media;
using ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid.Primitives;

namespace ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid {

	/// <summary>
	/// Represents a data-bound column for use in a <c>DataGrid</c> that utilizes the <see cref="BrushEditBox"/> control.
	/// </summary>
	public class DataGridBrushColumn : DataGridPartEditBoxColumnBase<Brush> {
	
		#region Dependency Properties
		
		/// <summary>
		/// Identifies the <see cref="HasSwatch"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="HasSwatch"/> dependency property.</value>
		public static readonly DependencyProperty HasSwatchProperty = DependencyProperty.Register("HasSwatch", typeof(bool), typeof(DataGridBrushColumn), new PropertyMetadata(true));

		/// <summary>
		/// Identifies the <see cref="HasText"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="HasText"/> dependency property.</value>
		public static readonly DependencyProperty HasTextProperty = DependencyProperty.Register("HasText", typeof(bool), typeof(DataGridBrushColumn), new PropertyMetadata(true));

		/// <summary>
		/// Identifies the <see cref="IsAlphaEnabled"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="IsAlphaEnabled"/> dependency property.</value>
		public static readonly DependencyProperty IsAlphaEnabledProperty = DependencyProperty.Register("IsAlphaEnabled", typeof(bool), typeof(DataGridBrushColumn), new PropertyMetadata(true));

		/// <summary>
		/// Identifies the <see cref="IsGradientAllowed"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="IsGradientAllowed"/> dependency property.</value>
		public static readonly DependencyProperty IsGradientAllowedProperty = DependencyProperty.Register("IsGradientAllowed", typeof(bool), typeof(DataGridBrushColumn), new PropertyMetadata(true));
		
		#endregion

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the <see cref="DataGridBrushColumn"/> class.
		/// </summary>
		public DataGridBrushColumn() {
			this.IsArrowKeyPartNavigationEnabled = false;
			this.IsNullAllowed = true;
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

			var targetEditBox = targetElement as BrushEditBox;
			if (targetEditBox != null) {
				targetEditBox.CanReuseBrush = false;

				this.ApplyValue(HasSwatchProperty, targetElement, BrushEditBox.HasSwatchProperty);
				this.ApplyValue(HasTextProperty, targetElement, BrushEditBox.HasTextProperty);
				this.ApplyValue(IsAlphaEnabledProperty, targetElement, BrushEditBox.IsAlphaEnabledProperty);
				this.ApplyValue(IsGradientAllowedProperty, targetElement, BrushEditBox.IsGradientAllowedProperty);
			}
		}

		/// <summary>
		/// Gets the type of the associated <c>PartEditBoxBase</c>-derived control.
		/// </summary>
		/// <returns>The type of the associated <c>PartEditBoxBase</c>-derived control.</returns>
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		protected override Type GetEditBoxType() {
			return typeof(BrushEditBox);
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
		/// Gets or sets whether the alpha channel (transparency) of the brush value colors are enabled.
		/// </summary>
		/// <value>
		/// <c>true</c> if the alpha channel (transparency) of the brush value colors is enabled; otherwise, <c>false</c>.
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

		/// <summary>
		/// Gets or sets whether gradient brush values can be entered.
		/// </summary>
		/// <value>
		/// <c>true</c> if gradient brush values can be entered; otherwise, <c>false</c>.
		/// The default value is <c>true</c>.
		/// </value>
		public bool IsGradientAllowed {
			get {
				return (bool)this.GetValue(IsGradientAllowedProperty);
			}
			set {
				this.SetValue(IsGradientAllowedProperty, value);
			}
		}
		
	}

}
