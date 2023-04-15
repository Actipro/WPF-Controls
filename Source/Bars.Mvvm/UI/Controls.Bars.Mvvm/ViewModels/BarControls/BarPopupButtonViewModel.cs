using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a popup button control within a bar control.
	/// </summary>
	[ContentProperty(nameof(MenuItems))]
	public class BarPopupButtonViewModel : BarKeyedObjectViewModelBase, IHasVariantImages {

		private bool canCloneToRibbonQuickAccessToolBar = true;
		private ICommand command;
		private object commandParameter;
		private string description;
		private string keyTipText;
		private string label;
		private ImageSource largeImageSource;
		private ImageSource mediumImageSource;
		private ICommand popupOpeningCommand;
		private ImageSource smallImageSource;
		private string title;
		private ItemCollapseBehavior toolBarItemCollapseBehavior = ItemCollapseBehavior.Default;
		private ItemVariantBehavior toolBarItemVariantBehavior = ItemVariantBehavior.AlwaysSmall;
		private bool useLargeMenuItem;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarButtonViewModel()"/>
		public BarPopupButtonViewModel()  // Parameterless constructor required for XAML support
			: this(key: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string)"/>
		public BarPopupButtonViewModel(string key)
			: this(key, label: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string)"/>
		public BarPopupButtonViewModel(string key, string label)
			: this(key, label, keyTipText: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, string)"/>
		public BarPopupButtonViewModel(string key, string label, string keyTipText)
			: this(key, label, keyTipText, command: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, string, ICommand)"/>
		protected BarPopupButtonViewModel(string key, string label, string keyTipText, ICommand command)
			: base(key) {

			// NOTE: This class has an ICommand, but it is primarily only used by derived classes and
			//		 is why this overload is protected (instead of public)

			this.label = label ?? LabelGenerator.FromCommand(command) ?? LabelGenerator.FromKey(key);
			this.keyTipText = keyTipText ?? KeyTipTextGenerator.FromCommand(command) ?? KeyTipTextGenerator.FromLabel(this.label);
			this.command = command;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <inheritdoc cref="BarButtonViewModel.CanCloneToRibbonQuickAccessToolBar"/>
		public bool CanCloneToRibbonQuickAccessToolBar {
			get => canCloneToRibbonQuickAccessToolBar;
			set {
				if (canCloneToRibbonQuickAccessToolBar != value) {
					canCloneToRibbonQuickAccessToolBar = value;
					this.NotifyPropertyChanged(nameof(CanCloneToRibbonQuickAccessToolBar));
				}
			}
		}

		/// <inheritdoc cref="BarButtonViewModel.Command"/>
		public ICommand Command {
			get => command;
			set {
				if (command != value) {
					command = value;
					this.NotifyPropertyChanged(nameof(Command));
				}
			}
		}

		/// <inheritdoc cref="BarButtonViewModel.CommandParameter"/>
		public object CommandParameter {
			get => commandParameter;
			set {
				if (commandParameter != value) {
					commandParameter = value;
					this.NotifyPropertyChanged(nameof(CommandParameter));
				}
			}
		}

		/// <inheritdoc cref="BarButtonViewModel.Description"/>
		public string Description {
			get => description;
			set {
				if (description != value) {
					description = value;
					this.NotifyPropertyChanged(nameof(Description));
				}
			}
		}

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

		/// <inheritdoc cref="BarButtonViewModel.Label"/>
		public string Label {
			get => label;
			set {
				if (label != value) {
					label = value;
					this.NotifyPropertyChanged(nameof(Label));
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
		/// Gets or sets the <see cref="ICommand"/> that executes before the button's popup is opened, allowing its items to be customized in MVVM scenarios.
		/// </summary>
		/// <value>The <see cref="ICommand"/> that executes before the button's popup is opened, allowing its items to be customized in MVVM scenarios.</value>
		public ICommand PopupOpeningCommand {
			get => popupOpeningCommand;
			set {
				if (popupOpeningCommand != value) {
					popupOpeningCommand = value;
					this.NotifyPropertyChanged(nameof(PopupOpeningCommand));
				}
			}
		}

		/// <inheritdoc cref="BarButtonViewModel.SmallImageSource"/>
		public ImageSource SmallImageSource {
			get => smallImageSource;
			set {
				if (smallImageSource != value) {
					smallImageSource = value;
					this.NotifyPropertyChanged(nameof(SmallImageSource));
				}
			}
		}
		
		/// <inheritdoc cref="BarButtonViewModel.Title"/>
		public string Title {
			get => title;
			set {
				if (title != value) {
					title = value;
					this.NotifyPropertyChanged(nameof(Title));
				}
			}
		}
		
		/// <inheritdoc cref="BarButtonViewModel.ToolBarItemCollapseBehavior"/>
		public ItemCollapseBehavior ToolBarItemCollapseBehavior {
			get => toolBarItemCollapseBehavior;
			set {
				if (toolBarItemCollapseBehavior != value) {
					toolBarItemCollapseBehavior = value;
					this.NotifyPropertyChanged(nameof(ToolBarItemCollapseBehavior));
				}
			}
		}

		/// <inheritdoc cref="BarButtonViewModel.ToolBarItemVariantBehavior"/>
		public ItemVariantBehavior ToolBarItemVariantBehavior {
			get => toolBarItemVariantBehavior;
			set {
				if (toolBarItemVariantBehavior != value) {
					toolBarItemVariantBehavior = value;
					this.NotifyPropertyChanged(nameof(ToolBarItemVariantBehavior));
				}
			}
		}

		/// <inheritdoc cref="BarButtonViewModel.UseLargeMenuItem"/>
		public bool UseLargeMenuItem {
			get => useLargeMenuItem;
			set {
				if (useLargeMenuItem != value) {
					useLargeMenuItem = value;
					this.NotifyPropertyChanged(nameof(UseLargeMenuItem));
				}
			}
		}

	}

}
