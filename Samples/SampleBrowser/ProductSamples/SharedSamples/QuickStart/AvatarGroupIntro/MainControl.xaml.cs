using ActiproSoftware.Windows.Input;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.SharedSamples.QuickStart.AvatarGroupIntro;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	public DelegateCommand<object> AvatarClickedCommand { get; }
		= new DelegateCommand<object>(p => MessageBox.Show($"The avatar for '{p}' was clicked.", "Avatar Click", MessageBoxButton.OK, MessageBoxImage.Information));

	public DelegateCommand<object> AvatarGroupClickedCommand { get; }
		= new DelegateCommand<object>(_ => MessageBox.Show($"The avatar group was clicked.", "Avatar Group Click", MessageBoxButton.OK, MessageBoxImage.Information));

}
