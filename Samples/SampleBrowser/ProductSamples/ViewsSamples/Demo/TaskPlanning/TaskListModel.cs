using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using System.Windows.Media;
using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Input;

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.TaskPlanning {

	/// <summary>
	/// Stores information about a task list.
	/// </summary>
	public class TaskListModel : ObservableObjectBase {
		
		private DelegateCommand<string>				addTaskCommand;
		private DelegateCommand<object>				deleteListCommand;
		private DelegateCommand<object>				duplicateListCommand;
		private string								name;
		private ObservableCollection<TaskModel>		tasks					= new ObservableCollection<TaskModel>();
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the <c>TaskListModel</c> class.
		/// </summary>
		/// <param name="name">The task list name.</param>
		public TaskListModel(string name) {
			this.Name = name;

			tasks.CollectionChanged += OnTasksCollectionChanged;

			addTaskCommand = new DelegateCommand<string>(this.OnAddTaskCommandExecuted);
			deleteListCommand = new DelegateCommand<object>(this.OnDeleteListCommandExecuted);
			duplicateListCommand = new DelegateCommand<object>(this.OnDuplicateListCommandExecuted);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the command is executed.
		/// </summary>
		/// <param name="parameter">The command parameter.</param>
		private void OnAddTaskCommandExecuted(string parameter) {
			var name = parameter ?? "New task";
			this.Tasks.Add(new TaskModel(name, TaskModel.LabelYellowColor));
		}

		/// <summary>
		/// Occurs when the command is executed.
		/// </summary>
		/// <param name="parameter">The command parameter.</param>
		private void OnDeleteListCommandExecuted(object parameter) {
			if ((this.Board != null) && (this.Board.Lists.Contains(this)))
				this.Board.Lists.Remove(this);
		}

		/// <summary>
		/// Occurs when the command is executed.
		/// </summary>
		/// <param name="parameter">The command parameter.</param>
		private void OnDuplicateListCommandExecuted(object parameter) {
			if (this.Board != null) {
				var clone = this.Clone();
				var index = this.Board.Lists.IndexOf(this);
				this.Board.Lists.Insert(index + 1, clone);
			}
		}
		
		/// <summary>
		/// Occurs when the collection is changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> containing data related to this event.</param>
		private void OnTasksCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
			if (e.Action == NotifyCollectionChangedAction.Reset) {
				foreach (var task in tasks)
					task.List = this;
			}
			else {
				if (e.OldItems != null) {
					foreach (TaskModel task in e.OldItems)
						task.List = null;
				}
				if (e.NewItems != null) {
					foreach (TaskModel task in e.NewItems)
						task.List = this;
				}
			}
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the command used to add a task.
		/// </summary>
		/// <value>The command used to add a task.</value>
		public ICommand AddTaskCommand {
			get {
				return addTaskCommand;
			}
		}

		/// <summary>
		/// Gets or sets the owner board.
		/// </summary>
		/// <value>The owner board.</value>
		public TaskBoardModel Board { get; set; }

		/// <summary>
		/// Creates a deep clone of the list.
		/// </summary>
		/// <returns>The cloned list.</returns>
		public TaskListModel Clone() {
			var clone = new TaskListModel(this.Name + " Copy");
			foreach (var task in tasks)
				clone.Tasks.Add(task.Clone());
			return clone;
		}
		
		/// <summary>
		/// Gets the command used to delete this list.
		/// </summary>
		/// <value>The command used to delete this list.</value>
		public ICommand DeleteListCommand {
			get {
				return deleteListCommand;
			}
		}

		/// <summary>
		/// Gets the command used to duplicate this list.
		/// </summary>
		/// <value>The command used to duplicate this list.</value>
		public ICommand DuplicateListCommand {
			get {
				return duplicateListCommand;
			}
		}

		/// <summary>
		/// Gets or sets the task list name.
		/// </summary>
		/// <value>The task list name.</value>
		public string Name {
			get {
				return name;
			}
			set {
				if (name != value) {
					name = value;
					this.NotifyPropertyChanged("Name");
				}
			}
		}

		/// <summary>
		/// Gets the collection of tasks.
		/// </summary>
		/// <value>The collection of tasks.</value>
		public ObservableCollection<TaskModel> Tasks {
			get {
				return tasks;
			}
		}

	}

}
