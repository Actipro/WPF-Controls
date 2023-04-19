/*

RIBBON GETTING STARTED SERIES - STEP 2

STEP SUMMARY:

	Create a view model that will be bound to this view and migrate appropriate code behind
	logic to the view model. The view model will be expanded in future steps to support
	additional functionality.

	This step does not introduce new functionality and is only intended as a point of
	migration from the code behind to a view model.

CHANGES SINCE LAST STEP:

	Added SampleWindowViewModel class to be used as the view model for this view.

	Added a ViewModel property that, when set, also assigns the view model to the
	window's DataContext.

	Migrated the logic of 'ExecuteHelpCommand' to an instance of ICommand defined
	on the ViewModel.

	Prior sample comments were removed/condensed.

*/

using System.Windows.Input;


namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.GettingStarted.Step02 {

	/// <summary>
	/// Provides the main window for this sample.
	/// </summary>
	public partial class MainWindow {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="MainWindow"/> class.
		/// </summary>
		public MainWindow() {
			InitializeComponent();

			// Add command bindings
			this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Help, ExecuteHelpCommand));

			//	SAMPLE NOTE 2.1:
			//		Configure this view with the new view model
			this.ViewModel = new SampleWindowViewModel();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Executes the <see cref="ApplicationCommands.Help"/> command.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The event data.</param>
		private void ExecuteHelpCommand(object sender, ExecutedRoutedEventArgs e) {
			//	SAMPLE NOTE 2.2:
			//		The logic from this method has been migrated to ViewModel.HelpCommand.

			// Associate the CommandBinding with ViewModel.HelpCommand
			ViewModel?.HelpCommand?.Execute(e.Parameter);
		}

		/// <summary>
		/// Gets or sets the view model for this view.
		/// </summary>
		/// <value>A <see cref="SampleWindowViewModel"/>.</value>
		private SampleWindowViewModel ViewModel {
			//	SAMPLE NOTE 2.3:
			//		The view model is assigned directly to the DataContext of this window to easily support bindings.

			get => this.DataContext as SampleWindowViewModel;
			set => this.DataContext = value;
		}

	}

}
