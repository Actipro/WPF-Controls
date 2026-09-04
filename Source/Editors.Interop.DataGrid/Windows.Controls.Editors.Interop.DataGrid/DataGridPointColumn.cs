namespace ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid;

/// <summary>
/// Represents a data-bound column for use in a <c>DataGrid</c> that utilizes the <see cref="PointEditBox"/> control.
/// </summary>
public class DataGridPointColumn : DataGridPartEditBoxColumnBase<Point?> {

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="DefaultValue"/> property.
	/// </summary>
	public static readonly DependencyProperty DefaultValueProperty
		= DependencyProperty.Register(nameof(DefaultValue), typeof(Point), typeof(DataGridPointColumn), new PropertyMetadata(defaultValue: new Point(0, 0), NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="Format"/> property.
	/// </summary>
	public static readonly DependencyProperty FormatProperty
		= DependencyProperty.Register(nameof(Format), typeof(string), typeof(DataGridPointColumn), new PropertyMetadata(defaultValue: "G", NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="IsNaNAllowed"/> property.
	/// </summary>
	public static readonly DependencyProperty IsNaNAllowedProperty
		= DependencyProperty.Register(nameof(IsNaNAllowed), typeof(bool), typeof(DataGridPointColumn), new PropertyMetadata(defaultValue: false, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="IsNegativeInfinityAllowed"/> property.
	/// </summary>
	public static readonly DependencyProperty IsNegativeInfinityAllowedProperty
		= DependencyProperty.Register(nameof(IsNegativeInfinityAllowed), typeof(bool), typeof(DataGridPointColumn), new PropertyMetadata(defaultValue: false, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="IsPositiveInfinityAllowed"/> property.
	/// </summary>
	public static readonly DependencyProperty IsPositiveInfinityAllowedProperty
		= DependencyProperty.Register(nameof(IsPositiveInfinityAllowed), typeof(bool), typeof(DataGridPointColumn), new PropertyMetadata(defaultValue: false, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="LargeChange"/> property.
	/// </summary>
	public static readonly DependencyProperty LargeChangeProperty
		= DependencyProperty.Register(nameof(LargeChange), typeof(Point), typeof(DataGridPointColumn), new PropertyMetadata(defaultValue: new Point(5, 5), NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="Maximum"/> property.
	/// </summary>
	public static readonly DependencyProperty MaximumProperty
		= DependencyProperty.Register(nameof(Maximum), typeof(Point), typeof(DataGridPointColumn), new PropertyMetadata(defaultValue: new Point(100000, 100000), NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="Minimum"/> property.
	/// </summary>
	public static readonly DependencyProperty MinimumProperty
		= DependencyProperty.Register(nameof(Minimum), typeof(Point), typeof(DataGridPointColumn), new PropertyMetadata(defaultValue: new Point(-100000, -100000), NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="RoundingDecimalPlace"/> property.
	/// </summary>
	public static readonly DependencyProperty RoundingDecimalPlaceProperty
		= DependencyProperty.Register(nameof(RoundingDecimalPlace), typeof(int?), typeof(DataGridPointColumn), new PropertyMetadata(defaultValue: 8, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="SmallChange"/> property.
	/// </summary>
	public static readonly DependencyProperty SmallChangeProperty
		= DependencyProperty.Register(nameof(SmallChange), typeof(Point), typeof(DataGridPointColumn), new PropertyMetadata(defaultValue: new Point(1, 1), NotifyPropertyChangeForRefreshContent));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the class.
	/// </summary>
	static DataGridPointColumn() {
		HasPopupProperty.OverrideMetadata(typeof(DataGridPointColumn), new PropertyMetadata(defaultValue: false));
		SpinnerVisibilityProperty.OverrideMetadata(typeof(DataGridPointColumn), new PropertyMetadata(defaultValue: SpinnerVisibility.VisibleWhenActive));
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void ApplyStandardValues(FrameworkElement targetElement) {
		base.ApplyStandardValues(targetElement);

		if (targetElement is PointEditBox) {
			ApplyValue(DefaultValueProperty, targetElement, PointEditBox.DefaultValueProperty);
			ApplyValue(FormatProperty, targetElement, PointEditBox.FormatProperty);
			ApplyValue(IsNaNAllowedProperty, targetElement, PointEditBox.IsNaNAllowedProperty);
			ApplyValue(IsNegativeInfinityAllowedProperty, targetElement, PointEditBox.IsNegativeInfinityAllowedProperty);
			ApplyValue(IsPositiveInfinityAllowedProperty, targetElement, PointEditBox.IsPositiveInfinityAllowedProperty);
			ApplyValue(LargeChangeProperty, targetElement, PointEditBox.LargeChangeProperty);
			ApplyValue(MaximumProperty, targetElement, PointEditBox.MaximumProperty);
			ApplyValue(MinimumProperty, targetElement, PointEditBox.MinimumProperty);
			ApplyValue(RoundingDecimalPlaceProperty, targetElement, PointEditBox.RoundingDecimalPlaceProperty);
			ApplyValue(SmallChangeProperty, targetElement, PointEditBox.SmallChangeProperty);
		}
	}

	/// <summary>
	/// The value to set when incrementing/decrementing from a <c>null</c> value.
	/// </summary>
	/// <value>
	/// The default value is <c>0</c>.
	/// </value>
	public Point DefaultValue {
		get => (Point)GetValue(DefaultValueProperty);
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
		=> typeof(PointEditBox);

	/// <value>
	/// <c>true</c> if the control has a popup available; otherwise <c>false</c>.
	/// The default value is <c>false</c>.
	/// </value>
	/// <inheritdoc cref="DataGridPartEditBoxColumnBase{T}.HasPopup"/>
	public new bool HasPopup {
		// Property redefined to change the default value doc comment
		get => base.HasPopup;
		set => base.HasPopup = value;
	}

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
	public Point LargeChange {
		get => (Point)GetValue(LargeChangeProperty);
		set => SetValue(LargeChangeProperty, value);
	}

	/// <summary>
	/// The highest possible value.
	/// </summary>
	public Point Maximum {
		get => (Point)GetValue(MaximumProperty);
		set => SetValue(MaximumProperty, value);
	}

	/// <summary>
	/// The lowest possible value.
	/// </summary>
	public Point Minimum {
		get => (Point)GetValue(MinimumProperty);
		set => SetValue(MinimumProperty, value);
	}

	/// <summary>
	/// The rounding decimal place.
	/// </summary>
	/// <value>
	/// The rounding decimal place, which is a value between <c>0</c> and <c>15</c>.
	/// Pass a <c>null</c> value to disable rounding.
	/// The default value is <c>8</c>.
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
	public Point SmallChange {
		get => (Point)GetValue(SmallChangeProperty);
		set => SetValue(SmallChangeProperty, value);
	}

	/// <value>
	/// The default value is <see cref="SpinnerVisibility.VisibleWhenActive"/>.
	/// </value>
	/// <inheritdoc cref="DataGridPartEditBoxColumnBase{T}.SpinnerVisibility"/>
	public new SpinnerVisibility SpinnerVisibility {
		// Property redefined to change the default value doc comment
		get => base.SpinnerVisibility;
		set => base.SpinnerVisibility = value;
	}

}
