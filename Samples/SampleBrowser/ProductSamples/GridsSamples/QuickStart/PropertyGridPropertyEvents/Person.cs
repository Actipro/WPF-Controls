using System;
using System.Collections.Generic;
using System.ComponentModel;
using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors;
using ActiproSoftware.Windows.Controls.Grids.PropertyEditors;
using System.ComponentModel.DataAnnotations;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridPropertyEvents {
	
	/// <summary>
	/// Represents a test object for demonstration purposes.
	/// </summary>
	[ReadOnly(true)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class Person : ObservableObjectBase {

		private DateTime? birthday;
		private List<Person> children = new List<Person>();
		private string firstName;
		private string lastName;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the age of the person.
		/// </summary>
		/// <value>The age of the person.</value>
		[DefaultValue("(not set)")]
		[Description("Indicates the age of the person, based on the birthday.")]
		[Display(Order = 4)]
		public string Age {
			get {
				if (null != this.Birthday) {
					int age = (int)((DateTime.Now - this.Birthday.Value).TotalDays / 365.0);
					if (age == 1)
						return "1 year old";
					else if (age < 0)
						return "(invalid)";
					else
						return string.Format("{0} years old", age);
				}

				return "(not set)";
			}
		}

		/// <summary>
		/// Gets or sets the birthday of the person.
		/// </summary>
		/// <value>The birthday of the person.</value>
		[DefaultValue(null)]
		[Description("Indicates the birthday of the person.")]
		[Editor(typeof(NullableDatePropertyEditor), typeof(PropertyEditor))]
		[Display(Order = 3)]
		public DateTime? Birthday {
			get {
				return this.birthday;
			}
			set {
				if (value != this.birthday) {
					this.birthday = value;
					this.NotifyPropertyChanged("Birthday");
					this.NotifyPropertyChanged("Age");
				}
			}
		}

		/// <summary>
		/// Gets the children of the person.
		/// </summary>
		/// <value>The children of the person.</value>
		[Description("Lists the children of the person.")]
		[TypeConverter(typeof(ChildrenCollectionConverter))]
		[Display(Order = 5)]
		public List<Person> Children {
			get {
				return this.children;
			}
		}

		/// <summary>
		/// Gets or sets the first name of the person.
		/// </summary>
		/// <value>The first name of the person.</value>
		[DefaultValue(null)]
		[Description("Indicates the first name of the person.")]
		[NotifyParentProperty(true)]
		[Display(Order = 2)]
		public string FirstName {
			get {
				return this.firstName;
			}
			set {
				if (value != this.firstName) {
					this.firstName = value;
					this.NotifyPropertyChanged("FirstName");
				}
			}
		}

		/// <summary>
		/// Gets or sets the last name of the person.
		/// </summary>
		/// <value>The last name of the person.</value>
		[DefaultValue(null)]
		[Description("Indicates the last name of the person.")]
		[NotifyParentProperty(true)]
		[Display(Order = 1)]
		public string LastName {
			get {
				return this.lastName;
			}
			set {
				if (value != this.lastName) {
					this.lastName = value;
					this.NotifyPropertyChanged("LastName");
				}
			}
		}

		/// <summary>
		/// Returns a <see cref="String"/> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="String"/> that represents this instance.
		/// </returns>
		public override string ToString() {
			return string.Format("{0}, {1}", this.LastName, this.FirstName);
		}

	}

}
