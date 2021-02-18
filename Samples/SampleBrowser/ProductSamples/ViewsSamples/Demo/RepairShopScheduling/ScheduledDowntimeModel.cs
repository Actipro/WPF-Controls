namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.RepairShopScheduling {

	/// <summary>
	/// Stores information about a scheduled downtime (break).
	/// </summary>
	public class ScheduledDowntimeModel : TaskModelBase {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the <c>ScheduledDowntimeModel</c> class.
		/// </summary>
		/// <param name="hours">The number of hours for the task to complete.</param>
		public ScheduledDowntimeModel(int hours) : base(hours) {}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the name of the task.
		/// </summary>
		/// <value>The name of the task.</value>
		public override string Name {
			get {
				return "Scheduled Downtime";
			}
		}
		
	}

}
