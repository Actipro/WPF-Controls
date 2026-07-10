namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.TaskPlanning;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

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
	/// Initializes the models.
	/// </summary>
	private void InitializeModels() {
		var model = new TaskBoardModel();

		model.Lists.Add(new TaskListModel("Planned") {
			Tasks = {
				new TaskModel("Check out TaskBoard's customization features", TaskModel.LabelBlueColor),
				new TaskModel("See how easily columns and cards can be dragged and dropped", TaskModel.LabelBlueColor),
				new TaskModel("Implement a task board in my own app with Actipro's TaskBoard control", TaskModel.LabelGreenColor),
				new TaskModel("Make my customers happy with great UI functionality", TaskModel.LabelGreenColor)
			}
		});

		model.Lists.Add(new TaskListModel("Completed") {
			Tasks = {
				new TaskModel("Evaluate Actipro's UI control products", TaskModel.LabelRedColor)
			}
		});

		DataContext = model;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The task board model.
	/// </summary>
	public TaskBoardModel Board
		=> (TaskBoardModel)DataContext;

	/// <inheritdoc/>
	protected override void OnTextInput(TextCompositionEventArgs e) {
		base.OnTextInput(e);

		if (!e.Handled) {
			switch (e.Text.ToUpperInvariant()) {
				case "X": {
					// Delete the card under the pointer
					var column = taskBoard.HitTestForColumn(Mouse.GetPosition(taskBoard));
					var card = column?.HitTestForCard(Mouse.GetPosition(column));
					var task = card?.Content as TaskModel;
					task?.DeleteTaskCommand.Execute(parameter: null);
					break;
				}
			}
		}
	}

}
