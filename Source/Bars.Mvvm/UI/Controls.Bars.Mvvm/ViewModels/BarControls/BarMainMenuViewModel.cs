using System.Collections.ObjectModel;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a main menu control.
	/// </summary>
	public class BarMainMenuViewModel : ObservableObjectBase, IHasTag {

		private BarControlTemplateSelector itemContainerTemplateSelector = new BarControlTemplateSelector();
		private object tag;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the collection of items in the control.
		/// </summary>
		/// <value>The collection of items in the control.</value>
		public ObservableCollection<object> Items { get; } = new ObservableCollection<object>();

		/// <summary>
		/// Gets or sets the <see cref="BarControlTemplateSelector"/> that creates UI controls for bar control view models.
		/// </summary>
		/// <value>The <see cref="BarControlTemplateSelector"/> that creates UI controls for bar control view models.</value>
		public BarControlTemplateSelector ItemContainerTemplateSelector {
			get => itemContainerTemplateSelector;
			set {
				if (itemContainerTemplateSelector != value) {
					itemContainerTemplateSelector = value;
					this.NotifyPropertyChanged(nameof(ItemContainerTemplateSelector));
				}
			}
		}

		/// <inheritdoc cref="IHasTag.Tag"/>
		public object Tag {
			get => tag;
			set {
				if (tag != value) {
					tag = value;
					this.NotifyPropertyChanged(nameof(Tag));
				}
			}
		}

		/// <inheritdoc/>
		public override string ToString() {
			return $"{this.GetType().FullName}[{this.Items.Count} items]";
		}

	}

}
