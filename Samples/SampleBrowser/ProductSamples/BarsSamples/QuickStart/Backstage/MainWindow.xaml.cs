using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Input;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.Backstage {

	public partial class MainWindow : INotifyPropertyChanged {

		private ICommand backstageHeaderButtonCommand;

		private int			backstageMinHeaderWidth		= 0;
		private int			backstageMaxHeaderWidth		= 300;
		private bool		canClose					= true;
		private bool		isBackstageOpen				= true;
		private bool		isFirstBackstage			= true;
		private string		sampleButton3Label			= "Sample Button 3";
		private bool		selectOptionsTabOnOpen	= false;
		private bool		useSampleButtonImages		= false;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// EVENTS
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public event PropertyChangedEventHandler PropertyChanged;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MainWindow() {
			InitializeComponent();

			// Bind the view to itself since an explicit view model is not created for this sample
			this.DataContext = this;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Raises the <see cref="PropertyChanged"/> event.
		/// </summary>
		/// <param name="propertyName">The name of the changed property.</param>
		private void NotifyPropertyChanged(string propertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		/// <summary>
		/// Called when the <see cref="RibbonBackstage.IsOpen"/> property value is changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The event args.</param>
		private void OnBackstageIsOpenChanged(object sender, RoutedEventArgs e) {
			// Optionally pre-select the 'Options' tab when opening the backstage
			if (sender is RibbonBackstage backstage
				&& backstage.IsOpen
				&& this.SelectOptionsTabOnOpen) {

				// Find the desired tab to select
				var optionsTab = backstage.Items.OfType<RibbonBackstageTabItem>()
					.FirstOrDefault(tabItem => tabItem.Key == "Options");

				// Configure the backstage selection
				if (optionsTab != null)
					backstage.SelectedItem = optionsTab;
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the command to be executed when one of the sample backstage buttons is invoked.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand BackstageHeaderButtonCommand {
			get {
				if (backstageHeaderButtonCommand is null) {
					backstageHeaderButtonCommand = new DelegateCommand<object>(param => {
						ThemedMessageBox.Show("When a RibbonBackstageHeaderButton is invoked, the Backstage automatically closes.\r\n\r\nThese buttons are typically associated with commands that perform simple operations like Help, Save, or Close that do not need the additional content area of a RibbonBackstageTabItem.",
							"About RibbonBackstageHeaderButton", MessageBoxButton.OK, MessageBoxImage.Information);
					});
				}
				return backstageHeaderButtonCommand;
			}
		}

		/// <summary>
		/// Gets or sets the maximum width of the backstage header where tabs and buttons are displayed.
		/// </summary>
		/// <value>An integer value.</value>
		public int BackstageMaxHeaderWidth {
			get => backstageMaxHeaderWidth;
			set {
				if (backstageMaxHeaderWidth != value) {
					backstageMaxHeaderWidth = value;
					NotifyPropertyChanged(nameof(BackstageMaxHeaderWidth));

					this.BackstageMinHeaderWidth = Math.Min(this.BackstageMinHeaderWidth, value);
				}
			}
		}

		/// <summary>
		/// Gets or sets the minimum width of the backstage header where tabs and buttons are displayed.
		/// </summary>
		/// <value>An integer value.</value>
		public int BackstageMinHeaderWidth {
			get => backstageMinHeaderWidth;
			set {
				if (backstageMinHeaderWidth != value) {
					backstageMinHeaderWidth = value;
					NotifyPropertyChanged(nameof(BackstageMinHeaderWidth));

					this.BackstageMaxHeaderWidth = Math.Max(this.BackstageMaxHeaderWidth, value);
				}
			}
		}

		/// <summary>
		/// Gets or sets if the backstage can be closed.
		/// </summary>
		/// <value><c>true</c> if the backstage can close; otherwise <c>false</c>.</value>
		public bool CanClose {
			get => canClose;
			set {
				if (canClose != value) {
					canClose = value;
					NotifyPropertyChanged(nameof(CanClose));
				}
			}
		}

		/// <summary>
		/// Gets or sets if the backstage is open.
		/// </summary>
		/// <value><c>true</c> if the backstage is open; otherwise <c>false</c>.</value>
		public bool IsBackstageOpen {
			get => isBackstageOpen;
			set {
				if (isBackstageOpen != value) {
					isBackstageOpen = value;
					NotifyPropertyChanged(nameof(IsBackstageOpen));

					// When the backstage closes, set a flag that the initial backstage is no longer displayed
					if (!isBackstageOpen)
						IsFirstBackstage = false;
				}
			}
		}

		/// <summary>
		/// Gets or sets if the backstage should be configured for the initial view where some tabs are larger
		/// and unnecessary buttons are hidden.
		/// </summary>
		/// <value><c>true</c> if this is the initial view for the backstage; otherwise <c>false</c>.</value>
		public bool IsFirstBackstage {
			get => isFirstBackstage;
			set {
				if (isFirstBackstage != value) {
					isFirstBackstage = value;
					NotifyPropertyChanged(nameof(IsFirstBackstage));

					// Notify dependent properties have changed
					NotifyPropertyChanged(nameof(PrimaryBackstageTabVariantSize));
				}
			}
		}

		/// <summary>
		/// Gets the <see cref="VariantSize"/> to be used for the primary tabs.
		/// </summary>
		/// <remarks>
		/// This property is used to show large variants of the most important tabs when the backstage is initially displayed.
		/// </remarks>
		/// <value>One of the <see cref="VariantSize"/> values.</value>
		public VariantSize PrimaryBackstageTabVariantSize => (IsFirstBackstage) ? VariantSize.Large : VariantSize.Small;

		/// <summary>
		/// Gets or sets the label to be displayed on the third sample button.
		/// </summary>
		/// <value>A string value.</value>
		public string SampleButton3Label {
			get => sampleButton3Label;
			set {
				if (sampleButton3Label != value) {
					sampleButton3Label = value;
					NotifyPropertyChanged(nameof(SampleButton3Label));
				}
			}
		}

		/// <summary>
		/// Gets the <see cref="ImageSource"/>, if any, to be displayed on the sample buttons.
		/// </summary>
		/// <value>An <see cref="ImageSource"/> or <c>null</c> if no image should be displayed.</value>
		public ImageSource SampleButtonImageSource => UseSampleButtonImages ? new BitmapImage(new Uri("/Images/Icons/QuickStart16.png", UriKind.Relative)) : null;

		/// <summary>
		/// Gets or sets if the "Options" tab should be selected when opening the backstage.
		/// </summary>
		/// <value><c>true</c> to select the "Options" tab when the backstage is opened; otherwise, <c>false</c> to use the default selection.</value>
		public bool SelectOptionsTabOnOpen {
			get => selectOptionsTabOnOpen;
			set {
				if (selectOptionsTabOnOpen != value) {
					selectOptionsTabOnOpen = value;
					NotifyPropertyChanged(nameof(SelectOptionsTabOnOpen));
				}
			}
		}

		/// <summary>
		/// Gets or sets if images should be displayed on the sample buttons.
		/// </summary>
		/// <value><c>true</c> to show images; otherwise <c>false</c>.</value>
		public bool UseSampleButtonImages {
			get => useSampleButtonImages;
			set {
				if (useSampleButtonImages != value) {
					useSampleButtonImages = value;
					NotifyPropertyChanged(nameof(UseSampleButtonImages));

					// Notify dependent properties have changed
					NotifyPropertyChanged(nameof(SampleButtonImageSource));
				}
			}
		}

	}

}
