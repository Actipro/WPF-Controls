using System;

namespace ActiproSoftware.ProductSamples.DataGridSamples.Demo.LicenseDashboard {

	/// <summary>
	/// Indicates the various states of a license.
	/// </summary>
	public enum LicenseState {

		/// <summary>
		/// Indicates that the license has expired.
		/// </summary>
		Expired,

		/// <summary>
		/// Indicates that the license is expiring soon.
		/// </summary>
		ExpiringSoon,

		/// <summary>
		/// Indicates that the license is valid and not expiring any time soon.
		/// </summary>
		Valid
	}
}
