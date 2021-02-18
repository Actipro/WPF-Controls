using System;

#if WINRT
using ActiproSoftware.UI.Xaml.Controls.Docking;
#else
using ActiproSoftware.Windows.Controls.Docking;
#endif

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.CustomDockingWindows {

	/// <summary>
	/// Represents a custom <see cref="DocumentWindow"/> implementation.
	/// </summary>
	public partial class CustomDocumentWindow : DocumentWindow {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>CustomDocumentWindow</c> class.
		/// </summary>
		public CustomDocumentWindow() {
			InitializeComponent();
		}
	}
}