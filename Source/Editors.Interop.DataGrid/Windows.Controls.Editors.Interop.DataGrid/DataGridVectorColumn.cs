namespace ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid;

/// <summary>
/// Represents a data-bound column for use in a <c>DataGrid</c> that utilizes the <see cref="VectorEditBox"/> control.
/// </summary>
public class DataGridVectorColumn : DataGridPartEditBoxColumnBase<Vector?> {

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="DefaultValue"/> property.
	/// </summary>
	public static readonly DependencyProperty DefaultValueProperty
		= DependencyProperty.Register(nameof(DefaultValue), typeof(Vector), typeof(DataGridVectorColumn), new PropertyMetadata(defaultValue: new Vector(0, 0), NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="Format"/> property.
	/// </summary>
	public static readonly DependencyProperty FormatProperty
		= DependencyProperty.Register(nameof(Format), typeof(string), typeof(DataGridVectorColumn), new PropertyMetadata(defaultValue: "G", NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="IsNaNAllowed"/> property.
	/// </summary>
	public static readonly DependencyProperty IsNaNAllowedProperty
		= DependencyProperty.Register(nameof(IsNaNAllowed), typeof(bool), typeof(DataGridVectorColumn), new PropertyMetadata(defaultValue: false, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="IsNegativeInfinityAllowed"/> property.
	/// </summary>
	public static readonly DependencyProperty IsNegativeInfinityAllowedProperty
		= DependencyProperty.Register(nameof(IsNegativeInfinityAllowed), typeof(bool), typeof(DataGridVectorColumn), new PropertyMetadata(defaultValue: false, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="IsPositiveInfinityAllowed"/> property.
	/// </summary>
	public static readonly DependencyProperty IsPositiveInfinityAllowedProperty
		= DependencyProperty.Register(nameof(IsPositiveInfinityAllowed), typeof(bool), typeof(DataGridVectorColumn), new PropertyMetadata(defaultValue: false, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="LargeChange"/> property.
	/// </summary>
	public static readonly DependencyProperty LargeChangeProperty
		= DependencyProperty.Register(nameof(LargeChange), typeof(Vector), typeof(DataGridVectorColumn), new PropertyMetadata(defaultValue: new Vector(5, 5), NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="Maximum"/> property.
	/// </summary>
	public static readonly DependencyProperty MaximumProperty
		= DependencyProperty.Register(nameof(Maximum), typeof(Vector), typeof(DataGridVectorColumn), new PropertyMetadata(defaultValue: new Vector(100000, 100000), NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="Minimum"/> property.
	/// </summary>
	public static readonly DependencyProperty MinimumProperty
		= DependencyProperty.Register(nameof(Minimum), typeof(Vector), typeof(DataGridVectorColumn), new PropertyMetadata(defaultValue: new Vector(-100000, -100000), NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="RoundingDecimalPlace"/> property.
	/// </summary>
	public static readonly DependencyProperty RoundingDecimalPlaceProperty
		= DependencyProperty.Register(nameof(RoundingDecimalPlace), typeof(int?), typeof(DataGridVectorColumn), new PropertyMetadata(defaultValue: 8, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="SmallChange"/> property.
	/// </summary>
	public static readonly DependencyProperty SmallChangeProperty
		= DependencyProperty.Register(nameof(SmallChange), typeof(Vector), typeof(DataGridVectorColumn), new PropertyMetadata(defaultValue: new Vector(1, 1), NotifyPropertyChangeForRefreshContent));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the class.
	/// </summary>
	static DataGridVectorColumn() {
		HasPopupProperty.OverrideMetadata(typeof(DataGridVectorColumn), new PropertyMetadata(defaultValue: false));
		SpinnerVisibilityProperty.OverrideMetadata(typeof(DataGridVectorColumn), new PropertyMetadata(defaultValue: SpinnerVisibility.VisibleWhenActive));
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void ApplyStandardValues(FrameworkElement targetElement) {
		base.ApplyStandardValues(targetElement);

		if (targetElement is VectorEditBox) {
			ApplyValue(DefaultValueProperty, targetElement, VectorEditBox.DefaultValueProperty);
			ApplyValue(FormatProperty, targetElement, VectorEditBox.FormatProperty);
			ApplyValue(IsNaNAllowedProperty, targetElement, VectorEditBox.IsNaNAllowedProperty);
			ApplyValue(IsNegativeInfinityAllowedProperty, targetElement, VectorEditBox.IsNegativeInfinityAllowedProperty);
			ApplyValue(IsPositiveInfinityAllowedProperty, targetElement, VectorEditBox.IsPositiveInfinityAllowedProperty);
			ApplyValue(LargeChangeProperty, targetElement, VectorEditBox.LargeChangeProperty);
			ApplyValue(MaximumProperty, targetElement, VectorEditBox.MaximumProperty);
			ApplyValue(MinimumProperty, targetElement, VectorEditBox.MinimumProperty);
			ApplyValue(RoundingDecimalPlaceProperty, targetElement, VectorEditBox.RoundingDecimalPlaceProperty);
			ApplyValue(SmallChangeProperty, targetElement, VectorEditBox.SmallChangeProperty);
		}
	}

	/// <summary>
	/// The value to set when incrementing/decrementing from a <c>null</c> value.
	/// </summary>
	/// <value>
	/// The default value is <c>0</c>.
	/// </value>
	public Vector DefaultValue {
		get => (Vector)GetValue(DefaultValueProperty);
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
		=> typeof(VectorEditBox);

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
	public Vector LargeChange {
		get => (Vector)GetValue(LargeChangeProperty);
		set => SetValue(LargeChangeProperty, value);
	}

	/// <summary>
	/// The highest possible value.
	/// </summary>
	public Vector Maximum {
		get => (Vector)GetValue(MaximumProperty);
		set => SetValue(MaximumProperty, value);
	}

	/// <summary>
	/// The lowest possible value.
	/// </summary>
	public Vector Minimum {
		get => (Vector)GetValue(MinimumProperty);
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
	public Vector SmallChange {
		get => (Vector)GetValue(SmallChangeProperty);
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
