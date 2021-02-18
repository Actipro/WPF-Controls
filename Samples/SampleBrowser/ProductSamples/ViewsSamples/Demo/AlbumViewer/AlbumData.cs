using System;
using System.Windows.Media;
using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.AlbumViewer {
	
	/// <summary>
	/// Stores brush data.
	/// </summary>
	public class AlbumData : ObservableObjectBase {

		private string		albumName;
		private string		artistName;
		private ImageSource	imageSource;
		private double		rating;
		private string		releaseDate;
		private int			trackCount;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the album name.
		/// </summary>
		/// <value>The album name.</value>
		public string AlbumName { 
			get {
				return this.albumName;
			}
			set {
				if (this.albumName != value) {
					this.albumName = value;
					this.NotifyPropertyChanged("AlbumName");
				}
			}
		}

		/// <summary>
		/// Gets or sets the artist name.
		/// </summary>
		/// <value>The artist name.</value>
		public string ArtistName {
			get {
				return this.artistName;
			}
			set {
				if (this.artistName != value) {
					this.artistName = value;
					this.NotifyPropertyChanged("ArtistName");
				}
			}
		}

		/// <summary>
		/// Gets or sets the image source.
		/// </summary>
		/// <value>The image source.</value>
		public ImageSource ImageSource {
			get {
				return this.imageSource;
			}
			set {
				this.imageSource = value;
				this.NotifyPropertyChanged("ImageSource");
			}
		}

		/// <summary>
		/// Gets or sets the rating.
		/// </summary>
		/// <value>The rating.</value>
		public double Rating {
			get {
				return this.rating;
			}
			set {
				if (this.rating != value) {
					this.rating = value;
					this.NotifyPropertyChanged("Rating");
				}
			}
		}

		/// <summary>
		/// Gets or sets the release date.
		/// </summary>
		/// <value>The release date.</value>
		public string ReleaseDate {
			get {
				return this.releaseDate;
			}
			set {
				if (this.releaseDate != value) {
					this.releaseDate = value;
					this.NotifyPropertyChanged("ReleaseDate");
				}
			}
		}

		/// <summary>
		/// Gets or sets the track count.
		/// </summary>
		/// <value>The track count.</value>
		public int TrackCount {
			get {
				return this.trackCount;
			}
			set {
				if (this.trackCount != value) {
					this.trackCount = value;
					this.NotifyPropertyChanged("TrackCount");
				}
			}
		}

	}

}
