using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.ShellSamples.QuickStart.CustomShellObjects {

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
		/// Occurs when a key is pressed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="KeyEventArgs"/> that contains the event data.</param>
		private void OnPathTextBoxKeyDown(object sender, KeyEventArgs e) {
			if (e.Key == Key.Enter) {
				var exp = BindingOperations.GetBindingExpression(pathTextBox, TextBox.TextProperty);
				if (exp != null)
					exp.UpdateSource();
			}
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
