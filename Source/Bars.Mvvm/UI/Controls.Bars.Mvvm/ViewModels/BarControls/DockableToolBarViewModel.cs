using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a dockable toolbar control.
	/// </summary>
	public class DockableToolBarViewModel : BarKeyedObjectViewModelBase {

		private bool? hasGripper;
		private bool? hasOptionsButton;
		private bool isVisible = true;
		private int lineIndex;
		private double offset;
		private Dock placement = Dock.Top;
		private int sortOrder;
		private string title;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public DockableToolBarViewModel()  // Parameterless constructor required for XAML support
			: this(key: null) { }
		
		/// <summary>
		/// Initializes a new instance of the class with the specified key.  The title is auto-generated.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		public DockableToolBarViewModel(string key)
			: this(key, title: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key and title.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="title">The toolbar's title.</param>
		public DockableToolBarViewModel(string key, string title)
			: base(key) {
			this.title = title ?? BarControlService.LabelGenerator.FromKey(key);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets whether the toolbar has a gripper.
		/// </summary>
		/// <value>
		/// <c>true</c> if the toolbar has a gripper; otherwise, <c>false</c>.
		/// The default value is <c>null</c>, meaning inherit the value from the <see cref="DockableToolBarHostViewModel.ToolBarsHaveGrippers"/> property.
		/// </value>
		public bool? HasGripper {
			get => hasGripper;
			set {
				if (hasGripper != value) {
					hasGripper = value;
					this.NotifyPropertyChanged(nameof(HasGripper));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets whether the toolbar has an options button.
		/// </summary>
		/// <value>
		/// <c>true</c> if the toolbar has an options button; otherwise, <c>false</c>.
		/// The default value is <c>null</c>, meaning inherit the value from the <see cref="DockableToolBarHostViewModel.ToolBarsHaveOptionsButtons"/> property.
		/// </value>
		public bool? HasOptionsButton {
			get => hasOptionsButton;
			set {
				if (hasOptionsButton != value) {
					hasOptionsButton = value;
					this.NotifyPropertyChanged(nameof(HasOptionsButton));
				}
			}
		}
		
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
		/// Gets or sets the index of the <see cref="Placement"/> line the toolbar is within.
		/// </summary>
		/// <value>The index of the <see cref="Placement"/> line the toolbar is within.</value>
		public int LineIndex {
			get => lineIndex;
			set {
				if (lineIndex != value) {
					lineIndex = value;
					this.NotifyPropertyChanged(nameof(LineIndex));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the anchor offset of the toolbar within its line.
		/// </summary>
		/// <value>The anchor offset of the toolbar within its line.</value>
		public double Offset {
			get => offset;
			set {
				if (offset != value) {
					offset = value;
					this.NotifyPropertyChanged(nameof(Offset));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets a <see cref="Dock"/> specifying the toolbar placement.
		/// </summary>
		/// <value>
		/// A <see cref="Dock"/> specifying the toolbar placement.
		/// The default value is <see cref="Dock.Top"/>.
		/// </value>
		public Dock Placement {
			get => placement;
			set {
				if (placement != value) {
					placement = value;
					this.NotifyPropertyChanged(nameof(Placement));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the toolbar's sort order within its <see cref="Placement"/> line.
		/// </summary>
		/// <value>The toolbar's sort order within its <see cref="Placement"/> line.</value>
		public int SortOrder {
			get => sortOrder;
			set {
				if (sortOrder != value) {
					sortOrder = value;
					this.NotifyPropertyChanged(nameof(SortOrder));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the toolbar's title.
		/// </summary>
		/// <value>The toolbar's title.</value>
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
			return $"{this.GetType().FullName}[{this.Items.Count} items]";
		}

	}

}
