using System;
using System.Globalization;
using System.IO;
using System.Windows.Media.Imaging;
using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.BreadcrumbFileSystem {
	/// <summary>
	/// Holds data relating to a drive.
	/// </summary>
	public class DriveData {
		private DeferrableObservableCollection<DirectoryData> directories;
		private BitmapSource imageSource;
		private DriveInfo info;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>DriveData</c> class.
		/// </summary>
		public DriveData(DriveInfo info) {
			if (null == info)
				throw new ArgumentNullException("info");
			this.info = info;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the drives.
		/// </summary>
		/// <value>The drives.</value>
		public DeferrableObservableCollection<DirectoryData> Directories {
			get {
				if (null == this.directories) {
					this.directories = new DeferrableObservableCollection<DirectoryData>();

					try {
						if (null != this.Info.RootDirectory) {
							DirectoryInfo[] directoryInfos = this.Info.RootDirectory.GetDirectories();
							foreach (DirectoryInfo directoryInfo in directoryInfos)
								this.directories.Add(new DirectoryData(directoryInfo));
						}
					}
					catch (DirectoryNotFoundException) {
						// No-op
					}
					catch (IOException) {
						// No-op
					}
					catch (UnauthorizedAccessException) {
						// No-op
					}
				}

				return this.directories;
			}
		}

		/// <summary>
		/// Gets the image source.
		/// </summary>
		/// <value>The image source.</value>
		public BitmapSource ImageSource {
			get {
				if (null == this.imageSource)
					this.imageSource = ShellIconHelper.GetSystemImageSource(this.Info.Name);
				return this.imageSource;
			}
		}

		/// <summary>
		/// Gets the info.
		/// </summary>
		/// <value>The info.</value>
		public DriveInfo Info {
			get {
				return this.info;
			}
		}

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name {
			get {
				string name = this.Info.Name.TrimEnd('\\');
				switch (this.Info.DriveType) {
					case DriveType.Fixed:
						return string.Format(CultureInfo.CurrentCulture, "Local Disk ({0})", name);
					case DriveType.CDRom:
						return string.Format(CultureInfo.CurrentCulture, "CD Drive ({0})", name);
					case DriveType.Network:
						return string.Format(CultureInfo.CurrentCulture, "Network Drive ({0})", name);
					case DriveType.Ram:
						return string.Format(CultureInfo.CurrentCulture, "RAM Disk ({0})", name);
					case DriveType.Removable:
						return string.Format(CultureInfo.CurrentCulture, "Removable Disk ({0})", name);
					default:
						return string.Format(CultureInfo.CurrentCulture, "Unknown Disk ({0})", name);
				}
			}
		}
	}
}
