using ActiproSoftware.Windows.Input;
using System.Collections.Specialized;

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.TaskPlanning;

/// <summary>
/// Stores information about a task list.
/// </summary>
public class TaskListModel : ObservableObjectBase {

	private string _name;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="name">The task list name.</param>
	public TaskListModel(string name) {
		_name = name;

		Tasks.CollectionChanged += OnTasksCollectionChanged;

		AddTaskCommand = new DelegateCommand<string>(OnAddTaskCommandExecuted);
		DeleteListCommand = new DelegateCommand<object>(OnDeleteListCommandExecuted);
		DuplicateListCommand = new DelegateCommand<object>(OnDuplicateListCommandExecuted);
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnAddTaskCommandExecuted(string? parameter) {
		var name = parameter ?? "New task";
		Tasks.Add(new TaskModel(name, TaskModel.LabelYellowColor));
	}

	private void OnDeleteListCommandExecuted(object? parameter) {
		if (Board?.Lists.Contains(this) == true)
			Board.Lists.Remove(this);
	}

	private void OnDuplicateListCommandExecuted(object? parameter) {
		if (Board is { } board) {
			var clone = Clone();
			var index = board.Lists.IndexOf(this);
			board.Lists.Insert(index + 1, clone);
		}
	}

	private void OnTasksCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		if (e.Action == NotifyCollectionChangedAction.Reset) {
			foreach (var task in Tasks)
				task.List = this;
		}
		else {
			if (e.OldItems is not null) {
				foreach (var task in e.OldItems.OfType<TaskModel>())
					task.List = null;
			}
			if (e.NewItems is not null) {
				foreach (var task in e.NewItems.OfType<TaskModel>())
					task.List = this;
			}
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The command used to add a task.
	/// </summary>
	public ICommand AddTaskCommand { get; }

	/// <summary>
	/// The owner board.
	/// </summary>
	public TaskBoardModel? Board { get; set; }

	/// <summary>
	/// Creates a deep clone of the list.
	/// </summary>
	public TaskListModel Clone() {
		var clone = new TaskListModel(Name + " Copy");
		foreach (var task in Tasks)
			clone.Tasks.Add(task.Clone());
		return clone;
	}

	/// <summary>
	/// The command used to delete this list.
	/// </summary>
	public ICommand DeleteListCommand { get; }

	/// <summary>
	/// The command used to duplicate this list.
	/// </summary>
	public ICommand DuplicateListCommand { get; }

	/// <summary>
	/// The task list name.
	/// </summary>
	public string Name {
		get => _name;
		set => SetProperty(ref _name, value);
	}

	/// <summary>
	/// The collection of tasks.
	/// </summary>
	public ObservableCollection<TaskModel> Tasks { get; } = [];

}
