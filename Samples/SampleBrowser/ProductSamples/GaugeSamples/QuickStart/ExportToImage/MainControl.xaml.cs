using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using ActiproSoftware.Windows.Controls.Gauge.Primitives;

namespace ActiproSoftware.ProductSamples.GaugeSamples.QuickStart.ExportToImage {

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
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a <see cref="BitmapSource"/> for the specified symbology.
		/// </summary>
		/// <param name="gauge">The <see cref="GaugeBase"/> for which to create a bitmap.</param>
		/// <returns>The <see cref="BitmapSource"/> that was created.</returns>
		private BitmapSource CreateBitmap(GaugeBase gauge) {
			return gauge.ToBitmap(96, 96);
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
			outputImage.Source = this.CreateBitmap(this.gauge);
			saveImageButton.IsEnabled = ((!BrowserInteropHelper.IsBrowserHosted) && (outputImage.Source != null));
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
	}
}