namespace ActiproSoftware.ProductSamples.ChartsSamples.Demo.Baseball;

/// <summary>
/// Sample view model for <see cref="PlayerCard"/>.
/// </summary>
public class PlayerCardSampleViewModel : Batter {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public PlayerCardSampleViewModel() {
		FirstName = "Bob";
		LastName = "Johnson";
		Number = 39;
		Team = new Team {
			Name = "Flying Squirrels",
			Color = Colors.Gray
		};
		BuildRandomStats(2000, 2012);
	}

}
