#if WINRT
using Windows.UI.Xaml.Controls;
#else
using System.Windows.Controls;
#endif

namespace ActiproSoftware.ProductSamples.ChartsSamples.Demo.Baseball {

	/// <summary>
	/// Represents a player card view.
	/// </summary>
	public partial class PlayerCard : UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>PlayerCard</c> class.
		/// </summary>
		public PlayerCard() {
			InitializeComponent();
		}

		#endregion OBJECT
	}
}