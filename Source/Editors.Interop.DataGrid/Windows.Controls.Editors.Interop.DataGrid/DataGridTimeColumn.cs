namespace ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid;

/// <summary>
/// Represents a data-bound column for use in a <c>DataGrid</c> that utilizes the <see cref="TimeEditBox"/> control.
/// </summary>
public class DataGridTimeColumn : DataGridDateTimeColumn {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the class.
	/// </summary>
	static DataGridTimeColumn() {
		FormatProperty.OverrideMetadata(typeof(DataGridTimeColumn), new PropertyMetadata(defaultValue: "t"));
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <value>
	/// The default value is <c>"t"</c>.
	/// </value>
	/// <inheritdoc cref="DataGridDateTimeColumn.Format"/>
	public new string Format {
		// Property redefined to change the default value doc comment
		get => base.Format;
		set => base.Format = value;
	}

	/// <inheritdoc/>
	protected override Type GetEditBoxType()
		=> typeof(TimeEditBox);

}
