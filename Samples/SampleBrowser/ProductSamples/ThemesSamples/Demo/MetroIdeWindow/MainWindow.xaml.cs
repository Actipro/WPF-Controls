using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Media;
using ActiproSoftware.Windows.Themes;
using System.Windows.Media.Imaging;

namespace ActiproSoftware.ProductSamples.ThemesSamples.QuickStart.MetroIdeWindow;

/// <summary>
/// Provides the main window for this sample.
/// </summary>
public partial class MainWindow {

	private int _colorIndex = 0;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainWindow() {
		// Force a Metro theme for this sample
		if (ThemeManager.CurrentTheme?.StartsWith("Metro") != true) {
			ThemeManager.UnregisterAutomaticThemes();
			ThemeManager.CurrentTheme = ThemeNames.MetroLight;
		}

		InitializeComponent();

		// Set up the title bar icon so that it matches the foreground
		var iconSource = new BitmapImage(new Uri("/Images/Icons/ActiproSwoosh24.png", UriKind.RelativeOrAbsolute));
		ImageProvider.SetProvider(iconSource, new ImageProvider() {
			DesignForegroundColor = Color.FromRgb(0x40, 0x40, 0x40)
		});
		chrome.IconSource = iconSource;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Change the color of the status bar.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnStatusBarColorButtonClick(object sender, RoutedEventArgs e) {
		var barColors = new Color[] {
			Color.FromRgb(1, 119, 206),
			Color.FromRgb(14, 99, 156),
			Color.FromRgb(104, 33, 122),
			Color.FromRgb(202, 81, 0)
		};

		statusBar.Background = new SolidColorBrush(barColors[++_colorIndex % 4]);
		BorderBrush = statusBar.Background;
	}

	private void OnSyntaxEditorIsOverwriteModeActiveChanged(object sender, RoutedEventArgs e) {
		// Update the overwrite mode in the statusbar
		overwriteModePanel.Content = (editor.IsOverwriteModeActive ? "OVR" : "INS");
	}

	private void OnSyntaxEditorViewSelectionChanged(object sender, EditorViewSelectionEventArgs e) {
		// Quit if this event is not for the active view
		if (!e.View.IsActive)
			return;

		// Update line, col, and character display in the statusbar
		linePanel.Text = string.Format("Ln {0}", e.CaretPosition.DisplayLine);
		columnPanel.Text = string.Format("Col {0}", e.CaretDisplayCharacterColumn);
		characterPanel.Text = string.Format("Ch {0}", e.CaretPosition.DisplayCharacter);
	}

}
