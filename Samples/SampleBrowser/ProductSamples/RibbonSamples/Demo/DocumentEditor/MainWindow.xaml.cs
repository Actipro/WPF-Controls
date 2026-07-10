using System.Windows.Documents;
using System.Windows.Interop;
using System.Windows.Threading;
using Microsoft.Win32;
using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Ribbon;
using ActiproSoftware.Windows.Controls.Ribbon.Input;
using ActiproSoftware.Windows.Controls.Ribbon.UI;
using RibbonControls = ActiproSoftware.Windows.Controls.Ribbon.Controls;
using ActiproSoftware.Windows.DocumentManagement;
using ActiproSoftware.ProductSamples.RibbonSamples.Common;
using ActiproSoftware.Windows.Extensions;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.RibbonSamples.Demo.DocumentEditor;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainWindow {

	private DocumentData? _currentDocumentData;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainWindow() {
		// Register UI providers before doing anything else, including InitializeComponent
		ApplicationCommands.RegisterUIProvidersForNonRibbonCommands();

		InitializeComponent();

		// Populate some sample recent documents
		DocumentReferenceGenerator.BindRecentDocumentManager(recentDocManager);

		// Store this container reference in the Ribbon's Tag property
		ribbon.Tag = this;

		// Create a new document
		DocumentData.NewDocumentCount = 0;
		CurrentDocumentData = DocumentData.CreateNewDocument();

		// Add command bindings
		CommandBindings.Add(new CommandBinding(ApplicationCommands.ApplicationExit, OnApplicationExitCommandExecute));
		CommandBindings.Add(new CommandBinding(System.Windows.Input.ApplicationCommands.Help, OnApplicationHelpCommandExecute));
		CommandBindings.Add(new CommandBinding(ApplicationCommands.ApplicationOptions, OnApplicationOptionsCommandExecute));
		CommandBindings.Add(new CommandBinding(System.Windows.Input.ApplicationCommands.Close, OnFileCloseCommandExecute, OnFileRequiredCanExecute));
		CommandBindings.Add(new CommandBinding(ApplicationCommands.Comments, OnCommentsCommandExecute));
		CommandBindings.Add(new CommandBinding(ApplicationCommands.CoverPage, OnCoverPageCommandExecute));
		CommandBindings.Add(new CommandBinding(RibbonCommands.CustomizeQuickAccessToolBar, OnCustomizeQuickAccessToolBarCommandExecute));
		CommandBindings.Add(new CommandBinding(ApplicationCommands.Disabled, null, OnDisabledCommandCanExecute));
		CommandBindings.Add(new CommandBinding(ApplicationCommands.FileNewRtfDocument, OnFileNewRtfDocumentCommandExecute));
		CommandBindings.Add(new CommandBinding(ApplicationCommands.FileNewTextDocument, OnFileNewTextDocumentCommandExecute));
		CommandBindings.Add(new CommandBinding(System.Windows.Input.ApplicationCommands.New, OnFileNewCommandExecute));
		CommandBindings.Add(new CommandBinding(System.Windows.Input.ApplicationCommands.Open, OnFileOpenCommandExecute));
		CommandBindings.Add(new CommandBinding(System.Windows.Input.ApplicationCommands.Save, OnFileSaveCommandExecute, OnFileRequiredCanExecute));
		CommandBindings.Add(new CommandBinding(System.Windows.Input.ApplicationCommands.Find, OnFindCommandExecute));
		CommandBindings.Add(new CommandBinding(ApplicationCommands.ShowDialog, OnShowDialogCommandExecute));
		CommandBindings.Add(new CommandBinding(ApplicationCommands.ToggleContextualTabGroup, OnToggleContextualTabGroupCommandExecute, OnToggleContextualTabGroupCommandCanExecute));
		CommandBindings.Add(new CommandBinding(ApplicationCommands.ToggleFlowDirection, OnToggleFlowDirectionCommand_Execute));
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Returns the root container of this sample.
	/// </summary>
	internal FrameworkElement GetRootContainer()
		=> this.FindAncestorOfType<Window>() ?? this;

	/// <summary>
	/// Loads the text for the specified document file.
	/// </summary>
	/// <param name="path">The full path to the file.</param>
	/// <returns>The text that was loaded.</returns>
	private static string? LoadDocumentText(string path) {
		if (File.Exists(path)) {
			var reader = new StreamReader(path);
			var text = reader.ReadToEnd();
			reader.Close();
			return text;
		}
		return null;
	}

	private void OnApplicationExitCommandExecute(object sender, ExecutedRoutedEventArgs e) {
		var window = ribbon.FindAncestorOfType<Window>();
		window?.Close();
	}

	private void OnApplicationHelpCommandExecute(object sender, ExecutedRoutedEventArgs e) {
		// First look to see if a screentip is displayed, and if so, show the context help for that
		if (ScreenTipService.CurrentScreenTip is { } screenTip) {
			MessageBox.Show(string.Format("Show the help topic for '{0}' here if appropriate.\r\n\r\nThe owner element is: {1}\r\nThe pre-defined help URI is: {2}",
				screenTip.Header, screenTip.OwnerElement,
				screenTip?.HelpUri.AbsoluteUri ?? "<null>"));
			return;
		}

		// Show default help topic
		MessageBox.Show("Show the default help topic here.");
	}

	private void OnApplicationOptionsCommandExecute(object sender, ExecutedRoutedEventArgs e)
		=> MessageBox.Show("Show the application options dialog here.");

	private void OnShowDialogCommandExecute(object sender, ExecutedRoutedEventArgs e)
		=> MessageBox.Show(string.Format("Show the {0} dialog here.", e.Parameter));

	private void OnCommentsCommandExecute(object sender, ExecutedRoutedEventArgs e)
		=> MessageBox.Show("Show the comments pane here.");

	private void OnCoverPageCommandExecute(object sender, ExecutedRoutedEventArgs e)
		=> MessageBox.Show("Add a cover page here.");

	/// <summary>
	/// Occurs when an underline gallery item needs to be drawn.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnCustomDrawUnderlineGalleryItems(object sender, CustomDrawElementCustomDrawEventArgs e) {
		// Draw the underline style onto the specified element
		UnderlineStyleRenderer.Render(e, (UnderlineStyle)e.Element.DataContext);
	}

	private void OnCustomizeQuickAccessToolBarCommandExecute(object sender, ExecutedRoutedEventArgs e)
		=> ShowOptionsWindow();

	private void OnDisabledCommandCanExecute(object sender, CanExecuteRoutedEventArgs e) {
		// Flag as disabled
		e.CanExecute = false;
		e.Handled = true;
	}

	private void OnFileCloseCommandExecute(object sender, ExecutedRoutedEventArgs e) {
		// Close the document
		CurrentDocumentData = null;
	}

	private void OnFileNewCommandExecute(object sender, ExecutedRoutedEventArgs e) {
		// Create a new document
		CurrentDocumentData = DocumentData.CreateNewDocument();
	}

	private void OnFileNewRtfDocumentCommandExecute(object sender, ExecutedRoutedEventArgs e) {
		// Create a new document
		CurrentDocumentData = DocumentData.CreateNewDocument(".rtf");
	}

	private void OnFileNewTextDocumentCommandExecute(object sender, ExecutedRoutedEventArgs e) {
		// Create a new document
		CurrentDocumentData = DocumentData.CreateNewDocument(".txt");
	}

	private void OnFileOpenCommandExecute(object sender, ExecutedRoutedEventArgs e) {
		if (e.Parameter is IDocumentReference) {
			// Process recent document clicks
			MessageBox.Show("Open document '" + ((IDocumentReference)e.Parameter).Name + "' here.", "Open Recent Document", MessageBoxButton.OK, MessageBoxImage.Information);
			return;
		}

		// Open a document
		var dialog = new OpenFileDialog {
			DefaultExt = ".rtf",
			CheckFileExists = true,
			Filter = "Document Files (*.rtf; *.txt)|*.rtf;*.txt|Rich Text Files (*.rtf)|*.rtf|Text Files (*.txt)|*.txt"
		};
		if (dialog.ShowDialog() == true)
			CurrentDocumentData = new DocumentData(dialog.FileName);
	}

	private void OnFileRequiredCanExecute(object sender, CanExecuteRoutedEventArgs e) {
		// Only allow the command if there is a document open
		e.CanExecute = (CurrentDocumentData is not null);
		e.Handled = true;
	}

	private void OnFileSaveCommandExecute(object sender, ExecutedRoutedEventArgs e)
		=> MessageBox.Show("Save file here.");

	private void OnFindCommandExecute(object sender, ExecutedRoutedEventArgs e) {
		var parameter = e.Parameter as IValueCommandParameter;
		if (parameter is not null) {
			parameter.Handled = true;
			MessageBox.Show("Implement text search for '" + parameter.Value + "' here.");
		}
	}

	/// <summary>
	/// Occurs when the application menu opens or closes.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnIsApplicationMenuOpenChanged(object sender, BooleanPropertyChangedRoutedEventArgs e) {
		// If opening, ensure the that the New is always selected
		if (ribbon.IsApplicationMenuOpen)
			appMenu.SelectedItem = newBackstageTab;
	}

	private void OnToggleContextualTabGroupCommandCanExecute(object sender, CanExecuteRoutedEventArgs e) {
		e.CanExecute = true;
		e.Handled = true;

		var parameter = e.Parameter as ICheckableCommandParameter;
		if (parameter is not null) {
			RibbonControls.ContextualTabGroup contextualTabGroup = ribbon.ContextualTabGroups[Convert.ToString(parameter.Tag)];
			parameter.Handled = true;
			parameter.IsChecked = contextualTabGroup.IsVisible;
		}
	}

	private void OnToggleContextualTabGroupCommandExecute(object sender, ExecutedRoutedEventArgs e) {
		RibbonControls.ContextualTabGroup? contextualTabGroup = null;

		var parameter = e.Parameter as ICheckableCommandParameter;
		if (parameter is not null)
			contextualTabGroup = ribbon.ContextualTabGroups[Convert.ToString(parameter.Tag)];
		else if (e.Parameter is string)
			contextualTabGroup = ribbon.ContextualTabGroups[Convert.ToString(e.Parameter)];

		if (contextualTabGroup is not null)
			contextualTabGroup.IsActive = !contextualTabGroup.IsActive;
	}

	private void OnToggleFlowDirectionCommand_Execute(object sender, ExecutedRoutedEventArgs e) {
		var container = GetRootContainer();
		container.FlowDirection = (container.FlowDirection == FlowDirection.LeftToRight)
			? FlowDirection.RightToLeft
			: FlowDirection.LeftToRight;
	}

	/// <summary>
	/// Shows the Options window.
	/// </summary>
	private void ShowOptionsWindow()
		=> new OptionsWindow(ribbon).ShowDialog();

	/// <summary>
	/// Updates the <see cref="RibbonWindow.DocumentName"/> property.
	/// </summary>
	private void UpdateDocumentName() {
		var window = this.FindAncestorOfType<RibbonWindow>();
		if (window is not null)
			window.DocumentName = _currentDocumentData?.FilenameWithoutExtension;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="DocumentData"/> that contains information about the currently loaded document.
	/// </summary>
	public DocumentData? CurrentDocumentData {
		get => _currentDocumentData;
		set {
			// If the file is changing...
			if (_currentDocumentData != value) {
				// Set the new value
				_currentDocumentData = value;

				if (_currentDocumentData is not null) {
					// Load a new editor based on the file type
					switch (_currentDocumentData.FilenameExtension) {
						case ".rtf": {
							var dropShadow = new DropShadowChrome {
								BorderThickness = new Thickness(10),
								Color = Color.FromArgb(113, 120, 120, 120),
								CornerRadius = new CornerRadius(10),
								Margin = new Thickness(35, 20, 35, 20),
								XOffset = 0,
								YOffset = 0,
								ZOffset = 10
							};
							var editor = new RichTextBoxExtended {
								Padding = new Thickness(20)
							};
							dropShadow.Child = editor;
							dropShadow.Width = 690;
							contentContainer.Content = dropShadow;

							var text = LoadDocumentText(_currentDocumentData.Path);
							if (text is not null)
								editor.LoadDocument(text);
							else
								editor.Document = Application.LoadComponent(new Uri("/ProductSamples/RibbonSamples/Demo/DocumentEditor/FeaturesDocument.xaml", UriKind.Relative)) as FlowDocument;

							FocusDocument();
							break;
						}
						default: {
							var editor = new TextBox {
								TextWrapping = TextWrapping.NoWrap,
								AcceptsReturn = true,
								AcceptsTab = true,
								HorizontalScrollBarVisibility = ScrollBarVisibility.Visible,
								VerticalScrollBarVisibility = ScrollBarVisibility.Visible
							};
							contentContainer.Content = editor;

							var text = LoadDocumentText(_currentDocumentData.Path);
							if (text is not null)
								editor.Text = text;
							else
								editor.Text = "Welcome to the Actipro Ribbon control demo for WPF.";

							FocusDocument();
							break;
						}
					}
				}
				else {
					// Ensure there is no editor loaded
					var reflection = new ReflectionContentControl();
					var logo = new ActiproLogo {
						Focusable = true,
						Opacity = 0.85
					};
					reflection.Content = logo;
					contentContainer.Content = reflection;
					FocusDocument();
				}
			}

			// Update the document name
			UpdateDocumentName();
		}
	}

	/// <summary>
	/// Focuses the document.
	/// </summary>
	public void FocusDocument() {
		if (contentContainer.Content is UIElement element) {
			// Set focus after everything is rendered
			Dispatcher.BeginInvoke(DispatcherPriority.Render, () => {
				element.UpdateLayout();
				element.MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
			});
		}
	}

	/// <inheritdoc/>
	public override void OnApplyTemplate() {
		base.OnApplyTemplate();

		// Update the document name (needed because when the control is first created the parent hasn't been assigned yet)
		UpdateDocumentName();
	}

}
