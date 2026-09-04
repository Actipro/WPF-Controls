namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.RepairShopScheduling;

/// <summary>
/// Stores information about a repair shop.
/// </summary>
public class ShopModel : ObservableObjectBase {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The collection of shop employees.
	/// </summary>
	/// <value>The collection of shop employees.</value>
	public ObservableCollection<EmployeeModel> Employees { get; } = [];

}
