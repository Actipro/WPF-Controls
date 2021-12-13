using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Microsoft.Win32;
using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Ribbon;
using ActiproSoftware.Windows.Controls.Ribbon.Input;
using ActiproSoftware.Windows.Controls.Ribbon.UI;
using RibbonControls = ActiproSoftware.Windows.Controls.Ribbon.Controls;
using ActiproSoftware.Windows.DocumentManagement;
using ActiproSoftware.Windows.Media;
using ActiproSoftware.ProductSamples.RibbonSamples.Common;
using ActiproSoftware.Windows.Themes;

#if WPF
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;
#endif

namespace ActiproSoftware.ProductSamples.RibbonSamples.Demo.DocumentEditor {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainWindow {

		private DocumentData	currentDocumentData;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainWindow</c> class.
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
			this.CurrentDocumentData = DocumentData.CreateNewDocument();

			// Add command bindings
			this.CommandBindings.Add(new CommandBinding(ApplicationCommands.ApplicationExit, applicationExitCommand_Execute));
            this.CommandBindings.Add(new CommandBinding(System.Windows.Input.ApplicationCommands.Help, applicationHelpCommand_Execute));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.ApplicationOptions, applicationOptionsCommand_Execute));
            this.CommandBindings.Add(new CommandBinding(System.Windows.Input.ApplicationCommands.Close, fileCloseCommand_Execute, fileRequired_CanExecute));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Comments, commentsCommand_Execute));
			this.CommandBindings.Add(new CommandBinding(ApplicationCommands.CoverPage, coverPageCommand_Execute));
			this.CommandBindings.Add(new CommandBinding(RibbonCommands.CustomizeQuickAccessToolBar, customizeQuickAccessToolBarCommand_Execute));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Disabled, null, disabledCommand_CanExecute));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.FileNewRtfDocument, fileNewRtfDocumentCommand_Execute));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.FileNewTextDocument, fileNewTextDocumentCommand_Execute));
            this.CommandBindings.Add(new CommandBinding(System.Windows.Input.ApplicationCommands.New, fileNewCommand_Execute));
            this.CommandBindings.Add(new CommandBinding(System.Windows.Input.ApplicationCommands.Open, fileOpenCommand_Execute));
            this.CommandBindings.Add(new CommandBinding(System.Windows.Input.ApplicationCommands.Save, fileSaveCommand_Execute, fileRequired_CanExecute));
            this.CommandBindings.Add(new CommandBinding(System.Windows.Input.ApplicationCommands.Find, findCommand_Execute));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.ShowDialog, showDialogCommand_Execute));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.ToggleContextualTabGroup, toggleContextualTabGroupCommand_Execute, toggleContextualTabGroupCommand_CanExecute));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.ToggleFlowDirection, toggleFlowDirectionCommand_Execute));
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// COMMAND HANDLERS
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private void applicationExitCommand_Execute(object sender, ExecutedRoutedEventArgs e) {
			if (BrowserInteropHelper.IsBrowserHosted) {
				Page currentPage = (Page)VisualTreeHelperExtended.GetAncestor(this, typeof(Page));
				if ((currentPage != null) && (currentPage.NavigationService.CanGoBack))
					currentPage.NavigationService.Navigate(Application.Current.StartupUri);
				else
					MessageBox.Show("Exit the application here.");
			}
			else {
				Window window = (Window)ActiproSoftware.Windows.Media.VisualTreeHelperExtended.GetAncestor(ribbon, typeof(Window));
				if (window != null)
					window.Close();
			}
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private void applicationHelpCommand_Execute(object sender, ExecutedRoutedEventArgs e) {
			// First look to see if a screentip is displayed, and if so, show the context help for that
			if (ScreenTipService.CurrentScreenTip != null) {
				MessageBox.Show(String.Format("Show the help topic for '{0}' here if appropriate.\r\n\r\nThe owner element is: {1}\r\nThe pre-defined help URI is: {2}", 
					ScreenTipService.CurrentScreenTip.Header, ScreenTipService.CurrentScreenTip.OwnerElement, 
					(ScreenTipService.CurrentScreenTip.HelpUri != null ? ScreenTipService.CurrentScreenTip.HelpUri.AbsoluteUri : "<null>")));
				return;
			}

			// Show default help topic
			MessageBox.Show("Show the default help topic here.");
		}

		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private void applicationOptionsCommand_Execute(object sender, ExecutedRoutedEventArgs e) {
			MessageBox.Show("Show the application options dialog here.");
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private void showDialogCommand_Execute(object sender, ExecutedRoutedEventArgs e) {
			MessageBox.Show(String.Format("Show the {0} dialog here.", e.Parameter));
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private void commentsCommand_Execute(object sender, ExecutedRoutedEventArgs e) {
			MessageBox.Show("Show the comments pane here.");
		}

		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private void coverPageCommand_Execute(object sender, ExecutedRoutedEventArgs e) {
			MessageBox.Show("Add a cover page here.");
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private void customizeQuickAccessToolBarCommand_Execute(object sender, ExecutedRoutedEventArgs e) {
			if (BrowserInteropHelper.IsBrowserHosted)
				MessageBox.Show("Show an application options dialog with the Customize QAT tab selected here.  The downloadable evaluation includes a complete working implementation of the Customize QAT dialog.  The IsCustomizeQuickAccessToolBarMenuItemVisible option controls whether the menu item you clicked is visible.");
			else
				this.ShowOptionsWindow();
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> needs to determine whether it can execute.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="CanExecuteRoutedEventArgs"/> that contains the event data.</param>
		private void disabledCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
			// Flag as disabled
			e.CanExecute = false;
			e.Handled = true;
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private void fileCloseCommand_Execute(object sender, ExecutedRoutedEventArgs e) {
			// Close the document
			this.CurrentDocumentData = null;
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private void fileNewCommand_Execute(object sender, ExecutedRoutedEventArgs e) {
			// Create a new document
			this.CurrentDocumentData = DocumentData.CreateNewDocument();
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private void fileNewRtfDocumentCommand_Execute(object sender, ExecutedRoutedEventArgs e) {
			// Create a new document
			this.CurrentDocumentData = DocumentData.CreateNewDocument(".rtf");
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private void fileNewTextDocumentCommand_Execute(object sender, ExecutedRoutedEventArgs e) {
			// Create a new document
			this.CurrentDocumentData = DocumentData.CreateNewDocument(".txt");
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private void fileOpenCommand_Execute(object sender, ExecutedRoutedEventArgs e) {
			if (e.Parameter is IDocumentReference) {
				// Process recent document clicks
				MessageBox.Show("Open document '" + ((IDocumentReference)e.Parameter).Name + "' here.", "Open Recent Document", MessageBoxButton.OK, MessageBoxImage.Information);
				return;
			}

			// Open a document
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.DefaultExt = ".rtf";
			dialog.CheckFileExists = true;
			dialog.Filter = "Document Files (*.rtf; *.txt)|*.rtf;*.txt|Rich Text Files (*.rtf)|*.rtf|Text Files (*.txt)|*.txt";
			if (dialog.ShowDialog() == true)
				this.CurrentDocumentData = new DocumentData(dialog.FileName);
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> needs to determine whether it can execute.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="CanExecuteRoutedEventArgs"/> that contains the event data.</param>
		private void fileRequired_CanExecute(object sender, CanExecuteRoutedEventArgs e) {			
			// Only allow the command if there is a document open
			e.CanExecute = (CurrentDocumentData != null);
			e.Handled = true;
		}

		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private void fileSaveCommand_Execute(object sender, ExecutedRoutedEventArgs e) {
			MessageBox.Show("Save file here.");
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private void findCommand_Execute(object sender, ExecutedRoutedEventArgs e) {
			IValueCommandParameter parameter = e.Parameter as IValueCommandParameter;
			if (parameter != null) {
				parameter.Handled = true;
				MessageBox.Show("Implement text search for '" + parameter.Value + "' here.");
			}
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> needs to determine whether it can execute.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="CanExecuteRoutedEventArgs"/> that contains the event data.</param>
		private void toggleContextualTabGroupCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
			e.CanExecute = true;
			e.Handled = true;

			ICheckableCommandParameter parameter = e.Parameter as ICheckableCommandParameter;
			if (parameter != null) {
				RibbonControls.ContextualTabGroup contextualTabGroup = ribbon.ContextualTabGroups[Convert.ToString(parameter.Tag)];
				parameter.Handled = true;
				parameter.IsChecked = contextualTabGroup.IsVisible;
			}
		}

		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private void toggleContextualTabGroupCommand_Execute(object sender, ExecutedRoutedEventArgs e) {
			RibbonControls.ContextualTabGroup contextualTabGroup = null;

			ICheckableCommandParameter parameter = e.Parameter as ICheckableCommandParameter;
			if (parameter != null)
				contextualTabGroup = ribbon.ContextualTabGroups[Convert.ToString(parameter.Tag)];
			else if (e.Parameter is string)
				contextualTabGroup = ribbon.ContextualTabGroups[Convert.ToString(e.Parameter)];

			if (contextualTabGroup != null) {
				contextualTabGroup.IsActive = !contextualTabGroup.IsActive;

				// If making active, select the first tab in it
				// if ((contextualTabGroup.IsActive) && (contextualTabGroup.Items.Count > 0))
				//	ribbon.SelectedTab = (RibbonControls.Tab)contextualTabGroup.Items[0];
			}
		}
		
		/// <summary>
		/// Occurs when the <see cref="RoutedCommand"/> is executed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">An <see cref="ExecutedRoutedEventArgs"/> that contains the event data.</param>
		private void toggleFlowDirectionCommand_Execute(object sender, ExecutedRoutedEventArgs e) {
			FrameworkElement container = this.GetRootContainer();
			container.FlowDirection = (container.FlowDirection == FlowDirection.LeftToRight ? FlowDirection.RightToLeft : FlowDirection.LeftToRight);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Returns the root container of this sample.
		/// </summary>
		/// <returns>The root container of this sample.</returns>
		internal FrameworkElement GetRootContainer() {
			FrameworkElement container = null;
			if (!BrowserInteropHelper.IsBrowserHosted) {
				container = (Window)ActiproSoftware.Windows.Media.VisualTreeHelperExtended.GetAncestor(this, typeof(Window));
			}
			else
				container = (Page)ActiproSoftware.Windows.Media.VisualTreeHelperExtended.GetAncestor(this, typeof(Page));
			return container ?? this;
		}
		
		/// <summary>
		/// Loads the text for the specified document file.
		/// </summary>
		/// <param name="path">The full path to the file.</param>
		/// <returns>The text that was loaded.</returns>
		private string LoadDocumentText(string path) {
			if (File.Exists(path)) {
				StreamReader reader = new StreamReader(path);
				string text = reader.ReadToEnd();
				reader.Close();
				return text;
			}
			return null;
		}
		
		/// <summary>
		/// Occurs when an underline gallery item needs to be drawn.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="CustomDrawElementCustomDrawEventArgs"/> that contains the event data.</param>
		private void OnCustomDrawUnderlineGalleryItems(object sender, CustomDrawElementCustomDrawEventArgs e) {
			// Draw the underline style onto the specified element
			UnderlineStyleRenderer.Render(e, (UnderlineStyle)e.Element.DataContext);
		}
		
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
		/// Shows the Options window.
		/// </summary>
		private void ShowOptionsWindow() {
			new OptionsWindow(ribbon).ShowDialog();
		}

		/// <summary>
		/// Updates the <see cref="RibbonWindow.DocumentName"/> property.
		/// </summary>
		private void UpdateDocumentName() {
			RibbonWindow window = (RibbonWindow)ActiproSoftware.Windows.Media.VisualTreeHelperExtended.GetAncestor(this, typeof(RibbonWindow));
			if (window != null) {
				if (currentDocumentData != null)
					window.DocumentName = currentDocumentData.FilenameWithoutExtension;
				else
					window.DocumentName = null;
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the <see cref="DocumentData"/> that contains information about the currently loaded document.
		/// </summary>
		/// <value>The <see cref="DocumentData"/> that contains information about the currently loaded document.</value>
		public DocumentData CurrentDocumentData {
			get {
				return currentDocumentData;
			}
			set {
				// If the file is changing...
				if (currentDocumentData != value) {
					// Set the new value
					currentDocumentData = value;

					if (currentDocumentData != null) {
						// Load a new editor based on the file type
						switch (currentDocumentData.FilenameExtension) {
							case ".rtf": {
								ActiproSoftware.Windows.Controls.DropShadowChrome dropShadow = new ActiproSoftware.Windows.Controls.DropShadowChrome();
								dropShadow.BorderThickness = new Thickness(10);
								dropShadow.Color = Color.FromArgb(113, 120, 120, 120);
								dropShadow.CornerRadius = new CornerRadius(10);
								dropShadow.Margin = new Thickness(35, 20, 35, 20);
								dropShadow.XOffset = 0;
								dropShadow.YOffset = 0;
								dropShadow.ZOffset = 10;
								RichTextBoxExtended editor = new RichTextBoxExtended();
								editor.Padding = new Thickness(20);
								dropShadow.Child = editor;
								dropShadow.Width = 690;
								contentContainer.Content = dropShadow;

								string text = this.LoadDocumentText(currentDocumentData.Path);
								if (text != null)
									editor.LoadDocument(text);
								else
									editor.Document = Application.LoadComponent(new Uri("/ProductSamples/RibbonSamples/Demo/DocumentEditor/FeaturesDocument.xaml", UriKind.Relative)) as FlowDocument;

								this.FocusDocument();
								break;
							}
							default: {
								TextBox editor = new TextBox();
								editor.TextWrapping = TextWrapping.NoWrap;
								editor.AcceptsReturn = true;
								editor.AcceptsTab = true;
								editor.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
								editor.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
								contentContainer.Content = editor;

								string text = this.LoadDocumentText(currentDocumentData.Path);
								if (text != null)
									editor.Text = text;
								else
									editor.Text = "Welcome to the Actipro Ribbon control demo for WPF.";

								this.FocusDocument();
								break;
							}
						}
					}
					else {
						// Ensure there is no editor loaded
						ActiproSoftware.Windows.Controls.ReflectionContentControl reflection = new ActiproSoftware.Windows.Controls.ReflectionContentControl();
						ActiproSoftware.Windows.Controls.ActiproLogo logo = new ActiproSoftware.Windows.Controls.ActiproLogo();
						logo.Focusable = true;
						logo.Opacity = 0.85;
						reflection.Content = logo;
						contentContainer.Content = reflection;
						this.FocusDocument();
					}
				}

				// Update the document name
				if (!BrowserInteropHelper.IsBrowserHosted)
					this.UpdateDocumentName();
			}
		}

		/// <summary>
		/// Focuses the document.
		/// </summary>
		public void FocusDocument() {
			if (contentContainer.Content is UIElement) {
				// Set focus after everything is rendered
				this.Dispatcher.BeginInvoke(DispatcherPriority.Render, (DispatcherOperationCallback)delegate { 
					((UIElement)contentContainer.Content).UpdateLayout();
					((UIElement)contentContainer.Content).MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
					return null; 
				}, null);
			}
		}
		
		/// <summary>
		/// Invoked whenever application code or internal processes call <see cref="FrameworkElement.ApplyTemplate"/>.
		/// </summary>
		public override void OnApplyTemplate() {
			// Call the base method
			base.OnApplyTemplate();

			// Update the document name (needed because when the control is first created the parent hasn't been assigned yet)
			if (!BrowserInteropHelper.IsBrowserHosted)
				this.UpdateDocumentName();
		}
		
	}
}