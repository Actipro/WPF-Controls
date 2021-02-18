using ActiproSoftware.Windows.Controls.Docking;
using ActiproSoftware.Windows.WindowsWorkflowIntegration.ExpressionEditing;
using System.Activities.Core.Presentation;
using System.Activities.Presentation;
using System.Activities.Presentation.Toolbox;
using System.Activities.Presentation.View;
using System.Activities.Statements;
using System.Windows;

namespace ActiproSoftware.Windows.WindowsWorkflowIntegration {

	/// <summary>
	/// Represents the main window.
	/// </summary>
	public partial class MainWindow : Window {

		private WorkflowDesigner designer;
		private ExpressionEditorService expressionEditorService;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>MainWindow</c> class.
		/// </summary>
		public MainWindow() {
			InitializeComponent();

			// Initialize the designer
			this.RegisterDesignerMetadata();
			this.CreateDesigner();

			// Set the tool window content
			toolboxToolWindow.Content = CreateToolboxControl();
			propertiesToolWindow.Content = designer.PropertyInspectorView;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates the designer.
		/// </summary>
		private void CreateDesigner() {
			// Create an instance of WorkflowDesigner class.
			designer = new WorkflowDesigner();

			// Load a Sequence as a default
			var root = new Sequence() {
				Activities = {
					new Assign(),
					new WriteLine()
				}
			};
			designer.Load(root);

			// Create an expression editor service
			expressionEditorService = new ExpressionEditorService(designer);
			designer.Context.Services.Publish<IExpressionEditorService>(expressionEditorService);

			// Add to a document window
			var documentWindow = new DocumentWindow(dockSite, "Designer1", "Designer1", null, designer.View);
			documentWindow.CanClose = false;
			documentWindow.Activate();
		}

		/// <summary>
		/// Creates a toolbox control.
		/// </summary>
		/// <returns>The control that was created.</returns>
		private ToolboxControl CreateToolboxControl() {
			// Create the toolbox control
			var toolbox = new ToolboxControl();
			toolbox.BorderThickness = new Thickness(0);

			// Create toolbox items
			var assignItem = new ToolboxItemWrapper("System.Activities.Statements.Assign", typeof(Assign).Assembly.FullName, null, "Assign");
			var sequenceItem = new ToolboxItemWrapper("System.Activities.Statements.Sequence", typeof(Sequence).Assembly.FullName, null, "Sequence");

			// Add the items to the toolbox in a category
			var category = new ToolboxCategory("category1");
			category.Add(assignItem);
			category.Add(sequenceItem);
			toolbox.Categories.Add(category);

			return toolbox;
		}

		/// <summary>
		/// Occurs when the menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>RoutedEventArgs</c> that contains data related to the event.</param>
		private void OnExitMenuItemClick(object sender, RoutedEventArgs e) {
			this.Close();
		}

		/// <summary>
		/// Registers the designer metadata.
		/// </summary>
		private void RegisterDesignerMetadata() {
			var dm = new DesignerMetadata();
			dm.Register();
		}

	}

}
