using ActiproSoftware.Text;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;
using System.Activities.Presentation;
using System.Activities.Presentation.Model;
using System.Activities.Presentation.View;
using System.Windows.Threading;

namespace ActiproSoftware.Windows.WindowsWorkflowIntegration.ExpressionEditing {

	/// <summary>
	/// Implements an <see cref="IExpressionEditorInstance"/> that uses SyntaxEditor.
	/// </summary>
	public class MyExpressionEditorInstance : IExpressionEditorInstance {

		private readonly WorkflowDesigner _designer;
		private readonly SyntaxEditor _editor;
		private readonly List<ModelItem> _variableModels;

		// --------------------------------------------------------------------------------------------------
		// OBJECT
		// --------------------------------------------------------------------------------------------------

		/// <inheritdoc cref="IExpressionEditorInstance.Closing"/>
		public event EventHandler? Closing;

		/// <inheritdoc cref="IExpressionEditorInstance.GotAggregateFocus"/>
		public event EventHandler? GotAggregateFocus;

		/// <inheritdoc cref="IExpressionEditorInstance.LostAggregateFocus"/>
		public event EventHandler? LostAggregateFocus;

		/// <inheritdoc cref="IExpressionEditorInstance.TextChanged"/>
		public event EventHandler? TextChanged;

		// --------------------------------------------------------------------------------------------------
		// OBJECT
		// --------------------------------------------------------------------------------------------------

		/// <summary>
		/// Initializes an instance of the class.
		/// </summary>
		/// <param name="designer">The <see cref="WorkflowDesigner"/> that owns the instance.</param>
		/// <param name="variableModels">The variable list.</param>
		/// <param name="language">The <see cref="ISyntaxLanguage"/> to use.</param>
		public MyExpressionEditorInstance(WorkflowDesigner designer, List<ModelItem> variableModels, ISyntaxLanguage language) {
			_designer = designer ?? throw new ArgumentNullException(nameof(designer));
			_variableModels = variableModels;

			// Create a SyntaxEditor
			_editor = new SyntaxEditor {
				BorderThickness = new Thickness(0),
				CanSplitHorizontally = false,
				IsDefaultContextMenuEnabled = false,
				IsMultiLine = false,
				IsOutliningMarginVisible = false,
				IsSelectionMarginVisible = false
			};
			_editor.Document.Language = language ?? throw new ArgumentNullException(nameof(language));
			_editor.IsKeyboardFocusWithinChanged += OnSyntaxEditorIsKeyboardFocusWithinChanged;
			_editor.DocumentTextChanged += OnSyntaxEditorDocumentTextChanged;
			_editor.Unloaded += OnSyntaxEditorUnloaded;

			// Use this line to change the editor's font, but not affect IntelliPrompt popups
			// AmbientHighlightingStyleRegistry.Instance[new DisplayItemClassificationTypeProvider().PlainText].FontFamilyName = "Consolas";

			// Initialize header and footer text (so we edit an expression and variables appear in automated IntelliPrompt)
			InitializeHeaderAndFooter();
		}

		// --------------------------------------------------------------------------------------------------
		// NON-PUBLIC PROCEDURES
		// --------------------------------------------------------------------------------------------------

		/// <summary>
		/// Initialize the header and footer text that for parsing purposes will surround the visible document's text.
		/// </summary>
		private void InitializeHeaderAndFooter() {
			if (_editor.Document.Language is IExpressionEditorSyntaxLanguage language) {
				// Assign the header and footer text
				var headerText = language.GetHeaderText(_variableModels);
				var footerText = language.GetFooterText();
				_editor.Document.SetHeaderAndFooterText(headerText, footerText);
			}
		}

		/// <summary>
		/// Occurs when the editor's text has changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The event data.</param>
		private void OnSyntaxEditorDocumentTextChanged(object sender, EditorSnapshotChangedEventArgs e) {
			// If the text that was typed is a letter character that is starting a word... 
			if (e.IsTypedWordStart) {
				// If no completion session is currently open, show a completion list
				if (!_editor.IntelliPrompt.Sessions.Contains(IntelliPromptSessionTypes.Completion))
					_editor.ActiveView.IntelliPrompt.RequestCompletionSession();
			}

			// Raise an event
			TextChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// Occurs when keyboard focus within changes.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The event data.</param>
		private void OnSyntaxEditorIsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e) {
			if (_editor.IsKeyboardFocusWithin) {
				// Dispatch to ensure that the visual is available before trying to move focus to the view
				_editor.Dispatcher.BeginInvoke(DispatcherPriority.Send, (DispatcherOperationCallback)((arg) => {
					if (!_editor.ActiveView.VisualElement.IsKeyboardFocusWithin)
						_editor.ActiveView.Focus();
					return null;
				}), null);

				// Raise an event
				GotAggregateFocus?.Invoke(this, EventArgs.Empty);
			}
			else {
				// Raise an event
				LostAggregateFocus?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Occurs when the control is unloaded.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The event data.</param>
		private void OnSyntaxEditorUnloaded(object sender, RoutedEventArgs e)
			=> Closing?.Invoke(this, EventArgs.Empty);

		// --------------------------------------------------------------------------------------------------
		// PUBLIC PROCEDURES
		// --------------------------------------------------------------------------------------------------

		/// <inheritdoc cref="IExpressionEditorInstance.AcceptsReturn"/>
		public bool AcceptsReturn {
			get => _editor.IsMultiLine;
			set {
				// No-op: _editor.IsMultiLine = value;
			}
		}

		/// <inheritdoc cref="IExpressionEditorInstance.AcceptsTab"/>
		public bool AcceptsTab {
			get => _editor.AcceptsTab;
			set => _editor.AcceptsTab = value;
		}

		/// <inheritdoc cref="IExpressionEditorInstance.HasAggregateFocus"/>
		public bool HasAggregateFocus
			=> _editor.IsKeyboardFocusWithin;

		/// <inheritdoc cref="IExpressionEditorInstance.HorizontalScrollBarVisibility"/>
		public ScrollBarVisibility HorizontalScrollBarVisibility {
			get => _editor.HorizontalScrollBarVisibility;
			set => _editor.HorizontalScrollBarVisibility = value;
		}

		/// <inheritdoc cref="IExpressionEditorInstance.HostControl"/>
		public Control HostControl
			=> _editor;

		/// <inheritdoc cref="IExpressionEditorInstance.CanCompleteWord"/>
		public bool CanCompleteWord()
			=> true;

		/// <inheritdoc cref="IExpressionEditorInstance.CanCopy"/>
		public bool CanCopy()
			=> true;

		/// <inheritdoc cref="IExpressionEditorInstance.CanCut"/>
		public bool CanCut()
			=> true;

		/// <inheritdoc cref="IExpressionEditorInstance.CanDecreaseFilterLevel"/>
		public bool CanDecreaseFilterLevel()
			=> false;

		/// <inheritdoc cref="IExpressionEditorInstance.CanGlobalIntellisense"/>
		public bool CanGlobalIntellisense()
			=> true;

		/// <inheritdoc cref="IExpressionEditorInstance.CanIncreaseFilterLevel"/>
		public bool CanIncreaseFilterLevel()
			=> false;

		/// <inheritdoc cref="IExpressionEditorInstance.CanParameterInfo"/>
		public bool CanParameterInfo()
			=> true;

		/// <inheritdoc cref="IExpressionEditorInstance.CanPaste"/>
		public bool CanPaste()
			=> true;

		/// <inheritdoc cref="IExpressionEditorInstance.CanQuickInfo"/>
		public bool CanQuickInfo()
			=> true;

		/// <inheritdoc cref="IExpressionEditorInstance.CanRedo"/>
		public bool CanRedo()
			=> _editor.Document.UndoHistory.CanRedo;

		/// <inheritdoc cref="IExpressionEditorInstance.CanUndo"/>
		public bool CanUndo()
			=> _editor.Document.UndoHistory.CanUndo;

		/// <inheritdoc cref="IExpressionEditorInstance.ClearSelection"/>
		public void ClearSelection()
			=> _editor.ActiveView.Selection.Collapse();

		/// <inheritdoc cref="IExpressionEditorInstance.Close"/>
		public void Close()
			=> _editor.IntelliPrompt.CloseAllSessions();

		/// <inheritdoc cref="IExpressionEditorInstance.CompleteWord"/>
		public bool CompleteWord() {
			_editor.ActiveView.IntelliPrompt.RequestAutoComplete();
			return true;
		}

		/// <inheritdoc cref="IExpressionEditorInstance.Copy"/>
		public bool Copy() {
			_editor.ActiveView.CopyToClipboard();
			return true;
		}

		/// <inheritdoc cref="IExpressionEditorInstance.Cut"/>
		public bool Cut() {
			_editor.ActiveView.CutToClipboard();
			return true;
		}

		/// <inheritdoc cref="IExpressionEditorInstance.DecreaseFilterLevel"/>
		public bool DecreaseFilterLevel()
			=> false;

		/// <inheritdoc cref="IExpressionEditorInstance.Focus"/>
		public void Focus()
			=> _editor.Focus();

		/// <inheritdoc cref="IExpressionEditorInstance.GetCommittedText"/>
		public string GetCommittedText()
			=> _editor.Document.CurrentSnapshot.Text;

		/// <inheritdoc cref="IExpressionEditorInstance.GlobalIntellisense"/>
		public bool GlobalIntellisense() {
			_editor.ActiveView.IntelliPrompt.RequestCompletionSession();
			return (_editor.IntelliPrompt.Sessions[IntelliPromptSessionTypes.Completion] is not null);
		}

		/// <inheritdoc cref="IExpressionEditorInstance.IncreaseFilterLevel"/>
		public bool IncreaseFilterLevel()
			=> false;

		/// <inheritdoc cref="IExpressionEditorInstance.MaxLines"/>
		public int MaxLines { get; set; }

		/// <inheritdoc cref="IExpressionEditorInstance.MinLines"/>
		public int MinLines { get; set; }

		/// <inheritdoc cref="IExpressionEditorInstance.ParameterInfo"/>
		public bool ParameterInfo() {
			_editor.ActiveView.IntelliPrompt.RequestParameterInfoSession();
			return (_editor.IntelliPrompt.Sessions[IntelliPromptSessionTypes.ParameterInfo] is not null);
		}

		/// <inheritdoc cref="IExpressionEditorInstance.Paste"/>
		public bool Paste() {
			_editor.ActiveView.PasteFromClipboard();
			return true;
		}

		/// <inheritdoc cref="IExpressionEditorInstance.QuickInfo"/>
		public bool QuickInfo() {
			_editor.ActiveView.IntelliPrompt.RequestQuickInfoSession();
			return (_editor.IntelliPrompt.Sessions[IntelliPromptSessionTypes.QuickInfo] is not null);
		}

		/// <inheritdoc cref="IExpressionEditorInstance.Redo"/>
		public bool Redo()
			=> _editor.Document.UndoHistory.Redo();

		/// <inheritdoc cref="IExpressionEditorInstance.Text"/>
		public string Text {
			get => _editor.Document.CurrentSnapshot.Text;
			set => _editor.Document.SetText(value);
		}

		/// <inheritdoc cref="IExpressionEditorInstance.Undo"/>
		public bool Undo()
			=> _editor.Document.UndoHistory.Undo();

		/// <inheritdoc cref="IExpressionEditorInstance.VerticalScrollBarVisibility"/>
		public ScrollBarVisibility VerticalScrollBarVisibility {
			get => _editor.VerticalScrollBarVisibility;
			set => _editor.VerticalScrollBarVisibility = value;
		}

	}

}
