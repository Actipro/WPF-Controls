using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Implementation;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.EditActions;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl : UserControl {

	private CommandBinding? _customCommandBinding;
	private KeyBinding? _customInputBinding;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		BindList();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Binds data to the list.
	/// </summary>
	private void BindList() {
		string ClipboardCategory = "Clipboard / Undo";
		string DeletionCategory = "Deletion";
		string InsertionCategory = "Insertion";
		string IntelliPromptCategory = "IntelliPrompt";
		string MacroCategory = "Macro Recording";
		string MiscellaneousCategory = "Miscellaneous";
		string MovementCategory = "Movement";
		string ScrollCategory = "Scroll";
		string SearchCategory = "Search";
		string SelectionCategory = "Selection";

		var actionDataArray = new EditActionData[] {
			// Clipboard/undo
			new(category: ClipboardCategory, action: new CopyAndAppendToClipboardAction()),
			new(category: ClipboardCategory, action: new CopyToClipboardAction()),
			new(category: ClipboardCategory, action: new CutAndAppendToClipboardAction()),
			new(category: ClipboardCategory, action: new CutLineToClipboardAction()),
			new(category: ClipboardCategory, action: new CutToClipboardAction()),
			new(category: ClipboardCategory, action: new PasteFromClipboardAction()),
			new(category: ClipboardCategory, action: new RedoAction()),
			new(category: ClipboardCategory, action: new Windows.Controls.SyntaxEditor.EditActions.UndoAction()),
			// Deletion
			new(category: DeletionCategory, action: new BackspaceAction()),
			new(category: DeletionCategory, action: new BackspaceToPreviousWordAction()),
			new(category: DeletionCategory, action: new DeleteAction()),
			new(category: DeletionCategory, action: new DeleteBlankLinesAction()),
			new(category: DeletionCategory, action: new DeleteHorizontalWhitespaceAction()),
			new(category: DeletionCategory, action: new DeleteLineAction()),
			new(category: DeletionCategory, action: new DeleteToLineEndAction()),
			new(category: DeletionCategory, action: new DeleteToLineStartAction()),
			new(category: DeletionCategory, action: new DeleteToNextWordAction()),
			// Insertion
			new(category: InsertionCategory, action: new InsertLineBreakAction()),
			new(category: InsertionCategory, action: new OpenLineAboveAction()),
			new(category: InsertionCategory, action: new OpenLineBelowAction()),
			new(category: InsertionCategory, action: new TypingAction("*Typing*", false)),
			// IntelliPrompt
			new(category: IntelliPromptCategory, action: new RequestIntelliPromptAutoCompleteAction()),
			new(category: IntelliPromptCategory, action: new RequestIntelliPromptCompletionSessionAction()),
			new(category: IntelliPromptCategory, action: new RequestIntelliPromptParameterInfoSessionAction()),
			new(category: IntelliPromptCategory, action: new RequestIntelliPromptQuickInfoSessionAction()),
			// Macro
			new(category: MacroCategory, action: new CancelMacroRecordingAction()),
			new(category: MacroCategory, action: new PauseResumeMacroRecordingAction()),
			new(category: MacroCategory, action: new RunMacroAction()),
			new(category: MacroCategory, action: new ToggleMacroRecordingAction()),
			// Miscellaneous
			new(category: MiscellaneousCategory, action: new CapitalizeAction()),
			new(category: MiscellaneousCategory, action: new CommentLinesAction()),
			new(category: MiscellaneousCategory, action: new ConvertSpacesToTabsAction()),
			new(category: MiscellaneousCategory, action: new ConvertTabsToSpacesAction()),
			new(category: MiscellaneousCategory, action: new DuplicateAction()),
			new(category: MiscellaneousCategory, action: new FormatDocumentAction()),
			new(category: MiscellaneousCategory, action: new FormatSelectionAction()),
			new(category: MiscellaneousCategory, action: new IndentAction()),
			new(category: MiscellaneousCategory, action: new InsertTabStopOrIndentAction()),
			new(category: MiscellaneousCategory, action: new MakeLowercaseAction()),
			new(category: MiscellaneousCategory, action: new MakeUppercaseAction()),
			new(category: MiscellaneousCategory, action: new MoveSelectedLinesDownAction()),
			new(category: MiscellaneousCategory, action: new MoveSelectedLinesUpAction()),
			new(category: MiscellaneousCategory, action: new NormalizeLineTerminatorsToCRLFAction()),
			new(category: MiscellaneousCategory, action: new NormalizeLineTerminatorsToLFAction()),
			new(category: MiscellaneousCategory, action: new OutdentAction()),
			new(category: MiscellaneousCategory, action: new RemoveTabStopOrOutdentAction()),
			new(category: MiscellaneousCategory, action: new ResetZoomLevelAction()),
			new(category: MiscellaneousCategory, action: new TabifySelectedLinesAction()),
			new(category: MiscellaneousCategory, action: new ToggleCharacterCasingAction()),
			new(category: MiscellaneousCategory, action: new ToggleOverwriteModeAction()),
			new(category: MiscellaneousCategory, action: new TransposeCharactersAction()),
			new(category: MiscellaneousCategory, action: new TransposeLinesAction()),
			new(category: MiscellaneousCategory, action: new TransposeWordsAction()),
			new(category: MiscellaneousCategory, action: new TrimAllTrailingWhitespaceAction()),
			new(category: MiscellaneousCategory, action: new TrimTrailingWhitespaceAction()),
			new(category: MiscellaneousCategory, action: new UncommentLinesAction()),
			new(category: MiscellaneousCategory, action: new UntabifySelectedLinesAction()),
			new(category: MiscellaneousCategory, action: new ZoomInAction()),
			new(category: MiscellaneousCategory, action: new ZoomOutAction()),
			// Movement
			new(category: MovementCategory, action: new MoveDownAction()),
			new(category: MovementCategory, action: new MoveLeftAction()),
			new(category: MovementCategory, action: new MovePageDownAction()),
			new(category: MovementCategory, action: new MovePageUpAction()),
			new(category: MovementCategory, action: new MoveRightAction()),
			new(category: MovementCategory, action: new MoveToDocumentEndAction()),
			new(category: MovementCategory, action: new MoveToDocumentStartAction()),
			new(category: MovementCategory, action: new MoveToLineEndAction()),
			new(category: MovementCategory, action: new MoveToLineStartAction()),
			new(category: MovementCategory, action: new MoveToLineStartAfterIndentationAction()),
			new(category: MovementCategory, action: new MoveToMatchingBracketAction()),
			new(category: MovementCategory, action: new MoveToNextLineStartAfterIndentationAction()),
			new(category: MovementCategory, action: new MoveToNextWordAction()),
			new(category: MovementCategory, action: new MoveToPreviousLineStartAfterIndentationAction()),
			new(category: MovementCategory, action: new MoveToPreviousWordAction()),
			new(category: MovementCategory, action: new MoveToVisibleBottomAction()),
			new(category: MovementCategory, action: new MoveToVisibleTopAction()),
			new(category: MovementCategory, action: new MoveUpAction()),
			// Scroll
			new(category: ScrollCategory, action: new ScrollDownAction()),
			new(category: ScrollCategory, action: new ScrollLeftAction()),
			new(category: ScrollCategory, action: new ScrollLineToVisibleBottomAction()),
			new(category: ScrollCategory, action: new ScrollLineToVisibleMiddleAction()),
			new(category: ScrollCategory, action: new ScrollLineToVisibleTopAction()),
			new(category: ScrollCategory, action: new ScrollPageDownAction()),
			new(category: ScrollCategory, action: new ScrollPageUpAction()),
			new(category: ScrollCategory, action: new ScrollRightAction()),
			new(category: ScrollCategory, action: new ScrollToDocumentEndAction()),
			new(category: ScrollCategory, action: new ScrollToDocumentStartAction()),
			new(category: ScrollCategory, action: new ScrollUpAction()),
			// Search
			new(category: SearchCategory, action: new FindAction()),
			new(category: SearchCategory, action: new FindNextAction()),
			new(category: SearchCategory, action: new FindNextSelectedAction()),
			new(category: SearchCategory, action: new FindPreviousAction()),
			new(category: SearchCategory, action: new FindPreviousSelectedAction()),
			new(category: SearchCategory, action: new IncrementalSearchAction()),
			new(category: SearchCategory, action: new ReplaceAction()),
			new(category: SearchCategory, action: new ReverseIncrementalSearchAction()),
			// Selection
			new(category: SelectionCategory, action: new CodeBlockSelectionContractAction()),
			new(category: SelectionCategory, action: new CodeBlockSelectionExpandAction()),
			new(category: SelectionCategory, action: new CollapseSelectionAction()),
			new(category: SelectionCategory, action: new CollapseSelectionLeftAction()),
			new(category: SelectionCategory, action: new CollapseSelectionRightAction()),
			new(category: SelectionCategory, action: new SelectAllAction()),
			new(category: SelectionCategory, action: new SelectBlockDownAction()),
			new(category: SelectionCategory, action: new SelectBlockLeftAction()),
			new(category: SelectionCategory, action: new SelectBlockRightAction()),
			new(category: SelectionCategory, action: new SelectBlockToNextWordAction()),
			new(category: SelectionCategory, action: new SelectBlockToPreviousWordAction()),
			new(category: SelectionCategory, action: new SelectBlockUpAction()),
			new(category: SelectionCategory, action: new SelectDownAction()),
			new(category: SelectionCategory, action: new SelectLeftAction()),
			new(category: SelectionCategory, action: new SelectPageDownAction()),
			new(category: SelectionCategory, action: new SelectPageUpAction()),
			new(category: SelectionCategory, action: new SelectRightAction()),
			new(category: SelectionCategory, action: new SelectToDocumentEndAction()),
			new(category: SelectionCategory, action: new SelectToDocumentStartAction()),
			new(category: SelectionCategory, action: new SelectToLineEndAction()),
			new(category: SelectionCategory, action: new SelectToLineStartAction()),
			new(category: SelectionCategory, action: new SelectToLineStartAfterIndentationAction()),
			new(category: SelectionCategory, action: new SelectToMatchingBracketAction()),
			new(category: SelectionCategory, action: new SelectToNextWordAction()),
			new(category: SelectionCategory, action: new SelectToPreviousWordAction()),
			new(category: SelectionCategory, action: new SelectToVisibleBottomAction()),
			new(category: SelectionCategory, action: new SelectToVisibleTopAction()),
			new(category: SelectionCategory, action: new SelectUpAction()),
			new(category: SelectionCategory, action: new SelectWordAction()),
		};

		// Find the default binding for each action
		foreach (var actionData in actionDataArray) {
			foreach (var keyBinding in editor.InputBindings.OfType<KeyBinding>()) {
				var command = keyBinding.Command as IEditAction;
				if (command?.Key == actionData.Name) {
					actionData.Key = EditActionBase.GetKeyText(keyBinding.Modifiers, keyBinding.Key);
					break;
				}
			}
		}

		// Create a collection view source
		var source = new ListCollectionView(actionDataArray);
		source.GroupDescriptions.Add(new PropertyGroupDescription("Category"));

		// Set list items source
		editActionsListView.ItemsSource = source;
	}

	private void OnBindCustomAction(object sender, RoutedEventArgs e) {
		// Unbind
		UnbindCustomEditAction();

		// Add a command binding to action
		var command = new CustomAction();
		_customCommandBinding = command.CreateCommandBinding();
		editor.CommandBindings.Insert(0, _customCommandBinding);

		// Bind to Ctrl+P
		_customInputBinding = new KeyBinding(command, Key.P, ModifierKeys.Control);
		editor.InputBindings.Add(_customInputBinding);

		// Notify user
		MessageBox.Show("Bound Ctrl+P to custom edit action.", "Result", MessageBoxButton.OK, MessageBoxImage.Information);
		editor.Focus();
	}

	private void OnEditActionListViewDoubleClick(object sender, MouseButtonEventArgs e) {
		if (sender is ListBox { SelectedItem: EditActionData actionData }) {
			// If the action can execute...
			if (actionData.Action.CanExecute(editor.ActiveView)) {
				// Focus the editor
				editor.Focus();

				// Execute it
				actionData.Action.Execute(editor.ActiveView);
			}
			else {
				// Display a message
				MessageBox.Show("The selected edit action cannot currently execute based on the current selection context.", "Cannot Execute", MessageBoxButton.OK, MessageBoxImage.Information);
			}
		}
	}

	private void OnExecuteCustomAction(object sender, RoutedEventArgs e) {
		editor.Focus();
		new CustomAction().Execute(editor.ActiveView);
	}

	private void OnUnbindCustomAction(object sender, RoutedEventArgs e) {
		// Unbind
		UnbindCustomEditAction();

		// Notify user
		MessageBox.Show("Unbound Ctrl+P.", "Result", MessageBoxButton.OK, MessageBoxImage.Information);
		editor.Focus();
	}

	/// <summary>
	/// Unbinds the custom edit action.
	/// </summary>
	private void UnbindCustomEditAction() {
		if (_customCommandBinding is not null) {
			editor.CommandBindings.Remove(_customCommandBinding);
			_customCommandBinding = null;
		}
		if (_customInputBinding is not null) {
			editor.InputBindings.Remove(_customInputBinding);
			_customInputBinding = null;
		}
	}

}
