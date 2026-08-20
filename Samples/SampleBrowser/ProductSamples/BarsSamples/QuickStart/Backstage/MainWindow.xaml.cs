using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Input;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.Backstage;

public partial class MainWindow : INotifyPropertyChanged {

	private ICommand? _backstageHeaderButtonCommand;

	private int _backstageMinHeaderWidth = 0;
	private int _backstageMaxHeaderWidth = 300;
	private bool _canClose = true;
	private bool _canSelectFirstTabOnOpen = true;
	private bool _isBackstageOpen = true;
	private bool _isFirstBackstage = true;
	private bool _sampleButton3CanCloseBackstage = true;
	private string _sampleButton3Label = "Sample Button 3";
	private string _selectedTabKeyOnOpen = "(Previous Selection)";
	private bool _useSampleButtonImages = false;

	// --------------------------------------------------------------------------------------------------
	// EVENTS
	// --------------------------------------------------------------------------------------------------

	public event PropertyChangedEventHandler? PropertyChanged;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	public MainWindow() {
		InitializeComponent();

		// Bind the view to itself since an explicit view model is not created for this sample
		DataContext = this;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Called when the <see cref="RibbonBackstage.IsOpen"/> property value is changed.
	/// </summary>
	private void OnBackstageIsOpenChanged(object sender, RoutedEventArgs e) {
		// Optionally pre-select the 'Options' tab when opening the backstage
		if (sender is RibbonBackstage { IsOpen: true } backstage
			&& !CanSelectFirstTabOnOpen
			&& !string.IsNullOrWhiteSpace(SelectedTabKeyOnOpen)) {

			// Find the desired tab to select
			var tab = backstage.Items.OfType<RibbonBackstageTabItem>()
				.FirstOrDefault(tabItem => tabItem.Key == SelectedTabKeyOnOpen);

			// Configure the backstage selection
			if (tab is not null)
				backstage.SelectedItem = tab;
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The command to be executed when one of the sample backstage buttons is invoked.
	/// </summary>
	public ICommand BackstageHeaderButtonCommand {
		get => _backstageHeaderButtonCommand ??= new DelegateCommand<object>(_ => {
			MessageBox.Show("When a RibbonBackstageHeaderButton is invoked and its CanCloseBackstage property is the default of true, the Backstage automatically closes.\r\n\r\nThese buttons are typically associated with commands that perform simple operations like Help, Save, or Close that do not need the additional content area of a RibbonBackstageTabItem.",
				"About RibbonBackstageHeaderButton", MessageBoxButton.OK, MessageBoxImage.Information);
		});
	}

	/// <summary>
	/// The maximum width of the backstage header where tabs and buttons are displayed.
	/// </summary>
	public int BackstageMaxHeaderWidth {
		get => _backstageMaxHeaderWidth;
		set {
			if (SetProperty(ref _backstageMaxHeaderWidth, value))
				BackstageMinHeaderWidth = Math.Min(BackstageMinHeaderWidth, value);
		}
	}

	/// <summary>
	/// The minimum width of the backstage header where tabs and buttons are displayed.
	/// </summary>
	public int BackstageMinHeaderWidth {
		get => _backstageMinHeaderWidth;
		set {
			if (SetProperty(ref _backstageMinHeaderWidth, value))
				BackstageMaxHeaderWidth = Math.Max(BackstageMaxHeaderWidth, value);
		}
	}

	/// <summary>
	/// Indicates if the backstage can be closed.
	/// </summary>
	public bool CanClose {
		get => _canClose;
		set => SetProperty(ref _canClose, value);
	}

	/// <summary>
	/// Indicates if the first tab should be selected when the Backstage is opened.
	/// </summary>
	public bool CanSelectFirstTabOnOpen {
		get => _canSelectFirstTabOnOpen;
		set => SetProperty(ref _canSelectFirstTabOnOpen, value);
	}

	/// <summary>
	/// Indicates if the backstage is open.
	/// </summary>
	public bool IsBackstageOpen {
		get => _isBackstageOpen;
		set {
			if (SetProperty(ref _isBackstageOpen, value)) {
				// When the backstage closes, set a flag that the initial backstage is no longer displayed
				if (!_isBackstageOpen)
					IsFirstBackstage = false;
			}
		}
	}

	/// <summary>
	/// Indicates if the backstage should be configured for the initial view where some tabs are larger
	/// and unnecessary buttons are hidden.
	/// </summary>
	public bool IsFirstBackstage {
		get => _isFirstBackstage;
		set {
			if (SetProperty(ref _isFirstBackstage, value)) {
				// Notify dependent properties have changed
				OnPropertyChanged(nameof(PrimaryBackstageTabVariantSize));
			}
		}
	}

	/// <summary>
	/// Raises the <see cref="PropertyChanged"/> event.
	/// </summary>
	/// <param name="propertyName">(Optional) The name of the property that changed.</param>
	protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		=> OnPropertyChanged(new PropertyChangedEventArgs(propertyName));

	/// <summary>
	/// Raises the <see cref="PropertyChanged"/> event.
	/// </summary>
	/// <param name="e">The event data.</param>
	protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		=> PropertyChanged?.Invoke(this, e);

	/// <summary>
	/// The <see cref="VariantSize"/> to be used for the primary tabs.
	/// </summary>
	/// <remarks>
	/// This property is used to show large variants of the most important tabs when the backstage is initially displayed.
	/// </remarks>
	public VariantSize PrimaryBackstageTabVariantSize
		=> IsFirstBackstage ? VariantSize.Large : VariantSize.Medium;

	/// <summary>
	/// Indicates whether the third sample button can close backstage.
	/// </summary>
	public bool SampleButton3CanCloseBackstage {
		get => _sampleButton3CanCloseBackstage;
		set => SetProperty(ref _sampleButton3CanCloseBackstage, value);
	}

	/// <summary>
	/// The label to be displayed on the third sample button.
	/// </summary>
	public string SampleButton3Label {
		get => _sampleButton3Label;
		set => SetProperty(ref _sampleButton3Label, value);
	}

	/// <summary>
	/// The <see cref="ImageSource"/>, if any, to be displayed on the sample buttons.
	/// </summary>
	public ImageSource? SampleButtonImageSource
		=> UseSampleButtonImages ? new BitmapImage(new Uri("/Images/Icons/QuickStart16.png", UriKind.Relative)) : null;

	/// <summary>
	/// The key of the tab that should be manually selected when the backstage opens.
	/// </summary>
	public string SelectedTabKeyOnOpen {
		get => _selectedTabKeyOnOpen;
		set => SetProperty(ref _selectedTabKeyOnOpen, value);
	}

	/// <summary>
	/// Called from a property setter to change the backing field's value and raise
	/// <see cref="PropertyChanged"/> notification events if the new value is not equal to the current value.
	/// </summary>
	/// <typeparam name="T">The type of the property that changed.</typeparam>
	/// <param name="field">The backing field that holds the property's value, which may be updated.</param>
	/// <param name="newValue">The new property value.</param>
	/// <param name="propertyName">(Optional) The name of the property that changed.</param>
	/// <returns>
	/// <c>true</c> if the property was changed; otherwise, <c>false</c>.
	/// </returns>
	protected bool SetProperty<T>(
		#if NET
		[NotNullIfNotNull(nameof(newValue))]
		#endif
		ref T field, T newValue, [CallerMemberName] string? propertyName = null) {
		// IMPORTANT NOTE: This method is replicated over multiple classes and edits must be kept in sync
		if (!EqualityComparer<T>.Default.Equals(field, newValue)) {
			field = newValue;
			OnPropertyChanged(propertyName);
			return true;
		}

		return false;
	}

	/// <summary>
	/// Indicates if images should be displayed on the sample buttons.
	/// </summary>
	public bool UseSampleButtonImages {
		get => _useSampleButtonImages;
		set {
			if (SetProperty(ref _useSampleButtonImages, value)) {
				// Notify dependent properties have changed
				OnPropertyChanged(nameof(SampleButtonImageSource));
			}
		}
	}

}
