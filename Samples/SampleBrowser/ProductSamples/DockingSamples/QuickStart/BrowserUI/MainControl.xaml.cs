using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.Controls.Docking;
using System.Windows.Navigation;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.BrowserUI;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	private int _browserIndex = 0;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		CreateBrowserWindow("http://www.bing.com");
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates a new web browser <see cref="DocumentWindow"/>.
	/// </summary>
	/// <param name="url">The URL to load.</param>
	private DocumentWindow CreateBrowserWindow(string url) {
		var browser = new WebBrowser();
		InteropFocusTracking.SetIsEnabled(browser, true);
		WebBrowserBehavior.SetAreScriptErrorsDisabled(browser, true);

		// Create the document
		var documentWindow = new DocumentWindow(dockSite, serializationId: "Browser" + (++_browserIndex), title: "New Tab", content: browser);

		// Activate the document
		documentWindow.Activate();

		// Navigate to a page
		browser.Navigate(url);

		return documentWindow;
	}

	/// <summary>
	/// Occurs when the browser completes page loading.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnBrowserLoadCompleted(object sender, NavigationEventArgs e) {
		var browser = (WebBrowser)sender;
		UpdateUrlAndTitle(browser);
	}

	/// <summary>
	/// Occurs when the browser starts navigation.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnBrowserNavigated(object sender, NavigationEventArgs e) {
		var browser = (WebBrowser)sender;
		UpdateUrlAndTitle(browser);
	}

	/// <summary>
	/// Occurs when a new window is requested by the user.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnDockSiteNewWindowRequested(object sender, RoutedEventArgs e)
		=> CreateBrowserWindow("about:blank");

	/// <summary>
	/// Occurs when a docking window is activated.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnDockSiteWindowActivated(object sender, DockingWindowEventArgs e) {
		if (e.Window?.Content is WebBrowser browser)
			UpdateUrlAndTitle(browser);
	}

	/// <summary>
	/// Occurs when a docking window is registered.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnDockSiteWindowRegistered(object sender, DockingWindowEventArgs e) {
		if (e.Window?.Content is WebBrowser browser) {
			browser.LoadCompleted += OnBrowserLoadCompleted;
			browser.Navigated += OnBrowserNavigated;
		}
	}

	/// <summary>
	/// Occurs when a docking window is registered.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnDockSiteWindowUnregistered(object sender, DockingWindowEventArgs e) {
		if (e.Window?.Content is WebBrowser browser) {
			browser.LoadCompleted -= OnBrowserLoadCompleted;
			browser.Navigated -= OnBrowserNavigated;
		}

		// Ensure there is always at least one browser tab
		if (dockSite.DocumentWindows.Count == 0)
			CreateBrowserWindow("about:blank");
	}

	/// <summary>
	/// Occurs when the button is clicked.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnGoButtonClick(object sender, RoutedEventArgs e)
		=> NavigateTo(urlTextBox.Text.Trim());

	/// <summary>
	/// Occurs when a key is pressed.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnUrlTextBoxKeyDown(object sender, KeyEventArgs e) {
		switch (e.Key) {
			case Key.Enter:
				NavigateTo(urlTextBox.Text.Trim());
				break;
		}
	}

	/// <summary>
	/// Navigates to the specified URL.
	/// </summary>
	/// <param name="url">The URL to navigate to.</param>
	private void NavigateTo(string url) {
		if (dockSite.PrimaryDocument is { } activeWindow) {
			var browser = (WebBrowser)activeWindow.Content;
			try {
				if (
					!url.StartsWith("http://", StringComparison.OrdinalIgnoreCase)
					&& !url.StartsWith("https://", StringComparison.OrdinalIgnoreCase)
					&& !url.StartsWith("about:", StringComparison.OrdinalIgnoreCase)
				) {
					url = "http://" + url;
				}

				var uri = new Uri(url, UriKind.Absolute);
				browser.Navigate(uri.AbsoluteUri);
			}
			catch {
				MessageBox.Show("Please enter an absolute URL: " + url, "Browser");
			}
		}
	}

	/// <summary>
	/// Updates the URL and title from the specified web browser.
	/// </summary>
	/// <param name="browser">The <see cref="WebBrowser"/> to examine.</param>
	private void UpdateUrlAndTitle(WebBrowser browser) {
		try {
			urlTextBox.Text = browser.Source?.ToString() ?? string.Empty;
			var window = (DocumentWindow)browser.Parent;
			window.Title = urlTextBox.Text;
			window.FileName = urlTextBox.Text;
		}
		catch { }
	}

}
