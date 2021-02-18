#if WINRT
using Windows.UI.Xaml.Controls;
#else
using System.Windows.Controls;
#endif

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.RepairShopScheduling {

	/// <summary>
	/// Provides the user control for an employee header.
	/// </summary>
	public partial class EmployeeHeaderControl  : UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>EmployeeHeaderControl </c> class.
		/// </summary>
		public EmployeeHeaderControl () {
			InitializeComponent();
		}

	}

}
