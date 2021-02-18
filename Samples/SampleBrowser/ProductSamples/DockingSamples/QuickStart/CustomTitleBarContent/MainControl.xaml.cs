using System.Windows;
using System.Windows.Input;
using ActiproSoftware.Windows.Input;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStart.CustomTitleBarContent {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private DelegateCommand<object>			searchCommand;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			this.CreateCommands();
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates the command properties.
		/// </summary>
		private void CreateCommands() {
			searchCommand = new DelegateCommand<object>((param) => {
				MessageBox.Show("Search button clicked.");
			});
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the <see cref="ICommand"/> that can be used to open the drop-down menu.
		/// </summary>
		/// <value>
		/// The <see cref="ICommand"/> that can be used to open the drop-down menu.
		/// </value>
		public ICommand SearchCommand {
			get {
				return searchCommand;
			}
		}
		
	}

}
