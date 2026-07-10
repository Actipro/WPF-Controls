using ActiproSoftware.Windows.Controls.Wizard;

namespace ActiproSoftware.ProductSamples.WizardSamples.QuickStart.CustomPageClasses;

/// <summary>
/// Represents an item entry page.
/// </summary>
public partial class ItemEntryPage {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public ItemEntryPage() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the page.
	/// </summary>
	private void InitializePage() {
		var store = (ItemStore)Wizard!.Tag;

		// Update the caption and content of the page (content will be the item being edited)
		Caption = string.Format("Item #{0} Details", store.CurrentIndex + 1);
		Content = store.Items[store.CurrentIndex];
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void OnSelecting(WizardSelectedPageChangeEventArgs e) {
		base.OnSelecting(e);

		// Initialize the page
		InitializePage();
	}

	/// <inheritdoc/>
	protected override void OnUnselecting(WizardSelectedPageChangeEventArgs e) {
		base.OnUnselecting(e);

		if (!e.Handled) {
			bool isForwardProgress = ((e.SelectionFlags & WizardPageSelectionFlags.ForwardProgress) == WizardPageSelectionFlags.ForwardProgress);
			bool isBackwardProgress = ((e.SelectionFlags & WizardPageSelectionFlags.BackwardProgress) == WizardPageSelectionFlags.BackwardProgress);

			var store = (ItemStore)Wizard!.Tag;
			if (
				(isForwardProgress && (store.CurrentIndex < store.Items.Count - 1))
				|| (isBackwardProgress && (store.CurrentIndex > 0))
			) {
				// Cancel the page change
				e.Handled = true;
				e.Cancel = true;

				// Update the current item index and re-initialize the page
				store.CurrentIndex = store.CurrentIndex + (isForwardProgress ? 1 : -1);
				InitializePage();
			}
		}
	}

}
