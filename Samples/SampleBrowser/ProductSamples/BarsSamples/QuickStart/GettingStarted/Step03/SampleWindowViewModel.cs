/*

RIBBON GETTING STARTED SERIES - STEP 3

STEP SUMMARY:

	The SampleBarManager created during this step is the central point of access to the
	control view models that will be used by the Ribbon, so this view model will also need
	access to SampleBarManager when defining its own view models specific to the Ribbon
	that is associated with this model.

CHANGES SINCE LAST STEP:

	Added a BarManager property initialized to an instance of the new SampleBarManager class.

	Prior sample comments were removed/condensed.

*/

using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.Input;
using System;
using System.Windows;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.GettingStarted.Step03 {

	/// <summary>
	/// Defines the view model for this sample.
	/// </summary>
	public class SampleWindowViewModel : ObservableObjectBase {

		private ICommand helpCommand;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="SampleWindowViewModel"/> class.
		/// </summary>
		public SampleWindowViewModel() {

			// Initialize the view model for the Ribbon
			this.Ribbon = new RibbonViewModel() {
				QuickAccessToolBarMode = RibbonQuickAccessToolBarMode.None,
			};
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		//	SAMPLE NOTE 3.1:
		//		The SampleBarManager will be used to access view models for individual controls.
		//		While not used in this step, a reference will be made to the manager class for
		//		use by future steps.

		/// <summary>
		/// Gets the manager for working with the objects used by Ribbon and related menus.
		/// </summary>
		private SampleBarManager BarManager { get; } = new SampleBarManager();

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the command for displaying Help.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand HelpCommand {
			get {
				if (helpCommand is null) {
					helpCommand = new DelegateCommand<object>(
						param => {
							// Execute
							ThemedMessageBox.Show("This is where contextual Help would be displayed.", "Help", MessageBoxButton.OK, MessageBoxImage.Information);
						});
				}
				return helpCommand;
			}
		}

		/// <summary>
		/// Gets the view model for the ribbon control.
		/// </summary>
		/// <value>A <see cref="RibbonViewModel"/>.</value>
		public RibbonViewModel Ribbon { get; }

	}

}
