using ActiproSoftware.Windows.Controls.Navigation;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.BreadcrumbFileSystem;

/// <summary>
/// This class includes helper methods for working with the Breadcrumb ConvertItem event.
/// </summary>
public static class ConvertItemHelper {

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Reports an error to the user.
	/// </summary>
	/// <param name="text">The text.</param>
	private static void ReportError(string text)
		=> MessageBox.Show(text, "Breadcrumb Sample", MessageBoxButton.OK, MessageBoxImage.Error);

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Returns the path for the specified item.
	/// </summary>
	/// <param name="item">The item.</param>
	public static string GetPath(object? item) {
		return item switch {
			MyComputerData myComputer => myComputer.Name,
			DriveData driveData => driveData.Info.Name,
			DirectoryData directoryData => directoryData.Info.FullName,
			_ => string.Empty
		};
	}

	/// <summary>
	/// Returns the trail for the specified item.
	/// </summary>
	/// <param name="rootItem">The root item.</param>
	/// <param name="item">The item.</param>
	public static IList? GetTrail(object? rootItem, object? item) {
		var path = GetPath(item);
		return GetTrail(rootItem, path);
	}

	/// <summary>
	/// Returns the trail for the specified path.
	/// </summary>
	/// <param name="rootItem">The root item.</param>
	/// <param name="path">The path.</param>
	public static IList? GetTrail(object? rootItem, string? path) {
		// Make sure the specified path is valid
		if (string.IsNullOrEmpty(path))
			return null;

		// If the root item was not passed, then we cannot build a trail
		if (rootItem is not MyComputerData myComputerData)
			return null;

		// Break the path up based on the available path separators
		var pathEntries = path!.Split([Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar], StringSplitOptions.RemoveEmptyEntries);
		if (pathEntries is not { Length: > 0 })
			return null;

		// Start to build the trail
		var trail = new List<object> { myComputerData };

		if (string.Compare(MyComputerData.DefaultName, pathEntries[0], StringComparison.CurrentCultureIgnoreCase) != 0) {
			// For the remaining entries in the path, we will search the child items for a match at each level. If at any
			//   point we don't find a match, then we will need to cancel the conversion.
			if (!Directory.Exists(path))
				return null;

			// The split above will remove the backslash, which we need for the comparison for drives below.
			var driveEntry = pathEntries[0] + @"\";

			// The first entry should be a drive, so we will start there
			DriveData? driveData = null;
			for (var driveIndex = 0; driveIndex < myComputerData.Drives.Count; driveIndex++) {
				// Get the next DriveData and see if it's a match, if so the exit the loop
				driveData = myComputerData.Drives[driveIndex];
				if (string.Compare(driveData.Info.Name, driveEntry, StringComparison.CurrentCultureIgnoreCase) == 0)
					break;

				// Set to null, because we didn't find a match and we want driveData to be null in that case
				driveData = null;
			}

			// If we found the drive, then add it to the trail and continue. Otherwise, there's a problem and we have
			//   failed to convert.
			if (driveData is not null) {
				trail.Add(driveData);

				// See if there are more items, which should be all directories
				if (pathEntries.Length > 1) {

					// We need to get the first directory directly from the drive object
					DirectoryData? directoryData = null;
					for (var directoryIndex = 0; directoryIndex < driveData.Directories.Count; directoryIndex++) {
						// Get the next DirectoryData and see if it's a match, if so the exit the loop
						directoryData = driveData.Directories[directoryIndex];
						if (string.Compare(directoryData.Info.Name, pathEntries[1], StringComparison.CurrentCultureIgnoreCase) == 0)
							break;

						// Set to null, because we didn't find a match and we want directoryData to be null in that case
						directoryData = null;
					}

					// If we found the directory, then add it to the trail and continue. Otherwise, there's a problem and
					//   we have failed to convert.
					if (directoryData is not null) {
						trail.Add(directoryData);

						// We are now looking for the remaining directories, which we can do in this loop
						for (var index = 2; index < pathEntries.Length; index++) {
							var found = false;
							for (var directoryIndex = 0; directoryIndex < directoryData.Directories.Count; directoryIndex++) {
								// Get the next DirectoryData and see if it's a match, if so the exit the loop
								var childDirectoryData = directoryData.Directories[directoryIndex];
								if (string.Compare(childDirectoryData.Info.Name, pathEntries[index], StringComparison.CurrentCultureIgnoreCase) == 0) {
									found = true;
									trail.Add(childDirectoryData);
									directoryData = childDirectoryData;
									break;
								}
							}

							if (!found)
								return null;
						}

						return trail;
					}
				}
				else {
					return trail;
				}
			}
		}
		else {
			return trail;
		}

		return null;
	}

	/// <summary>
	/// Handles the <see cref="Breadcrumb.ConvertItem"/> event.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The event data.</param>
	public static void HandleConvertItem(object? sender, BreadcrumbConvertItemEventArgs e) {
		switch (e.TargetType) {
			case BreadcrumbConvertItemTargetType.Path:
				// Convert either the item or the trail to a path
				var item = e.Item;
				if ((item is null) && (e.Trail is { Count: > 0 }))
					item = e.Trail[e.Trail.Count - 1];

				e.Path = GetPath(item);
				break;
			case BreadcrumbConvertItemTargetType.Trail:
				IList? trail = null;
				if (e.Path is not null)
					trail = GetTrail(e.RootItem, e.Path);
				else if (e.Item is not null)
					trail = GetTrail(e.RootItem, e.Item);

				if (trail is null) {
					ReportError("The specified path could not be found.");
					return;
				}

				e.Trail = trail;
				break;
			default:
				throw new NotImplementedException("Unsupported Breadcrumb target type");
		}
	}

}
