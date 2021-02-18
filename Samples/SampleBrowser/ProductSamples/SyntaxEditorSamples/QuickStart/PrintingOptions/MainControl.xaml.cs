using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using ActiproSoftware.Windows.Media;
using ActiproSoftware.Windows.Themes;
using ActiproSoftware.Windows.Controls.Docking;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.PrintingOptions {

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

			if (BrowserInteropHelper.IsBrowserHosted) {
				Grid.SetColumnSpan(refreshPrintPreviewButton, 2);
				showPrintPreviewDialogButton.Visibility = Visibility.Collapsed;
			}
       
			this.Loaded += new RoutedEventHandler(OnLoaded);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the control is loaded.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnLoaded(object sender, RoutedEventArgs e) {
			this.Dispatcher.BeginInvoke(DispatcherPriority.Send, (DispatcherOperationCallback)delegate(object arg) {
				this.RefreshPrintPreview();
				return null;
			}, null);
		}
		
		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnRefreshPrintPreviewButtonClick(object sender, RoutedEventArgs e) {
			this.RefreshPrintPreview();
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnShowPrintPreviewDialogButtonClick(object sender, RoutedEventArgs e) {
			editor.ShowPrintPreviewDialog();
		}

		/// <summary>
		/// Refreshes the print preview.
		/// </summary>
		private void RefreshPrintPreview() {
			editor.PrintSettings.DocumentTitle = documentTitleTextBox.Text;
			editor.PrintSettings.IsDocumentTitleMarginVisible = (isDocumentTitleMarginVisibleCheckBox.IsChecked == true);
			editor.PrintSettings.IsLineNumberMarginVisible = (isLineNumberMarginVisibleCheckBox.IsChecked == true);
			editor.PrintSettings.IsPageNumberMarginVisible = (isPageNumberMarginVisibleCheckBox.IsChecked == true);
			editor.PrintSettings.IsSyntaxHighlightingEnabled = (isSyntaxHighlightingEnabledCheckBox.IsChecked == true);
			editor.PrintSettings.IsWordWrapGlyphMarginVisible = (isWordWrapGlyphMarginVisibleCheckBox.IsChecked == true);
			editor.PrintSettings.IsWhitespaceVisible = (isWhitespaceVisibleCheckBox.IsChecked == true);
			editor.PrintSettings.AreCollapsedOutliningNodesAllowed = (areCollapsedOutliningNodesAllowedCheckBox.IsChecked == true);
			editor.PrintSettings.AreIndentationGuidesVisible = (areIndentationGuidesVisibleCheckBox.IsChecked == true);
			editor.PrintSettings.AreSquiggleLinesVisible = (areSquiggleLinesVisibleCheckBox.IsChecked == true);

			documentViewer.Document = editor.PrintSettings.CreateFixedDocument(editor);
		}

    }

}