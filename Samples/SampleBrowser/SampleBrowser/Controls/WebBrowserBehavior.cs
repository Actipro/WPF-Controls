using ActiproSoftware.Windows.Extensions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Navigation;

namespace ActiproSoftware.SampleBrowser;

/// <summary>
/// Provides behaviors for the <see cref="WebBrowser"/> control.
/// </summary>
public class WebBrowserBehavior {

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="AreScriptErrorsDisabled"/> property.
	/// </summary>
	public static readonly DependencyProperty AreScriptErrorsDisabledProperty
		= DependencyProperty.RegisterAttached("AreScriptErrorsDisabled", typeof(bool), typeof(WebBrowserBehavior), new FrameworkPropertyMetadata(defaultValue: false, OnAreScriptErrorsDisabledPropertyValueChanged));

	/// <summary>
	/// Defines the <see cref="WebBrowserBehaviorContext"/> property.
	/// </summary>
	private static readonly DependencyProperty WebBrowserBehaviorContextProperty
		= DependencyProperty.RegisterAttached("WebBrowserBehaviorContext", typeof(BehaviorContext), typeof(WebBrowserBehavior), new FrameworkPropertyMetadata(defaultValue: null, OnWebBrowserBehaviorContextPropertyValueChanged));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// NESTED TYPES
	// --------------------------------------------------------------------------------------------------

	#region BehaviorContext

	/// <summary>
	/// Stores context information.
	/// </summary>
	private class BehaviorContext : DisposableObjectBase {

		private WebBrowser? _browser;

		// --------------------------------------------------------------------------------------------------
		// OBJECT
		// --------------------------------------------------------------------------------------------------

		/// <summary>
		/// Initializes an instance of the class.
		/// </summary>
		public BehaviorContext(WebBrowser browser) {
			_browser = browser ?? throw new ArgumentNullException(nameof(browser));

			browser.Navigated += OnWebBrowserNavigated;
		}

		/// <inheritdoc/>
		protected override void Dispose(bool disposing) {
			// Detach the event
			if (_browser is not null)
				_browser.Navigated -= OnWebBrowserNavigated;
			_browser = null;
		}

		// --------------------------------------------------------------------------------------------------
		// PUBLIC PROCEDURES
		// --------------------------------------------------------------------------------------------------

		private void OnWebBrowserNavigated(object sender, NavigationEventArgs e)
			=> SetSilentMode();

		private void SetSilentMode() {
			if (_browser is not null) {
				// Set the IWebBrowser2.Silent property
				if (_browser.Document is IServiceProvider serviceProvider) {
					var SID_SWebBrowserApp = new Guid("0002DF05-0000-0000-C000-000000000046");
					var IID_IWebBrowser2 = new Guid("D30C1661-CDAF-11d0-8A3E-00C04FC9E26E");
					var webBrowser2 = serviceProvider.QueryService(ref SID_SWebBrowserApp, ref IID_IWebBrowser2);
					webBrowser2?.GetType().InvokeMember("Silent", BindingFlags.Instance | BindingFlags.Public | BindingFlags.PutDispProperty, null, webBrowser2, new object[] { true });
				}

				// Remove the context
				SetWebBrowserBehaviorContext(_browser, null);
			}
		}

	}

	#endregion

	#region IServiceProvider

	/// <summary>
	/// Provides the COM interface for <c>IServiceProvider</c>.
	/// </summary>
	[ComImport, Guid("6D5140C1-7436-11CE-8034-00AA006009FA"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	private interface IServiceProvider {

		[return: MarshalAs(UnmanagedType.IUnknown)]
		object QueryService(ref Guid serviceGuid, ref Guid riid);

	}

	#endregion

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Gets the value of the <c>WebBrowserBehaviorContext</c> attached property for the specified object.
	/// </summary>
	/// <param name="obj">The object from which the property value is read.</param>
	[AttachedPropertyBrowsableForType(typeof(WebBrowser))]
	private static BehaviorContext GetWebBrowserBehaviorContext(DependencyObject obj)
		=> (BehaviorContext)obj.GetValue(WebBrowserBehaviorContextProperty);

	/// <summary>
	/// Sets the value of the <c>WebBrowserBehaviorContext</c> attached property to the specified object.
	/// </summary>
	/// <param name="obj">The object from which the property value is read.</param>
	/// <param name="value">The value to set.</param>
	private static void SetWebBrowserBehaviorContext(DependencyObject obj, BehaviorContext? value)
		=> obj.SetValue(WebBrowserBehaviorContextProperty, value);

	private static void OnAreScriptErrorsDisabledPropertyValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) {
		if (sender is WebBrowser browser) {
			var oldContext = GetWebBrowserBehaviorContext(browser);
			if (oldContext is not null)
				SetWebBrowserBehaviorContext(browser, null);

			if (e.GetNewValue<bool>())
				SetWebBrowserBehaviorContext(browser, new BehaviorContext(browser));
		}
	}

	private static void OnWebBrowserBehaviorContextPropertyValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		=> (e.OldValue as BehaviorContext)?.Dispose();

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Gets the value of the <c>AreScriptErrorsDisabled</c> attached property for the specified object.
	/// </summary>
	/// <param name="obj">The object from which the property value is read.</param>
	[AttachedPropertyBrowsableForType(typeof(WebBrowser))]
	public static bool GetAreScriptErrorsDisabled(DependencyObject obj)
		=> (bool)obj.GetValue(AreScriptErrorsDisabledProperty);

	/// <summary>
	/// Sets the value of the <c>AreScriptErrorsDisabled</c> attached property to the specified object.
	/// </summary>
	/// <param name="obj">The object from which the property value is read.</param>
	/// <param name="value">The value to set.</param>
	public static void SetAreScriptErrorsDisabled(DependencyObject obj, bool value)
		=> obj.SetValue(AreScriptErrorsDisabledProperty, value);

}
