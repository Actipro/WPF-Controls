namespace ActiproSoftware.ProductSamples.GridsSamples.Demo.MusicCatalog {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			this.GenerateItems();
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Generates the sample items.
		/// </summary>
		private void GenerateItems() {
			var rootModel = new ArtistTreeNodeModel();

			ArtistTreeNodeModel artistModel;
			AlbumTreeNodeModel albumModel;

			artistModel = new ArtistTreeNodeModel() { Name = "Massive Attack", IsExpanded = true };
			rootModel.Children.Add(artistModel);

			albumModel = new AlbumTreeNodeModel() { Name = "Mezzanine", Length = "1:03:36", IsExpanded = false };
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Angel", Length = "6:19", Popularity = 7 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Risingson", Length = "4:58", Popularity = 2 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Teardrop", Length = "5:30", Popularity = 10 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Inertia Creeps", Length = "5:57", Popularity = 3 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Exchange", Length = "4:11", Popularity = 1 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Dissolved Girl", Length = "6:06", Popularity = 6 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Man Next Door", Length = "5:56", Popularity = 1 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Black Milk", Length = "6:21", Popularity = 2 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Mezzanine", Length = "5:56", Popularity = 2 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Group Four", Length = "8:12", Popularity = 2 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Exchange", Length = "4:10", Popularity = 3 });
			artistModel.Children.Add(albumModel);
			
			albumModel = new AlbumTreeNodeModel() { Name = "Protection", Length = "48:59", IsExpanded = false };
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Protection", Length = "7:51", Popularity = 10 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Karmacoma", Length = "5:16", Popularity = 3 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Three", Length = "3:49", Popularity = 2 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Weather Storm", Length = "5:00", Popularity = 2 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Spying Glass", Length = "5:21", Popularity = 4 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Better Things", Length = "4:13", Popularity = 1 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Euro Child", Length = "5:11", Popularity = 3 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Sly", Length = "5:24", Popularity = 4 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Heat Miser", Length = "3:39", Popularity = 5 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Light My Fire (Live)", Length = "3:15", Popularity = 1 });
			artistModel.Children.Add(albumModel);

			artistModel = new ArtistTreeNodeModel() { Name = "Portishead", IsExpanded = true };
			rootModel.Children.Add(artistModel);

			albumModel = new AlbumTreeNodeModel() { Name = "Dummy", Length = "49:13", IsExpanded = false };
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Mysterons", Length = "5:06", Popularity = 2 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Sour Times", Length = "4:14", Popularity = 7 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Strangers", Length = "3:58", Popularity = 2 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "It Could Be Sweet", Length = "4:19", Popularity = 3 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Wandering Star", Length = "4:54", Popularity = 3 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "It's A Fire", Length = "3:49", Popularity = 1 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Numb", Length = "3:58", Popularity = 2 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Roads", Length = "5:05", Popularity = 8 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Pedestal", Length = "3:41", Popularity = 2 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Biscuit", Length = "5:04", Popularity = 2 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Glory Box", Length = "5:05", Popularity = 10 });
			artistModel.Children.Add(albumModel);
			
			albumModel = new AlbumTreeNodeModel() { Name = "Portishead", Length = "50:03", IsExpanded = true };
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Cowboys", Length = "4:39", Popularity = 4 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "All Mine", Length = "4:00", Popularity = 9 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Undenied", Length = "4:20", Popularity = 3 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Half Day Closing", Length = "3:47", Popularity = 4 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Over", Length = "3:55", Popularity = 5 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Humming", Length = "6:02", Popularity = 10 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Mourning Air", Length = "4:12", Popularity = 8 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Seven Months", Length = "4:16", Popularity = 3 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Only You", Length = "4:59", Popularity = 10 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Elysium", Length = "5:54", Popularity = 3 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Western Eyes", Length = "3:59", Popularity = 6 });
			artistModel.Children.Add(albumModel);

			artistModel = new ArtistTreeNodeModel() { Name = "Zero 7", IsExpanded = true };
			rootModel.Children.Add(artistModel);

			albumModel = new AlbumTreeNodeModel() { Name = "Simple Things", Length = "1:12:26", IsExpanded = false };
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "I Have Seen", Length = "5:09", Popularity = 1 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Polaris", Length = "4:48", Popularity = 2 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Destiny", Length = "5:37", Popularity = 9 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Give It Away", Length = "5:17", Popularity = 3 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Simple Things", Length = "4:24", Popularity = 1 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Red Dust", Length = "5:40", Popularity = 4 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Distractions", Length = "5:16", Popularity = 4 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "In the Waiting Line", Length = "4:32", Popularity = 10 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Out of Town", Length = "4:47", Popularity = 2 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "This World", Length = "5:35", Popularity = 2 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Likufanele", Length = "6:11", Popularity = 1 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "End Theme", Length = "3:39", Popularity = 1 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Salt Water Sound", Length = "5:30", Popularity = 4 });
			albumModel.Children.Add(new TrackTreeNodeModel() { Name = "Spinning", Length = "6:03", Popularity = 2 });
			artistModel.Children.Add(albumModel);

			this.DataContext = rootModel;
		}

	}

}
