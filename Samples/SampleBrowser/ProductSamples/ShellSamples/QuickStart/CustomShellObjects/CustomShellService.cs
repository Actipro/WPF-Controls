using ActiproSoftware.Shell;
using System.Windows.Media.Imaging;

namespace ActiproSoftware.ProductSamples.ShellSamples.QuickStart.CustomShellObjects;

/// <summary>
/// Provides an <see cref="IShellService"/> implementation that can inject custom shell objects.
/// </summary>
public class CustomShellService : WindowsShellService {

	private const string CloudStorageParsingNameSeparator = @"\";
	private const string CloudStorageParsingNameRoot = "cloud:";

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Combines multiple relative parsing name parts into a full parsing name with separators between parts.
	/// </summary>
	/// <param name="relativeParsingNames">The relative parsing name parts to be combined.</param>
	private static string? CombineCloudStorageParsingNames(params string[] relativeParsingNames) {
		if (relativeParsingNames is not { Length: > 0 })
			return null;

		var parsingName = relativeParsingNames[0].TrimEnd(CloudStorageParsingNameSeparator.ToCharArray());
		for (var i = 1; i < relativeParsingNames.Length; i++)
			parsingName += CloudStorageParsingNameSeparator + relativeParsingNames[i].Trim(CloudStorageParsingNameSeparator.ToCharArray());

		return parsingName;
	}

	/// <summary>
	/// Creates a custom shell object that represents a cloud storage folder.
	/// </summary>
	/// <param name="name">The name.</param>
	/// <param name="parsingName">The parsing name used to uniquely identify the custom shell object.</param>
	/// <param name="relativeParsingName">Optionally define the relative parsing name of the shell object used as the individual part of a full parsing name, if different than <paramref name="name"/>.</param>
	/// <param name="editingName">Optionally define the user-friendly editing name of the shell object, if different than <paramref name="parsingName"/>.</param>
	private CustomShellObject CreateCloudStorageFolder(string name, string? parsingName, string? relativeParsingName = null, string? editingName = null) {
		var folder = new CustomShellObject(this, ShellObjectKind.VirtualSpecialFolder, name, parsingName, relativeParsingName, editingName) {
			SmallIcon = new BitmapImage(new Uri("/Images/Icons/FolderClosed16.png", UriKind.Relative)),
			SortOrder = -1
		};

		return folder;
	}

	/// <summary>
	/// Tests if a shell object represents a cloud storage folder.
	/// </summary>
	private static bool IsCloudStorageFolder(IShellObject shellObject) {
		if ((shellObject is CustomShellObject) && IsCloudStorageParsingName(shellObject.ParsingName))
			return true;

		return false;
	}

	/// <summary>
	/// Tests if a parsing name for a shell object represents a cloud storage folder.
	/// </summary>
	/// <param name="parsingName">The parsing name to examine.</param>
	private static bool IsCloudStorageParsingName(string? parsingName) {
		if (parsingName?.StartsWith(CloudStorageParsingNameRoot) == true)
			return true;

		return false;
	}

	/// <summary>
	/// Splits a parsing name into relative parsing name parts.
	/// </summary>
	/// <param name="parsingName">The full parsing name.</param>
	/// <returns>An <see cref="IList{string}"/> of the relative parsing names; or an empty <see cref="IList{string}"/> if the parsing name is not recognized.</returns>
	private static IList<string> SplitCloudStorageParsingName(string? parsingName) {
		if ((parsingName is null) || !IsCloudStorageParsingName(parsingName))
			return [];

		return [..parsingName.Split([CloudStorageParsingNameSeparator], StringSplitOptions.RemoveEmptyEntries)];
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override IList<IShellObject> CreateObjectChildren(IShellObject parentShellObject) {
		var results = base.CreateObjectChildren(parentShellObject);

		// Remove any results that are special folders
		for (var index = results.Count - 1; index >= 0; index--) {
			if (results[index].SpecialFolderKind != SpecialFolderKind.None)
				results.RemoveAt(index);
		}

		if (parentShellObject.SpecialFolderKind == SpecialFolderKind.Computer) {
			// Add a root folder
			results.Add(CreateObjectForParsingName(CloudStorageParsingNameRoot)!);
		}
		else if (IsCloudStorageFolder(parentShellObject) && (parentShellObject.ParsingName is not null)) {

			var relativeParsingNames = SplitCloudStorageParsingName(parentShellObject.ParsingName);
			if (relativeParsingNames.Count == 1) {
				// Add additional child folders below the root cloud folder (e.g., cloud:\NewFolder)
				foreach (var folderName in new string[] { "Private", "Public" })
					results.Add(CreateObjectForParsingName(CombineCloudStorageParsingNames(parentShellObject.ParsingName, folderName))!);
			}
			else if (relativeParsingNames.Count == 2) {
				// Add additional child folders below the first level cloud folders (e.g., cloud:\FirstLevel\NewFolder)
				for (int i = 0; i < 10; i++)
					results.Add(CreateObjectForParsingName(CombineCloudStorageParsingNames(parentShellObject.ParsingName, "Folder" + i))!);
			}

		}

		return results;
	}

	/// <inheritdoc/>
	public override IShellObject? CreateObjectForParsingName(string? parsingName) {
		// Does the parsing name refer to a custom folder?
		if (IsCloudStorageParsingName(parsingName)) {
			var relativeParsingNames = SplitCloudStorageParsingName(parsingName);
			if (relativeParsingNames.Count == 1) {
				// Create root cloud storage folder.
				//   A relative parsing name is provided since the name of the folder, "Custom Cloud Storage", differs from the name used in the path, "cloud:".
				//   A specific editing name is used so that the root folder displays as "cloud:\" in a path text box instead of "cloud:"
				var editingName = CloudStorageParsingNameRoot + CloudStorageParsingNameSeparator;
				return CreateCloudStorageFolder("Custom Cloud Storage (" + CloudStorageParsingNameRoot + ")", parsingName, relativeParsingNames[0], editingName);
			}
			else if (relativeParsingNames.Count > 1) {
				// Create nested cloud storage folder
				var name = relativeParsingNames.Last();
				return CreateCloudStorageFolder(name, parsingName);
			}

			// Invalid parsing name
			return null;
		}

		return base.CreateObjectForParsingName(parsingName);
	}

	/// <inheritdoc/>
	public override string? GetFullPath(IShellObject shellObject, string pathSeparator) {
		if (IsCloudStorageFolder(shellObject)) {
			// The full path should be all of the relative parsing names separated by the given path separator
			var relativeParsingNames = SplitCloudStorageParsingName(shellObject.ParsingName);
			return string.Join(pathSeparator, relativeParsingNames);
		}

		return base.GetFullPath(shellObject, pathSeparator);
	}

}
