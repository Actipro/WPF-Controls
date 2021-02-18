using System;

#if WINRT
using ActiproSoftware.UI.Xaml.Controls.Docking;
#else
using ActiproSoftware.Windows.Controls.Docking;
#endif

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.LayoutSerialization {

	/// <summary>
	/// Represents a custom <see cref="ToolWindow"/> implementation.
	/// </summary>
	public partial class CustomToolWindow : ToolWindow  {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>CustomToolWindow</c> class.
		/// </summary>
		public CustomToolWindow() {
			InitializeComponent();
		}
	}
}