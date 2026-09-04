using ActiproSoftware.Windows.Controls.Wizard;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.WizardSamples.QuickStart.CustomPageClasses;

/// <summary>
/// Represents the start page.
/// </summary>
public class StartPage : WizardPage {

	/// <inheritdoc/>
	protected override void OnUnselecting(WizardSelectedPageChangeEventArgs e) {
		base.OnUnselecting(e);

		// Get the repeat count
		var repeatCountTextbox = (TextBox)FindName("PART_RepeatCountTextBox");
		if (repeatCountTextbox is not null) {
			if ((int.TryParse(repeatCountTextbox.Text, out var repeatCount)) && (repeatCount > 0) && (repeatCount <= 10)) {
				// Place an ItemStore in the Wizard's Tag
				if (Wizard is { } wizard) {
					var store = wizard.Tag as ItemStore;
					if ((store is null) || (store.Items.Count != repeatCount)) {
						store = new ItemStore();
						for (var index = 0; index < repeatCount; index++) {
							var item = new Item {
								Index = index + 1
							};
							store.Items.Add(item);
						}
						wizard.Tag = store;
					}
				}
				return;
			}

			MessageBox.Show("Please enter a number between 1 and 10.");
		}

		e.Handled = true;
		e.Cancel = true;
	}

}
