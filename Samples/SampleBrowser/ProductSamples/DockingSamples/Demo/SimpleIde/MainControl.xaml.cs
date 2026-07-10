using ActiproSoftware.Text;
using ActiproSoftware.Text.Parsing.LLParser.Implementation;
using ActiproSoftware.Text.Searching;
using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Docking;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Extensions;
using Microsoft.Win32;
using System.Windows.Threading;

namespace ActiproSoftware.ProductSamples.DockingSamples.Demo.SimpleIde;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	private int _documentIndex;

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="IsWindowActivationEventOutputEnabled"/> property.
	/// </summary>
	public static readonly DependencyProperty IsWindowActivationEventOutputEnabledProperty
		= DependencyProperty.Register(nameof(IsWindowActivationEventOutputEnabled), typeof(bool), typeof(MainControl), new PropertyMetadata(defaultValue: true));

	/// <summary>
	/// Defines the <see cref="IsWindowRegistrationEventOutputEnabled"/> property.
	/// </summary>
	public static readonly DependencyProperty IsWindowRegistrationEventOutputEnabledProperty
		= DependencyProperty.Register(nameof(IsWindowRegistrationEventOutputEnabled), typeof(bool), typeof(MainControl), new PropertyMetadata(defaultValue: false));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		CreateSyntaxEditorDocument(".cs", fileName: null, text: null);

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

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Appends a message to the events <see cref="ListBox"/>.
	/// </summary>
	/// <param name="text">The text to append.</param>
	private void AppendMessage(string text) {
		var item = new ListBoxItem {
			Content = text
		};
		eventsListBox.Items.Add(item);
		eventsListBox.SelectedItem = item;
		eventsListBox.ScrollIntoView(item);
	}

	/// <summary>
	/// Creates and activates a new editor document.
	/// </summary>
	/// <param name="extension">The file extension, used to determine a language.</param>
	/// <param name="fileName">The file name.</param>
	/// <param name="text">The optional text to use.</param>
	private void CreateSyntaxEditorDocument(string extension, string? fileName, string? text) {
		if (fileName is not null) {
			// Load the file's text
			try {
				if (File.Exists(fileName))
					text = File.ReadAllText(fileName);
			}
			catch {
				text = string.Empty;
			}
		}
		else {
			// Ensure a filename has been set
			fileName = string.Format("Document{0}{1}", ++_documentIndex, extension.ToLowerInvariant());
		}

		// Create document data
		var data = new DocumentData {
			FileName = fileName,
			NotifyDocumentOutlineUpdated = NotifyDocumentOutlineUpdated,
			NotifySearchAction = NotifyEditorViewSearch
		};

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
	/// <param name="e">The event data.</param>
	private void OnDockSiteFloatingWindowOpening(object sender, FloatingWindowOpeningEventArgs e) {
		if (limitFloatingWindowInitialSizeMenuItem.IsChecked) {
			// Make sure the long side is no longer than 600, and the short side is no longer than 300
			e.Size = (e.Size.Width > e.Size.Height)
				? new Size(Math.Min(600.0, e.Size.Width), Math.Min(300.0, e.Size.Height))
				: new Size(Math.Min(300.0, e.Size.Width), Math.Min(600.0, e.Size.Height));
		}
	}

	/// <summary>
	/// Occurs when the primary document is changed.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnDockSiteMdiKindChanged(object sender, RoutedEventArgs e)
		=> AppendMessage(string.Format("MdiKindChanged: Kind={0}", dockSite.MdiKind));

	/// <summary>
	/// Occurs when a docking-related context menu is opening, allowing for customization before it is displayed.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnDockSiteMenuOpening(object sender, DockingMenuEventArgs e) {
		var sb = new StringBuilder(string.Format("WindowContextMenu: Kind={0}", e.Kind));

		if ((e.Window is { } window) && (e.Menu is { } menu)) {
			sb.AppendFormat(", Title={0} ", window.Title);

			// Append custom context menu items to the Output tool window
			if (window == outputToolWindow) {
				menu.Items.Add(new Separator());

				var menuItem = new MenuItem() { Header = "Activation Events", IsCheckable = true };
				menuItem.BindToProperty(MenuItem.IsCheckedProperty, this, nameof(IsWindowActivationEventOutputEnabled), BindingMode.TwoWay);
				menu.Items.Add(menuItem);

				menuItem = new MenuItem() { Header = "Registration Events", IsCheckable = true };
				menuItem.BindToProperty(MenuItem.IsCheckedProperty, this, nameof(IsWindowRegistrationEventOutputEnabled), BindingMode.TwoWay);
				menu.Items.Add(menuItem);
			}
		}

		AppendMessage(sb.ToString());
	}

	/// <summary>
	/// Occurs when a new docking window is requested, generally via a user click on a new tab button.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnDockSiteNewWindowRequested(object sender, RoutedEventArgs e)
		=> AppendMessage("NewWindowRequested");

	/// <summary>
	/// Occurs when the primary document is changed.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnDockSitePrimaryDocumentChanged(object sender, DockingWindowEventArgs e) {
		UpdatePrimaryDocumentBindings();

		if (e.Window?.Title is { } title)
			AppendMessage(string.Format("PrimaryDocumentChanged: Title={0}", title));
		else
			AppendMessage("PrimaryDocumentChanged: (none)");
	}

	/// <summary>
	/// Occurs when a docking window is activated.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnDockSiteWindowActivated(object sender, DockingWindowEventArgs e) {
		if (IsWindowActivationEventOutputEnabled)
			AppendMessage(string.Format("WindowActivated: Title={0}", e.Window?.Title));
	}

	/// <summary>
	/// Occurs when an auto-hide popup has been closed that displayed a tool window.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnDockSiteWindowAutoHidePopupClosed(object sender, DockingWindowEventArgs e)
		=> AppendMessage(string.Format("AutoHidePopupClosed: Title={0}", e.Window?.Title));

	/// <summary>
	/// Occurs when an auto-hide popup has been opened that displays a tool window.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnDockSiteWindowAutoHidePopupOpened(object sender, DockingWindowEventArgs e)
		=> AppendMessage(string.Format("AutoHidePopupOpened: Title={0}", e.Window?.Title));

	/// <summary>
	/// Occurs when a docking window is deactivated.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnDockSiteWindowDeactivated(object sender, DockingWindowEventArgs e) {
		if (IsWindowActivationEventOutputEnabled)
			AppendMessage(string.Format("WindowDeactivated: Title={0}", e.Window?.Title));
	}

	/// <summary>
	/// Occurs when a docking window is registered.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnDockSiteWindowRegistered(object sender, DockingWindowEventArgs e) {
		if (IsWindowRegistrationEventOutputEnabled)
			AppendMessage(string.Format("WindowRegistered: Title={0}", e.Window?.Title));
	}

	/// <summary>
	/// Occurs before one or more docking windows are auto-hidden, allowing for side customization.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnDockSiteWindowsAutoHiding(object sender, DockingWindowsAutoHidingEventArgs e) {
		if (forceAutoHideToBottomMenuItem.IsChecked)
			e.Side = Side.Bottom;

		var count = e.Windows.Count();
		if (count == 1)
			AppendMessage(string.Format("WindowsAutoHiding: Title={0}, Side={1}", e.Windows.First().Title, e.Side));
		else
			AppendMessage(string.Format("WindowsAutoHiding: Count={0}, Side={1}", count, e.Side));
	}

	/// <summary>
	/// Occurs after one or more docking windows have been closed.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnDockSiteWindowsClosed(object sender, DockingWindowsEventArgs e) {
		var count = e.Windows.Count();
		if (count == 1)
			AppendMessage(string.Format("WindowsClosed: Title={0}", e.Windows.First().Title));
		else
			AppendMessage(string.Format("WindowsClosed: Count={0}", count));
	}

	/// <summary>
	/// Occurs before one or more docking windows are closed, allowing for cancellation of the close.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnDockSiteWindowsClosing(object sender, DockingWindowsEventArgs e) {
		var count = e.Windows.Count();
		if (count == 1)
			AppendMessage(string.Format("WindowsClosing: Title={0}", e.Windows.First().Title));
		else
			AppendMessage(string.Format("WindowsClosing: Count={0}", count));
	}

	/// <summary>
	/// Occurs after one or more docking windows are dragged by the end user.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnDockSiteWindowsDragged(object sender, DockingWindowsEventArgs e) {
		var count = e.Windows.Count();
		if (count == 1)
			AppendMessage(string.Format("WindowsDragged: Title={0}", e.Windows.First().Title));
		else
			AppendMessage(string.Format("WindowsDragged: Count={0}", count));
	}

	/// <summary>
	/// Occurs before one or more docking windows are dragged by the end user.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnDockSiteWindowsDragging(object sender, DockingWindowsEventArgs e) {
		var count = e.Windows.Count();
		if (count == 1)
			AppendMessage(string.Format("WindowsDragging: Title={0}", e.Windows.First().Title));
		else
			AppendMessage(string.Format("WindowsDragging: Count={0}", count));
	}

	/// <summary>
	/// Occurs when one or more docking windows are dragged over a new dock target, allowing for certain dock guides to be hidden.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
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
	/// <param name="e">The event data.</param>
	private void OnDockSiteWindowsOpened(object sender, DockingWindowsEventArgs e) {
		var count = e.Windows.Count();
		if (count == 1)
			AppendMessage(string.Format("WindowsOpened: Title={0}", e.Windows.First().Title));
		else
			AppendMessage(string.Format("WindowsOpened: Count={0}", count));
	}

	/// <summary>
	/// Occurs before one or more docking windows are opened.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnDockSiteWindowsOpening(object sender, DockingWindowsEventArgs e) {
		var count = e.Windows.Count();
		if (count == 1)
			AppendMessage(string.Format("WindowsOpening: Title={0}", e.Windows.First().Title));
		else
			AppendMessage(string.Format("WindowsOpening: Count={0}", count));
	}

	/// <summary>
	/// Occurs after one or more docking windows' states have changed.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnDockSiteWindowsStateChanged(object sender, DockingWindowsEventArgs e) {
		var count = e.Windows.Count();
		if (count == 1)
			AppendMessage(string.Format("WindowsStateChanged: Title={0}, State={1}", e.Windows.First().Title, e.Windows.First().State));
		else
			AppendMessage(string.Format("WindowsStateChanged: Count={0}, State={1}", count, e.Windows.First().State));
	}

	/// <summary>
	/// Occurs when a docking window is unregistered.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnDockSiteWindowUnregistered(object sender, DockingWindowEventArgs e) {
		if (IsWindowRegistrationEventOutputEnabled)
			AppendMessage(string.Format("WindowUnregistered: Title={0}", e.Window?.Title));
	}

	/// <summary>
	/// Occurs when the mouse is double-clicked.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnFindResultsTextBoxDoubleClick(object sender, MouseButtonEventArgs e) {
		// Quit if there is no editor or result set stored yet
		if (findResultsToolWindow.DataContext is not SyntaxEditor editor)
			return;
		if (findResultsTextBox.DataContext is not ISearchResultSet resultSet)
			return;

		var charIndex = findResultsTextBox.GetCharacterIndexFromPoint(e.GetPosition(findResultsTextBox), snapToText: true);
		var lineIndex = findResultsTextBox.GetLineIndexFromCharacterIndex(charIndex);

		var resultIndex = lineIndex - 1;  // Account for first line in results displaying search info
		if ((0 <= resultIndex) && (resultIndex < resultSet.Results.Count)) {
			// A valid result was clicked
			var result = resultSet.Results[resultIndex];
			TextSnapshotRange? selectionSnapshotRange;
			if (result.ReplaceSnapshotRange.HasValue) {
				// Replace result
				selectionSnapshotRange = result.ReplaceSnapshotRange.Value.TranslateTo(editor.ActiveView.CurrentSnapshot, TextRangeTrackingModes.Default);
			}
			else {
				// Find result
				selectionSnapshotRange = result.FindSnapshotRange.TranslateTo(editor.ActiveView.CurrentSnapshot, TextRangeTrackingModes.Default);
			}

			// Select the range
			if (selectionSnapshotRange.HasValue)
				editor.ActiveView.Selection.SelectRange(selectionSnapshotRange.Value.TextRange);

			// Focus the editor
			editor.Focus();
		}
	}

	/// <summary>
	/// Occurs when the menu item is clicked.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnNewDocumentMenuItemClick(object sender, RoutedEventArgs e) {
		var menuItem = (MenuItem)sender;
		var extension = (menuItem.Tag as string) ?? ".txt";
		CreateSyntaxEditorDocument(extension, fileName: null, text: null);
	}

	/// <summary>
	/// Occurs when the menu item is clicked.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnOpenDocumentMenuItemClick(object sender, RoutedEventArgs e) {
		// Show a file open dialog
		var dialog = new OpenFileDialog {
			CheckFileExists = true,
			Multiselect = false,
			Filter = "Code files (*.txt;*.cs;*.js;*.py;*.vb;*.xml)|*.txt;*.cs;*.js;*.py;*.vb;*.xml|All files (*.*)|*.*"
		};
		if (dialog.ShowDialog() == true)
			CreateSyntaxEditorDocument(Path.GetExtension(dialog.FileName), dialog.FileName, text: null);
	}

	/// <summary>
	/// Occurs when the <see cref="SyntaxEditor.IsOverwriteModeActiveChanged"/> event is raised.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnSyntaxEditorIsOverwriteModeActiveChanged(object? sender, RoutedEventArgs e)
		=> UpdateStatusBar();

	/// <summary>
	/// Occurs when the editor view's selection is changed.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnSyntaxEditorViewSelectionChanged(object? sender, EditorViewSelectionEventArgs e) {
		// Quit if this event is not for the active view
		if (!e.View.IsActive)
			return;

		UpdateStatusBar();
	}

	/// <summary>
	/// Updates the primary document bindings.
	/// </summary>
	private void UpdatePrimaryDocumentBindings() {
		propGrid.IsReadOnly = false;

		if (searchView.SyntaxEditor is not null) {
			searchView.SyntaxEditor.IsOverwriteModeActiveChanged -= OnSyntaxEditorIsOverwriteModeActiveChanged;
			searchView.SyntaxEditor.ViewSelectionChanged -= OnSyntaxEditorViewSelectionChanged;
		}

		var primaryDocument = dockSite.PrimaryDocument as EditorDocumentWindow;

		if (primaryDocument is not null) {
			propGrid.DataObject = primaryDocument.DataContext;
			searchView.SyntaxEditor = primaryDocument.Editor;
			propGrid.IsReadOnly = true;
			propGrid.Visibility = Visibility.Visible;
		}
		else {
			propGrid.DataObject = null;
			searchView.SyntaxEditor = null;
			propGrid.Visibility = Visibility.Collapsed;
		}

		if (searchView.SyntaxEditor is not null) {
			searchView.SyntaxEditor.IsOverwriteModeActiveChanged += OnSyntaxEditorIsOverwriteModeActiveChanged;
			searchView.SyntaxEditor.ViewSelectionChanged += OnSyntaxEditorViewSelectionChanged;
		}

		UpdateStatusBar();

		NotifyDocumentOutlineUpdated(primaryDocument);
	}

	/// <summary>
	/// Updates the statusbar.
	/// </summary>
	private void UpdateStatusBar() {
		if (searchView.SyntaxEditor is { } editor) {
			linePanel.Text = string.Format("Ln {0}", editor.ActiveView.Selection.CaretPosition.DisplayLine);
			columnPanel.Text = string.Format("Col {0}", editor.ActiveView.Selection.CaretDisplayCharacterColumn);
			characterPanel.Text = string.Format("Ch {0}", editor.ActiveView.Selection.CaretPosition.DisplayCharacter);
			overwriteModePanel.Content = (editor.IsOverwriteModeActive ? "OVR" : "INS");
		}
		else {
			linePanel.Text = null;
			columnPanel.Text = null;
			characterPanel.Text = null;
			overwriteModePanel.Content = "INS";
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Indicates whether docking window activation event output is enabled.
	/// </summary>
	public bool IsWindowActivationEventOutputEnabled {
		get => (bool)GetValue(IsWindowActivationEventOutputEnabledProperty);
		set => SetValue(IsWindowActivationEventOutputEnabledProperty, value);
	}

	/// <summary>
	/// Indicates whether docking window registration event output is enabled.
	/// </summary>
	public bool IsWindowRegistrationEventOutputEnabled {
		get => (bool)GetValue(IsWindowRegistrationEventOutputEnabledProperty);
		set => SetValue(IsWindowRegistrationEventOutputEnabledProperty, value);
	}

	/// <summary>
	/// Occurs when the document outline is updated.
	/// </summary>
	/// <param name="window">The window that was updated.</param>
	public void NotifyDocumentOutlineUpdated(EditorDocumentWindow? window) {
		if (window is not null) {
			documentOutlineToolWindow.Title = "Document Outline - " + window.Title;

			if (window.Editor.Document.ParseData is LLParseData { Ast: not null }  parseData) {
				Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, () => {
					var astString = parseData.Ast.ToTreeString(indentLevel: 0);
					documentOutlineTextBox.Text = (astString.Length <= 10000)
						? astString
						: "(AST is too large to display)";
				});
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
		findResultsToolWindow.Title = string.Format("Find Results - {0} match{1}", resultSet.Results.Count, (resultSet.Results.Count == 1 ? string.Empty : "es"));
		findResultsToolWindow.DataContext = window.Editor;
		findResultsTextBox.Text = resultSet.ToString();
		findResultsTextBox.DataContext = resultSet;

		if (findResultsToolWindow.IsOpen)
			findResultsToolWindow.Activate(focus: false);

		if (resultSet.Results.Count > 0)
			window.Activate();
	}

	/// <inheritdoc/>
	public override void NotifyUnloaded() {
		// Ensure the DataObject property is cleared when the primary UI closes... don't use PropertyGrid.CanClearDataObjectOnUnload
		//   since the property grid is in a tool window
		propGrid.DataObject = null;
	}

}
