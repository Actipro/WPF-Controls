using System;
using System.Windows.Controls;
using ActiproSoftware.ProductSamples.GridsSamples.Common;
using ActiproSoftware.Windows.Controls.Grids;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.TreeListBoxSelection {

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
		/// Occurs before an item is selected.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>RoutedEventArgs</c> that contains data related to this event.</param>
		private void OnTreeListBoxItemSelecting(object sender, TreeListBoxItemEventArgs e) {
			if (true.Equals(this.CanBlockNames)) {
				var model = e.Item as TreeNodeModel;
				if ((model != null) && (!string.IsNullOrEmpty(model.Name)))
					e.Cancel = ("ABCDE".IndexOf(model.Name[0].ToString(), StringComparison.OrdinalIgnoreCase) != -1);
			}
		}

		/// <summary>
		/// Occurs when the selection has changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>SelectionChangedEventArgs</c> that contains data related to this event.</param> 
		private void OnTreeListBoxSelectionChanged(object sender, SelectionChangedEventArgs e) {
			selectedItemTextBlock.Text = String.Format("Selected item ({0} added, {1} removed, updated {2}):",
				e.AddedItems.Count, e.RemovedItems.Count, DateTime.Now.ToLongTimeString());
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets whether certain names can be blocked.
		/// </summary>
		/// <value>
		/// <c>true</c> if certain names can be blocked; otherwise, <c>false</c>.
		/// </value>
		public bool CanBlockNames { get; set; }

	}

}
