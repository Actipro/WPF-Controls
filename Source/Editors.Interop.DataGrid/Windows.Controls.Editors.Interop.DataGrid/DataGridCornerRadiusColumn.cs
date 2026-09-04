namespace ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid;

/// <summary>
/// Represents a data-bound column for use in a <c>DataGrid</c> that utilizes the <see cref="CornerRadiusEditBox"/> control.
/// </summary>
public class DataGridCornerRadiusColumn : DataGridPartEditBoxColumnBase<CornerRadius?> {

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="DefaultValue"/> property.
	/// </summary>
	public static readonly DependencyProperty DefaultValueProperty
		= DependencyProperty.Register(nameof(DefaultValue), typeof(CornerRadius), typeof(DataGridCornerRadiusColumn), new PropertyMetadata(defaultValue: new CornerRadius(0.0), NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="Format"/> property.
	/// </summary>
	public static readonly DependencyProperty FormatProperty
		= DependencyProperty.Register(nameof(Format), typeof(string), typeof(DataGridCornerRadiusColumn), new PropertyMetadata(defaultValue: "G", NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="IsNaNAllowed"/> property.
	/// </summary>
	public static readonly DependencyProperty IsNaNAllowedProperty
		= DependencyProperty.Register(nameof(IsNaNAllowed), typeof(bool), typeof(DataGridCornerRadiusColumn), new PropertyMetadata(defaultValue: false, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="IsNegativeInfinityAllowed"/> property.
	/// </summary>
	public static readonly DependencyProperty IsNegativeInfinityAllowedProperty
		= DependencyProperty.Register(nameof(IsNegativeInfinityAllowed), typeof(bool), typeof(DataGridCornerRadiusColumn), new PropertyMetadata(defaultValue: false, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="IsPositiveInfinityAllowed"/> property.
	/// </summary>
	public static readonly DependencyProperty IsPositiveInfinityAllowedProperty
		= DependencyProperty.Register(nameof(IsPositiveInfinityAllowed), typeof(bool), typeof(DataGridCornerRadiusColumn), new PropertyMetadata(defaultValue: false, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="LargeChange"/> property.
	/// </summary>
	public static readonly DependencyProperty LargeChangeProperty
		= DependencyProperty.Register(nameof(LargeChange), typeof(CornerRadius), typeof(DataGridCornerRadiusColumn), new PropertyMetadata(defaultValue: new CornerRadius(5.0), NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="Maximum"/> property.
	/// </summary>
	public static readonly DependencyProperty MaximumProperty
		= DependencyProperty.Register(nameof(Maximum), typeof(CornerRadius), typeof(DataGridCornerRadiusColumn), new PropertyMetadata(defaultValue: new CornerRadius(50.0), NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="Minimum"/> property.
	/// </summary>
	public static readonly DependencyProperty MinimumProperty
		= DependencyProperty.Register(nameof(Minimum), typeof(CornerRadius), typeof(DataGridCornerRadiusColumn), new PropertyMetadata(defaultValue: new CornerRadius(0.0), NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="RoundingDecimalPlace"/> property.
	/// </summary>
	public static readonly DependencyProperty RoundingDecimalPlaceProperty
		= DependencyProperty.Register(nameof(RoundingDecimalPlace), typeof(int?), typeof(DataGridCornerRadiusColumn), new PropertyMetadata(defaultValue: 8, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="SmallChange"/> property.
	/// </summary>
	public static readonly DependencyProperty SmallChangeProperty
		= DependencyProperty.Register(nameof(SmallChange), typeof(CornerRadius), typeof(DataGridCornerRadiusColumn), new PropertyMetadata(defaultValue: new CornerRadius(1.0), NotifyPropertyChangeForRefreshContent));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public DataGridCornerRadiusColumn() {
		HasPopup = false;
		SpinnerVisibility = SpinnerVisibility.VisibleWhenActive;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void ApplyStandardValues(FrameworkElement targetElement) {
		base.ApplyStandardValues(targetElement);

		if (targetElement is CornerRadiusEditBox) {
			ApplyValue(DefaultValueProperty, targetElement, CornerRadiusEditBox.DefaultValueProperty);
			ApplyValue(FormatProperty, targetElement, CornerRadiusEditBox.FormatProperty);
			ApplyValue(IsNaNAllowedProperty, targetElement, CornerRadiusEditBox.IsNaNAllowedProperty);
			ApplyValue(IsNegativeInfinityAllowedProperty, targetElement, CornerRadiusEditBox.IsNegativeInfinityAllowedProperty);
			ApplyValue(IsPositiveInfinityAllowedProperty, targetElement, CornerRadiusEditBox.IsPositiveInfinityAllowedProperty);
			ApplyValue(LargeChangeProperty, targetElement, CornerRadiusEditBox.LargeChangeProperty);
			ApplyValue(MaximumProperty, targetElement, CornerRadiusEditBox.MaximumProperty);
			ApplyValue(MinimumProperty, targetElement, CornerRadiusEditBox.MinimumProperty);
			ApplyValue(RoundingDecimalPlaceProperty, targetElement, CornerRadiusEditBox.RoundingDecimalPlaceProperty);
			ApplyValue(SmallChangeProperty, targetElement, CornerRadiusEditBox.SmallChangeProperty);
		}
	}

	/// <summary>
	/// The value to set when incrementing/decrementing from a <c>null</c> value.
	/// </summary>
	/// <value>
	/// The default value is <c>0</c>.
	/// </value>
	public CornerRadius DefaultValue {
		get => (CornerRadius)GetValue(DefaultValueProperty);
		set => SetValue(DefaultValueProperty, value);
	}

	/// <summary>
	/// The number format string.
	/// </summary>
	/// <value>
	/// The default value is <c>"G"</c>.
	/// </value>
	public string Format {
		get => (string)GetValue(FormatProperty);
		set => SetValue(FormatProperty, value);
	}

	/// <inheritdoc/>
	protected override Type GetEditBoxType()
		=> typeof(CornerRadiusEditBox);

	/// <summary>
	/// Indicates whether <see cref="Double.NaN"/> is accepted as a component value.
	/// </summary>
	/// <value>
	/// <c>true</c> if <see cref="Double.NaN"/> is accepted as a component value; otherwise, <c>false</c>.
	/// The default value is <c>false</c>.
	/// </value>
	public bool IsNaNAllowed {
		get => (bool)GetValue(IsNaNAllowedProperty);
		set => SetValue(IsNaNAllowedProperty, value);
	}

	/// <summary>
	/// Indicates whether <see cref="Double.NegativeInfinity"/> is accepted as a component value.
	/// </summary>
	/// <value>
	/// <c>true</c> if <see cref="Double.NegativeInfinity"/> is accepted as a component value; otherwise, <c>false</c>.
	/// The default value is <c>false</c>.
	/// </value>
	public bool IsNegativeInfinityAllowed {
		get => (bool)GetValue(IsNegativeInfinityAllowedProperty);
		set => SetValue(IsNegativeInfinityAllowedProperty, value);
	}

	/// <summary>
	/// Indicates whether <see cref="Double.PositiveInfinity"/> is accepted as a component value.
	/// </summary>
	/// <value>
	/// <c>true</c> if <see cref="Double.PositiveInfinity"/> is accepted as a component value; otherwise, <c>false</c>.
	/// The default value is <c>false</c>.
	/// </value>
	public bool IsPositiveInfinityAllowed {
		get => (bool)GetValue(IsPositiveInfinityAllowedProperty);
		set => SetValue(IsPositiveInfinityAllowedProperty, value);
	}

	/// <summary>
	/// The large change value.
	/// </summary>
	/// <value>
	/// The default value is <c>5</c>.
	/// </value>
	public CornerRadius LargeChange {
		get => (CornerRadius)GetValue(LargeChangeProperty);
		set => SetValue(LargeChangeProperty, value);
	}

	/// <summary>
	/// The highest possible value.
	/// </summary>
	public CornerRadius Maximum {
		get => (CornerRadius)GetValue(MaximumProperty);
		set => SetValue(MaximumProperty, value);
	}

	/// <summary>
	/// The lowest possible value.
	/// </summary>
	public CornerRadius Minimum {
		get => (CornerRadius)GetValue(MinimumProperty);
		set => SetValue(MinimumProperty, value);
	}

	/// <summary>
	/// The rounding decimal place.
	/// </summary>
	/// <value>
	/// The rounding decimal place, which is a value between <c>0</c> and <c>15</c>.
	/// Pass a <c>null</c> value to disable rounding.  The default value is <c>8</c>.
	/// </value>
	public int? RoundingDecimalPlace {
		get => (int?)GetValue(RoundingDecimalPlaceProperty);
		set => SetValue(RoundingDecimalPlaceProperty, value);
	}

	/// <summary>
	/// The small change value.
	/// </summary>
	/// <value>
	/// The default value is <c>1</c>.
	/// </value>
	public CornerRadius SmallChange {
		get => (CornerRadius)GetValue(SmallChangeProperty);
		set => SetValue(SmallChangeProperty, value);
	}

}
