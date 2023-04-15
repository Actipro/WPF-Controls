using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.PopupAndContextMenus {

	/// <summary>
	/// Provides the base user control of shared logic for this sample that is extended for MVVM- and XAML-based samples.
	/// </summary>
	public abstract class SampleControlBase : UserControl {

		private ICommand pasteSpecialCommand;
		private CollectionViewSource pasteOptions;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public SampleControlBase() {
			// Initialize collections
			pasteOptions = PasteOptionGalleryItem.CreateDefaultCollectionViewSource();
			this.TagColors = new ObservableCollection<TagColorGalleryItem>(TagColorGalleryItem.CreateDefaultCollection());
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the collection of view models for the available paste options used by the "Advanced Paste Options" sample.
		/// </summary>
		/// <value>An <see cref="ICollectionView"/> of type <see cref="PasteOptionGalleryItem"/>.</value>
		public ICollectionView PasteOptions => pasteOptions.View;

		/// <summary>
		/// Gets the "Paste Special" command used by the "Advanced Paste Options" sample.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand PasteSpecialCommand {
			get {
				if (pasteSpecialCommand is null) {
					pasteSpecialCommand = new PreviewableDelegateCommand<object>(
						// Execute
						param => {
							if (param is null) {
								// This is where a dialog would typically be displayed to prompt the user on the type of paste to be performed
								ThemedMessageBox.Show($"This is where you would prompt the user for the type of special paste to be performed.", "Paste Special", MessageBoxButton.OK, MessageBoxImage.Information);
							}
							else if (param is PasteOptionGalleryItem pasteOption) {
								pasteOption.Execute(ResolveDefaultTargetControl());
							}
						},

						// CanExecute
						param => {
							if (param is null) {
								// No parameter indicates the user should be prompted for the type of paste to perform
								return true;
							}
							if (param is PasteOptionGalleryItem pasteOption) {
								// Test if the special paste operation is supported
								return pasteOption.CanExecute(ResolveDefaultTargetControl());
							}
							return false;
						},

						// Preview
						param => {
							// This is where you could optionally preview a special paste operation when the user
							// hovers over (or selects with keyboard) an option without invoking it
						},

						// CancelPreview
						param => {
							// This is where any preview of a special paste operation would be canceled
						}
					);
				}
				return pasteSpecialCommand;

				/// <summary>
				/// Gets the control that is the default target for commands.
				/// </summary>
				/// <returns>An object.</returns>
				object ResolveDefaultTargetControl() {
					return Keyboard.FocusedElement;
				}
			}
		}

		/// <summary>
		/// Gets the collection of view models for the available tag colors used by the "View Options with Color Tagging" showcase sample.
		/// </summary>
		/// <value>An <see cref="ObservableCollection{T}"/> of type <see cref="TagColorGalleryItem"/>.</value>
		public ObservableCollection<TagColorGalleryItem> TagColors { get; }

	}

}
