using System.Windows.Input;
using System.Windows.Media;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a size selection gallery control within a bar control.
	/// </summary>
	public class BarSizeSelectionMenuGalleryViewModel : BarGalleryViewModelBase, IHasVariantImages {

		private string defaultHeadingText;
		private int menuColumnCount = 10;
		private int menuRowCount = 8;
		private string sizeHeadingTextFormat;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarButtonViewModel()"/>
		public BarSizeSelectionMenuGalleryViewModel()  // Parameterless constructor required for XAML support
			: this(key: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string)"/>
		public BarSizeSelectionMenuGalleryViewModel(string key)
			: this(key, label: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string)"/>
		public BarSizeSelectionMenuGalleryViewModel(string key, string label)
			: this(key, label, command: null) { }

		/// <inheritdoc cref="BarButtonViewModel(RoutedCommand)"/>
		public BarSizeSelectionMenuGalleryViewModel(RoutedCommand routedCommand)
			: this(routedCommand?.Name, routedCommand) { }

		/// <inheritdoc cref="BarButtonViewModel(string, ICommand)"/>
		public BarSizeSelectionMenuGalleryViewModel(string key, ICommand command)
			: this(key, label: null, command) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, ICommand)"/>
		public BarSizeSelectionMenuGalleryViewModel(string key, string label, ICommand command)
			: base(key, label, command) {

			ItemSpacing = 1.0;
			MinItemHeight = 20.0;
			MinItemWidth = 20.0;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc/>
		ImageSource IHasVariantImages.LargeImageSource {
			get => null;
			set { /* No-op since a large image is not supported by the control */ }
		}

		/// <inheritdoc/>
		ImageSource IHasVariantImages.MediumImageSource {
			get => null;
			set { /* No-op since a medium image is not supported by the control */ }
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the default heading text that is displayed above the menu gallery.
		/// </summary>
		/// <value>The default heading text that is displayed above the menu gallery.</value>
		public string DefaultHeadingText {
			get => defaultHeadingText;
			set {
				if (defaultHeadingText != value) {
					defaultHeadingText = value;
					this.NotifyPropertyChanged(nameof(DefaultHeadingText));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the number of columns when in a menu.
		/// </summary>
		/// <value>
		/// The number of columns when in a menu.
		/// The default value is <c>10</c>.
		/// </value>
		public int MenuColumnCount {
			get => menuColumnCount ;
			set {
				if (menuColumnCount  != value) {
					menuColumnCount  = value;
					this.NotifyPropertyChanged(nameof(MenuColumnCount));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the number of rows when in a menu.
		/// </summary>
		/// <value>
		/// The number of rows when in a menu.
		/// The default value is <c>8</c>.
		/// </value>
		public int MenuRowCount {
			get => menuRowCount ;
			set {
				if (menuRowCount  != value) {
					menuRowCount  = value;
					this.NotifyPropertyChanged(nameof(MenuRowCount));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the format string to use for the heading when the end user is selecting a size in the gallery.
		/// </summary>
		/// <value>The format string to use for the heading when the end user is selecting a size in the gallery.</value>
		/// <remarks>
		/// The first parameter to the format string is the current width.
		/// The second parameter to the format string is the current height.
		/// </remarks>
		public string SizeHeadingTextFormat {
			get => sizeHeadingTextFormat;
			set {
				if (sizeHeadingTextFormat != value) {
					sizeHeadingTextFormat = value;
					this.NotifyPropertyChanged(nameof(SizeHeadingTextFormat));
				}
			}
		}
		
	}

}
