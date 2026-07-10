using ActiproSoftware.Windows.Controls.Bars;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.SerializationXaml;

/// <summary>
/// Defines a view model for turning options on or off that will be used during Ribbon serialization.
/// </summary>
public class SerializerOptionsViewModel : ObservableObjectBase {

	private bool _layoutMode = true;
	private bool _minimizedState = true;
	private bool _quickAccessToolBarAllowLabels = true;
	private bool _quickAccessToolBarItems = true;
	private bool _quickAccessToolBarLocation = true;
	private bool _quickAccessToolBarMode = true;
	private bool _userInterfaceDensity = true;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="RibbonSerializerOptions"/> that are reflected by the current configuration.
	/// </summary>
	public RibbonSerializerOptions CreateOptions() {
		// Start with no options enabled
		var options = RibbonSerializerOptions.None;

		// Add each option that is currently turned on
		if (LayoutMode)
			options |= RibbonSerializerOptions.LayoutMode;
		if (MinimizedState)
			options |= RibbonSerializerOptions.MinimizedState;
		if (QuickAccessToolBarAllowLabels)
			options |= RibbonSerializerOptions.QuickAccessToolBarAllowLabels;
		if (QuickAccessToolBarItems)
			options |= RibbonSerializerOptions.QuickAccessToolBarItems;
		if (QuickAccessToolBarLocation)
			options |= RibbonSerializerOptions.QuickAccessToolBarLocation;
		if (QuickAccessToolBarMode)
			options |= RibbonSerializerOptions.QuickAccessToolBarMode;
		if (UserInterfaceDensity)
			options |= RibbonSerializerOptions.UserInterfaceDensity;

		return options;
	}

	/// <summary>
	/// Indicates if <see cref="Ribbon.LayoutMode"/> will be processed when serializing or deserializing the Ribbon.
	/// </summary>
	public bool LayoutMode {
		get => _layoutMode;
		set => SetProperty(ref _layoutMode, value);
	}

	/// <summary>
	/// Indicates if <see cref="Ribbon.IsMinimized"/> will be processed when serializing or deserializing the Ribbon.
	/// </summary>
	public bool MinimizedState {
		get => _minimizedState;
		set => SetProperty(ref _minimizedState, value);
	}

	/// <summary>
	/// Indicates if <see cref="Ribbon.AllowLabelsOnQuickAccessToolBar"/> will be processed when serializing or deserializing the Ribbon.
	/// </summary>
	[DisplayName("QAT allow labels")]
	public bool QuickAccessToolBarAllowLabels {
		get => _quickAccessToolBarAllowLabels;
		set => SetProperty(ref _quickAccessToolBarAllowLabels, value);
	}

	/// <summary>
	/// Indicates if the items displayed in <see cref="Ribbon.QuickAccessToolBar"/> will be processed when serializing or deserializing the Ribbon.
	/// </summary>
	[DisplayName("QAT items")]
	public bool QuickAccessToolBarItems {
		get => _quickAccessToolBarItems;
		set => SetProperty(ref _quickAccessToolBarItems, value);
	}

	/// <summary>
	/// Indicates if <see cref="Ribbon.QuickAccessToolBarLocation"/> will be processed when serializing or deserializing the Ribbon.
	/// </summary>
	[DisplayName("QAT location")]
	public bool QuickAccessToolBarLocation {
		get => _quickAccessToolBarLocation;
		set => SetProperty(ref _quickAccessToolBarLocation, value);
	}

	/// <summary>
	/// Indicates if <see cref="Ribbon.QuickAccessToolBarMode"/> will be processed when serializing or deserializing the Ribbon.
	/// </summary>
	[DisplayName("QAT mode")]
	public bool QuickAccessToolBarMode {
		get => _quickAccessToolBarMode;
		set => SetProperty(ref _quickAccessToolBarMode, value);
	}

	/// <summary>
	/// Indicates if <see cref="Ribbon.UserInterfaceDensity"/> will be processed when serializing or deserializing the Ribbon.
	/// </summary>
	public bool UserInterfaceDensity {
		get => _userInterfaceDensity;
		set => SetProperty(ref _userInterfaceDensity, value);
	}

}
