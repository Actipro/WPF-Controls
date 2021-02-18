using System;
using System.Collections.Generic;

namespace ActiproSoftware.SampleBrowser.SampleData {

	/// <summary>
	/// Provides access to people data.
	/// </summary>
	public static class People {

		private static List<Person> all;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets a collection of all people.
		/// </summary>
		/// <value>A collection of all people.</value>
		public static IList<Person> All {
			get {
				if (all == null) {
					all = new List<Person>() {
						new Person(3, "Barnes", "Harold", "harold.barnes@corpticaenterprises.com", "Vice President", new DateTime(1999, 1, 11), new Uri("/Images/ProfilePhotos/Man03.png", UriKind.RelativeOrAbsolute)),
						new Person(35, "Cazalla", "Miguel", "miguel.cazalla@corpticaenterprises.com", "Operator", new DateTime(2018, 12, 2), new Uri("/Images/ProfilePhotos/Man01.png", UriKind.RelativeOrAbsolute)),
						new Person(21, "Dawson", "Barbara", "barb.dawson@corpticaenterprises.com", "Executive Assistant", new DateTime(2015, 8, 20), new Uri("/Images/ProfilePhotos/Woman01.png", UriKind.RelativeOrAbsolute)),
						new Person(8, "Ellington", "Cliff", "cliff.ellington@corpticaenterprises.com", "Manager", new DateTime(2003, 6, 17), new Uri("/Images/ProfilePhotos/Man02.png", UriKind.RelativeOrAbsolute)),
						new Person(31, "Fleming", "Michael", "michael.fleming@corpticaenterprises.com", "Operator", new DateTime(2017, 4, 4), new Uri("/Images/ProfilePhotos/Man04.png", UriKind.RelativeOrAbsolute)),
						new Person(27, "Gi", "Jang", "jang.gi@corpticaenterprises.com", "Analyst", new DateTime(2016, 5, 25), new Uri("/Images/ProfilePhotos/Woman03.png", UriKind.RelativeOrAbsolute)),
						new Person(4, "Harrison", "Ashley", "ashley.harrison@corpticaenterprises.com", "Sales Director", new DateTime(1999, 5, 9), new Uri("/Images/ProfilePhotos/Woman06.png", UriKind.RelativeOrAbsolute)),
						new Person(26, "Lai", "Erik", "erik.lai@corpticaenterprises.com", "Operator", new DateTime(2016, 4, 24), new Uri("/Images/ProfilePhotos/Man05.png", UriKind.RelativeOrAbsolute)),
						new Person(13, "Mitchell", "Tammy", "tammy.mitchell@corpticaenterprises.com", "Accountant", new DateTime(2011, 11, 20), new Uri("/Images/ProfilePhotos/Woman02.png", UriKind.RelativeOrAbsolute)),
						new Person(17, "O'Connell", "Scarlette", "scarlette.oconnell@corpticaenterprises.com", "Assistant Manager", new DateTime(2013, 10, 5), new Uri("/Images/ProfilePhotos/Woman05.png", UriKind.RelativeOrAbsolute)),
						new Person(1, "Taylor", "Tamara", "tamara.taylor@corpticaenterprises.com", "President", new DateTime(1996, 2, 19), new Uri("/Images/ProfilePhotos/Woman04.png", UriKind.RelativeOrAbsolute)),
					};
				}

				return all.AsReadOnly();
			}
		}

		/// <summary>
		/// Gets a random <see cref="Person"/>.
		/// </summary>
		/// <value>A random <see cref="Person"/>.</value>
		public static Person Random {
			get {
				var index = new Random().Next(All.Count - 1);
				return All[index];
			}
		}

	}

}
