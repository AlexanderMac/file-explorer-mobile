using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using FileExplorerMobile.Core;
using FileExplorerMobile.Core.Interfaces;
using FileExplorerMobile.Core.Managers;

namespace droidApp.UI
{
	[Activity (Label = "droidApp", MainLauncher = true)]
	public class HomeActivity : TabActivity
	{			 
		#region Activity logic
		/// <summary>
		/// Activitie's create event handeler.
		/// </summary>
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Create managers and register references
			MgrAccessor.RegisterReference<IFileEntryManager>(new FileEntryManager());
			MgrAccessor.RegisterReference<ICommonUtils>(new CommonUtils());
			MgrAccessor.RegisterReference<IDiskUtils>(new DiskUtils());
#if DEBUG
			// Create test data
			MgrAccessor.DiskUtils.CreateTestData();
#endif
		
			SetContentView(Resource.Layout.Home);
					  
			// Create FileEntryList tab
			var intent = new Intent(this, typeof(FileEntryListActivity));
			intent.AddFlags(ActivityFlags.NewTask);				  
			var spec = TabHost.NewTabSpec("Explorer");
			spec.SetIndicator("", Resources.GetDrawable(Resource.Drawable.ic_tab_explorer));
			spec.SetContent(intent);
			TabHost.AddTab(spec);

			// Create About tab
			intent = new Intent(this, typeof(AboutActivity));
			intent.AddFlags(ActivityFlags.NewTask);		 				  
			spec = TabHost.NewTabSpec("About");
			spec.SetIndicator("", Resources.GetDrawable(Resource.Drawable.ic_tab_about));
			spec.SetContent(intent);
			TabHost.AddTab(spec);
			
			TabHost.CurrentTab = 0;	 
		}
		#endregion
	}
}