using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ActiproSoftware.Text;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.EditActions {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl : System.Windows.Controls.UserControl {

		private CommandBinding	customCommandBinding;
		private KeyBinding		customInputBinding;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			this.BindList();
        }

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
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

			EditActionData[] actionDataArray = new EditActionData[] {
				// Clipboard/undo
				new EditActionData() { Category = ClipboardCategory, Action = new CopyAndAppendToClipboardAction() },
				new EditActionData() { Category = ClipboardCategory, Action = new CopyToClipboardAction() },
				new EditActionData() { Category = ClipboardCategory, Action = new CutAndAppendToClipboardAction() },
				new EditActionData() { Category = ClipboardCategory, Action = new CutLineToClipboardAction() },
				new EditActionData() { Category = ClipboardCategory, Action = new CutToClipboardAction() },
				new EditActionData() { Category = ClipboardCategory, Action = new PasteFromClipboardAction() },
				new EditActionData() { Category = ClipboardCategory, Action = new RedoAction() },
				new EditActionData() { Category = ClipboardCategory, Action = new ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.UndoAction() },
				// Deletion
				new EditActionData() { Category = DeletionCategory, Action = new BackspaceAction() },
				new EditActionData() { Category = DeletionCategory, Action = new BackspaceToPreviousWordAction() },
				new EditActionData() { Category = DeletionCategory, Action = new DeleteAction() },
				new EditActionData() { Category = DeletionCategory, Action = new DeleteBlankLinesAction() },
				new EditActionData() { Category = DeletionCategory, Action = new DeleteHorizontalWhitespaceAction() },
				new EditActionData() { Category = DeletionCategory, Action = new DeleteLineAction() },
				new EditActionData() { Category = DeletionCategory, Action = new DeleteToLineEndAction() },
				new EditActionData() { Category = DeletionCategory, Action = new DeleteToLineStartAction() },
				new EditActionData() { Category = DeletionCategory, Action = new DeleteToNextWordAction() },
				// Insertion
				new EditActionData() { Category = InsertionCategory, Action = new InsertLineBreakAction() },
				new EditActionData() { Category = InsertionCategory, Action = new OpenLineAboveAction() },
				new EditActionData() { Category = InsertionCategory, Action = new OpenLineBelowAction() },
				new EditActionData() { Category = InsertionCategory, Action = new TypingAction("*Typing*", false) },
				// IntelliPrompt
				new EditActionData() { Category = IntelliPromptCategory, Action = new RequestIntelliPromptAutoCompleteAction() },
				new EditActionData() { Category = IntelliPromptCategory, Action = new RequestIntelliPromptCompletionSessionAction() },
				new EditActionData() { Category = IntelliPromptCategory, Action = new RequestIntelliPromptParameterInfoSessionAction() },
				new EditActionData() { Category = IntelliPromptCategory, Action = new RequestIntelliPromptQuickInfoSessionAction() },
				// Macro
				new EditActionData() { Category = MacroCategory, Action = new CancelMacroRecordingAction() },
				new EditActionData() { Category = MacroCategory, Action = new PauseResumeMacroRecordingAction() },
				new EditActionData() { Category = MacroCategory, Action = new RunMacroAction() },
				new EditActionData() { Category = MacroCategory, Action = new ToggleMacroRecordingAction() },
				// Miscellaneous
				new EditActionData() { Category = MiscellaneousCategory, Action = new CapitalizeAction() },
				new EditActionData() { Category = MiscellaneousCategory, Action = new CommentLinesAction() },
				new EditActionData() { Category = MiscellaneousCategory, Action = new ConvertSpacesToTabsAction() },
				new EditActionData() { Category = MiscellaneousCategory, Action = new ConvertTabsToSpacesAction() },
				new EditActionData() { Category = MiscellaneousCategory, Action = new DuplicateAction() },
				new EditActionData() { Category = MiscellaneousCategory, Action = new FormatDocumentAction() },
				new EditActionData() { Category = MiscellaneousCategory, Action = new FormatSelectionAction() },
				new EditActionData() { Category = MiscellaneousCategory, Action = new IndentAction() },
				new EditActionData() { Category = MiscellaneousCategory, Action = new InsertTabStopOrIndentAction() },
				new EditActionData() { Category = MiscellaneousCategory, Action = new MakeLowercaseAction() },
				new EditActionData() { Category = MiscellaneousCategory, Action = new MakeUppercaseAction() },
				new EditActionData() { Category = MiscellaneousCategory, Action = new MoveSelectedLinesDownAction() },
				new EditActionData() { Category = MiscellaneousCategory, Action = new MoveSelectedLinesUpAction() },
				new EditActionData() { Category = MiscellaneousCategory, Action = new OutdentAction() },
				new EditActionData() { Category = MiscellaneousCategory, Action = new RemoveTabStopOrOutdentAction() },
				new EditActionData() { Category = MiscellaneousCategory, Action = new ResetZoomLevelAction() },
				new EditActionData() { Category = MiscellaneousCategory, Action = new TabifySelectedLinesAction() },
				new EditActionData() { Category = MiscellaneousCategory, Action = new ToggleCharacterCasingAction() },
				new EditActionData() { Category = MiscellaneousCategory, Action = new ToggleOverwriteModeAction() },
				new EditActionData() { Category = MiscellaneousCategory, Action = new TransposeCharactersAction() },
				new EditActionData() { Category = MiscellaneousCategory, Action = new TransposeLinesAction() },
				new EditActionData() { Category = MiscellaneousCategory, Action = new TransposeWordsAction() },
				new EditActionData() { Category = MiscellaneousCategory, Action = new TrimAllTrailingWhitespaceAction() },
				new EditActionData() { Category = MiscellaneousCategory, Action = new TrimTrailingWhitespaceAction() },
				new EditActionData() { Category = MiscellaneousCategory, Action = new UncommentLinesAction() },
				new EditActionData() { Category = MiscellaneousCategory, Action = new UntabifySelectedLinesAction() },
				new EditActionData() { Category = MiscellaneousCategory, Action = new ZoomInAction() },
				new EditActionData() { Category = MiscellaneousCategory, Action = new ZoomOutAction() },
				// Movement
				new EditActionData() { Category = MovementCategory, Action = new MoveDownAction() },
				new EditActionData() { Category = MovementCategory, Action = new MoveLeftAction() },
				new EditActionData() { Category = MovementCategory, Action = new MovePageDownAction() },
				new EditActionData() { Category = MovementCategory, Action = new MovePageUpAction() },
				new EditActionData() { Category = MovementCategory, Action = new MoveRightAction() },
				new EditActionData() { Category = MovementCategory, Action = new MoveToDocumentEndAction() },
				new EditActionData() { Category = MovementCategory, Action = new MoveToDocumentStartAction() },
				new EditActionData() { Category = MovementCategory, Action = new MoveToLineEndAction() },
				new EditActionData() { Category = MovementCategory, Action = new MoveToLineStartAction() },
				new EditActionData() { Category = MovementCategory, Action = new MoveToLineStartAfterIndentationAction() },
				new EditActionData() { Category = MovementCategory, Action = new MoveToMatchingBracketAction() },
				new EditActionData() { Category = MovementCategory, Action = new MoveToNextLineStartAfterIndentationAction() },
				new EditActionData() { Category = MovementCategory, Action = new MoveToNextWordAction() },
				new EditActionData() { Category = MovementCategory, Action = new MoveToPreviousLineStartAfterIndentationAction() },
				new EditActionData() { Category = MovementCategory, Action = new MoveToPreviousWordAction() },
				new EditActionData() { Category = MovementCategory, Action = new MoveToVisibleBottomAction() },
				new EditActionData() { Category = MovementCategory, Action = new MoveToVisibleTopAction() },
				new EditActionData() { Category = MovementCategory, Action = new MoveUpAction() },
				// Scroll
				new EditActionData() { Category = ScrollCategory, Action = new ScrollDownAction() },
				new EditActionData() { Category = ScrollCategory, Action = new ScrollLeftAction() },
				new EditActionData() { Category = ScrollCategory, Action = new ScrollLineToVisibleBottomAction() },
				new EditActionData() { Category = ScrollCategory, Action = new ScrollLineToVisibleMiddleAction() },
				new EditActionData() { Category = ScrollCategory, Action = new ScrollLineToVisibleTopAction() },
				new EditActionData() { Category = ScrollCategory, Action = new ScrollPageDownAction() },
				new EditActionData() { Category = ScrollCategory, Action = new ScrollPageUpAction() },
				new EditActionData() { Category = ScrollCategory, Action = new ScrollRightAction() },
				new EditActionData() { Category = ScrollCategory, Action = new ScrollToDocumentEndAction() },
				new EditActionData() { Category = ScrollCategory, Action = new ScrollToDocumentStartAction() },
				new EditActionData() { Category = ScrollCategory, Action = new ScrollUpAction() },
				// Search
				new EditActionData() { Category = SearchCategory, Action = new FindAction() },
				new EditActionData() { Category = SearchCategory, Action = new FindNextAction() },
				new EditActionData() { Category = SearchCategory, Action = new FindNextSelectedAction() },
				new EditActionData() { Category = SearchCategory, Action = new FindPreviousAction() },
				new EditActionData() { Category = SearchCategory, Action = new FindPreviousSelectedAction() },
				new EditActionData() { Category = SearchCategory, Action = new IncrementalSearchAction() },
				new EditActionData() { Category = SearchCategory, Action = new ReplaceAction() },
				new EditActionData() { Category = SearchCategory, Action = new ReverseIncrementalSearchAction() },
				// Selection
				new EditActionData() { Category = SelectionCategory, Action = new CodeBlockSelectionContractAction() },
				new EditActionData() { Category = SelectionCategory, Action = new CodeBlockSelectionExpandAction() },
				new EditActionData() { Category = SelectionCategory, Action = new CollapseSelectionAction() },
				new EditActionData() { Category = SelectionCategory, Action = new CollapseSelectionLeftAction() },
				new EditActionData() { Category = SelectionCategory, Action = new CollapseSelectionRightAction() },
				new EditActionData() { Category = SelectionCategory, Action = new SelectAllAction() },
				new EditActionData() { Category = SelectionCategory, Action = new SelectBlockDownAction() },
				new EditActionData() { Category = SelectionCategory, Action = new SelectBlockLeftAction() },
				new EditActionData() { Category = SelectionCategory, Action = new SelectBlockRightAction() },
				new EditActionData() { Category = SelectionCategory, Action = new SelectBlockToNextWordAction() },
				new EditActionData() { Category = SelectionCategory, Action = new SelectBlockToPreviousWordAction() },
				new EditActionData() { Category = SelectionCategory, Action = new SelectBlockUpAction() },
				new EditActionData() { Category = SelectionCategory, Action = new SelectDownAction() },
				new EditActionData() { Category = SelectionCategory, Action = new SelectLeftAction() },
				new EditActionData() { Category = SelectionCategory, Action = new SelectPageDownAction() },
				new EditActionData() { Category = SelectionCategory, Action = new SelectPageUpAction() },
				new EditActionData() { Category = SelectionCategory, Action = new SelectRightAction() },
				new EditActionData() { Category = SelectionCategory, Action = new SelectToDocumentEndAction() },
				new EditActionData() { Category = SelectionCategory, Action = new SelectToDocumentStartAction() },
				new EditActionData() { Category = SelectionCategory, Action = new SelectToLineEndAction() },
				new EditActionData() { Category = SelectionCategory, Action = new SelectToLineStartAction() },
				new EditActionData() { Category = SelectionCategory, Action = new SelectToLineStartAfterIndentationAction() },
				new EditActionData() { Category = SelectionCategory, Action = new SelectToMatchingBracketAction() },
				new EditActionData() { Category = SelectionCategory, Action = new SelectToNextWordAction() },
				new EditActionData() { Category = SelectionCategory, Action = new SelectToPreviousWordAction() },
				new EditActionData() { Category = SelectionCategory, Action = new SelectToVisibleBottomAction() },
				new EditActionData() { Category = SelectionCategory, Action = new SelectToVisibleTopAction() },
				new EditActionData() { Category = SelectionCategory, Action = new SelectUpAction() },
				new EditActionData() { Category = SelectionCategory, Action = new SelectWordAction() },
			};

			// Find the default binding for each action
			foreach (EditActionData actionData in actionDataArray) {
				foreach (InputBinding binding in editor.InputBindings) {
					KeyBinding keyBinding = binding as KeyBinding;
					if (keyBinding != null) {
						IEditAction command = binding.Command as IEditAction;
						if ((command != null) && (command.Key == actionData.Name)) {
							actionData.Key = EditActionBase.GetKeyText(keyBinding.Modifiers, keyBinding.Key);
							break;
						}
					}
				}
			}

			// Create a collection view source
			ListCollectionView source = new ListCollectionView(actionDataArray);
			source.GroupDescriptions.Add(new PropertyGroupDescription("Category"));

			// Set list items source
			editActionsListView.ItemsSource = source;
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnBindCustomAction(object sender, RoutedEventArgs e) {
			// Unbind
			this.UnbindCustomEditAction();

			// Add a command binding to action
			var command = new CustomAction();
			customCommandBinding = command.CreateCommandBinding();
			editor.CommandBindings.Insert(0, customCommandBinding);

			// Bind to Ctrl+P
			customInputBinding = new KeyBinding(command, Key.P, ModifierKeys.Control);
			editor.InputBindings.Add(customInputBinding);

			// Notify user
			MessageBox.Show("Bound Ctrl+P to custom edit action.", "Result", MessageBoxButton.OK, MessageBoxImage.Information);
			editor.Focus();
		}

		/// <summary>
		/// Occurs when a mouse is double-clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="MouseButtonEventArgs"/> that contains the event data.</param>
		private void OnEditActionListViewDoubleClick(object sender, MouseButtonEventArgs e) {
			ListBox listBox = (ListBox)sender;
			EditActionData actionData = listBox.SelectedItem as EditActionData;
			if (actionData != null) {
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
		
		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnExecuteCustomAction(object sender, RoutedEventArgs e) {
			editor.Focus();
			new CustomAction().Execute(editor.ActiveView);
		}
		
		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnUnbindCustomAction(object sender, RoutedEventArgs e) {
			// Unbind
			this.UnbindCustomEditAction();

			// Notify user
			MessageBox.Show("Unbound Ctrl+P.", "Result", MessageBoxButton.OK, MessageBoxImage.Information);
			editor.Focus();
		}

		/// <summary>
		/// Unbinds the custom edit action.
		/// </summary>
		private void UnbindCustomEditAction() {
			if (customCommandBinding != null) {
				editor.CommandBindings.Remove(customCommandBinding);
				customCommandBinding = null;
			}
			if (customInputBinding != null) {
				editor.InputBindings.Remove(customInputBinding);
				customInputBinding = null;
			}
		}

    }

}