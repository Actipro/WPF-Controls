using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Media;
using ActiproSoftware.Windows.Themes;

namespace ActiproSoftware.ProductSamples.ThemesSamples.QuickStart.MetroIdeWindow {

	/// <summary>
	/// Provides the main window for this sample.
	/// </summary>
	public partial class MainWindow {

		private int colorIndex = 0;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainWindow</c> class.
		/// </summary>
		public MainWindow() {
			// Force a Metro theme for this sample
			if (!ThemeManager.CurrentTheme.StartsWith("Metro")) {
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

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Change the color of the status bar.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnStatusBarColorButtonClick(object sender, RoutedEventArgs e) {
			var barColors = new Color[] {
				Color.FromRgb(1, 119, 206), 
				Color.FromRgb(14, 99, 156), 
				Color.FromRgb(104, 33, 122), 
				Color.FromRgb(202, 81, 0)
			};

			statusBar.Background = new SolidColorBrush(barColors[++colorIndex % 4]);
			this.BorderBrush = statusBar.Background;
		}

		/// <summary>
		/// Occurs when the <c>SyntaxEditor.IsOverwriteModeActiveChanged</c> event occurs.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="BooleanPropertyChangedRoutedEventArgs"/> that contains the event data.</param>
		private void OnSyntaxEditorIsOverwriteModeActiveChanged(object sender, RoutedEventArgs e) {
			overwriteModePanel.Content = (editor.IsOverwriteModeActive ? "OVR" : "INS");
		}

		/// <summary>
		/// Occurs when the <c>SyntaxEditor.ViewSelectionChanged</c> event occurs.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="EditorViewSelectionEventArgs"/> that contains the event data.</param>
		private void OnSyntaxEditorViewSelectionChanged(object sender, EditorViewSelectionEventArgs e) {
			// Quit if this event is not for the active view
			if (!e.View.IsActive)
				return;

			// Update line, col, and character display
			linePanel.Text = String.Format("Ln {0}", e.CaretPosition.DisplayLine);
			columnPanel.Text = String.Format("Col {0}", e.CaretDisplayCharacterColumn);
			characterPanel.Text = String.Format("Ch {0}", e.CaretPosition.DisplayCharacter);
		}
		
	}

}