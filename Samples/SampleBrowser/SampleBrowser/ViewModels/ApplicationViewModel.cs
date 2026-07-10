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
using System.Reflection;
using System.Windows.Documents;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.SampleBrowser;

/// <summary>
/// Provides the application view-model.
/// </summary>
public class ApplicationViewModel : ObservableObjectBase {

	private const string ReadyMessage = "Ready";

	private string? _codeViewerSelectedPath;
	private CodeViewerWindow? _codeViewerWindow;
	private bool _isBackstageOpen;
	private bool _isLoadingExternalSample;
	private bool _isUsingAutomaticThemes;
	private readonly NavigationService _navigationService = new();
	private ApplicationOverlayMode _overlayMode = ApplicationOverlayMode.HomeBackstage;
	private ProductData? _productData;
	private string? _productSamplesPath;
	private IList<ProductItemInfo>? _searchResults;
	private string _searchText = string.Empty;
	private string? _statusMessage;
	private CSharpSyntaxLanguage? _syntaxLanguageCSharp;
	private ISyntaxLanguage? _syntaxLanguageXaml;
	private FrameworkElement? _viewElement;
	private bool _viewHasCustomStatusBar;
	private bool _viewHasInterop;
	private bool _viewHasNavigationButtons;
	private ImageSource? _viewImageSource;
	private ProductItemInfo? _viewItemInfo;
	private string? _viewSubTitle;
	private string? _viewTitle;
	private TransitionDirection _viewTransitionDirection = TransitionDirection.Forward;

	private DelegateCommand<object>? _navigateViewToHomeCommand;
	private DelegateCommand<object>? _navigateViewToItemInfoCommand;
	private DelegateCommand<object>? _navigateViewToNextItemInfoCommand;
	private DelegateCommand<object>? _navigateViewToPreviousItemInfoCommand;
	private DelegateCommand<object>? _openDocumentationCommand;
	private DelegateCommand<object>? _openExternalSampleCommand;
	private DelegateCommand<object>? _openSampleCodeCommand;
	private DelegateCommand<object>? _openSampleFolderCommand;
	private DelegateCommand<object>? _openSampleProjectCommand;
	private DelegateCommand<object>? _openUrlCommand;
	private DelegateCommand<object>? _setApplicationThemeCommand;
	private DelegateCommand<object>? _toggleAutomaticThemesCommand;
	private DelegateCommand<object>? _toggleIsBackstageOpenCommand;
	private DelegateCommand<object>? _toggleNativeThemesCommand;
	private DelegateCommand<object>? _toggleWindowBackdropCommand;

	private static readonly bool IsMainWindowSystemBackdropSupported = EnvironmentHelper.IsFeatureSupported(WindowsFeatureKind.MainWindowSystemBackdrop);

	private const int MaximumSearchResults = 50;

	private const string DefaultSampleUri = null;
	// private const string DefaultSampleUri = "https://ActiproSoftware/SampleBrowser/Documents/ProductOverviews/Themes";
	// private const string DefaultSampleUri = "https://ActiproSoftware/ProductSamples/ViewsSamples/Demo/ApplicationSettings/MainControl";

	private const string OnlineDocumentationUrl = "https://www.actiprosoftware.com/docs/controls/wpf/index";

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public ApplicationViewModel() {
		ThemeManager.CurrentThemeChanged += OnThemeManagerCurrentThemeChanged;
		NavigateViewToHome(TransitionDirection.Forward);

		// Make the current application view model easily accessible to the sample application
		Current = this;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates the <see cref="FrameworkElement"/> for the specified element's XAML path.
	/// </summary>
	/// <param name="path">The element's XAML path.</param>
	private static FrameworkElement? CreateElement(string? path) {
		FrameworkElement? element = null;
		if (!string.IsNullOrEmpty(path)) {
			var component = Application.LoadComponent(new Uri(path + ".xaml", UriKind.Relative));
			element = component as FrameworkElement
				?? new SimpleFlowDocumentReader { Document = component as FlowDocument };
		}

		return element;
	}

	/// <summary>
	/// Finds the <see cref="ProductItemInfo"/> for the specified <see cref="Uri"/>.
	/// </summary>
	/// <param name="uriString">The <see cref="Uri"/> to examine.</param>
	private ProductItemInfo? FindProductInfo(Uri uri) {
		if ((uri is not null) && (_productData is not null)) {
			var targetPath = uri.LocalPath;

			foreach (var familyInfo in _productData.ProductFamilies) {
				if ((familyInfo.OverviewItem is { } overviewItem) && (string.Compare(overviewItem.Path, targetPath, StringComparison.OrdinalIgnoreCase) == 0))
					return overviewItem;

				foreach (var itemInfo in familyInfo.Items) {
					if (string.Compare(itemInfo.Path, targetPath, StringComparison.OrdinalIgnoreCase) == 0)
						return itemInfo;
				}
			}

			if (_productData.Utilities is not null) {
				foreach (var itemInfo in _productData.Utilities.Items) {
					if (string.Compare(itemInfo.Path, targetPath, StringComparison.OrdinalIgnoreCase) == 0)
						return itemInfo;
				}
			}
		}

		return null;
	}

	/// <summary>
	/// Returns the file system path to the sample project's folder.
	/// </summary>
	private static string GetSampleProjectPath() {
		var location = Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!;
		var path = Path.GetFullPath(Path.Combine(location, @"..\..\.."));
		return path;
	}

	/// <summary>
	/// Returns the <see cref="TransitionDirection"/> to use from one <see cref="ProductItemInfo"/> to another.
	/// </summary>
	/// <param name="fromItemInfo">The from <see cref="ProductItemInfo"/>.</param>
	/// <param name="toItemInfo">The to <see cref="ProductItemInfo"/>.</param>
	private static TransitionDirection GetTransitionDirection(ProductItemInfo? fromItemInfo, ProductItemInfo toItemInfo) {
		var transitionDirection = TransitionDirection.Forward;

		if ((fromItemInfo?.ProductFamily is { } fromProductFamily) && (toItemInfo is not null)) {
			var oldIndex = fromProductFamily.Items.IndexOf(fromItemInfo);
			var newIndex = fromProductFamily.Items.IndexOf(toItemInfo);
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
	private static void InitializeViewElement(FrameworkElement element, ProductItemInfo itemInfo) {
		if (element is ProductItemControl productItemControl)
			productItemControl.SideBarWidth = (itemInfo.SideBarWidth == PredefinedSideBarWidth.Wide ? 400.0 : 300.0);
	}

	private void OnCodeViewerWindowClosed(object? sender, EventArgs e) {
		if (_codeViewerWindow is not null)
			_codeViewerWindow.Closed -= OnCodeViewerWindowClosed;
		_codeViewerWindow = null;
	}

	private void OnThemeManagerCurrentThemeChanged(object? sender, EventArgs e)
		=> UpdateThemeCommands();

	/// <summary>
	/// Opens an external sample window, if one is available for the current sample.
	/// </summary>
	/// <param name="fullName">The full path to the demo window.</param>
	private static void OpenExternalSampleCore(string fullName) {
		if (!string.IsNullOrEmpty(fullName)) {
			try {
				var demoWindow = Application.LoadComponent(new Uri(fullName, UriKind.Relative)) as Window;
				demoWindow?.Show();
			}
			catch (Exception ex) {
				while (ex.InnerException is not null)
					ex = ex.InnerException;

				MessageBox.Show(string.Format("The sample '{0}' was unable to be loaded.\r\n\r\n{1}", fullName, ex.Message),
					"Sample Not Loaded", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}
	}

	private void UpdateNavigationCommands() {
		_navigateViewToNextItemInfoCommand?.RaiseCanExecuteChanged();
		_navigateViewToPreviousItemInfoCommand?.RaiseCanExecuteChanged();
		_openSampleCodeCommand?.RaiseCanExecuteChanged();
		_openSampleFolderCommand?.RaiseCanExecuteChanged();
	}

	private void UpdateSearchResults() {
		var list = new List<ProductItemInfo>();

		// Score all items
		var searchParts = SearchText.Split([' '], StringSplitOptions.RemoveEmptyEntries);
		if (ProductData is not null) {
			foreach (var productFamily in ProductData.ProductFamilies) {
				foreach (var productItemInfo in productFamily.Items) {
					productItemInfo.SearchScore = SampleSearchScorer.Score(productItemInfo, searchParts);
					if (productItemInfo.SearchScore > 0)
						list.Add(productItemInfo);
				}
			}
		}

		// Sort
		list.Sort((x, y) => y.SearchScore.CompareTo(x.SearchScore));

		// Trim to the maximum number of results
		if (list.Count > MaximumSearchResults)
			list.RemoveRange(MaximumSearchResults, list.Count - MaximumSearchResults);

		SearchResults = list;
	}

	private void UpdateThemeCommands() {
		_setApplicationThemeCommand?.RaiseCanExecuteChanged();
		_toggleNativeThemesCommand?.RaiseCanExecuteChanged();
		_toggleWindowBackdropCommand?.RaiseCanExecuteChanged();

		IsUsingAutomaticThemes = ThemeManager.HasAutomaticThemes;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	#pragma warning disable CA1822 // Mark members as static
	/// <summary>
	/// The Actipro blog URL.
	/// </summary>
	public string ActiproBlogUrl
		=> "https://www.actiprosoftware.com/blog";
	#pragma warning restore CA1822

	#pragma warning disable CA1822 // Mark members as static
	/// <summary>
	/// The Actipro contact URL.
	/// </summary>
	public string ActiproContactUrl
		=> "https://www.actiprosoftware.com/company/contact";
	#pragma warning restore CA1822

	#pragma warning disable CA1822 // Mark members as static
	/// <summary>
	/// The Actipro Twitter URL.
	/// </summary>
	public string ActiproTwitterUrl
		=> "https://x.com/Actipro";
	#pragma warning restore CA1822

	/// <summary>
	/// The code viewer's selected path.
	/// </summary>
	public string? CodeViewerSelectedPath {
		get => _codeViewerSelectedPath;
		set => SetProperty(ref _codeViewerSelectedPath, value);
	}

	#pragma warning disable CA1822 // Mark members as static
	/// <summary>
	/// The copyright message.
	/// </summary>
	public string Copyright
		=> ActiproSoftware.Properties.Shared.AssemblyInfo.Instance.CopyrightDisplayText;
	#pragma warning restore CA1822

	/// <summary>
	/// The currently-loaded application view-model.
	/// </summary>
	public static ApplicationViewModel? Current { get; private set; }

	/// <summary>
	/// Indicates whether the application Backstage is open.
	/// </summary>
	public bool IsBackstageOpen {
		get => _isBackstageOpen;
		set {
			if (SetProperty(ref _isBackstageOpen, value)) {
				if (_isBackstageOpen) {
					IsLoadingExternalSample = false;

					if (ViewItemInfo is not null) {
						if (ViewItemInfo.IsReleaseHistory)
							OverlayMode = ApplicationOverlayMode.ReleaseHistoryBackstage;
						else if (ViewItemInfo.IsUtility)
							OverlayMode = ApplicationOverlayMode.UtilitiesBackstage;
						else
							OverlayMode = ApplicationOverlayMode.ProductItemInfoBackstage;
					}
					else
						OverlayMode = ApplicationOverlayMode.HomeBackstage;
				}
			}
		}
	}

	/// <summary>
	/// Indicates whether an external sample is loading.
	/// </summary>
	public bool IsLoadingExternalSample {
		get => _isLoadingExternalSample;
		set {
			if (SetProperty(ref _isLoadingExternalSample, value)) {
				if (_isLoadingExternalSample) {
					IsBackstageOpen = false;
					OverlayMode = ApplicationOverlayMode.ExternalSample;
				}
			}
		}
	}

	/// <summary>
	/// Indicates whether automatic themes are in use.
	/// </summary>
	public bool IsUsingAutomaticThemes {
		get => _isUsingAutomaticThemes;
		set => SetProperty(ref _isUsingAutomaticThemes, value);
	}

	/// <summary>
	/// Navigates the view backward.
	/// </summary>
	public void NavigateViewBackward() {
		if (_navigationService.CanGoBack) {
			var itemInfo = _navigationService.GoBack();

			_navigationService.IsNavigatingThroughHistory = true;
			try {
				if (itemInfo is not null)
					NavigateViewToItemInfo(itemInfo, TransitionDirection.Backward);
				else
					NavigateViewToHome(TransitionDirection.Backward);
			}
			finally {
				_navigationService.IsNavigatingThroughHistory = false;
			}
		}
	}

	/// <summary>
	/// Navigates the view forward.
	/// </summary>
	public void NavigateViewForward() {
		if (_navigationService.CanGoForward) {
			_navigationService.IsNavigatingThroughHistory = true;
			try {
				var itemInfo = _navigationService.GoForward();
				if (itemInfo is not null)
					NavigateViewToItemInfo(itemInfo, TransitionDirection.Forward);
				else
					NavigateViewToHome(TransitionDirection.Forward);
			}
			finally {
				_navigationService.IsNavigatingThroughHistory = false;
			}
		}
	}

	/// <summary>
	/// Navigates the view to the home view.
	/// </summary>
	/// <param name="transitionDirection">The <see cref="TransitionDirection"/> if known.</param>
	public void NavigateViewToHome(TransitionDirection transitionDirection) {
		// Close Backstage
		IsBackstageOpen = false;

		// Update the view
		ViewItemInfo = null;
		ViewImageSource = ActiproSoftware.Properties.Shared.AssemblyInfo.ActiproIconImageSource;
		ViewSubTitle = "Actipro Software";
		ViewTitle = "WPF Controls";
		ViewHasCustomStatusBar = false;
		ViewHasInterop = false;
		ViewHasNavigationButtons = false;
		StatusMessage = Copyright;
		ViewTransitionDirection = transitionDirection;
		ViewElement = new HomeControl();

		_navigationService.NavigateTo(itemInfo: null);

		UpdateNavigationCommands();
	}

	/// <summary>
	/// Navigates the view to the specified <see cref="ProductItemInfo"/>.
	/// </summary>
	/// <param name="itemInfo">The <see cref="ProductItemInfo"/> navigation target.</param>
	/// <param name="transitionDirection">The <see cref="TransitionDirection"/> if known.</param>
	public void NavigateViewToItemInfo(ProductItemInfo itemInfo, TransitionDirection? transitionDirection) {
		// Create the view control
		FrameworkElement? newViewElement = null;
		Exception? ex = null;
		try {
			newViewElement = CreateElement(itemInfo.Path);
		}
		catch (Exception ex2) {
			while (ex2.InnerException is not null)
				ex2 = ex2.InnerException;
			ex = ex2;
		}
		if (newViewElement is null) {
			var errorTextBlock = new TextBlock() {
				FontSize = 18,
				HorizontalAlignment = HorizontalAlignment.Center,
				Margin = new Thickness(50),
				MaxWidth = 800,
				Text = string.Format(CultureInfo.CurrentCulture, "The sample '{0}' was unable to be loaded.", itemInfo.Path),
				TextWrapping = TextWrapping.Wrap,
				VerticalAlignment = VerticalAlignment.Center
			};
			if (ex is not null)
				errorTextBlock.Text += Environment.NewLine + Environment.NewLine + "The error message was: " + ex.Message;
			newViewElement = errorTextBlock;
		}
		else
			InitializeViewElement(newViewElement, itemInfo);

		// Ensure a transition direction is set
		if (!transitionDirection.HasValue)
			transitionDirection = GetTransitionDirection(_viewItemInfo, itemInfo);

		// Close Backstage
		IsBackstageOpen = false;

		// Update the view
		StatusMessage = (itemInfo.IsProductOverview ? Copyright : itemInfo.FolderPath);
		ViewItemInfo = itemInfo;
		ViewImageSource = itemInfo.ProductFamily?.LogoImageSource;
		ViewSubTitle = string.Format(CultureInfo.CurrentCulture, "{0} / {1}", itemInfo.ProductFamily?.Title, itemInfo.Category);
		ViewTitle = itemInfo.Title;
		ViewHasCustomStatusBar = itemInfo.HasCustomStatusBar;
		ViewHasInterop = itemInfo.HasInterop;
		ViewHasNavigationButtons = true;
		ViewTransitionDirection = transitionDirection.Value;
		ViewElement = newViewElement;

		_navigationService.NavigateTo(itemInfo);

		UpdateNavigationCommands();
	}

	/// <summary>
	/// The <see cref="ICommand"/> that navigates the view to the home page.
	/// </summary>
	public ICommand NavigateViewToHomeCommand
		=> _navigateViewToHomeCommand ??= new DelegateCommand<object>(_ => NavigateViewToHome(TransitionDirection.Forward));

	/// <summary>
	/// The <see cref="ICommand"/> that navigates the view to a <see cref="ProductItemInfo"/>.
	/// </summary>
	public ICommand NavigateViewToItemInfoCommand {
		get => _navigateViewToItemInfoCommand ??= new DelegateCommand<object>(
			param => {
				var openExternalSampleAfter = false;
				var itemInfo = param as ProductItemInfo;
				if (itemInfo is null) {
					var uriString = param as string;
					if (uriString?.StartsWith("https://ActiproSoftware/", StringComparison.OrdinalIgnoreCase) == true)
						param = new Uri(uriString);

					var uri = param as Uri;
					if (uri is not null) {
						itemInfo = FindProductInfo(uri);
						openExternalSampleAfter = (uri.Query == "?action=open");
					}
				}

				if (itemInfo is not null)
					NavigateViewToItemInfo(itemInfo, transitionDirection: null);

				if (openExternalSampleAfter)
					OpenExternalSample(className: null);
			},
			_ => {
				return true;

				// Ideally we'd use the logic below instead, however due to the WPF bug where MenuItem.Command can-executes are not called again when the CommandParameter changes,
				//   some root MenuItems would remain disabled due to a null CommandParameter initially being passed in (https://github.com/dotnet/wpf/issues/316)
				// return (param is ProductItemInfo) || (param is Uri) || (param is string);
			}
		);
	}

	/// <summary>
	/// The <see cref="ICommand"/> that navigates the view to the next <see cref="ProductItemInfo"/>.
	/// </summary>
	public ICommand NavigateViewToNextItemInfoCommand {
		get => _navigateViewToNextItemInfoCommand ??= new DelegateCommand<object>(
			_ => {
				if (_viewItemInfo?.NextItem is { } nextItemInfo)
					NavigateViewToItemInfo(nextItemInfo, TransitionDirection.Forward);
			},
			_ => _viewItemInfo?.NextItem is not null
		);
	}

	/// <summary>
	/// The <see cref="ICommand"/> that navigates the view to the previous <see cref="ProductItemInfo"/>.
	/// </summary>
	public ICommand NavigateViewToPreviousItemInfoCommand {
		get => _navigateViewToPreviousItemInfoCommand ??= new DelegateCommand<object>(
			_ => {
				if (_viewItemInfo?.PreviousItem is { } previousItemInfo)
					NavigateViewToItemInfo(previousItemInfo, TransitionDirection.Backward);
			},
			_ => _viewItemInfo?.PreviousItem is not null
		);
	}

	/// <summary>
	/// The <see cref="ICommand"/> that opens the sample project.
	/// </summary>
	public ICommand OpenDocumentationCommand {
		get => _openDocumentationCommand ??= new DelegateCommand<object>((param) => {
			// Try and find the offline documentation location in the registry
			string? path = null;
			var version = ActiproSoftware.Properties.Shared.AssemblyInfo.Instance.Version;
			var regKeyName = string.Format(@"SOFTWARE\Actipro Software\WPF Controls\{0}.{1}\Installed", version.Major, version.Minor);
			var regKey = Registry.LocalMachine.OpenSubKey(regKeyName);
			if (regKey is null) {
				regKeyName = regKeyName.Replace(@"SOFTWARE\", @"SOFTWARE\WOW6432Node\");
				regKey = Registry.LocalMachine.OpenSubKey(regKeyName);
			}
			if (regKey is not null) {
				path = regKey.GetValue("Path") as string;
				if (path is not null)
					path = Path.Combine(path, @"Documentation\index.html");
				regKey.Close();
			}

			if (File.Exists(path)) {
				try {
					// Open the documentation
					Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
				}
				catch (Exception ex) {
					MessageBox.Show(string.Format(CultureInfo.CurrentCulture, "The documentation file '{0}' was unable to be opened.  The error message was: {1}", path, ex.Message),
						"Documentation Not Opened", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				}
			}
			else {
				// Open online documentation
				Process.Start(new ProcessStartInfo(string.Format(@"{0}?v={1}.{2}", OnlineDocumentationUrl, version.Major, version.Minor)) { UseShellExecute = true });
			}
		});
	}

	/// <summary>
	/// Opens an external sample window, if one is available for the current sample.
	/// </summary>
	/// <param name="className">The XAML class name.</param>
	public void OpenExternalSample(string? className) {
		if (_viewItemInfo is not null) {
			try {
				IsLoadingExternalSample = true;

				var fullName = _viewItemInfo.FolderPath + "/" + (className ?? "MainWindow") + ".xaml";
				OpenExternalSampleCore(fullName);
			}
			finally {
				IsLoadingExternalSample = false;
			}
		}
	}

	/// <summary>
	/// The <see cref="ICommand"/> that opens an external sample window.
	/// </summary>
	public ICommand OpenExternalSampleCommand {
		get => _openExternalSampleCommand ??= new DelegateCommand<object>(
			param => OpenExternalSample(param as string),
			param => (param is DependencyObject depObj) && Window.GetWindow(depObj) is RootWindow
		);
	}

	/// <summary>
	/// The <see cref="ICommand"/> that opens the code containing the current sample.
	/// </summary>
	public ICommand OpenSampleCodeCommand {
		get => _openSampleCodeCommand ??= new DelegateCommand<object>(
			param => {
				// Create the code viewer window as needed
				if (_codeViewerWindow is null) {
					_codeViewerWindow = new CodeViewerWindow(this);
					_codeViewerWindow.Closed += OnCodeViewerWindowClosed;
					_codeViewerWindow.Show();
				}

				// Activate the window
				_codeViewerWindow.Activate();

				// Select the sample's path
				var sampleRelativePath = (param as string) ?? _viewItemInfo?.Path;
				if (sampleRelativePath is not null) {
					var path = Path.Combine(GetSampleProjectPath(), sampleRelativePath.Replace('/', '\\').Substring(1)) + ".xaml";
					CodeViewerSelectedPath = path;
				}
			},
			param => {
				if (param is string { Length: > 0 })
					return true;
				return (_viewItemInfo is { IsProductOverview: false, IsReleaseHistory: false }) && (!string.IsNullOrEmpty(ProductSamplesPath));
			}
		);
	}

	/// <summary>
	/// The <see cref="ICommand"/> that opens the folder containing the current sample.
	/// </summary>
	public ICommand OpenSampleFolderCommand {
		get => _openSampleFolderCommand ??= new DelegateCommand<object>(
			_ => {
				var path = GetSampleProjectPath();

				if (_viewItemInfo?.Path is not null) {
					var folderPath = _viewItemInfo.Path.Replace("/", @"\");
					if (folderPath.StartsWith(@"\"))
						folderPath = folderPath.Substring(1);

					path = Path.GetDirectoryName(Path.Combine(path, folderPath));
				}

				try {
					Process.Start(new ProcessStartInfo(path!) { UseShellExecute = true });
				}
				catch (Exception ex) {
					MessageBox.Show(string.Format(CultureInfo.CurrentCulture, "The folder '{0}' was unable to be opened.  The error message was: {1}", path, ex.Message),
						"Folder Not Opened", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				}
			},
			_ => (_viewItemInfo is { IsProductOverview: false, IsReleaseHistory: false }) && (!string.IsNullOrEmpty(ProductSamplesPath))
		);
	}

	/// <summary>
	/// The <see cref="ICommand"/> that opens the sample project.
	/// </summary>
	public ICommand OpenSampleProjectCommand {
		get => _openSampleProjectCommand ??= new DelegateCommand<object>(_ => {
			var path = Path.Combine(GetSampleProjectPath(), @"SampleBrowser.sln");

			try {
				Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
			}
			catch (Exception ex) {
				MessageBox.Show(string.Format(CultureInfo.CurrentCulture, "The project '{0}' was unable to be opened.  The error message was: {1}", path, ex.Message),
					"Project Not Opened", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		});
	}

	/// <summary>
	/// The <see cref="ICommand"/> that opens a web URL.
	/// </summary>
	public ICommand OpenUrlCommand {
		get => _openUrlCommand ??= new DelegateCommand<object>(param => {
			if (param is string { Length: > 0 } uriString) {
				// For web URLs, navigate externally
				if (uriString.StartsWith("https://") || uriString.StartsWith("http://")) {
					try {
						Process.Start(new ProcessStartInfo(uriString) { UseShellExecute = true });
					}
					catch (Exception ex) {
						MessageBox.Show(string.Format(CultureInfo.CurrentCulture, "Navigation to the URL '{0}' was unable to be completed.  The error message was: {1}", uriString, ex.Message),
							"Navigation Unsuccessful", MessageBoxButton.OK, MessageBoxImage.Exclamation);
					}
					return;
				}
			}
		});
	}

	/// <summary>
	/// The application overlay mode.
	/// </summary>
	public ApplicationOverlayMode OverlayMode {
		get => _overlayMode;
		set => SetProperty(ref _overlayMode, value);
	}

	/// <summary>
	/// The <see cref="ProductData"/> model.
	/// </summary>
	public ProductData? ProductData {
		get => _productData;
		set {
			if (SetProperty(ref _productData, value)) {
				// Navigate to a default sample if specified
				if (!string.IsNullOrEmpty(DefaultSampleUri))
					NavigateViewToItemInfoCommand.Execute(DefaultSampleUri);
			}
		}
	}

	/// <summary>
	/// The path to the product sample code root folder.
	/// </summary>
	public string ProductSamplesPath {
		get {
			if (_productSamplesPath is null) {
				var path = Path.Combine(GetSampleProjectPath(), "ProductSamples");
				_productSamplesPath = Directory.Exists(path)
					? path
					: string.Empty;
			}

			return _productSamplesPath;
		}
	}

	/// <summary>
	/// The search results.
	/// </summary>
	public IList<ProductItemInfo>? SearchResults {
		get => _searchResults;
		set => SetProperty(ref _searchResults, value);
	}

	/// <summary>
	/// The search text.
	/// </summary>
	public string SearchText {
		get => _searchText;
		set {
			if (SetProperty(ref _searchText, value))
				UpdateSearchResults();
		}
	}

	/// <summary>
	/// The <see cref="ICommand"/> that sets the application's theme.
	/// </summary>
	public ICommand SetApplicationThemeCommand {
		get => _setApplicationThemeCommand ??= new DelegateCommand<object>(
			param => {
				var themeName = param as string
					?? (param as FrameworkElement)?.Tag as string;

				if (!string.IsNullOrEmpty(themeName)) {
					ThemeManager.UnregisterAutomaticThemes();
					ThemeManager.CurrentTheme = themeName;
				}
			},
			param => {
				var themeName = param as string
					?? (param as FrameworkElement)?.Tag as string;

				if (!string.IsNullOrEmpty(themeName)) {
					if (param is MenuItem menuItem)
						menuItem.IsChecked = (ThemeManager.CurrentTheme == themeName);

					return true;
				}
				else
					return false;
			}
		);
	}

	/// <summary>
	/// The status message.
	/// </summary>
	public string? StatusMessage {
		get => _statusMessage;
		set => SetProperty(ref _statusMessage, value ?? ReadyMessage);
	}

	/// <summary>
	/// The C# syntax language for the code viewer.
	/// </summary>
	public ISyntaxLanguage SyntaxLanguageCSharp {
		get {
			if (_syntaxLanguageCSharp is null) {
				_syntaxLanguageCSharp = new CSharpSyntaxLanguage();

				var projectAssembly = new CSharpProjectAssembly("CodeViewer");
				_syntaxLanguageCSharp.RegisterService<IProjectAssembly>(projectAssembly);

				var assemblyLoader = new BackgroundWorker();
				assemblyLoader.DoWork += (sender, e) => {
					// Add some common assemblies for reflection (any custom assemblies could be added using various Add overloads instead)
					SyntaxEditorHelper.AddCommonDotNetSystemAssemblyReferences(projectAssembly);
				};
				assemblyLoader.RunWorkerAsync();
			}

			return _syntaxLanguageCSharp;
		}
	}

	/// <summary>
	/// The XAML syntax language for the code viewer.
	/// </summary>
	public ISyntaxLanguage SyntaxLanguageXaml {
		get {
			if (_syntaxLanguageXaml is null) {
				_syntaxLanguageXaml = new XmlSyntaxLanguage();
				SyntaxEditorHelper.InitializeLanguageFromResourceStream(_syntaxLanguageXaml, "Xaml.langdef");
			}

			return _syntaxLanguageXaml;
		}
	}

	/// <summary>
	/// The <see cref="ICommand"/> that toggles whether the theme should automatically change to match the system's light/dark setting.
	/// </summary>
	public ICommand ToggleAutomaticThemesCommand {
		get => _toggleAutomaticThemesCommand ??= new DelegateCommand<object>(_ => {
			if (ThemeManager.HasAutomaticThemes) {
				ThemeManager.UnregisterAutomaticThemes();
				ThemeManager.CurrentTheme = (ThemeManager.SystemApplicationMode == SystemApplicationMode.Light ? ThemeNames.Light : ThemeNames.Dark);
			}
			else
				ThemeManager.RegisterAutomaticThemes(ThemeNames.Light, ThemeNames.Dark, ThemeNames.HighContrast);

			UpdateThemeCommands();
		});
	}

	/// <summary>
	/// The <see cref="ICommand"/> that toggles whether the Backstage is open.
	/// </summary>
	public ICommand ToggleIsBackstageOpenCommand
		=> _toggleIsBackstageOpenCommand ??= new DelegateCommand<object>(_ => IsBackstageOpen = !IsBackstageOpen);

	/// <summary>
	/// The <see cref="ICommand"/> that toggles whether native control theming are enabled.
	/// </summary>
	public ICommand ToggleNativeThemesCommand {
		get => _toggleNativeThemesCommand ??= new DelegateCommand<object>(
			param => {
				ThemeManager.AreNativeThemesEnabled = !ThemeManager.AreNativeThemesEnabled;

				if (param is MenuItem menuItem)
					menuItem.IsChecked = ThemeManager.AreNativeThemesEnabled;
			},
			param => {
				if (param is MenuItem menuItem)
					menuItem.IsChecked = ThemeManager.AreNativeThemesEnabled;

				return true;
			}
		);
	}

	/// <summary>
	/// The <see cref="ICommand"/> that toggles whether the root window has a system backdrop enabled.
	/// </summary>
	public ICommand ToggleWindowBackdropCommand {
		get => _toggleWindowBackdropCommand ??= new DelegateCommand<object>(
			param => {
				if (
					App.Current.MainWindow is RootWindow window
					&& WindowChrome.GetChrome(window) is { } chrome
				) {
					chrome.BackdropKind = (chrome.BackdropKind == WindowChromeBackdropKind.None ? WindowChromeBackdropKind.MainWindow : WindowChromeBackdropKind.None);

					if (param is MenuItem menuItem)
						menuItem.IsChecked = (chrome.BackdropKind == WindowChromeBackdropKind.MainWindow);
				}
			},
			param => {
				if (!IsMainWindowSystemBackdropSupported)
					return false;

				if (
					App.Current.MainWindow is RootWindow window
					&& WindowChrome.GetChrome(window) is { } chrome
				) {
					if (param is MenuItem menuItem)
						menuItem.IsChecked = (chrome.BackdropKind == WindowChromeBackdropKind.MainWindow);
				}

				return true;
			}
		);
	}

	/// <summary>
	/// The <see cref="FrameworkElement"/> that renders the view's UI.
	/// </summary>
	public FrameworkElement? ViewElement {
		get => _viewElement;
		set {
			if (_viewElement != value) {
				// Notify any existing control that it is being unloaded
				(_viewElement as ProductItemControl)?.NotifyUnloaded();

				_viewElement = value;
				OnPropertyChanged(nameof(ViewElement));
			}
		}
	}

	/// <summary>
	/// Indicates whether the view has a custom statusbar.
	/// </summary>
	public bool ViewHasCustomStatusBar {
		get => _viewHasCustomStatusBar;
		set => SetProperty(ref _viewHasCustomStatusBar, value);
	}

	/// <summary>
	/// Indicates whether the view has any interop controls that may cause airspace issues with Backstage overlays.
	/// </summary>
	public bool ViewHasInterop {
		get => _viewHasInterop;
		set => SetProperty(ref _viewHasInterop, value);
	}

	/// <summary>
	/// Indicates whether the view has navigation buttons.
	/// </summary>
	public bool ViewHasNavigationButtons {
		get => _viewHasNavigationButtons;
		set => SetProperty(ref _viewHasNavigationButtons, value);
	}

	/// <summary>
	/// The view's image source.
	/// </summary>
	public ImageSource? ViewImageSource {
		get => _viewImageSource;
		set => SetProperty(ref _viewImageSource, value);
	}

	/// <summary>
	/// The <see cref="ProductItemInfo"/> currently in the view, if any.
	/// </summary>
	public ProductItemInfo? ViewItemInfo {
		get => _viewItemInfo;
		set => SetProperty(ref _viewItemInfo, value);
	}

	/// <summary>
	/// The view's sub-title.
	/// </summary>
	public string? ViewSubTitle {
		get => _viewSubTitle;
		set => SetProperty(ref _viewSubTitle, value);
	}

	/// <summary>
	/// The view's title.
	/// </summary>
	public string? ViewTitle {
		get => _viewTitle;
		set => SetProperty(ref _viewTitle, value);
	}

	/// <summary>
	/// The view's transition direction.
	/// </summary>
	public TransitionDirection ViewTransitionDirection {
		get => _viewTransitionDirection;
		set => SetProperty(ref _viewTransitionDirection, value);
	}

	#pragma warning disable CA1822 // Mark members as static
	/// <summary>
	/// The products URL.
	/// </summary>
	public string WpfProductsUrl
		=> "https://www.actiprosoftware.com/products/controls/wpf";
	#pragma warning restore CA1822

	#pragma warning disable CA1822 // Mark members as static
	/// <summary>
	/// The purchase licenses URL.
	/// </summary>
	public string WpfPurchaseLicensesUrl
		=> "https://www.actiprosoftware.com/purchase/pricing/controls/wpf";
	#pragma warning restore CA1822

}
