using ActiproSoftware.Windows.Input;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.PopupAndContextMenus;

/// <summary>
/// Provides the base user control of shared logic for this sample that is extended for MVVM- and XAML-based samples.
/// </summary>
public abstract class SampleControlBase : UserControl {

	private ICommand? _pasteSpecialCommand;
	private readonly CollectionViewSource _pasteOptions;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	public SampleControlBase() {
		// Initialize collections
		_pasteOptions = PasteOptionGalleryItem.CreateDefaultCollectionViewSource();
		TagColors = new ObservableCollection<TagColorGalleryItem>(TagColorGalleryItem.CreateDefaultCollection());
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The collection of view models for the available paste options used by the "Advanced Paste Options" sample.
	/// </summary>
	/// <value>An <see cref="ICollectionView"/> of type <see cref="PasteOptionGalleryItem"/>.</value>
	public ICollectionView PasteOptions
		=> _pasteOptions.View;

	/// <summary>
	/// The "Paste Special" command used by the "Advanced Paste Options" sample.
	/// </summary>
	public ICommand PasteSpecialCommand {
		get {
			return _pasteSpecialCommand ??= new PreviewableDelegateCommand<object>(
				// Execute
				p => {
					if (p is null) {
						// This is where a dialog would typically be displayed to prompt the user on the type of paste to be performed
						MessageBox.Show($"This is where you would prompt the user for the type of special paste to be performed.", "Paste Special", MessageBoxButton.OK, MessageBoxImage.Information);
					}
					else if (p is PasteOptionGalleryItem pasteOption) {
						pasteOption.Execute(ResolveDefaultTargetControl());
					}
				},

				// CanExecute
				p => {
					if (p is null) {
						// No parameter indicates the user should be prompted for the type of paste to perform
						return true;
					}
					if (p is PasteOptionGalleryItem pasteOption) {
						// Test if the special paste operation is supported
						return pasteOption.CanExecute(ResolveDefaultTargetControl());
					}
					return false;
				},

				// Preview
				_ => {
					// This is where you could optionally preview a special paste operation when the user
					//   hovers over (or selects with keyboard) an option without invoking it
				},

				// CancelPreview
				_ => {
					// This is where any preview of a special paste operation would be canceled
				}
			);

			/// <summary>
			/// Returns the control that is the default target for commands.
			/// </summary>
			object ResolveDefaultTargetControl()
				=> Keyboard.FocusedElement;
		}
	}

	/// <summary>
	/// The collection of view models for the available tag colors used by the "View Options with Color Tagging" showcase sample.
	/// </summary>
	public ObservableCollection<TagColorGalleryItem> TagColors { get; }

}
