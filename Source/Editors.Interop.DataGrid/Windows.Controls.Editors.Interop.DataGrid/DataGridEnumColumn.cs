namespace ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid;

/// <summary>
/// Represents a data-bound column for use in a <c>DataGrid</c> that utilizes the <see cref="EnumEditBox"/> control.
/// </summary>
public class DataGridEnumColumn : DataGridPartEditBoxColumnBase<object> {

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="EnumSortComparer"/> property.
	/// </summary>
	public static readonly DependencyProperty EnumSortComparerProperty
		= DependencyProperty.Register(nameof(EnumSortComparer), typeof(IComparer<Enum>), typeof(DataGridEnumColumn), new PropertyMetadata(defaultValue: null, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="EnumType"/> property.
	/// </summary>
	public static readonly DependencyProperty EnumTypeProperty
		= DependencyProperty.Register(nameof(EnumType), typeof(Type), typeof(DataGridEnumColumn), new PropertyMetadata(defaultValue: null, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="UseDisplayAttributes"/> property.
	/// </summary>
	public static readonly DependencyProperty UseDisplayAttributesProperty
		= DependencyProperty.Register(nameof(UseDisplayAttributes), typeof(bool), typeof(DataGridEnumColumn), new PropertyMetadata(defaultValue: false, NotifyPropertyChangeForRefreshContent));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the class.
	/// </summary>
	static DataGridEnumColumn() {
		IsArrowKeyPartNavigationEnabledProperty.OverrideMetadata(typeof(DataGridEnumColumn), new PropertyMetadata(defaultValue: false));
		IsEditableProperty.OverrideMetadata(typeof(DataGridEnumColumn), new PropertyMetadata(defaultValue: false));
		SpinWrappingProperty.OverrideMetadata(typeof(DataGridEnumColumn), new PropertyMetadata(defaultValue: SpinWrapping.SimpleWrap));
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void ApplyStandardValues(FrameworkElement targetElement) {
		base.ApplyStandardValues(targetElement);

		if (targetElement is EnumEditBox) {
			ApplyValue(EnumSortComparerProperty, targetElement, EnumEditBox.EnumSortComparerProperty);
			ApplyValue(EnumTypeProperty, targetElement, EnumEditBox.EnumTypeProperty);
			ApplyValue(UseDisplayAttributesProperty, targetElement, EnumEditBox.UseDisplayAttributesProperty);
		}
	}

	/// <summary>
	/// The <see cref="IComparer{Enum}"/> used to sort the enumeration values.
	/// </summary>
	/// <value>
	/// The <see cref="IComparer{Enum}"/> used to sort the enumeration values; otherwise <c>null</c> to indicate no sorting, which will use the order the enumeration values are defined.
	/// </value>
	public IComparer<Enum>? EnumSortComparer {
		get => (IComparer<Enum>)GetValue(EnumSortComparerProperty);
		set => SetValue(EnumSortComparerProperty, value);
	}

	/// <summary>
	/// The enumeration type.
	/// </summary>
	public Type? EnumType {
		get => GetValue(EnumTypeProperty) as Type;
		set => SetValue(EnumTypeProperty, value);
	}

	/// <inheritdoc/>
	protected override Type GetEditBoxType()
		=> typeof(EnumEditBox);

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

	/// <value>
	/// <c>true</c> if the edit box's text area is editable; otherwise <c>false</c>.
	/// The default value is <c>false</c>.
	/// </value>
	/// <inheritdoc cref="DataGridPartEditBoxColumnBase{T}.IsEditable"/>
	public new bool IsEditable {
		// Property redefined to change the default value doc comment
		get => base.IsEditable;
		set => base.IsEditable = value;
	}

	/// <value>
	/// The default value is <see cref="SpinWrapping.SimpleWrap"/>.
	/// </value>
	/// <inheritdoc cref="DataGridPartEditBoxColumnBase{T}.SpinWrapping"/>
	public new SpinWrapping SpinWrapping {
		// Property redefined to change the default value doc comment
		get => base.SpinWrapping;
		set => base.SpinWrapping = value;
	}

	/// <summary>
	/// A value indicating whether enumeration values should be displayed using an associated <c>DisplayAttribute</c>, if any.
	/// </summary>
	/// <value>
	/// <c>true</c> if enumeration values should be displayed using an associated <c>DisplayAttribute</c>, if any; otherwise <c>false</c>.
	/// The default value is <c>false</c>.
	/// </value>
	public bool UseDisplayAttributes {
		get => (bool)GetValue(UseDisplayAttributesProperty);
		set => SetValue(UseDisplayAttributesProperty, value);
	}

}
