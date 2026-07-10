namespace ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid;

public partial class DataGridMaskedStringColumn {

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="IsCaseAutoCorrected"/> property.
	/// </summary>
	public static readonly DependencyProperty IsCaseAutoCorrectedProperty
		= DependencyProperty.Register(nameof(IsCaseAutoCorrected), typeof(bool), typeof(DataGridMaskedStringColumn), new PropertyMetadata(defaultValue: false, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="IsCaseSensitive"/> property.
	/// </summary>
	public static readonly DependencyProperty IsCaseSensitiveProperty
		= DependencyProperty.Register(nameof(IsCaseSensitive), typeof(bool), typeof(DataGridMaskedStringColumn), new PropertyMetadata(defaultValue: false, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="Mask"/> property.
	/// </summary>
	public static readonly DependencyProperty MaskProperty
		= DependencyProperty.Register(nameof(Mask), typeof(string), typeof(DataGridMaskedStringColumn), new PropertyMetadata(defaultValue: null, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="MaskKind"/> property.
	/// </summary>
	public static readonly DependencyProperty MaskKindProperty
		= DependencyProperty.Register(nameof(MaskKind), typeof(MaskKind), typeof(DataGridMaskedStringColumn), new PropertyMetadata(defaultValue: MaskKind.Regex, NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="PromptChar"/> property.
	/// </summary>
	public static readonly DependencyProperty PromptCharProperty
		= DependencyProperty.Register(nameof(PromptChar), typeof(char), typeof(DataGridMaskedStringColumn), new PropertyMetadata(defaultValue: '\u2022', NotifyPropertyChangeForRefreshContent));

	/// <summary>
	/// Defines the <see cref="PromptVisibility"/> property.
	/// </summary>
	public static readonly DependencyProperty PromptVisibilityProperty
		= DependencyProperty.Register(nameof(PromptVisibility), typeof(MaskPromptVisibility), typeof(DataGridMaskedStringColumn), new PropertyMetadata(defaultValue: MaskPromptVisibility.FocusedOnly, NotifyPropertyChangeForRefreshContent));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Applies standard values to the specified target element.
	/// </summary>
	/// <param name="targetElement">The target element.</param>
	protected virtual void ApplyStandardValues(FrameworkElement targetElement) {
		ApplyValue(IsCaseAutoCorrectedProperty, targetElement, MaskedTextBox.IsCaseAutoCorrectedProperty);
		ApplyValue(IsCaseSensitiveProperty, targetElement, MaskedTextBox.IsCaseSensitiveProperty);
		ApplyValue(MaskProperty, targetElement, MaskedTextBox.MaskProperty);
		ApplyValue(MaskKindProperty, targetElement, MaskedTextBox.MaskKindProperty);
		ApplyValue(PromptCharProperty, targetElement, MaskedTextBox.PromptCharProperty);
		ApplyValue(PromptVisibilityProperty, targetElement, MaskedTextBox.PromptVisibilityProperty);
	}

	/// <summary>
	/// Indicates whether characters entered will have their case auto-corrected to match the defined mask.
	/// </summary>
	/// <value>
	/// <c>true</c> if characters entered will have their case auto-corrected to match the defined mask; otherwise, <c>false</c>.
	/// The default value is <c>false</c>.
	/// </value>
	/// <remarks>
	/// When this property is set to <c>true</c>, the <see cref="IsCaseSensitive"/> property is ignored.
	/// </remarks>
	public bool IsCaseAutoCorrected {
		get => (bool)GetValue(IsCaseAutoCorrectedProperty);
		set => SetValue(IsCaseAutoCorrectedProperty, value);
	}

	/// <summary>
	/// Indicates whether the mask is case sensitive.
	/// </summary>
	/// <value>
	/// <c>true</c> if the mask is case sensitive; otherwise, <c>false</c>.
	/// The default value is <c>false</c>.
	/// </value>
	/// <remarks>
	/// When the <see cref="IsCaseAutoCorrected"/> property is set to <c>true</c>, this property is ignored.
	/// </remarks>
	public bool IsCaseSensitive {
		get => (bool)GetValue(IsCaseSensitiveProperty);
		set => SetValue(IsCaseSensitiveProperty, value);
	}

	/// <summary>
	/// The text that designates the input mask.
	/// </summary>
	/// <value>
	/// The default value is <c>null</c>.
	/// </value>
	public string? Mask {
		get => (string)GetValue(MaskProperty);
		set => SetValue(MaskProperty, value);
	}

	/// <summary>
	/// The kind of input mask.
	/// </summary>
	/// <value>
	/// The default value is <see cref="MaskKind.Regex"/>.
	/// </value>
	public MaskKind MaskKind {
		get => (MaskKind)GetValue(MaskKindProperty);
		set => SetValue(MaskKindProperty, value);
	}

	/// <summary>
	/// The character used to indicate required input.
	/// </summary>
	/// <value>
	/// The default value is <c>\u2022</c>.
	/// </value>
	public char PromptChar {
		get => (char)GetValue(PromptCharProperty);
		set => SetValue(PromptCharProperty, value);
	}

	/// <summary>
	/// The visibility of mask prompts.
	/// </summary>
	/// <value>
	/// The default value is <see cref="MaskPromptVisibility.FocusedOnly"/>.
	/// </value>
	public MaskPromptVisibility PromptVisibility {
		get => (MaskPromptVisibility)GetValue(PromptVisibilityProperty);
		set => SetValue(PromptVisibilityProperty, value);
	}

}
