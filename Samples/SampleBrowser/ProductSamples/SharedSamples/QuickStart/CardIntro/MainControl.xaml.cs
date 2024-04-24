using ActiproSoftware.Windows.Controls;
using System;
using System.Windows;


namespace ActiproSoftware.ProductSamples.SharedSamples.QuickStart.CardIntro {

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
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private void OnActionableCardClick(object sender, RoutedEventArgs e) {
			ThemedMessageBox.Show("Respond to click events or assign a Command.");
		}

	}

}