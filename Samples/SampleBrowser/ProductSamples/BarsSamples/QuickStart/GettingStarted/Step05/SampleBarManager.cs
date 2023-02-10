/*

RIBBON GETTING STARTED SERIES - STEP 5

STEP SUMMARY:

	Expand the Ribbon configure to include new view models for commands.

CHANGES SINCE LAST STEP:

	Added a "NotImplementedCommand" implementation to be associated with commands
	that will not perform real logic in the application.

	Added control view models and registered images for Undo/Redo and Cut/Copy/Paste
	commands.

	Registered images for Undo and Clipboard control groups.

*/

using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.Input;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.GettingStarted.Step05 {

	/// <summary>
	/// Defines a manager for working with the controls used by Ribbon and related menus.
	/// </summary>
	public class SampleBarManager {

		private static ICommand notImplementedCommand;

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
		/// Gets a command that will display a message that the logic for the command has yet to be implemented.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		internal static ICommand NotImplementedCommand {
			//	SAMPLE NOTE 5.1:
			//		This sample command is associated with view models that will appear in the Ribbon
			//		for demonstration purposes only. This command is used instead of NULL or
			//		ApplicationCommands.NotACommand so the UI is enabled and responds to interaction.
			get {
				if (notImplementedCommand is null) {
					notImplementedCommand = new DelegateCommand<object>(
						(param) => {
							ThemedMessageBox.Show(
								"This control is for user interface demonstration purposes only and no application functionality has been implemented for it.",
								"Not Implemented",
								MessageBoxButton.OK, MessageBoxImage.Information);
						}
					);
				}
				return notImplementedCommand;
			}
		}

		/// <summary>
		/// Registers control view models with <see cref="ControlViewModels"/>.
		/// </summary>
		private void RegisterControlViewModels() {

			//	SAMPLE NOTE 5.2:
			//		Several new view models have been added in this step and are designated with the "(New)" comment.

			/*(New)*/ ControlViewModels.Register(SampleBarControlKeys.Copy, key
				=> new BarButtonViewModel(key, ApplicationCommands.Copy) { Description = "Puts a copy of the selection on the Clipboard so you can paste it elsewhere." });

			/*(New)*/ ControlViewModels.Register(SampleBarControlKeys.Cut, key
				=> new BarButtonViewModel(key, ApplicationCommands.Cut) { Description = "Removes the selection and puts it on the Clipboard so you can paste it elsewhere." });

			ControlViewModels.Register(SampleBarControlKeys.Help, key
				=> new BarButtonViewModel(key, ApplicationCommands.Help) { Description = "Open application help." });

			/*(New)*/ ControlViewModels.Register(SampleBarControlKeys.Paste, key
				=> new BarButtonViewModel(key, ApplicationCommands.Paste) { Description = "Adds content from the Clipboard into your document." });

			//	SAMPLE NOTE 5.3:
			//		This Paste Menu is a split button. The primary command is "Paste", but it also has a popup menu
			//		with an additional command for "Paste Special". The following note was included in Step 3:
			//
			//			Control models are not created by BarControlViewModelCollection during registration. Only the
			//			factory method is associated with a key. When a view model is requested for the first time,
			//			it will be created as needed by invoking the registered factory method. This is particularly
			//			useful for menus or popup buttons whose view model must also populate a collection of child
			//			view models. Since all view models are registered before any view model is created, the
			//			factory method of a view model can safely refer to other view models.
			//
			//		Note that the "Paste Special" view model has yet to be registered, but it can still be accessed
			//		in the factory method for the "Paste" view model since the factory method is not executed
			//		until all registrations are complete.

			/*(New)*/ ControlViewModels.Register(SampleBarControlKeys.PasteMenu, key
				=> new BarSplitButtonViewModel(key, "Paste", ApplicationCommands.Paste) {
					Description = "Adds content from the Clipboard into your document.",
					MenuItems = {
						ControlViewModels[SampleBarControlKeys.Paste],
						ControlViewModels[SampleBarControlKeys.PasteSpecial],
					}
				});

			//	SAMPLE NOTE 5.4:
			//		This sample application will add several view models to the Ribbon to demonstrate Ribbon configuration,
			//		but the actual commands will not be implemented. The "Paste Special" view model is just one example
			//		of this. These view models will be associated with a custom "NotImplementedCommand" so that the
			//		commands appear enabled in the UI and respond to interaction.

			/*(New)*/ ControlViewModels.Register(SampleBarControlKeys.PasteSpecial, key
				=> new BarButtonViewModel(key, NotImplementedCommand) { KeyTipText = "S" });

			/*(New)*/ ControlViewModels.Register(SampleBarControlKeys.Redo, key
				=> new BarButtonViewModel(key, ApplicationCommands.Redo) { KeyTipText = "AQ", Description = "Redo the last operation." });

			/*(New)*/ ControlViewModels.Register(SampleBarControlKeys.Undo, key
				=> new BarButtonViewModel(key, ApplicationCommands.Undo) { KeyTipText = "AZ", Description = "Undo the last operation." });
		}

		/// <summary>
		/// Registers images with <see cref="ImageProvider"/>.
		/// </summary>
		private void RegisterImages() {
			//	SAMPLE NOTE 5.5:
			//		Several of the commands in this sample do not register large images, only small images.
			//		Most applications will want to register all supported image variants. For the purpose
			//		of this sample application, the commands without large images will be used in
			//		configurations where only small images are necessary.

			/*(New)*/ ImageProvider.Register(SampleBarControlKeys.Copy, options => CreateBitmapImage(options, "Copy16.png", largeFileName: null));
			/*(New)*/ ImageProvider.Register(SampleBarControlKeys.Cut, options => CreateBitmapImage(options, "Cut16.png", largeFileName: null));
			/*(New)*/ ImageProvider.Register(SampleBarControlKeys.GroupClipboard, options => CreateBitmapImage(options, "Paste16.png", largeFileName: null));
			/*(New)*/ ImageProvider.Register(SampleBarControlKeys.GroupUndo, options => CreateBitmapImage(options, "Undo16.png", largeFileName: null));
			ImageProvider.Register(SampleBarControlKeys.Help, options => CreateBitmapImage(options, "Help16.png", "Help32.png"));
			/*(New)*/ ImageProvider.Register(SampleBarControlKeys.Paste, options => CreateBitmapImage(options, "Paste16.png", "Paste32.png"));
			/*(New)*/ ImageProvider.Register(SampleBarControlKeys.PasteMenu, options => CreateBitmapImage(options, "Paste16.png", "Paste32.png"));
			/*(New)*/ ImageProvider.Register(SampleBarControlKeys.PasteSpecial, options => CreateBitmapImage(options, "PasteSpecial16.png", largeFileName: null));
			/*(New)*/ ImageProvider.Register(SampleBarControlKeys.Redo, options => CreateBitmapImage(options, "Redo16.png", largeFileName: null));
			/*(New)*/ ImageProvider.Register(SampleBarControlKeys.Undo, options => CreateBitmapImage(options, "Undo16.png", largeFileName: null));
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
