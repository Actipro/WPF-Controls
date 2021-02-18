namespace ActiproSoftware.Windows.PrismIntegration.ViewModels {

	//
	// NOTE: This enum and the related ToolItemDockSideConverter can be used in scenarios where you don't wish for your models to directly 
	//       reference types in the Docking/MDI assembly... it allows you to have a layer of abstraction if desired, but there
	//       is nothing wrong with directly referencing Side in your VM class to avoid having to use this abstraction layer
	//

	/// <summary>
	/// Specifies a dock side for a tool item's view.
	/// </summary>
	public enum ToolItemDockSide {
		
		/// <summary>
		/// Indicates the left side.
		/// </summary>
		Left,

		/// <summary>
		/// Indicates the top side.
		/// </summary>
		Top,

		/// <summary>
		/// Indicates the right side.
		/// </summary>
		Right,

		/// <summary>
		/// Indicates the bottom side.
		/// </summary>
		Bottom,

	}

}