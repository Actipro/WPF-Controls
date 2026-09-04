namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.RepairShopScheduling;

/// <summary>
/// Stores information about a scheduled downtime (break).
/// </summary>
/// <param name="hours">The number of hours for the task to complete.</param>
public class ScheduledDowntimeModel(int hours) : TaskModelBase(hours) {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override string Name
		=> "Scheduled Downtime";

}
