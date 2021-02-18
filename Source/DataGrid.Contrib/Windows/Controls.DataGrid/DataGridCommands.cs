using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using DataGridControl = System.Windows.Controls.DataGrid;
using System.Windows.Input;
using ActiproSoftware.Windows.Media;

namespace ActiproSoftware.Windows.Controls.DataGrid {

	/// <summary>
	/// Contains the commands used for the <see cref="DataGrid"/> control.
	/// </summary>
	public static class DataGridCommands {

		private static RoutedCommand toggleFrozenColumn;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes the <see cref="DataGridCommands"/> class.
		/// </summary>
		[SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
		static DataGridCommands() {
			toggleFrozenColumn = new RoutedCommand("ToggleFrozenColumn", typeof(DataGridControl));
			CommandManager.RegisterClassCommandBinding(typeof(DataGridControl),
				new CommandBinding(toggleFrozenColumn, new ExecutedRoutedEventHandler(OnToggleFrozenColumnExecute)));
		}

		#endregion // OBJECT

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region COMMAND HANDLERS
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private static void OnToggleFrozenColumnExecute(object sender, ExecutedRoutedEventArgs e) {
			DataGridColumnHeader header = e.Parameter as DataGridColumnHeader;
			if (null != header) {
				DataGridControl datagrid = VisualTreeHelperExtended.GetAncestor(header, typeof(DataGridControl)) as DataGridControl;
				DataGridColumn column = header.Column;
				if (null != datagrid && null != column) {
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

		#endregion // COMMAND HANDLERS

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the <see cref="RoutedCommand"/> that is used to toggle the frozen state of a <see cref="DataGridColumn"/>.
		/// </summary>
		/// <value>The <see cref="RoutedCommand"/> that is used to toggle the frozen state of a <see cref="DataGridColumn"/>.</value>
		/// <remarks>
		/// The related <see cref="DataGridColumnHeader"/> must be located in the command parameter.
		/// </remarks>
		public static RoutedCommand ToggleFrozenColumn {
			get { return toggleFrozenColumn; }
		}

		#endregion // PUBLIC PROCEDURES

	}
}
