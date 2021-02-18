using System;
using System.Linq;

#if WINRT
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ActiproSoftware.UI.Xaml.Controls.Views;
using ActiproSoftware.UI.Xaml.Extensions;
#else
using System.Windows;
using System.Windows.Controls;
using ActiproSoftware.Windows.Controls.Views;
#endif

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.RepairShopScheduling {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private int			nextEmployeeNumber	= 1;
		private Random		random				= new Random(30);

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			this.InitializeModels();
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Create an employee model.
		/// </summary>
		/// <returns>The model that was created.</returns>
		private EmployeeModel CreateEmployeeModel() {
			var model = new EmployeeModel(this.GetNextEmployeeName());

			switch (random.Next(4)) {
				case 0:
					model.Status = EmployeeStatus.Unavailable;
					break;
				case 1:
					model.Status = EmployeeStatus.Idle;
					break;
				default:
					model.Status = EmployeeStatus.Working;
					break;
			}

			var taskCount = (int)(1 + random.NextDouble() * 10);
			for (var taskIndex = 1; taskIndex <= taskCount; taskIndex++) {
				if ((model.Tasks.Count >= 3) && (!model.Tasks.Any(o => o is ScheduledDowntimeModel)) && (random.Next(5) == 0)) {
					// Add some scheduled downtime
					model.Tasks.Insert(Math.Min(1 + random.Next(4), model.Tasks.Count), this.CreateScheduledDowntimeModel());
				}
				else {
					// Add a regular service
					model.Tasks.Add(this.CreateServiceModel());
				}
			}
			
			return model;
		}

		/// <summary>
		/// Create a service model.
		/// </summary>
		/// <returns>The model that was created.</returns>
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

			var description = descriptions[random.Next(descriptions.Length)];
			var orderNumber = 30000 + random.Next(10000);
			var itemCount = (int)Math.Max(1, (-1 + random.Next(5)));
			var itemNumber = (int)(1 + random.Next(itemCount));
			var tomorrow = DateTime.Today.AddDays(1);
			var dueDate = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, 9 + random.Next(12), 30 * random.Next(2), 0);
			var hours = 1 + random.Next(4);

			return new ServiceModel(description, orderNumber.ToString(), itemNumber, itemCount, dueDate, hours);
		}

		/// <summary>
		/// Create a scheduled downtime model.
		/// </summary>
		/// <returns>The model that was created.</returns>
		private ScheduledDowntimeModel CreateScheduledDowntimeModel() {
			var hours = 1 + random.Next(3);
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

			return String.Format("{0} {1} (#{2})", firstNames[random.Next(firstNames.Length)], lastNames[random.Next(lastNames.Length)], nextEmployeeNumber++);
		}

		/// <summary>
		/// Initializes the models.
		/// </summary>
		private void InitializeModels() {
			var model = new ShopModel();
			var count = (int)(3 + random.NextDouble() * 4);
			for (var index = 0; index < count; index++)
				model.Employees.Add(this.CreateEmployeeModel());
			
			this.DataContext = model;
		}
		
		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="SelectionChangedEventArgs"/> that contains data related to the event.</param>
		private void OnAddEmployeeButtonClick(object sender, RoutedEventArgs e) {
			// Create a new employee model and insert it at a random location
			var model = this.CreateEmployeeModel();
			var index = (int)(new Random().NextDouble() * (this.Shop.Employees.Count - 1));
			this.Shop.Employees.Insert(index, model);
			
			// Ensure the new column is visible
			var column = taskBoard.ItemContainerGenerator.ContainerFromItem(model) as TaskColumn;
			if (column != null)
				column.BringIntoView();
		
			this.SetStatusMessage(String.Format("Employee '{0}' added", model.Name));
		}
		
		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="SelectionChangedEventArgs"/> that contains data related to the event.</param>
		private void OnAddServiceButtonClick(object sender, RoutedEventArgs e) {
			// Choose a random employee
			var index = (int)(new Random().NextDouble() * (this.Shop.Employees.Count - 1));
			var employee = this.Shop.Employees[index];

			// Create a new service model and insert it at a random location
			var model = this.CreateServiceModel();
			index = (int)(new Random().NextDouble() * (employee.Tasks.Count - 1));
			employee.Tasks.Insert(index, model);

			// Ensure the new card is visible
			var column = taskBoard.ItemContainerGenerator.ContainerFromItem(employee) as TaskColumn;
			if (column != null) {
				column.BringIntoView();
				column.UpdateLayout();
				var card = column.ItemContainerGenerator.ContainerFromItem(model) as TaskCard;
				if (card != null)
					card.BringIntoView();
			}

			this.SetStatusMessage(String.Format("Task '{0}' added", model.Name));
		}

		/// <summary>
		/// Occurs before a card is starting to be dragged.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="TaskCardEventArgs"/> that contains data related to the event.</param>
		private void OnTaskBoardCardDragging(object sender, TaskCardEventArgs e) {
			var task = (TaskModelBase)e.Card.Content;
			var employee = (EmployeeModel)e.SourceColumn.DataContext;

			if (task is ScheduledDowntimeModel) {
				e.Cancel = true;
				this.SetStatusMessage(String.Format("Task '{0}' dragging not permitted", task.Name, employee.Name, e.SourceIndex));
			}
			else
				this.SetStatusMessage(String.Format("Task '{0}' dragging from employee '{1}' index {2}...", task.Name, employee.Name, e.SourceIndex));
		}
		
		/// <summary>
		/// Occurs when a card is dragged over another column or card.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="TaskCardEventArgs"/> that contains data related to the event.</param>
		private void OnTaskBoardCardDragOver(object sender, TaskCardEventArgs e) {
			var task = (TaskModelBase)e.Card.Content;
			var sourceEmployee = (EmployeeModel)e.SourceColumn.DataContext;
			var targetEmployee = (EmployeeModel)e.TargetColumn.DataContext;

			this.SetStatusMessage(String.Format("Task '{0}' dragging from employee '{1}' index {2} over employee '{3}' index {4}", task.Name, sourceEmployee.Name, e.SourceIndex, targetEmployee.Name, e.TargetIndex));
		}

		/// <summary>
		/// Occurs after a card is dropped at a new location.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="TaskCardEventArgs"/> that contains data related to the event.</param>
		private void OnTaskBoardCardDropped(object sender, TaskCardEventArgs e) {
			var task = (TaskModelBase)e.Card.Content;
			var sourceEmployee = (EmployeeModel)e.SourceColumn.DataContext;
			var targetEmployee = (EmployeeModel)e.TargetColumn.DataContext;

			if ((e.SourceColumn == e.TargetColumn) && (e.SourceIndex == e.TargetIndex))
				this.SetStatusMessage(String.Format("Task '{0}' not moved", task.Name, e.SourceIndex, e.TargetIndex));
			else
				this.SetStatusMessage(String.Format("Task '{0}' moved from employee '{1}' index {2} to employee '{3}' index {4}", task.Name, sourceEmployee.Name, e.SourceIndex, targetEmployee.Name, e.TargetIndex));
		}
		
		/// <summary>
		/// Occurs when a card is tapped.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="TaskCardEventArgs"/> that contains data related to the event.</param>
		private void OnTaskBoardCardTapped(object sender, TaskCardEventArgs e) {
			var task = (TaskModelBase)e.Card.Content;
			this.SetStatusMessage(String.Format("Task '{0}' tapped", task.Name));
		}

		/// <summary>
		/// Occurs before a column is starting to be dragged.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="TaskColumnEventArgs"/> that contains data related to the event.</param>
		private void OnTaskBoardColumnDragging(object sender, TaskColumnEventArgs e) {
			var employee = (EmployeeModel)e.Column.DataContext;
			this.SetStatusMessage(String.Format("Employee '{0}' dragging from index {1}...", employee.Name, e.SourceIndex));
		}
		
		/// <summary>
		/// Occurs before a column is dragged over a column.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="TaskColumnEventArgs"/> that contains data related to the event.</param>
		private void OnTaskBoardColumnDragOver(object sender, TaskColumnEventArgs e) {
			var employee = (EmployeeModel)e.Column.DataContext;
			this.SetStatusMessage(String.Format("Employee '{0}' dragging from index {1} over index {2}...", employee.Name, e.SourceIndex, e.TargetIndex));
		}

		/// <summary>
		/// Occurs after a column is dropped at a new location.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="TaskColumnEventArgs"/> that contains data related to the event.</param>
		private void OnTaskBoardColumnDropped(object sender, TaskColumnEventArgs e) {
			var employee = (EmployeeModel)e.Column.DataContext;
			if (e.SourceIndex == e.TargetIndex)
				this.SetStatusMessage(String.Format("Employee '{0}' not moved", employee.Name, e.SourceIndex, e.TargetIndex));
			else
				this.SetStatusMessage(String.Format("Employee '{0}' moved from index {1} to index {2}", employee.Name, e.SourceIndex, e.TargetIndex));
		}

		/// <summary>
		/// Sets the status message.
		/// </summary>
		/// <param name="text">The message text.</param>
		private void SetStatusMessage(string text) {
			statusTextBlock.Text = text;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the repair shop model.
		/// </summary>
		/// <value>The repair shop model.</value>
		public ShopModel Shop {
			get {
				return (ShopModel)this.DataContext;
			}
		}

	}

}
