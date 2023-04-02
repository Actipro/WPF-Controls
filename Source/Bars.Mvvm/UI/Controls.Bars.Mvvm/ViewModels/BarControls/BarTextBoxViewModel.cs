using System;
using System.Windows.Input;
using System.Windows.Media;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a textbox control within a bar control.
	/// </summary>
	public class BarTextBoxViewModel : BarKeyedObjectViewModelBase, IHasVariantImages {

		private bool canCloneToRibbonQuickAccessToolBar = true;
		private ICommand command;
		private object commandParameter;
		private string description;
		private bool isStarSizingAllowed;
		private string keyTipText;
		private string label;
		private string placeholderText;
		private double requestedWidth = 110.0;
		private ImageSource smallImageSource;
		private string text;
		private string title;
		private ItemCollapseBehavior toolBarItemCollapseBehavior = ItemCollapseBehavior.Default;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarButtonViewModel()"/>
		public BarTextBoxViewModel() { }  // Parameterless constructor required for XAML support

		/// <inheritdoc cref="BarButtonViewModel(string)"/>
		public BarTextBoxViewModel(string key)
			: this(key, label: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string)"/>
		public BarTextBoxViewModel(string key, string label)
			: this(key, label, keyTipText: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, string)"/>
		public BarTextBoxViewModel(string key, string label, string keyTipText)
			: this(key, label, keyTipText, command: null) { }

		/// <inheritdoc cref="BarButtonViewModel(RoutedCommand)"/>
		public BarTextBoxViewModel(RoutedCommand routedCommand)
			: this(routedCommand?.Name, routedCommand) { }

		/// <inheritdoc cref="BarButtonViewModel(string, ICommand)"/>
		public BarTextBoxViewModel(string key, ICommand command)
			: this(key, label: null, command) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, ICommand)"/>
		public BarTextBoxViewModel(string key, string label, ICommand command)
			: this(key, label, keyTipText: null, command) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, string, ICommand)"/>
		public BarTextBoxViewModel(string key, string label, string keyTipText, ICommand command)
			: base(key) {

			this.label = label ?? LabelGenerator.FromCommand(command) ?? LabelGenerator.FromKey(key);
			this.keyTipText = keyTipText ?? KeyTipTextGenerator.FromCommand(command) ?? KeyTipTextGenerator.FromLabel(this.label);
			this.command = command;
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

		/// <inheritdoc cref="BarComboBoxViewModel.IsStarSizingAllowed"/>
		public bool IsStarSizingAllowed {
			get => isStarSizingAllowed;
			set {
				if (isStarSizingAllowed != value) {
					isStarSizingAllowed = value;
					this.NotifyPropertyChanged(nameof(IsStarSizingAllowed));
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

		/// <summary>
		/// Gets or sets the placeholder text to display when the control is empty.
		/// </summary>
		/// <value>The placeholder text to display when the control is empty.</value>
		public string PlaceholderText {
			get {
				return placeholderText;
			}
			set {
				if (placeholderText != value) {
					placeholderText = value;
					this.NotifyPropertyChanged(nameof(PlaceholderText));
				}
			}
		}
		
		/// <inheritdoc cref="BarComboBoxViewModel.RequestedWidth"/>
		public double RequestedWidth {
			get {
				return requestedWidth;
			}
			set {
				if (requestedWidth != value) {
					requestedWidth = value;
					this.NotifyPropertyChanged(nameof(RequestedWidth));
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
		
		/// <summary>
		/// Gets or sets the text being edited in the control.
		/// </summary>
		/// <value>The text being edited in the control.</value>
		public string Text {
			get {
				return text;
			}
			set {
				if (text != value) {
					text = value;
					this.NotifyPropertyChanged(nameof(Text));
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

	}

}
