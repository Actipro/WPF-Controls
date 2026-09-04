using ActiproSoftware.Windows.Input;
using ActiproSoftware.Windows.Media;

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.TaskPlanning;

/// <summary>
/// Stores information about a task.
/// </summary>
public class TaskModel : ObservableObjectBase {

	private Color _color;
	private string _name;

	public static readonly Color LabelBlueColor = Color.FromArgb(0xff, 0x00, 0x79, 0xbf);
	public static readonly Color LabelGreenColor = Color.FromArgb(0xff, 0x3c, 0xb5, 0x00);
	public static readonly Color LabelRedColor = Color.FromArgb(0xff, 0xeb, 0x46, 0x46);
	public static readonly Color LabelYellowColor = Color.FromArgb(0xff, 0xfa, 0xd9, 0x00);

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="name">The task name.</param>
	/// <param name="color">The label color.</param>
	public TaskModel(string name, Color color) {
		_name = name;
		_color = color;

		DeleteTaskCommand = new DelegateCommand<object>(OnDeleteTaskCommandExecuted);
		SetLabelColorCommand = new DelegateCommand<string>(OnSetLabelColorCommandExecuted);
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnDeleteTaskCommandExecuted(object? parameter) {
		if (List?.Tasks.Contains(this) == true)
			List.Tasks.Remove(this);
	}

	private void OnSetLabelColorCommandExecuted(string? parameter) {
		if (parameter is not null)
			Color = UIColor.FromWebColor(parameter).ToColor();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates a deep clone of the task.
	/// </summary>
	public TaskModel Clone()
		=> new(Name, Color);

	/// <summary>
	/// The label color.
	/// </summary>
	public Color Color {
		get => _color;
		set => SetProperty(ref _color, value);
	}

	/// <summary>
	/// The command used to delete this task.
	/// </summary>
	public ICommand DeleteTaskCommand { get; }

	/// <summary>
	/// The owner list.
	/// </summary>
	public TaskListModel? List { get; set; }

	/// <summary>
	/// The task name.
	/// </summary>
	public string Name {
		get => _name;
		set => SetProperty(ref _name, value);
	}

	/// <summary>
	/// The command used to set the label color.
	/// </summary>
	public ICommand SetLabelColorCommand { get; }

}
