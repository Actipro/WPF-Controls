using ActiproSoftware.Windows.Themes;
using System.Collections.ObjectModel;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a dockable toolbar host control.
	/// </summary>
	public class DockableToolBarHostViewModel : ObservableObjectBase, IHasTag {

		private bool canToolBarsFloat = true;
		private VariantCollection controlVariants;
		private double lineSpacing = 1.0;
		private double toolBarItemSpacing = 1.0;
		private double toolBarSpacing = 1.0;
		private BarControlTemplateSelector itemContainerTemplateSelector = new BarControlTemplateSelector();
		private object tag;
		private bool toolBarsHaveGrippers = true;
		private bool toolBarsHaveOptionsButtons = true;
		private UserInterfaceDensity userInterfaceDensity = UserInterfaceDensity.Compact;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets whether toolbars can float.
		/// </summary>
		/// <value>
		/// <c>true</c> if toolbars can float; otherwise, <c>false</c>.
		/// The default value is <c>true</c>.
		/// </value>
		public bool CanToolBarsFloat {
			get => canToolBarsFloat;
			set {
				if (canToolBarsFloat != value) {
					canToolBarsFloat = value;
					this.NotifyPropertyChanged(nameof(CanToolBarsFloat));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the collection of variant size transitions to apply to all controls within the toolbars.
		/// </summary>
		/// <value>The collection of variant size transitions to apply to all controls within the toolbars.</value>
		public VariantCollection ControlVariants {
			get => controlVariants;
			set {
				if (controlVariants != value) {
					controlVariants = value;
					this.NotifyPropertyChanged(nameof(ControlVariants));
				}
			}
		}
		
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
		
		/// <summary>
		/// Gets or sets the spacing between lines.
		/// </summary>
		/// <value>
		/// The spacing between lines.
		/// The default value is <c>1.0</c>.
		/// </value>
		public double LineSpacing {
			get => lineSpacing;
			set {
				if (lineSpacing != value) {
					lineSpacing = value;
					this.NotifyPropertyChanged(nameof(LineSpacing));
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

		/// <summary>
		/// Gets or sets the amount of spacing between toolbar items.
		/// </summary>
		/// <value>
		/// The amount of spacing between toolbar items.
		/// The default value is <c>1.0</c>.
		/// </value>
		public double ToolBarItemSpacing {
			get => toolBarItemSpacing;
			set {
				if (toolBarItemSpacing != value) {
					toolBarItemSpacing = value;
					this.NotifyPropertyChanged(nameof(ToolBarItemSpacing));
				}
			}
		}

		/// <summary>
		/// Gets the collection of dockable toolbars managed by the host.
		/// </summary>
		/// <value>The collection of dockable toolbars managed by the host.</value>
		public ObservableCollection<DockableToolBarViewModel> ToolBars { get; } = new ObservableCollection<DockableToolBarViewModel>();
		
		/// <summary>
		/// Gets or sets the spacing between toolbars on the same line.
		/// </summary>
		/// <value>
		/// The spacing between toolbars on the same line.
		/// The default value is <c>1.0</c>.
		/// </value>
		public double ToolBarSpacing {
			get => toolBarSpacing;
			set {
				if (toolBarSpacing != value) {
					toolBarSpacing = value;
					this.NotifyPropertyChanged(nameof(ToolBarSpacing));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the default setting for whether toolbars have grippers.
		/// </summary>
		/// <value>
		/// <c>true</c> if toolbars have grippers by default; otherwise, <c>false</c>.
		/// The default value is <c>true</c>.
		/// </value>
		public bool ToolBarsHaveGrippers {
			get => toolBarsHaveGrippers;
			set {
				if (toolBarsHaveGrippers != value) {
					toolBarsHaveGrippers = value;
					this.NotifyPropertyChanged(nameof(ToolBarsHaveGrippers));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the default setting for whether toolbars have options buttons.
		/// </summary>
		/// <value>
		/// <c>true</c> if toolbars have options buttons by default; otherwise, <c>false</c>.
		/// The default value is <c>true</c>.
		/// </value>
		public bool ToolBarsHaveOptionsButtons {
			get => toolBarsHaveOptionsButtons;
			set {
				if (toolBarsHaveOptionsButtons != value) {
					toolBarsHaveOptionsButtons = value;
					this.NotifyPropertyChanged(nameof(ToolBarsHaveOptionsButtons));
				}
			}
		}
		
		/// <inheritdoc/>
		public override string ToString() {
			return $"{this.GetType().FullName}[{this.ToolBars.Count} toolbars]";
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
