using System;
using System.Diagnostics.CodeAnalysis;

namespace ActiproSoftware.Windows.Controls.Editors.Interop.DataGrid {

	/// <summary>
	/// Represents a data-bound column for use in a <c>DataGrid</c> that utilizes the <see cref="DateEditBox"/> control.
	/// </summary>
	public class DataGridDateColumn : DataGridDateTimeColumn {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the <see cref="DataGridDateColumn"/> class.
		/// </summary>
		public DataGridDateColumn() {
			this.Format = "d";
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
			return typeof(DateEditBox);
		}

	}

}
