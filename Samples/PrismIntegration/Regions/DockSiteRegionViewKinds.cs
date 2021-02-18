using System;

namespace ActiproSoftware.Windows.PrismIntegration.Regions {

	/// <summary>
	/// Indicates the view kinds supported by <see cref="DockSiteRegionAdapter"/>.
	/// </summary>
	[Flags]
	internal enum DockSiteRegionViewKinds {

		/// <summary>
		/// No views are supported.
		/// </summary>
		None = 0x0,

		/// <summary>
		/// Indicates the views are documents.
		/// </summary>
		Document = 0x1,
		
		/// <summary>
		/// Indicates the view are tools.
		/// </summary>
		Tool = 0x2,

		/// <summary>
		/// Indicates the views can be documents or tools.
		/// </summary>
		Both = Document | Tool,

	}

}
