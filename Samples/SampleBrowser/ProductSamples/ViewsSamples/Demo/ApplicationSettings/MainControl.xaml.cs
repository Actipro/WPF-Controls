using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Input;

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.ApplicationSettings {

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
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public DelegateCommand<object> SettingClickedCommand { get; }
			= new DelegateCommand<object>(p => ThemedMessageBox.Show($"The setting for '{p}' was clicked.", "Setting Click", MessageBoxButton.OK, MessageBoxImage.Information));

	}
}