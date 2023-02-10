using ActiproSoftware.Windows;
using System.ComponentModel;
using System.Windows.Media;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.GalleryColorPickers {

	/// <summary>
	/// Defines configurable options for this sample.
	/// </summary>
	public class OptionsViewModel : ObservableObjectBase {

		private bool	areSurroundingSeparatorsAllowed	= true;
		private Color	fontColor						= Colors.Red;
		private bool	fontColorCanCategorize			= true;
		private bool	fontColorCanFilter				= false;
		private int		itemSpacing						= 4;
		private Color	livePreviewColor				= Colors.Transparent;
		private Color	textHighlightColor				= Colors.Yellow;
		private int		textHighlightColCount			= 5;
		private bool	useAccentedItemBorder			= true;
		private bool	useCustomColors					= false;
		private bool	useMenuItemIndent				= false;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets if separators are displayed immediately before and after a gallery.
		/// </summary>
		/// <value><c>true</c> to display separators before and after the gallery; otherwise <c>false</c>.</value>
		[DisplayName("Use surrounding separators")]
		public bool AreSurroundingSeparatorsAllowed {
			get => areSurroundingSeparatorsAllowed;
			set {
				if (areSurroundingSeparatorsAllowed != value) {
					areSurroundingSeparatorsAllowed = value;
					NotifyPropertyChanged(nameof(AreSurroundingSeparatorsAllowed));
				}
			}
		}

		/// <summary>
		/// Gets or sets the current font color.
		/// </summary>
		/// <value>A <see cref="Color"/> value.</value>
		public Color FontColor {
			get => fontColor;
			set {
				if (fontColor != value) {
					fontColor = value;
					NotifyPropertyChanged(nameof(FontColor));
				}
			}
		}

		/// <summary>
		/// Gets or sets if font color controls can be categorized;
		/// </summary>
		/// <value><c>true</c> to enable categorization; otherwise <c>false</c>.</value>
		public bool FontColorCanCategorize {
			get => fontColorCanCategorize;
			set {
				if (fontColorCanCategorize != value) {
					fontColorCanCategorize = value;
					NotifyPropertyChanged(nameof(FontColorCanCategorize));
				}
			}
		}

		/// <summary>
		/// Gets or sets if font color controls can be filtered;
		/// </summary>
		/// <remarks>This property will typically only be set to <c>true</c> when categorization is also enabled.</remarks>
		/// <value><c>true</c> to enable filters; otherwise <c>false</c>.</value>
		public bool FontColorCanFilter {
			get => fontColorCanFilter;
			set {
				if (fontColorCanFilter != value) {
					fontColorCanFilter = value;
					NotifyPropertyChanged(nameof(FontColorCanFilter));
				}
			}
		}

		/// <summary>
		/// Gets or sets the amount of spacing between non-grouped gallery items.
		/// </summary>
		/// <value>An integer value.</value>
		public int ItemSpacing {
			get => itemSpacing;
			set {
				if (itemSpacing != value) {
					itemSpacing = value;
					NotifyPropertyChanged(nameof(ItemSpacing));
				}
			}
		}

		/// <summary>
		/// Gets or sets the current live preview color as a result of moving the mouse over gallery items.
		/// </summary>
		/// <value>A <see cref="Color"/> value; or <see cref="Colors.Transparent"/> if there is not a preview color.</value>
		public Color LivePreviewColor {
			get => livePreviewColor;
			set {
				if (livePreviewColor != value) {
					livePreviewColor = value;
					NotifyPropertyChanged(nameof(LivePreviewColor));
				}
			}
		}

		/// <summary>
		/// Gets or sets the current text highlight color.
		/// </summary>
		/// <value>A <see cref="Color"/> value.</value>
		public Color TextHighlightColor {
			get => textHighlightColor;
			set {
				if (textHighlightColor != value) {
					textHighlightColor = value;
					NotifyPropertyChanged(nameof(TextHighlightColor));
				}
			}
		}

		/// <summary>
		/// Gets or sets the number of columns to be displayed for a text highlight color picker.
		/// </summary>
		/// <value>An integer value.</value>
		[DisplayName("Highlight color col count")]
		public int TextHighlightColCount {
			get => textHighlightColCount;
			set {
				if (textHighlightColCount != value) {
					textHighlightColCount = value;
					NotifyPropertyChanged(nameof(TextHighlightColCount));
				}
			}
		}

		/// <summary>
		/// Gets or sets if an accented border is displayed around gallery items.
		/// </summary>
		/// <value><c>true</c> to use an accented border; otherwise <c>false</c>.</value>
		public bool UseAccentedItemBorder {
			get => useAccentedItemBorder;
			set {
				if (useAccentedItemBorder != value) {
					useAccentedItemBorder = value;
					NotifyPropertyChanged(nameof(UseAccentedItemBorder));
				}
			}
		}

		/// <summary>
		/// Gets or sets if a collection of custom colors should be used instead of the default colors.
		/// </summary>
		/// <value><c>true</c> to use custom colors; otherwise <c>false</c>.</value>
		public bool UseCustomColors {
			get => useCustomColors;
			set {
				if (useCustomColors != value) {
					useCustomColors = value;
					NotifyPropertyChanged(nameof(UseCustomColors));
				}
			}
		}

		/// <summary>
		/// Gets or sets if gallery items are indented on the left and right like other menu items.
		/// </summary>
		/// <value><c>true</c> use indenting consistent with menu items; otherwise <c>false</c> to use all available horizontal space for the gallery.</value>
		public bool UseMenuItemIndent {
			get => useMenuItemIndent;
			set {
				if (useMenuItemIndent != value) {
					useMenuItemIndent = value;
					NotifyPropertyChanged(nameof(UseMenuItemIndent));
				}
			}
		}
	}

}
