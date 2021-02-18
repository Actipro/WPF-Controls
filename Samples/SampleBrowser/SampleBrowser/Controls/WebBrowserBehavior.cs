using ActiproSoftware.Windows;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Provides behaviors for the <see cref="WebBrowser"/> control.
	/// </summary>
	public class WebBrowserBehavior {

		#region Dependency Properties

		/// <summary>
		/// Identifies the <see cref="AreScriptErrorsDisabled"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="AreScriptErrorsDisabled"/> dependency property.</value>
		public static readonly DependencyProperty AreScriptErrorsDisabledProperty = DependencyProperty.RegisterAttached("AreScriptErrorsDisabled", typeof(bool), typeof(WebBrowserBehavior), new FrameworkPropertyMetadata(false, OnAreScriptErrorsDisabledPropertyValueChanged));

		/// <summary>
		/// Identifies the <see cref="WebBrowserBehaviorContext"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="WebBrowserBehaviorContext"/> dependency property.</value>
		private static readonly DependencyProperty WebBrowserBehaviorContextProperty = DependencyProperty.RegisterAttached("WebBrowserBehaviorContext", typeof(BehaviorContext), typeof(WebBrowserBehavior), new FrameworkPropertyMetadata(null, OnWebBrowserBehaviorContextPropertyValueChanged));

		#endregion // Dependency Properties

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NESTED TYPES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		#region BehaviorContext

		/// <summary>
		/// Stores context information.
		/// </summary>
		private class BehaviorContext : DisposableObjectBase {

			private WebBrowser browser;

			/////////////////////////////////////////////////////////////////////////////////////////////////////
			// OBJECT
			/////////////////////////////////////////////////////////////////////////////////////////////////////

			/// <summary>
			/// Initializes an instance of the <c>BehaviorContext</c> class.
			/// </summary>
			/// <param name="browser">The target <see cref="WebBrowser"/>.</param>
			public BehaviorContext(WebBrowser browser) {
				if (browser == null)
					throw new ArgumentNullException(nameof(browser));

				this.browser = browser;

				browser.Navigated += this.OnWebBrowserNavigated;
			}

			/////////////////////////////////////////////////////////////////////////////////////////////////////
			// PUBLIC PROCEDURES
			/////////////////////////////////////////////////////////////////////////////////////////////////////

			/// <summary>
			/// Occurs when the browser is navigated.
			/// </summary>
			/// <param name="sender">The sender of the event.</param>
			/// <param name="e">The <see cref="NavigationEventArgs"/> that contains the event data.</param>
			private void OnWebBrowserNavigated(object sender, NavigationEventArgs e) {
				this.SetSilentMode();
			}

			/// <summary>
			/// Sets silent mode.
			/// </summary>
			private void SetSilentMode() {
				if (browser != null) {
					// Set the IWebBrowser2.Silent property
					var serviceProvider = browser.Document as IServiceProvider;
					if (serviceProvider != null) {
						var SID_SWebBrowserApp = new Guid("0002DF05-0000-0000-C000-000000000046");
						var IID_IWebBrowser2 = new Guid("D30C1661-CDAF-11d0-8A3E-00C04FC9E26E");
						var webBrowser2 = serviceProvider.QueryService(ref SID_SWebBrowserApp, ref IID_IWebBrowser2);
						if (webBrowser2 != null)
							webBrowser2.GetType().InvokeMember("Silent", BindingFlags.Instance | BindingFlags.Public | BindingFlags.PutDispProperty, null, webBrowser2, new object[] { true });
					}

					// Remove the context
					SetWebBrowserBehaviorContext(browser, null);
				}
			}

			/////////////////////////////////////////////////////////////////////////////////////////////////////
			// PUBLIC PROCEDURES
			/////////////////////////////////////////////////////////////////////////////////////////////////////

			/// <summary>
			/// Releases the unmanaged resources used by the object and optionally releases the managed resources.
			/// </summary>
			/// <param name="disposing">
			/// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources. 
			/// </param>
			/// <remarks>
			/// This method is called by the public <c>Dispose</c> method and the <c>Finalize</c> method. 
			/// <c>Dispose</c> invokes this method with the <paramref name="disposing"/> parameter set to <c>true</c>. 
			/// <c>Finalize</c> invokes this method with <paramref name="disposing"/> set to <c>false</c>.
			/// </remarks>
			protected override void Dispose(bool disposing) {
				// Detach the event
				if (browser != null) {
					browser.Navigated -= this.OnWebBrowserNavigated;
					browser = null;
				}

				// Call the base method
				base.Dispose(disposing);
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

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the value of the <c>WebBrowserBehaviorContext</c> attached property for the specified object.
		/// </summary>
		/// <param name="obj">The object from which the property value is read.</param>
		/// <returns>The object's value.</returns>
		[AttachedPropertyBrowsableForType(typeof(WebBrowser))]
		private static BehaviorContext GetWebBrowserBehaviorContext(DependencyObject obj) {
			if (obj == null)
				throw new ArgumentNullException("obj");
			return (BehaviorContext)obj.GetValue(WebBrowserBehaviorContextProperty);
		}
		/// <summary>
		/// Sets the value of the <c>WebBrowserBehaviorContext</c> attached property to the specified object. 
		/// </summary>
		/// <param name="obj">The object from which the property value is read.</param>
		/// <param name="value">The value to set.</param>
		private static void SetWebBrowserBehaviorContext(DependencyObject obj, BehaviorContext value) {
			if (obj == null)
				throw new ArgumentNullException("obj");
			obj.SetValue(WebBrowserBehaviorContextProperty, value);
		}

		/// <summary>
		/// Occurs when the property value has changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> containing data related to this event.</param>
		private static void OnAreScriptErrorsDisabledPropertyValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) {
			var browser = sender as WebBrowser;
			if (browser != null) {
				var oldContext = GetWebBrowserBehaviorContext(browser);
				if (oldContext != null)
					SetWebBrowserBehaviorContext(browser, null);

				if (true.Equals(e.NewValue))
					SetWebBrowserBehaviorContext(browser, new BehaviorContext(browser));
			}
		}

		/// <summary>
		/// Occurs when the property value has changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> containing data related to this event.</param>
		private static void OnWebBrowserBehaviorContextPropertyValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) {
			var oldContext = e.OldValue as BehaviorContext;
			if (oldContext != null)
				oldContext.Dispose();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the value of the <c>AreScriptErrorsDisabled</c> attached property for the specified object.
		/// </summary>
		/// <param name="obj">The object from which the property value is read.</param>
		/// <returns>The object's value.</returns>
		[AttachedPropertyBrowsableForType(typeof(WebBrowser))]
		public static bool GetAreScriptErrorsDisabled(DependencyObject obj) {
			if (obj == null)
				throw new ArgumentNullException("obj");
			return (bool)obj.GetValue(AreScriptErrorsDisabledProperty);
		}
		/// <summary>
		/// Sets the value of the <c>AreScriptErrorsDisabled</c> attached property to the specified object. 
		/// </summary>
		/// <param name="obj">The object from which the property value is read.</param>
		/// <param name="value">The value to set.</param>
		public static void SetAreScriptErrorsDisabled(DependencyObject obj, bool value) {
			if (obj == null)
				throw new ArgumentNullException("obj");
			obj.SetValue(AreScriptErrorsDisabledProperty, value);
		}

	}

}
