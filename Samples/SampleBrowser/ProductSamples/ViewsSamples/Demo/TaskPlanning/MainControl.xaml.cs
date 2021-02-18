using System.Windows.Input;
using System.Windows.Media;

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.TaskPlanning {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

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
			
			this.DataContext = model;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the task board model.
		/// </summary>
		/// <value>The task board model.</value>
		public TaskBoardModel Board {
			get {
				return (TaskBoardModel)this.DataContext;
			}
		}

		/// <summary>
		/// Occurs when text is input.
		/// </summary>
		/// <param name="e">The <see cref="KeyEventArgs"/> that contains the event data.</param>
		protected override void OnTextInput(TextCompositionEventArgs e) {
			// Call the base method
			base.OnTextInput(e);

			if (!e.Handled) {
				switch (e.Text.ToUpperInvariant()) {
					case "X": { 
						// Delete the card under the pointer
						var column = taskBoard.HitTestForColumn(Mouse.GetPosition(taskBoard));
						if (column != null) {
							var card = column.HitTestForCard(Mouse.GetPosition(column));
							if (card != null) {
								var task = card.Content as TaskModel;
								if (task != null)
									task.DeleteTaskCommand.Execute(null);
							}
						}
						break;
					}
				}
			}
		}

	}

}
