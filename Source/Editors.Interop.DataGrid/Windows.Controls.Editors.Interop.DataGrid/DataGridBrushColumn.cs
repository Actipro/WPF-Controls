namespace ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid;

/// <summary>
/// Represents a data-bound column for use in a <c>DataGrid</c> that utilizes the <see cref="BrushEditBox"/> control.
/// </summary>
public class DataGridBrushColumn : DataGridPartEditBoxColumnBase<Brush> {

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="CanSwatchStretch"/> property.
	/// </summary>
	public static readonly DependencyProperty CanSwatchStretchProperty
		= DependencyProperty.Register(nameof(CanSwatchStretch), typeof(bool), typeof(DataGridBrushColumn), new PropertyMetadata(defaultValue: true, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="HasSwatch"/> property.
	/// </summary>
	public static readonly DependencyProperty HasSwatchProperty
		= DependencyProperty.Register(nameof(HasSwatch), typeof(bool), typeof(DataGridBrushColumn), new PropertyMetadata(defaultValue: true, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="HasText"/> property.
	/// </summary>
	public static readonly DependencyProperty HasTextProperty
		= DependencyProperty.Register(nameof(HasText), typeof(bool), typeof(DataGridBrushColumn), new PropertyMetadata(defaultValue: true, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="IsAlphaEnabled"/> property.
	/// </summary>
	public static readonly DependencyProperty IsAlphaEnabledProperty
		= DependencyProperty.Register(nameof(IsAlphaEnabled), typeof(bool), typeof(DataGridBrushColumn), new PropertyMetadata(defaultValue: true, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="IsGradientAllowed"/> property.
	/// </summary>
	public static readonly DependencyProperty IsGradientAllowedProperty
		= DependencyProperty.Register(nameof(IsGradientAllowed), typeof(bool), typeof(DataGridBrushColumn), new PropertyMetadata(defaultValue: true, NotifyPropertyChangeForRefreshContent));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public DataGridBrushColumn() {
		IsArrowKeyPartNavigationEnabled = false;
		IsNullAllowed = true;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void ApplyStandardValues(FrameworkElement targetElement) {
		base.ApplyStandardValues(targetElement);

		if (targetElement is BrushEditBox targetEditBox) {
			targetEditBox.CanReuseBrush = false;

			ApplyValue(CanSwatchStretchProperty, targetElement, BrushEditBox.CanSwatchStretchProperty);
			ApplyValue(HasSwatchProperty, targetElement, BrushEditBox.HasSwatchProperty);
			ApplyValue(HasTextProperty, targetElement, BrushEditBox.HasTextProperty);
			ApplyValue(IsAlphaEnabledProperty, targetElement, BrushEditBox.IsAlphaEnabledProperty);
			ApplyValue(IsGradientAllowedProperty, targetElement, BrushEditBox.IsGradientAllowedProperty);
		}
	}

	/// <summary>
	/// Indicates whether the swatch can stretch when <see cref="HasText"/> is <c>false</c>.
	/// </summary>
	/// <value>
	/// <c>true</c> if the swatch can stretch when <see cref="HasText"/> is <c>false</c>; otherwise, <c>false</c>.
	/// The default value is <c>true</c>.
	/// </value>
	public bool CanSwatchStretch {
		get => (bool)GetValue(CanSwatchStretchProperty);
		set => SetValue(CanSwatchStretchProperty, value);
	}

	/// <inheritdoc/>
	protected override Type GetEditBoxType()
		=> typeof(BrushEditBox);

	/// <summary>
	/// Indicates whether the edit box should display a swatch that previews the <c>Value</c>.
	/// </summary>
	/// <value>
	/// <c>true</c> if the edit box should display a swatch that previews the <c>Value</c>; otherwise, <c>false</c>.
	/// The default value is <c>true</c>.
	/// </value>
	public bool HasSwatch {
		get => (bool)GetValue(HasSwatchProperty);
		set => SetValue(HasSwatchProperty, value);
	}

	/// <summary>
	/// Indicates whether the edit box should display a text representation of the <c>Value</c>.
	/// </summary>
	/// <value>
	/// <c>true</c> if the edit box should display a text representation of the <c>Value</c>; otherwise, <c>false</c>.
	/// The default value is <c>true</c>.
	/// </value>
	public bool HasText {
		get => (bool)GetValue(HasTextProperty);
		set => SetValue(HasTextProperty, value);
	}

	/// <summary>
	/// Indicates whether the alpha channel (transparency) of the brush value colors are enabled.
	/// </summary>
	/// <value>
	/// <c>true</c> if the alpha channel (transparency) of the brush value colors is enabled; otherwise, <c>false</c>.
	/// The default value is <c>true</c>.
	/// </value>
	/// <remarks>
	/// When disabled, no transparency is supported.
	/// </remarks>
	public bool IsAlphaEnabled {
		get => (bool)GetValue(IsAlphaEnabledProperty);
		set => SetValue(IsAlphaEnabledProperty, value);
	}

	/// <summary>
	/// Indicates whether gradient brush values can be entered.
	/// </summary>
	/// <value>
	/// <c>true</c> if gradient brush values can be entered; otherwise, <c>false</c>.
	/// The default value is <c>true</c>.
	/// </value>
	public bool IsGradientAllowed {
		get => (bool)GetValue(IsGradientAllowedProperty);
		set => SetValue(IsGradientAllowedProperty, value);
	}

}
