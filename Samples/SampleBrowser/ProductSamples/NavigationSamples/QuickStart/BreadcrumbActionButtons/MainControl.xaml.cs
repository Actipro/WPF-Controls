using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Threading;

using ActiproSoftware.Windows.Controls.Navigation;
using ActiproSoftware.ProductSamples.NavigationSamples.Common.Breadcrumb.ShellItem;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.BreadcrumbActionButtons {

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
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Handles the ConvertItem event of the breadcrumb control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="ActiproSoftware.Windows.Controls.Navigation.BreadcrumbConvertItemEventArgs"/> instance containing the event data.</param>
		private void OnBreadcrumbConvertItem(object sender, BreadcrumbConvertItemEventArgs e) {
			ConvertItemHelper.HandleConvertItem(sender, e);
		}
	}
}