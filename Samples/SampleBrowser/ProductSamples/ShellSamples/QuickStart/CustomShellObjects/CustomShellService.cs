using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;
using ActiproSoftware.Shell;

namespace ActiproSoftware.ProductSamples.ShellSamples.QuickStart.CustomShellObjects {
	
	/// <summary>
	/// Provides an <see cref="IShellService"/> implementation that can inject custom shell objects.
	/// </summary>
	public class CustomShellService : WindowsShellService {

		private const string CloudStorageParsingNameSeparator = @"\";
		private const string CloudStorageParsingNameRoot = "cloud:";
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Combines multiple relative parsing name parts into a full parsing name with separators between parts.
		/// </summary>
		/// <param name="relativeParsingNames">The relative parsing name parts to be combined.</param>
		/// <returns>The combined full parsing name.</returns>
		private string CombineCloudStorageParsingNames(params string[] relativeParsingNames) {
			if (relativeParsingNames is null || relativeParsingNames.Length == 0)
				return null;

			string parsingName = relativeParsingNames[0].TrimEnd(CloudStorageParsingNameSeparator.ToCharArray());
			for (int i = 1; i < relativeParsingNames.Length; i++) {
				parsingName += CloudStorageParsingNameSeparator + relativeParsingNames[i].Trim(CloudStorageParsingNameSeparator.ToCharArray());
			}

			return parsingName;
		}

		/// <summary>
		/// Creates a custom shell object that represents a cloud storage folder.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="parsingName">The parsing name used to uniquely identify the custom shell object.</param>
		/// <param name="relativeParsingName">Optionally define the relative parsing name of the shell object used as the individual part of a full parsing name, if different than <paramref name="name"/>.</param>
		/// <param name="editingName">Optionally define the user-friendly editing name of the shell object, if different than <paramref name="parsingName"/>.</param>
		/// <returns>The <see cref="IShellObject"/> that was created.</returns>
		private IShellObject CreateCloudStorageFolder(string name, string parsingName, string relativeParsingName = null, string editingName = null) {
			var folder = new CustomShellObject(this, ShellObjectKind.VirtualSpecialFolder, name, parsingName, relativeParsingName, editingName);
			folder.SmallIcon = new BitmapImage(new Uri("/Images/Icons/FolderClosed16.png", UriKind.Relative));
			folder.SortOrder = -1;
			
			return folder;
		}

		/// <summary>
		/// Tests if a shell object represents a cloud storage folder.
		/// </summary>
		/// <param name="shellObject">The <see cref="IShellObject"/> to examine.</param>
		/// <returns><c>true</c> when the shell object is a cloud storage folder; otherwise <c>false</c>.</returns>
		private bool IsCloudStorageFolder(IShellObject shellObject) {
			if (shellObject is CustomShellObject && IsCloudStorageParsingName(shellObject.ParsingName))
				return true;

			return false;
		}

		/// <summary>
		/// Tests if a parsing name for a shell object represents a cloud storage folder.
		/// </summary>
		/// <param name="parsingName">The parsing name to examine.</param>
		/// <returns><c>true</c> when the parsing name is for a cloud storage folder; otherwise <c>false</c>.</returns>
		private bool IsCloudStorageParsingName(string parsingName) {
			if (parsingName != null && parsingName.StartsWith(CloudStorageParsingNameRoot))
				return true;

			return false;
		}

		/// <summary>
		/// Splits a parsing name into relative parsing name parts.
		/// </summary>
		/// <param name="parsingName">The full parsing name.</param>
		/// <returns>A <see cref="List{string}"/> of the relative parsing names; or an empty <see cref="List{string}"/> if the parsing name is not recognized.</returns>
		private IList<string> SplitCloudStorageParsingName(string parsingName) {
			if (!IsCloudStorageParsingName(parsingName))
				return new List<string>(0);

			return new List<string>(parsingName.Split(new string[] { CloudStorageParsingNameSeparator }, StringSplitOptions.RemoveEmptyEntries));
		}


		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Creates the child <see cref="IShellObject"/> collection for the specified parent <see cref="IShellObject"/>.
		/// </summary>
		/// <param name="parentShellObject">The parent <see cref="IShellObject"/> to examine.</param>
		/// <returns>The child <see cref="IShellObject"/> collection for the specified parent <see cref="IShellObject"/>.</returns>
		public override IList<IShellObject> CreateObjectChildren(IShellObject parentShellObject) {
			var results = base.CreateObjectChildren(parentShellObject);
			
			// Remove any results that are special folders
			for (var index = results.Count - 1; index >= 0; index--) {
				if (results[index].SpecialFolderKind != SpecialFolderKind.None)
					results.RemoveAt(index);
			}

			if (parentShellObject.SpecialFolderKind == SpecialFolderKind.Computer) {
				// Add a root folder
				results.Add(this.CreateObjectForParsingName(CloudStorageParsingNameRoot));
			}
			else if (IsCloudStorageFolder(parentShellObject)) {
				
				var relativeParsingNames = SplitCloudStorageParsingName(parentShellObject.ParsingName);
				if (relativeParsingNames.Count == 1) {
					// Add additional child folders below the root cloud folder (e.g. cloud:\NewFolder)
					foreach (string folderName in new string[] { "Private", "Public" })
						results.Add(this.CreateObjectForParsingName(CombineCloudStorageParsingNames(parentShellObject.ParsingName, folderName)));
				}
				else if (relativeParsingNames.Count == 2) {
					// Add additional child folders below the first level cloud folders (e.g. cloud:\FirstLevel\NewFolder)
					for (int i = 0; i < 10; i++)
						results.Add(this.CreateObjectForParsingName(CombineCloudStorageParsingNames(parentShellObject.ParsingName, "Folder" + i)));
				}

			}

			return results;
		}

		/// <summary>
		/// Creates the <see cref="IShellObject"/> for the specified full parsing name (commonly the same as the file system path).
		/// </summary>
		/// <param name="parsingName">The parsing name to examine.</param>
		/// <returns>The <see cref="IShellObject"/> for the specified full parsing name.</returns>
		public override IShellObject CreateObjectForParsingName(string parsingName) {
			// Does the parsing name refer to a custom folder?
			if (IsCloudStorageParsingName(parsingName)) {
				var relativeParsingNames = SplitCloudStorageParsingName(parsingName);
				if (relativeParsingNames.Count == 1) {
					// Create root cloud storage folder.
					// A relative parsing name is provided since the name of the folder, "Custom Cloud Storage", differs from the name used in the path, "cloud:".
					// A specific editing name is used so that the root folder displays as "cloud:\" in a path text box instead of "cloud:"
					string editingName = CloudStorageParsingNameRoot + CloudStorageParsingNameSeparator;
					return this.CreateCloudStorageFolder("Custom Cloud Storage (" + CloudStorageParsingNameRoot + ")", parsingName, relativeParsingNames[0], editingName);
				}
				else if (relativeParsingNames.Count > 1) {
					// Create nested cloud storage folder
					string name = relativeParsingNames.Last();
					return this.CreateCloudStorageFolder(name, parsingName);
				}

				// Invalid parsing name
				return null;
			}

			return base.CreateObjectForParsingName(parsingName);
		}

		/// <summary>
		/// Returns the full path of the specified <see cref="IShellObject"/>.
		/// </summary>
		/// <param name="shellObject">The <see cref="IShellObject"/> to examine.</param>
		/// <param name="pathSeparator">The path separator.</param>
		/// <returns>The full path of the specified <see cref="IShellObject"/>.</returns>
		public override string GetFullPath(IShellObject shellObject, string pathSeparator) {
			if (IsCloudStorageFolder(shellObject)) {
				// The full path should be all of the relative parsing names separated by the given path separator
				var relativeParsingNames = SplitCloudStorageParsingName(shellObject.ParsingName);
				return string.Join(pathSeparator, relativeParsingNames);
			}
			
			return base.GetFullPath(shellObject, pathSeparator);
		}
	}

}