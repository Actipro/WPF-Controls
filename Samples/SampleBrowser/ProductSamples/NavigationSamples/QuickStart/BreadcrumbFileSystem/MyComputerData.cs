using ActiproSoftware.Windows;
using System.Windows.Media.Imaging;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.BreadcrumbFileSystem;

/// <summary>
/// Holds the root My Computer data.
/// </summary>
public class MyComputerData {

	public const string DefaultName = "My Computer";

	private DeferrableObservableCollection<DriveData>? _drives;
	private BitmapSource? _imageSource;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The drives.
	/// </summary>
	public DeferrableObservableCollection<DriveData> Drives {
		get {
			if (_drives is null) {
				_drives = [];

				try {
					foreach (var driveInfo in DriveInfo.GetDrives())
						_drives.Add(new DriveData(driveInfo));
				}
				catch (IOException) { } // Ignore
				catch (UnauthorizedAccessException) { } // Ignore
			}

			return _drives;
		}
	}

	/// <summary>
	/// The image source.
	/// </summary>
	public BitmapSource ImageSource
		=> _imageSource ??= new BitmapImage(new Uri("/Images/Icons/Computer16.png", UriKind.Relative));

	/// <summary>
	/// The name.
	/// </summary>
	public string Name
		=> DefaultName;

}
