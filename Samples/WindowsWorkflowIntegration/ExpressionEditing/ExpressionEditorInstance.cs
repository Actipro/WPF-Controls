using ActiproSoftware.Text;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;
using System;
using System.Activities.Presentation;
using System.Activities.Presentation.Model;
using System.Activities.Presentation.View;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ActiproSoftware.Windows.WindowsWorkflowIntegration.ExpressionEditing {

	/// <summary>
	/// Implements an <see cref="IExpressionEditorInstance"/> that uses SyntaxEditor.
	/// </summary>
	public class MyExpressionEditorInstance : IExpressionEditorInstance {

		private WorkflowDesigner designer;
		private SyntaxEditor editor;
		private List<ModelItem> variableModels;

		public event EventHandler Closing;
		public event EventHandler GotAggregateFocus;
		public event EventHandler LostAggregateFocus;
		public event EventHandler TextChanged;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>MyExpressionEditorInstance</c> class.
		/// </summary>
		/// <param name="designer">The <see cref="WorkflowDesigner"/> that owns the instance.</param>
		/// <param name="variableModels">The variable list.</param>
		/// <param name="language">The <see cref="ISyntaxLanguage"/> to use.</param>
		public MyExpressionEditorInstance(WorkflowDesigner designer, List<ModelItem> variableModels, ISyntaxLanguage language) {
			if (designer == null)
				throw new ArgumentNullException("designer");
			if (language == null)
				throw new ArgumentNullException("language");

			this.designer = designer;
			this.variableModels = variableModels;

			// Create a SyntaxEditor
			editor = new SyntaxEditor();
			editor.BorderThickness = new Thickness(0);
			editor.CanSplitHorizontally = false;
			editor.IsDefaultContextMenuEnabled = false;
			editor.IsMultiLine = false;
			editor.IsOutliningMarginVisible = false;
			editor.IsSelectionMarginVisible = false;
			editor.Document.Language = language;
			editor.IsKeyboardFocusWithinChanged += OnSyntaxEditorIsKeyboardFocusWithinChanged;
			editor.DocumentTextChanged += OnSyntaxEditorDocumentTextChanged;
			editor.Unloaded += OnSyntaxEditorUnloaded;

			// Use this line to change the editor's font, but not affect IntelliPrompt popups
			// AmbientHighlightingStyleRegistry.Instance[new DisplayItemClassificationTypeProvider().PlainText].FontFamilyName = "Consolas";

			// Initialize header and footer text (so we edit an expression and variables appear in automated IntelliPrompt)
			this.InitializeHeaderAndFooter();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initialize the header and footer text that for parsing purposes will surround the visible document's text.
		/// </summary>
		private void InitializeHeaderAndFooter() {
			var language = editor.Document.Language as IExpressionEditorSyntaxLanguage;
			if (language != null) {
				// Assign the header and footer text
				var headerText = language.GetHeaderText(variableModels);
				var footerText = language.GetFooterText();
				editor.Document.SetHeaderAndFooterText(headerText.ToString(), footerText);
			}
		}

		/// <summary>
		/// Occurs when the editor's text has changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>EditorSnapshotChangedEventArgs</c> that contains data related to the event.</param>
		private void OnSyntaxEditorDocumentTextChanged(object sender, EditorSnapshotChangedEventArgs e) {
			// If the text that was typed is a letter character that is starting a word... 
			if (e.IsTypedWordStart) {
				// If no completion session is currently open, show a completion list
				if (!editor.IntelliPrompt.Sessions.Contains(IntelliPromptSessionTypes.Completion))
					editor.ActiveView.IntelliPrompt.RequestCompletionSession();
			}

			// Raise an event
			var handler = this.TextChanged;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// Occurs when keyboard focus within changes.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DependencyPropertyChangedEventArgs</c> that contains data related to the event.</param>
		private void OnSyntaxEditorIsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e) {
			if (editor.IsKeyboardFocusWithin) {
				// Dispatch to ensure that the visual is available before trying to move focus to the view
				editor.Dispatcher.BeginInvoke(DispatcherPriority.Send, (DispatcherOperationCallback)delegate (object arg) {
					if (!editor.ActiveView.VisualElement.IsKeyboardFocusWithin)
						editor.ActiveView.Focus();
					return null;
				}, null);

				// Raise an event
				var handler = this.GotAggregateFocus;
				if (handler != null)
					handler(this, EventArgs.Empty);
			}
			else {
				// Raise an event
				var handler = this.LostAggregateFocus;
				if (handler != null)
					handler(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Occurs when the control is unloaded.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>RoutedEventArgs</c> that contains data related to the event.</param>
		private void OnSyntaxEditorUnloaded(object sender, RoutedEventArgs e) {
			var handler = this.Closing;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets whether the control accepts the <c>Return</c> key.
		/// </summary>
		/// <value>
		/// <c>true</c> if the control accepts the <c>Return</c> key; otherwise, <c>false</c>.
		/// </value>
		public bool AcceptsReturn {
			get {
				return editor.IsMultiLine;
			}
			set {
				// No-op: editor.IsMultiLine = value;
			}
		}

		/// <summary>
		/// Gets or sets whether the control accepts the <c>Tab</c> key.
		/// </summary>
		/// <value>
		/// <c>true</c> if the control accepts the <c>Tab</c> key; otherwise, <c>false</c>.
		/// </value>
		public bool AcceptsTab {
			get {
				return editor.AcceptsTab;
			}
			set {
				editor.AcceptsTab = value;
			}
		}

		/// <summary>
		/// Gets whether the editor contains focus.
		/// </summary>
		/// <value>
		/// <c>true</c> if the editor contains focus; otherwise, <c>false</c>.
		/// </value>
		public bool HasAggregateFocus {
			get {
				return editor.IsKeyboardFocusWithin;
			}
		}

		/// <summary>
		/// Gets or sets the horizontal scrollbar visibility.
		/// </summary>
		/// <value>The horizontal scrollbar visibility.</value>
		public ScrollBarVisibility HorizontalScrollBarVisibility {
			get {
				return editor.HorizontalScrollBarVisibility;
			}
			set {
				editor.HorizontalScrollBarVisibility = value;
			}
		}

		/// <summary>
		/// Gets the host control.
		/// </summary>
		/// <value>The host control.</value>
		public Control HostControl {
			get {
				return editor;
			}
		}

		/// <summary>
		/// Gets whether the editor can complete words.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the editor can complete words; otherwise, <c>false</c>.
		/// </returns>
		public bool CanCompleteWord() {
			return true;
		}

		/// <summary>
		/// Gets whether the editor can copy text.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the editor can copy text; otherwise, <c>false</c>.
		/// </returns>
		public bool CanCopy() {
			return true;
		}

		/// <summary>
		/// Gets whether the editor can cut text.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the editor can cut text; otherwise, <c>false</c>.
		/// </returns>
		public bool CanCut() {
			return true;
		}

		/// <summary>
		/// Gets whether the editor can decrease filter level.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the editor can decrease filter level; otherwise, <c>false</c>.
		/// </returns>
		public bool CanDecreaseFilterLevel() {
			return false;
		}

		/// <summary>
		/// Gets whether the editor can show the completion list.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the editor can show the completion list; otherwise, <c>false</c>.
		/// </returns>
		public bool CanGlobalIntellisense() {
			return true;
		}

		/// <summary>
		/// Gets whether the editor can increase filter level.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the editor can increase filter level; otherwise, <c>false</c>.
		/// </returns>
		public bool CanIncreaseFilterLevel() {
			return false;
		}

		/// <summary>
		/// Gets whether the editor can show parameter info.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the editor can show parameter info; otherwise, <c>false</c>.
		/// </returns>
		public bool CanParameterInfo() {
			return true;
		}

		/// <summary>
		/// Gets whether the editor can paste text.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the editor can paste text; otherwise, <c>false</c>.
		/// </returns>
		public bool CanPaste() {
			return true;
		}

		/// <summary>
		/// Gets whether the editor can show quick info.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the editor can show quick info; otherwise, <c>false</c>.
		/// </returns>
		public bool CanQuickInfo() {
			return true;
		}

		/// <summary>
		/// Gets whether the editor can redo.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the editor can redo; otherwise, <c>false</c>.
		/// </returns>
		public bool CanRedo() {
			return editor.Document.UndoHistory.CanRedo;
		}

		/// <summary>
		/// Gets whether the editor can undo.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the editor can undo; otherwise, <c>false</c>.
		/// </returns>
		public bool CanUndo() {
			return editor.Document.UndoHistory.CanUndo;
		}

		/// <summary>
		/// Collapses the selection.
		/// </summary>
		public void ClearSelection() {
			editor.ActiveView.Selection.Collapse();
		}

		/// <summary>
		/// Closes all IntelliPrompt popups.
		/// </summary>
		public void Close() {
			editor.IntelliPrompt.CloseAllSessions();
		}

		/// <summary>
		/// Requests auto-complete.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the operation was successful; otherwise, <c>false</c>.
		/// </returns>
		public bool CompleteWord() {
			editor.ActiveView.IntelliPrompt.RequestAutoComplete();
			return true;
		}

		/// <summary>
		/// Copies the selected text.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the operation was successful; otherwise, <c>false</c>.
		/// </returns>
		public bool Copy() {
			editor.ActiveView.CopyToClipboard();
			return true;
		}

		/// <summary>
		/// Cuts the selected text.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the operation was successful; otherwise, <c>false</c>.
		/// </returns>
		public bool Cut() {
			editor.ActiveView.CutToClipboard();
			return true;
		}

		/// <summary>
		/// Decreases the filter level.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the operation was successful; otherwise, <c>false</c>.
		/// </returns>
		public bool DecreaseFilterLevel() {
			return false;
		}

		/// <summary>
		/// Focuses the editor.
		/// </summary>
		public void Focus() {
			editor.Focus();
		}

		/// <summary>
		/// Returns the editor's text.
		/// </summary>
		/// <returns>The editor's text.</returns>
		public string GetCommittedText() {
			return editor.Document.CurrentSnapshot.Text;
		}

		/// <summary>
		/// Shows the completion list.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the operation was successful; otherwise, <c>false</c>.
		/// </returns>
		public bool GlobalIntellisense() {
			editor.ActiveView.IntelliPrompt.RequestCompletionSession();
			return (editor.IntelliPrompt.Sessions[IntelliPromptSessionTypes.Completion] != null);
		}

		/// <summary>
		/// Increases the filter level.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the operation was successful; otherwise, <c>false</c>.
		/// </returns>
		public bool IncreaseFilterLevel() {
			return false;
		}

		/// <summary>
		/// Gets or sets the maximum number of lines.
		/// </summary>
		/// <value>The maximum number of lines.</value>
		public int MaxLines { get; set; }

		/// <summary>
		/// Gets or sets the minimum number of lines.
		/// </summary>
		/// <value>The minimum number of lines.</value>
		public int MinLines { get; set; }

		/// <summary>
		/// Shows the parameter info.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the operation was successful; otherwise, <c>false</c>.
		/// </returns>
		public bool ParameterInfo() {
			editor.ActiveView.IntelliPrompt.RequestParameterInfoSession();
			return (editor.IntelliPrompt.Sessions[IntelliPromptSessionTypes.ParameterInfo] != null);
		}

		/// <summary>
		/// Pastes text.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the operation was successful; otherwise, <c>false</c>.
		/// </returns>
		public bool Paste() {
			editor.ActiveView.PasteFromClipboard();
			return true;
		}

		/// <summary>
		/// Shows the quick info.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the operation was successful; otherwise, <c>false</c>.
		/// </returns>
		public bool QuickInfo() {
			editor.ActiveView.IntelliPrompt.RequestQuickInfoSession();
			return (editor.IntelliPrompt.Sessions[IntelliPromptSessionTypes.QuickInfo] != null);
		}

		/// <summary>
		/// Executes a redo operation.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the operation was successful; otherwise, <c>false</c>.
		/// </returns>
		public bool Redo() {
			return editor.Document.UndoHistory.Redo();
		}

		/// <summary>
		/// Gets or sets the editor's text.
		/// </summary>
		/// <value>The editor's text.</value>
		public string Text {
			get {
				return editor.Document.CurrentSnapshot.Text;
			}
			set {
				editor.Document.SetText(value);
			}
		}

		/// <summary>
		/// Executes an undo operation.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the operation was successful; otherwise, <c>false</c>.
		/// </returns>
		public bool Undo() {
			return editor.Document.UndoHistory.Undo();
		}

		/// <summary>
		/// Gets or sets the vertical scrollbar visibility.
		/// </summary>
		/// <value>The vertical scrollbar visibility.</value>
		public ScrollBarVisibility VerticalScrollBarVisibility {
			get {
				return editor.VerticalScrollBarVisibility;
			}
			set {
				editor.VerticalScrollBarVisibility = value;
			}
		}

	}

}
