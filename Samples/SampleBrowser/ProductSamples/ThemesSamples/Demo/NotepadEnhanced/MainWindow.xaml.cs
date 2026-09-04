using ActiproSoftware.Windows.Controls.Docking;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace ActiproSoftware.ProductSamples.ThemesSamples.Demo.NotepadEnhanced;

/// <summary>
/// Provides the main window for this sample.
/// </summary>
public partial class MainWindow {

	private int _documentIndex = 1;
	private TextDocumentWindow? _primaryDocumentWindow;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainWindow() {
		InitializeComponent();

		// Create an initial document
		Dispatcher.BeginInvoke(DispatcherPriority.Input, () => {
			CreateTextDocumentWindow(fileName: null).Text = @"This demo shows how an enhanced Notepad-like application can be
created with cohesive visual themes and additional functionality.

It is comprised of a combination of Actipro-themed native WPF controls and
Actipro products such as Docking/MDI and SyntaxEditor.

Actipro Themes uses the same common brush resource pool for its native WPF
control styles and custom control styles.  Thus no matter which native or
Actipro controls you use together, the appearance will consistently look great.
";
		});
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates a new text <see cref="DocumentWindow"/>.
	/// </summary>
	/// <param name="fileName">The filename to open; <c>null</c> to create a new document.</param>
	private TextDocumentWindow CreateTextDocumentWindow(string? fileName) {
		string title;
		string text;
		if (fileName is not null) {
			// Open an existing document
			title = Path.GetFileName(fileName);
			text = File.ReadAllText(fileName);
		}
		else {
			// Create a new document
			title = string.Format("Document{0}.txt", _documentIndex);
			text = string.Format("Document {0} created at {1}.", _documentIndex, DateTime.Now);
			_documentIndex++;
		}

		// Create the document
		var documentWindow = new TextDocumentWindow {
			Description = "Text document",
			FileName = fileName,
			ImageSource = new BitmapImage(new Uri("/Images/Icons/TextDocument16.png", UriKind.Relative)),
			Title = title,
			Text = text
		};
		dockSite.DocumentWindows.Add(documentWindow);

		// Activate the document
		documentWindow.Activate();

		return documentWindow;
	}

	private void OnCascadeMenuItemClick(object sender, RoutedEventArgs e)
		=> tabbedMdiHost.Cascade();

	private void OnDockSitePrimaryDocumentChanged(object sender, DockingWindowEventArgs e)
		=> PrimaryDocumentWindow = (dockSite.PrimaryDocument as TextDocumentWindow);

	private void OnExitMenuItemClick(object sender, RoutedEventArgs e)
		=> Close();

	private void OnNewFileMenuItemClick(object sender, RoutedEventArgs e) {
		// Create a new document window
		CreateTextDocumentWindow(fileName: null);
	}

	private void OnOpenFileMenuItemClick(object sender, RoutedEventArgs e) {
		// Show a file open dialog
		var dialog = new OpenFileDialog {
			CheckFileExists = true,
			Multiselect = false,
			Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
		};
		if (dialog.ShowDialog() == true) {
			// Create a document window
			CreateTextDocumentWindow(dialog.FileName);
		}
	}

	private void OnTileHorizontallyMenuItemClick(object sender, RoutedEventArgs e)
		=> tabbedMdiHost.TileHorizontally();

	private void OnTileVerticallyMenuItemClick(object sender, RoutedEventArgs e)
		=> tabbedMdiHost.TileVertically();

	private void OnViewSelectionChanged(object? sender, EditorViewSelectionEventArgs e) {
		// Quit if this event is not for the active view
		if (!e.View.IsActive)
			return;

		// Update line, col, and character display
		UpdateStatusBarTextLocation(e.CaretPosition.DisplayLine, e.CaretDisplayCharacterColumn, e.CaretPosition.DisplayCharacter);
	}

	/// <summary>
	/// The primary document window.
	/// </summary>
	private TextDocumentWindow? PrimaryDocumentWindow {
		get => _primaryDocumentWindow;
		set {
			if (_primaryDocumentWindow == value)
				return;

			if (_primaryDocumentWindow is not null)
				_primaryDocumentWindow.ViewSelectionChanged -= OnViewSelectionChanged;

			_primaryDocumentWindow = value;

			if (_primaryDocumentWindow is not null) {
				_primaryDocumentWindow.ViewSelectionChanged += OnViewSelectionChanged;
				messagePanel.Content = _primaryDocumentWindow.FileName ?? "Ready";
				UpdateStatusBarTextLocation(
					_primaryDocumentWindow.CaretPosition.DisplayLine,
					_primaryDocumentWindow.CaretColumn,
					_primaryDocumentWindow.CaretPosition.DisplayCharacter
				);
			}
			else {
				messagePanel.Content = "Ready";
				UpdateStatusBarTextLocation(line: null, column: null, character: null);
			}
		}
	}

	/// <summary>
	/// Updates line, column, and character display.
	/// </summary>
	/// <param name="line">The line value.</param>
	/// <param name="column">The column value.</param>
	/// <param name="character">The character value.</param>
	private void UpdateStatusBarTextLocation(int? line, int? column, int? character) {
		linePanel.Text = (line.HasValue ? string.Format("Ln {0}", line) : null);
		columnPanel.Text = (column.HasValue ? string.Format("Col {0}", column) : null);
		characterPanel.Text = (character.HasValue ? string.Format("Ch {0}", character) : null);
	}

}
