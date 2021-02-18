using System;
using System.Windows;
using System.Windows.Controls;
using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.WallCalendar {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Goes to the April view index.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void GotoApril(object sender, RoutedEventArgs e) {
			if (book == null)
				return;

			book.GoToPage(8);
		}

		/// <summary>
		/// Goes to the August view index.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void GotoAugust(object sender, RoutedEventArgs e) {
			if (book == null)
				return;

			book.GoToPage(16);
		}

		/// <summary>
		/// Goes to the February view index.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void GotoFebruary(object sender, RoutedEventArgs e) {
			if (book == null)
				return;

			book.GoToPage(4);
		}

		/// <summary>
		/// Goes to the December view index.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void GotoDecember(object sender, RoutedEventArgs e) {
			if (book == null)
				return;

			book.GoToPage(24);
		}

		/// <summary>
		/// Goes to the January view index.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void GotoJanuary(object sender, RoutedEventArgs e) {
			if (book == null)
				return;

			book.GoToPage(2);
		}

		/// <summary>
		/// Goes to the July view index.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void GotoJuly(object sender, RoutedEventArgs e) {
			if (book == null)
				return;

			book.GoToPage(14);
		}

		/// <summary>
		/// Goes to the June view index.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void GotoJune(object sender, RoutedEventArgs e) {
			if (book == null)
				return;

			book.GoToPage(12);
		}

		/// <summary>
		/// Goes to the March view index.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void GotoMarch(object sender, RoutedEventArgs e) {
			if (book == null)
				return;

			book.GoToPage(6);
		}

		/// <summary>
		/// Goes to the May view index.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void GotoMay(object sender, RoutedEventArgs e) {
			if (book == null)
				return;

			book.GoToPage(10);
		}

		/// <summary>
		/// Goes to the November view index.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void GotoNovember(object sender, RoutedEventArgs e) {
			if (book == null)
				return;

			book.GoToPage(22);
		}

		/// <summary>
		/// Goes to the October view index.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void GotoOctober(object sender, RoutedEventArgs e) {
			if (book == null)
				return;

			book.GoToPage(20);
		}

		/// <summary>
		/// Goes to the September view index.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void GotoSeptember(object sender, RoutedEventArgs e) {
			if (book == null)
				return;

			book.GoToPage(18);
		}

		/// <summary>
		/// Called when the <see cref="Book"/>'s selected view index changed.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="PropertyChangedRoutedEventArgs&lt;System.Int32&gt;"/> instance containing the event data.</param>
		private void OnSelectedViewIndexChanged(object sender, PropertyChangedRoutedEventArgs<int> e) {
			if(e == null)
				throw new ArgumentNullException("e");

			if (e.NewValue > 1)
				upperBorder.Visibility = Visibility.Visible;
			else
				upperBorder.Visibility = Visibility.Collapsed;
		}
	}
}
