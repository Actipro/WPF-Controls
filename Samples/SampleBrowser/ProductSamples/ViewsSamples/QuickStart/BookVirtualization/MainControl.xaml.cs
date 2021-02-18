using System.Windows;
using System.Windows.Controls;
using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.BookVirtualization {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private DeferrableObservableCollection<TestObject> pages = new DeferrableObservableCollection<TestObject>();

		private static string[] LoremIpsumStrings = {
			"\t\tLorem ipsum dolor sit amet, consectetur " +
			"adipiscing elit. Maecenas eu justo non felis interdum pretium. Nam nec risus et enim " +
			"pellentesque interdum. Vestibulum quis ullamcorper ligula. Suspendisse at elementum " +
			"metus. Nam quis dictum nisl. Praesent lacinia varius laoreet. Praesent dapibus " +
			"bibendum metus, vel blandit tellus tincidunt et. Vivamus pellentesque ultricies diam, " +
			"id congue lacus pretium sed. Proin vel massa sed eros hendrerit vehicula. Vestibulum " +
			"id lacus at arcu porttitor vulputate. Etiam egestas, felis id tincidunt egestas, quam " +
			"eros tempor neque, venenatis pulvinar nisi nisl at diam. Morbi egestas orci ut neque " +
			"pretium nec pulvinar arcu bibendum. Sed at sodales sem.",

			"\t\tPellentesque imperdiet, purus vulputate " +
			"convallis pellentesque, arcu ante fermentum turpis, quis auctor est dui vel lectus. " +
			"Aliquam sagittis sem ac ante elementum facilisis. Ut diam dolor, fringilla vel gravida " +
			"vel, pharetra eget arcu. Vivamus sed lacinia ligula. Praesent rhoncus adipiscing " +
			"magna, sit amet consequat urna hendrerit eget. Praesent porta, nunc sit amet pretium " +
			"blandit, diam massa vestibulum felis, mattis consectetur sem elit ac ante. Phasellus " +
			"sit amet tortor id turpis commodo feugiat dignissim imperdiet neque. Vestibulum " +
			"dapibus, ipsum eget feugiat mattis, lectus urna lacinia eros, nec aliquet purus felis " +
			"at arcu. Sed adipiscing sem in enim posuere dictum.",

			"\t\tVivamus vel magna massa. Maecenas a mauris " +
			"orci, sed aliquet nibh. Curabitur accumsan, elit eget mattis vestibulum, diam ante " +
			"dapibus odio, quis ultrices libero nulla non felis. Quisque a purus ligula, at " +
			"imperdiet nisl. Curabitur egestas laoreet lorem, non dignissim lacus ultrices eu. Duis " +
			"lobortis elit nec odio fermentum egestas. Morbi ipsum odio, laoreet sit amet " +
			"sollicitudin nec, rutrum vitae libero. Maecenas nec arcu ut risus accumsan iaculis. " +
			"Integer eget nibh in massa dignissim rhoncus. Nullam dictum nibh ut turpis placerat " +
			"lobortis. Integer vel massa eget odio lacinia tempus. Quisque eget metus risus. Etiam " +
			"a odio a risus laoreet pretium. Pellentesque lacinia, metus ac viverra rhoncus, felis " +
			"quam rutrum risus.",

			"\t\tMaecenas egestas sem quis diam euismod " +
			"venenatis. Cras nibh enim, scelerisque in viverra vitae, auctor a nunc. Donec quam " +
			"arcu, cursus quis accumsan eget, scelerisque at mi. Donec fermentum mauris tempus " +
			"tortor mattis pulvinar. Fusce mattis neque a dui fermentum non mattis arcu blandit. " +
			"Integer ornare ante ac dolor euismod hendrerit. In aliquam interdum ligula eget " +
			"condimentum. Praesent fermentum posuere dictum. Nam eget ante eu arcu tristique " +
			"consequat eget eget lacus. Nunc condimentum hendrerit tellus. In pretium bibendum " +
			"tellus, sed accumsan tellus viverra at. Praesent in luctus tortor. Donec ac sapien " +
			"nisl. Suspendisse ut ipsum massa. Morbi quis ligula dui.",

			"\t\tDonec sapien metus, pharetra non " +
			"fermentum sit amet, viverra ut risus. Nullam imperdiet hendrerit tortor, ut imperdiet " +
			"dui tristique ac. In justo mauris, vestibulum et eleifend nec, egestas quis sapien. " +
			"Cras elit dui, convallis eu elementum in, elementum ut eros. Sed at lacus nunc, ut " +
			"facilisis tellus. Integer eget tellus arcu. Cras euismod odio justo. Pellentesque quam " +
			"sapien, cursus et ullamcorper vitae, eleifend ut mauris. Duis ante augue, convallis " +
			"nec luctus at, pellentesque nec risus. Praesent sed tellus nunc. Ut at nisi augue. " +
			"Pellentesque ultricies felis vitae augue tempor id hendrerit nibh volutpat. Aliquam " +
			"consequat sagittis volutpat. Aenean eget eleifend lacus.",

			"\t\tPellentesque dignissim, dolor a rhoncus " +
			"vestibulum, dui neque tincidunt erat, sed suscipit est magna eu elit. Nullam eleifend " +
			"volutpat diam eget suscipit. Mauris eu lectus at sem tincidunt adipiscing sed vel " +
			"risus. In neque lorem, hendrerit sed porttitor ut, bibendum ac tellus. Nullam " +
			"dignissim mattis purus, vel molestie justo mollis eget. Duis vel nisi facilisis arcu " +
			"dignissim blandit a sit amet leo. Fusce et dui nisi. Integer laoreet justo euismod " +
			"metus ornare tempor. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices " +
			"posuere cubilia Curae; Integer et neque felis, quis ultrices velit. Morbi velit diam, " +
			"tempus et pulvinar tincidunt, dictum at eros. Integer hendrerit libero ornare nisl " +
			"hendrerit et molestie erat iaculis.",

			"\t\tMauris sed ligula quis orci vehicula " +
			"aliquam. In hendrerit dui vel magna rutrum ut sollicitudin lectus eleifend. Proin nec " +
			"turpis nisi. Aliquam tempus urna et nisl sagittis sit amet gravida felis fringilla. " +
			"Vivamus mi ligula, lacinia quis commodo eget, blandit in turpis. Sed venenatis dapibus " +
			"turpis, ac egestas augue rhoncus vitae. Praesent mattis feugiat dapibus. Praesent ut " +
			"magna libero, sed sagittis est. Integer vestibulum ante in nisi pharetra luctus. " +
			"Aliquam varius ligula sed magna laoreet eu auctor risus porta. Vivamus egestas, felis " +
			"quis mattis faucibus, mi nunc scelerisque lorem, nec adipiscing nunc dui eget odio. " +
			"Vestibulum commodo mattis erat eu rhoncus. Vestibulum ante ipsum primis in faucibus " +
			"orci luctus.",

			"\t\tNullam at augue porttitor turpis " +
			"aliquam feugiat eu in ante. Vivamus viverra sem ac lectus facilisis semper auctor " +
			"est accumsan. Cras rhoncus sapien vitae dui blandit porttitor. Nunc rhoncus, erat " +
			"eget venenatis porttitor, turpis ipsum pretium odio, ac sollicitudin eros tellus " +
			"pretium erat. Aliquam ac rhoncus nunc. Phasellus aliquam aliquet metus sed aliquam. " +
			"Proin sodales auctor odio. Cras magna magna, viverra in fermentum eu, blandit mollis " +
			"magna. Praesent sagittis convallis nulla, ut ullamcorper nibh vehicula sed. Curabitur " +
			"id nibh magna. Donec sapien dui, vehicula sed sodales a, porttitor vitae sapien. " +
			"Aliquam facilisis, ante et blandit mattis, diam justo sollicitudin elit, at suscipit " +
			"purus leo bibendum lectus.",

			"\t\tQuisque ut eros vehicula dui rhoncus " +
			"accumsan id sed odio. Nunc vitae vehicula enim. Etiam volutpat quam eu odio ultricies " +
			"ut consectetur orci mollis. Praesent vitae nunc sed leo blandit ultrices a et orci. " +
			"Nam eu odio nunc, ut tristique risus. Suspendisse a mattis tellus. Fusce fermentum " +
			"odio eget mauris vulputate laoreet. Vivamus vulputate arcu in neque placerat ac " +
			"tincidunt ipsum viverra. Vivamus vestibulum pretium augue, eget feugiat nunc dignissim " +
			"congue. Phasellus mauris nunc, venenatis ac auctor semper, mollis quis tellus. " +
			"Pellentesque ac arcu libero. Etiam placerat dapibus risus, in consequat dui bibendum " +
			"sit amet. Curabitur cursus ligula ac neque feugiat vestibulum.",

			"\t\tInteger nec justo a tellus luctus " +
			"ultrices. Donec id augue vitae mi porttitor scelerisque. Donec suscipit nibh vel magna " +
			"faucibus sodales. Donec ornare accumsan tortor non vehicula. Donec suscipit pharetra " +
			"tortor ornare egestas. Phasellus at magna sem, at ultricies odio. Donec tortor velit, " +
			"lacinia tempor luctus at, aliquet quis arcu. Suspendisse interdum tempus vulputate. " +
			"In facilisis arcu at nisi ultricies placerat. In dapibus faucibus ante, non porta sem " +
			"pretium vel. Maecenas sed tortor quis libero tristique mollis sit amet quis est. " +
			"Pellentesque eget arcu tellus, ac viverra enim. Ut rhoncus enim eu purus pharetra " +
			"pellentesque. Donec sem justo, dignissim ut auctor eget, porta ut ligula."
		};

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();
			this.UpdateTextBlock();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Generates a <see cref="TestObject"/> for use in the Book control.
		/// </summary>
		/// <param name="index">The index of the item.</param>
		/// <returns>An item for use in the Book control.</returns>
		private TestObject GenerateItem(int index) {
			return new TestObject() {
				PageNumber = index + 1,
				Text = (LoremIpsumStrings.Length != 0) ? LoremIpsumStrings[index % LoremIpsumStrings.Length] : string.Empty
			};
		}

		/// <summary>
		/// Handles the <c>SelectedViewChanged</c> event of the <c>book</c> control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="PropertyChangedRoutedEventArgs&lt;Int32&gt;"/> instance containing the event data.</param>
		private void OnBookSelectedViewChanged(object sender, PropertyChangedRoutedEventArgs<int> e) {
			this.UpdateTextBlock();
		}

		/// <summary>
		/// Called when the first page button is clicked.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnFirstPageButtonClick(object sender, RoutedEventArgs e) {
			this.book.GoToFirstPage();
		}

		/// <summary>
		/// Called when the Generate button is clicked.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnGenerateClick(object sender, RoutedEventArgs e) {
			this.pages.BeginUpdate();
			try {
				this.pages.Clear();
				// Generate one million pages
				for (int i = 0; i < 1000000; i++)
					this.pages.Add(GenerateItem(i));
			}
			finally {
				this.pages.EndUpdate();
				this.border.Visibility = Visibility.Collapsed;
				this.UpdateTextBlock();
			}
		}

		/// <summary>
		/// Called when the Last button is clicked.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnLastPageButtonClick(object sender, RoutedEventArgs e) {
			this.book.GoToLastPage();
		}

		/// <summary>
		/// Called when the next page button is clicked.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnNextPageButtonClick(object sender, RoutedEventArgs e) {
			this.book.GoToNextPage();
		}

		/// <summary>
		/// Called when the prevous button is clicked.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnPreviousPageButtonClick(object sender, RoutedEventArgs e) {
			this.book.GoToPreviousPage();
		}

		/// <summary>
		/// Called when the skip backwards button is clicked.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnSkipBackwardsButtonClick(object sender, RoutedEventArgs e) {
			this.book.GoToPreviousPage(50);
		}

		/// <summary>
		/// Called when the skip forwards button is clicked.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnSkipForwardsButtonClick(object sender, RoutedEventArgs e) {
			this.book.GoToNextPage(50);
		}

		/// <summary>
		/// Updates the text block.
		/// </summary>
		private void UpdateTextBlock() {
			if (this.book.ViewCount == 0)
				this.textBlock.Text = "Generate pages above";
			else
				this.textBlock.Text = string.Format("{0} of {1}", this.book.SelectedViewIndex + 1, this.book.ViewCount);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the pages collection.
		/// </summary>
		/// <value>The pages collection.</value>
		public DeferrableObservableCollection<TestObject> Pages {
			get {
				return this.pages;
			}
		}
	}
}
