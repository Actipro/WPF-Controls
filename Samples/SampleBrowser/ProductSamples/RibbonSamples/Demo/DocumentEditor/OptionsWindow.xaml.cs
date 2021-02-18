using System;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using RibbonControls = ActiproSoftware.Windows.Controls.Ribbon.Controls;
using ActiproSoftware.Windows.Controls.Ribbon.Customization;
using ActiproSoftware.Windows.Controls.Ribbon.UI;

namespace ActiproSoftware.ProductSamples.RibbonSamples.Demo.DocumentEditor {

	/// <summary>
	/// Provides the options window for this sample.
	/// </summary>
	public partial class OptionsWindow : Window {

		#region Dependency Properties
		
		/// <summary>
		/// Identifies the <see cref="Ribbon"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="Ribbon"/> dependency property.</value>
		public static readonly DependencyProperty RibbonProperty = DependencyProperty.Register("Ribbon", typeof(ActiproSoftware.Windows.Controls.Ribbon.Ribbon), typeof(OptionsWindow), new FrameworkPropertyMetadata(null));
		
		#endregion

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>OptionsWindow</c> class.
		/// </summary>
		/// <param name="ribbon">The ribbon being customized.</param>
		public OptionsWindow(ActiproSoftware.Windows.Controls.Ribbon.Ribbon ribbon) {
			this.Ribbon = ribbon;

			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnOKButtonClick(object sender, RoutedEventArgs e) {
			// Save changes to QAT customization
			customizeQat.Save();

			this.DialogResult = true;
			this.Close();
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when the window is closing.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
		/// <param name="e"></param>
		protected override void OnClosing(CancelEventArgs e) {
			if (this.DialogResult != true) {
				// Cancel changes from QAT customization
				customizeQat.Cancel();
			}

			// Call the base method
			base.OnClosing(e);
		}

		/// <summary>
		/// Occurs after the control has been initialized.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
		protected override void OnInitialized(EventArgs e) {
			// Call the base method
			base.OnInitialized(e);

			// Assign the ribbon
			customizeQat.Ribbon = this.Ribbon;
		}

		/// <summary>
		/// Gets or sets the ribbon that is being customized.
		/// </summary>
		/// <value>The ribbon that is being customized.</value>
		public ActiproSoftware.Windows.Controls.Ribbon.Ribbon Ribbon {
			get {
				return (ActiproSoftware.Windows.Controls.Ribbon.Ribbon)this.GetValue(OptionsWindow.RibbonProperty);
			}
			set {
				this.SetValue(OptionsWindow.RibbonProperty, value);
			}
		}
		
	}
}