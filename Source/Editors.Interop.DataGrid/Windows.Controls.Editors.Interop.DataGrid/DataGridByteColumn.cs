namespace ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid;

/// <summary>
/// Represents a data-bound column for use in a <c>DataGrid</c> that utilizes the <see cref="ByteEditBox"/> control.
/// </summary>
public class DataGridByteColumn : DataGridPartEditBoxColumnBase<Byte?> {

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="DefaultValue"/> property.
	/// </summary>
	public static readonly DependencyProperty DefaultValueProperty
		= DependencyProperty.Register(nameof(DefaultValue), typeof(Byte), typeof(DataGridByteColumn), new PropertyMetadata(defaultValue: (Byte)0, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="Format"/> property.
	/// </summary>
	public static readonly DependencyProperty FormatProperty
		= DependencyProperty.Register(nameof(Format), typeof(string), typeof(DataGridByteColumn), new PropertyMetadata(defaultValue: "D", NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="LargeChange"/> property.
	/// </summary>
	public static readonly DependencyProperty LargeChangeProperty
		= DependencyProperty.Register(nameof(LargeChange), typeof(Byte), typeof(DataGridByteColumn), new PropertyMetadata(defaultValue: (Byte)5, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="Maximum"/> property.
	/// </summary>
	public static readonly DependencyProperty MaximumProperty
		= DependencyProperty.Register(nameof(Maximum), typeof(Byte), typeof(DataGridByteColumn), new PropertyMetadata(defaultValue: Byte.MaxValue, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="Minimum"/> property.
	/// </summary>
	public static readonly DependencyProperty MinimumProperty
		= DependencyProperty.Register(nameof(Minimum), typeof(Byte), typeof(DataGridByteColumn), new PropertyMetadata(defaultValue: Byte.MinValue, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="PickerKind"/> property.
	/// </summary>
	public static readonly DependencyProperty PickerKindProperty
		= DependencyProperty.Register(nameof(PickerKind), typeof(ByteEditBoxPickerKind), typeof(DataGridByteColumn), new PropertyMetadata(defaultValue: ByteEditBoxPickerKind.Default, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="SmallChange"/> property.
	/// </summary>
	public static readonly DependencyProperty SmallChangeProperty
		= DependencyProperty.Register(nameof(SmallChange), typeof(Byte), typeof(DataGridByteColumn), new PropertyMetadata(defaultValue: (Byte)1, NotifyPropertyChangeForRefreshContent));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public DataGridByteColumn() {
		HasPopup = false;
		IsArrowKeyPartNavigationEnabled = false;
		PickerKind = ByteEditBoxPickerKind.Calculator;
		SpinnerVisibility = SpinnerVisibility.VisibleWhenActive;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void ApplyStandardValues(FrameworkElement targetElement) {
		base.ApplyStandardValues(targetElement);

		if (targetElement is ByteEditBox) {
			ApplyValue(DefaultValueProperty, targetElement, ByteEditBox.DefaultValueProperty);
			ApplyValue(FormatProperty, targetElement, ByteEditBox.FormatProperty);
			ApplyValue(LargeChangeProperty, targetElement, ByteEditBox.LargeChangeProperty);
			ApplyValue(MaximumProperty, targetElement, ByteEditBox.MaximumProperty);
			ApplyValue(MinimumProperty, targetElement, ByteEditBox.MinimumProperty);
			ApplyValue(PickerKindProperty, targetElement, ByteEditBox.PickerKindProperty);
			ApplyValue(SmallChangeProperty, targetElement, ByteEditBox.SmallChangeProperty);
		}
	}

	/// <summary>
	/// The value to set when incrementing/decrementing from a <c>null</c> value.
	/// </summary>
	/// <value>
	/// The default value is <c>0</c>.
	/// </value>
	public Byte DefaultValue {
		get => (Byte)GetValue(DefaultValueProperty);
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
		=> typeof(ByteEditBox);

	/// <summary>
	/// The large change value.
	/// </summary>
	/// <value>
	/// The default value is <c>5</c>.
	/// </value>
	public Byte LargeChange {
		get => (Byte)GetValue(LargeChangeProperty);
		set => SetValue(LargeChangeProperty, value);
	}

	/// <summary>
	/// The highest possible value.
	/// </summary>
	public Byte Maximum {
		get => (Byte)GetValue(MaximumProperty);
		set => SetValue(MaximumProperty, value);
	}

	/// <summary>
	/// The lowest possible value.
	/// </summary>
	public Byte Minimum {
		get => (Byte)GetValue(MinimumProperty);
		set => SetValue(MinimumProperty, value);
	}

	/// <summary>
	/// An <see cref="ByteEditBoxPickerKind"/> indicating the pre-defined <c>Style</c> to apply to the picker used within the popup.
	/// </summary>
	/// <value>
	/// The default value is <see cref="ByteEditBoxPickerKind.Calculator"/>.
	/// </value>
	public ByteEditBoxPickerKind PickerKind {
		get => (ByteEditBoxPickerKind)GetValue(PickerKindProperty);
		set => SetValue(PickerKindProperty, value);
	}

	/// <summary>
	/// The small change value.
	/// </summary>
	/// <value>
	/// The default value is <c>1</c>.
	/// </value>
	public Byte SmallChange {
		get => (Byte)GetValue(SmallChangeProperty);
		set => SetValue(SmallChangeProperty, value);
	}

}
