//
//  FileEntryManager.cs
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
using System.Collections.Generic;
using System.Linq;
using FileExplorerMobile.Core.Interfaces;
using FileExplorerMobile.Core.Data.Objects;
using FileExplorerMobile.Core.Data.Enums;

namespace FileExplorerMobile.Core.Managers
{
	public class FileEntryManager : BaseManager, IFileEntryManager
	{
		#region Logic
		/// <summary>
		/// Gets the children collection of the <see cref="FileEntry"/> by the <see cref="parentEntry"/>.
		/// </summary>
		/// <param name='parententry'>The parent <see cref="FileEntry"/> object or null.</param>
		/// <returns>The children collection of the <see cref="FileEntry"/>.</returns>
		public List<FileEntry> GetChildren(FileEntry parentEntry)
		{
			var pathes = parentEntry == null 
				? MgrAccessor.DiskUtils.GetFileEntries(null)
				: MgrAccessor.DiskUtils.GetFileEntries(parentEntry.Path);

			var coll = new List<FileEntry>();
			foreach (var path in pathes) {
				var type = _GetTypeByPath(path);
				var name = MgrAccessor.DiskUtils.GetFileNameWithoutExtension(path);
				var size = MgrAccessor.DiskUtils.GetFileSize(path);

				var entry = new FileEntry {
					FeType = type,
					Path = path,
					Name = name,
					Size = size
				};
				coll.Add(entry);
			}

			return coll.OrderBy(x => x.FeType).ToList();
		}
		
		/// <summary>
		/// Renames the <see cref="entry"/> name for the <see cref="newName"/>.
		/// </summary>
		/// <param name='entry'>The <see cref="FileEntry"/>.</param>
		/// <param name='newName'>The new name.</param>
		/// <returns><value>true</value> if the operation success, otherwise <value>false</value>.</returns>
		public bool Rename(FileEntry entry, string newName)
		{
			string newPath;
			var success = MgrAccessor.DiskUtils.RenameFileSystemInfo(entry.Path, newName, out newPath);
			if (success) {
				entry.Path = newPath; 
				entry.Name = MgrAccessor.DiskUtils.GetFileNameWithoutExtension(newPath);
				return true;
			}
			return false;
		}
		
		/// <summary>
		/// Deletes the <see cref="entry"/>.
		/// </summary>
		/// <param name='entry'>The <see cref="FileEntry"/>.</param>
		/// <returns><value>true</value> if the operation success, otherwise <value>false</value>.</returns>
		public bool Delete(FileEntry entry)
		{
			var success = MgrAccessor.DiskUtils.DeleteFileSystemInfo(entry.Path);
			return success;
		}
		#endregion

		#region Helpers
		/// <summary>
		/// Gets the FileEntry type by <see cref="path"/>.
		/// </summary>
		/// <param name='path'>The path.</param>
		/// <returns>The FileEntry subType.</returns>
		private FileEntryTypes _GetTypeByPath(string path)
		{
			var isFolder = MgrAccessor.DiskUtils.FileSystemInfoIsIsDirectory(path);
			if (isFolder) {
				return FileEntryTypes.Folder;
			} else {
				var ext = MgrAccessor.DiskUtils.GetFileExtension(path);
				return MgrAccessor.CommonUtils.GetTypeByExtension(ext);
			}
		}
		#endregion
	}
}