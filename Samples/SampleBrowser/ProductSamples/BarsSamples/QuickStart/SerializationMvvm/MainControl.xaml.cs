using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.Extensions;
using ActiproSoftware.Windows.Input;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.SerializationMvvm;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl : ISampleCommands {

	private string? _originalLayout;
	private RibbonViewModel? _ribbonViewModel;

	private readonly SampleBarManager _barManager;

	private ICommand? _restoreLayoutCommand;
	private ICommand? _restoreOriginalCommand;
	private ICommand? _saveLayoutCommand;

	// --------------------------------------------------------------------------------------------------
	// INTERFACE IMPLEMENTATION
	// --------------------------------------------------------------------------------------------------

	ICommand ISampleCommands.RestoreLayout {
		get => _restoreLayoutCommand ??= new DelegateCommand<object>(_ => {
			var currentLayout = CurrentLayout;
			if (string.IsNullOrEmpty(currentLayout)) {
				MessageBox.Show("The current layout is undefined and cannot be restored.  Please save the current layout first", "Restore", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// Attempt to restore the current layout
			TryRestoreLayout(currentLayout);
		});
	}

	ICommand ISampleCommands.RestoreOriginalLayout {
		get => _restoreOriginalCommand ??= new DelegateCommand<object>(_ => {
			if (string.IsNullOrEmpty(_originalLayout)) {
				MessageBox.Show("The original layout is undefined and cannot be restored.", "Restore", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// Attempt to restore the original layout
			if (TryRestoreLayout(_originalLayout!))
				CurrentLayout = _originalLayout!;
		});
	}

	ICommand ISampleCommands.SaveLayout {
		get => _saveLayoutCommand ??= new DelegateCommand<object>(_ => {
			// Attempt to save the current layout
			if (TrySaveLayout(out var layout))
				CurrentLayout = layout!;
		});
	}

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		// Initialize the bar manager for working with Ribbon view models
		_barManager = new SampleBarManager(this);

		// Bind the view to itself since we do not define an explicit view model
		DataContext = this;

		InitializeComponent();

		// Try to cache the original layout so it can be restored
		Loaded += (s, e) => {
			if ((_originalLayout is null) && TrySaveLayout(out var layout)) {
				_originalLayout = layout;
				CurrentLayout = layout!;
			}
		};
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates the ribbon view model for the MVVM sample.
	/// </summary>
	private RibbonViewModel CreateRibbonViewModel() {
		var cutButtonViewModel = _barManager.ControlViewModels[SampleBarControlKeys.Cut];
		var copyButtonViewModel = _barManager.ControlViewModels[SampleBarControlKeys.Copy];
		var pasteButtonViewModel = _barManager.ControlViewModels[SampleBarControlKeys.Paste];

		return new RibbonViewModel() {
			IsApplicationButtonVisible = false,
			QuickAccessToolBarLocation = RibbonQuickAccessToolBarLocation.Below,
			QuickAccessToolBarMode = RibbonQuickAccessToolBarMode.Visible,
			QuickAccessToolBar = new RibbonQuickAccessToolBarViewModel() {
				CommonItems = { cutButtonViewModel, copyButtonViewModel, pasteButtonViewModel },
				Items = { cutButtonViewModel, copyButtonViewModel, pasteButtonViewModel },
			},
			Tabs = {
				new RibbonTabViewModel("Home") {
					Groups = {
						new RibbonGroupViewModel("Serialization") {
							Items = {
								new RibbonControlGroupViewModel() {
									ItemVariantBehavior = ItemVariantBehavior.AlwaysLarge,
									SeparatorMode = RibbonControlGroupSeparatorMode.Always,
									Items = {
										_barManager.ControlViewModels[SampleBarControlKeys.SaveLayout],
										_barManager.ControlViewModels[SampleBarControlKeys.RestoreLayout]
									}
								},
								_barManager.ControlViewModels[SampleBarControlKeys.RestoreOriginalLayout],
							}
						}
					}
				},
			}
		};
	}

	/// <summary>
	/// Occurs when an item is being added to the Quick Access Toolbar.
	/// </summary>
	/// <param name="sender">A reference to the <see cref="Ribbon"/>.</param>
	/// <param name="e">The event arguments.</param>
	private void OnQuickAccessToolBarItemAdding(object sender, RibbonQuickAccessToolBarItemAddingEventArgs e) {
		// This event is raised when an item is being added to the Quick Access Toolbar.
		//   The event data may define the Key of the item being added and, if found, the Item
		//   that will be added. If the Ribbon is unable to automatically locate an item with
		//   the desired key, the Item property will be NULL. When this happens, you can manually assign
		//   a corresponding Item. If Item is NULL or Cancel is set to TRUE then nothing will be added.
		//
		// This event can also be used to notify a user if an attempt was made to add an item
		//   to the Quick Access Toolbar that might no longer be available.
		if (!e.Cancel) {
			Debug.WriteLine($"Adding QAT Item... Key={e.Key}; Item={e.Item?.ToString() ?? "NULL"}");

			// Attempt to resolve any unspecified items by looking up the key in the bar manager
			if ((e.Item is null) && (e.Key is { } key))
				e.Item = _barManager?.ControlViewModels[key];

			if (e.Item is null) {
				MessageBox.Show($"Unable to restore the Quick Access Toolbar item '{e.Key}' because the corresponding command could not be found.", "Command Not Found", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
		}
	}

	/// <summary>
	/// Tries to restore the specified layout data to the Ribbon.
	/// </summary>
	/// <param name="xmlLayout">The XML layout data.</param>
	/// <returns><c>true</c> if the layout was successfully restored; otherwise <c>false</c>.</returns>
	private bool TryRestoreLayout(string xmlLayout) {
		try {
			// Initialize the options that will be supported during restore based on current settings
			var options = OptionsModel.CreateOptions();

			// Deserialize the layout to the Ribbon
			new RibbonSerializer().Deserialize(ribbon, xmlLayout, options);

			// Indicate success
			return true;
		}
		catch (Exception ex) {
			// Exception during the deserialization process
			Debug.WriteLine(ex);
			UserPromptBuilder.Configure().ForException(ex, "Error restoring layout.").Show();

			// Indicate failure to restore
			return false;
		}
	}

	/// <summary>
	/// Tries to save the current Ribbon layout.
	/// </summary>
	/// <param name="layout">When successful, outputs the layout data.</param>
	/// <returns><c>true</c> if the layout was successfully saved; otherwise <c>false</c>.</returns>
	private bool TrySaveLayout(out string? layout) {
		try {
			// Initialize the options that will be supported during restore based on current settings
			var options = OptionsModel.CreateOptions();

			// Serialize the layout from the Ribbon
			layout = new RibbonSerializer().Serialize(ribbon, options);

			// Indicate success
			return true;
		}
		catch (Exception ex) {
			// Exception during the serialization process
			Debug.WriteLine(ex);
			UserPromptBuilder.Configure().ForException(ex, "Error saving layout.").Show();

			// Indicate failure
			layout = null;
			return false;
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The current layout data.
	/// </summary>
	/// <value>An XML-formatted string.</value>
	public string CurrentLayout {
		get => xmlDataEditor.Text;
		set => xmlDataEditor.Text = value;
	}

	/// <summary>
	/// The view model for controlling which options are included when serializing and deserialization.
	/// </summary>
	public SerializerOptionsViewModel OptionsModel { get; } = new SerializerOptionsViewModel();

	/// <summary>
	/// The ribbon view model for the MVVM sample.
	/// </summary>
	public RibbonViewModel RibbonViewModel
		=> _ribbonViewModel ??= CreateRibbonViewModel();

}
