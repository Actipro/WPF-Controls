﻿using ActiproSoftware.Windows.Themes;
using System.Collections.ObjectModel;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a standalone toolbar control.
	/// </summary>
	public class StandaloneToolBarViewModel : ObservableObjectBase, IHasTag {

		private bool isVisible = true;
		private BarControlTemplateSelector itemContainerTemplateSelector = new BarControlTemplateSelector();
		private object tag;
		private UserInterfaceDensity userInterfaceDensity;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarButtonViewModel.IsVisible"/>
		public bool IsVisible {
			get => isVisible;
			set {
				if (isVisible != value) {
					isVisible = value;
					this.NotifyPropertyChanged(nameof(IsVisible));
				}
			}
		}

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
