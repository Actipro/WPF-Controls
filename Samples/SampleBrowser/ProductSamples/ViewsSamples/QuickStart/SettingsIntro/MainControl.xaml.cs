

using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Views;
using ActiproSoftware.Windows.Input;
using System.Windows;

namespace ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.SettingsIntro {

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

		private void OnSettingClick(object sender, RoutedEventArgs e) {
			// Make sure the source of the Click is a SettingsCard since some Click events can bubble up from content hosted on the card (like a CheckBox)
			if ((e.Source is SettingsCard) && (!e.Handled))
				ThemedMessageBox.Show("Handle the 'Click' event for a setting as an alternative to using a 'Command'.", "Setting Click", MessageBoxButton.OK, MessageBoxImage.Information);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public DelegateCommand<object> SettingClickedCommand { get; }
			= new DelegateCommand<object>(p => ThemedMessageBox.Show($"The setting for '{p}' was clicked.", "Setting Click", MessageBoxButton.OK, MessageBoxImage.Information));

	}
}