using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Win32;
using ActiproSoftware.Windows.Controls.Docking;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Media;
using ActiproSoftware.Windows.Themes;

namespace ActiproSoftware.ProductSamples.ThemesSamples.Demo.NotepadEnhanced {

	/// <summary>
	/// Provides the main window for this sample.
	/// </summary>
	public partial class MainWindow {

		private int					documentIndex			= 1;
		private TextDocumentWindow	primaryDocumentWindow;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainWindow</c> class.
		/// </summary>
		public MainWindow() {
			InitializeComponent();
			
			// Create an initial document
			this.Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() => {
				this.CreateTextDocumentWindow(null).Text = @"This demo shows how an enhanced Notepad-like application can be 
created with cohesive visual themes and additional functionality.

It is comprised of a combination of Actipro-themed native WPF controls and 
Actipro products such as Docking/MDI and SyntaxEditor.

Actipro Themes uses the same common brush resource pool for its native WPF 
control styles and custom control styles.  Thus no matter which native or 
Actipro controls you use together, the appearance will consistently look great.
";
			}));
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Creates a new text <see cref="DocumentWindow"/>.
		/// </summary>
		/// <param name="filename">The filename to open; <c>null</c> to create a new document.</param>
		/// <returns>The <see cref="DocumentWindow"/> that was created.</returns>
		private TextDocumentWindow CreateTextDocumentWindow(string filename) {
			string title, text;
			if (filename != null) {
				// Open an existing document
				title = System.IO.Path.GetFileName(filename);
				text = File.ReadAllText(filename);
			}
			else {
				// Create a new document
				title = String.Format("Document{0}.txt", documentIndex);
				text = String.Format("Document {0} created at {1}.", documentIndex, DateTime.Now);
				documentIndex++;
			}

			// Create the document
			TextDocumentWindow documentWindow = new TextDocumentWindow();
			documentWindow.Description = "Text document";
			documentWindow.FileName = filename;
			documentWindow.ImageSource = new BitmapImage(new Uri("/Images/Icons/TextDocument16.png", UriKind.Relative));
			documentWindow.Title = title;
			documentWindow.Text = text;
			dockSite.DocumentWindows.Add(documentWindow);

			// Activate the document
			documentWindow.Activate();

			return documentWindow;
		}

		/// <summary>
		/// Occurs when a menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnCascadeMenuItemClick(object sender, RoutedEventArgs e) {
			tabbedMdiHost.Cascade();
		}
		
		/// <summary>
		/// Occurs when the primary document is changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowEventArgs</c> that contains data related to this event.</param>
		private void OnDockSitePrimaryDocumentChanged(object sender, DockingWindowEventArgs e) {
			this.PrimaryDocumentWindow = dockSite.PrimaryDocument as TextDocumentWindow;
		}

		/// <summary>
		/// Occurs when the menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnExitMenuItemClick(object sender, RoutedEventArgs e) {
			this.Close();
		}
		
		/// <summary>
		/// Occurs when a menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnNewFileMenuItemClick(object sender, RoutedEventArgs e) {
			// Create a new document window
			this.CreateTextDocumentWindow(null);
		}

		/// <summary>
		/// Occurs when a menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnOpenFileMenuItemClick(object sender, RoutedEventArgs e) {
			// Show a file open dialog
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.CheckFileExists = true;
			dialog.Multiselect = false;
			dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
			if (dialog.ShowDialog() == true) {
				// Create a document window
				this.CreateTextDocumentWindow(dialog.FileName);
			}
		}
		
		/// <summary>
		/// Occurs when a menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnTileHorizontallyMenuItemClick(object sender, RoutedEventArgs e) {
			tabbedMdiHost.TileHorizontally();
		}

		/// <summary>
		/// Occurs when a menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnTileVerticallyMenuItemClick(object sender, RoutedEventArgs e) {
			tabbedMdiHost.TileVertically();
		}

		/// <summary>
		/// Occurs when the selection changes.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="EditorViewSelectionEventArgs"/> that contains the event data.</param>
		private void OnViewSelectionChanged(object sender, EditorViewSelectionEventArgs e) {
			// Quit if this event is not for the active view
			if (!e.View.IsActive)
				return;

			// Update line, col, and character display
			this.UpdateStatusBarTextLocation(e.CaretPosition.DisplayLine, e.CaretDisplayCharacterColumn, e.CaretPosition.DisplayCharacter);
		}

		/// <summary>
		/// Gets or sets the primary document window.
		/// </summary>
		/// <value>The primary document window.</value>
		private TextDocumentWindow PrimaryDocumentWindow {
			get {
				return primaryDocumentWindow;
			}
			set {
				if (primaryDocumentWindow == value)
					return;

				if (primaryDocumentWindow != null)
					primaryDocumentWindow.ViewSelectionChanged -= OnViewSelectionChanged;
				
				primaryDocumentWindow = value;

				if (primaryDocumentWindow != null) {
					primaryDocumentWindow.ViewSelectionChanged += OnViewSelectionChanged;
					messagePanel.Content = primaryDocumentWindow.FileName ?? "Ready";
					this.UpdateStatusBarTextLocation(primaryDocumentWindow.CaretPosition.DisplayLine, primaryDocumentWindow.CaretColumn, primaryDocumentWindow.CaretPosition.DisplayCharacter);
				}
				else {
					messagePanel.Content = "Ready";
					this.UpdateStatusBarTextLocation(null, null, null);
				}
			}
		}

		/// <summary>
		/// Updates line, col, and character display.
		/// </summary>
		/// <param name="line">The line value.</param>
		/// <param name="col">The column value.</param>
		/// <param name="character">The character value.</param>
		private void UpdateStatusBarTextLocation(int? line, int? col, int? character) {
			if (line.HasValue) {
				linePanel.Text = String.Format("Ln {0}", line);
				columnPanel.Text = String.Format("Col {0}", col);
				characterPanel.Text = String.Format("Ch {0}", character);
			}
			else {
				linePanel.Text = null;
				columnPanel.Text = null;
				characterPanel.Text = null;
			}
		}
		
	}

}