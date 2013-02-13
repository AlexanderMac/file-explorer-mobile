using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;
using Android.Runtime;

namespace droidApp.UI
{
	[Activity (Label = "AboutActivity")]
	public class AboutActivity : ListActivity
	{
		#region Constants
		private const string TitleKey = "Title";
		private const string DetailsKey = "Details";
		#endregion

		#region Activity logic
		/// <summary>
		/// Activitie's create event handeler.
		/// </summary>
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			ListAdapter = new SimpleAdapter(this, _GetItems(), Android.Resource.Layout.SimpleListItem2, 
			                                new string[] { TitleKey, DetailsKey },
											new int[] { Android.Resource.Id.Text1, Android.Resource.Id.Text2 });
		}

		/// <summary>
		/// Gets the ListView items.
		/// </summary>
		/// <returns>
		/// The get items.
		/// </returns>
		private IList<IDictionary<string, object>> _GetItems()
		{
			var item1 = new JavaDictionary<string, object>();
			item1.Add(TitleKey, "FileExplorer");
			item1.Add(DetailsKey, "12312");

			var item2 = new JavaDictionary<string, object>();
			item2.Add(TitleKey, "Author");
			item2.Add(DetailsKey, "Alexander Matsibarov (macasun)");

			var item3 = new JavaDictionary<string, object>();
			item3.Add(TitleKey, "Email");
			item3.Add(DetailsKey, "amatsibarov@gmail.com");

			var item4 = new JavaDictionary<string, object>();
			item4.Add(TitleKey, "Copyrights");
			item4.Add(DetailsKey, "2013");

			var item5 = new JavaDictionary<string, object>();
			item5.Add(TitleKey, "Application description");
			item5.Add(DetailsKey, "FileExplorer application for iOS and Android");

			var coll = new JavaList<IDictionary<string, object>>();
			coll.Add(item1);
			coll.Add(item2);
			coll.Add(item3);
			coll.Add(item4);
			coll.Add(item5);
			return coll;
		}	
		#endregion
	}
}