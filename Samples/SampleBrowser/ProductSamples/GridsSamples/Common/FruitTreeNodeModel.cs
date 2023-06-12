namespace ActiproSoftware.ProductSamples.GridsSamples.Common {
	
	/// <summary>
	/// Provides a tree node model implementation for a fruit.
	/// </summary>
	public class FruitTreeNodeModel : TreeNodeModel {

		private string colorCategory;
		private string leadingProducer;
		private int? servingCalories;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the color category.
		/// </summary>
		/// <value>The color category.</value>
		public string ColorCategory {
			get {
				return colorCategory;
			}
			set {
				if (colorCategory == value)
					return;

				colorCategory = value;
				this.NotifyPropertyChanged("ColorCategory");
			}
		}
		
		/// <summary>
		/// Gets or sets the leading producer.
		/// </summary>
		/// <value>The leading producer.</value>
		public string LeadingProducer {
			get {
				return leadingProducer;
			}
			set {
				if (leadingProducer == value)
					return;

				leadingProducer = value;
				this.NotifyPropertyChanged("LeadingProducer");
			}
		}
		
		/// <summary>
		/// Gets or sets the serving calories.
		/// </summary>
		/// <value>The serving calories.</value>
		public int? ServingCalories {
			get {
				return servingCalories;
			}
			set {
				if (servingCalories == value)
					return;

				servingCalories = value;
				this.NotifyPropertyChanged("ServingCalories");
			}
		}
		
	}

}
