using System;
using System.IO;
using System.Windows.Media.Imaging;
using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.BreadcrumbFileSystem {
	/// <summary>
	/// Holds data relating to a directory.
	/// </summary>
	public class DirectoryData {
		private DeferrableObservableCollection<DirectoryData> directories;
		private BitmapSource imageSource;
		private DirectoryInfo info;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>DirectoryData</c> class.
		/// </summary>
		public DirectoryData(DirectoryInfo info) {
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
						DirectoryInfo[] directoryInfos = this.Info.GetDirectories();
						foreach (DirectoryInfo directoryInfo in directoryInfos)
							this.directories.Add(new DirectoryData(directoryInfo));
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
					this.imageSource = ShellIconHelper.GetSystemImageSource(this.Info.FullName);
				return this.imageSource;
			}
		}

		/// <summary>
		/// Gets the info.
		/// </summary>
		/// <value>The info.</value>
		public DirectoryInfo Info {
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
				return this.Info.Name;
			}
		}
	}
}
