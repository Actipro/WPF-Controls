#if WINRT
using ActiproSoftware.UI.Xaml.Controls.Docking;
#else
using ActiproSoftware.Windows.Controls.Docking;
#endif

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.CustomDockingWindows {

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
			InitializeComponent();
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets whether to use tabbed MDI.
		/// </summary>
		/// <value>
		/// <c>true</c> if tabbed MDI should be used; otherwise, <c>false</c>.
		/// </value>
		public bool UseTabbedMdi {
			get {
				return (dockSite.MdiKind == MdiKind.Tabbed);
			}
			set {
				if (value)
					dockSite.MdiKind = MdiKind.Tabbed;
				else
					dockSite.MdiKind = MdiKind.Standard;
			}
		}

	}

}
