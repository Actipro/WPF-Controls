using System;
using System.Windows.Controls.Primitives;

namespace ActiproSoftware.Windows.Controls.DataGrid {

	/// <summary>
	/// Specifies the various tracking modes available by the <see cref="SelectionBehavior"/>.
	/// </summary>
	[Flags]
	public enum SelectionTrackingModes {

		/// <summary>
		/// Indicates that the selection will not be tracked.
		/// </summary>
		None = 0x00,

		/// <summary>
		/// Indicates <see cref="DataGridColumnHeader"/> selection will be tracked.
		/// </summary>
		Headers = 0x01,

		/// <summary>
		/// Indicates that all available types will be tracked.
		/// </summary>
		All = Headers
	}
}
