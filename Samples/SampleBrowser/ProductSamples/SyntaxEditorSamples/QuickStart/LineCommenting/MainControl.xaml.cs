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
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.LineCommenting {

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

			// Select the first line commenter
			lineCommentersComboBox.SelectedIndex = 0;
        }
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the selection changes in a <see cref="Selector"/>.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="SelectionChangedEventArgs"/> that contains the event data.</param>
		private void OnLineCommenterSelectionChangedEvent(object sender, SelectionChangedEventArgs e) {
			if (lineCommentersComboBox.SelectedIndex == 0)
				editor.Document.Language.RegisterLineCommenter(new LineBasedLineCommenter() { StartDelimiter = "//" });
			else
				editor.Document.Language.RegisterLineCommenter(new RangeLineCommenter() { StartDelimiter = "/*", EndDelimiter = "*/" });
		}
		
    }

}