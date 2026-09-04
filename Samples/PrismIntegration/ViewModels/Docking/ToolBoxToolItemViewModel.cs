using System.Windows.Media.Imaging;

namespace ActiproSoftware.Windows.PrismIntegration.ViewModels;

/// <summary>
/// Represents a tool view-model for the sample.
/// </summary>
/// <remarks>
/// This view-model derives from a base class that initializes the <c>ToolWindow</c> from instance properties.
/// </remarks>
public class ToolboxToolItemViewModel : ToolItemViewModel {

	public const string SerializationIdText = "ToolboxToolWindow";

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public ToolboxToolItemViewModel() {
		DefaultDockSide = ToolItemDockSide.Left;
		ImageSource = new BitmapImage(new Uri("/Resources/Images/Toolbox16.png", UriKind.Relative));
		SerializationId = SerializationIdText;
		State = ToolItemState.AutoHide;
		Title = "Toolbox";
	}

}
