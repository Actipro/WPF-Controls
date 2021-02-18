using System;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Scores for a sample search.
	/// </summary>
	public class SampleSearchScorer {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Returns the score for the specified <see cref="ProductItemInfo"/>.
		/// </summary>
		/// <param name="productItemInfo">The <see cref="ProductItemInfo"/> to examine.</param>
		/// <param name="searchParts">The search parts.</param>
		/// <returns>The score for the specified <see cref="ProductItemInfo"/>.</returns>
		public static int Score(ProductItemInfo productItemInfo, string[] searchParts) {
			var score = 0;
			const int maxParts = 10;
			const int multiplierFactor = 30;

			for (var searchPartIndex = Math.Min(maxParts, searchParts.Length) - 1; searchPartIndex >= 0; searchPartIndex--) {
				var searchPart = searchParts[searchPartIndex];
				if (!string.IsNullOrEmpty(searchPart)) {
					var titleScore = 0;
					var titleIndex = productItemInfo.Title.IndexOf(searchPart, StringComparison.OrdinalIgnoreCase);
					if (titleIndex >= 0)
						titleScore = (Math.Max(0, 30 - titleIndex) + 1) * (int)Math.Pow(multiplierFactor, 3);

					var categoryScore = 0;
					var categoryIndex = productItemInfo.Category.IndexOf(searchPart, StringComparison.OrdinalIgnoreCase);
					if (categoryIndex >= 0)
						categoryScore = (Math.Max(0, 30 - categoryIndex) + 1) * (int)Math.Pow(multiplierFactor, 2);

					var familyTitleScore = 0;
					var familyTitleIndex = productItemInfo.ProductFamily.Title.IndexOf(searchPart, StringComparison.OrdinalIgnoreCase);
					if (familyTitleIndex >= 0)
						familyTitleScore = (Math.Max(0, 30 - familyTitleIndex) + 1) * multiplierFactor;

					score += (maxParts - searchPartIndex) * (titleScore + categoryScore + familyTitleScore);
				}
			}

			return score;
		}

	}

}
