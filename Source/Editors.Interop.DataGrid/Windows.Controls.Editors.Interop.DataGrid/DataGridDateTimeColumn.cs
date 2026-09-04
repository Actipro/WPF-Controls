namespace ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid;

/// <summary>
/// Represents a data-bound column for use in a <c>DataGrid</c> that utilizes the <see cref="DateTimeEditBox"/> control.
/// </summary>
public class DataGridDateTimeColumn : DataGridPartEditBoxColumnBase<DateTime?> {

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="Format"/> property.
	/// </summary>
	public static readonly DependencyProperty FormatProperty
		= DependencyProperty.Register(nameof(Format), typeof(string), typeof(DataGridDateTimeColumn), new PropertyMetadata("g", NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="Maximum"/> property.
	/// </summary>
	public static readonly DependencyProperty MaximumProperty
		= DependencyProperty.Register(nameof(Maximum), typeof(DateTime), typeof(DataGridDateTimeColumn), new PropertyMetadata(new DateTime(9998, 12, 31), NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="Minimum"/> property.
	/// </summary>
	public static readonly DependencyProperty MinimumProperty
		= DependencyProperty.Register(nameof(Minimum), typeof(DateTime), typeof(DataGridDateTimeColumn), new PropertyMetadata(new DateTime(1753, 1, 1), NotifyPropertyChangeForRefreshContent));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the class.
	/// </summary>
	static DataGridDateTimeColumn() {
		SpinWrappingProperty.OverrideMetadata(typeof(DataGridDateTimeColumn), new PropertyMetadata(defaultValue: SpinWrapping.Wrap));
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void ApplyStandardValues(FrameworkElement targetElement) {
		base.ApplyStandardValues(targetElement);
		if (targetElement is DateTimeEditBox) {
			ApplyValue(FormatProperty, targetElement, DateTimeEditBox.FormatProperty);
			ApplyValue(MaximumProperty, targetElement, DateTimeEditBox.MaximumProperty);
			ApplyValue(MinimumProperty, targetElement, DateTimeEditBox.MinimumProperty);
		}
	}

	/// <summary>
	/// The date/time format string.
	/// </summary>
	/// <value>
	/// The default value is <c>"g"</c>.
	/// </value>
	public string Format {
		get => (string)GetValue(FormatProperty);
		set => SetValue(FormatProperty, value);
	}

	/// <inheritdoc/>
	protected override Type GetEditBoxType()
		=> typeof(DateTimeEditBox);

	/// <summary>
	/// The highest possible value.
	/// </summary>
	public DateTime Maximum {
		get => (DateTime)GetValue(MaximumProperty);
		set => SetValue(MaximumProperty, value);
	}

	/// <summary>
	/// The lowest possible value.
	/// </summary>
	public DateTime Minimum {
		get => (DateTime)GetValue(MinimumProperty);
		set => SetValue(MinimumProperty, value);
	}

	/// <value>
	/// The default value is <see cref="SpinWrapping.Wrap"/>.
	/// </value>
	/// <inheritdoc cref="DataGridPartEditBoxColumnBase{T}.SpinWrapping"/>
	public new SpinWrapping SpinWrapping {
		// Property redefined to change the default value doc comment
		get => base.SpinWrapping;
		set => base.SpinWrapping = value;
	}

}
