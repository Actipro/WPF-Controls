using System.Windows.Input;
using System.Windows.Media;
using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Input;
using ActiproSoftware.Windows.Media;

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.TaskPlanning {

	/// <summary>
	/// Stores information about a task.
	/// </summary>
	public class TaskModel : ObservableObjectBase {
		
		private Color								color;
		private DelegateCommand<object>				deleteTaskCommand;
		private string								name;
		private DelegateCommand<string>				setLabelColorCommand;

		public static Color LabelBlueColor = Color.FromArgb(0xff, 0x00, 0x79, 0xbf);
		public static Color LabelGreenColor = Color.FromArgb(0xff, 0x3c, 0xb5, 0x00);
		public static Color LabelRedColor = Color.FromArgb(0xff, 0xeb, 0x46, 0x46);
		public static Color LabelYellowColor = Color.FromArgb(0xff, 0xfa, 0xd9, 0x00);

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the <c>TaskModel</c> class.
		/// </summary>
		/// <param name="name">The task name.</param>
		/// <param name="color">The label color.</param>
		public TaskModel(string name, Color color) {
			this.Name = name;
			this.Color = color;
		
			deleteTaskCommand = new DelegateCommand<object>(this.OnDeleteTaskCommandExecuted);
			setLabelColorCommand = new DelegateCommand<string>(this.OnSetLabelColorCommandExecuted);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the command is executed.
		/// </summary>
		/// <param name="parameter">The command parameter.</param>
		private void OnDeleteTaskCommandExecuted(object parameter) {
			if ((this.List != null) && (this.List.Tasks.Contains(this)))
				this.List.Tasks.Remove(this);
		}

		/// <summary>
		/// Occurs when the command is executed.
		/// </summary>
		/// <param name="parameter">The command parameter.</param>
		private void OnSetLabelColorCommandExecuted(string parameter) {
			this.Color = UIColor.FromWebColor(parameter).ToColor();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Creates a deep clone of the task.
		/// </summary>
		/// <returns>The cloned task.</returns>
		public TaskModel Clone() {
			return new TaskModel(this.Name, this.Color);
		}
		
		/// <summary>
		/// Gets or sets the label color.
		/// </summary>
		/// <value>The label color.</value>
		public Color Color {
			get {
				return color;
			}
			set {
				if (color != value) {
					color = value;
					this.NotifyPropertyChanged("Color");
				}
			}
		}
		
		/// <summary>
		/// Gets the command used to delete this task.
		/// </summary>
		/// <value>The command used to delete this task.</value>
		public ICommand DeleteTaskCommand {
			get {
				return deleteTaskCommand;
			}
		}

		/// <summary>
		/// Gets or sets the owner list.
		/// </summary>
		/// <value>The owner list.</value>
		public TaskListModel List { get; set; }

		/// <summary>
		/// Gets or sets the task name.
		/// </summary>
		/// <value>The task name.</value>
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
		/// Gets the command used to set the label color.
		/// </summary>
		/// <value>The command used to set the label color.</value>
		public ICommand SetLabelColorCommand {
			get {
				return setLabelColorCommand;
			}
		}

	}

}
