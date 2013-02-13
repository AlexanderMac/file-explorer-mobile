//
//  ObjTableVC.cs
//
//  Author:
//       Alexander Matsibarov (macasun) <amatsibarov@gmail.com>
//
//  Copyright (c) 2013 Alexander Matsibarov
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Drawing;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using FileExplorerMobile.Core;
using FileExplorerMobile.Core.Data.Objects;
using FileExplorerMobile.Core.Data.Enums;

namespace FileExplorerMobile.mTouch.Views
{
	public class FileEntryTableVC : UITableViewController
	{
		#region Fields
		/// <summary>
		/// The opened file entry.
		/// </summary>
		private FileEntry _OpenedFileEntry;

		/// <summary>
		/// The set editing mode button.
		/// </summary>
		private UIBarButtonItem _SetEditingModeButton;
		#endregion

		#region Initialization
		public FileEntryTableVC(FileEntry fileEntry) : base(null, null)
		{
			_OpenedFileEntry = fileEntry;
		}
		#endregion
		
		#region UI Logic		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			Title = _OpenedFileEntry == null ? "Root" : _OpenedFileEntry.Name;
			_SetEditingModeButton = new UIBarButtonItem(UIImage.FromFile("Content/Images/NavBar/edit20.png"), UIBarButtonItemStyle.Plain, (s, e) => _SetEditingMode());
			NavigationItem.RightBarButtonItems = new UIBarButtonItem[] { _SetEditingModeButton };

			var children = MgrAccessor.FileEntryMgr.GetChildren(_OpenedFileEntry);
			TableView.Source = new TableSource(this, children);
			TableView.RowHeight = 64;
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}
		#endregion

		#region Logic
		/// <summary>
		/// Deletes the file entry.
		/// </summary>
		private void _SetEditingMode()
		{
			bool isEditingMode = _SetEditingModeButton.Style == UIBarButtonItemStyle.Done;
			if (!isEditingMode) {
				_SetEditingModeButton.Style = UIBarButtonItemStyle.Done;
			} else {
				_SetEditingModeButton.Style = UIBarButtonItemStyle.Plain;
			}

			isEditingMode = !isEditingMode;
			TableView.SetEditing(isEditingMode, true);
		}
		#endregion

		#region TableSource
		private class TableSource : UITableViewSource
		{
			#region Fields
			/// <summary>
			/// The cell identifier for the folder.
			/// </summary>
			private const string FolderCellIdentifier = "FolderCellIdentifier";

			/// <summary>
			/// The cell identifier for the file.
			/// </summary>
			private const string FileCellIdentifier = "FileCellIdentifier";

			/// <summary>
			/// The table view controller.
			/// </summary>
			private FileEntryTableVC _Controller;

			/// <summary>
			/// The file entries.
			/// </summary>
			private List<FileEntry> _FileEntries;
			#endregion

			#region Initialization
			/// <summary>
			/// Working constructor.
			/// </summary>
			public TableSource(FileEntryTableVC controller, List<FileEntry> fileEntries)
			{
				_Controller = controller;
				_FileEntries = fileEntries;
			}
			#endregion

			#region Logic
			/// <summary>
			/// Opens the file entry.
			/// </summary>
			private void _OpenFileEntry(UITableView tableView, NSIndexPath indexPath)
			{
				var fileEntry = _FileEntries[indexPath.Row];
				if (fileEntry.FeType == FileEntryTypes.Folder) {
					var tableVC = new FileEntryTableVC(fileEntry);
					_Controller.NavigationController.PushViewController(tableVC, true);
				}
			}

			/// <summary>
			/// Deletes the file entry.
			/// </summary>
			private void _DeleteFileEntry(UITableView tableView, NSIndexPath indexPath)
			{
				var fileEntry = _FileEntries[indexPath.Row];
				if (MgrAccessor.FileEntryMgr.Delete(fileEntry)) {
					_FileEntries.Remove(fileEntry);
					tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
				}
			}
			#endregion

			#region UITableViewSource
			public override UITableViewCellEditingStyle EditingStyleForRow(UITableView tableView, NSIndexPath indexPath)
			{
				return UITableViewCellEditingStyle.Delete;
			}
			
			public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
			{
				_DeleteFileEntry(tableView, indexPath);
			}
			
			public override string TitleForDeleteConfirmation(UITableView tableView, NSIndexPath indexPath)
			{
				return "Delete";
			}

			public override int RowsInSection(UITableView tableview, int section)
			{
				return _FileEntries.Count;
			}
			
			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				var fileEntry = _FileEntries[indexPath.Row];
				var cellIdentifier = fileEntry.FeType == FileEntryTypes.Folder ? FolderCellIdentifier : FileCellIdentifier;
				var cell = tableView.DequeueReusableCell(cellIdentifier);
				if (cell == null) {
					cell = new UITableViewCell(UITableViewCellStyle.Subtitle, cellIdentifier);
				}

				cell.TextLabel.Text = fileEntry.Name;
				if (fileEntry.FeType != FileEntryTypes.Folder) {
					cell.DetailTextLabel.Text = string.Format("Size: {0}b", fileEntry.Size);
				} else {
					cell.Accessory = UITableViewCellAccessory.DetailDisclosureButton;
				}
				cell.ImageView.Image = MgrAccessor.CommonUtils.GetTypeImage(fileEntry.FeType);

				return cell;
			}
			
			public override void AccessoryButtonTapped(UITableView tableView, NSIndexPath indexPath)
			{
				_OpenFileEntry(tableView, indexPath);
			}
			#endregion
		}
		#endregion
	}
}


