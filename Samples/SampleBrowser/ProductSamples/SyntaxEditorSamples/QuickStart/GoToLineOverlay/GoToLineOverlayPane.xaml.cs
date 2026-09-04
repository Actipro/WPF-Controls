using ActiproSoftware.Extensions;
using ActiproSoftware.Text;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Implementation;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GoToLineOverlay;

/// <summary>
/// Interaction logic for GoToLineOverlayPane.xaml
/// </summary>
public partial class GoToLineOverlayPane : OverlayPaneBase {

	public const string Key = "GoToCustom";

	private bool _isAttachedToViewEvents;
	private readonly IEditorView _view;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="view">The editor view where the overlay pane will be displayed.</param>
	public GoToLineOverlayPane(IEditorView view) : base(Key) {
		_view = view ?? throw new ArgumentNullException(nameof(view));

		InitializeComponent();
		AttachDetachViewEvents(attach: true);
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Attaches to or detaches from target view events.
	/// </summary>
	/// <param name="attach">Whether to attach to events.</param>
	private void AttachDetachViewEvents(bool attach) {
		if (_view is null)
			return;

		if (attach) {
			if (!_isAttachedToViewEvents) {
				_view.SelectionChanged += OnViewSelectionChanged;
				_view.SyntaxEditor.ActiveViewChanged += OnSyntaxEditorActiveViewChanged;
				_isAttachedToViewEvents = true;
			}
		}
		else {  // Detach
			if (_isAttachedToViewEvents) {
				_view.SelectionChanged -= OnViewSelectionChanged;
				_view.SyntaxEditor.ActiveViewChanged -= OnSyntaxEditorActiveViewChanged;
				_isAttachedToViewEvents = false;
			}
		}
	}

	/// <summary>
	/// Navigates the view to the line number defined in the view.
	/// </summary>
	private void GoToLine() {
		// Validate
		if (
			!int.TryParse(lineNumberTextBox.Text, out var lineNumber)
			|| !lineNumber.IsBetween(1, _view.CurrentSnapshot.Lines.Count)
		) {
			MessageBox.Show($"Please enter a valid line number (1-{_view.CurrentSnapshot.Lines.Count}).", "Invalid Line Number", MessageBoxButton.OK, MessageBoxImage.Error);
			return;
		}

		// Set caret position (make zero-based)
		_view.Selection.CaretPosition = new TextPosition(lineNumber - 1, 0);
		_view.Scroller.ScrollLineToVisibleMiddle();

		// Close the pane
		Close();
	}

	private void OnLineNumberTextBoxGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e) {
		// Select all text when focused
		(sender as TextBox)?.SelectAll();
	}

	private void OnGoToLineButtonClick(object sender, RoutedEventArgs e)
		=> GoToLine();

	private void OnSyntaxEditorActiveViewChanged(object? sender, EditorViewChangedEventArgs e) {
		// Close the pane if the attached view is no longer active
		if (e.NewValue != _view)
			Close();
	}

	private void OnViewSelectionChanged(object? sender, EditorViewSelectionEventArgs e) {
		// Close the pane any time selection changes
		Close();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override void Activate() {
		base.Activate();

		// Define the range of valid lines
		lineNumberTextBlock.Text = $"Line number (1 - {_view.CurrentSnapshot.Lines.Count})";

		// Populate the textbox with the current line
		lineNumberTextBox.Text = _view.Selection.EndPosition.DisplayLine.ToString();
		lineNumberTextBox.Focus();
	}

	/// <inheritdoc/>
	public override void Close() {
		// Detach from the view
		AttachDetachViewEvents(attach: false);

		base.Close();
	}

	/// <inheritdoc/>
	protected override bool ProcessKeyDown(ModifierKeys modifierKeys, Key key) {
		switch (modifierKeys) {
			case ModifierKeys.None:
			case ModifierKeys.Shift:
				switch (key) {
					case System.Windows.Input.Key.Enter:
						// Go to line
						GoToLine();
						return true;
				}
				break;
		}

		return base.ProcessKeyDown(modifierKeys, key);
	}

	/// <summary>
	/// Shows the overlay pane within the specified <paramref name="view"/>.
	/// </summary>
	/// <param name="view">The editor view where the overlay pane will be displayed.</param>
	public static void Show(IEditorView view) {
		// Get or create the overlay pane
		var overlayPanes = view.OverlayPanes;
		var goToLinePane = overlayPanes[Key] as GoToLineOverlayPane;
		if (goToLinePane is null) {
			// Close any existing overlay panes before adding a new one
			overlayPanes.Clear();

			// Add a new overlay pane
			goToLinePane = new GoToLineOverlayPane(view);
			overlayPanes.Add(goToLinePane);
		}

		// Activate the pane to initialize the valid line range and focus the textbox
		goToLinePane.Activate();
	}

}
