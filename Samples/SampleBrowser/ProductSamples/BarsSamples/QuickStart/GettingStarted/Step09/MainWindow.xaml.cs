/*

RIBBON GETTING STARTED SERIES - STEP 9

STEP SUMMARY:

	Associate the RibbonBackstageViewModel.ContentTemplateSelector with the static resource
	defined in XAML.

	Add support for ApplicationCommands.New command that is used on the Backstage "New" tab.

CHANGES SINCE LAST STEP:

	Updated the configuration of SampleWindowViewModel to include RibbonBackstageViewModel.ContentTemplateSelector.

	Added binding for ApplicationCommands.New Routed Command that, when executed
	will either create a new document for the editor or load a previously defined template.

*/

using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;


namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.GettingStarted.Step09 {

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
			this.CommandBindings.Add(new CommandBinding(ApplicationCommands.New, ExecuteNewCommand));

			// Configure this view with the new view model
			var viewModel = new SampleWindowViewModel();

			// SAMPLE NOTE 9.1:
			//		Each RibbonBackstageTabViewModel configured for the Backstage must have a view to associated with the view model,
			//		so configure the RibbonBackstageViewModel to use the custom DataTemplateSelected configured in the XAML of this sample.
			viewModel.Ribbon.Backstage.ContentTemplateSelector = this.FindResource(SampleResourceKeys.SampleBackstageTabContentTemplateSelector) as DataTemplateSelector;
			this.ViewModel = viewModel;
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
		/// Executes the <see cref="ApplicationCommands.New"/> command.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The event data.</param>
		private void ExecuteNewCommand(object sender, ExecutedRoutedEventArgs e) {
			// Since RichTextBox does not work well with MVVM patterns, the New command is handled by the
			// view instead of adding a corresponding command to the view model. This reduces the complexity
			// of the sample since the focus should be on configuring the Ribbon instead of RichTextBox.

			//	SAMPLE NOTE 9.2:
			//		When a button is clicked that is defined within the Tab content of a Backstage Tab, the
			//		Backstage does not automatically close. This behavior is intentional since some buttons
			//		may intend to leave the backstage open after they are clicked.
			//
			//		Since there is no reason to leave the Backstage open after opening a new document, make
			//		sure the Backstage is closed.

			// Make sure the backstage is closed
			this.ViewModel.Ribbon.Backstage.IsOpen = false;

			// Load the document, using the parameter (if given) as a key for a resource that defines a
			// pre-defined template for a FlowDocument
			var templateKey = e.Parameter as string;
			if (!string.IsNullOrEmpty(templateKey) && this.TryFindResource(templateKey) is FlowDocument flowDocument)
				editor.Document = flowDocument;
			else
				editor.Document = new FlowDocument();

			// Focus the editor
			editor.Focus();
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
