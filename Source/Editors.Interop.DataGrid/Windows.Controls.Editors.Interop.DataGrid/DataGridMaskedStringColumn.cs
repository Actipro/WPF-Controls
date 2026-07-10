using ActiproSoftware.Security;

namespace ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid;

/// <summary>
/// Represents a base class for data-bound columns for use in a <c>DataGrid</c> that utilize <c>MaskedTextBox</c> controls.
/// </summary>
public partial class DataGridMaskedStringColumn : DataGridBoundColumnBase {

	private static Style? _defaultElementStyle;
	private static Style? _defaultEditingElementStyle;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the class.
	/// </summary>
	static DataGridMaskedStringColumn() {
		EditingElementStyleProperty.OverrideMetadata(typeof(DataGridMaskedStringColumn), new FrameworkPropertyMetadata(defaultValue: DefaultEditingElementStyle));
		ElementStyleProperty.OverrideMetadata(typeof(DataGridMaskedStringColumn), new FrameworkPropertyMetadata(defaultValue: DefaultElementStyle));
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Generates a <see cref="Style"/> that can be used as the basis for the element styles.
	/// </summary>
	/// <param name="targetType">Type of the target.</param>
	/// <param name="isEditing">if set to <c>true</c> the style will be used for the editing element.</param>
	/// <returns>A <see cref="Style"/> that can be used as the basis for the element styles.</returns>
	private static Style GenerateBaseStyle(Type targetType, bool isEditing) {
		var style = new Style(targetType);
		style.Setters.Add(new Setter(MaskedTextBox.BorderThicknessProperty, new Thickness(0.0)));
		style.Setters.Add(new Setter(MaskedTextBox.MinHeightProperty, 20.0));
		style.Setters.Add(new Setter(MaskedTextBox.PaddingProperty, new Thickness(0.0)));
		style.Setters.Add(new Setter(MaskedTextBox.VerticalContentAlignmentProperty, VerticalAlignment.Center));

		if (!isEditing) {
			style.Setters.Add(new Setter(MaskedTextBox.BackgroundProperty, Brushes.Transparent));
			style.Setters.Add(new Setter(MaskedTextBox.IsHitTestVisibleProperty, false));
			style.Setters.Add(new Setter(MaskedTextBox.IsTabStopProperty, false));
		}

		return style;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override bool CommitCellEdit(FrameworkElement editingElement) {
		if (editingElement is MaskedTextBox { IsMatched: false } textBox) {
			UpdateBindingTarget(textBox, MaskedTextBox.TextProperty);
			return false;
		}

		return base.CommitCellEdit(editingElement);
	}

	/// <summary>
	/// The default value for the <c>EditingElementStyleProperty</c> property.
	/// </summary>
	public static Style DefaultEditingElementStyle {
		get {
			if (_defaultEditingElementStyle is null) {
				_defaultEditingElementStyle = GenerateBaseStyle(typeof(MaskedTextBox), isEditing: true);
				_defaultEditingElementStyle.Seal();
			}
			return _defaultEditingElementStyle;
		}
	}

	/// <summary>
	/// The default value for the <c>EditingElementStyleProperty</c> dependency property.
	/// </summary>
	public static Style DefaultElementStyle {
		get {
			if (_defaultElementStyle is null) {
				_defaultElementStyle = GenerateBaseStyle(typeof(MaskedTextBox), isEditing: false);
				_defaultElementStyle.Seal();
			}
			return _defaultElementStyle;
		}
	}

	/// <summary>
	/// Generates an instance of a <c>MaskedTextBox</c> object.
	/// </summary>
	/// <param name="isEditing">Whether the text box will be used for editing.</param>
	/// <param name="cell">The cell.</param>
	protected virtual MaskedTextBox GenerateMaskedTextBox(bool isEditing, DataGridCell cell) {
		var textBox = ((cell?.Content is { } cellContent) && (typeof(MaskedTextBox).IsInstanceOfType(cellContent)))
			? (MaskedTextBox)cellContent
			: TrustedCodeService.CreateInstance<MaskedTextBox>();

		ApplyStandardValues(textBox);
		if (isEditing)
			textBox.Style = EditingElementStyle;
		else
			textBox.Style = ElementStyle;
		ApplyBinding(textBox, MaskedTextBox.TextProperty);

		return textBox;
	}

	/// <inheritdoc/>
	protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
		=> GenerateMaskedTextBox(isEditing: true, cell);

	/// <inheritdoc/>
	protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
		=> GenerateMaskedTextBox(isEditing: false, cell);

	/// <inheritdoc/>
	protected override object? PrepareCellForEdit(FrameworkElement editingElement, RoutedEventArgs editingEventArgs) {
		if (editingElement is not MaskedTextBox textBox)
			return null;

		textBox.Focus();
		return textBox.Text;
	}

}
