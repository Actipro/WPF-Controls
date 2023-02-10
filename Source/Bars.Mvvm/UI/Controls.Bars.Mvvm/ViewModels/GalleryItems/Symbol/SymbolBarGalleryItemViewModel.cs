using ActiproSoftware.Products.Bars.Mvvm;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a gallery item view model for a symbol.
	/// </summary>
	public class SymbolBarGalleryItemViewModel : BarGalleryItemViewModelBase {

		private string symbol;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public SymbolBarGalleryItemViewModel() : this("\u00A9", "Copyright Sign") { }  // Parameterless constructor required for XAML support
		
		/// <summary>
		/// Initializes a new instance of the class with the specified symbol and label.
		/// </summary>
		/// <param name="symbol">The string symbol.</param>
		/// <param name="label">The text label to display.</param>
		public SymbolBarGalleryItemViewModel(string symbol, string label) : this(SR.GetString(SRName.UIGalleryItemCategorySymbols), symbol, label) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified symbol and label.
		/// </summary>
		/// <param name="category">The item's category.</param>
		/// <param name="symbol">The string symbol.</param>
		/// <param name="label">The text label to display.</param>
		public SymbolBarGalleryItemViewModel(string category, string symbol, string label) : base(category) {
			this.symbol = symbol;
			this.Label = label;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the string symbol.
		/// </summary>
		/// <value>The string symbol.</value>
		public string Symbol {
			get {
				return symbol;
			}
			set {
				if (symbol != value) {
					symbol = value;
					this.NotifyPropertyChanged(nameof(Symbol));
				}
			}
		}

		/// <inheritdoc/>
		public override string ToString() {
			return $"{this.GetType().FullName}[Symbol={this.Symbol}]";
		}

	}

}
