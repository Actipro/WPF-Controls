using ActiproSoftware.Windows.Controls.Gauge.Primitives;
using Microsoft.Win32;
using System.Windows.Media.Imaging;

namespace ActiproSoftware.ProductSamples.GaugeSamples.QuickStart.ExportToImage;

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
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates a <see cref="BitmapSource"/> for the specified gauge.
	/// </summary>
	/// <param name="gauge">The <see cref="GaugeBase"/> for which to create a bitmap.</param>
	private static BitmapSource? CreateBitmap(GaugeBase gauge)
		=> gauge.ToBitmap(dpiX: 96, dpiY: 96);

	private void OnGenerateImageButtonClick(object? sender, RoutedEventArgs e) {
		outputImage.Source = CreateBitmap(gauge);
		saveImageButton.IsEnabled = (outputImage.Source is not null);
	}

	private void OnSaveImageButtonClick(object? sender, RoutedEventArgs e) {
		// Show a save dialog
		var dialog = new SaveFileDialog {
			CheckPathExists = true,
			Title = "Save .PNG Image",
			Filter = "Image files (*.png)|*.png",
			OverwritePrompt = true
		};
		if (dialog.ShowDialog() == true) {
			// Write the PNG file... use different encoders to output file types like BMP, GIF, JPEG, TIFF, etc.
			using (var outStream = new FileStream(dialog.FileName, FileMode.Create)) {
				var enc = new PngBitmapEncoder();
				enc.Frames.Add(BitmapFrame.Create((BitmapSource)outputImage.Source));
				enc.Save(outStream);
			}
		}
	}

}
