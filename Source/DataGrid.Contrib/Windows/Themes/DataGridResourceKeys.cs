using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using DataGridControl = System.Windows.Controls.DataGrid;

namespace ActiproSoftware.Windows.Themes {

	/// <summary>
	/// Provides access to the resource keys that identify all reusable styles/resources included in this assembly.
	/// </summary>
	public static class DataGridResourceKeys {

		// Styles
		private static ComponentResourceKey dataGridCellStyleKey;
		private static ComponentResourceKey dataGridColumnHeaderStyleKey;
		private static ComponentResourceKey dataGridRowHeaderStyleKey;
		private static ComponentResourceKey dataGridRowStyleKey;
		private static ComponentResourceKey dataGridSelectAllButtonStyleKey;
		private static ComponentResourceKey dataGridStyleKey;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region RESOURCE KEYS (STYLES)
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for a <see cref="Style"/> that may be applied to <see cref="DataGridCell"/> elements.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey DataGridCellStyleKey {
			get {
				if (dataGridCellStyleKey == null)
					dataGridCellStyleKey = new ComponentResourceKey(typeof(DataGridResourceKeys), "DataGridCellStyleKey");
				return dataGridCellStyleKey;
			}
		}

		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for a <see cref="Style"/> that may be applied to <see cref="DataGridColumnHeader"/> elements.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey DataGridColumnHeaderStyleKey {
			get {
				if (dataGridColumnHeaderStyleKey == null)
					dataGridColumnHeaderStyleKey = new ComponentResourceKey(typeof(DataGridResourceKeys), "DataGridColumnHeaderStyleKey");
				return dataGridColumnHeaderStyleKey;
			}
		}

		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for a <see cref="Style"/> that may be applied to <see cref="DataGridRowHeader"/> elements.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey DataGridRowHeaderStyleKey {
			get {
				if (dataGridRowHeaderStyleKey == null)
					dataGridRowHeaderStyleKey = new ComponentResourceKey(typeof(DataGridResourceKeys), "DataGridRowHeaderStyleKey");
				return dataGridRowHeaderStyleKey;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for a <see cref="Style"/> that may be applied to <see cref="DataGridRow"/> elements.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey DataGridRowStyleKey {
			get {
				if (dataGridRowStyleKey == null)
					dataGridRowStyleKey = new ComponentResourceKey(typeof(DataGridResourceKeys), "DataGridRowStyleKey");
				return dataGridRowStyleKey;
			}
		}

		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for a <see cref="Style"/> that may be applied to the select all button.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey DataGridSelectAllButtonStyleKey {
			get {
				if (dataGridSelectAllButtonStyleKey == null)
					dataGridSelectAllButtonStyleKey = new ComponentResourceKey(typeof(DataGridResourceKeys), "DataGridSelectAllButtonStyleKey");
				return dataGridSelectAllButtonStyleKey;
			}
		}

		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for a <see cref="Style"/> that may be applied to <see cref="DataGridControl"/> elements.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey DataGridStyleKey {
			get {
				if (dataGridStyleKey == null)
					dataGridStyleKey = new ComponentResourceKey(typeof(DataGridResourceKeys), "DataGridStyleKey");
				return dataGridStyleKey;
			}
		}

		#endregion // RESOURCE KEYS (STYLES)
	}
}

