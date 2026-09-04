using ActiproSoftware.Windows;
using System.Windows.Media.Imaging;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.BreadcrumbFileSystem;

/// <summary>
/// Holds data relating to a directory.
/// </summary>
/// <param name="info">The directory information.</param>
public class DirectoryData(DirectoryInfo info) {

	private DeferrableObservableCollection<DirectoryData>? _directories;
	private BitmapSource? _imageSource;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The drives.
	/// </summary>
	public DeferrableObservableCollection<DirectoryData> Directories {
		get {
			if (_directories is null) {
				_directories = [];

				try {
					foreach (var directoryInfo in Info.GetDirectories())
						_directories.Add(new DirectoryData(directoryInfo));
				}
				catch (DirectoryNotFoundException) { } // Ignore
				catch (IOException) { } // Ignore
				catch (UnauthorizedAccessException) { } // Ignore
			}

			return _directories;
		}
	}

	/// <summary>
	/// The image source.
	/// </summary>
	public BitmapSource ImageSource
		=> _imageSource ??= ShellIconHelper.GetSystemImageSource(Info.FullName);

	/// <summary>
	/// The info.
	/// </summary>
	public DirectoryInfo Info { get; } = info ?? throw new ArgumentNullException(nameof(info));

	/// <summary>
	/// The name.
	/// </summary>
	public string Name
		=> Info.Name;

}
