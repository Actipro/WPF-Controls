using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a Backstage control within a ribbon.
	/// </summary>
	public class RibbonBackstageViewModel : ObservableObjectBase {

		private DataTemplateSelector contentTemplateSelector;
		private bool isOpen;
		private object selectedItem;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Closes the Backstage.
		/// </summary>
		public void Close() {
			this.IsOpen = false;
		}

		/// <summary>
		/// Gets or sets the <see cref="DataTemplateSelector"/> that picks a <see cref="System.Windows.DataTemplate"/> used to display Backstage tab content.
		/// </summary>
		/// <value>The <see cref="DataTemplateSelector"/> that picks a <see cref="System.Windows.DataTemplate"/> used to display Backstage tab content.</value>
		public DataTemplateSelector ContentTemplateSelector {
			get => contentTemplateSelector;
			set {
				if (contentTemplateSelector != value) {
					contentTemplateSelector = value;
					this.NotifyPropertyChanged(nameof(ContentTemplateSelector));
				}
			}
		}

		/// <summary>
		/// Gets or sets whether the Backstage is open.
		/// </summary>
		/// <value>
		/// <c>true</c> if the Backstage is open; otherwise, <c>false</c>.
		/// The default value is <c>false</c>.
		/// </value>
		public bool IsOpen {
			get => isOpen;
			set {
				if (isOpen != value) {
					isOpen = value;
					this.NotifyPropertyChanged(nameof(IsOpen));
				}
			}
		}
		
		/// <summary>
		/// Gets the collection of items to display in the Backstage.
		/// </summary>
		/// <value>The collection of items to display in the Backstage.</value>
		public ObservableCollection<object> Items { get; } = new ObservableCollection<object>();
		
		/// <summary>
		/// Opens the Backstage.
		/// </summary>
		public void Open() {
			this.IsOpen = true;
		}

		/// <summary>
		/// Gets or sets the Backstage's selected item, which is a tab item in the <see cref="Items"/> collection.
		/// </summary>
		/// <value>
		/// The selected item.
		/// </value>
		public object SelectedItem {
			get => selectedItem;
			set {
				if (selectedItem != value) {
					selectedItem = value;
					this.NotifyPropertyChanged(nameof(SelectedItem));
				}
			}
		}
		
		/// <inheritdoc/>
		public override string ToString() {
			return $"{this.GetType().FullName}[{this.Items.Count} items]";
		}

	}

}
