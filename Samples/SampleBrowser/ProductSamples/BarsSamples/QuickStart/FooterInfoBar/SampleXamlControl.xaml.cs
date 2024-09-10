using ActiproSoftware.Windows.Input;
using System;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.FooterInfoBar {

	/// <summary>
	/// Provides the user control for this sample that uses a XAML-based ribbon configuration.
	/// </summary>
	public partial class SampleXamlControl : SampleControlBase {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="SampleXamlControl"/> class.
		/// </summary>
		public SampleXamlControl() {
			InitializeComponent();

			// Configure this code-behind to be the view model for this sample
			this.DataContext = this;

			// IMPORTANT: This sample uses a RibbonViewModel to help pre-populate the ribbon controls,
			// and this will, by default, bind Ribbon.ClearFooterCommand to the RibbonViewModel.ClearFooterCommand.
			// Since this sample is not using MVVM footers, restore the default XAML-friendly command logic by setting
			// Ribbon.ClearFooterCommand to null.
			//
			// This step is ONLY necessary if RibbonViewModel is used and the the Ribbon.Footer is defined in XAML.
			this.Dispatcher.BeginInvoke((Action)(() => ribbon.ClearFooterCommand = null), System.Windows.Threading.DispatcherPriority.Input);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Shows the footer.
		/// </summary>
		private void ShowFooter() {
			// When the footer is closed the FooterContent is cleared.
			// Show the footer again by re-assigning the original RibbonFooterControl to the FooterContent.
			ribbon.FooterContent = footer;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc/>
		protected override void OnOptionsPropertyValueChanged(OptionsViewModel oldValue, OptionsViewModel newValue) {
			// Configure the command to show the XAML-based footer
			if (newValue is not null)
				newValue.ShowFooterXamlCommand = new DelegateCommand<object>(_ => ShowFooter());

			base.OnOptionsPropertyValueChanged(oldValue, newValue);
		}

	}

}
