using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ActiproSoftware.Text;
using ActiproSoftware.Windows.Controls.SyntaxEditor;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.TextChangesCancelling {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl : System.Windows.Controls.UserControl {

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
		/// Occurs before the editor's document text has changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="EditorSnapshotChangingEventArgs"/> that contains the event data.</param>
		private void OnEditorDocumentTextChanging(object sender, EditorSnapshotChangingEventArgs e) {
			e.Cancel = (cancelCheckBox.IsChecked == true);

			if (e.Cancel) {
				if (alternateTextCheckBox.IsChecked == true) {
					// Temporarily turn off cancel and insert date/time instead
					cancelCheckBox.IsChecked = false;
					editor.ActiveView.ReplaceSelectedText(TextChangeTypes.Custom, DateTime.Now.ToString());
					cancelCheckBox.IsChecked = true;
					MessageBox.Show("Text change cancelled, current date/time inserted instead.", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
				}
				else {
					// Simple cancel
					MessageBox.Show("Text change cancelled.", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
				}
			}
		}

		
    }

}