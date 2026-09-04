using ActiproSoftware.ProductSamples.BarsSamples.Common;
using ActiproSoftware.SampleBrowser.SampleData;
using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.Input;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.ComboBoxAndEditors;

/// <summary>
/// Provides the base user control of shared logic for this sample that is extended for MVVM- and XAML-based samples.
/// </summary>
public class SampleControlBase : UserControl {

	private ICommand? _comboBoxGalleryCommand;
	private ICommand? _comboBoxUnmatchedNumberTextCommand;
	private ICommand? _comboBoxUnmatchedTextCommand;
	private ICommand? _notImplementedCommand;
	private ICommand? _textBoxCommitCommand;

	private CollectionViewSource? _comboBoxColorItems;
	private CollectionViewSource? _comboBoxEnumItems;
	private CollectionViewSource? _comboBoxFontFamilyItems;
	private IEnumerable? _comboBoxFontSizeItems;
	private CollectionViewSource? _comboBoxNumberItems;
	private CollectionViewSource? _comboBoxPersonItems;

	#region Dependency Properties

	public static readonly DependencyProperty ComboboxPreviewLabelProperty
		= DependencyProperty.Register(nameof(ComboboxPreviewLabel), typeof(string), typeof(SampleControlBase), new PropertyMetadata(defaultValue: "<None>"));

	#endregion Dependency Properties

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The items to be displayed in combobox for selecting colors.
	/// </summary>
	public IEnumerable ComboBoxColorItems {
		get {
			if (_comboBoxColorItems is null) {
				var primaryCategory = "Primary Colors";
				var secondaryCategory = "Secondary Colors";

				_comboBoxColorItems = BarGalleryViewModel.CreateCollectionViewSource(
					new TextBarGalleryItemViewModel[] {
						// Primary
						new ("Red", primaryCategory) { ImageSource = (ImageSource)FindResource(LocalResourceKeys.RedSwatch) },
						new ("Yellow", primaryCategory) { ImageSource = (ImageSource)FindResource(LocalResourceKeys.YellowSwatch) },
						new ("Blue", primaryCategory) { ImageSource = (ImageSource)FindResource(LocalResourceKeys.BlueSwatch) },

						// Secondary
						new ("Orange", secondaryCategory) { ImageSource = (ImageSource)FindResource(LocalResourceKeys.OrangeSwatch) },
						new ("Green", secondaryCategory) { ImageSource = (ImageSource)FindResource(LocalResourceKeys.GreenSwatch) },
						new ("Purple", secondaryCategory) { ImageSource = (ImageSource)FindResource(LocalResourceKeys.PurpleSwatch) },
					},
					categorize: true
				);
			}

			return _comboBoxColorItems.View;
		}
	}

	/// <summary>
	/// The items to be displayed in combobox based on an enum.
	/// </summary>
	public IEnumerable ComboBoxEnumItems {
		get {
			_comboBoxEnumItems ??= BarGalleryViewModel.CreateCollectionViewSource(
				EnumBarGalleryItemViewModel<SampleEnum>.CreateCollection().Select(vm => {
					// Apply a default category
					if (vm.Category is null)
						vm.Category = "Uncategorized";
					return vm;
				}),
				categorize: true
			);

			return _comboBoxEnumItems.View;
		}
	}

	/// <summary>
	/// The items to be displayed in combobox for selecting font families.
	/// </summary>
	public IEnumerable ComboBoxFontFamilyItems {
		get {
			if (_comboBoxFontFamilyItems is null) {
				const string RecentlyUsedCategory = "Recently-Used Fonts";

				_comboBoxFontFamilyItems = BarGalleryViewModel.CreateCollectionViewSource(
					new FontFamilyBarGalleryItemViewModel[] {
						new(FontSettings.DefaultFontFamilyName, RecentlyUsedCategory)
					}.Concat(FontFamilyBarGalleryItemViewModel.CreateDefaultCollection()),
					categorize: true
				);
			}

			return _comboBoxFontFamilyItems.View;
		}
	}

	/// <summary>
	/// The items to be displayed in combobox for selecting font sizes.
	/// </summary>
	public IEnumerable ComboBoxFontSizeItems
		=> _comboBoxFontSizeItems ??= FontSizeBarGalleryItemViewModel.CreateDefaultCollection();

	/// <summary>
	/// The command for a gallery item selection from a combobox.
	/// </summary>
	public ICommand ComboBoxGalleryCommand {
		get => _comboBoxGalleryCommand ??= new PreviewableDelegateCommand<IBarGalleryItemViewModel>(
			executeAction: p =>
				MessageBox.Show($"The value '{p?.Label}' was matched from the gallery.", "Value Committed", MessageBoxButton.OK, MessageBoxImage.Information),
			canExecuteFunc: _ => true,

			// The items of BarComboBox support previewing the current item just like other gallery-based controls
			previewAction: p =>
				ComboboxPreviewLabel = p?.Label ?? "<Unknown>",
			cancelPreviewAction: _ => ComboboxPreviewLabel = "<None>"
		);
	}

	/// <summary>
	/// The items to be displayed in combobox for selecting numbers.
	/// </summary>
	public IEnumerable ComboBoxNumberItems {
		get {
			if (_comboBoxNumberItems is null) {
				var evenCategory = "Even Numbers";
				var oddCategory = "Odd Numbers";

				var items = new List<SimpleComboBoxGalleryItem>();
				for (var i = 1; i <= 20; i++) {
					bool isEven = (i % 2 == 0);
					items.Add(new SimpleComboBoxGalleryItem(i.ToString(), (isEven ? evenCategory : oddCategory)));
				}

				_comboBoxNumberItems = BarGalleryViewModel.CreateCollectionViewSource(items, categorize: true);
			}

			return _comboBoxNumberItems.View;
		}
	}

	/// <summary>
	/// The items to be displayed in combobox for selecting people.
	/// </summary>
	public IEnumerable ComboBoxPersonItems {
		get {
			if (_comboBoxPersonItems is null) {
				var items = new List<SimpleComboBoxGalleryItem>();

				foreach (var person in People.All.OrderBy(x => x.FullName))
					items.Add(new SimpleComboBoxGalleryItem(person.FullName, person.Position));

				_comboBoxPersonItems = BarGalleryViewModel.CreateCollectionViewSource(items, categorize: true);
			}

			return _comboBoxPersonItems.View;
		}
	}

	/// <summary>
	/// The combobox preview label.
	/// </summary>
	public string? ComboboxPreviewLabel {
		get => (string)GetValue(ComboboxPreviewLabelProperty);
		set => SetValue(ComboboxPreviewLabelProperty, value);
	}

	/// <summary>
	/// The command that is executed when an unmatched value is entered into a combobox for selecting numbers.
	/// </summary>
	public ICommand ComboBoxUnmatchedNumberTextCommand {
		// This command is raised when text is typed in a BarComboBox that does not match one of the known gallery items
		get => _comboBoxUnmatchedNumberTextCommand ??= new DelegateCommand<string>(
			executeAction: p => {
				// No action necessary, but show a message to indicate that the value was accepted
				MessageBox.Show($"The integer text value '{p}' was manually entered and accepted without a match in the gallery.", "Custom Number Text Value Committed", MessageBoxButton.OK, MessageBoxImage.Information);
			},
			canExecuteFunc: p => {
				// The BarComboBox.UnmatchedTextCommand.CanExecute result will determine if the
				//   typed text should be allowed... true to allow the value and false to reject it
				return int.TryParse(p, out var number) && (number > 0);
			}
		);
	}

	/// <summary>
	/// The command that is executed when an unmatched value is entered into a general combobox.
	/// </summary>
	public ICommand ComboBoxUnmatchedTextCommand {
		// This command is raised when text is typed in a BarComboBox that does not match one of the known gallery items
		get => _comboBoxUnmatchedTextCommand ??= new DelegateCommand<string>(
			executeAction: p => {
				// No action necessary, but show a message to indicate that the value was accepted
				MessageBox.Show($"The text value '{p}' was manually entered and accepted without a match in the gallery.", "Custom Text Value Committed", MessageBoxButton.OK, MessageBoxImage.Information);
			},
			canExecuteFunc: _ => {
				// The BarComboBox.UnmatchedTextCommand.CanExecute result will determine if the
				//   typed text should be allowed... true to allow the value and false to reject it
				return true;
			}
		);
	}

	/// <summary>
	/// The committed text associated with <see cref="TextBoxCommitCommand"/>.
	/// </summary>
	/// <returns>The committed text; or <c>null</c> if the text could not be determined.</returns>
	protected virtual string? GetTextBoxCommitCommandText(object? commandParameter)
		=> null;

	/// <summary>
	/// The command for functionality that has not been implemented by the sample.
	/// </summary>
	public ICommand NotImplementedCommand {
		get => _notImplementedCommand ??= new DelegateCommand<object>(
			_ => {
				MessageBox.Show(
					"This control is for user interface demonstration purposes only and no application functionality has been implemented for it.", "Not Implemented",
					MessageBoxButton.OK, MessageBoxImage.Information);
			}
		);
	}

	/// <summary>
	/// The command for a commit from a textbox.
	/// </summary>
	public ICommand TextBoxCommitCommand {
		get => _textBoxCommitCommand ??= new DelegateCommand<string>(
			executeAction: p => MessageBox.Show($"The value '{GetTextBoxCommitCommandText(p)}' was committed from the textbox.", "Value Committed", MessageBoxButton.OK, MessageBoxImage.Information),
			canExecuteFunc: _ => true
		);
	}

}
