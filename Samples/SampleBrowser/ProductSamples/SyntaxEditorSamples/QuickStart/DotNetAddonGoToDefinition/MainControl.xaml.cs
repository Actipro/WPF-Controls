using ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Languages.CSharp.Implementation;
using ActiproSoftware.Text.Languages.DotNet;
using ActiproSoftware.Text.Languages.DotNet.Reflection;
using ActiproSoftware.Text.Languages.DotNet.Resolution;
using ActiproSoftware.Text.Languages.DotNet.Resolution.Implementation;
using ActiproSoftware.Text.Lexing;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

#if WPF
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;
#endif

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.DotNetAddonGoToDefinition {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl : System.Windows.Controls.UserControl {

		// A project assembly (similar to a Visual Studio project) contains source files and assembly references for reflection
		private readonly GoToDefinitionService	goToDefinitionService;
		private readonly IProjectAssembly		projectAssembly;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			// Initialize the project assembly (enables support for automated IntelliPrompt features)
			projectAssembly = new CSharpProjectAssembly("SampleBrowser");
			var assemblyLoader = new BackgroundWorker();
			assemblyLoader.DoWork += DotNetProjectAssemblyReferenceLoader;
			assemblyLoader.RunWorkerAsync();

			// Load the .NET Languages Add-on C# language and register the project assembly on it
			var language = new CSharpSyntaxLanguage();
			language.RegisterProjectAssembly(projectAssembly);

			// Initialize the GoToDefinitionService
			goToDefinitionService = new GoToDefinitionService();
			goToDefinitionService.OpenEditors.Add(codeEditor);
			selectDefinitionCheckBox.IsChecked = goToDefinitionService.IsWordSelectedOnNavigation;

			// Register the GoToDefinitionService on the language so it can be resolved by other types
			language.RegisterService(goToDefinitionService);

			// Register the GoToDefinitionTagger on the language
			language.RegisterService(new TextViewTaggerProvider<GoToDefinitionTagger>(typeof(GoToDefinitionTagger)));

			// Configure this editor to use the language
			codeEditor.Document.Language = language;
		}

		private void DotNetProjectAssemblyReferenceLoader(object sender, DoWorkEventArgs e) {
			// Add some common assemblies for reflection (any custom assemblies could be added using various Add overloads instead)
			SyntaxEditorHelper.AddCommonDotNetSystemAssemblyReferences(projectAssembly);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnGoToDefinitionButtonClick(object sender, RoutedEventArgs e) {
			// Resolve the item under the caret
			var resultSet = PerformResolution(codeEditor.ActiveView.Selection.EndSnapshotOffset);

			// Use the GoToDefinition service to navigate to the item
			if (resultSet.Results.Count > 0) {
				var result = resultSet.Results[0];
				var goToDefinitionService = codeEditor.Document.Language.GetService<GoToDefinitionService>();
				goToDefinitionService.NavigateToDefinition(result);
			}

			// Restore focus to the SyntaxEditor since the "Go To Definition" button in this sample will steal focus when clicked
			codeEditor.ActiveView.Focus(Windows.FocusState.Programmatic);
		}

		/// <summary>
		/// Occurs when the checked state changes.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnSelectDefinitionCheckBoxIsCheckedChanged(object sender, RoutedEventArgs e) {
			if (goToDefinitionService != null)
				goToDefinitionService.IsWordSelectedOnNavigation = (selectDefinitionCheckBox.IsChecked == true);
		}

		/// <summary>
		/// Occurs when the mouse moves over the control.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="MouseEventArgs"/> that contains the event data.</param>
		private void OnSyntaxEditorMouseMove(object sender, MouseEventArgs e) {
			// When moving the mouse, resolve the item under the pointer
			var result = codeEditor.HitTest(e.GetPosition(codeEditor));
			if (result.Type == HitTestResultType.ViewTextAreaOverCharacter) {
				var snapshotOffset = new TextSnapshotOffset(result.Snapshot, result.Offset);
				PerformResolution(snapshotOffset);
			}
		}

		/// <summary>
		/// Occurs when the mouse leaves the control.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="MouseEventArgs"/> that contains the event data.</param>
		private void OnSyntaxEditorMouseLeave(object sender, MouseEventArgs e) {
			// When the mouse leaves, resolve the item under the caret position
			PerformResolution(codeEditor.ActiveView.Selection.EndSnapshotOffset);
		}

		/// <summary>
		/// Occurs when the selection changes within the view.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="EditorViewSelectionEventArgs"/> that contains the event data.</param>
		private void OnSyntaxEditorViewSelectionChanged(object sender, EditorViewSelectionEventArgs e) {
			// When selection changes, resolve the item under the caret
			PerformResolution(e.View.Selection.EndSnapshotOffset);
		}

		/// <summary>
		/// Performs a resolve request at the given offset and outputs the results.
		/// </summary>
		/// <param name="snapshotOffset">The snapshot offset.</param>
		/// <returns>An <see cref="IResolverResultSet"/> if available; otherwise <c>null</c>.</returns>
		private IResolverResultSet PerformResolution(TextSnapshotOffset snapshotOffset) {
			var resultSet = codeEditor.Document.Language.GetService<GoToDefinitionService>().PerformResolution(snapshotOffset);
			UpdateResolverResults(resultSet);
			return resultSet;
		}

		/// <summary>
		/// Updates the resolver results.
		/// </summary>
		/// <param name="resultSet">The result set.</param>
		private void UpdateResolverResults(IResolverResultSet resultSet) {
			var text = new StringBuilder();

			if (resultSet != null) {
				for (var resultIndex = 0; resultIndex < resultSet.Results.Count; resultIndex++) {

					// Get the result from the set
					var result = resultSet.Results[resultIndex];
					text.AppendLine($"Result[{resultIndex}] :: {result.GetType().Name}");

					// NOTE: These are just some of the details available from an IResolverResult shown for example

					// Output common result data
					text.AppendLine($"IResolverResult");
					text.AppendLine($"    Kind:    {result.Kind}");
					text.AppendLine($"    Name:    {result.Name}");
					text.AppendLine($"    TypeIsInstance:    {result.TypeIsInstance}");
					if (result.Type != null) {
						text.AppendLine($"    Type");
						text.AppendLine($"        QualifiedName:    {result.Type.QualifiedName}");
						text.AppendLine($"        AssemblyName:    {result.Type.AssemblyName}");
						text.AppendLine($"        IsGenericType:    {result.Type.IsGenericType}");
						text.AppendLine($"        IsTypeDefinition:    {result.Type.IsTypeDefinition}");
						text.AppendLine($"        IsTypeParameter:    {result.Type.IsTypeParameter}");
						if (result.Type is ITypeDefinition typeDefinition) {
							for (var sourceIndex = 0; sourceIndex < typeDefinition.SourceFileLocations.Count; sourceIndex++)
								text.AppendLine($"        SourceFileLocation[{sourceIndex}]:    {typeDefinition.SourceFileLocations[sourceIndex]}");
						}
					}

					// Output type-specific result data
					if (result is INamespaceResolverResult namespaceResult) {
						text.AppendLine($"INamespaceResolverResult");
						text.AppendLine($"    FullName:    {namespaceResult.FullName}");
					}
					if (result is ITypeResolverResult typeResult) {
						text.AppendLine($"ITypeResolverResult");
						text.AppendLine($"    TypeIsBaseTypeReference:    {typeResult.TypeIsBaseTypeReference}");
					}
					if (result is ITypeMemberResolverResult typeMemberResult) {
						text.AppendLine($"ITypeMemberResolverResult.Member");
						text.AppendLine($"    Kind:    {typeMemberResult.Member.Kind}");
						text.AppendLine($"    Name:    {typeMemberResult.Member.Name}");
						text.AppendLine($"    Access:    {typeMemberResult.Member.Access}");
						text.AppendLine($"    ReturnType:    {typeMemberResult.Member.ReturnType}");
						text.AppendLine($"    SourceFileLocation:    {typeMemberResult.Member.SourceFileLocation}");
					}
					if (result is IParameterResolverResult parameterResult) {
						text.AppendLine($"IParameterResolverResult.Parameter");
						text.AppendLine($"    Name:    {parameterResult.Parameter.Name}");
						text.AppendLine($"    IsOptional:    {parameterResult.Parameter.IsOptional}");
						text.AppendLine($"    Type:    {parameterResult.Parameter.Type}");
						text.AppendLine($"    SourceFileLocation:    {parameterResult.Parameter.SourceFileLocation}");
					}
					if (result is IVariableResolverResult variableResult) {
						text.AppendLine($"IVariableResolverResult.Variable");
						text.AppendLine($"    Name:    {variableResult.Variable.Name}");
						text.AppendLine($"    Type:    {variableResult.Variable.Type}");
						text.AppendLine($"    SourceFileLocation:    {variableResult.Variable.SourceFileLocation}");
					}
					text.AppendLine(string.Empty);
				}
			}
			if (text.Length == 0)
				text.AppendLine("Resolution not available");

			resultsTextBox.Text = text.ToString();
		}

	}

}