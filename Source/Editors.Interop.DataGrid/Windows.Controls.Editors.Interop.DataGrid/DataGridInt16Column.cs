namespace ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid;

/// <summary>
/// Represents a data-bound column for use in a <c>DataGrid</c> that utilizes the <see cref="Int16EditBox"/> control.
/// </summary>
public class DataGridInt16Column : DataGridPartEditBoxColumnBase<Int16?> {

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="DefaultValue"/> property.
	/// </summary>
	public static readonly DependencyProperty DefaultValueProperty
		= DependencyProperty.Register(nameof(DefaultValue), typeof(Int16), typeof(DataGridInt16Column), new PropertyMetadata(defaultValue: (Int16)0, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="Format"/> property.
	/// </summary>
	public static readonly DependencyProperty FormatProperty
		= DependencyProperty.Register(nameof(Format), typeof(string), typeof(DataGridInt16Column), new PropertyMetadata(defaultValue: "D", NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="LargeChange"/> property.
	/// </summary>
	public static readonly DependencyProperty LargeChangeProperty
		= DependencyProperty.Register(nameof(LargeChange), typeof(Int16), typeof(DataGridInt16Column), new PropertyMetadata(defaultValue: (Int16)5, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="Maximum"/> property.
	/// </summary>
	public static readonly DependencyProperty MaximumProperty
		= DependencyProperty.Register(nameof(Maximum), typeof(Int16), typeof(DataGridInt16Column), new PropertyMetadata(defaultValue: Int16.MaxValue, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="Minimum"/> property.
	/// </summary>
	public static readonly DependencyProperty MinimumProperty
		= DependencyProperty.Register(nameof(Minimum), typeof(Int16), typeof(DataGridInt16Column), new PropertyMetadata(defaultValue: Int16.MinValue, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="PickerKind"/> property.
	/// </summary>
	public static readonly DependencyProperty PickerKindProperty
		= DependencyProperty.Register(nameof(PickerKind), typeof(Int16EditBoxPickerKind), typeof(DataGridInt16Column), new PropertyMetadata(defaultValue: Int16EditBoxPickerKind.Calculator, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="SmallChange"/> property.
	/// </summary>
	public static readonly DependencyProperty SmallChangeProperty
		= DependencyProperty.Register(nameof(SmallChange), typeof(Int16), typeof(DataGridInt16Column), new PropertyMetadata(defaultValue: (Int16)1, NotifyPropertyChangeForRefreshContent));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the class.
	/// </summary>
	static DataGridInt16Column() {
		HasPopupProperty.OverrideMetadata(typeof(DataGridInt16Column), new PropertyMetadata(defaultValue: false));
		IsArrowKeyPartNavigationEnabledProperty.OverrideMetadata(typeof(DataGridInt16Column), new PropertyMetadata(defaultValue: false));
		SpinnerVisibilityProperty.OverrideMetadata(typeof(DataGridInt16Column), new PropertyMetadata(defaultValue: SpinnerVisibility.VisibleWhenActive));
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void ApplyStandardValues(FrameworkElement targetElement) {
		base.ApplyStandardValues(targetElement);

		if (targetElement is Int16EditBox) {
			ApplyValue(DefaultValueProperty, targetElement, Int16EditBox.DefaultValueProperty);
			ApplyValue(FormatProperty, targetElement, Int16EditBox.FormatProperty);
			ApplyValue(LargeChangeProperty, targetElement, Int16EditBox.LargeChangeProperty);
			ApplyValue(MaximumProperty, targetElement, Int16EditBox.MaximumProperty);
			ApplyValue(MinimumProperty, targetElement, Int16EditBox.MinimumProperty);
			ApplyValue(PickerKindProperty, targetElement, Int16EditBox.PickerKindProperty);
			ApplyValue(SmallChangeProperty, targetElement, Int16EditBox.SmallChangeProperty);
		}
	}

	/// <summary>
	/// The value to set when incrementing/decrementing from a <c>null</c> value.
	/// </summary>
	/// <value>
	/// The default value is <c>0</c>.
	/// </value>
	public Int16 DefaultValue {
		get => (Int16)GetValue(DefaultValueProperty);
		set => SetValue(DefaultValueProperty, value);
	}

	/// <summary>
	/// The number format string.
	/// </summary>
	/// <value>
	/// The default value is <c>"D"</c>.
	/// </value>
	public string Format {
		get => (string)GetValue(FormatProperty);
		set => SetValue(FormatProperty, value);
	}

	/// <inheritdoc/>
	protected override Type GetEditBoxType()
		=> typeof(Int16EditBox);

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
	public Int16 LargeChange {
		get => (Int16)GetValue(LargeChangeProperty);
		set => SetValue(LargeChangeProperty, value);
	}

	/// <summary>
	/// The highest possible value.
	/// </summary>
	public Int16 Maximum {
		get => (Int16)GetValue(MaximumProperty);
		set => SetValue(MaximumProperty, value);
	}

	/// <summary>
	/// The lowest possible value.
	/// </summary>
	public Int16 Minimum {
		get => (Int16)GetValue(MinimumProperty);
		set => SetValue(MinimumProperty, value);
	}

	/// <summary>
	/// An <see cref="Int16EditBoxPickerKind"/> indicating the pre-defined <c>Style</c> to apply to the picker used within the popup.
	/// </summary>
	/// <value>
	/// The default value is <see cref="Int16EditBoxPickerKind.Calculator"/>.
	/// </value>
	public Int16EditBoxPickerKind PickerKind {
		get => (Int16EditBoxPickerKind)GetValue(PickerKindProperty);
		set => SetValue(PickerKindProperty, value);
	}

	/// <summary>
	/// The small change value.
	/// </summary>
	/// <value>
	/// The default value is <c>1</c>.
	/// </value>
	public Int16 SmallChange {
		get => (Int16)GetValue(SmallChangeProperty);
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
