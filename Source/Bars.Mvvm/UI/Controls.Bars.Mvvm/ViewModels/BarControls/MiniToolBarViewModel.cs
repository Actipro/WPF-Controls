using ActiproSoftware.Windows.Themes;
using System.Collections.ObjectModel;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a mini-toolbar control.
	/// </summary>
	public class MiniToolBarViewModel : ObservableObjectBase, IHasTag {

		private bool canUseMultiRowLayout;
		private UserInterfaceDensity userInterfaceDensity = UserInterfaceDensity.Compact;
		private object tag;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets whether the items can be arranged in a multi-row layout.
		/// </summary>
		/// <value>
		/// <c>true</c> if the items can be arranged in a multi-row layout; otherwise, <c>false</c>.
		/// The default value is <c>false</c>.
		/// </value>
		public bool CanUseMultiRowLayout {
			get => canUseMultiRowLayout;
			set {
				if (canUseMultiRowLayout != value) {
					canUseMultiRowLayout = value;
					this.NotifyPropertyChanged(nameof(CanUseMultiRowLayout));
				}
			}
		}

		/// <summary>
		/// Gets the collection of items in the control.
		/// </summary>
		/// <value>The collection of items in the control.</value>
		public ObservableCollection<object> Items { get; } = new ObservableCollection<object>();

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
		
		/// <summary>
		/// Gets or sets a <see cref="Themes.UserInterfaceDensity"/> that indicates how compact or spacious the UI should appear.
		/// </summary>
		/// <value>
		/// A <see cref="Themes.UserInterfaceDensity"/> that indicates how compact or spacious the UI should appear.
		/// The default value is <see cref="UserInterfaceDensity.Compact"/>.
		/// </value>
		public UserInterfaceDensity UserInterfaceDensity {
			get => userInterfaceDensity;
			set {
				if (userInterfaceDensity != value) {
					userInterfaceDensity = value;
					this.NotifyPropertyChanged(nameof(UserInterfaceDensity));
				}
			}
		}

	}

}
