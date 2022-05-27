using ActiproSoftware.Text;
using ActiproSoftware.Text.Languages.CSharp.Implementation;
using ActiproSoftware.Text.Languages.DotNet.Reflection;
using ActiproSoftware.Text.Languages.DotNet.Resolution;
using ActiproSoftware.Text.Languages.DotNet.Resolution.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

#if WPF
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;
#endif

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.DotNetAddonGoToDefinition {

	/// <summary>
	/// Defines a service which can assist with navigation to the definition of an identifier.
	/// </summary>
	public class GoToDefinitionService : IEditorViewPointerInputEventSink, IEditorViewKeyInputEventSink {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		#region IEditorViewKeyInputEventSink Interface Implementation

		/// <inheritdoc/>
		void IEditorViewKeyInputEventSink.NotifyKeyDown(IEditorView view, KeyEventArgs e) {
			// Notify the view's tagger of the key down
			GetTaggerForView(view)?.NotifyKeyDown(e);
		}

		/// <inheritdoc/>
		void IEditorViewKeyInputEventSink.NotifyKeyUp(IEditorView view, KeyEventArgs e) {
			// Notify the view's tagger of the key up
			GetTaggerForView(view)?.NotifyKeyUp(e);
		}

		#endregion IEditorViewKeyInputEventSink Interface Implementation

		#region IEditorViewPointerInputEventSink Interface Implementation

		/// <inheritdoc/>
		void IEditorViewPointerInputEventSink.NotifyPointerEntered(IEditorView view, InputPointerEventArgs e) { /* no-op */ }

		/// <inheritdoc/>
		void IEditorViewPointerInputEventSink.NotifyPointerExited(IEditorView view, InputPointerEventArgs e) {
			// Notify the view's tagger of the pointer exit
			GetTaggerForView(view)?.NotifyPointerExited(e);
		}

		/// <inheritdoc/>
		void IEditorViewPointerInputEventSink.NotifyPointerHovered(IEditorView view, InputPointerEventArgs e) { /* no-op */ }

		/// <inheritdoc/>
		void IEditorViewPointerInputEventSink.NotifyPointerMoved(IEditorView view, InputPointerEventArgs e) {
			// Notify the view's tagger of the pointer move
			GetTaggerForView(view)?.NotifyPointerMoved(e);
		}

		/// <inheritdoc/>
		void IEditorViewPointerInputEventSink.NotifyPointerPressed(IEditorView view, InputPointerButtonEventArgs e) {
			// Notify the view's tagger of the pointer press
			GetTaggerForView(view)?.NotifyPointerPressed(e);
		}

		/// <inheritdoc/>
		void IEditorViewPointerInputEventSink.NotifyPointerReleased(IEditorView view, InputPointerButtonEventArgs e) {
			// Notify the view's tagger of the pointer release
			GetTaggerForView(view)?.NotifyPointerReleased(e);
		}

		/// <inheritdoc/>
		void IEditorViewPointerInputEventSink.NotifyPointerWheel(IEditorView view, InputPointerWheelEventArgs e) { /* no-op */ }

		#endregion IEditorViewPointerInputEventSink Interface Implementation

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the <see cref="SyntaxEditor"/>, if any, that is currently displaying a source file.
		/// </summary>
		/// <param name="sourceFileLocation">The source file location to examine.</param>
		/// <returns>An instance of <see cref="SyntaxEditor"/> if available; otherwise <c>null</c>.</returns>
		private SyntaxEditor GetEditorForSourceFile(ISourceFileLocation sourceFileLocation) {
			// Find the open editor whose document matches the source file
			return OpenEditors.FirstOrDefault(editor => editor.Document.UniqueId.ToString() == sourceFileLocation.Key);
		}

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
		/// Gets the <see cref="GoToDefinitionTagger"/>, if any, that is associated with the given <see cref="IEditorView"/>.
		/// </summary>
		/// <param name="view">The <see cref="IEditorView"/> to examine.</param>
		/// <returns>
		/// The associated <see cref="GoToDefinitionTagger"/> if available; otherwise, <c>null</c>.
		/// </returns>
		private static GoToDefinitionTagger GetTaggerForView(IEditorView view) {
			if (view.Properties.TryGetValue<GoToDefinitionTagger>(typeof(GoToDefinitionTagger), out var tagger))
				return tagger;
			return null;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets if the entire word of an identifier is selected after navigation.
		/// </summary>
		/// <value>
		/// <c>true</c> to select the entire identifier word after navigation.
		/// <c>false</c> to move the caret to the identifier start without selection.
		/// </value>
		public bool IsWordSelectedOnNavigation { get; set; } = true;
		
		/// <summary>
		/// Attempts to navigate to the definition of the identifier at the given offset.
		/// </summary>
		/// <param name="snapshotOffset">The offset to examine.</param>
		/// <returns><c>true</c> if navigation is successful; otherwise, <c>false</c>.</returns>
		public bool NavigateToDefinition(TextSnapshotOffset snapshotOffset) {
			var resultSet = PerformResolution(snapshotOffset);
			if (resultSet.Results.Count > 0)
				return NavigateToDefinition(resultSet.Results[0]);

			MessageBox.Show("Cannot navigate to the symbol under the caret.", "Go To Definition", MessageBoxButton.OK, MessageBoxImage.Information);
			return false;
		}

		/// <summary>
		/// Attempts to navigate to a resolver result.
		/// </summary>
		/// <param name="result">The resolver result.</param>
		/// <returns><c>true</c> if navigation is successful; otherwise, <c>false</c>.</returns>
		public bool NavigateToDefinition(IResolverResult result) {
			var sourceFileLocation = GetSourceFileLocation(result);
			if (sourceFileLocation != null) {
				// Find the editor which is displaying this file
				var codeEditor = GetEditorForSourceFile(sourceFileLocation);
				if (codeEditor != null) {
					// Determine the location of the definition
					int navigationOffset = -1;
					if (sourceFileLocation.NavigationOffset.HasValue)
						navigationOffset = sourceFileLocation.NavigationOffset.Value;
					else if (!sourceFileLocation.TextRange.IsDeleted)
						navigationOffset = sourceFileLocation.TextRange.StartOffset;

					// Move to the definition
					if ((0 <= navigationOffset) && (navigationOffset < codeEditor.Document.CurrentSnapshot.Length)) {
						// Should the definition be selected?
						if (IsWordSelectedOnNavigation) {
							// Find the full range of the "word" at the navigation offset and select it
							var wordTextRange = codeEditor.Document.CurrentSnapshot.GetWordTextRange(navigationOffset);
							if (!wordTextRange.IsZeroLength) {
								codeEditor.ActiveView.Selection.SelectRange(wordTextRange);
								return true;
							}
						}

						// No selection... simply adjust the caret location to the definition
						codeEditor.ActiveView.Selection.CaretOffset = navigationOffset;
						return true;
					}
				}
			}

			MessageBox.Show("Cannot navigate to the symbol under the caret.", "Go To Definition", MessageBoxButton.OK, MessageBoxImage.Information);
			return false;
		}

		/// <summary>
		/// Gets a list of open editors.
		/// </summary>
		/// <remarks>
		/// When editors are opened or closed that support navigation, they must be added and removed from this list.
		/// </remarks>
		public List<SyntaxEditor> OpenEditors { get; } = new List<SyntaxEditor>();

		/// <summary>
		/// Performs a resolve request at the given offset and outputs the results.
		/// </summary>
		/// <param name="snapshotOffset">The snapshot offset.</param>
		/// <returns>An <see cref="IResolverResultSet"/> if available; otherwise <c>null</c>.</returns>
		public IResolverResultSet PerformResolution(TextSnapshotOffset snapshotOffset) {
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

					return resultSet;
				}
			}

			return null;
		}

	}

}
