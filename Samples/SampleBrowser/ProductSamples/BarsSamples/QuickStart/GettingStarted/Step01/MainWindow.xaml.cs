/*

RIBBON GETTING STARTED SERIES - STEP 1

STEP SUMMARY:

	Always refer to the notes in MainWindow.xaml for the most detail about the current step.

	Most of the work for this step is completed in the XAML file. At this stage of the
	Getting Started series, the code behind for the RibbonWindow is only responsible for
	defining the methods associated with the CommandBinding	configured to handle the
	RoutedCommand for Help.
	
	Notes similar to these will also be reflected in other C# code files that accompany each step.

CHANGES SINCE LAST STEP:

	This is the first step. In subsequent steps, check here for notes about what changed since the
	previous step.

*/

using ActiproSoftware.Windows.Controls;
using System.Windows;
using System.Windows.Input;


namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.GettingStarted.Step01 {

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

			//	SAMPLE NOTE 1.1:
			//		Configure a CommandBinding so this window can handle the standard RoutedCommand for Help.
			//		For this sample, this command is bound to a standard Button. In later steps of the series,
			//		the command will be bound to controls on the Ribbon. This step makes sure the RoutedCommand
			//		is being properly handled before starting to configure the Ribbon.
			this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Help, ExecuteHelpCommand));
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
			ThemedMessageBox.Show("This is where contextual Help would be displayed.", "Help", MessageBoxButton.OK, MessageBoxImage.Information);
		}

	}

}
