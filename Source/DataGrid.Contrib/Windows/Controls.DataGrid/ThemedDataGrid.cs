using DataGridControl = System.Windows.Controls.DataGrid;

namespace ActiproSoftware.Windows.Controls.DataGrid;

/// <summary>
/// Represents a <see cref="DataGridControl"/> that uses custom themes and integrates into the Actipro theme manager.
/// </summary>
public class ThemedDataGrid : DataGridControl {

	private Style? _defaultCheckBoxEditingStyle;
	private Style? _defaultCheckBoxStyle;
	private Style? _defaultComboBoxStyle;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the class.
	/// </summary>
	static ThemedDataGrid() {
		DefaultStyleKeyProperty.OverrideMetadata(typeof(ThemedDataGrid), new FrameworkPropertyMetadata(defaultValue: typeof(ThemedDataGrid)));
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Applies a themed style to the element in the specified column, if the current style is defaulted.
	/// </summary>
	/// <param name="column">The column to examine.</param>
	protected virtual void ApplyThemedStyle(DataGridCheckBoxColumn column) {
		if (column is null)
			throw new ArgumentNullException(nameof(column));

		// If one of the styles is defaulted...
		if (column.ElementStyle == DataGridCheckBoxColumn.DefaultElementStyle
			|| column.EditingElementStyle == DataGridCheckBoxColumn.DefaultEditingElementStyle
		) {
			if (column.ElementStyle == DataGridCheckBoxColumn.DefaultElementStyle) {
				// Create the default style as necessary
				if (_defaultCheckBoxStyle is null) {
					var basedOnStyle = TryFindResource(SharedResourceKeys.CheckBoxStyleKey) as Style;
					if (basedOnStyle is null)
						return;

					_defaultCheckBoxStyle = new Style(typeof(CheckBox)) {
						BasedOn = basedOnStyle,
						Setters = {
							new Setter(IsHitTestVisibleProperty, false),
							new Setter(FocusableProperty, false),
							new Setter(HorizontalAlignmentProperty, HorizontalAlignment.Center),
							new Setter(VerticalAlignmentProperty, VerticalAlignment.Top),
						}
					};
					_defaultCheckBoxStyle.Seal();
				}

				column.ElementStyle = _defaultCheckBoxStyle;
			}

			if (column.EditingElementStyle == DataGridCheckBoxColumn.DefaultEditingElementStyle) {
				// Create the default style as necessary
				if (_defaultCheckBoxEditingStyle is null) {
					var basedOnStyle = TryFindResource(SharedResourceKeys.CheckBoxStyleKey) as Style;
					if (basedOnStyle is null)
						return;

					_defaultCheckBoxEditingStyle = new Style(typeof(CheckBox)) {
						BasedOn = basedOnStyle,
						Setters = {
							new Setter(HorizontalAlignmentProperty, HorizontalAlignment.Center),
							new Setter(VerticalAlignmentProperty, VerticalAlignment.Top),
						}
					};
					_defaultCheckBoxEditingStyle.Seal();
				}

				column.EditingElementStyle = _defaultCheckBoxEditingStyle;
			}
		}
	}

	/// <summary>
	/// Applies a themed style to the element in the specified column, if the current style is defaulted.
	/// </summary>
	/// <param name="column">The column to examine.</param>
	protected virtual void ApplyThemedStyle(DataGridComboBoxColumn column) {
		if (column is null)
			throw new ArgumentNullException(nameof(column));

		// If one of the styles is defaulted...
		if (
			column.ElementStyle == DataGridComboBoxColumn.DefaultElementStyle
			|| column.EditingElementStyle == DataGridComboBoxColumn.DefaultEditingElementStyle
		) {
			// Create the default style as necessary
			if (_defaultComboBoxStyle is null) {
				var basedOnStyle = TryFindResource(SharedResourceKeys.ComboBoxStyleKey) as Style;
				if (basedOnStyle is null)
					return;

				_defaultComboBoxStyle = new Style(typeof(ComboBox)) {
					BasedOn = basedOnStyle,
					Setters = {
						new Setter(IsSynchronizedWithCurrentItemProperty, false)
					}
				};
				_defaultComboBoxStyle.Seal();
			}

			if (column.ElementStyle == DataGridComboBoxColumn.DefaultElementStyle)
				column.ElementStyle = _defaultComboBoxStyle;

			if (column.EditingElementStyle == DataGridComboBoxColumn.DefaultEditingElementStyle)
				column.EditingElementStyle = _defaultComboBoxStyle;
		}
	}

	/// <inheritdoc/>
	protected override void OnAutoGeneratingColumn(DataGridAutoGeneratingColumnEventArgs e) {
		if (e is null)
			throw new ArgumentNullException(nameof(e));

		// Apply themed styles as appropriate
		switch (e.Column) {
			case DataGridComboBoxColumn comboBoxColumn:
				ApplyThemedStyle(comboBoxColumn);
				break;
			case DataGridCheckBoxColumn checkBoxColumn:
				ApplyThemedStyle(checkBoxColumn);
				break;
		}

		// Call the base method
		base.OnAutoGeneratingColumn(e);
	}

}
