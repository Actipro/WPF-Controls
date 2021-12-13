using System;
using System.Windows;
using System.Windows.Controls;
using ActiproSoftware.Windows.Controls.Wizard;

#if WPF
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;
#endif

namespace ActiproSoftware.ProductSamples.WizardSamples.QuickStart.CustomPageClasses {

	/// <summary>
	/// Represents the start page.
	/// </summary>
	public class StartPage : WizardPage {

		/// <summary>
		/// Raises the <see cref="UnselectingEvent"/> event. 
		/// </summary>
		/// <param name="e">A <see cref="WizardSelectedPageChangeEventArgs"/> that contains the event data.</param>
		protected override void OnUnselecting(WizardSelectedPageChangeEventArgs e) {
			// Call the base method
			base.OnUnselecting(e);

			// Get the repeat count
			TextBox repeatCountTextbox = (TextBox)this.FindName("PART_RepeatCountTextBox");
			if (repeatCountTextbox != null) {
				int repeatCount;
				if ((int.TryParse(repeatCountTextbox.Text, out repeatCount)) && (repeatCount > 0) && (repeatCount <= 10)) {
					// Place an ItemStore in the Wizard's Tag 
					ItemStore store = (ItemStore)this.Wizard.Tag;
					if ((store == null) || (store.Items.Count != repeatCount)) {
						store = new ItemStore();
						for (int index = 0; index < repeatCount; index++) {
							Item item = new Item();
							item.Index = index + 1;
							store.Items.Add(item);
						}
						this.Wizard.Tag = store;
					}
					return;
				}

				MessageBox.Show("Please enter a number between 1 and 10.");
			}
			
			e.Handled = true;
			e.Cancel = true;
		}

	}

}
