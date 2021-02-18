using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Provides the code viewer window.
	/// </summary>
	public partial class CodeViewerWindow {

		private int										updateVersion;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the <c>CodeViewerWindow </c> class.
		/// </summary>
		/// <param name="viewModel">The application view-model.</param>
		public CodeViewerWindow(ApplicationViewModel viewModel) {
			this.DataContext = viewModel;

			InitializeComponent();

			// Register SyntaxEditor display item classification types
			new DisplayItemClassificationTypeProvider().RegisterAll();
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when a property changes.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="SelectionChangedEventArgs"/> containing data related to this event.</param>
		private void OnShellListBoxSelectionChanged(object sender, SelectionChangedEventArgs e) {
			updateVersion = (updateVersion + 1) % 1000;
			var requestedUpdateVersion = updateVersion;
			
			this.Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)(() => {
				if (updateVersion == requestedUpdateVersion)
					this.UpdateSourcePane();
			}));
		}

		/// <summary>
		/// Updates the source pane.
		/// </summary>
		private void UpdateSourcePane() {
			var selectedShellObject = shellListBox.SelectedShellObject;
			if (selectedShellObject != null) {
				if (selectedShellObject.IsFolder) {
					editorDockPanel.Visibility = Visibility.Collapsed;
				}
				else {
					// NOTE: Any changes to supported extensions need to be made in CodeViewerTreeFilter as well
					switch (Path.GetExtension(selectedShellObject.ParsingName).ToUpperInvariant()) {
						case ".CS":
							editor.Document.Language = this.ViewModel.SyntaxLanguageCSharp;
							break;
						case ".XAML":
							editor.Document.Language = this.ViewModel.SyntaxLanguageXaml;
							break;
						default:
							editor.Document.Language = SyntaxLanguage.PlainText;
							break;
					}

					// Load the file
					try {
						editor.Document.LoadFile(selectedShellObject.ParsingName);
					}
					catch (Exception ex) {
						editor.Document.Language = SyntaxLanguage.PlainText;
						editor.Document.SetText(String.Format("An exception occurred while loading the file '{0}':\r\n\r\n{1}", selectedShellObject.ParsingName, ex.Message));
					}

					editorDockPanel.Visibility = Visibility.Visible;
				}
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when the window is closed.
		/// </summary>
		/// <param name="e">The <see cref="EventArgs"/> containing data related to this event.</param>
		protected override void OnClosed(EventArgs e) {
			// Call the base method
			base.OnClosed(e);

			// Dispose any unmanaged resources held by the shell instances now that the UI is closing
			shellListBox.DisposeShellInstances();
		}

		/// <summary>
		/// Gets the view-model for this view.
		/// </summary>
		/// <value>The view-model for this view.</value>
		public ApplicationViewModel ViewModel => (ApplicationViewModel)this.DataContext;

	}

}
