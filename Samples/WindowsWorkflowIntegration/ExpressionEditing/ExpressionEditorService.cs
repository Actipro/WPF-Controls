using System.Activities.Presentation;
using System.Activities.Presentation.View;
using System.Activities.Presentation.Hosting;
using System.Activities.Presentation.Model;
using ActiproSoftware.Text;

namespace ActiproSoftware.Windows.WindowsWorkflowIntegration.ExpressionEditing;

/// <summary>
/// Implements an <see cref="IExpressionEditorService"/> that uses SyntaxEditor.
/// </summary>
public class ExpressionEditorService : IExpressionEditorService {

	private readonly WorkflowDesigner _designer;
	private readonly List<IExpressionEditorInstance> _editors = [];
	private static readonly ISyntaxLanguage _language = new VBExpressionEditorSyntaxLanguage();

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the class.
	/// </summary>
	static ExpressionEditorService() {
		AddCustomResourcesToApplication();
	}

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="designer">The <see cref="WorkflowDesigner"/> that owns the service.</param>
	public ExpressionEditorService(WorkflowDesigner designer) {
		_designer = designer ?? throw new ArgumentNullException(nameof(designer));
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Merges the <c>Resources.xaml</c> file into the application resources to work around a workflow designer focus issue with the completion list.
	/// </summary>
	private static void AddCustomResourcesToApplication() {
		if (Application.Current?.Resources?.MergedDictionaries is { } mergedDictionaries) {
			var customResourceDictionary = new ResourceDictionary {
				Source = ResourceHelper.GetLocationUri(typeof(ExpressionEditorService).Assembly, "ExpressionEditing/Resources.xaml")
			};
			mergedDictionaries.Add(customResourceDictionary);
		}
	}

	/// <summary>
	/// Occurs when the editor loses focus.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnEditorLostFocus(object sender, EventArgs e) {
		if (sender is IExpressionEditorInstance editor)
			DesignerView.CommitCommand.Execute(editor.Text);
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="IExpressionEditorService.CloseExpressionEditors"/>
	public void CloseExpressionEditors() {
		foreach (var editor in _editors)
			editor.LostAggregateFocus -= OnEditorLostFocus;
		_editors.Clear();
	}

	/// <inheritdoc cref="IExpressionEditorService.CreateExpressionEditor(AssemblyContextControlItem, ImportedNamespaceContextItem, List{ModelItem}, string)"/>
	public IExpressionEditorInstance CreateExpressionEditor(AssemblyContextControlItem assemblies, ImportedNamespaceContextItem importedNamespaces, List<ModelItem> variables, string text) {
		var editor = new MyExpressionEditorInstance(_designer, variables, _language) {
			Text = text
		};
		editor.LostAggregateFocus += OnEditorLostFocus;
		_editors.Add(editor);
		return editor;
	}

	/// <inheritdoc cref="IExpressionEditorService.CreateExpressionEditor(AssemblyContextControlItem, ImportedNamespaceContextItem, List{ModelItem}, string, Size)"/>
	public IExpressionEditorInstance CreateExpressionEditor(AssemblyContextControlItem assemblies, ImportedNamespaceContextItem importedNamespaces, List<ModelItem> variables, string text, Size initialSize)
		=> CreateExpressionEditor(assemblies, importedNamespaces, variables, text);

	/// <inheritdoc cref="IExpressionEditorService.CreateExpressionEditor(AssemblyContextControlItem, ImportedNamespaceContextItem, List{ModelItem}, string, Type)"/>
	public IExpressionEditorInstance CreateExpressionEditor(AssemblyContextControlItem assemblies, ImportedNamespaceContextItem importedNamespaces, List<ModelItem> variables, string text, Type expressionType)
		=> CreateExpressionEditor(assemblies, importedNamespaces, variables, text);

	/// <inheritdoc cref="IExpressionEditorService.CreateExpressionEditor(AssemblyContextControlItem, ImportedNamespaceContextItem, List{ModelItem}, string, Type, Size)"/>
	public IExpressionEditorInstance CreateExpressionEditor(AssemblyContextControlItem assemblies, ImportedNamespaceContextItem importedNamespaces, List<ModelItem> variables, string text, Type expressionType, Size initialSize)
		=> CreateExpressionEditor(assemblies, importedNamespaces, variables, text);

	/// <inheritdoc cref="IExpressionEditorService.UpdateContext"/>
	public void UpdateContext(AssemblyContextControlItem assemblies, ImportedNamespaceContextItem importedNamespaces) { /* no-op */ }

}
