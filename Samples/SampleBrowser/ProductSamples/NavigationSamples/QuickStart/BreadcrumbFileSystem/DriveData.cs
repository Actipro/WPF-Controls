using ActiproSoftware.Windows;
using System.Windows.Media.Imaging;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.BreadcrumbFileSystem;

/// <summary>
/// Holds data relating to a drive.
/// </summary>
/// <param name="info">The drive information.</param>
public class DriveData(DriveInfo info) {

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
					if (Info.RootDirectory is { } rootDirectory) {
						foreach (var directoryInfo in rootDirectory.GetDirectories())
							_directories.Add(new DirectoryData(directoryInfo));
					}
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
		=> _imageSource ??= ShellIconHelper.GetSystemImageSource(Info.Name);

	/// <summary>
	/// The info.
	/// </summary>
	public DriveInfo Info { get; } = info ?? throw new ArgumentNullException(nameof(info));

	/// <summary>
	/// The name.
	/// </summary>
	public string Name {
		get {
			var name = Info.Name.TrimEnd('\\');
			return Info.DriveType switch {
				DriveType.Fixed => string.Format(CultureInfo.CurrentCulture, "Local Disk ({0})", name),
				DriveType.CDRom => string.Format(CultureInfo.CurrentCulture, "CD Drive ({0})", name),
				DriveType.Network => string.Format(CultureInfo.CurrentCulture, "Network Drive ({0})", name),
				DriveType.Ram => string.Format(CultureInfo.CurrentCulture, "RAM Disk ({0})", name),
				DriveType.Removable => string.Format(CultureInfo.CurrentCulture, "Removable Disk ({0})", name),
				_ => string.Format(CultureInfo.CurrentCulture, "Unknown Disk ({0})", name)
			};
		}
	}

}
