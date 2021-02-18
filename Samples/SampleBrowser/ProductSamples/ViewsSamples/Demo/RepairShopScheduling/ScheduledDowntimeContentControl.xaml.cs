#if WINRT
using Windows.UI.Xaml.Controls;
#else
using System.Windows.Controls;
#endif

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.RepairShopScheduling {

	/// <summary>
	/// Provides the user control for a scheduled downtime content.
	/// </summary>
	public partial class ScheduledDowntimeContentControl : UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>ScheduledDowntimeContentControl</c> class.
		/// </summary>
		public ScheduledDowntimeContentControl() {
			InitializeComponent();
		}

	}

}
