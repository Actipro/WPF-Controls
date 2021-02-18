#if WINRT
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Animation;
#else
using System.Windows;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Media.Animation;
#endif

namespace ActiproSoftware.ProductSamples.ChartsSamples.Demo.Financial {

	/// <summary>
	/// Financial demo that demonstrates charts used with updating stock data.
	/// </summary>
	public partial class MainControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="MainControl" /> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			#if !WINRT
			if (DesignerProperties.GetIsInDesignMode(this))
				return;
			#endif

			var stockBinding = new Binding {Path = new PropertyPath("SelectedStock")};
			SetBinding(SelectedStockProperty, stockBinding);

			Loaded += OnLoaded;
		}

		#endregion OBJECT

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region DEPENDENCY PROPERTIES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Identifies the <see cref="SelectedStock"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="SelectedStock"/> dependency property.</value>
		public static readonly DependencyProperty SelectedStockProperty =
			DependencyProperty.Register("SelectedStock", typeof(Stock), typeof(MainControl), new PropertyMetadata(null));

		#endregion DEPENDENCY PROPERTIES

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Called when the <see cref="FrameworkElement.Loaded"/> event is called.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
		private void OnLoaded(object sender, RoutedEventArgs e) {
			if (SelectedStock != null)
				((Storyboard) Resources["ShowChart"]).Begin();
		}

		#endregion NON-PUBLIC PROCEDURES

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Notifies the UI that it has been unloaded.
		/// </summary>
		public override void NotifyUnloaded() {
			base.NotifyUnloaded();
			((FinancialViewModel)DataContext).Teardown();
		}

		/// <summary>
		/// Gets or sets the selected stock.
		/// </summary>
		/// <value>The selected stock.</value>
		public Stock SelectedStock {
			get { return (Stock)GetValue(SelectedStockProperty); }
			set { SetValue(SelectedStockProperty, value); }
		}

		#endregion PUBLIC PROCEDURES
	}
}
