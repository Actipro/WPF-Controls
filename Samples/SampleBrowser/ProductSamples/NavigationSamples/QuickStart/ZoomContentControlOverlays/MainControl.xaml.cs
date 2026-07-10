namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.ZoomContentControlOverlays;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	private static RoutedCommand? _addPushPin;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();
		CommandBindings.Add(new CommandBinding(AddPushPin, OnAddPushPinExecute));

		zoomContentControl.ZoomToFit();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnAddPushPinExecute(object sender, ExecutedRoutedEventArgs e) {
		// Create a push pin and set the canvas location, anchoring the bottom-left point
		var pushPin = new PushPin();
		var point = Mouse.GetPosition(map);
		Canvas.SetLeft(pushPin, point.X);
		Canvas.SetBottom(pushPin, map.ActualHeight - point.Y);

		zoomContentControl.Overlays.Add(pushPin);
		e.Handled = true;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="RoutedCommand"/> that is used to add a push pin.
	/// </summary>
	public static RoutedCommand AddPushPin
		=> _addPushPin ??= new RoutedCommand(nameof(AddPushPin), typeof(MainControl));

}
