using ActiproSoftware.Windows;
using System.Windows.Media.Imaging;

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.AlbumViewer;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	private static RoutedCommand? _selectNext;
	private static RoutedCommand? _selectPrevious;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		CommandBindings.Add(new CommandBinding(SelectNext, OnSelectNextExecuted));
		CommandBindings.Add(new CommandBinding(SelectPrevious, OnSelectPreviousExecuted));

		Albums = [
			new AlbumData() {
				AlbumName = "Wireless World",
				ArtistName = "Armando Cossutta",
				ImageSource = new BitmapImage(new Uri("/ProductSamples/ViewsSamples/Demo/AlbumViewer/Images/album1.png", UriKind.RelativeOrAbsolute)),
				Rating = 3.0,
				ReleaseDate = "3/4/2019",
				TrackCount = 12
			},
			new AlbumData() {
				AlbumName = "Volcanoes in Papua",
				ArtistName = "Martin Marks",
				ImageSource = new BitmapImage(new Uri("/ProductSamples/ViewsSamples/Demo/AlbumViewer/Images/album2.png", UriKind.RelativeOrAbsolute)),
				Rating = 4.0,
				ReleaseDate = "5/6/2015",
				TrackCount = 9
			},
			new AlbumData() {
				AlbumName = "Archibald Lush",
				ArtistName = "Jack Van Berg",
				ImageSource = new BitmapImage(new Uri("/ProductSamples/ViewsSamples/Demo/AlbumViewer/Images/album3.png", UriKind.RelativeOrAbsolute)),
				Rating = 4.0,
				ReleaseDate = "8/20/2019",
				TrackCount = 11
			},
			new AlbumData() {
				AlbumName = "Spaghetti Blue",
				ArtistName = "Kalmyk Dialect",
				ImageSource = new BitmapImage(new Uri("/ProductSamples/ViewsSamples/Demo/AlbumViewer/Images/album4.png", UriKind.RelativeOrAbsolute)),
				Rating = 5.0,
				ReleaseDate = "1/1/2020",
				TrackCount = 15
			},
			new AlbumData() {
				AlbumName = "stop",
				ArtistName = "airbrakes",
				ImageSource = new BitmapImage(new Uri("/ProductSamples/ViewsSamples/Demo/AlbumViewer/Images/album5.png", UriKind.RelativeOrAbsolute)),
				Rating = 2.0,
				ReleaseDate = "12/3/2018",
				TrackCount = 8
			},
			new AlbumData() {
				AlbumName = "Language",
				ArtistName = "Transaction Control",
				ImageSource = new BitmapImage(new Uri("/ProductSamples/ViewsSamples/Demo/AlbumViewer/Images/album6.png", UriKind.RelativeOrAbsolute)),
				Rating = 4.0,
				ReleaseDate = "4/21/2017",
				TrackCount = 14
			},
			new AlbumData() {
				AlbumName = "Gong",
				ArtistName = "Ping Pong",
				ImageSource = new BitmapImage(new Uri("/ProductSamples/ViewsSamples/Demo/AlbumViewer/Images/album7.png", UriKind.RelativeOrAbsolute)),
				Rating = 4.0,
				ReleaseDate = "6/17/1987",
				TrackCount = 7
			},
			new AlbumData() {
				AlbumName = "Sanity",
				ArtistName = "Epic Souls",
				ImageSource = new BitmapImage(new Uri("/ProductSamples/ViewsSamples/Demo/AlbumViewer/Images/album8.png", UriKind.RelativeOrAbsolute)),
				Rating = 1.0,
				ReleaseDate = "3/19/2007",
				TrackCount = 11
			},
			new AlbumData() {
				AlbumName = "Lost with Lambert",
				ArtistName = "Snodgrass",
				ImageSource = new BitmapImage(new Uri("/ProductSamples/ViewsSamples/Demo/AlbumViewer/Images/album9.png", UriKind.RelativeOrAbsolute)),
				Rating = 5.0,
				ReleaseDate = "8/2/2015",
				TrackCount = 13
			},
		];

		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnSelectNextExecuted(object sender, ExecutedRoutedEventArgs e) {
		var next = listBox.SelectedIndex + 1;
		if (next >= listBox.Items.Count)
			next = 0;
		listBox.SelectedIndex = next;
	}

	private void OnSelectPreviousExecuted(object sender, ExecutedRoutedEventArgs e) {
		var previous = listBox.SelectedIndex - 1;
		if (previous < 0)
			previous = listBox.Items.Count - 1;
		listBox.SelectedIndex = previous;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCECURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The albums.
	/// </summary>
	public DeferrableObservableCollection<AlbumData> Albums { get; }

	/// <summary>
	/// The <see cref="RoutedCommand"/> that is used to select the next item.
	/// </summary>
	public static RoutedCommand SelectNext
		=> _selectNext ??= new RoutedCommand(nameof(SelectNext), typeof(MainControl));

	/// <summary>
	/// The <see cref="RoutedCommand"/> that is used to select the previous item.
	/// </summary>
	public static RoutedCommand SelectPrevious
		=> _selectPrevious ??= new RoutedCommand(nameof(SelectPrevious), typeof(MainControl));

}
