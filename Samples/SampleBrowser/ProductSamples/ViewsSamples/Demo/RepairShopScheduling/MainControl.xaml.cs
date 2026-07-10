using ActiproSoftware.Windows.Controls.Views;

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.RepairShopScheduling;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	private int _nextEmployeeNumber = 1;
	private readonly Random _random = new(30);

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		InitializeModels();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Create an employee model.
	/// </summary>
	private EmployeeModel CreateEmployeeModel() {
		var model = new EmployeeModel(GetNextEmployeeName()) {
			Status = _random.Next(4) switch {
				0 => EmployeeStatus.Unavailable,
				1 => EmployeeStatus.Idle,
				2 or _ => EmployeeStatus.Working
			}
		};

		var taskCount = (int)(1 + _random.NextDouble() * 10);
		for (var taskIndex = 1; taskIndex <= taskCount; taskIndex++) {
			if ((model.Tasks.Count >= 3) && (!model.Tasks.Any(m => m is ScheduledDowntimeModel)) && (_random.Next(5) == 0)) {
				// Add some scheduled downtime
				model.Tasks.Insert(Math.Min(1 + _random.Next(4), model.Tasks.Count), CreateScheduledDowntimeModel());
			}
			else {
				// Add a regular service
				model.Tasks.Add(CreateServiceModel());
			}
		}

		return model;
	}

	/// <summary>
	/// Create a service model.
	/// </summary>
	private ServiceModel CreateServiceModel() {
		var descriptions = new string[] {
			"30K maintenance service",
			"60K maintenance service",
			"90K maintenance service",
			"Battery replacement",
			"Brake replacement",
			"Major engine repair",
			"Minor engine repair",
			"Oil change and lube service",
			"Tire repair",
			"Tire rotation and alignment",
			"Transmission repair",
		};

		var description = descriptions[_random.Next(descriptions.Length)];
		var orderNumber = 30000 + _random.Next(10000);
		var itemCount = Math.Max(1, (-1 + _random.Next(5)));
		var itemNumber = (1 + _random.Next(itemCount));
		var tomorrow = DateTime.Today.AddDays(1);
		var dueDate = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, 9 + _random.Next(12), 30 * _random.Next(2), 0);
		var hours = 1 + _random.Next(4);

		return new ServiceModel(description, orderNumber.ToString(), itemNumber, itemCount, dueDate, hours);
	}

	/// <summary>
	/// Create a scheduled downtime model.
	/// </summary>
	private ScheduledDowntimeModel CreateScheduledDowntimeModel() {
		var hours = 1 + _random.Next(3);
		return new ScheduledDowntimeModel(hours);
	}

	/// <summary>
	/// Returns the next employee name.
	/// </summary>
	/// <returns>The next employee name.</returns>
	private string GetNextEmployeeName() {
		var lastNames = new string[] {
			"Smith", "Brown", "Johnson", "Jones", "Williams", "Davis", "Miller", "Wilson", "Taylor", "Clark", "Moore",
			"Thompson", "Allen", "Martin", "Hall", "Adams", "Baker", "Wright", "Anderson", "Lewis", "Harris", "Hill", "King"
		};

		var firstNames = new string[] {
			"Aiden", "Jack", "Ethan", "Jacob", "Caleb", "Ryan", "Connor", "Michael", "John", "Bob", "Evan", "Luke", "Tyler", "Lucy", "Anne",
			"Carter", "Daniel", "Andrew", "William", "James", "Matthew", "Nathan", "Benjamin", "Max", "Issac", "Zachary", "David", "Mary"
		};

		return string.Format("{0} {1} (#{2})", firstNames[_random.Next(firstNames.Length)], lastNames[_random.Next(lastNames.Length)], _nextEmployeeNumber++);
	}

	/// <summary>
	/// Initializes the models.
	/// </summary>
	private void InitializeModels() {
		var model = new ShopModel();
		var count = (int)(3 + _random.NextDouble() * 4);
		for (var index = 0; index < count; index++)
			model.Employees.Add(CreateEmployeeModel());

		DataContext = model;
	}

	private void OnAddEmployeeButtonClick(object sender, RoutedEventArgs e) {
		// Create a new employee model and insert it at a random location
		var model = CreateEmployeeModel();
		var index = (int)(new Random().NextDouble() * (Shop.Employees.Count - 1));
		Shop.Employees.Insert(index, model);

		// Ensure the new column is visible
		var column = taskBoard.ItemContainerGenerator.ContainerFromItem(model) as TaskColumn;
		column?.BringIntoView();

		SetStatusMessage(string.Format("Employee '{0}' added", model.Name));
	}

	private void OnAddServiceButtonClick(object sender, RoutedEventArgs e) {
		// Choose a random employee
		var index = (int)(new Random().NextDouble() * (Shop.Employees.Count - 1));
		var employee = Shop.Employees[index];

		// Create a new service model and insert it at a random location
		var model = CreateServiceModel();
		index = (int)(new Random().NextDouble() * (employee.Tasks.Count - 1));
		employee.Tasks.Insert(index, model);

		// Ensure the new card is visible
		var column = taskBoard.ItemContainerGenerator.ContainerFromItem(employee) as TaskColumn;
		if (column is not null) {
			column.BringIntoView();
			column.UpdateLayout();
			var card = column.ItemContainerGenerator.ContainerFromItem(model) as TaskCard;
			card?.BringIntoView();
		}

		SetStatusMessage(string.Format("Task '{0}' added", model.Name));
	}

	/// <summary>
	/// Occurs before a card is starting to be dragged.
	/// </summary>
	private void OnTaskBoardCardDragging(object sender, TaskCardEventArgs e) {
		var task = (TaskModelBase)e.Card.Content;
		var employee = (EmployeeModel)e.SourceColumn.DataContext;

		if (task is ScheduledDowntimeModel) {
			e.Cancel = true;
			SetStatusMessage(string.Format("Task '{0}' dragging not permitted", task.Name));
		}
		else
			SetStatusMessage(string.Format("Task '{0}' dragging from employee '{1}' index {2}...", task.Name, employee.Name, e.SourceIndex));
	}

	/// <summary>
	/// Occurs when a card is dragged over another column or card.
	/// </summary>
	private void OnTaskBoardCardDragOver(object sender, TaskCardEventArgs e) {
		var task = (TaskModelBase)e.Card.Content;
		var sourceEmployee = (EmployeeModel)e.SourceColumn.DataContext;
		var targetEmployee = (EmployeeModel)e.TargetColumn.DataContext;

		SetStatusMessage(string.Format("Task '{0}' dragging from employee '{1}' index {2} over employee '{3}' index {4}", task.Name, sourceEmployee.Name, e.SourceIndex, targetEmployee.Name, e.TargetIndex));
	}

	/// <summary>
	/// Occurs after a card is dropped at a new location.
	/// </summary>
	private void OnTaskBoardCardDropped(object sender, TaskCardEventArgs e) {
		var task = (TaskModelBase)e.Card.Content;
		var sourceEmployee = (EmployeeModel)e.SourceColumn.DataContext;
		var targetEmployee = (EmployeeModel)e.TargetColumn.DataContext;

		if ((e.SourceColumn == e.TargetColumn) && (e.SourceIndex == e.TargetIndex))
			SetStatusMessage(string.Format("Task '{0}' not moved", task.Name));
		else
			SetStatusMessage(string.Format("Task '{0}' moved from employee '{1}' index {2} to employee '{3}' index {4}", task.Name, sourceEmployee.Name, e.SourceIndex, targetEmployee.Name, e.TargetIndex));
	}

	/// <summary>
	/// Occurs when a card is tapped.
	/// </summary>
	private void OnTaskBoardCardTapped(object sender, TaskCardEventArgs e) {
		var task = (TaskModelBase)e.Card.Content;
		SetStatusMessage(string.Format("Task '{0}' tapped", task.Name));
	}

	/// <summary>
	/// Occurs before a column is starting to be dragged.
	/// </summary>
	private void OnTaskBoardColumnDragging(object sender, TaskColumnEventArgs e) {
		var employee = (EmployeeModel)e.Column.DataContext;
		SetStatusMessage(string.Format("Employee '{0}' dragging from index {1}...", employee.Name, e.SourceIndex));
	}

	/// <summary>
	/// Occurs before a column is dragged over a column.
	/// </summary>
	private void OnTaskBoardColumnDragOver(object sender, TaskColumnEventArgs e) {
		var employee = (EmployeeModel)e.Column.DataContext;
		SetStatusMessage(string.Format("Employee '{0}' dragging from index {1} over index {2}...", employee.Name, e.SourceIndex, e.TargetIndex));
	}

	/// <summary>
	/// Occurs after a column is dropped at a new location.
	/// </summary>
	private void OnTaskBoardColumnDropped(object sender, TaskColumnEventArgs e) {
		var employee = (EmployeeModel)e.Column.DataContext;
		if (e.SourceIndex == e.TargetIndex)
			SetStatusMessage(string.Format("Employee '{0}' not moved", employee.Name));
		else
			SetStatusMessage(string.Format("Employee '{0}' moved from index {1} to index {2}", employee.Name, e.SourceIndex, e.TargetIndex));
	}

	/// <summary>
	/// Sets the status message.
	/// </summary>
	/// <param name="text">The message text.</param>
	private void SetStatusMessage(string text)
		=> statusTextBlock.Text = text;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The repair shop model.
	/// </summary>
	public ShopModel Shop
		=> (ShopModel)DataContext;

}
