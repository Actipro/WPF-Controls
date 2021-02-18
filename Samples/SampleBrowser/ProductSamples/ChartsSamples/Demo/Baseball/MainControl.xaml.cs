#if WINRT
using Windows.UI.Xaml.Controls;
#else
using System.Windows.Controls;
#endif

namespace ActiproSoftware.ProductSamples.ChartsSamples.Demo.Baseball {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl : UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();
		}

		#endregion OBJECT
	}
}