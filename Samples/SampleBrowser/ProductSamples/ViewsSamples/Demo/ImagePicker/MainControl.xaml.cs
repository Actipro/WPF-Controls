using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ActiproSoftware.Windows.Controls.Views;
using ActiproSoftware.Windows.Media;
using ActiproSoftware.SampleBrowser;

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.ImagePicker {

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

			var data = this.FindResource("ProductData") as ProductData;
			if (data != null) {
				// Add product logos as sample images
				productNameListBox.ItemsSource = data.ProductFamilies;
				productLogoListBox.ItemsSource = productNameListBox.ItemsSource;
			}
		}

	}
}