using System;
using System.ComponentModel;

#if WINRT
using Windows.UI.Xaml.Media.Imaging;
#else
using System.Windows.Media.Imaging;
#endif

namespace ActiproSoftware.ProductSamples.GridsSamples.Common {
	
	/// <summary>
	/// Provides a tree node model implementation for folders that can toggle the image based on expansion state.
	/// </summary>
	public class FolderTreeNodeModel : ThreeStateCheckableTreeNodeModel {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <see cref="FolderTreeNodeModel"/> class.
		/// </summary>
		public FolderTreeNodeModel() {
			this.UpdateImageSource();
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Updates the folder image source based on the expansion state.
		/// </summary>
		private void UpdateImageSource() {
			var imageUri = new Uri(this.IsExpanded ? "/Images/Icons/FolderOpen16.png" : "/Images/Icons/FolderClosed16.png", UriKind.Relative);
			this.ImageSource = new BitmapImage(imageUri);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Raises the <see cref="PropertyChanged"/> event.
		/// </summary>
		/// <param name="e">The <see cref="PropertyChangedEventArgs"/> that contains the event data.</param>
		protected override void OnPropertyChanged(PropertyChangedEventArgs e) {
			// Call the base method
			base.OnPropertyChanged(e);

			if (e != null) {
				switch (e.PropertyName) {
					case "IsExpanded":
						this.UpdateImageSource();
						break;
				}
			}
		}

		/// <summary>
		/// Gets or sets the folder path.
		/// </summary>
		public string Path { get; set; }

	}

}
