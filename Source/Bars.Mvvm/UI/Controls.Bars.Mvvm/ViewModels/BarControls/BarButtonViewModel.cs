using System;
using System.Windows.Input;
using System.Windows.Media;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a regular button control within a bar control.
	/// </summary>
	public class BarButtonViewModel : BarKeyedObjectViewModelBase, IHasVariantImages {

		// NOTE: Documentation comments on the members of this type are referenced via inheritdoc and reused throughout other view model types

		private bool canCloneToRibbonQuickAccessToolBar = true;
		private ICommand command;
		private object commandParameter;
		private string description;
		private string inputGestureText;
		private bool isInputGestureTextVisible = true;
		private string keyTipText;
		private string label;
		private ImageSource largeImageSource;
		private ImageSource mediumImageSource;
		private ImageSource smallImageSource;
		private bool staysOpenOnClick;
		private string title;
		private ItemCollapseBehavior toolBarItemCollapseBehavior = ItemCollapseBehavior.Default;
		private ItemVariantBehavior toolBarItemVariantBehavior = ItemVariantBehavior.AlwaysSmall;
		private bool useLargeMenuItem;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public BarButtonViewModel()  // Parameterless constructor required for XAML support
			: this(key: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key.  The label and key tip text are auto-generated.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		public BarButtonViewModel(string key)
			: this(key, label: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key and label.  The key tip text is auto-generated.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="label">The text label to display, which is auto-generated from the <paramref name="key"/> if <c>null</c>.</param>
		public BarButtonViewModel(string key, string label) 
			: this(key, label, keyTipText: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key, label, and key tip text.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="label">The text label to display, which is auto-generated from the <paramref name="key"/> if <c>null</c>.</param>
		/// <param name="keyTipText">The key tip text, which is auto-generated from the <paramref name="label"/> if <c>null</c>.</param>
		public BarButtonViewModel(string key, string label, string keyTipText)
			: this(key, label, keyTipText, command: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified <see cref="RoutedCommand"/>, also used to auto-generate a key, label, and key tip text.
		/// </summary>
		/// <param name="routedCommand">The command to attach to the control.</param>
		public BarButtonViewModel(RoutedCommand routedCommand)
			: this(routedCommand?.Name, routedCommand) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key and command.  The label and key tip text are auto-generated.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="command">The command to attach to the control.</param>
		public BarButtonViewModel(string key, ICommand command)
			: this(key, label: null, command) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key, label, and command.  The key tip text is auto-generated.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="label">The text label to display, which is auto-generated from the <paramref name="command"/> or <paramref name="key"/> if <c>null</c>.</param>
		/// <param name="command">The command to attach to the control.</param>
		public BarButtonViewModel(string key, string label, ICommand command)
			: this(key, label, keyTipText: null, command) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key, label, key tip text, and command.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="label">The text label to display, which is auto-generated from the <paramref name="command"/> or <paramref name="key"/> if <c>null</c>.</param>
		/// <param name="keyTipText">The key tip text, which is auto-generated from the <paramref name="command"/> or <paramref name="label"/> if <c>null</c>.</param>
		/// <param name="command">The command to attach to the control.</param>
		public BarButtonViewModel(string key, string label, string keyTipText, ICommand command)
			: base(key) {

			this.label = label ?? BarControlService.LabelGenerator.FromCommand(command) ?? BarControlService.LabelGenerator.FromKey(key);
			this.keyTipText = keyTipText ?? BarControlService.KeyTipTextGenerator.FromCommand(command) ?? BarControlService.KeyTipTextGenerator.FromLabel(this.label);
			this.command = command;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets whether the control can be cloned to the ribbon quick-access toolbar.
		/// </summary>
		/// <value>
		/// <c>true</c> if the control can be cloned to the ribbon quick-access toolbar; otherwise, <c>false</c>.
		/// The default value is <c>true</c>.
		/// </value>
		public bool CanCloneToRibbonQuickAccessToolBar {
			get => canCloneToRibbonQuickAccessToolBar;
			set {
				if (canCloneToRibbonQuickAccessToolBar != value) {
					canCloneToRibbonQuickAccessToolBar = value;
					this.NotifyPropertyChanged(nameof(CanCloneToRibbonQuickAccessToolBar));
				}
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="ICommand"/> to attach to the control.
		/// </summary>
		/// <value>The <see cref="ICommand"/> to attach to the control.</value>
		public ICommand Command {
			get => command;
			set {
				if (command != value) {
					command = value;
					this.NotifyPropertyChanged(nameof(Command));
				}
			}
		}

		/// <summary>
		/// Gets or sets the parameter to pass to the <see cref="Command"/> property.
		/// </summary>
		/// <value>The parameter to pass to the <see cref="Command"/> property.</value>
		public object CommandParameter {
			get => commandParameter;
			set {
				if (commandParameter != value) {
					commandParameter = value;
					this.NotifyPropertyChanged(nameof(CommandParameter));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the text description to display in screen tips.
		/// </summary>
		/// <value>The text description to display in screen tips.</value>
		public string Description {
			get => description;
			set {
				if (description != value) {
					description = value;
					this.NotifyPropertyChanged(nameof(Description));
				}
			}
		}

		/// <summary>
		/// Gets or sets the input gesture text to display in menu items and screen tips, which overrides any auto-generated input gesture text from the <see cref="Command"/>.
		/// </summary>
		/// <value>The input gesture text to display in menu items and screen tips.</value>
		public string InputGestureText {
			get => inputGestureText;
			set {
				if (inputGestureText != value) {
					inputGestureText = value;
					this.NotifyPropertyChanged(nameof(InputGestureText));
				}
			}
		}

		/// <summary>
		/// Gets or sets whether the input gesture text is allowed to be visible in the user interface.
		/// </summary>
		/// <value>
		/// <c>true</c> if the input gesture text is allowed to be visible in the user interface; otherwise, <c>false</c>.
		/// The default value is <c>true</c>.
		/// </value>
		public bool IsInputGestureTextVisible {
			get => isInputGestureTextVisible;
			set {
				if (isInputGestureTextVisible != value) {
					isInputGestureTextVisible = value;
					this.NotifyPropertyChanged(nameof(IsInputGestureTextVisible));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the key tip text used to access the control.
		/// </summary>
		/// <value>The key tip text used to access the control.</value>
		public string KeyTipText {
			get => keyTipText;
			set {
				if (keyTipText != value) {
					keyTipText = value;
					this.NotifyPropertyChanged(nameof(KeyTipText));
				}
			}
		}

		/// <summary>
		/// Gets or sets the text label to display.
		/// </summary>
		/// <value>The text label to display.</value>
		public string Label {
			get => label;
			set {
				if (label != value) {
					label = value;
					this.NotifyPropertyChanged(nameof(Label));
				}
			}
		}

		/// <inheritdoc cref="IHasVariantImages.LargeImageSource"/>
		public ImageSource LargeImageSource {
			get => largeImageSource;
			set {
				if (largeImageSource != value) {
					largeImageSource = value;
					this.NotifyPropertyChanged(nameof(LargeImageSource));
				}
			}
		}

		/// <inheritdoc cref="IHasVariantImages.MediumImageSource"/>
		public ImageSource MediumImageSource {
			get => mediumImageSource;
			set {
				if (mediumImageSource != value) {
					mediumImageSource = value;
					this.NotifyPropertyChanged(nameof(MediumImageSource));
				}
			}
		}

		/// <inheritdoc cref="IHasVariantImages.SmallImageSource"/>
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
		/// Gets or sets whether menus should try and remain open when the control is clicked.
		/// </summary>
		/// <value>
		/// <c>true</c> if menus should try and remain open when the control is clicked; otherwise, <c>false</c>.
		/// The default value is <c>false</c>.
		/// </value>
		public bool StaysOpenOnClick {
			get => staysOpenOnClick;
			set {
				if (staysOpenOnClick != value) {
					staysOpenOnClick = value;
					this.NotifyPropertyChanged(nameof(StaysOpenOnClick));
				}
			}
		}

		/// <summary>
		/// Gets or sets the string title, which can override the <see cref="Label"/> when displayed in screen tips and customization UI.
		/// </summary>
		/// <value>The string title.</value>
		public string Title {
			get => title;
			set {
				if (title != value) {
					title = value;
					this.NotifyPropertyChanged(nameof(Title));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the <see cref="ItemCollapseBehavior"/> for the control when in a ribbon using Simplified layout mode.
		/// </summary>
		/// <value>
		/// The <see cref="ItemCollapseBehavior"/> for the control when in a ribbon using Simplified layout mode.
		/// The default value is <c>Default</c>.
		/// </value>
		public ItemCollapseBehavior ToolBarItemCollapseBehavior {
			get => toolBarItemCollapseBehavior;
			set {
				if (toolBarItemCollapseBehavior != value) {
					toolBarItemCollapseBehavior = value;
					this.NotifyPropertyChanged(nameof(ToolBarItemCollapseBehavior));
				}
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="ItemVariantBehavior"/> for the control when in a toolbar, which also applies when in a ribbon using Simplified layout mode.
		/// </summary>
		/// <value>
		/// The <see cref="ItemVariantBehavior"/> for the control when in a toolbar, which also applies when in a ribbon using Simplified layout mode.
		/// The default value is <c>AlwaysSmall</c>.
		/// </value>
		public ItemVariantBehavior ToolBarItemVariantBehavior {
			get => toolBarItemVariantBehavior;
			set {
				if (toolBarItemVariantBehavior != value) {
					toolBarItemVariantBehavior = value;
					this.NotifyPropertyChanged(nameof(ToolBarItemVariantBehavior));
				}
			}
		}

		/// <summary>
		/// Gets or sets whether to use a large size when the control is a menu item.
		/// </summary>
		/// <value>
		/// <c>true</c> if a large size should be used when the control is a menu item; otherwise, <c>false</c>.
		/// The default value is <c>false</c>.
		/// </value>
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
