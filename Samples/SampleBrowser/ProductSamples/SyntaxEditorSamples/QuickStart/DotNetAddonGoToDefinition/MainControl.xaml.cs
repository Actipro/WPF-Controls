using ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Languages.CSharp.Implementation;
using ActiproSoftware.Text.Languages.DotNet;
using ActiproSoftware.Text.Languages.DotNet.Reflection;
using ActiproSoftware.Text.Languages.DotNet.Resolution;
using ActiproSoftware.Text.Languages.DotNet.Resolution.Implementation;
using ActiproSoftware.Text.Lexing;
using System.ComponentModel;
using System.Text;
using System.Windows;

#if WPF
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;
#endif

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.DotNetAddonGoToDefinition {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl : System.Windows.Controls.UserControl {

		// A project assembly (similar to a Visual Studio project) contains source files and assembly references for reflection
		private readonly IProjectAssembly projectAssembly;

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
		/// Gets the source file location, if any, represented by the given resolver result.
		/// </summary>
		/// <param name="result">The result to examine.</param>
		/// <returns>An <see cref="ISourceFileLocation"/> if available; otherwise <c>null</c>.</returns>
		private ISourceFileLocation GetSourceFileLocation(IResolverResult result) {
			if ((result?.Type is ITypeDefinition typeDefinition) && (typeDefinition.SourceFileLocations.Count > 0))
				return typeDefinition.SourceFileLocations[0];

			if (result is IParameterResolverResult parameterResult)
				return parameterResult.Parameter.SourceFileLocation;

			if (result is ITypeMemberResolverResult typeMemberResult)
				return typeMemberResult.Member.SourceFileLocation;

			if (result is IVariableResolverResult variableResult)
				return variableResult.Variable.SourceFileLocation;

			return null;
		}

		/// <summary>
		/// Navigates to the given location.
		/// </summary>
		/// <param name="sourceFileLocation">The source file location.</param>
		/// <param name="selectWord">When <c>true</c>, the word at the source file location will be selected (e.g. the identifier name).</param>
		private void GoToSourceFileLocation(ISourceFileLocation sourceFileLocation, bool selectWord) {
			if (sourceFileLocation != null) {
				// Is the location within the current document?
				if (sourceFileLocation.Key == codeEditor.Document.UniqueId.ToString()) {
					// Determine the location of the definition
					int navigationOffset = -1;
					if (sourceFileLocation.NavigationOffset.HasValue)
						navigationOffset = sourceFileLocation.NavigationOffset.Value;
					else if (!sourceFileLocation.TextRange.IsDeleted)
						navigationOffset = sourceFileLocation.TextRange.StartOffset;

					// Move to the definition
					if ((0 <= navigationOffset) && (navigationOffset < codeEditor.Document.CurrentSnapshot.Length)) {
						// Should the definition be selected?
						if (selectWord) {
							// Find the full range of the "word" at the navigation offset and select it
							var wordTextRange = codeEditor.Document.CurrentSnapshot.GetWordTextRange(navigationOffset);
							if (!wordTextRange.IsZeroLength) {
								codeEditor.ActiveView.Selection.SelectRange(wordTextRange);
								return;
							}
						}

						// No selection... simply adjust the caret location to the definition
						codeEditor.ActiveView.Selection.CaretOffset = navigationOffset;
						return;
					}
				}
			}
			MessageBox.Show("Cannot navigate to the symbol under the caret.", "Go To Definition", MessageBoxButton.OK, MessageBoxImage.Information);
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnGoToDefinitionButtonClick(object sender, RoutedEventArgs e) {
			// Resolve the item under the caret
			var resultSet = PerformResolution(codeEditor.ActiveView.Selection.EndSnapshotOffset);

			// Determine the source location of the resolver result
			ISourceFileLocation sourceFileLocation = null;
			if ((resultSet != null) && (resultSet.Results.Count > 0))
				sourceFileLocation = GetSourceFileLocation(resultSet.Results[0]);

			// Navigate to the source location
			GoToSourceFileLocation(sourceFileLocation, selectWord: (selectDefinitionCheckBox.IsChecked == true));

			// Restore focus to the SyntaxEditor since the "Go To Definition" button in this sample will steal focus when clicked
			codeEditor.ActiveView.Focus(Windows.FocusState.Programmatic);
		}

		/// <summary>
		/// Occurs when the selection changes within the view.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="Windows.Controls.SyntaxEditor.EditorViewSelectionEventArgs"/> that contains the event data.</param>
		private void OnSyntaxEditorViewSelectionChanged(object sender, Windows.Controls.SyntaxEditor.EditorViewSelectionEventArgs e) {
			PerformResolution(e.View.Selection.EndSnapshotOffset);
		}

		/// <summary>
		/// Performs a resolve request at the given offset and outputs the results.
		/// </summary>
		/// <param name="snapshotOffset">The snapshot offset.</param>
		/// <returns>An <see cref="IResolverResultSet"/> if available; otherwise <c>null</c>.</returns>
		private IResolverResultSet PerformResolution(TextSnapshotOffset snapshotOffset) {
			if (!snapshotOffset.IsDeleted) {
				// Create a context
				var context = new CSharpContextFactory().CreateContext(snapshotOffset);

				if (context.ProjectAssembly != null) {
					// Create a request
					var request = new ResolverRequest(context.TargetExpression) {
						Context = context
					};

					// Resolve
					var resolver = context.ProjectAssembly.Resolver;
					var resultSet = resolver.Resolve(request);

					// Output the results
					UpdateResolverResults(resultSet);

					return resultSet;
				}
			}

			UpdateResolverResults(resultSet: null);
			return null;
		}

		/// <summary>
		/// Updates the resolver results.
		/// </summary>
		/// <param name="resultSet">The result set.</param>
		private void UpdateResolverResults(IResolverResultSet resultSet) {
			StringBuilder text = new StringBuilder();

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