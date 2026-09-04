using DataGridControl = System.Windows.Controls.DataGrid;

namespace ActiproSoftware.Windows.Themes;

/// <summary>
/// Provides access to the resource keys that identify all reusable styles/resources included in this assembly.
/// </summary>
public static class DataGridResourceKeys {

	// Styles
	private static ComponentResourceKey? _dataGridCellStyleKey;
	private static ComponentResourceKey? _dataGridColumnHeaderStyleKey;
	private static ComponentResourceKey? _dataGridRowHeaderStyleKey;
	private static ComponentResourceKey? _dataGridRowStyleKey;
	private static ComponentResourceKey? _dataGridSelectAllButtonStyleKey;
	private static ComponentResourceKey? _dataGridStyleKey;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="ResourceKey"/> for a <see cref="Style"/> that may be applied to <see cref="DataGridCell"/> elements.
	/// </summary>
	public static ResourceKey DataGridCellStyleKey
		=> _dataGridCellStyleKey ??= new ComponentResourceKey(typeof(DataGridResourceKeys), nameof(DataGridCellStyleKey));

	/// <summary>
	/// The <see cref="ResourceKey"/> for a <see cref="Style"/> that may be applied to <see cref="DataGridColumnHeader"/> elements.
	/// </summary>
	public static ResourceKey DataGridColumnHeaderStyleKey
		=> _dataGridColumnHeaderStyleKey ??= new ComponentResourceKey(typeof(DataGridResourceKeys), nameof(DataGridColumnHeaderStyleKey));

	/// <summary>
	/// The <see cref="ResourceKey"/> for a <see cref="Style"/> that may be applied to <see cref="DataGridRowHeader"/> elements.
	/// </summary>
	public static ResourceKey DataGridRowHeaderStyleKey
		=> _dataGridRowHeaderStyleKey ??= new ComponentResourceKey(typeof(DataGridResourceKeys), nameof(DataGridRowHeaderStyleKey));

	/// <summary>
	/// The <see cref="ResourceKey"/> for a <see cref="Style"/> that may be applied to <see cref="DataGridRow"/> elements.
	/// </summary>
	public static ResourceKey DataGridRowStyleKey
		=> _dataGridRowStyleKey ??= new ComponentResourceKey(typeof(DataGridResourceKeys), nameof(DataGridRowStyleKey));

	/// <summary>
	/// The <see cref="ResourceKey"/> for a <see cref="Style"/> that may be applied to the select all button.
	/// </summary>
	public static ResourceKey DataGridSelectAllButtonStyleKey
		=> _dataGridSelectAllButtonStyleKey ??= new ComponentResourceKey(typeof(DataGridResourceKeys), nameof(DataGridSelectAllButtonStyleKey));

	/// <summary>
	/// The <see cref="ResourceKey"/> for a <see cref="Style"/> that may be applied to <see cref="DataGridControl"/> elements.
	/// </summary>
	public static ResourceKey DataGridStyleKey
		=> _dataGridStyleKey ??= new ComponentResourceKey(typeof(DataGridResourceKeys), nameof(DataGridStyleKey));

}
