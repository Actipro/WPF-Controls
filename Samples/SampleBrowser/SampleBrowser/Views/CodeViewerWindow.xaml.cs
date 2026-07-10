using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using System.Windows.Threading;

namespace ActiproSoftware.SampleBrowser;

/// <summary>
/// Provides the code viewer window.
/// </summary>
public partial class CodeViewerWindow {

	private int _updateVersion;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="viewModel">The application view-model.</param>
	public CodeViewerWindow(ApplicationViewModel viewModel) {
		ViewModel = viewModel;

		InitializeComponent();

		// Register SyntaxEditor display item classification types
		new BuiltInClassificationTypeProvider().RegisterAll();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnShellListBoxSelectionChanged(object sender, SelectionChangedEventArgs e) {
		_updateVersion = (_updateVersion + 1) % 1000;
		var requestedUpdateVersion = _updateVersion;

		Dispatcher.BeginInvoke(DispatcherPriority.Background, () => {
			if (_updateVersion == requestedUpdateVersion)
				UpdateSourcePane();
		});
	}

	private void UpdateSourcePane() {
		if (shellListBox.SelectedShellObject is { } selectedShellObject) {
			if (selectedShellObject.IsFolder)
				editorDockPanel.Visibility = Visibility.Collapsed;
			else if (selectedShellObject.ParsingName is not null) {
				// NOTE: Any changes to supported extensions need to be made in CodeViewerTreeFilter as well
				switch (Path.GetExtension(selectedShellObject.ParsingName).ToUpperInvariant()) {
					case ".CS":
						editor.Document.Language = ViewModel.SyntaxLanguageCSharp;
						break;
					case ".XAML":
						editor.Document.Language = ViewModel.SyntaxLanguageXaml;
						break;
					default:
						editor.Document.Language = SyntaxLanguage.PlainText;
						break;
				}

				// Load the file
				try {
					editor.Document.LoadFile(selectedShellObject.ParsingName);
				}
				catch (Exception ex) {
					editor.Document.Language = SyntaxLanguage.PlainText;
					editor.Document.SetText(string.Format("An exception occurred while loading the file '{0}':\r\n\r\n{1}", selectedShellObject.ParsingName, ex.Message));
				}

				editorDockPanel.Visibility = Visibility.Visible;
			}
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void OnClosed(EventArgs e) {
		base.OnClosed(e);

		// Dispose any unmanaged resources held by the shell instances now that the UI is closing
		shellListBox.DisposeShellInstances();
	}

	/// <summary>
	/// The view-model for this view.
	/// </summary>
	public ApplicationViewModel ViewModel {
		get => (ApplicationViewModel)DataContext;
		private set => DataContext = value;
	}

}
