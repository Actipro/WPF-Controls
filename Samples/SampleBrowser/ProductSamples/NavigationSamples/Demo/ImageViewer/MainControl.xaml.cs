using Microsoft.Win32;
using System.Windows.Media.Imaging;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.NavigationSamples.Demo.ImageViewer;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		// Add command bindings
		CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, OnFileOpenCommandExecute));
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnFileOpenCommandExecute(object sender, ExecutedRoutedEventArgs e) {
		// Open a document
		var dialog = new OpenFileDialog {
			Filter = "Images Files (*.bmp; *.gif; *.jpg; *.jpeg; *.png; *.tif; *.tiff)|*.bmp;*.gif;*.jpg;*.jpeg;*.png;*.tif;*.tiff"
		};
		if (dialog.ShowDialog() == true) {
			ImageSource? imageSource = null;
			try {
				var decoder = BitmapDecoder.Create(dialog.OpenFile(), BitmapCreateOptions.None, BitmapCacheOption.None);
				imageSource = decoder.Frames[0];
			}
			catch (Exception) { } // Ignore

			if (imageSource is null) {
				MessageBox.Show("Unable to open image file.", "ZoomContentControl", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			zoomContentControl.BeginUpdate();
			try {
				image.Source = imageSource;
				zoomContentControl.UpdateLayout();
				zoomContentControl.ZoomToFit();
			}
			finally {
				zoomContentControl.EndUpdate(animate: false);
			}
		}
	}

}
