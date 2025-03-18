using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using System.Collections;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.ComboBoxAndEditors {

	/// <summary>
	/// Represents a view model for an auto-complete box control within a bar control.
	/// </summary>
	public class AutoCompleteBoxViewModel : BarKeyedObjectViewModelBase {

		private string description;
		private bool hasClearButton;
		private IEnumerable itemsSource;
		private string itemsSourceDisplayMemberPath;
		private string itemsSourceTextMemberPath;
		private string label;
		private string popupHeader;
		private double requestedWidth = 110.0;
		private object selectedItem;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public AutoCompleteBoxViewModel()  // Parameterless constructor required for XAML support
			: this(key: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key.  The label is auto-generated.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		public AutoCompleteBoxViewModel(string key)
			: this(key, label: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key and label.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="label">The text label to display, which is auto-generated from the <paramref name="key"/> if <c>null</c>.</param>
		public AutoCompleteBoxViewModel(string key, string label) 
			: base(key) {

			this.label = label ?? BarControlService.LabelGenerator.FromKey(key);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

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
		/// Gets or sets if the clear button is displayed.
		/// </summary>
		/// <value><c>true</c> if the clear button is displayed; otherwise, <c>false</c>.</value>
		public bool HasClearButton {
			get => hasClearButton;
			set {
				if (hasClearButton != value) {
					hasClearButton = value;
					this.NotifyPropertyChanged(nameof(HasClearButton));
				}
			}
		}

		/// <summary>
		/// Gets or sets the source of items to be auto-completed.
		/// </summary>
		/// <value>The items source.</value>
		public IEnumerable ItemsSource {
			get => itemsSource;
			set {
				if (itemsSource != value) {
					itemsSource = value;
					this.NotifyPropertyChanged(nameof(ItemsSource));
				}
			}
		}

		/// <summary>
		/// Gets or sets a path to a value on the source object to serve as the visual representation of the object.
		/// </summary>
		/// <value>A path to a value on the source object to serve as the visual representation of the object.</value>
		public string ItemsSourceDisplayMemberPath {
			get => itemsSourceDisplayMemberPath;
			set {
				if (itemsSourceDisplayMemberPath != value) {
					itemsSourceDisplayMemberPath = value;
					this.NotifyPropertyChanged(nameof(ItemsSourceDisplayMemberPath));
				}
			}
		}

		/// <summary>
		/// Gets or sets the property path that is used to get the value for display in the text box portion of the control, when an item is selected.
		/// </summary>
		/// <value>The property path that is used to get the value for display in the text box portion of the control, when an item is selected.</value>
		public string ItemsSourceTextMemberPath {
			get => itemsSourceTextMemberPath;
			set {
				if (itemsSourceTextMemberPath != value) {
					itemsSourceTextMemberPath = value;
					this.NotifyPropertyChanged(nameof(ItemsSourceTextMemberPath));
				}
			}
		}

		/// <summary>
		/// Gets or sets the header to be displayed in the popup.
		/// </summary>
		/// <value>The header to be displayed in the popup.</value>
		public string PopupHeader {
			get => popupHeader;
			set {
				if (popupHeader != value) {
					popupHeader = value;
					this.NotifyPropertyChanged(nameof(PopupHeader));
				}
			}
		}

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
		/// Gets or sets the currently selected item.
		/// </summary>
		/// <value>The currently selected item.</value>
		public object SelectedItem {
			get => selectedItem;
			set {
				if (selectedItem != value) {
					selectedItem = value;
					this.NotifyPropertyChanged(nameof(SelectedItem));
				}
			}
		}
		
	}

}
