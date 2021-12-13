using ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Languages.CSharp.Implementation;
using ActiproSoftware.Text.Languages.DotNet.Reflection;
using ActiproSoftware.Text.Languages.Xml.Implementation;
using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Input;
using ActiproSoftware.Windows.Media.Animation;
using ActiproSoftware.Windows.Themes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

#if WPF
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;
#endif

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Provides the application view-model.
	/// </summary>
	public class ApplicationViewModel : ObservableObjectBase {

		private string					codeViewerSelectedPath;
		private CodeViewerWindow		codeViewerWindow;
		private bool					isBackstageOpen;
		private bool					isLoadingExternalSample;
		private bool					isUsingAutomaticThemes;
		private NavigationService		navigationService			= new NavigationService();
		private ApplicationOverlayMode	overlayMode					= ApplicationOverlayMode.HomeBackstage;
		private ProductData				productData;
		private string					productSamplesPath;
		private IList<ProductItemInfo>	searchResults;
		private string					searchText					= string.Empty;
		private string					statusMessage;
		private ISyntaxLanguage			syntaxLanguageCSharp;
		private ISyntaxLanguage			syntaxLanguageXaml;
		private FrameworkElement		viewElement;
		private bool					viewHasCustomStatusBar;
		private bool					viewHasInterop;
		private bool					viewHasNavigationButtons;
		private ImageSource				viewImageSource;
		private ProductItemInfo			viewItemInfo;
		private string					viewSubTitle;
		private string					viewTitle;
		private TransitionDirection		viewTransitionDirection		= TransitionDirection.Forward;

		private DelegateCommand<object> navigateViewToHomeCommand;
		private DelegateCommand<object> navigateViewToItemInfoCommand;
		private DelegateCommand<object> navigateViewToNextItemInfoCommand;
		private DelegateCommand<object> navigateViewToPreviousItemInfoCommand;
		private DelegateCommand<object> openDocumentationCommand;
		private DelegateCommand<object> openExternalSampleCommand;
		private DelegateCommand<object> openSampleCodeCommand;
		private DelegateCommand<object> openSampleFolderCommand;
		private DelegateCommand<object> openSampleProjectCommand;
		private DelegateCommand<object> openUrlCommand;
		private DelegateCommand<object> setApplicationThemeCommand;
		private DelegateCommand<object> toggleAutomaticThemesCommand;
		private DelegateCommand<object> toggleIsBackstageOpenCommand;
		private DelegateCommand<object> toggleNativeThemesCommand;

		private const int MaximumSearchResults = 50;

		private const string DefaultSampleUri = null;
		// private const string DefaultSampleUri = "https://ActiproSoftware/SampleBrowser/Documents/ProductOverviews/Themes";
		// private const string DefaultSampleUri = "https://ActiproSoftware/ProductSamples/ViewsSamples/QuickStart/MultiColumnPanelIntro/MainControl";
		
		private const string OnlineDocumentationUrl = "https://www.actiprosoftware.com/docs/controls/wpf/index";

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the <c>ApplicationViewModel</c> class.
		/// </summary>
		public ApplicationViewModel() {
			ThemeManager.CurrentThemeChanged += OnThemeManagerCurrentThemeChanged;
			this.NavigateViewToHome(TransitionDirection.Forward);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Creates the <see cref="FrameworkElement"/> for the specified element's XAML path.
		/// </summary>
		/// <param name="path">The element's XAML path.</param>
		/// <returns>The <see cref="FrameworkElement"/> for the specified element's XAML path.</returns>
		private static FrameworkElement CreateElement(string path) {
			FrameworkElement element = null;
			if (!string.IsNullOrEmpty(path)) {
				var component = Application.LoadComponent(new Uri(path + ".xaml", UriKind.Relative));
				element = component as FrameworkElement;
				
				if (element == null) {
					var document = component as FlowDocument;
					var reader = new SimpleFlowDocumentReader();
					reader.Document = document;
					element = reader;
				}
			}

			return element;
		}
		
		/// <summary>
		/// Finds the <see cref="ProductItemInfo"/> for the specified <see cref="Uri"/>.
		/// </summary>
		/// <param name="uriString">The <see cref="Uri"/> to examine.</param>
		/// <returns>The <see cref="ProductItemInfo"/> for the specified <see cref="Uri"/>.</returns>
		private ProductItemInfo FindProductInfo(Uri uri) {
			if ((uri != null) && (productData != null)) {
				var targetPath = uri.LocalPath;

				foreach (var familyInfo in productData.ProductFamilies) {
					if ((familyInfo.OverviewItem != null) && (string.Compare(familyInfo.OverviewItem.Path, targetPath, StringComparison.OrdinalIgnoreCase) == 0))
						return familyInfo.OverviewItem;

					foreach (var itemInfo in familyInfo.Items) {
						if (string.Compare(itemInfo.Path, targetPath, StringComparison.OrdinalIgnoreCase) == 0)
							return itemInfo;
					}
				}

				foreach (var itemInfo in productData.Utilities.Items) {
					if (string.Compare(itemInfo.Path, targetPath, StringComparison.OrdinalIgnoreCase) == 0)
						return itemInfo;
				}
			}

			return null;
		}

		/// <summary>
		/// Returns the file system path to the sample project's folder.
		/// </summary>
		/// <returns>The file system path to the sample project's folder.</returns>
		private static string GetSampleProjectPath() {
			var uri = new Uri(Assembly.GetEntryAssembly().GetName().CodeBase);
			var path = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(uri.LocalPath), @"..\..\.."));
			return path;
		}

		/// <summary>
		/// Returns the <see cref="TransitionDirection"/> to use from one <see cref="ProductItemInfo"/> to another.
		/// </summary>
		/// <param name="fromItemInfo">The from <see cref="ProductItemInfo"/>.</param>
		/// <param name="toItemInfo">The to <see cref="ProductItemInfo"/>.</param>
		/// <returns>The <see cref="TransitionDirection"/> to use.</returns>
		private static TransitionDirection GetTransitionDirection(ProductItemInfo fromItemInfo, ProductItemInfo toItemInfo) {
			var transitionDirection = TransitionDirection.Forward;

			if ((fromItemInfo != null) && (fromItemInfo.ProductFamily != null) && (toItemInfo != null)) {
				var oldIndex = fromItemInfo.ProductFamily.Items.IndexOf(fromItemInfo);
				var newIndex = fromItemInfo.ProductFamily.Items.IndexOf(toItemInfo);
				if (newIndex < oldIndex)
					transitionDirection = TransitionDirection.Backward;
			}

			return transitionDirection;
		}

		/// <summary>
		/// Initializes the view element.
		/// </summary>
		/// <param name="element">The target element.</param>
		/// <param name="itemInfo">The <see cref="ProductItemInfo"/> navigation target.</param>
		private void InitializeViewElement(FrameworkElement element, ProductItemInfo itemInfo) {
			var productItemControl = element as ProductItemControl;
			if (productItemControl != null)
				productItemControl.SideBarWidth = (itemInfo.SideBarWidth == PredefinedSideBarWidth.Wide ? 400.0 : 300.0);
		}
		
		/// <summary>
		/// Occurs when the application theme is changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> containing data related to this event.</param>
		private void OnCodeViewerWindowClosed(object sender, EventArgs e) {
			if (codeViewerWindow != null) {
				codeViewerWindow.Closed -= this.OnCodeViewerWindowClosed;
				codeViewerWindow = null;
			}
		}

		/// <summary>
		/// Occurs when the application theme is changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> containing data related to this event.</param>
		private void OnThemeManagerCurrentThemeChanged(object sender, EventArgs e) {
			this.UpdateThemeCommands();
		}
		
		/// <summary>
		/// Opens an external sample window, if one is available for the current sample.
		/// </summary>
		/// <param name="fullName">The full path to the demo window.</param>
		private void OpenExternalSampleCore(string fullName) {
			if (!string.IsNullOrEmpty(fullName)) {
				try {
					var demoWindow = Application.LoadComponent(new Uri(fullName, UriKind.Relative)) as Window;
					if (demoWindow != null)
						demoWindow.Show();
				}
				catch (Exception ex) {
					while (ex.InnerException != null)
						ex = ex.InnerException;

					MessageBox.Show(string.Format("The sample '{0}' was unable to be loaded.\r\n\r\n{1}", fullName, ex.Message),
						"Sample Not Loaded", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				}
			}
		}
		
		/// <summary>
		/// Updates the navigation command can-execute states.
		/// </summary>
		private void UpdateNavigationCommands() {
			if (navigateViewToNextItemInfoCommand != null)
				navigateViewToNextItemInfoCommand.RaiseCanExecuteChanged();
			if (navigateViewToPreviousItemInfoCommand != null)
				navigateViewToPreviousItemInfoCommand.RaiseCanExecuteChanged();
			if (openSampleCodeCommand != null)
				openSampleCodeCommand.RaiseCanExecuteChanged();
			if (openSampleFolderCommand != null)
				openSampleFolderCommand.RaiseCanExecuteChanged();
		}

		/// <summary>
		/// Updates the search results.
		/// </summary>
		private void UpdateSearchResults() {
			var list = new List<ProductItemInfo>();

			// Score all items
			var searchParts = this.SearchText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			foreach (var productFamily in this.ProductData.ProductFamilies) {
				foreach (var productItemInfo in productFamily.Items) {
					productItemInfo.SearchScore = SampleSearchScorer.Score(productItemInfo, searchParts);
					if (productItemInfo.SearchScore > 0)
						list.Add(productItemInfo);
				}
			}

			// Sort
			list.Sort((x, y) => y.SearchScore.CompareTo(x.SearchScore));

			// Trim to the maximum number of results
			if (list.Count > MaximumSearchResults)
				list.RemoveRange(MaximumSearchResults, list.Count - MaximumSearchResults);

			this.SearchResults = list;
		}

		/// <summary>
		/// Updates the theme command can-execute states.
		/// </summary>
		private void UpdateThemeCommands() {
			if (setApplicationThemeCommand != null)
				setApplicationThemeCommand.RaiseCanExecuteChanged();
			if (toggleNativeThemesCommand != null)
				toggleNativeThemesCommand.RaiseCanExecuteChanged();

			this.IsUsingAutomaticThemes = ThemeManager.HasAutomaticThemes;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the Actipro blog URL.
		/// </summary>
		/// <value>The Actipro blog URL.</value>
		public string ActiproBlogUrl => "https://www.actiprosoftware.com/blog";
		
		/// <summary>
		/// Gets the Actipro contact URL.
		/// </summary>
		/// <value>The Actipro contact URL.</value>
		public string ActiproContactUrl => "https://www.actiprosoftware.com/company/contact";
		
		/// <summary>
		/// Gets the Actipro Twitter URL.
		/// </summary>
		/// <value>The Actipro Twitter URL.</value>
		public string ActiproTwitterUrl => "https://twitter.com/Actipro";
		
		/// <summary>
		/// Gets or sets the code viewer's selected path.
		/// </summary>
		/// <value>
		/// <c>true</c> if the code viewer's selected path; otherwise, <c>false</c>.
		/// </value>
		public string CodeViewerSelectedPath {
			get {
				return codeViewerSelectedPath;
			}
			set {
				if (codeViewerSelectedPath != value) {
					codeViewerSelectedPath = value;
					this.NotifyPropertyChanged(nameof(CodeViewerSelectedPath));
				}
			}
		}
		
		/// <summary>
		/// Gets the copyright message.
		/// </summary>
		/// <value>The copyright message.</value>
		public string Copyright => String.Format(CultureInfo.CurrentCulture, "Copyright \u00A9 2006-{0} Actipro Software LLC", DateTime.Today.Year);
		
		/// <summary>
		/// Gets or sets whether the application Backstage is open.
		/// </summary>
		/// <value>
		/// <c>true</c> if the application Backstage is open; otherwise, <c>false</c>.
		/// </value>
		public bool IsBackstageOpen {
			get {
				return isBackstageOpen;
			}
			set {
				if (isBackstageOpen != value) {
					isBackstageOpen = value;

					if (isBackstageOpen) {
						this.IsLoadingExternalSample = false;

						if (this.ViewItemInfo != null) {
							if (this.ViewItemInfo.IsReleaseHistory)
								this.OverlayMode = ApplicationOverlayMode.ReleaseHistoryBackstage;
							else if (this.ViewItemInfo.IsUtility)
								this.OverlayMode = ApplicationOverlayMode.UtilitiesBackstage;
							else
								this.OverlayMode = ApplicationOverlayMode.ProductItemInfoBackstage;
						}
						else
							this.OverlayMode = ApplicationOverlayMode.HomeBackstage;
					}

					this.NotifyPropertyChanged(nameof(IsBackstageOpen));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets whether an external sample is loading.
		/// </summary>
		/// <value>
		/// <c>true</c> if an external sample is loading; otherwise, <c>false</c>.
		/// </value>
		public bool IsLoadingExternalSample {
			get {
				return isLoadingExternalSample;
			}
			set {
				if (isLoadingExternalSample != value) {
					isLoadingExternalSample = value;

					if (isLoadingExternalSample) { 
						this.IsBackstageOpen = false;
						this.OverlayMode = ApplicationOverlayMode.ExternalSample;
					}

					this.NotifyPropertyChanged(nameof(IsLoadingExternalSample));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets whether automatic themes are in use.
		/// </summary>
		/// <value>
		/// <c>true</c> if automatic themes are in use; otherwise, <c>false</c>.
		/// </value>
		public bool IsUsingAutomaticThemes {
			get {
				return isUsingAutomaticThemes;
			}
			set {
				if (isUsingAutomaticThemes != value) {
					isUsingAutomaticThemes = value;

					this.NotifyPropertyChanged(nameof(IsUsingAutomaticThemes));
				}
			}
		}
		
		/// <summary>
		/// Navigates the view backward.
		/// </summary>
		public void NavigateViewBackward() {
			if (navigationService.CanGoBack) {
				var itemInfo = navigationService.GoBack();

				navigationService.IsNavigatingThroughHistory = true;
				try {
					if (itemInfo != null)
						this.NavigateViewToItemInfo(itemInfo, TransitionDirection.Backward);
					else
						this.NavigateViewToHome(TransitionDirection.Backward);
				}
				finally {
					navigationService.IsNavigatingThroughHistory = false;
				}
			}
		}

		/// <summary>
		/// Navigates the view forward.
		/// </summary>
		public void NavigateViewForward() {
			if (navigationService.CanGoForward) {
				navigationService.IsNavigatingThroughHistory = true;
				try {
					var itemInfo = navigationService.GoForward();
					if (itemInfo != null)
						this.NavigateViewToItemInfo(itemInfo, TransitionDirection.Forward);
					else
						this.NavigateViewToHome(TransitionDirection.Forward);
				}
				finally {
					navigationService.IsNavigatingThroughHistory = false;
				}
			}
		}

		/// <summary>
		/// Navigates the view to the home view.
		/// </summary>
		/// <param name="transitionDirection">The <see cref="TransitionDirection"/> if known.</param>
		public void NavigateViewToHome(TransitionDirection transitionDirection) {
			// Close Backstage
			this.IsBackstageOpen = false;

			// Update the view
			this.ViewItemInfo = null;
			this.ViewImageSource = ActiproSoftware.Products.Shared.AssemblyInfo.ActiproIconImageSource;
			this.ViewSubTitle = "Actipro Software";
			this.ViewTitle = "WPF Controls";
			this.ViewHasCustomStatusBar = false;
			this.ViewHasInterop = false;
			this.ViewHasNavigationButtons = false;
			this.StatusMessage = this.Copyright;
			this.ViewTransitionDirection = transitionDirection;
			this.ViewElement = new HomeControl();

			navigationService.NavigateTo(null);

			this.UpdateNavigationCommands();
		}
		
		/// <summary>
		/// Navigates the view to the specified <see cref="ProductItemInfo"/>.
		/// </summary>
		/// <param name="itemInfo">The <see cref="ProductItemInfo"/> navigation target.</param>
		/// <param name="transitionDirection">The <see cref="TransitionDirection"/> if known.</param>
		public void NavigateViewToItemInfo(ProductItemInfo itemInfo, TransitionDirection? transitionDirection) {
			// Create the view control
			FrameworkElement newViewElement = null;
			Exception ex = null;
			try {
				newViewElement = CreateElement(itemInfo?.Path);
			}
			catch (Exception ex2) {
				while (ex2.InnerException != null)
					ex2 = ex2.InnerException;
				ex = ex2;
			}
			if (newViewElement == null) {
				var errorTextBlock = new TextBlock() {
					FontSize = 18,
					HorizontalAlignment = HorizontalAlignment.Center,
					Margin = new Thickness(50),
					MaxWidth = 800,
					Text = String.Format(CultureInfo.CurrentCulture, "The sample '{0}' was unable to be loaded.", itemInfo.Path),
					TextWrapping = TextWrapping.Wrap,
					VerticalAlignment = VerticalAlignment.Center
				};
				if (ex != null)
					errorTextBlock.Text += Environment.NewLine + Environment.NewLine + "The error message was: " + ex.Message;
				newViewElement = errorTextBlock;
			}
			else
				this.InitializeViewElement(newViewElement, itemInfo);

			// Ensure a transition direction is set
			if (!transitionDirection.HasValue)
				transitionDirection = GetTransitionDirection(viewItemInfo, itemInfo);

			// Close Backstage
			this.IsBackstageOpen = false;

			// Update the view
			this.StatusMessage = (itemInfo.IsProductOverview ? this.Copyright : itemInfo.FolderPath);
			this.ViewItemInfo = itemInfo;
			this.ViewImageSource = itemInfo.ProductFamily.LogoImageSource;
			this.ViewSubTitle = String.Format(CultureInfo.CurrentCulture, "{0} / {1}", itemInfo.ProductFamily.Title, itemInfo.Category);
			this.ViewTitle = itemInfo.Title;
			this.ViewHasCustomStatusBar = itemInfo.HasCustomStatusBar;
			this.ViewHasInterop = itemInfo.HasInterop;
			this.ViewHasNavigationButtons = true;
			this.ViewTransitionDirection = transitionDirection.Value;
			this.ViewElement = newViewElement;

			navigationService.NavigateTo(itemInfo);

			this.UpdateNavigationCommands();
		}
		
		/// <summary>
		/// Gets the <see cref="ICommand"/> that navigates the view to the home page.
		/// </summary>
		/// <value>
		/// The <see cref="ICommand"/> that navigates the view to the home page.
		/// </value>
		public ICommand NavigateViewToHomeCommand {
			get {
				if (navigateViewToHomeCommand == null) {
					navigateViewToHomeCommand = new DelegateCommand<object>((param) => { 
						this.NavigateViewToHome(TransitionDirection.Forward);
					});
				}

				return navigateViewToHomeCommand;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="ICommand"/> that navigates the view to a <see cref="ProductItemInfo"/>.
		/// </summary>
		/// <value>
		/// The <see cref="ICommand"/> that navigates the view to a <see cref="ProductItemInfo"/>.
		/// </value>
		public ICommand NavigateViewToItemInfoCommand {
			get {
				if (navigateViewToItemInfoCommand == null) {
					navigateViewToItemInfoCommand = new DelegateCommand<object>((param) => { 
						var openExternalSampleAfter = false;
						var itemInfo = param as ProductItemInfo;
						if (itemInfo == null) {
							var uriString = param as string;
							if ((!string.IsNullOrEmpty(uriString)) && (uriString.StartsWith("https://ActiproSoftware/", StringComparison.OrdinalIgnoreCase)))
								param = new Uri(uriString);

							var uri = param as Uri;
							if (uri != null) {
								itemInfo = this.FindProductInfo(uri);
								openExternalSampleAfter = (uri.Query == "?action=open");
							}
						}

						if (itemInfo != null)
							this.NavigateViewToItemInfo(itemInfo, null);

						if (openExternalSampleAfter)
							this.OpenExternalSample(null);
					}, (param) => {
						return true;

						// Ideally we'd use the logic below instead, however due to the WPF bug where MenuItem.Command can-executes are not called again when the CommandParameter changes, 
						//   some root MenuItems would remain disabled due to a null CommandParameter initially being passed in (https://github.com/dotnet/wpf/issues/316)
						// return (param is ProductItemInfo) || (param is Uri) || (param is string);
					});
				}

				return navigateViewToItemInfoCommand;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="ICommand"/> that navigates the view to the next <see cref="ProductItemInfo"/>.
		/// </summary>
		/// <value>
		/// The <see cref="ICommand"/> that navigates the view to the next <see cref="ProductItemInfo"/>.
		/// </value>
		public ICommand NavigateViewToNextItemInfoCommand {
			get {
				if (navigateViewToNextItemInfoCommand == null) {
					navigateViewToNextItemInfoCommand = new DelegateCommand<object>((param) => { 
						if (viewItemInfo != null) {
							var nextItemInfo = viewItemInfo.NextItem;
							if (nextItemInfo != null)
								this.NavigateViewToItemInfo(nextItemInfo, TransitionDirection.Forward);
						}
					}, (param) => {
						return (viewItemInfo?.NextItem != null);
					});
				}

				return navigateViewToNextItemInfoCommand;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="ICommand"/> that navigates the view to the previous <see cref="ProductItemInfo"/>.
		/// </summary>
		/// <value>
		/// The <see cref="ICommand"/> that navigates the view to the previous <see cref="ProductItemInfo"/>.
		/// </value>
		public ICommand NavigateViewToPreviousItemInfoCommand {
			get {
				if (navigateViewToPreviousItemInfoCommand == null) {
					navigateViewToPreviousItemInfoCommand = new DelegateCommand<object>((param) => { 
						if (viewItemInfo != null) {
							var previousItemInfo = viewItemInfo.PreviousItem;
							if (previousItemInfo != null)
								this.NavigateViewToItemInfo(previousItemInfo, TransitionDirection.Backward);
						}
					}, (param) => {
						return (viewItemInfo?.PreviousItem != null);
					});
				}

				return navigateViewToPreviousItemInfoCommand;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="ICommand"/> that opens the sample project.
		/// </summary>
		/// <value>
		/// The <see cref="ICommand"/> that opens the sample project.
		/// </value>
		public ICommand OpenDocumentationCommand {
			get {
				if (openDocumentationCommand == null) {
					openDocumentationCommand = new DelegateCommand<object>((param) => { 
						// Try and find the offline documentation location in the registry
						string path = null;
						var version = ActiproSoftware.Products.Shared.AssemblyInfo.Instance.GetAssemblyVersion();
						var regKeyName = String.Format(@"SOFTWARE\Actipro Software\WPF Controls\{0}.{1}\Installed", version.Major, version.Minor);
						var regKey = Registry.LocalMachine.OpenSubKey(regKeyName);
						if (regKey == null) {
							regKeyName = regKeyName.Replace(@"SOFTWARE\", @"SOFTWARE\WOW6432Node\");
							regKey = Registry.LocalMachine.OpenSubKey(regKeyName);
						}
						if (regKey != null) {
							path = regKey.GetValue("Path") as string;
							if (path != null)
								path = Path.Combine(path, @"Documentation\index.html");
							regKey.Close();
						}

						if (File.Exists(path)) {
							try {
								// Open the documentation
								Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
							}
							catch (Exception ex) {
								MessageBox.Show(String.Format(CultureInfo.CurrentCulture, "The documentation file '{0}' was unable to be opened.  The error message was: {1}", path, ex.Message),
									"Documentation Not Opened", MessageBoxButton.OK, MessageBoxImage.Exclamation);
							}
						}
						else {
							// Open online documentation
							Process.Start(new ProcessStartInfo(String.Format(@"{0}?v={1}.{2}", OnlineDocumentationUrl, version.Major, version.Minor)) { UseShellExecute = true });
						}
					});
				}

				return openDocumentationCommand;
			}
		}
		
		/// <summary>
		/// Opens an external sample window, if one is available for the current sample.
		/// </summary>
		/// <param name="className">The XAML class name.</param>
		public void OpenExternalSample(string className) {
			if (viewItemInfo != null) {
				try {
					this.IsLoadingExternalSample = true;

					var fullName = viewItemInfo.FolderPath + "/" + (className ?? "MainWindow") + ".xaml";
					this.OpenExternalSampleCore(fullName);
				}
				finally {
					this.IsLoadingExternalSample = false;
				}
			}
		}

		/// <summary>
		/// Gets the <see cref="ICommand"/> that opens an external sample window.
		/// </summary>
		/// <value>
		/// The <see cref="ICommand"/> that opens an external sample window.
		/// </value>
		public ICommand OpenExternalSampleCommand {
			get {
				if (openExternalSampleCommand == null) {
					openExternalSampleCommand = new DelegateCommand<object>((param) => { 
						this.OpenExternalSample(param as string);
					}, (param) => {
						var depObj = param as DependencyObject;
						if (depObj != null)
							return (Window.GetWindow(depObj) is RootWindow);
						else
							return false;
					});
				}

				return openExternalSampleCommand;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="ICommand"/> that opens the code containing the current sample.
		/// </summary>
		/// <value>
		/// The <see cref="ICommand"/> that opens the code containing the current sample.
		/// </value>
		public ICommand OpenSampleCodeCommand {
			get {
				if (openSampleCodeCommand == null) {
					openSampleCodeCommand = new DelegateCommand<object>((param) => {
						// Create the code viewer window as needed
						if (codeViewerWindow == null) {
							codeViewerWindow = new CodeViewerWindow(this);
							codeViewerWindow.Closed += this.OnCodeViewerWindowClosed;
							codeViewerWindow.Show();
						}

						// Activate the window
						codeViewerWindow.Activate();

						// Select the sample's path
						var path = Path.Combine(GetSampleProjectPath(), viewItemInfo.Path.Replace('/', '\\').Substring(1)) + ".xaml";
						this.CodeViewerSelectedPath = path;
					}, (param) => {
						return (viewItemInfo != null) && (!viewItemInfo.IsProductOverview) && (!viewItemInfo.IsReleaseHistory) && (!string.IsNullOrEmpty(this.ProductSamplesPath));
					});
				}

				return openSampleCodeCommand;
			}
		}

		/// <summary>
		/// Gets the <see cref="ICommand"/> that opens the folder containing the current sample.
		/// </summary>
		/// <value>
		/// The <see cref="ICommand"/> that opens the folder containing the current sample.
		/// </value>
		public ICommand OpenSampleFolderCommand {
			get {
				if (openSampleFolderCommand == null) {
					openSampleFolderCommand = new DelegateCommand<object>((param) => { 
						var path = GetSampleProjectPath();

						if (viewItemInfo != null) {
							var folderPath = viewItemInfo.Path.Replace("/", @"\");
							if (folderPath.StartsWith(@"\"))
								folderPath = folderPath.Substring(1);

							path = Path.GetDirectoryName(Path.Combine(path, folderPath));
						}

						try {
							Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
						}
						catch (Exception ex) {
							MessageBox.Show(String.Format(CultureInfo.CurrentCulture, "The folder '{0}' was unable to be opened.  The error message was: {1}", path, ex.Message),
								"Folder Not Opened", MessageBoxButton.OK, MessageBoxImage.Exclamation);
						}
					}, (param) => {
						return (viewItemInfo != null) && (!viewItemInfo.IsProductOverview) && (!viewItemInfo.IsReleaseHistory) && (!string.IsNullOrEmpty(this.ProductSamplesPath));
					});
				}

				return openSampleFolderCommand;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="ICommand"/> that opens the sample project.
		/// </summary>
		/// <value>
		/// The <see cref="ICommand"/> that opens the sample project.
		/// </value>
		public ICommand OpenSampleProjectCommand {
			get {
				if (openSampleProjectCommand == null) {
					openSampleProjectCommand = new DelegateCommand<object>((param) => { 
						var path = Path.Combine(GetSampleProjectPath(), @"SampleBrowser.sln");

						try {
							Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
						}
						catch (Exception ex) {
							MessageBox.Show(String.Format(CultureInfo.CurrentCulture, "The project '{0}' was unable to be opened.  The error message was: {1}", path, ex.Message),
								"Project Not Opened", MessageBoxButton.OK, MessageBoxImage.Exclamation);
						}
					});
				}

				return openSampleProjectCommand;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="ICommand"/> that opens a web URL.
		/// </summary>
		/// <value>
		/// The <see cref="ICommand"/> that opens a web URL.
		/// </value>
		public ICommand OpenUrlCommand {
			get {
				if (openUrlCommand == null) {
					openUrlCommand = new DelegateCommand<object>((param) => { 
						var uriString = param as string;
						if (!string.IsNullOrEmpty(uriString)) {
							// For web URLs, navigate externally
							if ((uriString.StartsWith("https://")) || (uriString.StartsWith("http://"))) {
								try {
									Process.Start(new ProcessStartInfo(uriString) { UseShellExecute = true });
								}
								catch (Exception ex) {
									MessageBox.Show(String.Format(CultureInfo.CurrentCulture, "Navigation to the URL '{0}' was unable to be completed.  The error message was: {1}", uriString, ex.Message),
										"Navigation Unsuccessful", MessageBoxButton.OK, MessageBoxImage.Exclamation);
								}
								return;
							}
						}
					});
				}

				return openUrlCommand;
			}
		}
		
		/// <summary>
		/// Gets or sets the application overlay mode.
		/// </summary>
		/// <value>The application overlay mode.</value>
		public ApplicationOverlayMode OverlayMode {
			get {
				return overlayMode;
			}
			set {
				if (overlayMode != value) {
					overlayMode = value;
					this.NotifyPropertyChanged(nameof(OverlayMode));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the <see cref="ProductData"/> model.
		/// </summary>
		/// <value>The <see cref="ProductData"/> model.</value>
		public ProductData ProductData {
			get {
				return productData;
			}
			set {
				if (productData != value) {
					productData = value;
					this.NotifyPropertyChanged(nameof(ProductData));

					// Navigate to a default sample if specified
					if (!string.IsNullOrEmpty(DefaultSampleUri))
						this.NavigateViewToItemInfoCommand.Execute(DefaultSampleUri);
				}
			}
		}

		/// <summary>
		/// Gets the path to the product sample code root folder.
		/// </summary>
		/// <value>The path to the product sample code root folder.</value>
		public string ProductSamplesPath {
			get {
				if (productSamplesPath == null) {
					var path = Path.Combine(GetSampleProjectPath(), "ProductSamples");
					if (Directory.Exists(path))
						productSamplesPath = path;
					else
						productSamplesPath = String.Empty;
				}

				return productSamplesPath;
			}
		}
		
		/// <summary>
		/// Gets or sets the search results.
		/// </summary>
		/// <value>The search results.</value>
		public IList<ProductItemInfo> SearchResults {
			get {
				return searchResults;
			}
			set {
				if (searchResults != value) {
					searchResults = value;
					this.NotifyPropertyChanged(nameof(SearchResults));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the search text.
		/// </summary>
		/// <value>The search text.</value>
		public string SearchText {
			get {
				return searchText;
			}
			set {
				if (searchText != value) {
					searchText = value;
					this.NotifyPropertyChanged(nameof(SearchText));

					this.UpdateSearchResults();
				}
			}
		}
		
		/// <summary>
		/// Gets the <see cref="ICommand"/> that sets the application's theme.
		/// </summary>
		/// <value>
		/// The <see cref="ICommand"/> that sets the application's theme.
		/// </value>
		public ICommand SetApplicationThemeCommand {
			get {
				if (setApplicationThemeCommand == null) {
					setApplicationThemeCommand = new DelegateCommand<object>((param) => { 
						var themeName = param as string;
						if (string.IsNullOrEmpty(themeName)) {
							var sender = param as FrameworkElement;
							if (sender != null)
								themeName = sender.Tag as string;
						}

						if (!string.IsNullOrEmpty(themeName)) {
							ThemeManager.UnregisterAutomaticThemes();
							ThemeManager.CurrentTheme = themeName;
						}
					}, (param) => {
						var themeName = param as string;
						if (string.IsNullOrEmpty(themeName)) {
							var sender = param as FrameworkElement;
							if (sender != null)
								themeName = sender.Tag as string;
						}

						if (!string.IsNullOrEmpty(themeName)) {
							var menuItem = param as MenuItem;
							if (menuItem != null)
								menuItem.IsChecked = (ThemeManager.CurrentTheme == themeName);

							return true;
						}
						else
							return false;
					});
				}

				return setApplicationThemeCommand;
			}
		}
		
		/// <summary>
		/// Gets or sets the status message.
		/// </summary>
		/// <value>The status message.</value>
		public string StatusMessage {
			get {
				return statusMessage;
			}
			set {
				value = value ?? "Ready";

				if (statusMessage != value) {
					statusMessage = value;
					this.NotifyPropertyChanged(nameof(StatusMessage));
				}
			}
		}
		
		/// <summary>
		/// Gets the C# syntax language for the code viewer.
		/// </summary>
		/// <value>The C# syntax language.</value>
		public ISyntaxLanguage SyntaxLanguageCSharp {
			get {
				if (syntaxLanguageCSharp == null) {
					syntaxLanguageCSharp = new CSharpSyntaxLanguage();

					var projectAssembly = new CSharpProjectAssembly("CodeViewer");
					syntaxLanguageCSharp.RegisterService<IProjectAssembly>(projectAssembly);

					var assemblyLoader = new BackgroundWorker();
					assemblyLoader.DoWork += (sender, e) => {
						// Add some common assemblies for reflection (any custom assemblies could be added using various Add overloads instead)
						SyntaxEditorHelper.AddCommonDotNetSystemAssemblyReferences(projectAssembly);
					};
					assemblyLoader.RunWorkerAsync();
				}

				return syntaxLanguageCSharp;
			}
		}
		
		/// <summary>
		/// Gets the XAML syntax language for the code viewer.
		/// </summary>
		/// <value>The XAML syntax language.</value>
		public ISyntaxLanguage SyntaxLanguageXaml {
			get {
				if (syntaxLanguageXaml == null) {
					syntaxLanguageXaml = new XmlSyntaxLanguage();
					SyntaxEditorHelper.InitializeLanguageFromResourceStream(syntaxLanguageXaml, "Xaml.langdef");
				}

				return syntaxLanguageXaml;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="ICommand"/> that toggles whether the theme should automatically change to match the system's light/dark setting.
		/// </summary>
		/// <value>
		/// The <see cref="ICommand"/> that toggles whether the theme should automatically change to match the system's light/dark setting.
		/// </value>
		public ICommand ToggleAutomaticThemesCommand {
			get {
				if (toggleAutomaticThemesCommand == null) {
					toggleAutomaticThemesCommand = new DelegateCommand<object>((param) => { 
						if (ThemeManager.HasAutomaticThemes) {
							ThemeManager.UnregisterAutomaticThemes();
							ThemeManager.CurrentTheme = (ThemeManager.SystemApplicationMode == SystemApplicationMode.Light ? ThemeNames.Light : ThemeNames.Dark);
						}
						else
							ThemeManager.RegisterAutomaticThemes(ThemeNames.Light, ThemeNames.Dark, ThemeNames.HighContrast);

						this.UpdateThemeCommands();
					});
				}

				return toggleAutomaticThemesCommand;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="ICommand"/> that toggles whether the Backstage is open.
		/// </summary>
		/// <value>
		/// The <see cref="ICommand"/> that toggles whether the Backstage is open.
		/// </value>
		public ICommand ToggleIsBackstageOpenCommand {
			get {
				if (toggleIsBackstageOpenCommand == null) {
					toggleIsBackstageOpenCommand = new DelegateCommand<object>((param) => { 
						this.IsBackstageOpen = !this.IsBackstageOpen;
					});
				}

				return toggleIsBackstageOpenCommand;
			}
		}
		
		/// <summary>
		/// Gets the <see cref="ICommand"/> that toggles whether native control theming are enabled.
		/// </summary>
		/// <value>
		/// The <see cref="ICommand"/> that toggles whether native control theming are enabled.
		/// </value>
		public ICommand ToggleNativeThemesCommand {
			get {
				if (toggleNativeThemesCommand == null) {
					toggleNativeThemesCommand = new DelegateCommand<object>((param) => { 
						ThemeManager.AreNativeThemesEnabled = !ThemeManager.AreNativeThemesEnabled;

						var menuItem = param as MenuItem;
						if (menuItem != null)
							menuItem.IsChecked = ThemeManager.AreNativeThemesEnabled;
					}, (param) => {
						var menuItem = param as MenuItem;
						if (menuItem != null)
							menuItem.IsChecked = ThemeManager.AreNativeThemesEnabled;

						return true;
					});
				}

				return toggleNativeThemesCommand;
			}
		}
		
		/// <summary>
		/// Gets or sets the <see cref="FrameworkElement"/> that renders the view's UI.
		/// </summary>
		/// <value>The <see cref="FrameworkElement"/> that renders the view's UI.</value>
		public FrameworkElement ViewElement {
			get {
				return viewElement;
			}
			set {
				if (viewElement != value) {
					// Notify any existing control that it is being unloaded
					var productItemControl = viewElement as ProductItemControl;
					if (productItemControl != null)
						productItemControl.NotifyUnloaded();

					viewElement = value;
					this.NotifyPropertyChanged(nameof(ViewElement));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets whether the view has a custom statusbar.
		/// </summary>
		/// <value>
		/// <c>true</c> if the view has a custom statusbar; otherwise, <c>false</c>.
		/// </value>
		public bool ViewHasCustomStatusBar {
			get {
				return viewHasCustomStatusBar;
			}
			set {
				if (viewHasCustomStatusBar != value) {
					viewHasCustomStatusBar = value;
					this.NotifyPropertyChanged(nameof(ViewHasCustomStatusBar));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets whether the view has any interop controls that may cause airspace issues with Backstage overlays.
		/// </summary>
		/// <value>
		/// <c>true</c> if the view has any interop controls that may cause airspace issues with Backstage overlays; otherwise, <c>false</c>.
		/// </value>
		public bool ViewHasInterop {
			get {
				return viewHasInterop;
			}
			set {
				if (viewHasInterop != value) {
					viewHasInterop = value;
					this.NotifyPropertyChanged(nameof(ViewHasInterop));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets whether the view has navigation buttons.
		/// </summary>
		/// <value>
		/// <c>true</c> if the view has navigation buttons; otherwise, <c>false</c>.
		/// </value>
		public bool ViewHasNavigationButtons {
			get {
				return viewHasNavigationButtons;
			}
			set {
				if (viewHasNavigationButtons != value) {
					viewHasNavigationButtons = value;
					this.NotifyPropertyChanged(nameof(ViewHasNavigationButtons));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the view's image source.
		/// </summary>
		/// <value>The view's image source.</value>
		public ImageSource ViewImageSource {
			get {
				return viewImageSource;
			}
			set {
				if (viewImageSource != value) {
					viewImageSource = value;
					this.NotifyPropertyChanged(nameof(ViewImageSource));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the <see cref="ProductItemInfo"/> currently in the view, if any.
		/// </summary>
		/// <value>The <see cref="ProductItemInfo"/> currently in the view, if any.</value>
		public ProductItemInfo ViewItemInfo {
			get {
				return viewItemInfo;
			}
			set {
				if (viewItemInfo != value) {
					viewItemInfo = value;
					this.NotifyPropertyChanged(nameof(ViewItemInfo));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the view's sub-title.
		/// </summary>
		/// <value>The view's sub-title.</value>
		public string ViewSubTitle {
			get {
				return viewSubTitle;
			}
			set {
				if (viewSubTitle != value) {
					viewSubTitle = value;
					this.NotifyPropertyChanged(nameof(ViewSubTitle));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the view's title.
		/// </summary>
		/// <value>The view's title.</value>
		public string ViewTitle {
			get {
				return viewTitle;
			}
			set {
				if (viewTitle != value) {
					viewTitle = value;
					this.NotifyPropertyChanged(nameof(ViewTitle));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the view's transition direction.
		/// </summary>
		/// <value>The view's transition direction.</value>
		public TransitionDirection ViewTransitionDirection {
			get {
				return viewTransitionDirection;
			}
			set {
				if (viewTransitionDirection != value) {
					viewTransitionDirection = value;
					this.NotifyPropertyChanged(nameof(ViewTransitionDirection));
				}
			}
		}
		
		/// <summary>
		/// Gets the purchase licenses URL.
		/// </summary>
		/// <value>The purchase licenses URL.</value>
		public string WpfProductsUrl => "https://www.actiprosoftware.com/products/controls/wpf";
		
		/// <summary>
		/// Gets the purchase licenses URL.
		/// </summary>
		/// <value>The purchase licenses URL.</value>
		public string WpfPurchaseLicensesUrl => "https://www.actiprosoftware.com/purchase/pricing/controls/wpf";
		
	}

}
