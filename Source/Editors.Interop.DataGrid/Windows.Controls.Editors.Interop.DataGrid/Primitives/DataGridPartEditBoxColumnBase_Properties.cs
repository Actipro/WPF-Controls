using ActiproSoftware.Windows.Controls.Editors.Primitives;

namespace ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid.Primitives;

public partial class DataGridPartEditBoxColumnBase<T> {

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="CommitTriggers"/> property.
	/// </summary>
	public static readonly DependencyProperty CommitTriggersProperty
		= DependencyProperty.Register(nameof(CommitTriggers), typeof(PartEditBoxCommitTriggers), typeof(DataGridPartEditBoxColumnBase<T>), new PropertyMetadata(defaultValue: PartEditBoxCommitTriggers.Default, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="HasPopup"/> property.
	/// </summary>
	public static readonly DependencyProperty HasPopupProperty
		= DependencyProperty.Register(nameof(HasPopup), typeof(bool), typeof(DataGridPartEditBoxColumnBase<T>), new PropertyMetadata(defaultValue: true, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="IsArrowKeyPartNavigationEnabled"/> property.
	/// </summary>
	public static readonly DependencyProperty IsArrowKeyPartNavigationEnabledProperty
		= DependencyProperty.Register(nameof(IsArrowKeyPartNavigationEnabled), typeof(bool), typeof(DataGridPartEditBoxColumnBase<T>), new PropertyMetadata(defaultValue: true, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="IsEditable"/> property.
	/// </summary>
	public static readonly DependencyProperty IsEditableProperty
		= DependencyProperty.Register(nameof(IsEditable), typeof(bool), typeof(DataGridPartEditBoxColumnBase<T>), new PropertyMetadata(defaultValue: true, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="IsNullAllowed"/> property.
	/// </summary>
	public static readonly DependencyProperty IsNullAllowedProperty
		= DependencyProperty.Register(nameof(IsNullAllowed), typeof(bool), typeof(DataGridPartEditBoxColumnBase<T>), new PropertyMetadata(defaultValue: false, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="IsUndoEnabled"/> property.
	/// </summary>
	public static readonly DependencyProperty IsUndoEnabledProperty
		= DependencyProperty.Register(nameof(IsUndoEnabled), typeof(bool), typeof(DataGridPartEditBoxColumnBase<T>), new PropertyMetadata(defaultValue: true, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="PlaceholderText"/> property.
	/// </summary>
	public static readonly DependencyProperty PlaceholderTextProperty
		= DependencyProperty.Register(nameof(PlaceholderText), typeof(string), typeof(DataGridPartEditBoxColumnBase<T>), new PropertyMetadata(defaultValue: null, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="SpinnerVisibility"/> property.
	/// </summary>
	public static readonly DependencyProperty SpinnerVisibilityProperty
		= DependencyProperty.Register(nameof(SpinnerVisibility), typeof(SpinnerVisibility), typeof(DataGridPartEditBoxColumnBase<T>), new PropertyMetadata(defaultValue: SpinnerVisibility.Collapsed, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="SpinWrapping"/> property.
	/// </summary>
	public static readonly DependencyProperty SpinWrappingProperty
		= DependencyProperty.Register(nameof(SpinWrapping), typeof(SpinWrapping), typeof(DataGridPartEditBoxColumnBase<T>), new PropertyMetadata(defaultValue: SpinWrapping.NoWrap, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="TextAlignment"/> property.
	/// </summary>
	public static readonly DependencyProperty TextAlignmentProperty
		= DependencyProperty.Register(nameof(TextAlignment), typeof(TextAlignment), typeof(DataGridPartEditBoxColumnBase<T>), new PropertyMetadata(defaultValue: TextAlignment.Left, NotifyPropertyChangeForRefreshContent));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Applies standard values to the specified target element.
	/// </summary>
	/// <param name="targetElement">The target element.</param>
	protected virtual void ApplyStandardValues(FrameworkElement targetElement) {
		ApplyValue(CommitTriggersProperty, targetElement, PartEditBoxBase<T>.CommitTriggersProperty);
		ApplyValue(HasPopupProperty, targetElement, PartEditBoxBase<T>.HasPopupProperty);
		ApplyValue(IsArrowKeyPartNavigationEnabledProperty, targetElement, PartEditBoxBase<T>.IsArrowKeyPartNavigationEnabledProperty);
		ApplyValue(IsEditableProperty, targetElement, PartEditBoxBase<T>.IsEditableProperty);
		ApplyValue(IsNullAllowedProperty, targetElement, PartEditBoxBase<T>.IsNullAllowedProperty);
		ApplyValue(IsUndoEnabledProperty, targetElement, PartEditBoxBase<T>.IsUndoEnabledProperty);
		ApplyValue(PlaceholderTextProperty, targetElement, PartEditBoxBase<T>.PlaceholderTextProperty);
		ApplyValue(SpinnerVisibilityProperty, targetElement, PartEditBoxBase<T>.SpinnerVisibilityProperty);
		ApplyValue(SpinWrappingProperty, targetElement, PartEditBoxBase<T>.SpinWrappingProperty);
		ApplyValue(TextAlignmentProperty, targetElement, PartEditBoxBase<T>.TextAlignmentProperty);
	}

	/// <summary>
	/// The triggers that will force this control to commit any changes.
	/// </summary>
	/// <value>
	/// The default value is <see cref="PartEditBoxCommitTriggers.Default"/>.
	/// </value>
	public PartEditBoxCommitTriggers CommitTriggers {
		get => (PartEditBoxCommitTriggers)GetValue(CommitTriggersProperty);
		set => SetValue(CommitTriggersProperty, value);
	}

	/// <summary>
	/// Indicates whether the control has a popup available.
	/// </summary>
	/// <value>
	/// <c>true</c> if the control has a popup available; otherwise <c>false</c>.
	/// The default value is <c>true</c>.
	/// </value>
	public bool HasPopup {
		get => (bool)GetValue(HasPopupProperty);
		set => SetValue(HasPopupProperty, value);
	}

	/// <summary>
	/// Indicates whether the left/right arrow keys can be used to move between and select editable parts.
	/// </summary>
	/// <value>
	/// <c>true</c> if the left/right arrow keys can be used to move between and select editable parts; otherwise <c>false</c>.
	/// The default value is <c>true</c>.
	/// </value>
	public bool IsArrowKeyPartNavigationEnabled {
		get => (bool)GetValue(IsArrowKeyPartNavigationEnabledProperty);
		set => SetValue(IsArrowKeyPartNavigationEnabledProperty, value);
	}

	/// <summary>
	/// Indicates whether the edit box's text area is editable.
	/// </summary>
	/// <value>
	/// <c>true</c> if the edit box's text area is editable; otherwise <c>false</c>.
	/// The default value is <c>true</c>.
	/// </value>
	/// <remarks>
	/// When <c>false</c>, the edit box behaves more like a <c>ComboBox</c>.
	/// </remarks>
	public bool IsEditable {
		get => (bool)GetValue(IsEditableProperty);
		set => SetValue(IsEditableProperty, value);
	}

	/// <summary>
	/// Indicates whether <c>null</c> values are allowed to be entered by the user.
	/// </summary>
	/// <value>
	/// <c>true</c> if <c>null</c> values are allowed to be entered by the user; otherwise <c>false</c>.
	/// The default value is <c>false</c>.
	/// </value>
	public bool IsNullAllowed {
		get => (bool)GetValue(IsNullAllowedProperty);
		set => SetValue(IsNullAllowedProperty, value);
	}

	/// <summary>
	/// Indicates whether undo/redo support is enabled for the text-editing portion of the control.
	/// </summary>
	/// <value>
	/// <c>true</c> if undo/redo support is enabled for the text-editing portion of the control.
	/// The default value is <c>true</c>.
	/// </value>
	public bool IsUndoEnabled {
		get => (bool)GetValue(IsUndoEnabledProperty);
		set => SetValue(IsUndoEnabledProperty, value);
	}

	/// <summary>
	/// The text that is displayed in the control until the value is changed by a user action or some other operation.
	/// </summary>
	public string PlaceholderText {
		get => (string)GetValue(PlaceholderTextProperty);
		set => SetValue(PlaceholderTextProperty, value);
	}

	/// <summary>
	/// A value indicating if and when the control has a spinner available.
	/// </summary>
	/// <value>
	/// The default value is <see cref="SpinnerVisibility.Collapsed"/>.
	/// </value>
	public SpinnerVisibility SpinnerVisibility {
		get => (SpinnerVisibility)GetValue(SpinnerVisibilityProperty);
		set => SetValue(SpinnerVisibilityProperty, value);
	}

	/// <summary>
	/// The wrapping behavior used when spinning past a minimum or maximum value in the active part.
	/// </summary>
	/// <value>
	/// The default value is <see cref="SpinWrapping.NoWrap"/>.
	/// </value>
	public SpinWrapping SpinWrapping {
		get => (SpinWrapping)GetValue(SpinWrappingProperty);
		set => SetValue(SpinWrappingProperty, value);
	}

	/// <summary>
	/// The text alignment of the text editing area.
	/// </summary>
	/// <value>
	/// The default value is <see cref="TextAlignment.Left"/>.
	/// </value>
	public TextAlignment TextAlignment {
		get => (TextAlignment)GetValue(TextAlignmentProperty);
		set => SetValue(TextAlignmentProperty, value);
	}

}
