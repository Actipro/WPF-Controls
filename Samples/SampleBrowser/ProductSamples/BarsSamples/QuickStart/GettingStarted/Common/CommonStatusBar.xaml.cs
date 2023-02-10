using ActiproSoftware.SampleBrowser;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.GettingStarted.Common {
	/// <summary>
	/// Interaction logic for CommonStatusBar.xaml
	/// </summary>
	public partial class CommonStatusBar : StatusBar {

		#region Dependency Properties

		/// <summary>
		/// Identifies the <see cref="SampleCodePath"/> dependency property.
		/// </summary>
		/// <value>The identifier for the <see cref="SampleCodePath"/> dependency property.</value>
		public static readonly DependencyProperty SampleCodePathProperty = DependencyProperty.Register(nameof(SampleCodePath), typeof(string), typeof(CommonStatusBar), new FrameworkPropertyMetadata(defaultValue: null, OnSampleCodePathPropertyValueChanged));

		#endregion DependencyProperties

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="CommonStatusBar"/> class.
		/// </summary>
		public CommonStatusBar() {
			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when the <see cref="SampleCodePathProperty"/> value is changed.
		/// </summary>
		/// <param name="obj">The <see cref="DependencyObject"/> whose property is changed.</param>
		/// <param name="e">A <see cref="DependencyPropertyChangedEventArgs"/> that contains the event data.</param>
		private static void OnSampleCodePathPropertyValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
			var commonStatusBar = (CommonStatusBar)obj;
			var viewItemInfo = ApplicationViewModel.Current?.ViewItemInfo;

			// Update the parameter passed when opening sample code
			var newValue = (e.NewValue as string);
			if ((newValue is null) || (viewItemInfo?.Path is null)) {
				commonStatusBar.viewCodeButton.Command = null;
				commonStatusBar.viewCodeButton.CommandParameter = null;
			}
			else {
				// The ViewItemInfo.Path will be pointed to the main control at the root of the "GettingStarted" series
				commonStatusBar.viewCodeButton.Command = ApplicationViewModel.Current?.OpenSampleCodeCommand;
				commonStatusBar.viewCodeButton.CommandParameter = viewItemInfo.Path.Replace("/MainControl", newValue);
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the relative path of the sample code for the current step.
		/// </summary>
		/// <value>A string value (e.g., <c>/Step01/MainWindow</c>).</value>
		public string SampleCodePath {
			get => (string)this.GetValue(SampleCodePathProperty);
			set => this.SetValue(SampleCodePathProperty, value);
		}

	}
}
