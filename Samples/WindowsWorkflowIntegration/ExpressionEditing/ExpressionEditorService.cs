using System;
using System.Collections.Generic;
using System.Activities.Presentation;
using System.Activities.Presentation.View;
using System.Activities.Presentation.Hosting;
using System.Activities.Presentation.Model;
using System.Windows;
using ActiproSoftware.Text;

namespace ActiproSoftware.Windows.WindowsWorkflowIntegration.ExpressionEditing {

	/// <summary>
	/// Implements an <see cref="IExpressionEditorService"/> that uses SyntaxEditor.
	/// </summary>
	public class ExpressionEditorService : IExpressionEditorService {

		private WorkflowDesigner designer;
		private List<IExpressionEditorInstance> editors = new List<IExpressionEditorInstance>();
		private static ISyntaxLanguage language = new VBExpressionEditorSyntaxLanguage();
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes the <c>ExpressionEditorService</c> class.
		/// </summary>
		static ExpressionEditorService() {
			AddCustomResourcesToApplication();
		}
		
		/// <summary>
		/// Initializes a new instance of the <c>ExpressionEditorService</c> class.
		/// </summary>
		/// <param name="designer">The <see cref="WorkflowDesigner"/> that owns the service.</param>
		public ExpressionEditorService(WorkflowDesigner designer) {
			this.designer = designer;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Merges the <c>Resources.xaml</c> file into the application resources to work around a workflow designer focus issue with the completion list.
		/// </summary>
		private static void AddCustomResourcesToApplication() {
			if (Application.Current != null) {
				var resources = Application.Current.Resources;
				if ((resources != null) && (resources.MergedDictionaries != null)) {
					var customResourceDictionary = new ResourceDictionary();
					customResourceDictionary.Source = ResourceHelper.GetLocationUri(typeof(ExpressionEditorService).Assembly, "ExpressionEditing/Resources.xaml");
					resources.MergedDictionaries.Add(customResourceDictionary);
				}
			}
		}

		/// <summary>
		/// Occurs when the editor loses focus.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>EventArgs</c> that contains data related to the event.</param>
		private void OnEditorLostFocus(object sender, EventArgs e) {
			var editor = sender as IExpressionEditorInstance;
			if (editor != null)
				DesignerView.CommitCommand.Execute(editor.Text);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Closes all expression editors.
		/// </summary>
		public void CloseExpressionEditors() {
            foreach (var editor in editors)
                editor.LostAggregateFocus -= OnEditorLostFocus;
            editors.Clear();
		}

		/// <summary>
		/// Creates an expression editor instance.
		/// </summary>
		/// <param name="assemblies">The accessible assemblies.</param>
		/// <param name="importedNamespaces">The imported namespaces.</param>
		/// <param name="variables">The variable list.</param>
		/// <param name="text">The text.</param>
		/// <returns>The <see cref="IExpressionEditorInstance"/> that was created.</returns>
		public IExpressionEditorInstance CreateExpressionEditor(AssemblyContextControlItem assemblies, ImportedNamespaceContextItem importedNamespaces, List<ModelItem> variables, string text) {
			var editor = new MyExpressionEditorInstance(designer, variables, language);
			editor.Text = text;
			editor.LostAggregateFocus += OnEditorLostFocus;
			editors.Add(editor);
			return editor;
		}

		/// <summary>
		/// Creates an expression editor instance.
		/// </summary>
		/// <param name="assemblies">The accessible assemblies.</param>
		/// <param name="importedNamespaces">The imported namespaces.</param>
		/// <param name="variables">The variable list.</param>
		/// <param name="text">The text.</param>
		/// <param name="initialSize">The initial size.</param>
		/// <returns>The <see cref="IExpressionEditorInstance"/> that was created.</returns>
		public IExpressionEditorInstance CreateExpressionEditor(AssemblyContextControlItem assemblies, ImportedNamespaceContextItem importedNamespaces, List<ModelItem> variables, string text, Size initialSize) {
			return CreateExpressionEditor(assemblies, importedNamespaces, variables, text);
		}

		/// <summary>
		/// Creates an expression editor instance.
		/// </summary>
		/// <param name="assemblies">The accessible assemblies.</param>
		/// <param name="importedNamespaces">The imported namespaces.</param>
		/// <param name="variables">The variable list.</param>
		/// <param name="text">The text.</param>
		/// <param name="expressionType">The expression type.</param>
		/// <returns>The <see cref="IExpressionEditorInstance"/> that was created.</returns>
		public IExpressionEditorInstance CreateExpressionEditor(AssemblyContextControlItem assemblies, ImportedNamespaceContextItem importedNamespaces, List<ModelItem> variables, string text, Type expressionType) {
			return CreateExpressionEditor(assemblies, importedNamespaces, variables, text);
		}

		/// <summary>
		/// Creates an expression editor instance.
		/// </summary>
		/// <param name="assemblies">The accessible assemblies.</param>
		/// <param name="importedNamespaces">The imported namespaces.</param>
		/// <param name="variables">The variable list.</param>
		/// <param name="text">The text.</param>
		/// <param name="expressionType">The expression type.</param>
		/// <param name="initialSize">The initial size.</param>
		/// <returns>The <see cref="IExpressionEditorInstance"/> that was created.</returns>
		public IExpressionEditorInstance CreateExpressionEditor(AssemblyContextControlItem assemblies, ImportedNamespaceContextItem importedNamespaces, List<ModelItem> variables, string text, Type expressionType, Size initialSize) {
			return CreateExpressionEditor(assemblies, importedNamespaces, variables, text);
		}

		/// <summary>
		/// Updates the context for the editing session.
		/// </summary>
		/// <param name="assemblies">The accessible assemblies.</param>
		/// <param name="importedNamespaces">The imported namespaces.</param>
		public void UpdateContext(AssemblyContextControlItem assemblies, ImportedNamespaceContextItem importedNamespaces) {}

	}

}
