using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Win32;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Parsing.LLParser.Implementation;
using ActiproSoftware.Text.Searching;
using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Docking;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Extensions;
using System.Windows.Threading;

namespace ActiproSoftware.ProductSamples.DockingSamples.Demo.SimpleIde {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private int		documentIndex;
		
		#region Dependency Properties
		
		/// <summary>
		/// Identifies the <see cref="IsWindowActivationEventOutputEnabled"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="IsWindowActivationEventOutputEnabled"/> dependency property.</value>
		public static readonly DependencyProperty IsWindowActivationEventOutputEnabledProperty = DependencyProperty.Register("IsWindowActivationEventOutputEnabled", typeof(bool), typeof(MainControl), new PropertyMetadata(true));
		
		/// <summary>
		/// Identifies the <see cref="IsWindowRegistrationEventOutputEnabled"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="IsWindowRegistrationEventOutputEnabled"/> dependency property.</value>
		public static readonly DependencyProperty IsWindowRegistrationEventOutputEnabledProperty = DependencyProperty.Register("IsWindowRegistrationEventOutputEnabled", typeof(bool), typeof(MainControl), new PropertyMetadata(false));
		
		#endregion

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			CreateSyntaxEditorDocument(".txt", "About.txt", @"This demo shows off a simple example of building an IDE using tool windows 
and a multiple document interface (MDI), powerful UI mechanisms made 
available with the Actipro Docking & MDI control product.
Everything from floating MDI containers and pinned/preview tabs to
complete MVVM support and much more is available.

This particular demo also shows integration with the Actipro SyntaxEditor
control and several of its premium syntax language add-ons.
Once you open a C#, VB, or other code document, take a peek at the
Document Outline tool window and use the Find & Replace tool window.

Browse through this demo and the rest of the included samples to discover
more about the enormous feature set this product provides.");
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Appends a message to the events <see cref="ListBox"/>.
		/// </summary>
		/// <param name="text">The text to append.</param>
		private void AppendMessage(string text) {
			var item = new ListBoxItem();
			item.Content = text;
			eventsListBox.Items.Add(item);
			eventsListBox.SelectedItem = item;
			eventsListBox.ScrollIntoView(item);
		}

		/// <summary>
		/// Creates and activates a new editor document.
		/// </summary>
		/// <param name="fileName">The file name.</param>
		/// <param name="extension">The file extension, used to determine a language.</param>
		/// <param name="text">The optional text to use.</param>
		private void CreateSyntaxEditorDocument(string extension, string fileName, string text) {
			if (fileName != null) {
				// Load the file's text
				try {
					if (File.Exists(fileName))
						text = File.ReadAllText(fileName);
				}
				catch {
					text = String.Empty;
				}
			}
			else {
				// Ensure a filename has been set
				fileName = String.Format("Document{0}{1}", ++documentIndex, extension.ToLowerInvariant());
			}

			// Create document data
			var data = new DocumentData();
			data.FileName = fileName;
			data.NotifyDocumentOutlineUpdated = this.NotifyDocumentOutlineUpdated;
			data.NotifySearchAction = this.NotifyEditorViewSearch;

			// Create the document
			var documentWindow = new EditorDocumentWindow(data, text);
			dockSite.DocumentWindows.Add(documentWindow);

			// Activate the document
			documentWindow.Activate();
		}
		
		/// <summary>
		/// Occurs when a floating window is opening, allowing for customization before it is displayed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>RoutedEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteFloatingWindowOpening(object sender, FloatingWindowOpeningEventArgs e) {
			if (limitFloatingWindowInitialSizeMenuItem.IsChecked) {
				// Make sure the long side is no longer than 600, and the short side is no longer than 300
				if (e.Size.Width > e.Size.Height)
					e.Size = new Size(Math.Min(600.0, e.Size.Width), Math.Min(300.0, e.Size.Height));
				else
					e.Size = new Size(Math.Min(300.0, e.Size.Width), Math.Min(600.0, e.Size.Height));
			}
		}
		
		/// <summary>
		/// Occurs when the primary document is changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>RoutedEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteMdiKindChanged(object sender, RoutedEventArgs e) {
			this.AppendMessage(String.Format("MdiKindChanged: Kind={0}", dockSite.MdiKind));
		}
		
		/// <summary>
		/// Occurs when a docking-related context menu is opening, allowing for customization before it is displayed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingMenuEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteMenuOpening(object sender, DockingMenuEventArgs e) {
			var sb = new StringBuilder(String.Format("WindowContextMenu: Kind={0}", e.Kind));

			if (e.Window != null) {
				sb.AppendFormat(", Title={0} ", e.Window.Title);

				if (e.Window == outputToolWindow) {
					e.Menu.Items.Add(new Separator());

					var menuItem = new MenuItem() { Header = "Activation Events", IsCheckable = true };
					menuItem.BindToProperty(MenuItem.IsCheckedProperty, this, "IsWindowActivationEventOutputEnabled", BindingMode.TwoWay);
					e.Menu.Items.Add(menuItem);

					menuItem = new MenuItem() { Header = "Registration Events", IsCheckable = true };
					menuItem.BindToProperty(MenuItem.IsCheckedProperty, this, "IsWindowRegistrationEventOutputEnabled", BindingMode.TwoWay);
					e.Menu.Items.Add(menuItem);
				}
			}

			this.AppendMessage(sb.ToString());
		}

		/// <summary>
		/// Occurs when a new docking window is requested, generally via a user click on a new tab button.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>RoutedEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteNewWindowRequested(object sender, RoutedEventArgs e) {
			this.AppendMessage("NewWindowRequested");
		}

		/// <summary>
		/// Occurs when the primary document is changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowEventArgs</c> that contains data related to this event.</param>
		private void OnDockSitePrimaryDocumentChanged(object sender, DockingWindowEventArgs e) {
			this.UpdatePrimaryDocumentBindings();

			if (e.Window != null)
				this.AppendMessage(String.Format("PrimaryDocumentChanged: Title={0}", e.Window.Title));
			else
				this.AppendMessage("PrimaryDocumentChanged: (none)");
		}

		/// <summary>
		/// Occurs when a docking window is activated.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteWindowActivated(object sender, DockingWindowEventArgs e) {
			if (this.IsWindowActivationEventOutputEnabled)
				this.AppendMessage(String.Format("WindowActivated: Title={0}", e.Window.Title));
		}
		
		/// <summary>
		/// Occurs when an auto-hide popup has been closed that displayed a tool window.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteWindowAutoHidePopupClosed(object sender, DockingWindowEventArgs e) {
			this.AppendMessage(String.Format("AutoHidePopupClosed: Title={0}", e.Window.Title));
		}

		/// <summary>
		/// Occurs when an auto-hide popup has been opened that displays a tool window.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteWindowAutoHidePopupOpened(object sender, DockingWindowEventArgs e) {
			this.AppendMessage(String.Format("AutoHidePopupOpened: Title={0}", e.Window.Title));
		}
		
		/// <summary>
		/// Occurs when a docking window is deactivated.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteWindowDeactivated(object sender, DockingWindowEventArgs e) {
			if (this.IsWindowActivationEventOutputEnabled)
				this.AppendMessage(String.Format("WindowDeactivated: Title={0}", e.Window.Title));
		}
		
		/// <summary>
		/// Occurs when a docking window is registered.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteWindowRegistered(object sender, DockingWindowEventArgs e) {
			if (this.IsWindowRegistrationEventOutputEnabled)
				this.AppendMessage(String.Format("WindowRegistered: Title={0}", e.Window.Title));
		}

		/// <summary>
		/// Occurs before one or more docking windows are auto-hidden, allowing for side customization.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowsAutoHidingEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteWindowsAutoHiding(object sender, DockingWindowsAutoHidingEventArgs e) {
			if (forceAutoHideToBottomMenuItem.IsChecked)
				e.Side = Side.Bottom;

			var count = e.Windows.Count();
			if (count == 1)
				this.AppendMessage(String.Format("WindowsAutoHiding: Title={0}, Side={1}", e.Windows.First().Title, e.Side));
			else
				this.AppendMessage(String.Format("WindowsAutoHiding: Count={0}, Side={1}", count, e.Side));
		}
		
		/// <summary>
		/// Occurs after one or more docking windows have been closed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowsEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteWindowsClosed(object sender, DockingWindowsEventArgs e) {
			var count = e.Windows.Count();
			if (count == 1)
				this.AppendMessage(String.Format("WindowsClosed: Title={0}", e.Windows.First().Title));
			else
				this.AppendMessage(String.Format("WindowsClosed: Count={0}", count));
		}
		
		/// <summary>
		/// Occurs before one or more docking windows are closed, allowing for cancellation of the close.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowsEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteWindowsClosing(object sender, DockingWindowsEventArgs e) {
			var count = e.Windows.Count();
			if (count == 1)
				this.AppendMessage(String.Format("WindowsClosing: Title={0}", e.Windows.First().Title));
			else
				this.AppendMessage(String.Format("WindowsClosing: Count={0}", count));
		}

		/// <summary>
		/// Occurs after one or more docking windows are dragged by the end user.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowsEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteWindowsDragged(object sender, DockingWindowsEventArgs e) {
			var count = e.Windows.Count();
			if (count == 1)
				this.AppendMessage(String.Format("WindowsDragged: Title={0}", e.Windows.First().Title));
			else
				this.AppendMessage(String.Format("WindowsDragged: Count={0}", count));
		}

		/// <summary>
		/// Occurs before one or more docking windows are dragged by the end user.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowsEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteWindowsDragging(object sender, DockingWindowsEventArgs e) {
			var count = e.Windows.Count();
			if (count == 1)
				this.AppendMessage(String.Format("WindowsDragging: Title={0}", e.Windows.First().Title));
			else
				this.AppendMessage(String.Format("WindowsDragging: Count={0}", count));
		}
		
		/// <summary>
		/// Occurs when one or more docking windows are dragged over a new dock target, allowing for certain dock guides to be hidden.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowsDragOverEventArgs</c> that contains the event data.</param>
		private void OnDockSiteWindowsDragOver(object sender, DockingWindowsDragOverEventArgs e) {
			// If this option is checked, prevent all dragged windows from being dropped anywhere other than in MDI
			//   or in a floating window by themselves
			if (restrictDraggedWindowsMenuItem.IsChecked) {
				if ((e.Target is TabbedMdiHost) || (e.Target is TabbedMdiContainer))
					e.AllowedDockGuideKinds = DockGuideKinds.Inner | DockGuideKinds.Center;
				else
					e.AllowedDockGuideKinds = DockGuideKinds.None;
			}

			// NOTE: You could create other restrictions here like only allowing left/right or top/bottom dock guides via AllowedDockGuideKinds too
		}

		/// <summary>
		/// Occurs after one or more docking windows have been opened.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowsEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteWindowsOpened(object sender, DockingWindowsEventArgs e) {
			var count = e.Windows.Count();
			if (count == 1)
				this.AppendMessage(String.Format("WindowsOpened: Title={0}", e.Windows.First().Title));
			else
				this.AppendMessage(String.Format("WindowsOpened: Count={0}", count));
		}
		
		/// <summary>
		/// Occurs before one or more docking windows are opened.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowsEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteWindowsOpening(object sender, DockingWindowsEventArgs e) {
			var count = e.Windows.Count();
			if (count == 1)
				this.AppendMessage(String.Format("WindowsOpening: Title={0}", e.Windows.First().Title));
			else
				this.AppendMessage(String.Format("WindowsOpening: Count={0}", count));
		}
		
		/// <summary>
		/// Occurs after one or more docking windows' states have changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteWindowsStateChanged(object sender, DockingWindowsEventArgs e) {
			var count = e.Windows.Count();
			if (count == 1)
				this.AppendMessage(String.Format("WindowsStateChanged: Title={0}, State={1}", e.Windows.First().Title, e.Windows.First().State));
			else
				this.AppendMessage(String.Format("WindowsStateChanged: Count={0}, State={1}", count, e.Windows.First().State));
		}
		
		/// <summary>
		/// Occurs when a docking window is unregistered.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteWindowUnregistered(object sender, DockingWindowEventArgs e) {
			if (this.IsWindowRegistrationEventOutputEnabled)
				this.AppendMessage(String.Format("WindowUnregistered: Title={0}", e.Window.Title));
		}
		
		/// <summary>
		/// Occurs when the mouse is double-clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="MouseButtonEventArgs"/> that contains the event data.</param>
		private void OnFindResultsTextBoxDoubleClick(object sender, MouseButtonEventArgs e) {
			// Quit if there is no editor or result set stored yet
			var editor = findResultsToolWindow.DataContext as SyntaxEditor;
			var resultSet = findResultsTextBox.DataContext as ISearchResultSet;
			if ((editor == null) || (resultSet == null))
				return;

			int charIndex = findResultsTextBox.GetCharacterIndexFromPoint(e.GetPosition(findResultsTextBox), true);
			int lineIndex = findResultsTextBox.GetLineIndexFromCharacterIndex(charIndex);

			int resultIndex = lineIndex - 1;  // Account for first line in results displaying search info
			if ((resultIndex >= 0) && (resultIndex < resultSet.Results.Count)) {
				// A valid result was clicked
				ISearchResult result = resultSet.Results[resultIndex];
				if (result.ReplaceSnapshotRange.IsDeleted) {
					// Find result
					editor.ActiveView.Selection.SelectRange(result.FindSnapshotRange.TranslateTo(editor.ActiveView.CurrentSnapshot, TextRangeTrackingModes.Default).TextRange);
				}
				else {
					// Replace result
					editor.ActiveView.Selection.SelectRange(result.ReplaceSnapshotRange.TranslateTo(editor.ActiveView.CurrentSnapshot, TextRangeTrackingModes.Default).TextRange);
				}

				// Focus the editor
				editor.Focus();
			}
		}

		/// <summary>
		/// Occurs when the menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>RoutedEventArgs</c> that contains data related to this event.</param>
		private void OnNewDocumentMenuItemClick(object sender, RoutedEventArgs e) {
			var menuItem = (MenuItem)sender;
			var extension = menuItem.Tag as string ?? ".txt";
			this.CreateSyntaxEditorDocument(extension, null, null);
		}

		/// <summary>
		/// Occurs when the menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>RoutedEventArgs</c> that contains data related to this event.</param>
		private void OnOpenDocumentMenuItemClick(object sender, RoutedEventArgs e) {
			// Show a file open dialog
			var dialog = new OpenFileDialog();
			dialog.CheckFileExists = true;
			dialog.Multiselect = false;
			dialog.Filter = "Code files (*.txt;*.cs;*.js;*.py;*.vb;*.xml)|*.txt;*.cs;*.js;*.py;*.vb;*.xml|All files (*.*)|*.*";
			if (dialog.ShowDialog() == true)
				this.CreateSyntaxEditorDocument(Path.GetExtension(dialog.FileName), dialog.FileName, String.Empty);
		}

		/// <summary>
		/// Occurs when the <c>SyntaxEditor.IsOverwriteModeActiveChanged</c> event occurs.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnSyntaxEditorIsOverwriteModeActiveChanged(object sender, RoutedEventArgs e) {
			this.UpdateStatusBar();
		}
		
		/// <summary>
		/// Occurs when the editor view's selection is changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="EditorViewSelectionEventArgs"/> that contains the event data.</param>
		private void OnSyntaxEditorViewSelectionChanged(object sender, EditorViewSelectionEventArgs e) {
			// Quit if this event is not for the active view
			if (!e.View.IsActive)
				return;

			this.UpdateStatusBar();
		}
		
		/// <summary>
		/// Updates the primary document bindings.
		/// </summary>
		private void UpdatePrimaryDocumentBindings() {
			propGrid.IsReadOnly = false;

			if (searchView.SyntaxEditor != null) {
				searchView.SyntaxEditor.IsOverwriteModeActiveChanged -= OnSyntaxEditorIsOverwriteModeActiveChanged;
				searchView.SyntaxEditor.ViewSelectionChanged -= OnSyntaxEditorViewSelectionChanged;
			}

			var primaryDocument = dockSite.PrimaryDocument as EditorDocumentWindow;
			if (primaryDocument != null) {
				propGrid.DataObject = primaryDocument.DataContext;
				searchView.SyntaxEditor = primaryDocument.Editor;
				propGrid.IsReadOnly = true;
			}
			else {
				propGrid.DataObject = null;
				searchView.SyntaxEditor = null;
			}

			if (searchView.SyntaxEditor != null) {
				searchView.SyntaxEditor.IsOverwriteModeActiveChanged += OnSyntaxEditorIsOverwriteModeActiveChanged;
				searchView.SyntaxEditor.ViewSelectionChanged += OnSyntaxEditorViewSelectionChanged;
			}

			this.UpdateStatusBar();

			this.NotifyDocumentOutlineUpdated(primaryDocument);
		}

		/// <summary>
		/// Updates the statusbar.
		/// </summary>
		private void UpdateStatusBar() {
			var editor = searchView.SyntaxEditor;
			if (editor != null) {
				linePanel.Text = String.Format("Ln {0}", editor.ActiveView.Selection.CaretPosition.DisplayLine);
				columnPanel.Text = String.Format("Col {0}", editor.ActiveView.Selection.CaretDisplayCharacterColumn);
				characterPanel.Text = String.Format("Ch {0}", editor.ActiveView.Selection.CaretPosition.DisplayCharacter);
				overwriteModePanel.Content = (editor.IsOverwriteModeActive ? "OVR" : "INS");
			}
			else {
				linePanel.Text = null;
				columnPanel.Text = null;
				characterPanel.Text = null;
				overwriteModePanel.Content = "INS";
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets whether docking window activation event output is enabled.
		/// </summary>
		/// <value>
		/// <c>true</c> if docking window activation event output is enabled; otherwise, <c>false</c>.
		/// </value>
		public bool IsWindowActivationEventOutputEnabled {
			get {
				return (bool)this.GetValue(IsWindowActivationEventOutputEnabledProperty);
			}
			set {
				this.SetValue(IsWindowActivationEventOutputEnabledProperty, value);
			}
		}
		
		/// <summary>
		/// Gets or sets whether docking window registration event output is enabled.
		/// </summary>
		/// <value>
		/// <c>true</c> if docking window registration event output is enabled; otherwise, <c>false</c>.
		/// </value>
		public bool IsWindowRegistrationEventOutputEnabled {
			get {
				return (bool)this.GetValue(IsWindowRegistrationEventOutputEnabledProperty);
			}
			set {
				this.SetValue(IsWindowRegistrationEventOutputEnabledProperty, value);
			}
		}

		/// <summary>
		/// Occurs when the document outline is updated.
		/// </summary>
		/// <param name="window">The window that was updated.</param>
		public void NotifyDocumentOutlineUpdated(EditorDocumentWindow window) {
			if (window != null) {
				documentOutlineToolWindow.Title = "Document Outline - " + window.Title;

				var parseData = window.Editor.Document.ParseData as LLParseData;
				if ((parseData != null) && (parseData.Ast != null)) {
					this.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, (Action)(() => {
						var astString = parseData.Ast.ToTreeString(0);
						if (astString.Length > 10000)
							documentOutlineTextBox.Text = "(AST is too large to display)";
						else
							documentOutlineTextBox.Text = astString;
					}));
					return;
				}
			}
			else
				documentOutlineToolWindow.Title = "Document Outline";

			documentOutlineTextBox.Text = "(none)";
		}
		
		/// <summary>
		/// Occurs when a search operation occurs in a view.
		/// </summary>
		/// <param name="window">The window that was searched.</param>
		/// <param name="resultSet">An <see cref="ISearchResultSet"/> that contains the search results.</param>
		public void NotifyEditorViewSearch(EditorDocumentWindow window, ISearchResultSet resultSet) {
			// Show the results
			findResultsToolWindow.Title = String.Format("Find Results - {0} match{1}", resultSet.Results.Count, (resultSet.Results.Count == 1 ? String.Empty : "es"));
			findResultsToolWindow.DataContext = window.Editor;
			findResultsTextBox.Text = resultSet.ToString();
			findResultsTextBox.DataContext = resultSet;

			if (findResultsToolWindow.IsOpen)
				findResultsToolWindow.Activate(false);

			if (resultSet.Results.Count > 0)
				window.Activate();
		}
		
		/// <summary>
		/// Notifies the sample that it has been unloaded.
		/// </summary>
		public override void NotifyUnloaded() {
			// Ensure the DataObject property is cleared when the primary UI closes... don't use PropertyGrid.CanClearDataObjectOnUnload 
			//   since the property grid is in a tool window
			propGrid.DataObject = null;
		}

	}

}
