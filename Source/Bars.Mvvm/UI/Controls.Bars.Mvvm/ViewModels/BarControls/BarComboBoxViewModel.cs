using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a combobox control within a bar control.
	/// </summary>
	public class BarComboBoxViewModel : BarKeyedObjectViewModelBase {

		private bool canCloneToRibbonQuickAccessToolBar = true;
		private ICommand command;
		private string description;
		private bool isEditable = true;
		private bool isReadOnly;
		private bool isStarSizingAllowed;
		private bool isTextSearchCaseSensitive;
		private bool isTextSearchEnabled = true;
		private string keyTipText;
		private string label;
		private double requestedWidth = 110.0;
		private string text;
		private string textPath;
		private string title;
		private ICommand unmatchedTextCommand;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <inheritdoc cref="BarButtonViewModel()"/>
		public BarComboBoxViewModel() { }  // Parameterless constructor required for XAML support

		/// <inheritdoc cref="BarButtonViewModel(string)"/>
		public BarComboBoxViewModel(string key)
			: this(key, label: null) { }
		
		/// <summary>
		/// Initializes a new instance of the class with the specified key.  The label and key tip text are auto-generated.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="menuGallery">The <see cref="BarGalleryViewModel"/> for the menu gallery in the drop-down.</param>
		public BarComboBoxViewModel(string key, BarGalleryViewModel menuGallery)
			: this(key, label: null, menuGallery) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string)"/>
		public BarComboBoxViewModel(string key, string label)
			: this(key, label, keyTipText: null) { }
		
		/// <summary>
		/// Initializes a new instance of the class with the specified key and label.  The key tip text is auto-generated.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="label">The text label to display, which is auto-generated from the <paramref name="key"/> if <c>null</c>.</param>
		/// <param name="menuGallery">The <see cref="BarGalleryViewModel"/> for the menu gallery in the drop-down.</param>
		public BarComboBoxViewModel(string key, string label, BarGalleryViewModel menuGallery)
			: this(key, label, keyTipText: null, menuGallery) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, string)"/>
		public BarComboBoxViewModel(string key, string label, string keyTipText)
			: this(key, label, keyTipText, command: null) { }
		
		/// <summary>
		/// Initializes a new instance of the class with the specified key, label, and key tip text.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="label">The text label to display, which is auto-generated from the <paramref name="key"/> if <c>null</c>.</param>
		/// <param name="keyTipText">The key tip text, which is auto-generated from the <paramref name="label"/> if <c>null</c>.</param>
		/// <param name="menuGallery">The <see cref="BarGalleryViewModel"/> for the menu gallery in the drop-down.</param>
		public BarComboBoxViewModel(string key, string label, string keyTipText, BarGalleryViewModel menuGallery)
			: this(key, label, keyTipText, command: null, menuGallery) { }
		
		/// <inheritdoc cref="BarButtonViewModel(RoutedCommand)"/>
		public BarComboBoxViewModel(RoutedCommand routedCommand)
			: this(routedCommand?.Name, routedCommand) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified <see cref="RoutedCommand"/>, also used to auto-generate a key, label, and key tip text.
		/// </summary>
		/// <param name="routedCommand">The command to attach to the control.</param>
		/// <param name="menuGallery">The <see cref="BarGalleryViewModel"/> for the menu gallery in the drop-down.</param>
		public BarComboBoxViewModel(RoutedCommand routedCommand, BarGalleryViewModel menuGallery)
			: this(routedCommand?.Name, routedCommand, menuGallery) { }

		/// <inheritdoc cref="BarButtonViewModel(string, ICommand)"/>
		public BarComboBoxViewModel(string key, ICommand command)
			: this(key, label: null, keyTipText: null, command, menuGallery: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, ICommand)"/>
		public BarComboBoxViewModel(string key, string label, ICommand command)
			: this(key, label, keyTipText: null, command) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, string, ICommand)"/>
		public BarComboBoxViewModel(string key, string label, string keyTipText, ICommand command)
			: this(key, label, keyTipText, command, menuGallery: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key and command.  The label and key tip text are auto-generated.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="command">The command to attach to the control.</param>
		/// <param name="menuGallery">The <see cref="BarGalleryViewModel"/> for the menu gallery in the drop-down.</param>
		public BarComboBoxViewModel(string key, ICommand command, BarGalleryViewModel menuGallery)
			: this(key, label: null, keyTipText: null, command, menuGallery) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key, label, and command.  The key tip text is auto-generated.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="label">The text label to display, which is auto-generated from the <paramref name="command"/> or <paramref name="key"/> if <c>null</c>.</param>
		/// <param name="command">The command to attach to the control.</param>
		/// <param name="menuGallery">The <see cref="BarGalleryViewModel"/> for the menu gallery in the drop-down.</param>
		public BarComboBoxViewModel(string key, string label, ICommand command, BarGalleryViewModel menuGallery)
			: this(key, label, keyTipText: null, command, menuGallery) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key, label, key tip text, and command.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="label">The text label to display, which is auto-generated from the <paramref name="command"/> or <paramref name="key"/> if <c>null</c>.</param>
		/// <param name="keyTipText">The key tip text, which is auto-generated from the <paramref name="command"/> or <paramref name="label"/> if <c>null</c>.</param>
		/// <param name="command">The command to attach to the control.</param>
		/// <param name="menuGallery">The <see cref="BarGalleryViewModel"/> for the menu gallery in the drop-down.</param>
		public BarComboBoxViewModel(string key, string label, string keyTipText, ICommand command, BarGalleryViewModel menuGallery)
			: base(key) {

			this.label = label ?? LabelGenerator.FromCommand(command) ?? LabelGenerator.FromKey(key);
			this.keyTipText = keyTipText ?? KeyTipTextGenerator.FromCommand(command) ?? KeyTipTextGenerator.FromLabel(this.label);
			this.command = command;

			if (menuGallery != null)
				this.MenuItems.Add(menuGallery);
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

		/// <summary>
		/// Gets the first <see cref="BarGalleryViewModel"/> found in the <see cref="MenuItems"/> collection.
		/// </summary>
		/// <value>The first <see cref="BarGalleryViewModel"/> found in the <see cref="MenuItems"/> collection.</value>
		protected BarGalleryViewModel FirstMenuGallery
			=> this.MenuItems.OfType<BarGalleryViewModel>().FirstOrDefault();
		
		/// <summary>
		/// Gets or sets whether the combobox is editable.
		/// </summary>
		/// <value>
		/// <c>true</c> if the combobox is editable; otherwise, <c>false</c>.
		/// The default value is <c>true</c>.
		/// </value>
		public bool IsEditable {
			get => isEditable;
			set {
				if (isEditable != value) {
					isEditable = value;
					this.NotifyPropertyChanged(nameof(IsEditable));
				}
			}
		}

		/// <summary>
		/// Gets or sets whether the combobox is read-only.
		/// </summary>
		/// <value>
		/// <c>true</c> if the combobox is read-only; otherwise, <c>false</c>.
		/// The default value is <c>false</c>.
		/// </value>
		public bool IsReadOnly {
			get => isReadOnly;
			set {
				if (isReadOnly != value) {
					isReadOnly = value;
					this.NotifyPropertyChanged(nameof(IsReadOnly));
				}
			}
		}

		/// <summary>
		/// Gets or sets whether the control can star-size and fill available space when appropriate.
		/// </summary>
		/// <value>
		/// <c>true</c> if the control can star-size; otherwise, <c>false</c>.
		/// The default value is <c>false</c>.
		/// </value>
		public bool IsStarSizingAllowed {
			get => isStarSizingAllowed;
			set {
				if (isStarSizingAllowed != value) {
					isStarSizingAllowed = value;
					this.NotifyPropertyChanged(nameof(IsStarSizingAllowed));
				}
			}
		}

		/// <summary>
		/// Gets or sets whether case is a condition when searching for items.
		/// </summary>
		/// <value>
		/// <c>true</c> if text searches are case-sensitive; otherwise <c>false</c>.
		/// The default value is <c>false</c>.
		/// </value>
		/// <seealso cref="IsTextSearchEnabled"/>
		/// <seealso cref="UnmatchedTextCommand"/>
		public bool IsTextSearchCaseSensitive {
			get => isTextSearchCaseSensitive;
			set {
				if (isTextSearchCaseSensitive != value) {
					isTextSearchCaseSensitive = value;
					this.NotifyPropertyChanged(nameof(IsTextSearchCaseSensitive));
				}
			}
		}

		/// <summary>
		/// Gets or sets whether known items are matched when text is entered.
		/// </summary>
		/// <value>
		/// <c>true</c> if items are matched when text is entered; otherwise <c>false</c>.
		/// The default value is <c>true</c>.
		/// </value>
		/// <seealso cref="IsTextSearchCaseSensitive"/>
		/// <seealso cref="UnmatchedTextCommand"/>
		public bool IsTextSearchEnabled {
			get => isTextSearchEnabled;
			set {
				if (isTextSearchEnabled != value) {
					isTextSearchEnabled = value;
					this.NotifyPropertyChanged(nameof(IsTextSearchEnabled));
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
		/// Gets the collection of menu items that appear within the drop-down.
		/// </summary>
		/// <value>The collection of menu items that appear within the drop-down.</value>
		public ObservableCollection<object> MenuItems { get; } = new ObservableCollection<object>();

		/// <summary>
		/// Gets or sets the requested width of the control.
		/// </summary>
		/// <value>
		/// The requested width of the control.
		/// The default value is <c>110</c>.
		/// </value>
		public double RequestedWidth {
			get => requestedWidth;
			set {
				if (requestedWidth != value) {
					requestedWidth = value;
					this.NotifyPropertyChanged(nameof(RequestedWidth));
				}
			}
		}
		
		/// <summary>
		/// Selects an item in the <see cref="FirstMenuGallery"/> that matches the predicate, 
		/// alternatively setting the specified fallback <see cref="Text"/> if no match is made.
		/// </summary>
		/// <typeparam name="T">The type of <see cref="BarGalleryItemViewModelBase"/> to examine.</typeparam>
		/// <param name="predicate">A predicate that determines when an item matches criteria.</param>
		/// <param name="fallbackText">The fallback text to set to <see cref="Text"/> when there is no match.</param>
		public virtual void SelectItemByValueMatch<T>(Func<T, bool> predicate, string fallbackText) where T : BarGalleryItemViewModelBase {
			var firstMenuGallery = this.FirstMenuGallery;
			if (firstMenuGallery != null) {
				firstMenuGallery.SelectItemByValueMatch(predicate);

				if (firstMenuGallery.SelectedItem != null)
					return;
			}

			this.Text = fallbackText;
		}
		
		/// <summary>
		/// Gets or sets the text to display in the control.
		/// </summary>
		/// <value>The text to display in the control.</value>
		public string Text {
			get => text;
			set {
				if (text != value) {
					text = value;
					this.NotifyPropertyChanged(nameof(Text));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the path to a string gallery item property, corresponding to <see cref="Text"/> display and entry.
		/// </summary>
		/// <value>The path to a string gallery item property, corresponding to <see cref="Text"/> display and entry.</value>
		public string TextPath {
			get => textPath;
			set {
				if (textPath != value) {
					textPath = value;
					this.NotifyPropertyChanged(nameof(TextPath));
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

		/// <inheritdoc cref="BarComboBox.UnmatchedTextCommand"/>
		public ICommand UnmatchedTextCommand {
			get => unmatchedTextCommand;
			set {
				if (unmatchedTextCommand != value) {
					unmatchedTextCommand = value;
					this.NotifyPropertyChanged(nameof(UnmatchedTextCommand));
				}
			}
		}
		
	}

}
