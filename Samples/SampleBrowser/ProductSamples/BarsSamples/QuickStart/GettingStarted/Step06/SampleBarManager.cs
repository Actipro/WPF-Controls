/*

RIBBON GETTING STARTED SERIES - STEP 6

STEP SUMMARY:

	Add support for a "SelectAll" command.

CHANGES SINCE LAST STEP:

	Added control view model and registered image for SelectAll command.

	Prior sample comments were removed/condensed.

 */

using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.Input;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.GettingStarted.Step06 {

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

			ControlViewModels.Register(SampleBarControlKeys.Copy, key
				=> new BarButtonViewModel(key, ApplicationCommands.Copy) { Description = "Puts a copy of the selection on the Clipboard so you can paste it elsewhere." });

			ControlViewModels.Register(SampleBarControlKeys.Cut, key
				=> new BarButtonViewModel(key, ApplicationCommands.Cut) { Description = "Removes the selection and puts it on the Clipboard so you can paste it elsewhere." });

			ControlViewModels.Register(SampleBarControlKeys.Help, key
				=> new BarButtonViewModel(key, ApplicationCommands.Help) { Description = "Open application help." });

			ControlViewModels.Register(SampleBarControlKeys.Paste, key
				=> new BarButtonViewModel(key, ApplicationCommands.Paste) { Description = "Adds content from the Clipboard into your document." });

			ControlViewModels.Register(SampleBarControlKeys.PasteMenu, key
				=> new BarSplitButtonViewModel(key, "Paste", ApplicationCommands.Paste) {
					Description = "Adds content from the Clipboard into your document.",
					MenuItems = {
						ControlViewModels[SampleBarControlKeys.Paste],
						ControlViewModels[SampleBarControlKeys.PasteSpecial],
					}
				});

			ControlViewModels.Register(SampleBarControlKeys.PasteSpecial, key
				=> new BarButtonViewModel(key, NotImplementedCommand) { KeyTipText = "S" });

			ControlViewModels.Register(SampleBarControlKeys.Redo, key
				=> new BarButtonViewModel(key, ApplicationCommands.Redo) { KeyTipText = "AQ", Description = "Redo the last operation." });

			/*(New)*/ ControlViewModels.Register(SampleBarControlKeys.SelectAll, key
				=> new BarButtonViewModel(key, ApplicationCommands.SelectAll) { KeyTipText = "SA", Description = "Select all text and objects." });

			ControlViewModels.Register(SampleBarControlKeys.Undo, key
				=> new BarButtonViewModel(key, ApplicationCommands.Undo) { KeyTipText = "AZ", Description = "Undo the last operation." });
		}

		/// <summary>
		/// Registers images with <see cref="ImageProvider"/>.
		/// </summary>
		private void RegisterImages() {
			ImageProvider.Register(SampleBarControlKeys.Copy, options => CreateBitmapImage(options, "Copy16.png", largeFileName: null));
			ImageProvider.Register(SampleBarControlKeys.Cut, options => CreateBitmapImage(options, "Cut16.png", largeFileName: null));
			ImageProvider.Register(SampleBarControlKeys.GroupClipboard, options => CreateBitmapImage(options, "Paste16.png", largeFileName: null));
			ImageProvider.Register(SampleBarControlKeys.GroupUndo, options => CreateBitmapImage(options, "Undo16.png", largeFileName: null));
			ImageProvider.Register(SampleBarControlKeys.Help, options => CreateBitmapImage(options, "Help16.png", "Help32.png"));
			ImageProvider.Register(SampleBarControlKeys.Paste, options => CreateBitmapImage(options, "Paste16.png", "Paste32.png"));
			ImageProvider.Register(SampleBarControlKeys.PasteMenu, options => CreateBitmapImage(options, "Paste16.png", "Paste32.png"));
			ImageProvider.Register(SampleBarControlKeys.PasteSpecial, options => CreateBitmapImage(options, "PasteSpecial16.png", largeFileName: null));
			ImageProvider.Register(SampleBarControlKeys.Redo, options => CreateBitmapImage(options, "Redo16.png", largeFileName: null));
			/*(New)*/ ImageProvider.Register(SampleBarControlKeys.SelectAll, options => CreateBitmapImage(options, "SelectAll16.png", largeFileName: null));
			ImageProvider.Register(SampleBarControlKeys.Undo, options => CreateBitmapImage(options, "Undo16.png", largeFileName: null));
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
