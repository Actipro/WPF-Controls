using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Shell;
using System.Linq;
using System.Windows;

namespace ActiproSoftware.ProductSamples.ShellSamples.QuickStart.ShellListViewColumns {

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
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnListCheckedItemsButtonClick(object sender, RoutedEventArgs e) {
			var checkedItems = listView.Items.OfType<ShellObjectViewModel>().Where(vm => true.Equals(vm.Tag));
			var checkedItemNames = string.Join("\r\n", checkedItems.Select(vm => vm.Name).ToArray());
			if (string.IsNullOrEmpty(checkedItemNames))
				checkedItemNames = "(none)";

			ThemedMessageBox.Show(checkedItemNames, "Checked Items");
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
			listView.DisposeShellInstances();
		}

	}

}
