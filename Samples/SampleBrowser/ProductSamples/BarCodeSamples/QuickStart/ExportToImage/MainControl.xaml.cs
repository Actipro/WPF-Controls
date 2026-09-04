using ActiproSoftware.Windows.Controls.BarCode;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.BarCodeSamples.QuickStart.ExportToImage;

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

		GenerateImage();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates a <see cref="BitmapSource"/> for the specified symbology.
	/// </summary>
	/// <param name="symbology">The <see cref="LinearBarCodeSymbology"/> for which to create a bitmap.</param>
	private BitmapSource? CreateBitmap(LinearBarCodeSymbology symbology) {
		// Validate the value
		var result = symbology.ValidateValue(Value);
		if (!result.IsValid) {
			MessageBox.Show(result.ErrorContent.ToString()!);
			return null;
		}

		// Build the bar code
		symbology.ValueDisplayStyle = ValueDisplayStyle;
		symbology.Value = Value;
		return symbology.ToBitmap(96, 96);
	}

	/// <summary>
	/// Generates an image.
	/// </summary>
	private void GenerateImage() {
		outputImage.Source = CreateBitmap(new Code39ExtendedSymbology());
		saveImageButton.IsEnabled = outputImage.Source is not null;
	}

	private void OnGenerateImageButtonClick(object? sender, RoutedEventArgs e)
		=> GenerateImage();

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

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The value.
	/// </summary>
	public string Value { get; set; } = "ABC-123";

	/// <summary>
	/// The display style.
	/// </summary>
	public LinearBarCodeValueDisplayStyle ValueDisplayStyle { get; set; } = LinearBarCodeValueDisplayStyle.Centered;

}
