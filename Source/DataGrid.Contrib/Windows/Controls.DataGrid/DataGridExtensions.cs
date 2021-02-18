using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using DataGridControl = System.Windows.Controls.DataGrid;

namespace ActiproSoftware.Windows.Controls.DataGrid {

	/// <summary>
	/// Represents a set of helper extension methods for the <c>DataGrid</c>.
	/// </summary>
	public static class DataGridExtensions {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the <c>DataGridRow</c> at the specified index.
		/// </summary>
		/// <param name="dataGrid">The <c>DataGrid</c>.</param>
		/// <param name="index">The index of the <c>DataGridRow</c>.</param>
		/// <returns>The <c>DataGridRow</c> at the specified index.</returns>
		public static DataGridRow GetRow(this DataGridControl dataGrid, int index) {
			if (dataGrid == null)
				throw new ArgumentNullException("dataGrid");
			else if (dataGrid.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
				return null;

			// 1/6/2009 - Added check for "dataGrid.IsLoaded" in if-statement below since the if-block will throw an exception if
			//   the grid is not loaded (3E0-12D5B392-DD90).
			DataGridRow row = dataGrid.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
			if (null == row && dataGrid.EnableRowVirtualization && dataGrid.IsLoaded && dataGrid.IsVisible) {
				// Rows are virtualized, so we need to bring it into view first, then get the row
				dataGrid.ScrollIntoView(dataGrid.Items[index]);
				dataGrid.UpdateLayout();
				row = dataGrid.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
			}

			return row;
		}

		/// <summary>
		/// Gets the <c>DataGridRow</c> for the specified item.
		/// </summary>
		/// <param name="dataGrid">The <c>DataGrid</c>.</param>
		/// <param name="item">The item.</param>
		/// <returns>The <c>DataGridRow</c> for the specified item.</returns>
		public static DataGridRow GetRow(this DataGridControl dataGrid, object item) {
			if (dataGrid == null)
				throw new ArgumentNullException("dataGrid");

			int index = dataGrid.Items.IndexOf(item);
			// 3/23/2010 - Ensure index is valid (http://www.actiprosoftware.com/Support/Forums/ViewForumTopic.aspx?ForumTopicID=4710#17370)
			return ((index != -1) ? dataGrid.GetRow(index) : null);
		}

		#endregion // PUBLIC PROCEDURES
	}
}
