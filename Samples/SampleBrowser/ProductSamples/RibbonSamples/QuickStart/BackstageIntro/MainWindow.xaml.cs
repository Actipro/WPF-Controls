using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Win32;
using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Controls.Ribbon;
using ActiproSoftware.Windows.DocumentManagement;
using ActiproSoftware.Windows.Media;
using ActiproSoftware.ProductSamples.RibbonSamples.Common;

namespace ActiproSoftware.ProductSamples.RibbonSamples.QuickStart.BackstageIntro {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainWindow : RibbonWindow {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainWindow</c> class.
		/// </summary>
		public MainWindow() {
			InitializeComponent();
			
			// Populate some sample recent documents
			DocumentReferenceGenerator.BindRecentDocumentManager(recentDocManager);
			

			// Add command bindings
            this.CommandBindings.Add(new CommandBinding(System.Windows.Input.ApplicationCommands.Open, OnOpenExecute));
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the application menu opens or closes.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="BooleanPropertyChangedRoutedEventArgs"/> that contains the event data.</param>
		private void OnIsApplicationMenuOpenChanged(object sender, BooleanPropertyChangedRoutedEventArgs e) {
			// If opening, ensure the that the New is always selected
			if (ribbon.IsApplicationMenuOpen)
				appMenu.SelectedItem = newBackstageTab;
		}
		
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
		}
	}
}