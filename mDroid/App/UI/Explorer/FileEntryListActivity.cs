using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using FileExplorerMobile.Core;
using FileExplorerMobile.Core.Data.Objects;
using FileExplorerMobile.Core.Data.Enums;
using Android.Content;

namespace droidApp.UI
{
	[Activity (Label = "FileEntryListActivity")]			
	public class FileEntryListActivity : ListActivity
	{
		#region Fields
		/// <summary>
		/// Gets or sets the temporary file entry (for nested navigation).
		/// </summary>
		public static FileEntry TempFileEntry;

		/// <summary>
		/// The opened file entry.
		/// </summary>
		private FileEntry _OpenedFileEntry;

		/// <summary>
		/// The children file entry list.
		/// </summary>
		private List<FileEntry> _ChildrenFileEntry;
		#endregion

		#region Activity logic
		/// <summary>
		/// Activitie's create event handeler.
		/// </summary>
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			ListView.ItemClick += new EventHandler<AdapterView.ItemClickEventArgs>(_ListView_ItemClick);

			_OpenedFileEntry = TempFileEntry;
			_ChildrenFileEntry = MgrAccessor.FileEntryMgr.GetChildren(_OpenedFileEntry);
			ListAdapter = new FileEntryListAdapter(this, _ChildrenFileEntry);
		}

		/// <summary>
		/// list view item click event handler.
		/// </summary>
		private void _ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			var fileEntry = _ChildrenFileEntry [e.Position];
			if (fileEntry.FeType == FileEntryTypes.Folder) {
				FileEntryListActivity.TempFileEntry = _ChildrenFileEntry [e.Position];
				var intent = new Intent(this, typeof(FileEntryListActivity));
				StartActivity(typeof(FileEntryListActivity));
			}
		}
		#endregion

		#region ListAdapter
		private class FileEntryListAdapter : BaseAdapter<FileEntry>
		{
			#region Fields
			/// <summary>
			/// The ListView activity.
			/// </summary>
			private Activity _Context;
			
			/// <summary>
			/// The file entries.
			/// </summary>
			private List<FileEntry> _FileEntries;
			#endregion

			#region Initialization
			/// <summary>
			/// Working constructor.
			/// </summary>
			public FileEntryListAdapter(Activity context, List<FileEntry> fileEntries)
			{
				_Context = context;
				_FileEntries = fileEntries;
			}
			#endregion

			#region Logic
			/// <summary>
			/// Gets the resource image id by the file type.
			/// </summary>
			private int _GetResourceIdByType(FileEntryTypes feType)
			{
				switch (feType) {
					case FileEntryTypes.Folder:
						return Resource.Drawable.Folder48;
					case FileEntryTypes.PDF:
						return Resource.Drawable.PDF48;
					case FileEntryTypes.Word:
						return Resource.Drawable.Word48;
					case FileEntryTypes.Excel:
						return Resource.Drawable.Excel48;
					case FileEntryTypes.PowerPoint:
						return Resource.Drawable.PowerPoint48;
					case FileEntryTypes.Image:
						return Resource.Drawable.Image48;
					default:
						return Resource.Drawable.Undefined48;
				}
			}
			#endregion

			#region Adapter logic
			/// <summary>
			/// Gets the count of the items.
			/// </summary>
			public override int Count {
				get {
					return _FileEntries.Count;
				}
			}

			/// <summary>
			/// Gets the <see cref="FileEntry"/> object by the item position.
			/// </summary>
			public override FileEntry this[int position] {
				get {
					return _FileEntries[position];
				}
			}

			/// <summary>
			/// Gets the item identifier.
			/// </summary>
			public override long GetItemId(int position)
			{
				return position;
			}

			/// <summary>
			/// Gets the item's view.
			/// </summary>
			public override View GetView(int position, View convertView, ViewGroup parent)
			{
				View lvView;
				if (convertView is LinearLayout) {
					lvView = convertView;
				} else {
					lvView = _Context.LayoutInflater.Inflate(Resource.Layout.FileEntryListItem, parent, false); 
				}
				var imageView = lvView.FindViewById(Resource.Id.FileEntryImageItem) as ImageView;
				var textView = lvView.FindViewById(Resource.Id.FileEntryTextItem) as TextView;
				var detailTextView = lvView.FindViewById(Resource.Id.FileEntryDetailTextItem) as TextView;

				var fileEntry = _FileEntries[position];
				imageView.SetImageResource(_GetResourceIdByType(fileEntry.FeType));
				textView.SetText(fileEntry.Name, TextView.BufferType.Normal);
				if (fileEntry.FeType != FileEntryTypes.Folder) {
					detailTextView.SetText(string.Format("Size: {0}b", fileEntry.Size), TextView.BufferType.Normal);
				}

				return lvView;
			}
			#endregion
		}
		#endregion
	}
}