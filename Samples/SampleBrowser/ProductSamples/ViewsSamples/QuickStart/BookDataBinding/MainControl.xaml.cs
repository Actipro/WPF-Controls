using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.BookDataBinding {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private DeferrableObservableCollection<TestObject> pages =
			new DeferrableObservableCollection<TestObject>();

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			// Create brochure pages
			pages.Add(new TestObject() {
				OverlayTopLeftColor = Color.FromArgb(102, 85, 50, 50),
				OverlayBottomRightColor = Color.FromArgb(102, 255, 50, 50),
				Header = "Travel in Italy",
				ImageSource = new BitmapImage(new Uri("/SampleBrowser;component/productsamples/viewssamples/quickstart/bookdatabinding/images/001.jpg", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.None },
				Text = "Need to get away? Want to see the world?\n\n    Italy is the perfect place for your next family vacation. Experience beautiful sights, taste delightful Italian cuisine, and see what Europe has to offer you!"
			});
			pages.Add(new TestObject() {
				OverlayTopLeftColor = Color.FromArgb(102, 142, 64, 64),
				OverlayBottomRightColor = Color.FromArgb(102, 113, 255, 255),
				Header = "Sightseeing",
				ImageSource = new BitmapImage(new Uri("/SampleBrowser;component/productsamples/viewssamples/quickstart/bookdatabinding/images/002.jpg", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.None },
				Text = "A few must-see places include:\n\n - Venice\n - Bologna\n - Florence\n - Rome\n - Naples\n - Palermo\n - Pisa\n - Siena"
			});
			pages.Add(new TestObject() {
				OverlayTopLeftColor = Color.FromArgb(102, 108, 132, 60),
				OverlayBottomRightColor = Color.FromArgb(102, 208, 255, 113),
				Header = "Cuisine",
				ImageSource = new BitmapImage(new Uri("/SampleBrowser;component/productsamples/viewssamples/quickstart/bookdatabinding/images/003.jpg", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.None },
				Text = "Italy is famous for its pasta, pizza, and gelato.\n\n    While you have probably had these foods before, there is nothing like fresh Italian pasta, pizza, or gelato. Get your taste buds ready because they are in for a treat."
			});
			pages.Add(new TestObject() {
				OverlayTopLeftColor = Color.FromArgb(102, 142, 64, 64),
				OverlayBottomRightColor = Color.FromArgb(102, 113, 255, 255),
				Header = "Getting Around",
				ImageSource = new BitmapImage(new Uri("/SampleBrowser;component/productsamples/viewssamples/quickstart/bookdatabinding/images/004.jpg", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.None },
				Text = "    While it is possible to get around Italy without a car, it can sometimes be difficult. Especially if you are traveling with your family, a car can sometimes be mandatory. If you are feeling adventurous, however, the Eurail system and the Italian buses are capable."
			});
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
				return pages;
			}
		}
	}
}
