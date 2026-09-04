using ActiproSoftware.ProductSamples.GridsSamples.Common;
using ActiproSoftware.Windows.Input;
using System.Windows.Media.Imaging;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.TreeListBoxChecking;

/// <summary>
/// Provides a tree node model implementation for tracking optional fields.
/// </summary>
public class FieldTreeNodeModel : CheckableTreeNodeModel {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public FieldTreeNodeModel() {
		var imageUri = new Uri("/Images/Icons/New16.png", UriKind.Relative);
		ImageSource = new BitmapImage(imageUri);

		IsCheckable = true;

		ShowDialogCommand = new DelegateCommand<object>(_ => {
			MessageBox.Show(string.Format(CultureInfo.CurrentCulture, "Show custom dialog here for item '{0}'.", Name), "Button Clicked");
		});
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="ICommand"/> that can be used to show a dialog.
	/// </summary>
	public ICommand ShowDialogCommand { get; }

}
