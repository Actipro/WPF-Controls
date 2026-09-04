using ActiproSoftware.Text.Undo;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.UndoRedo;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl : UserControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		redoStack.ItemsSource = editor.Document.UndoHistory.RedoStack;
		undoStack.ItemsSource = editor.Document.UndoHistory.UndoStack;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnAppendButtonClick(object sender, RoutedEventArgs e) {
		editor.Document.AppendText(CustomChangeType.Instance, Environment.NewLine + "Appended with custom change type.");
		editor.Focus();
	}

	private void OnRedoListBoxDoubleClick(object sender, MouseButtonEventArgs e) {
		if (sender is ListBox { SelectedItem: IUndoableTextChange textChange }) {
			var index = editor.Document.UndoHistory.RedoStack.IndexOf(textChange);
			if (index != -1) {
				editor.Document.UndoHistory.Redo(index + 1);
				editor.Focus();
			}
		}
	}

	private void OnUndoListBoxDoubleClick(object sender, MouseButtonEventArgs e) {
		if (sender is ListBox { SelectedItem: IUndoableTextChange textChange }) {
			var index = editor.Document.UndoHistory.UndoStack.IndexOf(textChange);
			if (index != -1) {
				editor.Document.UndoHistory.Undo(index + 1);
				editor.Focus();
			}
		}
	}

}
