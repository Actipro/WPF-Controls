using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using System;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.SerializationMvvm {

	/// <summary>
	/// Defines a manager for working with the controls used by Ribbon and related menus.
	/// </summary>
	public class SampleBarManager {

		private readonly ISampleCommands sampleCommands;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="SampleBarManager"/> class.
		/// </summary>
		public SampleBarManager(ISampleCommands sampleCommands) {
			this.sampleCommands = sampleCommands ?? throw new ArgumentNullException(nameof(sampleCommands));

			// Initialize view model collection
			this.ControlViewModels = new BarControlViewModelCollection(ImageProvider);

			// Register common images used by view models
			RegisterImages();

			// Register view models for controls
			RegisterControlViewModels();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates an <see cref="ImageSource"/> based on the given options.
		/// </summary>
		/// <param name="options">The options which define the image to be created.</param>
		/// <param name="smallFileName">The name of the resource file that represents the small image (e.g., 16x16).</param>
		/// <param name="largeFileName">The name of the resource file that represents the large image (e.g., 32x32).</param>
		/// <returns>An <see cref="ImageSource"/> based on the given options, or <c>null</c> if a corresponding image is not available.</returns>
		/// <seealso cref="RegisterImages"/>
		private static ImageSource CreateBitmapImage(BarImageOptions options, string smallFileName, string largeFileName) {
			// Determine the name of the resource file that is appropriate for the requested image size
			string fileNameResolved = null;
			switch (options.Size) {
				case BarImageSize.Small:
					fileNameResolved = smallFileName;
					break;
				case BarImageSize.Medium:
					// Medium images not supported by this sample
					break;
				case BarImageSize.Large:
					fileNameResolved = largeFileName;
					break;
			}
			if (!string.IsNullOrEmpty(fileNameResolved))
				return ImageLoader.GetIcon(fileNameResolved);

			return null;
		}

		/// <summary>
		/// Registers control view models with <see cref="ControlViewModels"/>.
		/// </summary>
		private void RegisterControlViewModels() {

			ControlViewModels.Register(SampleBarControlKeys.Copy, key
				=> new BarButtonViewModel(key, ApplicationCommands.Copy) { Description = "Puts a copy of the selection on the Clipboard so you can paste it elsewhere." });

			ControlViewModels.Register(SampleBarControlKeys.Cut, key
				=> new BarButtonViewModel(key, ApplicationCommands.Cut) { Description = "Removes the selection and puts it on the Clipboard so you can paste it elsewhere." });

			ControlViewModels.Register(SampleBarControlKeys.Paste, key
				=> new BarButtonViewModel(key, ApplicationCommands.Paste) { Description = "Adds content from the Clipboard into your document." });

			ControlViewModels.Register(SampleBarControlKeys.SaveLayout, key
				=> new BarButtonViewModel(key, sampleCommands.SaveLayout) { Description = "Serializes the current Ribbon configuration to a string that can be restored later." });

			ControlViewModels.Register(SampleBarControlKeys.RestoreLayout, key
				=> new BarButtonViewModel(key, sampleCommands.RestoreLayout) { Description = "Deserializes a previously saved configuration and applies it to the Ribbon." });

			ControlViewModels.Register(SampleBarControlKeys.RestoreOriginalLayout, key
				=> new BarButtonViewModel(key, "Restore Original", "O", sampleCommands.RestoreOriginalLayout) { Description = "Deserializes the original configuration and applies it to the Ribbon." });
		}

		/// <summary>
		/// Registers images with <see cref="ImageProvider"/>.
		/// </summary>
		private void RegisterImages() {
			ImageProvider.Register(SampleBarControlKeys.Copy, options => CreateBitmapImage(options, "Copy16.png", largeFileName: null));
			ImageProvider.Register(SampleBarControlKeys.Cut, options => CreateBitmapImage(options, "Cut16.png", largeFileName: null));
			ImageProvider.Register(SampleBarControlKeys.Paste, options => CreateBitmapImage(options, "Paste16.png", "Paste32.png"));
			ImageProvider.Register(SampleBarControlKeys.SaveLayout, options => CreateBitmapImage(options, "Save16.png", "Save32.png"));
			ImageProvider.Register(SampleBarControlKeys.RestoreLayout, options => CreateBitmapImage(options, "Open16.png", "Open32.png"));
			ImageProvider.Register(SampleBarControlKeys.RestoreOriginalLayout, options => CreateBitmapImage(options, "OpenMono16.png", "OpenMono32.png"));
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the collection of control view models.
		/// </summary>
		/// <value>A <see cref="BarControlViewModelCollection"/>.</value>
		public BarControlViewModelCollection ControlViewModels { get; }

		/// <summary>
		/// Gets the provider that will be used to provide images.
		/// </summary>
		/// <value>A <see cref="BarImageProvider"/>.</value>
		public BarImageProvider ImageProvider { get; } = new BarImageProvider();
	}

}
