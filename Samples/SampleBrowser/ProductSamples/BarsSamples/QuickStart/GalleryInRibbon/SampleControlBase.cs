using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.Input;
using ActiproSoftware.Windows.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.GalleryInRibbon {

	/// <summary>
	/// Provides the base user control of shared logic for this sample that is extended for MVVM- and XAML-based samples.
	/// </summary>
	public abstract class SampleControlBase : UserControl {

		private ICommand										configureOneRowLayoutCommand;
		private ICommand										configureTwoRowLayoutCommand;
		private ICommand										configureThreeRowLayoutCommand;
		private DelegateCommand<ColorBarGalleryItemViewModel>	setColorCommand;

		#region Dependency Properties

		public static readonly DependencyProperty OptionsProperty = DependencyProperty.Register(nameof(Options), typeof(OptionsViewModel), typeof(SampleControlBase), new PropertyMetadata(null, OnOptionsPropertyValueChanged));

		#endregion Dependency Properties

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="SampleControlBase"/> class.
		/// </summary>
		public SampleControlBase() {
			// Initialization of the base class is performed in the OnInitialized method that
			// is called after derived classes call InitializeComponent
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a collection of gallery item view models for the base colors consistent with the default standard colors.
		/// </summary>
		/// <returns>An array of <see cref="ColorBarGalleryItemViewModel"/>.</returns>
		private static ColorBarGalleryItemViewModel[] CreateColorItemsCollection() {
			var warmColorsCategory = "Warm Colors";
			var coolColorsCategory = "Cool Colors";
			var neutralColorsCategory = "Neutral Colors";
			return new[] {
				new ColorBarGalleryItemViewModel(neutralColorsCategory, UIColor.FromWebColor("#eeece1").ToColor(), "Tan"),
				new ColorBarGalleryItemViewModel(neutralColorsCategory, UIColor.FromWebColor("#ddd9c3").ToColor(), "Tan, Darker 10%"),
				new ColorBarGalleryItemViewModel(neutralColorsCategory, UIColor.FromWebColor("#c4bd97").ToColor(), "Tan, Darker 25%"),
				new ColorBarGalleryItemViewModel(neutralColorsCategory, UIColor.FromWebColor("#938953").ToColor(), "Tan, Darker 50%"),
				new ColorBarGalleryItemViewModel(neutralColorsCategory, UIColor.FromWebColor("#494429").ToColor(), "Tan, Darker 75%"),
				new ColorBarGalleryItemViewModel(neutralColorsCategory, UIColor.FromWebColor("#1d1b10").ToColor(), "Tan, Darker 90%"),

				new ColorBarGalleryItemViewModel(coolColorsCategory, UIColor.FromWebColor("#1f497d").ToColor(), "Dark Blue"),
				new ColorBarGalleryItemViewModel(coolColorsCategory, UIColor.FromWebColor("#c6d9f0").ToColor(), "Dark Blue, Lighter 80%"),
				new ColorBarGalleryItemViewModel(coolColorsCategory, UIColor.FromWebColor("#8db3e2").ToColor(), "Dark Blue, Lighter 60%"),
				new ColorBarGalleryItemViewModel(coolColorsCategory, UIColor.FromWebColor("#548dd4").ToColor(), "Dark Blue, Lighter 40%"),
				new ColorBarGalleryItemViewModel(coolColorsCategory, UIColor.FromWebColor("#17365d").ToColor(), "Dark Blue, Darker 25%"),
				new ColorBarGalleryItemViewModel(coolColorsCategory, UIColor.FromWebColor("#17365d").ToColor(), "Dark Blue, Darker 50%"),

				new ColorBarGalleryItemViewModel(coolColorsCategory, UIColor.FromWebColor("#4f81bd").ToColor(), "Blue"),
				new ColorBarGalleryItemViewModel(coolColorsCategory, UIColor.FromWebColor("#dbe5f1").ToColor(), "Blue, Lighter 80%"),
				new ColorBarGalleryItemViewModel(coolColorsCategory, UIColor.FromWebColor("#b8cce4").ToColor(), "Blue, Lighter 60%"),
				new ColorBarGalleryItemViewModel(coolColorsCategory, UIColor.FromWebColor("#95b3d7").ToColor(), "Blue, Lighter 40%"),
				new ColorBarGalleryItemViewModel(coolColorsCategory, UIColor.FromWebColor("#366092").ToColor(), "Blue, Darker 25%"),
				new ColorBarGalleryItemViewModel(coolColorsCategory, UIColor.FromWebColor("#244061").ToColor(), "Blue, Darker 50%"),

				new ColorBarGalleryItemViewModel(warmColorsCategory, UIColor.FromWebColor("#c0504d").ToColor(), "Red"),
				new ColorBarGalleryItemViewModel(warmColorsCategory, UIColor.FromWebColor("#f2dbdb").ToColor(), "Red, Lighter 80%"),
				new ColorBarGalleryItemViewModel(warmColorsCategory, UIColor.FromWebColor("#e5b9b7").ToColor(), "Red, Lighter 60%"),
				new ColorBarGalleryItemViewModel(warmColorsCategory, UIColor.FromWebColor("#d99694").ToColor(), "Red, Lighter 40%"),
				new ColorBarGalleryItemViewModel(warmColorsCategory, UIColor.FromWebColor("#953734").ToColor(), "Red, Darker 25%"),
				new ColorBarGalleryItemViewModel(warmColorsCategory, UIColor.FromWebColor("#632423").ToColor(), "Red, Darker 50%"),

				new ColorBarGalleryItemViewModel(warmColorsCategory, UIColor.FromWebColor("#f79646").ToColor(), "Orange"),
				new ColorBarGalleryItemViewModel(warmColorsCategory, UIColor.FromWebColor("#fdeada").ToColor(), "Orange, Lighter 80%"),
				new ColorBarGalleryItemViewModel(warmColorsCategory, UIColor.FromWebColor("#fbd5b5").ToColor(), "Orange, Lighter 60%"),
				new ColorBarGalleryItemViewModel(warmColorsCategory, UIColor.FromWebColor("#fac090").ToColor(), "Orange, Lighter 40%"),
				new ColorBarGalleryItemViewModel(warmColorsCategory, UIColor.FromWebColor("#e36c09").ToColor(), "Orange, Darker 25%"),
				new ColorBarGalleryItemViewModel(warmColorsCategory, UIColor.FromWebColor("#974806").ToColor(), "Orange, Darker 50%"),

				new ColorBarGalleryItemViewModel(coolColorsCategory, UIColor.FromWebColor("#9bbb59").ToColor(), "Olive Green"),
				new ColorBarGalleryItemViewModel(coolColorsCategory, UIColor.FromWebColor("#ebf1dd").ToColor(), "Olive Green, Lighter 80%"),
				new ColorBarGalleryItemViewModel(coolColorsCategory, UIColor.FromWebColor("#d6e3bc").ToColor(), "Olive Green, Lighter 60%"),
				new ColorBarGalleryItemViewModel(coolColorsCategory, UIColor.FromWebColor("#c3d69b").ToColor(), "Olive Green, Lighter 40%"),
				new ColorBarGalleryItemViewModel(coolColorsCategory, UIColor.FromWebColor("#76923c").ToColor(), "Olive Green, Darker 25%"),
				new ColorBarGalleryItemViewModel(coolColorsCategory, UIColor.FromWebColor("#4f6128").ToColor(), "Olive Green, Darker 50%"),

				new ColorBarGalleryItemViewModel(coolColorsCategory, UIColor.FromWebColor("#4bacc6").ToColor(), "Aqua"),
				new ColorBarGalleryItemViewModel(coolColorsCategory, UIColor.FromWebColor("#dbeef3").ToColor(), "Aqua, Lighter 80%"),
				new ColorBarGalleryItemViewModel(coolColorsCategory, UIColor.FromWebColor("#b6dde8").ToColor(), "Aqua, Lighter 60%"),
				new ColorBarGalleryItemViewModel(coolColorsCategory, UIColor.FromWebColor("#93cddc").ToColor(), "Aqua, Lighter 40%"),
				new ColorBarGalleryItemViewModel(coolColorsCategory, UIColor.FromWebColor("#31859b").ToColor(), "Aqua, Darker 25%"),
				new ColorBarGalleryItemViewModel(coolColorsCategory, UIColor.FromWebColor("#205867").ToColor(), "Aqua, Darker 50%"),

				new ColorBarGalleryItemViewModel(coolColorsCategory, UIColor.FromWebColor("#8064a2").ToColor(), "Purple"),
				new ColorBarGalleryItemViewModel(coolColorsCategory, UIColor.FromWebColor("#e5e0ec").ToColor(), "Purple, Lighter 80%"),
				new ColorBarGalleryItemViewModel(coolColorsCategory, UIColor.FromWebColor("#ccc0d9").ToColor(), "Purple, Lighter 60%"),
				new ColorBarGalleryItemViewModel(coolColorsCategory, UIColor.FromWebColor("#b2a2c7").ToColor(), "Purple, Lighter 40%"),
				new ColorBarGalleryItemViewModel(coolColorsCategory, UIColor.FromWebColor("#5f497a").ToColor(), "Purple, Darker 25%"),
				new ColorBarGalleryItemViewModel(coolColorsCategory, UIColor.FromWebColor("#3f3151").ToColor(), "Purple, Darker 50%"),

				new ColorBarGalleryItemViewModel(neutralColorsCategory, UIColor.FromWebColor("#ffffff").ToColor(), "White"),
				new ColorBarGalleryItemViewModel(neutralColorsCategory, UIColor.FromWebColor("#f2f2f2").ToColor(), "White, Darker 5%"),
				new ColorBarGalleryItemViewModel(neutralColorsCategory, UIColor.FromWebColor("#d8d8d8").ToColor(), "White, Darker 15%"),
				new ColorBarGalleryItemViewModel(neutralColorsCategory, UIColor.FromWebColor("#bfbfbf").ToColor(), "White, Darker 25%"),
				new ColorBarGalleryItemViewModel(neutralColorsCategory, UIColor.FromWebColor("#a5a5a5").ToColor(), "White, Darker 35%"),
				new ColorBarGalleryItemViewModel(neutralColorsCategory, UIColor.FromWebColor("#7f7f7f").ToColor(), "White, Darker 50%"),

			};
		}

		/// <summary>
		/// Initializes the collection of gallery item view models for the galleries used by this sample.
		/// </summary>
		private void InitializeColorGalleryItemViewModelCollections() {
			this.ColorItems = CreateColorItemsCollection();
		}

		/// <summary>
		/// Occurs when the <see cref="OptionsProperty"/> dependency property value has changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> containing data related to this event.</param>
		private static void OnOptionsPropertyValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
			=> ((SampleControlBase)obj).OnOptionsPropertyValueChanged(e.OldValue as OptionsViewModel, e.NewValue as OptionsViewModel);

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the gallery item view models for a Font Color gallery.
		/// </summary>
		/// <value>An <see cref="IEnumerable{T}"/> of <see cref="ColorBarGalleryItemViewModel"/>.</value>
		public IEnumerable<ColorBarGalleryItemViewModel> ColorItems { get; private set; }

		/// <summary>
		/// Gets the command that will configure the gallery with a 1-row layout.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand ConfigureOneRowLayoutCommand {
			get {
				if (configureOneRowLayoutCommand is null) {
					configureOneRowLayoutCommand = new DelegateCommand<object>(param => {
						if (this.Options != null)
							this.Options.ItemTemplate = FindResource("LargeItemDataTemplate") as DataTemplate;
					});
				}
				return configureOneRowLayoutCommand;
			}
		}

		/// <summary>
		/// Gets the command that will configure the gallery with a 3-row layout.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand ConfigureThreeRowLayoutCommand {
			get {
				if (configureThreeRowLayoutCommand is null) {
					configureThreeRowLayoutCommand = new DelegateCommand<object>(param => {
						if (this.Options != null)
							this.Options.ItemTemplate = FindResource("SmallItemDataTemplate") as DataTemplate;
					});
				}
				return configureThreeRowLayoutCommand;
			}
		}

		/// <summary>
		/// Gets the command that will configure the gallery with a 2-row layout.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand ConfigureTwoRowLayoutCommand {
			get {
				if (configureTwoRowLayoutCommand is null) {
					configureTwoRowLayoutCommand = new DelegateCommand<object>(param => {
						if (this.Options != null)
							this.Options.ItemTemplate = FindResource("MediumItemDataTemplate") as DataTemplate;
					});
				}
				return configureTwoRowLayoutCommand;
			}
		}

		/// <summary>
		/// Gets the large-sized <see cref="ImageSource"/> to be used for a command that configures a 1-row layout.
		/// </summary>
		public ImageSource OneRowLayoutLargeImageSource { get; private set; }

		/// <summary>
		/// Gets the small-sized <see cref="ImageSource"/> to be used for a command that configures a 1-row layout.
		/// </summary>
		public ImageSource OneRowLayoutSmallImageSource { get; private set; }

		/// <inheritdoc />
		protected override void OnInitialized(EventArgs e) {
			base.OnInitialized(e);

			// Cache the XAML-based images defined as resources
			this.OneRowLayoutLargeImageSource = FindResource("OneRowLayoutLargeImage") as DrawingImage;
			this.OneRowLayoutSmallImageSource = FindResource("OneRowLayoutSmallImage") as DrawingImage;
			this.TwoRowLayoutLargeImageSource = FindResource("TwoRowLayoutLargeImage") as DrawingImage;
			this.TwoRowLayoutSmallImageSource = FindResource("TwoRowLayoutSmallImage") as DrawingImage;
			this.ThreeRowLayoutLargeImageSource = FindResource("ThreeRowLayoutLargeImage") as DrawingImage;
			this.ThreeRowLayoutSmallImageSource = FindResource("ThreeRowLayoutSmallImage") as DrawingImage;

			// Initialize the collection of color gallery items (used by both XAML and MVVM samples)
			InitializeColorGalleryItemViewModelCollections();
		}

		/// <summary>
		/// Handles a change in one of the individual property values on <see cref="Options"/>.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="args">The event data.</param>
		protected virtual void OnOptionsPropertyChanged(object sender, PropertyChangedEventArgs args) {
			if (args.PropertyName == nameof(Options.IsSetColorCommandEnabled)) {
				// Notify that the state of the command has changed
				setColorCommand?.RaiseCanExecuteChanged();
			}
		}

		/// <summary>
		/// Handles a change in the <see cref="OptionsProperty"/> dependency property value.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		protected virtual void OnOptionsPropertyValueChanged(OptionsViewModel oldValue, OptionsViewModel newValue) {
			// Stop listening for changes
			if (oldValue != null)
				oldValue.PropertyChanged -= OnOptionsPropertyChanged;

			// Listen for changes
			if (newValue != null)
				newValue.PropertyChanged += OnOptionsPropertyChanged;

			// Use the 1-row layout by default
			if ((newValue != null) && (newValue.ItemTemplate == null))
				newValue.ItemTemplate = this.FindResource("LargeItemDataTemplate") as DataTemplate;
		}

		/// <summary>
		/// Gets or sets the options associated with this control.
		/// </summary>
		public OptionsViewModel Options {
			get => (OptionsViewModel)GetValue(OptionsProperty);
			set => SetValue(OptionsProperty, value);
		}

		/// <summary>
		/// Gets the command to be executed when a gallery item is selected.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand SetColorCommand {
			get {
				if (setColorCommand is null) {
					setColorCommand = new DelegateCommand<ColorBarGalleryItemViewModel>(
						param => {
							if (param != null) {
								ThemedMessageBox.Show($"This is where you would apply the following selected color:\r\n\r\n{param.Color} {param.Label}",
									"Set Color", MessageBoxButton.OK, MessageBoxImage.Information);
							}
						},
						param => this.Options?.IsSetColorCommandEnabled ?? false
					);
				}
				return setColorCommand;
			}
		}

		/// <summary>
		/// Gets the large-sized <see cref="ImageSource"/> to be used for a command that configures a 3-row layout.
		/// </summary>
		public ImageSource ThreeRowLayoutLargeImageSource { get; private set;  }

		/// <summary>
		/// Gets the small-sized <see cref="ImageSource"/> to be used for a command that configures a 3-row layout.
		/// </summary>
		public ImageSource ThreeRowLayoutSmallImageSource { get; private set; }

		/// <summary>
		/// Gets the large-sized <see cref="ImageSource"/> to be used for a command that configures a 2-row layout.
		/// </summary>
		public ImageSource TwoRowLayoutLargeImageSource { get; private set; }

		/// <summary>
		/// Gets the small-sized <see cref="ImageSource"/> to be used for a command that configures a 2-row layout.
		/// </summary>
		public ImageSource TwoRowLayoutSmallImageSource { get; private set; }

	}

}
