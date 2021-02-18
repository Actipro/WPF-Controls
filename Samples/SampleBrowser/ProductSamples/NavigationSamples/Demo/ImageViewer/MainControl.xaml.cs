using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace ActiproSoftware.ProductSamples.NavigationSamples.Demo.ImageViewer {

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

			// Add command bindings
			this.CommandBindings.Add(new CommandBinding(System.Windows.Input.ApplicationCommands.Open, fileOpenCommand_Execute));
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// COMMAND HANDLERS
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private void fileOpenCommand_Execute(object sender, ExecutedRoutedEventArgs e) {
			// Open a document
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "Images Files (*.bmp; *.gif; *.jpg; *.jpeg; *.png; *.tif; *.tiff)|*.bmp;*.gif;*.jpg;*.jpeg;*.png;*.tif;*.tiff";
			if (dialog.ShowDialog() == true) {
				ImageSource imageSource = null;
				try {
					BitmapDecoder decoder = BitmapDecoder.Create(dialog.OpenFile(), BitmapCreateOptions.None, BitmapCacheOption.None);
					imageSource = decoder.Frames[0];
				}
				catch (Exception) {
					// No-op
				}

				if (null == imageSource) {
					MessageBox.Show("Unable to open image file.", "ZoomContentControl", MessageBoxButton.OK, MessageBoxImage.Error);
					return;
				}

				this.zoomContentControl.BeginUpdate();
				try {
					this.image.Source = imageSource;
					this.zoomContentControl.UpdateLayout();
					this.zoomContentControl.ZoomToFit();
				}
				finally {
					this.zoomContentControl.EndUpdate(false);
				}
			}
		}
	}
}