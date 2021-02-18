using System;
using ActiproSoftware.Windows.Themes;

namespace ActiproSoftware.ProductSamples.ThemesSamples.QuickStart.ThemeOverride {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			// Register the various theme overrides... this code would normally be in the app startup logic where ThemeManager is configured
            ThemeManager.RegisterThemeCatalog("Custom", new CustomThemeCatalog());

			InitializeComponent();
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Notifies the UI that it has been unloaded.
		/// </summary>
		public override void NotifyUnloaded() {
			// Unregister the changes this sample makes
			ThemeManager.UnregisterThemeCatalog("Custom");
		}
		
	}

}