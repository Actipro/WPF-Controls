using ActiproSoftware.Windows.Input;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.CustomTitleBarContent;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	private DelegateCommand<object>? _searchCommand;

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

	/// <summary>
	/// The <see cref="ICommand"/> that can be used to open the drop-down menu.
	/// </summary>
	public ICommand SearchCommand {
		get => _searchCommand ??= new DelegateCommand<object>(_ => {
			MessageBox.Show("Search button clicked.");
		});
	}

}
