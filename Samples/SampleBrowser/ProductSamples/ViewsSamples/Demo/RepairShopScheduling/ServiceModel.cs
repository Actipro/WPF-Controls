namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.RepairShopScheduling;

/// <summary>
/// Stores information about an employee service task.
/// </summary>
/// <param name="description">The description.</param>
/// <param name="orderNumber">The order number.</param>
/// <param name="itemNumber">The item number.</param>
/// <param name="itemCount">The item count.</param>
/// <param name="dueDate">The due date.</param>
/// <param name="hours">The number of hours for the task to complete.</param>
public class ServiceModel(string description, string orderNumber, int itemNumber, int itemCount, DateTime dueDate, int hours) : TaskModelBase(hours) {

	private string _description = description;
	private DateTime _dueDate = dueDate;
	private int _itemCount = itemCount;
	private int _itemNumber = itemNumber;
	private string _orderNumber = orderNumber;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The description.
	/// </summary>
	public string Description {
		get => _description;
		set => SetProperty(ref _description, value);
	}

	/// <summary>
	/// The due date.
	/// </summary>
	public DateTime DueDate {
		get => _dueDate;
		set => SetProperty(ref _dueDate, value);
	}

	/// <summary>
	/// The item count.
	/// </summary>
	public int ItemCount {
		get => _itemCount;
		set => SetProperty(ref _itemCount, value);
	}

	/// <summary>
	/// The item number.
	/// </summary>
	public int ItemNumber {
		get => _itemNumber;
		set => SetProperty(ref _itemNumber, value);
	}

	/// <inheritdoc/>
	public override string Name
		=> Description;

	/// <summary>
	/// The order number.
	/// </summary>
	public string OrderNumber {
		get => _orderNumber;
		set => SetProperty(ref _orderNumber, value);
	}

}
