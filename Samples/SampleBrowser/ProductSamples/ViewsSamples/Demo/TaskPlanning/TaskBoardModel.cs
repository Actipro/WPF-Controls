using ActiproSoftware.Windows.Input;
using System.Collections.Specialized;

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.TaskPlanning;

/// <summary>
/// Stores information about a task board.
/// </summary>
public class TaskBoardModel : ObservableObjectBase {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public TaskBoardModel() {
		Lists.CollectionChanged += OnListsCollectionChanged;

		AddListCommand = new DelegateCommand<string>(OnAddListCommandExecuted);
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnAddListCommandExecuted(string? parameter) {
		var name = parameter ?? "New List";
		Lists.Add(new TaskListModel(name));
	}

	private void OnListsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		if (e.Action == NotifyCollectionChangedAction.Reset) {
			foreach (var list in Lists)
				list.Board = this;
		}
		else {
			if (e.OldItems is not null) {
				foreach (var list in e.OldItems.OfType<TaskListModel>())
					list.Board = null;
			}
			if (e.NewItems is not null) {
				foreach (var list in e.NewItems.OfType<TaskListModel>())
					list.Board = this;
			}
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The command used to add a list.
	/// </summary>
	public ICommand AddListCommand { get; }

	/// <summary>
	/// The collection of task lists.
	/// </summary>
	public ObservableCollection<TaskListModel> Lists { get; } = [];

}
