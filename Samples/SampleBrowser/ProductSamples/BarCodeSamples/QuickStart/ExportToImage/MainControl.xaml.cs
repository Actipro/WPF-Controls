using ActiproSoftware.Windows.Controls.BarCode;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace ActiproSoftware.ProductSamples.BarCodeSamples.QuickStart.ExportToImage {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			this.GenerateImage();
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a <see cref="BitmapSource"/> for the specified symbology.
		/// </summary>
		/// <param name="symbology">The <see cref="LinearBarCodeSymbology"/> for which to create a bitmap.</param>
		/// <returns>The <see cref="BitmapSource"/> that was created.</returns>
		private BitmapSource CreateBitmap(LinearBarCodeSymbology symbology) {
			// Validate the value
			var result = symbology.ValidateValue(this.Value);
			if (!result.IsValid) {
				MessageBox.Show(result.ErrorContent.ToString());
				return null;
			}

			// Build the bar code
			symbology.ValueDisplayStyle = this.ValueDisplayStyle;
			symbology.Value = this.Value;
			return symbology.ToBitmap(96, 96);
		}

		/// <summary>
		/// Generates an image.
		/// </summary>
		private void GenerateImage() {
			outputImage.Source = this.CreateBitmap(new Code39ExtendedSymbology());
			saveImageButton.IsEnabled = ((!BrowserInteropHelper.IsBrowserHosted) && (outputImage.Source != null));
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		public void OnGenerateImageButtonClick(object sender, RoutedEventArgs e) {
			this.GenerateImage();
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		public void OnSaveImageButtonClick(object sender, RoutedEventArgs e) {
			// Show a save dialog
			SaveFileDialog dialog = new SaveFileDialog();
			dialog.CheckPathExists = true;
			dialog.Title = "Save .PNG Image";
			dialog.Filter = "Image files (*.png)|*.png";
			dialog.OverwritePrompt = true;
			if (dialog.ShowDialog() == true) {
				// Write the PNG file... use different encoders to output file types like BMP, GIF, JPEG, TIFF, etc.
				using (FileStream outStream = new FileStream(dialog.FileName, FileMode.Create)) {
					PngBitmapEncoder enc = new PngBitmapEncoder();
					enc.Frames.Add(BitmapFrame.Create((BitmapSource)outputImage.Source));
					enc.Save(outStream);
				}
			}
		}

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>The value.</value>
		public string Value { get; set; } = "ABC-123";

		/// <summary>
		/// Gets or sets the display style.
		/// </summary>
		/// <value>The display style.</value>
		public LinearBarCodeValueDisplayStyle ValueDisplayStyle { get; set; } = LinearBarCodeValueDisplayStyle.Centered;

	}

}