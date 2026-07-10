namespace ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid;

/// <summary>
/// Represents a data-bound column for use in a <c>DataGrid</c> that utilizes the <see cref="DateEditBox"/> control.
/// </summary>
public class DataGridDateColumn : DataGridDateTimeColumn {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the class.
	/// </summary>
	static DataGridDateColumn() {
		FormatProperty.OverrideMetadata(typeof(DataGridDateColumn), new PropertyMetadata(defaultValue: "d"));
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <value>
	/// The default value is <c>"d"</c>.
	/// </value>
	/// <inheritdoc cref="DataGridDateTimeColumn.Format"/>
	public new string Format {
		// Property redefined to change the default value doc comment
		get => base.Format;
		set => base.Format = value;
	}

	/// <inheritdoc/>
	protected override Type GetEditBoxType()
		=> typeof(DateEditBox);

}
