namespace ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid;

/// <summary>
/// Represents a data-bound column for use in a <c>DataGrid</c> that utilizes the <see cref="DecimalEditBox"/> control.
/// </summary>
public class DataGridDecimalColumn : DataGridPartEditBoxColumnBase<Decimal?> {

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="CanSnapToChangePrecision"/> property.
	/// </summary>
	public static readonly DependencyProperty CanSnapToChangePrecisionProperty
		= DependencyProperty.Register(nameof(CanSnapToChangePrecision), typeof(bool), typeof(DataGridDecimalColumn), new PropertyMetadata(defaultValue: true));

	/// <summary>
	/// Defines the <see cref="DefaultValue"/> property.
	/// </summary>
	public static readonly DependencyProperty DefaultValueProperty
		= DependencyProperty.Register(nameof(DefaultValue), typeof(Decimal), typeof(DataGridDecimalColumn), new PropertyMetadata(defaultValue: (Decimal)0, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="Format"/> property.
	/// </summary>
	public static readonly DependencyProperty FormatProperty
		= DependencyProperty.Register(nameof(Format), typeof(string), typeof(DataGridDecimalColumn), new PropertyMetadata(defaultValue: "G", NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="LargeChange"/> property.
	/// </summary>
	public static readonly DependencyProperty LargeChangeProperty
		= DependencyProperty.Register(nameof(LargeChange), typeof(Decimal), typeof(DataGridDecimalColumn), new PropertyMetadata(defaultValue: 5.0, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="Maximum"/> property.
	/// </summary>
	public static readonly DependencyProperty MaximumProperty
		= DependencyProperty.Register(nameof(Maximum), typeof(Decimal), typeof(DataGridDecimalColumn), new PropertyMetadata(defaultValue: Decimal.MaxValue, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="Minimum"/> property.
	/// </summary>
	public static readonly DependencyProperty MinimumProperty
		= DependencyProperty.Register(nameof(Minimum), typeof(Decimal), typeof(DataGridDecimalColumn), new PropertyMetadata(defaultValue: Decimal.MinValue, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="PickerKind"/> property.
	/// </summary>
	public static readonly DependencyProperty PickerKindProperty
		= DependencyProperty.Register(nameof(PickerKind), typeof(DecimalEditBoxPickerKind), typeof(DataGridDecimalColumn), new PropertyMetadata(defaultValue: DecimalEditBoxPickerKind.Calculator, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="RoundingDecimalPlace"/> property.
	/// </summary>
	public static readonly DependencyProperty RoundingDecimalPlaceProperty
		= DependencyProperty.Register(nameof(RoundingDecimalPlace), typeof(int?), typeof(DataGridDecimalColumn), new PropertyMetadata(defaultValue: 8, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="SmallChange"/> property.
	/// </summary>
	public static readonly DependencyProperty SmallChangeProperty
		= DependencyProperty.Register(nameof(SmallChange), typeof(Decimal), typeof(DataGridDecimalColumn), new PropertyMetadata(defaultValue: 1.0, NotifyPropertyChangeForRefreshContent));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the class.
	/// </summary>
	static DataGridDecimalColumn() {
		HasPopupProperty.OverrideMetadata(typeof(DataGridDecimalColumn), new PropertyMetadata(defaultValue: false));
		IsArrowKeyPartNavigationEnabledProperty.OverrideMetadata(typeof(DataGridDecimalColumn), new PropertyMetadata(defaultValue: false));
		SpinnerVisibilityProperty.OverrideMetadata(typeof(DataGridDecimalColumn), new PropertyMetadata(defaultValue: SpinnerVisibility.VisibleWhenActive));
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void ApplyStandardValues(FrameworkElement targetElement) {
		base.ApplyStandardValues(targetElement);

		if (targetElement is DecimalEditBox) {
			ApplyValue(CanSnapToChangePrecisionProperty, targetElement, DecimalEditBox.CanSnapToChangePrecisionProperty);
			ApplyValue(DefaultValueProperty, targetElement, DecimalEditBox.DefaultValueProperty);
			ApplyValue(FormatProperty, targetElement, DecimalEditBox.FormatProperty);
			ApplyValue(LargeChangeProperty, targetElement, DecimalEditBox.LargeChangeProperty);
			ApplyValue(MaximumProperty, targetElement, DecimalEditBox.MaximumProperty);
			ApplyValue(MinimumProperty, targetElement, DecimalEditBox.MinimumProperty);
			ApplyValue(PickerKindProperty, targetElement, DecimalEditBox.PickerKindProperty);
			ApplyValue(RoundingDecimalPlaceProperty, targetElement, DecimalEditBox.RoundingDecimalPlaceProperty);
			ApplyValue(SmallChangeProperty, targetElement, DecimalEditBox.SmallChangeProperty);
		}
	}

	/// <summary>
	/// Indicates whether the value should be snapped to the precision of the incremental change value prior to applying the increment.
	/// </summary>
	/// <value>
	/// <c>true</c> if the value should be snapped to the precision of the incremental change value prior to applying the increment; otherwise, <c>false</c>.
	/// The default value is <c>true</c>.
	/// </value>
	/// <remarks>
	/// When <c>true</c>, a value of <c>1.24</c> with change value <c>0.1</c> would result in <c>1.3</c>.
	/// When <c>false</c>, a value of <c>1.24</c> with change value <c>0.1</c> would result in <c>1.34</c>.
	/// </remarks>
	public bool CanSnapToChangePrecision {
		get => (bool)GetValue(CanSnapToChangePrecisionProperty);
		set => SetValue(CanSnapToChangePrecisionProperty, value);
	}

	/// <summary>
	/// The value to set when incrementing/decrementing from a <c>null</c> value.
	/// </summary>
	/// <value>
	/// The default value is <c>0</c>.
	/// </value>
	public Decimal DefaultValue {
		get => (Decimal)GetValue(DefaultValueProperty);
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
		=> typeof(DecimalEditBox);

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

	/// <value>
	/// <c>true</c> if the left/right arrow keys can be used to move between and select editable parts; otherwise <c>false</c>.
	/// The default value is <c>false</c>.
	/// </value>
	/// <inheritdoc cref="DataGridPartEditBoxColumnBase{T}.IsArrowKeyPartNavigationEnabled"/>
	public new bool IsArrowKeyPartNavigationEnabled {
		// Property redefined to change the default value doc comment
		get => base.IsArrowKeyPartNavigationEnabled;
		set => base.IsArrowKeyPartNavigationEnabled = value;
	}

	/// <summary>
	/// The large change value.
	/// </summary>
	/// <value>
	/// The default value is <c>5</c>.
	/// </value>
	public Decimal LargeChange {
		get => (Decimal)GetValue(LargeChangeProperty);
		set => SetValue(LargeChangeProperty, value);
	}

	/// <summary>
	/// The highest possible value.
	/// </summary>
	public Decimal Maximum {
		get => (Decimal)GetValue(MaximumProperty);
		set => SetValue(MaximumProperty, value);
	}

	/// <summary>
	/// The lowest possible value.
	/// </summary>
	public Decimal Minimum {
		get => (Decimal)GetValue(MinimumProperty);
		set => SetValue(MinimumProperty, value);
	}

	/// <summary>
	/// An <see cref="DecimalEditBoxPickerKind"/> indicating the pre-defined <c>Style</c> to apply to the picker used within the popup.
	/// </summary>
	/// <value>
	/// The default value is <see cref="DecimalEditBoxPickerKind.Calculator"/>.
	/// </value>
	public DecimalEditBoxPickerKind PickerKind {
		get => (DecimalEditBoxPickerKind)GetValue(PickerKindProperty);
		set => SetValue(PickerKindProperty, value);
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
	public Decimal SmallChange {
		get => (Decimal)GetValue(SmallChangeProperty);
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
