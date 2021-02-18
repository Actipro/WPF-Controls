using System;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.AlbumViewer {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		public DeferrableObservableCollection<AlbumData> albums;

		private static RoutedCommand selectNext;
		private static RoutedCommand selectPrevious;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			this.CommandBindings.Add(new CommandBinding(MainControl.SelectNext, OnSelectNextExecuted));
			this.CommandBindings.Add(new CommandBinding(MainControl.SelectPrevious, OnSelectPreviousExecuted));

			this.albums = new DeferrableObservableCollection<AlbumData>();
			this.albums.Add(new AlbumData() {
				AlbumName = "Wireless World",
				ArtistName = "Armando Cossutta",
				ImageSource = new BitmapImage(new Uri("/ProductSamples/ViewsSamples/Demo/AlbumViewer/Images/album1.png", UriKind.RelativeOrAbsolute)),
				Rating = 3.0,
				ReleaseDate = "3/4/2019",
				TrackCount = 12
			});
			this.albums.Add(new AlbumData() {
				AlbumName = "Volcanoes in Papua",
				ArtistName = "Martin Marks",
				ImageSource = new BitmapImage(new Uri("/ProductSamples/ViewsSamples/Demo/AlbumViewer/Images/album2.png", UriKind.RelativeOrAbsolute)),
				Rating = 4.0,
				ReleaseDate = "5/6/2015",
				TrackCount = 9
			});
			this.albums.Add(new AlbumData() {
				AlbumName = "Archibald Lush",
				ArtistName = "Jack Van Berg",
				ImageSource = new BitmapImage(new Uri("/ProductSamples/ViewsSamples/Demo/AlbumViewer/Images/album3.png", UriKind.RelativeOrAbsolute)),
				Rating = 4.0,
				ReleaseDate = "8/20/2019",
				TrackCount = 11
			});
			this.albums.Add(new AlbumData() {
				AlbumName = "Spaghetti Blue",
				ArtistName = "Kalmyk Dialect",
				ImageSource = new BitmapImage(new Uri("/ProductSamples/ViewsSamples/Demo/AlbumViewer/Images/album4.png", UriKind.RelativeOrAbsolute)),
				Rating = 5.0,
				ReleaseDate = "1/1/2020",
				TrackCount = 15
			});
			this.albums.Add(new AlbumData() {
				AlbumName = "stop",
				ArtistName = "airbrakes",
				ImageSource = new BitmapImage(new Uri("/ProductSamples/ViewsSamples/Demo/AlbumViewer/Images/album5.png", UriKind.RelativeOrAbsolute)),
				Rating = 2.0,
				ReleaseDate = "12/3/2018",
				TrackCount = 8
			});
			this.albums.Add(new AlbumData() {
				AlbumName = "Language",
				ArtistName = "Transaction Control",
				ImageSource = new BitmapImage(new Uri("/ProductSamples/ViewsSamples/Demo/AlbumViewer/Images/album6.png", UriKind.RelativeOrAbsolute)),
				Rating = 4.0,
				ReleaseDate = "4/21/2017",
				TrackCount = 14
			});
			this.albums.Add(new AlbumData() {
				AlbumName = "Gong",
				ArtistName = "Ping Pong",
				ImageSource = new BitmapImage(new Uri("/ProductSamples/ViewsSamples/Demo/AlbumViewer/Images/album7.png", UriKind.RelativeOrAbsolute)),
				Rating = 4.0,
				ReleaseDate = "6/17/1987",
				TrackCount = 7
			});
			this.albums.Add(new AlbumData() {
				AlbumName = "Sanity",
				ArtistName = "Epic Souls",
				ImageSource = new BitmapImage(new Uri("/ProductSamples/ViewsSamples/Demo/AlbumViewer/Images/album8.png", UriKind.RelativeOrAbsolute)),
				Rating = 1.0,
				ReleaseDate = "3/19/2007",
				TrackCount = 11
			});
			this.albums.Add(new AlbumData() {
				AlbumName = "Lost with Lambert",
				ArtistName = "Snodgrass",
				ImageSource = new BitmapImage(new Uri("/ProductSamples/ViewsSamples/Demo/AlbumViewer/Images/album9.png", UriKind.RelativeOrAbsolute)),
				Rating = 5.0,
				ReleaseDate = "8/2/2015",
				TrackCount = 13
			});

			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private void OnSelectNextExecuted(object sender, ExecutedRoutedEventArgs e) {
			int next = this.listBox.SelectedIndex + 1;
			if (next >= this.listBox.Items.Count)
				next = 0;
			this.listBox.SelectedIndex = next;
		}

		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private void OnSelectPreviousExecuted(object sender, ExecutedRoutedEventArgs e) {
			int previous = this.listBox.SelectedIndex - 1;
			if (previous < 0)
				previous = this.listBox.Items.Count - 1;
			this.listBox.SelectedIndex = previous;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCECURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the albums.
		/// </summary>
		public DeferrableObservableCollection<AlbumData> Albums {
			get {
				return this.albums;
			}
		}

		/// <summary>
		/// Gets the <see cref="RoutedCommand"/> that is used to select the next item.
		/// </summary>
		/// <value>The <see cref="RoutedCommand"/> that is used to select the next item.</value>
		public static RoutedCommand SelectNext {
			get {
				if (selectNext == null)
					selectNext = new RoutedCommand("SelectNext", typeof(MainControl));
				return selectNext;
			}
		}

		/// <summary>
		/// Gets the <see cref="RoutedCommand"/> that is used to select the previous item.
		/// </summary>
		/// <value>The <see cref="RoutedCommand"/> that is used to select the previous item.</value>
		public static RoutedCommand SelectPrevious {
			get {
				if (selectPrevious == null)
					selectPrevious = new RoutedCommand("SelectPrevious", typeof(MainControl));
				return selectPrevious;
			}
		}

	}
}