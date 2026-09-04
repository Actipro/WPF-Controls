using ActiproSoftware.Text.Languages.DotNet;
using ActiproSoftware.Text.Languages.DotNet.Reflection;
using ActiproSoftware.Text.Languages.DotNet.Reflection.Implementation;
using ActiproSoftware.Text.Languages.VB.Implementation;
using ActiproSoftware.Text.Parsing;
using ActiproSoftware.Text.Parsing.Implementation;
using System.Activities;
using System.Activities.Presentation.Model;

namespace ActiproSoftware.Windows.WindowsWorkflowIntegration.ExpressionEditing;

/// <summary>
/// Represents a <c>Visual Basic</c> syntax language for use in an expression editor.
/// </summary>
public class VBExpressionEditorSyntaxLanguage : VBSyntaxLanguage, IExpressionEditorSyntaxLanguage {

	// A project assembly (similar to a Visual Studio project) contains source files and assembly references for reflection
	private readonly IProjectAssembly _projectAssembly;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the class.
	/// </summary>
	static VBExpressionEditorSyntaxLanguage() {
		// Ensure that worker threads are used to perform the parsing
		if (AmbientParseRequestDispatcherProvider.Dispatcher is null)
			AmbientParseRequestDispatcherProvider.Dispatcher = new ThreadedParseRequestDispatcher();

		// Create a SyntaxEditor .NET Languages Add-on ambient assembly repository as needed, which supports caching of 
		//   reflection data and improves performance for the add-on...
		if (AmbientAssemblyRepositoryProvider.Repository is null) {
			
			//
			// NOTE: Be sure to replace the appDataPath with a proper path for your own
			//   application (if file access is allowed)
			//
			
			var appDataPath = Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
				"Actipro Software",
				"Expression Editor Assembly Repository"
			);

			AmbientAssemblyRepositoryProvider.Repository = new FileBasedAssemblyRepository(appDataPath);
		}
	}

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public VBExpressionEditorSyntaxLanguage() {

		//
		// NOTE: Make sure that you've read through the add-on language's 'Getting Started' topic
		//   since it tells you how to set up an ambient parse request dispatcher and an ambient
		//   code repository within your application OnStartup code, and add related cleanup in your
		//   application OnExit code.  These steps are essential to having the add-on perform well.
		//

		// Initialize the project assembly (enables support for automated IntelliPrompt features)
		_projectAssembly = new VBProjectAssembly("ExpressionEditor");
		var assemblyLoader = new BackgroundWorker();
		assemblyLoader.DoWork += OnProjectAssemblyReferenceLoaderWork;
		assemblyLoader.RunWorkerAsync();

		// Load the .NET Languages Add-on VB language and register the project assembly on it
		this.RegisterProjectAssembly(_projectAssembly);
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Appends the specified type's full name to a <see cref="StringBuilder"/>.
	/// </summary>
	/// <param name="typeName">The type name <see cref="StringBuilder"/> to update.</param>
	/// <param name="type">The <see cref="Type"/> to examine.</param>
	private static void AppendTypeName(StringBuilder typeName, Type type) {
		var typeFullName = type.FullName;

		if (type.IsGenericType) {
			var tickIndex = typeFullName.IndexOf('`');
			if (tickIndex != -1) {
				typeName.Append(typeFullName.Substring(0, tickIndex));
				typeName.Append("(Of ");
				var genericArgumentIndex = 0;
				foreach (var genericArgument in type.GetGenericArguments()) {
					if (genericArgumentIndex++ > 0)
						typeName.Append(", ");

					AppendTypeName(typeName, genericArgument);
				}
				typeName.Append(")");
				return;
			}
		}

		typeName.Append(typeFullName);
	}

	/// <summary>
	/// Occurs when the project assembly loads assembly references.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnProjectAssemblyReferenceLoaderWork(object sender, DoWorkEventArgs e) {
		//
		// NOTE: Automated IntelliPrompt will only be available on types/members in the
		//   referenced assemblies, so add other assembly references if types/members from
		//   other assemblies are used in your workflow
		//

		// Add some common assemblies for reflection (any custom assemblies could be added using various Add overloads instead)...
		_projectAssembly.AssemblyReferences.AddMsCorLib();
		_projectAssembly.AssemblyReferences.Add("System");
		_projectAssembly.AssemblyReferences.Add("System.Core");
		_projectAssembly.AssemblyReferences.Add("System.Xml");
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="IExpressionEditorSyntaxLanguage.GetHeaderText"/>
	public string GetHeaderText(IEnumerable<ModelItem> variableModels) {
		// Inject namespace imports
		var headerText = new StringBuilder();
		headerText.AppendLine(@"Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq");

		//
		// NOTE: Automated IntelliPrompt will only show for namespaces and types that are
		//   within the imported namespaces.  Add other namespace imports here if types from
		//   other namespaces should be accessible.
		//

		// Inject a Class and Sub wrapper
		headerText.AppendLine();
		headerText.AppendLine(@"Shared Class Expression
Shared Sub ExpressionValue");

		// Append variable declarations so they appear in IntelliPrompt
		if (variableModels is not null) {
			foreach (var variableModel in variableModels) {
				if (variableModel?.GetCurrentValue() is LocationReference variable) {
					// Build a VB representation of the variable's type name
					var variableTypeName = new StringBuilder();
					AppendTypeName(variableTypeName, variable.Type);

					headerText.Append("Dim ");
					headerText.Append(variable.Name);
					headerText.Append(" As ");
					headerText.Append(variableTypeName.Replace("[", "(").Replace("]", ")"));
					headerText.AppendLine();
				}
			}
		}

		// Since the document text is an expression, inject a Return statement start at the end of the header text
		headerText.AppendLine();
		headerText.Append("Return ");

		return headerText.ToString();
	}

	/// <inheritdoc cref="IExpressionEditorSyntaxLanguage.GetFooterText"/>
	public string GetFooterText() {
		// Close out the Sub and Class in the footer
		return "\r\nEnd Sub\r\nEnd Class";
	}

}
