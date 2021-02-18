using System;
using System.Diagnostics.CodeAnalysis;

namespace ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid {

	/// <summary>
	/// Represents a data-bound column for use in a <c>DataGrid</c> that utilizes the <see cref="TimeEditBox"/> control.
	/// </summary>
	public class DataGridTimeColumn : DataGridDateTimeColumn {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the <see cref="DataGridTimeColumn"/> class.
		/// </summary>
		public DataGridTimeColumn() {
			this.Format = "t";
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the type of the associated <c>PartEditBoxBase</c>-derived control.
		/// </summary>
		/// <returns>The type of the associated <c>PartEditBoxBase</c>-derived control.</returns>
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		protected override Type GetEditBoxType() {
			return typeof(TimeEditBox);
		}

	}

}
