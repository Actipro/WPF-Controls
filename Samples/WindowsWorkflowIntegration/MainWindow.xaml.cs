using ActiproSoftware.Windows.Controls.Docking;
using ActiproSoftware.Windows.WindowsWorkflowIntegration.ExpressionEditing;
using System.Activities.Core.Presentation;
using System.Activities.Presentation;
using System.Activities.Presentation.Toolbox;
using System.Activities.Presentation.View;
using System.Activities.Statements;

namespace ActiproSoftware.Windows.WindowsWorkflowIntegration;

/// <summary>
/// Represents the main window.
/// </summary>
public partial class MainWindow : Window {

	private WorkflowDesigner _designer;
	private ExpressionEditorService? _expressionEditorService;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainWindow() {
		InitializeComponent();

		// Initialize the designer
		RegisterDesignerMetadata();
		CreateDesigner();

		// Set the tool window content
		toolboxToolWindow.Content = CreateToolboxControl();
		propertiesToolWindow.Content = _designer!.PropertyInspectorView;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates the designer.
	/// </summary>
	private void CreateDesigner() {
		// Create an instance of WorkflowDesigner class.
		_designer = new WorkflowDesigner();

		// Load a Sequence as a default
		var root = new Sequence() {
			Activities = {
				new Assign(),
				new WriteLine()
			}
		};
		_designer.Load(root);

		// Create an expression editor service
		_expressionEditorService = new ExpressionEditorService(_designer);
		_designer.Context.Services.Publish<IExpressionEditorService>(_expressionEditorService);

		// Add to a document window
		var documentWindow = new DocumentWindow(dockSite, serializationId: "Designer1", title: "Designer1", content: _designer.View) {
			CanClose = false
		};
		documentWindow.Activate();
	}

	/// <summary>
	/// Creates a toolbox control.
	/// </summary>
	/// <returns>The control that was created.</returns>
	private ToolboxControl CreateToolboxControl() {
		// Create the toolbox control
		var toolbox = new ToolboxControl {
			BorderThickness = new Thickness(0)
		};

		// Create toolbox items
		var assignItem = new ToolboxItemWrapper("System.Activities.Statements.Assign", typeof(Assign).Assembly.FullName, bitmapName: null, "Assign");
		var sequenceItem = new ToolboxItemWrapper("System.Activities.Statements.Sequence", typeof(Sequence).Assembly.FullName, bitmapName: null, "Sequence");

		// Add the items to the toolbox in a category
		var category = new ToolboxCategory("category1") {
			assignItem,
			sequenceItem
		};
		toolbox.Categories.Add(category);

		return toolbox;
	}

	/// <summary>
	/// Occurs when the menu item is clicked.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnExitMenuItemClick(object sender, RoutedEventArgs e)
		=> Close();

	/// <summary>
	/// Registers the designer metadata.
	/// </summary>
	private void RegisterDesignerMetadata()
		=> new DesignerMetadata().Register();

}
