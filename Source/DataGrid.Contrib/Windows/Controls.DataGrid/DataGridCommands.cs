using ActiproSoftware.Windows.Extensions;
using DataGridControl = System.Windows.Controls.DataGrid;

namespace ActiproSoftware.Windows.Controls.DataGrid;

/// <summary>
/// Contains the commands used for the <see cref="DataGrid"/> control.
/// </summary>
public static class DataGridCommands {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the class.
	/// </summary>
	static DataGridCommands() {
		ToggleFrozenColumn = new RoutedCommand(nameof(ToggleFrozenColumn), typeof(DataGridControl));
		CommandManager.RegisterClassCommandBinding(typeof(DataGridControl), new CommandBinding(ToggleFrozenColumn, new ExecutedRoutedEventHandler(OnToggleFrozenColumnExecute)));
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private static void OnToggleFrozenColumnExecute(object sender, ExecutedRoutedEventArgs e) {
		if (e.Parameter is DataGridColumnHeader header) {
			var datagrid = header.FindAncestorOfType<DataGridControl>();
			var column = header.Column;
			if ((datagrid is not null) && (column is not null)) {
				if (column.IsFrozen) {
					// Need to unfreeze column
					datagrid.FrozenColumnCount = column.DisplayIndex = datagrid.FrozenColumnCount - 1;
				}
				else {
					// Need to freeze column
					column.DisplayIndex = datagrid.FrozenColumnCount++;
				}
			}
		}
	}

	/// <summary>
	/// The <see cref="RoutedCommand"/> that is used to toggle the frozen state of a <see cref="DataGridColumn"/>.
	/// </summary>
	/// <remarks>
	/// The related <see cref="DataGridColumnHeader"/> must be located in the command parameter.
	/// </remarks>
	public static RoutedCommand ToggleFrozenColumn { get; }

}
