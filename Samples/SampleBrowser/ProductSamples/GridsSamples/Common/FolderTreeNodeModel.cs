using System.Windows.Media.Imaging;

namespace ActiproSoftware.ProductSamples.GridsSamples.Common;

/// <summary>
/// Provides a tree node model implementation for folders that can toggle the image based on expansion state.
/// </summary>
public class FolderTreeNodeModel : ThreeStateCheckableTreeNodeModel {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public FolderTreeNodeModel() {
		UpdateImageSource();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Updates the folder image source based on the expansion state.
	/// </summary>
	private void UpdateImageSource() {
		var imageUri = new Uri(IsExpanded ? "/Images/Icons/FolderOpen16.png" : "/Images/Icons/FolderClosed16.png", UriKind.Relative);
		ImageSource = new BitmapImage(imageUri);
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void OnPropertyChanged(PropertyChangedEventArgs e) {
		base.OnPropertyChanged(e);

		switch (e.PropertyName) {
			case nameof(IsExpanded):
				UpdateImageSource();
				break;
		}
	}

	/// <summary>
	/// The folder path.
	/// </summary>
	public string? Path { get; set; }

}
