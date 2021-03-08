using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows.Markup;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Provides product data information.
	/// </summary>
	[ContentProperty("ProductFamilies")]
	public class ProductData {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the build date text.
		/// </summary>
		/// <value>The build date text.</value>
		public string BuildDateText {
			get {
				// Try to get the attribute
				var attr = Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyInformationalVersionAttribute)) as AssemblyInformationalVersionAttribute;
				if (attr != null) {
					var index = attr.InformationalVersion.IndexOf(" - ");
					if (index != -1) {
						index += 3;
						return new DateTime(
							Convert.ToInt32(attr.InformationalVersion.Substring(index, 4)),
							Convert.ToInt32(attr.InformationalVersion.Substring(index + 4, 2)),
							Convert.ToInt32(attr.InformationalVersion.Substring(index + 6, 2))
							).ToShortDateString();
					}
				}

				return "(Unknown)";
			}
		}
		
        /// <summary>
        /// Gets the collection of featured sample items.
        /// </summary>
        /// <value>The collection of featured sample items.</value>
		public ObservableCollection<ListItemInfo> FeaturedSamples { get; } = new ObservableCollection<ListItemInfo>();
		
		/// <summary>
		/// Gets the collection of product families.
		/// </summary>
		/// <value>The collection of product families.</value>
		public ObservableCollection<ProductFamilyInfo> ProductFamilies { get; } = new ObservableCollection<ProductFamilyInfo>();
		
		/// <summary>
		/// Gets the collection of product families with news.
		/// </summary>
		/// <value>The collection of product families with news.</value>
		public IEnumerable<ProductFamilyInfo> ProductFamiliesWithNews => from pf in this.ProductFamilies.OfType<ProductFamilyInfo>() where pf.News.Any() orderby pf.NewsSortOrder, pf.News.Count descending select pf;
		
		/// <summary>
		/// Gets the product version text.
		/// </summary>
		/// <value>The product version text.</value>
		public string ProductVersionText {
			get {
				return "v" + ActiproSoftware.Products.Shared.AssemblyInfo.Instance.Version.Substring(0, 4);
			}
		}

		/// <summary>
		/// Gets the product version with build text.
		/// </summary>
		/// <value>The product version with build text.</value>
		public string ProductVersionWithBuildText {
			get {
				return "v" + ActiproSoftware.Products.Shared.AssemblyInfo.Instance.Version;
			}
		}
		
		/// <summary>
		/// Gets or sets the <see cref="ProductFamilyInfo"/> that contains release histories.
		/// </summary>
		/// <value>The <see cref="ProductFamilyInfo"/> that contains release histories.</value>
		public ProductFamilyInfo ReleaseHistory { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ProductFamilyInfo"/> that contains utilities.
		/// </summary>
		/// <value>The <see cref="ProductFamilyInfo"/> that contains utilities.</value>
		public ProductFamilyInfo Utilities { get; set; }

	}

}
