using System;
using System.Windows.Controls;
using ActiproSoftware.ProductSamples.EditorsSamples.Common;
using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.Controls.Editors.Primitives;

namespace ActiproSoftware.ProductSamples.EditorsSamples.QuickStart.EnumEditBoxIntro {

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

			this.EnumWithFlagsType = typeof(EnumWithFlags);
			this.DataContext = this;

			sortedEditBox.EnumSortComparer = EnumValueNameSortComparer.Instance;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when the selection is changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="SelectionChangedEventArgs"/> that contains data related to the event.</param>
		private void OnSortComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e) {
			var sortComboBox = (ComboBox)sender;
			switch (sortComboBox.SelectedIndex) {
				case 1:
					editBox.EnumSortComparer = EnumValueNameSortComparer.Instance;
					break;
				default:
					editBox.EnumSortComparer = null;
					break;
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the current value.
		/// </summary>
		/// <value>The current value.</value>
		public EnumWithFlags EnumWithFlagsCurrentValue { get; set; }
	
		/// <summary>
		/// Gets or sets the current value.
		/// </summary>
		/// <value>The current value.</value>
		public Type EnumWithFlagsType { get; set; }
	
		/// <summary>
		/// Gets or sets the current value.
		/// </summary>
		/// <value>The current value.</value>
		public EnumWithoutFlags EnumWithoutFlagsCurrentValue { get; set; }
	
	}
}