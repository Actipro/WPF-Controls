using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.Input;
using ActiproSoftware.Windows.Media;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.GalleryInRibbon;

/// <summary>
/// Provides the base user control of shared logic for this sample that is extended for MVVM- and XAML-based samples.
/// </summary>
public abstract class SampleControlBase : UserControl {

	private CollectionViewSource? _colorItems;
	private ICommand? _configureOneRowLayoutCommand;
	private ICommand? _configureTwoRowLayoutCommand;
	private ICommand? _configureThreeRowLayoutCommand;
	private DelegateCommand<ColorBarGalleryItemViewModel>? _setColorCommand;

	#region Dependency Properties

	public static readonly DependencyProperty OptionsProperty
		= DependencyProperty.Register(nameof(Options), typeof(OptionsViewModel), typeof(SampleControlBase), new PropertyMetadata(defaultValue: null, OnOptionsPropertyValueChanged));

	#endregion Dependency Properties

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public SampleControlBase() {
		// Initialization of the base class is performed in the OnInitialized method that
		//   is called after derived classes call InitializeComponent
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates a <see cref="CollectionViewSource"/> of gallery item view models for the base colors consistent with the default standard colors.
	/// </summary>
	/// <returns>A <see cref="CollectionViewSource"/> of <see cref="ColorBarGalleryItemViewModel"/>.</returns>
	private static CollectionViewSource CreateColorItemsCollectionViewSource() {
		var warmColorsCategory = "Warm Colors";
		var coolColorsCategory = "Cool Colors";
		var neutralColorsCategory = "Neutral Colors";

		return BarGalleryViewModel.CreateCollectionViewSource(
			new ColorBarGalleryItemViewModel[] {
				new(UIColor.FromWebColor("#eeece1").ToColor(), neutralColorsCategory, "Tan"),
				new(UIColor.FromWebColor("#ddd9c3").ToColor(), neutralColorsCategory, "Tan, Darker 10%"),
				new(UIColor.FromWebColor("#c4bd97").ToColor(), neutralColorsCategory, "Tan, Darker 25%"),
				new(UIColor.FromWebColor("#938953").ToColor(), neutralColorsCategory, "Tan, Darker 50%"),
				new(UIColor.FromWebColor("#494429").ToColor(), neutralColorsCategory, "Tan, Darker 75%"),
				new(UIColor.FromWebColor("#1d1b10").ToColor(), neutralColorsCategory, "Tan, Darker 90%"),

				new(UIColor.FromWebColor("#1f497d").ToColor(), coolColorsCategory, "Dark Blue"),
				new(UIColor.FromWebColor("#c6d9f0").ToColor(), coolColorsCategory, "Dark Blue, Lighter 80%"),
				new(UIColor.FromWebColor("#8db3e2").ToColor(), coolColorsCategory, "Dark Blue, Lighter 60%"),
				new(UIColor.FromWebColor("#548dd4").ToColor(), coolColorsCategory, "Dark Blue, Lighter 40%"),
				new(UIColor.FromWebColor("#17365d").ToColor(), coolColorsCategory, "Dark Blue, Darker 25%"),
				new(UIColor.FromWebColor("#17365d").ToColor(), coolColorsCategory, "Dark Blue, Darker 50%"),

				new(UIColor.FromWebColor("#4f81bd").ToColor(), coolColorsCategory, "Blue"),
				new(UIColor.FromWebColor("#dbe5f1").ToColor(), coolColorsCategory, "Blue, Lighter 80%"),
				new(UIColor.FromWebColor("#b8cce4").ToColor(), coolColorsCategory, "Blue, Lighter 60%"),
				new(UIColor.FromWebColor("#95b3d7").ToColor(), coolColorsCategory, "Blue, Lighter 40%"),
				new(UIColor.FromWebColor("#366092").ToColor(), coolColorsCategory, "Blue, Darker 25%"),
				new(UIColor.FromWebColor("#244061").ToColor(), coolColorsCategory, "Blue, Darker 50%"),

				new(UIColor.FromWebColor("#c0504d").ToColor(), warmColorsCategory, "Red"),
				new(UIColor.FromWebColor("#f2dbdb").ToColor(), warmColorsCategory, "Red, Lighter 80%"),
				new(UIColor.FromWebColor("#e5b9b7").ToColor(), warmColorsCategory, "Red, Lighter 60%"),
				new(UIColor.FromWebColor("#d99694").ToColor(), warmColorsCategory, "Red, Lighter 40%"),
				new(UIColor.FromWebColor("#953734").ToColor(), warmColorsCategory, "Red, Darker 25%"),
				new(UIColor.FromWebColor("#632423").ToColor(), warmColorsCategory, "Red, Darker 50%"),

				new(UIColor.FromWebColor("#f79646").ToColor(), warmColorsCategory, "Orange"),
				new(UIColor.FromWebColor("#fdeada").ToColor(), warmColorsCategory, "Orange, Lighter 80%"),
				new(UIColor.FromWebColor("#fbd5b5").ToColor(), warmColorsCategory, "Orange, Lighter 60%"),
				new(UIColor.FromWebColor("#fac090").ToColor(), warmColorsCategory, "Orange, Lighter 40%"),
				new(UIColor.FromWebColor("#e36c09").ToColor(), warmColorsCategory, "Orange, Darker 25%"),
				new(UIColor.FromWebColor("#974806").ToColor(), warmColorsCategory, "Orange, Darker 50%"),

				new(UIColor.FromWebColor("#9bbb59").ToColor(), coolColorsCategory, "Olive Green"),
				new(UIColor.FromWebColor("#ebf1dd").ToColor(), coolColorsCategory, "Olive Green, Lighter 80%"),
				new(UIColor.FromWebColor("#d6e3bc").ToColor(), coolColorsCategory, "Olive Green, Lighter 60%"),
				new(UIColor.FromWebColor("#c3d69b").ToColor(), coolColorsCategory, "Olive Green, Lighter 40%"),
				new(UIColor.FromWebColor("#76923c").ToColor(), coolColorsCategory, "Olive Green, Darker 25%"),
				new(UIColor.FromWebColor("#4f6128").ToColor(), coolColorsCategory, "Olive Green, Darker 50%"),

				new(UIColor.FromWebColor("#4bacc6").ToColor(), coolColorsCategory, "Aqua"),
				new(UIColor.FromWebColor("#dbeef3").ToColor(), coolColorsCategory, "Aqua, Lighter 80%"),
				new(UIColor.FromWebColor("#b6dde8").ToColor(), coolColorsCategory, "Aqua, Lighter 60%"),
				new(UIColor.FromWebColor("#93cddc").ToColor(), coolColorsCategory, "Aqua, Lighter 40%"),
				new(UIColor.FromWebColor("#31859b").ToColor(), coolColorsCategory, "Aqua, Darker 25%"),
				new(UIColor.FromWebColor("#205867").ToColor(), coolColorsCategory, "Aqua, Darker 50%"),

				new(UIColor.FromWebColor("#8064a2").ToColor(), coolColorsCategory, "Purple"),
				new(UIColor.FromWebColor("#e5e0ec").ToColor(), coolColorsCategory, "Purple, Lighter 80%"),
				new(UIColor.FromWebColor("#ccc0d9").ToColor(), coolColorsCategory, "Purple, Lighter 60%"),
				new(UIColor.FromWebColor("#b2a2c7").ToColor(), coolColorsCategory, "Purple, Lighter 40%"),
				new(UIColor.FromWebColor("#5f497a").ToColor(), coolColorsCategory, "Purple, Darker 25%"),
				new(UIColor.FromWebColor("#3f3151").ToColor(), coolColorsCategory, "Purple, Darker 50%"),

				new(UIColor.FromWebColor("#ffffff").ToColor(), neutralColorsCategory, "White"),
				new(UIColor.FromWebColor("#f2f2f2").ToColor(), neutralColorsCategory, "White, Darker 5%"),
				new(UIColor.FromWebColor("#d8d8d8").ToColor(), neutralColorsCategory, "White, Darker 15%"),
				new(UIColor.FromWebColor("#bfbfbf").ToColor(), neutralColorsCategory, "White, Darker 25%"),
				new(UIColor.FromWebColor("#a5a5a5").ToColor(), neutralColorsCategory, "White, Darker 35%"),
				new(UIColor.FromWebColor("#7f7f7f").ToColor(), neutralColorsCategory, "White, Darker 50%"),
			},
			categorize: true
		);
	}

	/// <summary>
	/// Initializes the collection of gallery item view models for the galleries used by this sample.
	/// </summary>
	private void InitializeColorGalleryItemViewModelCollections()
		=> _colorItems = CreateColorItemsCollectionViewSource();

	/// <summary>
	/// Occurs when the <see cref="OptionsProperty"/> dependency property value has changed.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private static void OnOptionsPropertyValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		=> ((SampleControlBase)obj).OnOptionsPropertyValueChanged(e.OldValue as OptionsViewModel, e.NewValue as OptionsViewModel);

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The gallery item view models for a Font Color gallery.
	/// </summary>
	/// <value>An <see cref="IEnumerable"/> of <see cref="ColorBarGalleryItemViewModel"/>.</value>
	public IEnumerable? ColorItems
		=> _colorItems?.View;

	/// <summary>
	/// The command that will configure the gallery with a 1-row layout.
	/// </summary>
	public ICommand ConfigureOneRowLayoutCommand {
		get => _configureOneRowLayoutCommand ??= new DelegateCommand<object>(_ => {
			if (Options is not null)
				Options.ItemTemplate = FindResource("LargeItemDataTemplate") as DataTemplate;
		});
	}

	/// <summary>
	/// The command that will configure the gallery with a 3-row layout.
	/// </summary>
	public ICommand ConfigureThreeRowLayoutCommand {
		get => _configureThreeRowLayoutCommand ??= new DelegateCommand<object>(_ => {
			if (Options is not null)
				Options.ItemTemplate = FindResource("SmallItemDataTemplate") as DataTemplate;
		});
	}

	/// <summary>
	/// The command that will configure the gallery with a 2-row layout.
	/// </summary>
	public ICommand ConfigureTwoRowLayoutCommand {
		get => _configureTwoRowLayoutCommand ??= new DelegateCommand<object>(_ => {
			if (Options is not null)
				Options.ItemTemplate = FindResource("MediumItemDataTemplate") as DataTemplate;
		});
	}

	/// <summary>
	/// The large-sized <see cref="ImageSource"/> to be used for a command that configures a 1-row layout.
	/// </summary>
	public ImageSource? OneRowLayoutLargeImageSource { get; private set; }

	/// <summary>
	/// The small-sized <see cref="ImageSource"/> to be used for a command that configures a 1-row layout.
	/// </summary>
	public ImageSource? OneRowLayoutSmallImageSource { get; private set; }

	/// <inheritdoc/>
	protected override void OnInitialized(EventArgs e) {
		base.OnInitialized(e);

		// Cache the XAML-based images defined as resources
		OneRowLayoutLargeImageSource = FindResource("OneRowLayoutLargeImage") as DrawingImage;
		OneRowLayoutSmallImageSource = FindResource("OneRowLayoutSmallImage") as DrawingImage;
		TwoRowLayoutLargeImageSource = FindResource("TwoRowLayoutLargeImage") as DrawingImage;
		TwoRowLayoutSmallImageSource = FindResource("TwoRowLayoutSmallImage") as DrawingImage;
		ThreeRowLayoutLargeImageSource = FindResource("ThreeRowLayoutLargeImage") as DrawingImage;
		ThreeRowLayoutSmallImageSource = FindResource("ThreeRowLayoutSmallImage") as DrawingImage;

		// Initialize the collection of color gallery items (used by both XAML and MVVM samples)
		InitializeColorGalleryItemViewModelCollections();
	}

	/// <summary>
	/// Handles a change in one of the individual property values on <see cref="Options"/>.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="args">The event data.</param>
	protected virtual void OnOptionsPropertyChanged(object? sender, PropertyChangedEventArgs args) {
		if (args.PropertyName == nameof(Options.IsSetColorCommandEnabled)) {
			// Notify that the state of the command has changed
			_setColorCommand?.RaiseCanExecuteChanged();
		}
	}

	/// <summary>
	/// Handles a change in the <see cref="OptionsProperty"/> dependency property value.
	/// </summary>
	/// <param name="oldValue">The old value.</param>
	/// <param name="newValue">The new value.</param>
	protected virtual void OnOptionsPropertyValueChanged(OptionsViewModel? oldValue, OptionsViewModel? newValue) {
		// Stop listening for changes
		if (oldValue is not null)
			oldValue.PropertyChanged -= OnOptionsPropertyChanged;

		// Listen for changes
		if (newValue is not null)
			newValue.PropertyChanged += OnOptionsPropertyChanged;

		// Use the 1-row layout by default
		if (newValue is { ItemTemplate: null })
			newValue.ItemTemplate = FindResource("LargeItemDataTemplate") as DataTemplate;
	}

	/// <summary>
	/// The options associated with this control.
	/// </summary>
	public OptionsViewModel? Options {
		get => (OptionsViewModel)GetValue(OptionsProperty);
		set => SetValue(OptionsProperty, value);
	}

	/// <summary>
	/// The command to be executed when a gallery item is selected.
	/// </summary>
	public ICommand SetColorCommand {
		get => _setColorCommand ??= new DelegateCommand<ColorBarGalleryItemViewModel>(
			p => {
				if (p is not null) {
					MessageBox.Show($"This is where you would apply the following selected color:\r\n\r\n{p.Value} {p.Label}",
						"Set Color", MessageBoxButton.OK, MessageBoxImage.Information);
				}
			},
			_ => Options?.IsSetColorCommandEnabled == true
		);
	}

	/// <summary>
	/// The large-sized <see cref="ImageSource"/> to be used for a command that configures a 3-row layout.
	/// </summary>
	public ImageSource? ThreeRowLayoutLargeImageSource { get; private set; }

	/// <summary>
	/// The small-sized <see cref="ImageSource"/> to be used for a command that configures a 3-row layout.
	/// </summary>
	public ImageSource? ThreeRowLayoutSmallImageSource { get; private set; }

	/// <summary>
	/// The large-sized <see cref="ImageSource"/> to be used for a command that configures a 2-row layout.
	/// </summary>
	public ImageSource? TwoRowLayoutLargeImageSource { get; private set; }

	/// <summary>
	/// The small-sized <see cref="ImageSource"/> to be used for a command that configures a 2-row layout.
	/// </summary>
	public ImageSource? TwoRowLayoutSmallImageSource { get; private set; }

}
