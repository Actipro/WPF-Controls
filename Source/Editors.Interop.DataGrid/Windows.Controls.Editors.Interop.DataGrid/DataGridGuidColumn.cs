namespace ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid;

/// <summary>
/// Represents a data-bound column for use in a <c>DataGrid</c> that utilizes the <see cref="GuidEditBox"/> control.
/// </summary>
public class DataGridGuidColumn : DataGridPartEditBoxColumnBase<Guid?> {

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="Format"/> property.
	/// </summary>
	public static readonly DependencyProperty FormatProperty
		= DependencyProperty.Register(nameof(Format), typeof(string), typeof(DataGridGuidColumn), new PropertyMetadata(defaultValue: "D", NotifyPropertyChangeForRefreshContent));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the class.
	/// </summary>
	static DataGridGuidColumn() {
		HasPopupProperty.OverrideMetadata(typeof(DataGridGuidColumn), new PropertyMetadata(defaultValue: false));
		IsArrowKeyPartNavigationEnabledProperty.OverrideMetadata(typeof(DataGridGuidColumn), new PropertyMetadata(defaultValue: false));
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void ApplyStandardValues(FrameworkElement targetElement) {
		base.ApplyStandardValues(targetElement);

		if (targetElement is GuidEditBox) {
			ApplyValue(FormatProperty, targetElement, GuidEditBox.FormatProperty);
		}
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
		=> typeof(GuidEditBox);

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

}
