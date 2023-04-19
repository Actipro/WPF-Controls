/*

RIBBON GETTING STARTED SERIES - STEP 6

STEP SUMMARY:

	This C# file is unchanged since the last step.

CHANGES SINCE LAST STEP:

	None.

*/

using System.Windows.Input;


namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.GettingStarted.Step06 {

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

			// Configure this view with the new view model
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
			// Associate the CommandBinding with ViewModel.HelpCommand
			ViewModel?.HelpCommand?.Execute(e.Parameter);
		}

		/// <summary>
		/// Gets or sets the view model for this view.
		/// </summary>
		/// <value>A <see cref="SampleWindowViewModel"/>.</value>
		private SampleWindowViewModel ViewModel {
			get => this.DataContext as SampleWindowViewModel;
			set => this.DataContext = value;
		}

	}

}
