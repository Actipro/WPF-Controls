using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using ActiproSoftware.Windows.Controls.Navigation;

#if WPF
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;
#endif

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.BreadcrumbFileSystem {
	/// <summary>
	/// This class includes helper methods for working with the Breadcrumb ConvertItem event.
	/// </summary>
	public static class ConvertItemHelper {
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Reports an error to the user.
		/// </summary>
		/// <param name="text">The text.</param>
		private static void ReportError(string text) {
			MessageBox.Show(text, "Breadcrumb Sample", MessageBoxButton.OK, MessageBoxImage.Error);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the path for the specified item.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns></returns>
		public static string GetPath(object item) {
			if (item is MyComputerData) {
				return "My Computer";
			}
			else {
				DriveData driveData = item as DriveData;
				if (null != driveData) {
					return driveData.Info.Name;
				}
				else {
					DirectoryData directoryData = item as DirectoryData;
					if (null != directoryData)
						return directoryData.Info.FullName;
				}
			}

			return string.Empty;
		}

		/// <summary>
		/// Gets the trail for the specified item.
		/// </summary>
		/// <param name="rootItem">The root item.</param>
		/// <param name="item">The item.</param>
		/// <returns></returns>
		public static IList GetTrail(object rootItem, object item) {
			string path = GetPath(item);
			return GetTrail(rootItem, path);
		}

		/// <summary>
		/// Gets the trail for the specified path.
		/// </summary>
		/// <param name="rootItem">The root item.</param>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		public static IList GetTrail(object rootItem, string path) {
			// Make sure the specified path is valid
			if (string.IsNullOrEmpty(path))
				return null;

			// If the root item was not passed, then we cannot build a trail
			MyComputerData myComputerData = rootItem as MyComputerData;
			if (null == myComputerData)
				return null;

			// Break the path up based on the available path separators
			string[] pathEntries = path.Split(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar },
				StringSplitOptions.RemoveEmptyEntries);
			if (null == pathEntries || 0 == pathEntries.Length)
				return null;

			// Start to build the trail
			List<object> trail = new List<object>();
			trail.Add(myComputerData);

			if (0 != string.Compare("My Computer", pathEntries[0], StringComparison.CurrentCultureIgnoreCase)) {
				// For the remaining entries in the path, we will search the child items for a match at each level. If at any
				//   point we don't find a match, then we will need to cancel the conversion.
				//
				if (!Directory.Exists(path))
					return null;

				// The split above will remove the backslash, which we need for the comparison for drives below.
				string driveEntry = pathEntries[0] + "\\";

				// The first entry should be a drive, so we will start there
				DriveData driveData = null;
				for (int driveIndex = 0; driveIndex < myComputerData.Drives.Count; driveIndex++) {
					// Get the next DriveData and see if it's a match, if so the exit the loop
					driveData = myComputerData.Drives[driveIndex];
					if (0 == string.Compare(driveData.Info.Name, driveEntry, StringComparison.CurrentCultureIgnoreCase))
						break;

					// Set to null, because we didn't find a match and we want driveData to be null in that case
					driveData = null;
				}

				// If we found the drive, then add it to the trail and continue. Otherwise, there's a problem and we have
				//   failed to convert.
				if (null != driveData) {
					trail.Add(driveData);

					// See if there are more items, which should be all directories
					if (pathEntries.Length > 1) {

						// We need to get the first directory directly from the drive object
						DirectoryData directoryData = null;
						for (int directoryIndex = 0; directoryIndex < driveData.Directories.Count; directoryIndex++) {
							// Get the next DirectoryData and see if it's a match, if so the exit the loop
							directoryData = driveData.Directories[directoryIndex];
							if (0 == string.Compare(directoryData.Info.Name, pathEntries[1], StringComparison.CurrentCultureIgnoreCase))
								break;

							// Set to null, because we didn't find a match and we want directoryData to be null in that case
							directoryData = null;
						}

						// If we found the directory, then add it to the trail and continue. Otherwise, there's a problem and
						//   we have failed to convert.
						if (null != directoryData) {
							trail.Add(directoryData);

							// We are now looking for the remaining directories, which we can do in this loop
							for (int index = 2; index < pathEntries.Length; index++) {
								bool found = false;
								for (int directoryIndex = 0; directoryIndex < directoryData.Directories.Count; directoryIndex++) {
									// Get the next DirectoryData and see if it's a match, if so the exit the loop
									DirectoryData childDirectoryData = directoryData.Directories[directoryIndex];
									if (0 == string.Compare(childDirectoryData.Info.Name, pathEntries[index], StringComparison.CurrentCultureIgnoreCase)) {
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
		/// Handles the ConvertItem event of a Breadcrumb control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="BreadcrumbConvertItemEventArgs"/> instance containing the event data.</param>
		public static void HandleConvertItem(object sender, BreadcrumbConvertItemEventArgs e) {
			if (BreadcrumbConvertItemTargetType.Path == e.TargetType) {
				// Convert either the item or the trail to a path
				object item = e.Item;
				if (null == item && null != e.Trail && 0 != e.Trail.Count)
					item = e.Trail[e.Trail.Count - 1];

				e.Path = GetPath(item);
			}
			else if (BreadcrumbConvertItemTargetType.Trail == e.TargetType) {
				IList trail = null;
				if (null != e.Path)
					trail = GetTrail(e.RootItem, e.Path);
				else if (null != e.Item)
					trail = GetTrail(e.RootItem, e.Item);

				if (null == trail) {
					ReportError("The specified path could not be found.");
					return;
				}

				e.Trail = trail;
			}
			else {
				throw new NotImplementedException("Unsupported Breadcrumb target type");
			}
		}
	}
}
