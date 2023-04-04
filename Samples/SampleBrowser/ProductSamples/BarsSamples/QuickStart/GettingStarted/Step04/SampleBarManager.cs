/*

RIBBON GETTING STARTED SERIES - STEP 4

STEP SUMMARY:

	The method used to construct a BarButtonViewModel was refactored to utilize automatic
	generation of Label and KeyTipText properties.

CHANGES SINCE LAST STEP:

	The constructor for BarButtonViewModel was simplified.

	Prior sample comments were removed/condensed. Some lines were reformatted after comments
	were removed.

*/

using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using System;
using System.Windows.Input;
using System.Windows.Media;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.GettingStarted.Step04 {

	/// <summary>
	/// Defines a manager for working with the controls used by Ribbon and related menus.
	/// </summary>
	public class SampleBarManager {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="SampleBarManager"/> class.
		/// </summary>
		public SampleBarManager() {
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
			//	SAMPLE NOTE 4.1:
			//		Almost all of the view models in the MVVM library can be initialized with just a key.
			//		Some constructors optionally accept a command (shown below) or a combination of label
			//		and key tip text values.
			//
			//		When not explicitly passed, the view model's Label property will be automatically
			//		derived from the given Key, so a Key of "Help" will result in a Label of "Help". Keys
			//		that use mixed character casing like "ExitApplication" will be separated into words
			//		for a label like "Exit Application".
			//
			//		If a RoutedUICommand is passed like this sample, the RoutedUICommand.Text property
			//		will be used as the Label instead of deriving the Label from the Key.
			//
			//		The KeyTipText property is automatically derived from the Label property (after it is
			//		derived from the Key or RoutedUICommand) and is typically defined as the first letter
			//		of the first word.
			//
			//		This sample previously defined a Key of "Help", an explicit Label of "Help", an explicit
			//		KeyTipText of "H". Since the RoutedUICommand of ApplicationCommands.Help has a default
			//		text of "Help" (en-US), both the Label and KeyTipText properties can be automatically
			//		derived and do not need to be explicitly passed.
			//
			//		In many cases, only the Key has to be passed to the view model constructor. When necessary,
			//		the Label and KeyTipText can be explicitly defined if the automatically derived values are
			//		not sufficient.
			//
			//		The Label and KeyTipText property are only automatically populated from the constructor.
			//		Setting the Label property after construction will not alter the KeyTipText property.
			ControlViewModels.Register(SampleBarControlKeys.Help, key
				=> new BarButtonViewModel(key, ApplicationCommands.Help) { Description = "Open application help." });
		}

		/// <summary>
		/// Registers images with <see cref="ImageProvider"/>.
		/// </summary>
		private void RegisterImages() {
			ImageProvider.Register(SampleBarControlKeys.Help, options => CreateBitmapImage(options, "Help16.png", "Help32.png"));
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
