using System.Collections.Generic;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Implements a navigation service for samples.
	/// </summary>
	public class NavigationService {

		private List<ProductItemInfo>	history			= new List<ProductItemInfo>();
		private int						historyIndex	= -1;

		private const int MaxHistoryCount = 100;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
	
		/// <summary>
		/// Gets whether a navigation backward can occur.
		/// </summary>
		/// <value>
		/// <c>true</c> if a navigation backward can occur; otherwise, <c>false</c>.
		/// </value>
		public bool CanGoBack => (historyIndex > 0);

		/// <summary>
		/// Gets whether a navigation forward can occur.
		/// </summary>
		/// <value>
		/// <c>true</c> if a navigation forward can occur; otherwise, <c>false</c>.
		/// </value>
		public bool CanGoForward => (historyIndex >= 0) && (historyIndex < history.Count - 1);

		/// <summary>
		/// Navigates backward in history.
		/// </summary>
		/// <returns>The <see cref="ProductItemInfo"/> to navigate to.</returns>
		public ProductItemInfo GoBack() {
			return (this.CanGoBack ? history[--historyIndex] : null);
		}

		/// <summary>
		/// Navigates forward in history.
		/// </summary>
		/// <returns>The <see cref="ProductItemInfo"/> to navigate to.</returns>
		public ProductItemInfo GoForward() {
			return (this.CanGoForward ? history[++historyIndex] : null);
		}

		/// <summary>
		/// Gets or sets whether a navigation through history is currently occurring.
		/// </summary>
		/// <value>
		/// <c>true</c> if a navigation through history is currently occurring; otherwise, <c>false</c>.
		/// </value>
		public bool IsNavigatingThroughHistory { get; set; }

		/// <summary>
		/// Navigates to the specified <see cref="ProductItemInfo"/>.
		/// </summary>
		/// <param name="itemInfo">The <see cref="ProductItemInfo"/> to navigate to.</param>
		public void NavigateTo(ProductItemInfo itemInfo) {
			if (this.IsNavigatingThroughHistory)
				return;

			historyIndex++;
			history.RemoveRange(historyIndex, history.Count - historyIndex);
			history.Add(itemInfo);

			if (history.Count > MaxHistoryCount) {
				historyIndex--;
				history.RemoveAt(0);
			}
		}
		
	}

}
