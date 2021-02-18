using System.Collections.Generic;
using ActiproSoftware.ProductSamples.DockingSamples.Common;
using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.MvvmToolWindows {

	/// <summary>
	/// Represents the main view-model.
	/// </summary>
	public class MainViewModel : ObservableObjectBase {

		private DeferrableObservableCollection<ToolItemViewModel> toolItems = new DeferrableObservableCollection<ToolItemViewModel>();
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="MainViewModel"/> class.
		/// </summary>
		public MainViewModel() {
			toolItems.Add(new ToolItem1ViewModel());
			toolItems.Add(new ToolItem2ViewModel() { State = ToolItemState.Document });
			toolItems.Add(new ToolItem3ViewModel() { State = ToolItemState.AutoHide, DefaultDockSide = ToolItemDockSide.Left });

			foreach (var toolItem in toolItems)
				toolItem.IsOpen = true;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the tool items associated with this view-model.
		/// </summary>
		/// <value>The tool items associated with this view-model.</value>
		public IList<ToolItemViewModel> ToolItems {
			get {
				return toolItems;
			}
		}

	}

}
