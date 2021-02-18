using System;
using System.Windows.Controls.Primitives;

namespace ActiproSoftware.Windows.Controls.DataGrid {

	/// <summary>
	/// Specifies the various tracking modes available by the <see cref="FocusBehavior"/>.
	/// </summary>
	[Flags]
	public enum FocusTrackingModes {

		/// <summary>
		/// Indicates that the focus will not be tracked.
		/// </summary>
		None = 0x00,

		/// <summary>
		/// Indicates <see cref="DataGridColumnHeader"/> focus will be tracked.
		/// </summary>
		Headers = 0x01,

		/// <summary>
		/// Indicates that all available types will be tracked.
		/// </summary>
		All = Headers
	}
}
