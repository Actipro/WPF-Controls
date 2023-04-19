using System.Collections.ObjectModel;
using System.Windows.Media;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a group within a ribbon tab.
	/// </summary>
	public class RibbonGroupViewModel : BarKeyedObjectViewModelBase {

		private bool canAutoCollapse = true;
		private bool canCloneToRibbonQuickAccessToolBar = true;
		private bool canUseMultiRowLayout;
		private RibbonGroupChildOverflowTarget childOverflowTarget = RibbonGroupChildOverflowTarget.Tab;
		private string collapsedButtonDescription;
		private string collapsedButtonKeyTipText;
		private string label;
		private ImageSource largeImageSource;
		private RibbonGroupLauncherButtonViewModel launcherButton;
		private ImageSource smallImageSource;
		private Int32Collection threeRowItemSortOrder;
		private string title;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public RibbonGroupViewModel()  // Parameterless constructor required for XAML support
			: this(key: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key.  The label and collapsed button key tip text are auto-generated.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		public RibbonGroupViewModel(string key)
			: this(key, label: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key and label.  The collapsed button key tip text is auto-generated.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="label">The text label to display, which is auto-generated from the <paramref name="key"/> if <c>null</c>.</param>
		public RibbonGroupViewModel(string key, string label)
			: this(key, label, collapsedButtonKeyTipText: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key, label, and key tip text.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="label">The text label to display, which is auto-generated from the <paramref name="key"/> if <c>null</c>.</param>
		/// <param name="collapsedButtonKeyTipText">The collapsed button key tip text, which is auto-generated from the <paramref name="label"/> if <c>null</c>.</param>
		public RibbonGroupViewModel(string key, string label, string collapsedButtonKeyTipText)
			: base(key) {

			this.label = label ?? LabelGenerator.FromKey(key);

			// By convention, the key tips for a collapsed RibbonGroup should start with "Z"
			if (collapsedButtonKeyTipText is null) {
				collapsedButtonKeyTipText = KeyTipTextGenerator.FromLabel(this.label);
				if (!string.IsNullOrWhiteSpace(collapsedButtonKeyTipText))
					collapsedButtonKeyTipText = "Z" + collapsedButtonKeyTipText;
			}
			this.collapsedButtonKeyTipText = collapsedButtonKeyTipText;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets whether the group can auto-collapse.
		/// </summary>
		/// <value>
		/// <c>true</c> if the group can auto-collapse; otherwise, <c>false</c>.
		/// The default value is <c>true</c>.
		/// </value>
		public bool CanAutoCollapse {
			get {
				return canAutoCollapse;
			}
			set {
				if (canAutoCollapse != value) {
					canAutoCollapse = value;
					this.NotifyPropertyChanged(nameof(CanAutoCollapse));
				}
			}
		}

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

		/// <summary>
		/// Gets or sets whether the items can be arranged in a multi-row layout.
		/// </summary>
		/// <value>
		/// <c>true</c> if the items can be arranged in a multi-row layout; otherwise, <c>false</c>.
		/// The default value is <c>false</c>.
		/// </value>
		public bool CanUseMultiRowLayout {
			get {
				return canUseMultiRowLayout;
			}
			set {
				if (canUseMultiRowLayout != value) {
					canUseMultiRowLayout = value;
					this.NotifyPropertyChanged(nameof(CanUseMultiRowLayout));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets a <see cref="RibbonGroupChildOverflowTarget"/> indicating where items overflow when in a Simplified layout mode.
		/// </summary>
		/// <value>
		/// A <see cref="RibbonGroupChildOverflowTarget"/> indicating where items overflow when in a Simplified layout mode.
		/// The default value is <see cref="RibbonGroupChildOverflowTarget.Tab"/>.
		/// </value>
		public RibbonGroupChildOverflowTarget ChildOverflowTarget {
			get {
				return childOverflowTarget;
			}
			set {
				if (childOverflowTarget != value) {
					childOverflowTarget = value;
					this.NotifyPropertyChanged(nameof(ChildOverflowTarget));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the text description to display in screen tips for the group when it is rendered as a collapsed button.
		/// </summary>
		/// <value>The text description to display in screen tips for the group when it is rendered as a collapsed button.</value>
		public string CollapsedButtonDescription {
			get {
				return collapsedButtonDescription;
			}
			set {
				if (collapsedButtonDescription != value) {
					collapsedButtonDescription = value;
					this.NotifyPropertyChanged(nameof(CollapsedButtonDescription));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the key tip text used to access the group when it is rendered as a collapsed button.
		/// </summary>
		/// <value>The key tip text used to access the group when it is rendered as a collapsed button.</value>
		public string CollapsedButtonKeyTipText {
			get {
				return collapsedButtonKeyTipText;
			}
			set {
				if (collapsedButtonKeyTipText != value) {
					collapsedButtonKeyTipText = value;
					this.NotifyPropertyChanged(nameof(CollapsedButtonKeyTipText));
				}
			}
		}
		
		/// <summary>
		/// Gets the collection of items in the control.
		/// </summary>
		/// <value>The collection of items in the control.</value>
		public ObservableCollection<object> Items { get; } = new ObservableCollection<object>();

		/// <inheritdoc cref="BarButtonViewModel.Label"/>
		public string Label {
			get {
				return label;
			}
			set {
				if (label != value) {
					label = value;
					this.NotifyPropertyChanged(nameof(Label));
				}
			}
		}
		
		/// <inheritdoc cref="BarButtonViewModel.LargeImageSource"/>
		public ImageSource LargeImageSource {
			get {
				return largeImageSource;
			}
			set {
				if (largeImageSource != value) {
					largeImageSource = value;
					this.NotifyPropertyChanged(nameof(LargeImageSource));
				}
			}
		}

		/// <summary>
		/// Gets or sets a <see cref="RibbonGroupLauncherButtonViewModel"/> for the optional launcher button.
		/// </summary>
		/// <value>A <see cref="RibbonGroupLauncherButtonViewModel"/> for the optional launcher button.</value>
		public RibbonGroupLauncherButtonViewModel LauncherButton {
			get {
				return launcherButton;
			}
			set {
				if (launcherButton != value) {
					launcherButton = value;
					this.NotifyPropertyChanged(nameof(LauncherButton));
				}
			}
		}
		
		/// <inheritdoc cref="BarButtonViewModel.SmallImageSource"/>
		public ImageSource SmallImageSource {
			get {
				return smallImageSource;
			}
			set {
				if (smallImageSource != value) {
					smallImageSource = value;
					this.NotifyPropertyChanged(nameof(SmallImageSource));
				}
			}
		}

		/// <summary>
		/// Gets or sets a collection of integers that indicates the indices of how items should be sorted when in a three-row layout.
		/// </summary>
		/// <value>A collection of integers that indicates the indices of how items should be sorted when in a three-row layout.</value>
		public Int32Collection ThreeRowItemSortOrder {
			get {
				return threeRowItemSortOrder;
			}
			set {
				if (threeRowItemSortOrder != value) {
					threeRowItemSortOrder = value;
					this.NotifyPropertyChanged(nameof(ThreeRowItemSortOrder));
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

		/// <inheritdoc/>
		public override string ToString() {
			return $"{this.GetType().FullName}[Label='{this.Label}']";
		}

	}

}
