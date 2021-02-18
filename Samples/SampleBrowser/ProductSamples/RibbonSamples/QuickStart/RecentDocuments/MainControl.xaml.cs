using ActiproSoftware.ProductSamples.RibbonSamples.Common;
using ActiproSoftware.Windows.DocumentManagement;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.RibbonSamples.QuickStart.RecentDocuments {

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
			// Register UI providers before doing anything else, including InitializeComponent
			ActiproSoftware.ProductSamples.RibbonSamples.Demo.DocumentEditor.ApplicationCommands.RegisterUIProvidersForNonRibbonCommands();

			InitializeComponent();
			
			// Populate some sample recent documents
			DocumentReferenceGenerator.BindRecentDocumentManager(recentDocManager);

			// Add command bindings
            this.CommandBindings.Add(new CommandBinding(System.Windows.Input.ApplicationCommands.Open, OnOpenExecute));
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// COMMAND HANDLERS
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private void OnOpenExecute(object sender, ExecutedRoutedEventArgs e) {
			if (e.Parameter is IDocumentReference) {
				// Process recent document clicks
				MessageBox.Show("Open document '" + ((IDocumentReference)e.Parameter).Name + "' here.", "Open Recent Document", MessageBoxButton.OK, MessageBoxImage.Information);
				return;
			}

			// Show the open file dialog
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.CheckFileExists = true;
			dialog.Filter = "All Files (*.*)|*.*";
			if (dialog.ShowDialog() == true) {
				// Add a new document reference to the recent document manager by calling the helper notify method...
				//   Alternatively you could create a DocumentReference and add it to recentDocManager.Documents manually
				//   but the benefit of this helper is that it checks for an existing Uri match so that you don't add duplicates
				recentDocManager.NotifyDocumentOpened(new Uri(dialog.FileName));
			}

		}
	}
}