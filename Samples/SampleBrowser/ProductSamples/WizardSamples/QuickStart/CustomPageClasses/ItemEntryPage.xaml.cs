using System;
using System.Windows;
using System.Windows.Controls;
using ActiproSoftware.Windows.Controls.Wizard;

namespace ActiproSoftware.ProductSamples.WizardSamples.QuickStart.CustomPageClasses {

	/// <summary>
	/// Represents an item entry page.
	/// </summary>
	public partial class ItemEntryPage {
		
		/// <summary>
		/// Initializes a new instance of the <c>ItemEntryPage</c> class.
		/// </summary>
		public ItemEntryPage() {
			// This call to InitializeComponent must be here or your XAML code will not be applied
			this.InitializeComponent();
		}

		/// <summary>
		/// Initializes the page.
		/// </summary>
		private void InitializePage() {
			ItemStore store = (ItemStore)this.Wizard.Tag;

			// Update the caption and content of the page (content will be the item being edited)
			this.Caption = String.Format("Item #{0} Details", store.CurrentIndex + 1);
			this.Content = store.Items[store.CurrentIndex];
		}
		
		/// <summary>
		/// Raises the <see cref="SelectingEvent"/> event. 
		/// </summary>
		/// <param name="e">A <see cref="WizardSelectedPageChangeEventArgs"/> that contains the event data.</param>
		protected override void OnSelecting(WizardSelectedPageChangeEventArgs e) {
			// Call the base method
			base.OnSelecting(e);

			// Initialize the page
			this.InitializePage();
		}

		/// <summary>
		/// Raises the <see cref="SelectingEvent"/> event. 
		/// </summary>
		/// <param name="e">A <see cref="WizardSelectedPageChangeEventArgs"/> that contains the event data.</param>
		protected override void OnUnselecting(WizardSelectedPageChangeEventArgs e) {
			// Call the base method
			base.OnUnselecting(e);

			if (!e.Handled) {
				bool isForwardProgress = ((e.SelectionFlags & WizardPageSelectionFlags.ForwardProgress) == WizardPageSelectionFlags.ForwardProgress);
				bool isBackwardProgress = ((e.SelectionFlags & WizardPageSelectionFlags.BackwardProgress) == WizardPageSelectionFlags.BackwardProgress);

				ItemStore store = (ItemStore)this.Wizard.Tag;
				if (
					((isForwardProgress) && (store.CurrentIndex < store.Items.Count - 1)) ||
					((isBackwardProgress) && (store.CurrentIndex > 0))
					) {
					// Cancel the page change
					e.Handled = true;
					e.Cancel = true;

					// Update the current item index and reinit the page
					store.CurrentIndex = store.CurrentIndex + (isForwardProgress ? 1 : -1);
					this.InitializePage();					
				}
			}
		}

	}

}
