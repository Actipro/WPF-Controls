/*

RIBBON GETTING STARTED SERIES - STEP 7

STEP SUMMARY:

	This C# file is unchanged since the last step.

CHANGES SINCE LAST STEP:

	None.

*/

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.GettingStarted.Step07;

/// <summary>
/// Provides the main window for this sample.
/// </summary>
public partial class MainWindow {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainWindow() {
		InitializeComponent();

		// Add command bindings
		CommandBindings.Add(new CommandBinding(ApplicationCommands.Help, ExecuteHelpCommand));

		// Configure this view with the new view model
		ViewModel = new SampleWindowViewModel();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

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
	/// The view model for this view.
	/// </summary>
	private SampleWindowViewModel? ViewModel {
		get => DataContext as SampleWindowViewModel;
		set => DataContext = value;
	}

}
