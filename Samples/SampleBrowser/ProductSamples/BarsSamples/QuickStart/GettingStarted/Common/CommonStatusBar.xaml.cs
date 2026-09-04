using ActiproSoftware.SampleBrowser;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.GettingStarted.Common;

/// <summary>
/// Interaction logic for CommonStatusBar.xaml
/// </summary>
public partial class CommonStatusBar : StatusBar {

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="SampleCodePath"/> dependency property.
	/// </summary>
	public static readonly DependencyProperty SampleCodePathProperty
		= DependencyProperty.Register(nameof(SampleCodePath), typeof(string), typeof(CommonStatusBar), new FrameworkPropertyMetadata(defaultValue: null, OnSampleCodePathPropertyValueChanged));

	#endregion DependencyProperties

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public CommonStatusBar() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private static void OnSampleCodePathPropertyValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
		var commonStatusBar = (CommonStatusBar)obj;
		var viewItemInfo = ApplicationViewModel.Current?.ViewItemInfo;

		// Update the parameter passed when opening sample code
		var newValue = (e.NewValue as string);
		if ((newValue is null) || (viewItemInfo?.Path is null)) {
			commonStatusBar.viewCodeButton.Command = null;
			commonStatusBar.viewCodeButton.CommandParameter = null;
		}
		else {
			// The ViewItemInfo.Path will be pointed to the main control at the root of the "GettingStarted" series
			commonStatusBar.viewCodeButton.Command = ApplicationViewModel.Current?.OpenSampleCodeCommand;
			commonStatusBar.viewCodeButton.CommandParameter = viewItemInfo.Path.Replace("/MainControl", newValue);
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The relative path of the sample code for the current step.
	/// </summary>
	/// <value>A string value (e.g., <c>/Step01/MainWindow</c>).</value>
	public string? SampleCodePath {
		get => (string)GetValue(SampleCodePathProperty);
		set => SetValue(SampleCodePathProperty, value);
	}

}
