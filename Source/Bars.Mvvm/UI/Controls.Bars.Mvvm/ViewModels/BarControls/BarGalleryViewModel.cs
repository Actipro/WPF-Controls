using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a standard gallery control within a bar control.
	/// </summary>
	[ContentProperty(nameof(Items))]
	public class BarGalleryViewModel : BarGalleryViewModelBase, IHasVariantImages {

		private bool canCategorize;
		private bool canFilter;
		private DataTemplate categoryHeaderTemplate;
		private string categoryPropertyName = nameof(BarGalleryItemViewModelBase.Category);
		private string collapsedButtonDescription;
		private bool hasCategoryHeaders = true;
		private bool isSelectionSupported = true;
		private string keyTipText;
		private ImageSource largeImageSource;
		private int maxMenuColumnCount = int.MaxValue;
		private int maxRibbonColumnCount = 15;
		private VariantSize maxSimplifiedVariantSize = VariantSize.Small;
		private ImageSource mediumImageSource;
		private ControlResizeMode menuResizeMode = ControlResizeMode.None;
		private int minLargeRibbonColumnCount = 5;
		private int minMediumRibbonColumnCount = 3;
		private int minMenuColumnCount = 1;
		private BarGalleryItemViewModelBase selectedItem;
		private bool useAccentedItemBorder;
		private bool useMenuItemAppearance;
		private bool useMenuItemIndent;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarButtonViewModel()"/>
		public BarGalleryViewModel() { }  // Parameterless constructor required for XAML support

		/// <inheritdoc cref="BarButtonViewModel(string)"/>
		public BarGalleryViewModel(string key)
			: this(key, label: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key.  The label and key tip text are auto-generated.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="items">The collection of gallery items.</param>
		public BarGalleryViewModel(string key, IEnumerable<BarGalleryItemViewModelBase> items)
			: this(key, label: null, items) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string)"/>
		public BarGalleryViewModel(string key, string label)
			: this(key, label, keyTipText: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key and label.  The key tip text is auto-generated.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="label">The text label to display, which is auto-generated from the <paramref name="key"/> if <c>null</c>.</param>
		/// <param name="items">The collection of gallery items.</param>
		public BarGalleryViewModel(string key, string label, IEnumerable<BarGalleryItemViewModelBase> items)
			: this(key, label, keyTipText: null, items) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, string)"/>
		public BarGalleryViewModel(string key, string label, string keyTipText)
			: this(key, label, keyTipText, items: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key, label, and key tip text.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="label">The text label to display, which is auto-generated from the <paramref name="key"/> if <c>null</c>.</param>
		/// <param name="keyTipText">The key tip text, which is auto-generated from the <paramref name="label"/> if <c>null</c>.</param>
		/// <param name="items">The collection of gallery items.</param>
		public BarGalleryViewModel(string key, string label, string keyTipText, IEnumerable<BarGalleryItemViewModelBase> items)
			: this(key, label, keyTipText, command: null, items) { }

		/// <inheritdoc cref="BarButtonViewModel(RoutedCommand)"/>
		public BarGalleryViewModel(RoutedCommand routedCommand)
			: this(routedCommand?.Name, routedCommand) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified <see cref="RoutedCommand"/>, also used to auto-generate a key, label, and key tip text.
		/// </summary>
		/// <param name="routedCommand">The command to attach to the control.</param>
		/// <param name="items">The collection of gallery items.</param>
		public BarGalleryViewModel(RoutedCommand routedCommand, IEnumerable<BarGalleryItemViewModelBase> items)
			: this(routedCommand?.Name, routedCommand, items) { }

		/// <inheritdoc cref="BarButtonViewModel(string, ICommand)"/>
		public BarGalleryViewModel(string key, ICommand command)
			: this(key, label: null, command) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, ICommand)"/>
		public BarGalleryViewModel(string key, string label, ICommand command)
			: this(key, label, keyTipText: null, command) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, string, ICommand)"/>
		public BarGalleryViewModel(string key, string label, string keyTipText, ICommand command)
			: this(key, label, keyTipText, command, items: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key and command.  The label and key tip text are auto-generated.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="command">The command to attach to the control.</param>
		/// <param name="items">The collection of gallery items.</param>
		public BarGalleryViewModel(string key, ICommand command, IEnumerable<BarGalleryItemViewModelBase> items)
			: this(key, label: null, keyTipText: null, command, items) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key, label, and command.  The key tip text is auto-generated.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="label">The text label to display, which is auto-generated from the <paramref name="command"/> or <paramref name="key"/> if <c>null</c>.</param>
		/// <param name="command">The command to attach to the control.</param>
		/// <param name="items">The collection of gallery items.</param>
		public BarGalleryViewModel(string key, string label, ICommand command, IEnumerable<BarGalleryItemViewModelBase> items)
			: this(key, label, keyTipText: null, command, items) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key, label, key tip text, and command.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="label">The text label to display, which is auto-generated from the <paramref name="command"/> or <paramref name="key"/> if <c>null</c>.</param>
		/// <param name="keyTipText">The key tip text, which is auto-generated from the <paramref name="command"/> or <paramref name="label"/> if <c>null</c>.</param>
		/// <param name="command">The command to attach to the control.</param>
		/// <param name="items">The collection of gallery items.</param>
		public BarGalleryViewModel(string key, string label, string keyTipText, ICommand command, IEnumerable<BarGalleryItemViewModelBase> items)
			: base(key, label, command) {

			this.keyTipText = keyTipText ?? KeyTipTextGenerator.FromCommand(command) ?? KeyTipTextGenerator.FromLabel(this.Label);

			// Add the gallery items
			if (items != null) {
				foreach (var item in items)
					this.Items.Add(item);
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets whether the gallery sorts and displays items by category.
		/// </summary>
		/// <value>
		/// <c>true</c> if the gallery sorts and displays items by category; otherwise, <c>false</c>.
		/// The default value is <c>false</c>.
		/// </value>
		public bool CanCategorize {
			get => canCategorize;
			set {
				if (canCategorize != value) {
					canCategorize = value;
					this.NotifyPropertyChanged(nameof(CanCategorize));
				}
			}
		}

		/// <summary>
		/// Gets or sets whether the gallery supports filtering of items by category.
		/// </summary>
		/// <value>
		/// <c>true</c> if the gallery supports filtering of items by category; otherwise, <c>false</c>.
		/// The default value is <c>false</c>.
		/// </value>
		public bool CanFilter {
			get => canFilter;
			set {
				if (canFilter != value) {
					canFilter = value;
					this.NotifyPropertyChanged(nameof(CanFilter));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets a <see cref="DataTemplate"/> for category headers when categorization is enabled.
		/// </summary>
		/// <value>A <see cref="DataTemplate"/> for category headers when categorization is enabled.</value>
		public DataTemplate CategoryHeaderTemplate {
			get => categoryHeaderTemplate;
			set {
				if (categoryHeaderTemplate != value) {
					categoryHeaderTemplate = value;
					NotifyPropertyChanged(nameof(CategoryHeaderTemplate));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the name of the category property on each item object.
		/// </summary>
		/// <value>
		/// The name of the category property on each item object.
		/// The default value is <c>Category</c>.
		/// </value>
		public string CategoryPropertyName {
			get => categoryPropertyName;
			set {
				if (categoryPropertyName != value) {
					categoryPropertyName = value;
					NotifyPropertyChanged(nameof(CategoryPropertyName));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the text description to display in screen tips for the gallery when it is rendered as a collapsed button.
		/// </summary>
		/// <value>The text description to display in screen tips for the gallery when it is rendered as a collapsed button.</value>
		public string CollapsedButtonDescription {
			get => collapsedButtonDescription;
			set {
				if (collapsedButtonDescription != value) {
					collapsedButtonDescription = value;
					this.NotifyPropertyChanged(nameof(CollapsedButtonDescription));
				}
			}
		}

		/// <summary>
		/// Gets or sets whether the gallery has category headers when categorizing.
		/// </summary>
		/// <value>
		/// <c>true</c> if the gallery has category headers when categorizing; otherwise, <c>false</c>.
		/// The default value is <c>true</c>.
		/// </value>
		public bool HasCategoryHeaders {
			get => hasCategoryHeaders;
			set {
				if (hasCategoryHeaders != value) {
					hasCategoryHeaders = value;
					this.NotifyPropertyChanged(nameof(HasCategoryHeaders));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets whether selection is supported by the gallery.
		/// </summary>
		/// <value>
		/// <c>true</c> if selection is supported by the gallery; otherwise, <c>false</c>.
		/// The default value is <c>true</c>.
		/// </value>
		public bool IsSelectionSupported {
			get => isSelectionSupported;
			set {
				if (isSelectionSupported != value) {
					isSelectionSupported = value;
					this.NotifyPropertyChanged(nameof(IsSelectionSupported));
				}
			}
		}
		
		/// <summary>
		/// Gets the collection of gallery items.
		/// </summary>
		/// <value>The collection of gallery items.</value>
		public ObservableCollection<BarGalleryItemViewModelBase> Items { get; } = new ObservableCollection<BarGalleryItemViewModelBase>();

		/// <inheritdoc cref="BarButtonViewModel.KeyTipText"/>
		public string KeyTipText {
			get => keyTipText;
			set {
				if (keyTipText != value) {
					keyTipText = value;
					this.NotifyPropertyChanged(nameof(KeyTipText));
				}
			}
		}

		/// <inheritdoc cref="BarButtonViewModel.LargeImageSource"/>
		public ImageSource LargeImageSource {
			get => largeImageSource;
			set {
				if (largeImageSource != value) {
					largeImageSource = value;
					this.NotifyPropertyChanged(nameof(LargeImageSource));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the maximum number of columns when in a menu.
		/// </summary>
		/// <value>
		/// The maximum number of columns when in a menu.
		/// The default value is <see cref="Int32.MaxValue"/>.
		/// </value>
		public int MaxMenuColumnCount {
			get => maxMenuColumnCount;
			set {
				if (maxMenuColumnCount != value) {
					maxMenuColumnCount = value;
					this.NotifyPropertyChanged(nameof(MaxMenuColumnCount));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the maximum number of columns when in-ribbon.
		/// </summary>
		/// <value>
		/// The maximum number of columns when in-ribbon.
		/// The default value is <c>15</c>.
		/// </value>
		public int MaxRibbonColumnCount {
			get => maxRibbonColumnCount;
			set {
				if (maxRibbonColumnCount != value) {
					maxRibbonColumnCount = value;
					this.NotifyPropertyChanged(nameof(MaxRibbonColumnCount));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the maximum <see cref="VariantSize"/> when using a Simplified layout mode.
		/// </summary>
		/// <value>
		/// The maximum <see cref="VariantSize"/> when using a Simplified layout mode.
		/// The default value is <see cref="VariantSize.Small"/>.
		/// </value>
		public VariantSize MaxSimplifiedVariantSize {
			get => maxSimplifiedVariantSize;
			set {
				if (maxSimplifiedVariantSize != value) {
					maxSimplifiedVariantSize = value;
					this.NotifyPropertyChanged(nameof(MaxSimplifiedVariantSize));
				}
			}
		}

		/// <inheritdoc cref="BarButtonViewModel.MediumImageSource"/>
		public ImageSource MediumImageSource {
			get => mediumImageSource;
			set {
				if (mediumImageSource != value) {
					mediumImageSource = value;
					this.NotifyPropertyChanged(nameof(MediumImageSource));
				}
			}
		}
		
		/// <summary>
		/// Gets the collection of menu items that appear within the popup.
		/// </summary>
		/// <value>The collection of menu items that appear within the popup.</value>
		public ObservableCollection<object> MenuItems { get; } = new ObservableCollection<object>();
		
		/// <summary>
		/// Gets or sets a <see cref="ControlResizeMode"/> that indicates how the popup menu can resize.
		/// </summary>
		/// <value>
		/// A <see cref="ControlResizeMode"/> that indicates how the popup menu can resize.
		/// The default value is <see cref="ControlResizeMode.None"/>.
		/// </value>
		public ControlResizeMode MenuResizeMode {
			get => menuResizeMode;
			set {
				if (menuResizeMode != value) {
					menuResizeMode = value;
					this.NotifyPropertyChanged(nameof(MenuResizeMode));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the minimum number of columns to use when in-ribbon with a <see cref="VariantSize.Large"/> variant size.
		/// </summary>
		/// <value>
		/// The minimum number of columns to use when in-ribbon with a <see cref="VariantSize.Large"/> variant size.
		/// The default value is <c>5</c>.
		/// </value>
		public int MinLargeRibbonColumnCount {
			get => minLargeRibbonColumnCount;
			set {
				if (minLargeRibbonColumnCount != value) {
					minLargeRibbonColumnCount = value;
					this.NotifyPropertyChanged(nameof(MinLargeRibbonColumnCount));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the minimum number of columns to use when in-ribbon with a <see cref="VariantSize.Medium"/> variant size.
		/// </summary>
		/// <value>
		/// The minimum number of columns to use when in-ribbon with a <see cref="VariantSize.Medium"/> variant size.
		/// The default value is <c>3</c>.
		/// </value>
		public int MinMediumRibbonColumnCount {
			get => minMediumRibbonColumnCount;
			set {
				if (minMediumRibbonColumnCount != value) {
					minMediumRibbonColumnCount = value;
					this.NotifyPropertyChanged(nameof(MinMediumRibbonColumnCount));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the minimum number of columns when in a menu.
		/// </summary>
		/// <value>
		/// The minimum number of columns when in a menu.
		/// The default value is <c>1</c>.
		/// </value>
		public int MinMenuColumnCount {
			get => minMenuColumnCount;
			set {
				if (minMenuColumnCount != value) {
					minMenuColumnCount = value;
					this.NotifyPropertyChanged(nameof(MinMenuColumnCount));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the selected item.
		/// </summary>
		/// <value>The selected item.</value>
		public BarGalleryItemViewModelBase SelectedItem {
			get => selectedItem;
			set {
				if (selectedItem != value) {
					selectedItem = value;
					this.NotifyPropertyChanged(nameof(SelectedItem));
				}
			}
		}
		
		/// <summary>
		/// Selects an item in the gallery that matches the predicate., 
		/// </summary>
		/// <typeparam name="T">The type of <see cref="BarGalleryItemViewModelBase"/> to examine.</typeparam>
		/// <param name="predicate">A predicate that determines when an item matches criteria.</param>
		public void SelectItemByValueMatch<T>(Func<T, bool> predicate) where T : BarGalleryItemViewModelBase {
			if (predicate == null)
				throw new ArgumentNullException(nameof(predicate));

			var newSelectedItem = this.Items.OfType<T>().FirstOrDefault(predicate);
			this.SelectedItem = newSelectedItem;
		}
		
		/// <summary>
		/// Gets or sets whether to use an accented item border for gallery items, common when they have vibrant content such as color swatches.
		/// </summary>
		/// <value>
		/// <c>true</c> if an accented item border should be used for gallery items; otherwise, <c>false</c>.
		/// The default value is <c>false</c>.
		/// </value>
		public bool UseAccentedItemBorder {
			get => useAccentedItemBorder;
			set {
				if (useAccentedItemBorder != value) {
					useAccentedItemBorder = value;
					this.NotifyPropertyChanged(nameof(UseAccentedItemBorder));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets whether to use a menu item appearance for gallery items, common for single-column menu galleries.
		/// </summary>
		/// <value>
		/// <c>true</c> if gallery items should use a menu item appearance; otherwise, <c>false</c>.
		/// The default value is <c>false</c>.
		/// </value>
		public bool UseMenuItemAppearance {
			get => useMenuItemAppearance;
			set {
				if (useMenuItemAppearance != value) {
					useMenuItemAppearance = value;
					this.NotifyPropertyChanged(nameof(UseMenuItemAppearance));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets whether to align gallery items in a menu so that they indent past the menu's icon column.
		/// </summary>
		/// <value>
		/// <c>true</c> if gallery items should be indented past the menu's icon column; otherwise, <c>false</c>.
		/// The default value is <c>false</c>.
		/// </value>
		public bool UseMenuItemIndent {
			get => useMenuItemIndent;
			set {
				if (useMenuItemIndent != value) {
					useMenuItemIndent = value;
					this.NotifyPropertyChanged(nameof(UseMenuItemIndent));
				}
			}
		}
		
	}

}
