using System;
using System.IO;
using System.Windows.Media.Imaging;
using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.BreadcrumbFileSystem {
	/// <summary>
	/// Holds the root My Computer data.
	/// </summary>
	public class MyComputerData {
		private DeferrableObservableCollection<DriveData> drives;
		private BitmapSource imageSource;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MyComputerData</c> class.
		/// </summary>
		public MyComputerData() {
			// No-op
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the drives.
		/// </summary>
		/// <value>The drives.</value>
		public DeferrableObservableCollection<DriveData> Drives {
			get {
				if (null == this.drives) {
					this.drives = new DeferrableObservableCollection<DriveData>();

					try {
						DriveInfo[] driveInfos = DriveInfo.GetDrives();
						foreach (DriveInfo driveInfo in driveInfos)
							this.drives.Add(new DriveData(driveInfo));
					}
					catch (IOException) {
						// No-op
					}
					catch (UnauthorizedAccessException) {
						// No-op
					}
				}

				return this.drives;
			}
		}

		/// <summary>
		/// Gets the image source.
		/// </summary>
		/// <value>The image source.</value>
		public BitmapSource ImageSource {
			get {
				if (null == this.imageSource)
					this.imageSource = new BitmapImage(new Uri("/Images/Icons/Computer16.png", UriKind.Relative));
				return this.imageSource;
			}
		}

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name {
			get {
				return "My Computer";
			}
		}
	}
}
