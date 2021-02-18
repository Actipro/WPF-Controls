using ActiproSoftware.Windows.Controls.Views;
using ActiproSoftware.Windows.Media;
using System.Windows.Controls;

namespace ActiproSoftware.ProductSamples.ViewsSamples.QuickStart.MultiColumnPanelIntro {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="MainControl"/> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the configurable panel.
		/// </summary>
		/// <value>The configurable panel.</value>
		public Panel ConfigurablePanel {
			get {
				var panel = VisualTreeHelperExtended.GetFirstDescendant(peopleItemsControl, typeof(MultiColumnPanel)) as MultiColumnPanel;
				return panel;
			}
		}

	}

}
