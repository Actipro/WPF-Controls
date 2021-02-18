#if WINRT
using Windows.UI;
#else
using System.Windows.Media;
#endif

namespace ActiproSoftware.ProductSamples.ChartsSamples.Demo.Baseball {

	/// <summary>
	///	Sample view model for <see cref="PlayerCard"/>.
	/// </summary>
	public class PlayerCardSampleViewModel : Batter {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>PlayerCardSampleViewModel</c> class.
		/// </summary>
		public PlayerCardSampleViewModel() {
			FirstName = "Bob";
			LastName= "Johnson";
			Number = 39;
			Team = new Team();
			Team.Name = "Flying Squirrels";
			Team.Color = Colors.Gray;
			BuildRandomStats(2000, 2012);
		}

		#endregion OBJECT
	}
}
