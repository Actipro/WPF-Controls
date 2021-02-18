using System;
using System.Xml.Serialization;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.NavigationBarLayout {

	/// <summary>
	/// Custom data that can be serialized to a layout.
	/// </summary>
	public class CustomData {

		private string stringValue;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the string value in this custom data. 
		/// </summary>
		/// <value>The string value in this custom data.</value>
		[XmlAttribute()]
		public string StringValue {
			get {
				return stringValue;
			}
			set {
				stringValue = value;
			}
		}

	}

}
