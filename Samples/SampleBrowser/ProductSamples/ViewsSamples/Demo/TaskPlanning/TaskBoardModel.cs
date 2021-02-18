using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Input;

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.TaskPlanning {

	/// <summary>
	/// Stores information about a task board.
	/// </summary>
	public class TaskBoardModel : ObservableObjectBase {

		private DelegateCommand<string>				addListCommand;
		private ObservableCollection<TaskListModel> lists			= new ObservableCollection<TaskListModel>();
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the <c>TaskBoardModel</c> class.
		/// </summary>
		public TaskBoardModel() {
			lists.CollectionChanged += OnListsCollectionChanged;

			addListCommand = new DelegateCommand<string>(this.OnAddListCommandExecuted);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when the command is executed.
		/// </summary>
		/// <param name="parameter">The command parameter.</param>
		private void OnAddListCommandExecuted(string parameter) {
			var name = parameter ?? "New List";
			this.Lists.Add(new TaskListModel(name));
		}
		
		/// <summary>
		/// Occurs when the collection is changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> containing data related to this event.</param>
		private void OnListsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
			if (e.Action == NotifyCollectionChangedAction.Reset) {
				foreach (var list in lists)
					list.Board = this;
			}
			else {
				if (e.OldItems != null) {
					foreach (TaskListModel list in e.OldItems)
						list.Board = null;
				}
				if (e.NewItems != null) {
					foreach (TaskListModel list in e.NewItems)
						list.Board = this;
				}
			}
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the command used to add a list.
		/// </summary>
		/// <value>The command used to add a list.</value>
		public ICommand AddListCommand {
			get {
				return addListCommand;
			}
		}

		/// <summary>
		/// Gets the collection of task lists.
		/// </summary>
		/// <value>The collection of task lists.</value>
		public ObservableCollection<TaskListModel> Lists {
			get {
				return lists;
			}
		}

	}

}
