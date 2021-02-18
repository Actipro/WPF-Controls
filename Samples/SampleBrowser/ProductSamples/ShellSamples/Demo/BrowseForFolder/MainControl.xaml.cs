using ActiproSoftware.Windows.Controls.Shell;
using System.Windows;

namespace ActiproSoftware.ProductSamples.ShellSamples.Demo.BrowseForFolder {

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

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnCancelButtonClick(object sender, RoutedEventArgs e) {
			MessageBox.Show("The dialog was canceled.");
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnOkButtonClick(object sender, RoutedEventArgs e) {
			var selectedViewModel = treeListBox.SelectedItem as ShellObjectViewModel;
			if (selectedViewModel != null)
				MessageBox.Show(string.Format("The '{0}' folder with parsing name '{1}' was selected.", selectedViewModel.Name, selectedViewModel.ParsingName));
			else
				MessageBox.Show("Nothing was selected.");
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Notifies the UI that it has been unloaded.
		/// </summary>
		public override void NotifyUnloaded() {
			base.NotifyUnloaded();
			
			// Dispose any unmanaged resources held by the shell instances now that the UI is closing
			treeListBox.DisposeShellInstances();
		}

	}

}
