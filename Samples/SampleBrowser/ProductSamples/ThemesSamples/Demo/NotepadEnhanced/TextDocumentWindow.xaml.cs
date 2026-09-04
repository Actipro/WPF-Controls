using ActiproSoftware.Text;
using ActiproSoftware.Windows.Controls.Docking;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.ThemesSamples.Demo.NotepadEnhanced;

/// <summary>
/// Represents a custom <see cref="DocumentWindow"/> implementation.
/// </summary>
public partial class TextDocumentWindow : DocumentWindow {

	// --------------------------------------------------------------------------------------------------
	// EVENTS
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Provides access to the selection changed event.
	/// </summary>
	public event EventHandler<EditorViewSelectionEventArgs> ViewSelectionChanged {
		add => editor.ViewSelectionChanged += value;
		remove => editor.ViewSelectionChanged -= value;
	}

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public TextDocumentWindow() {
		InitializeComponent();

		// Register class command bindings
		CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, OnCloseExecuted));
		CommandBindings.Add(new CommandBinding(ApplicationCommands.Save, OnSaveExecuted));
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnCloseExecuted(object sender, ExecutedRoutedEventArgs e)
		=> Close();

	private void OnSaveExecuted(object sender, ExecutedRoutedEventArgs e)
		=> MessageBox.Show("Save file here.");

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The caret position.
	/// </summary>
	public TextPosition CaretPosition
		=> editor.ActiveView.Selection.CaretPosition;

	/// <summary>
	/// The caret column.
	/// </summary>
	public int CaretColumn
		=> editor.ActiveView.Selection.CaretDisplayCharacterColumn;

	/// <summary>
	/// The document text.
	/// </summary>
	public string Text {
		get => editor.Document.CurrentSnapshot.Text;
		set => editor.Document.SetText(value);
	}

}
