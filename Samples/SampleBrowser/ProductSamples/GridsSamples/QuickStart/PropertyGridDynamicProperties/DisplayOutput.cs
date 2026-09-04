namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridDynamicProperties;

/// <summary>
/// Specifies where output should be displayed.
/// </summary>
public class DisplayOutput : ObservableObjectBase, IDynamicPropertyStateProvider {

	private int _height = DefaultHeight;
	private string _location = LocationPrimary;
	private ScreenOrientation _orientation;
	private DisplayTarget _target;
	private int _width = DefaultWidth;

	private const int DefaultWidth = 1920;
	private const int DefaultHeight = 1080;

	private const string LocationPrimary = "Primary";
	private const string LocationSecondary = "Secondary";
	private const string LocationTertiary = "Tertiary";

	private const string LocationBottomLeft = "Bottom-left";
	private const string LocationBottomRight = "Bottom-right";
	private const string LocationTopLeft = "Top-left";
	private const string LocationTopRight = "Top-right";

	private readonly string[] PaneLocations = [LocationTopLeft, LocationTopRight, LocationBottomRight, LocationBottomLeft];
	private readonly string[] ScreenLocations = [LocationPrimary, LocationSecondary, LocationTertiary];

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Updates the location.
	/// </summary>
	private void UpdateLocation() {
		Location = (_target == DisplayTarget.Screen)
			? LocationPrimary
			: LocationTopRight;
	}

	/// <summary>
	/// Updates the size properties.
	/// </summary>
	private void UpdateSize() {
		if (_target == DisplayTarget.Screen) {
			Width = (_orientation == ScreenOrientation.Landscape ? DefaultWidth : DefaultHeight);
			Height = (_orientation == ScreenOrientation.Landscape ? DefaultHeight : DefaultWidth);
		}
		else {
			Width = DefaultWidth / 4;
			Height = DefaultHeight / 4;
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="IDynamicPropertyStateProvider.GetPropertyHasStandardValues"/>
	public bool GetPropertyHasStandardValues(string propertyName) {
		return propertyName switch {
			nameof(Location) => true,
			_ => false
		};
	}

	/// <inheritdoc cref="IDynamicPropertyStateProvider.GetPropertyReadOnly"/>
	public bool GetPropertyReadOnly(string propertyName) {
		return propertyName switch {
			nameof(Height) or nameof(Width) => (Target != DisplayTarget.Pane),
			_ => false
		};
	}

	/// <inheritdoc cref="IDynamicPropertyStateProvider.GetPropertyStandardValues"/>
	public IEnumerable<object>? GetPropertyStandardValues(string propertyName) {
		return propertyName switch {
			nameof(Location) => (Target == DisplayTarget.Screen ? ScreenLocations : PaneLocations),
			_ => null
		};
	}

	/// <inheritdoc cref="IDynamicPropertyStateProvider.GetPropertyVisibility"/>
	public bool GetPropertyVisibility(string propertyName) {
		return propertyName switch {
			"ScreenProfile" or nameof(Orientation) => (Target == DisplayTarget.Screen),
			_ => true
		};
	}

	/// <summary>
	/// The display target.
	/// </summary>
	[Description("The display target, which is either a full screen or a pane within the current monitor.")]
	public DisplayTarget Target {
		get => _target;
		set {
			if (SetProperty(ref _target, value)) {
				OnPropertyChanged(nameof(Orientation));

				UpdateLocation();
				UpdateSize();
			}
		}
	}

	/// <summary>
	/// The location.
	/// </summary>
	[Description("The location of the screen (which screen) or pane (which corner).  This property has dynamic standard values based on the Target property selection.")]
	public string Location {
		get => _location;
		set => SetProperty(ref _location, value);
	}

	/// <summary>
	/// The screen orientation.
	/// </summary>
	[Description("The screen orientation.  This property is only visible when the Target is a Screen.")]
	public ScreenOrientation Orientation {
		get => _orientation;
		set {
			if (SetProperty(ref _orientation, value))
				UpdateSize();
		}
	}

	/// <summary>
	/// The width.
	/// </summary>
	[Description("The width of the screen or pane.  This property is read-only when the Target is a Screen.")]
	public int Width {
		get => _width;
		set => SetProperty(ref _width, value);
	}

	/// <summary>
	/// The height.
	/// </summary>
	[Description("The height of the screen or pane.  This property is read-only when the Target is a Screen.")]
	public int Height {
		get => _height;
		set => SetProperty(ref _height, value);
	}

}
