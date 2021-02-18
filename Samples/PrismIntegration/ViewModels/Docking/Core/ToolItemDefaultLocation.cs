namespace ActiproSoftware.Windows.PrismIntegration.ViewModels {

	/// <summary>
	/// Specifies a tool item view's default location.
	/// </summary>
	public class ToolItemDefaultLocation : ObservableObjectBase {

		private ToolItemDockSide? dockSide;
		private string targetSerializationId;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the optional <see cref="ToolItemDockSide"/> to dock against the target control.
		/// </summary>
		/// <value>The optional <see cref="ToolItemDockSide"/> to dock against the target control.</value>
		public ToolItemDockSide? DockSide {
			get {
				return this.dockSide;
			}
			set {
				if (this.dockSide != value) {
					this.dockSide = value;
					this.NotifyPropertyChanged("DockSide");
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the serialization ID of the target view-model.
		/// </summary>
		/// <value>The serialization ID of the target view-model.</value>
		public string TargetSerializationId {
			get {
				return this.targetSerializationId;
			}
			set {
				if (this.targetSerializationId != value) {
					this.targetSerializationId = value;
					this.NotifyPropertyChanged("TargetSerializationId");
				}
			}
		}

	}

}