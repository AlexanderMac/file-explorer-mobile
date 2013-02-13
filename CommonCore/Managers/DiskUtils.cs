//
//  DiskUtils.cs
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
using System.IO;
using FileExplorerMobile.Core.Interfaces;
using FileExplorerMobile.Core.Data.Enums;

namespace FileExplorerMobile.Core.Managers
{
	public class DiskUtils : BaseManager, IDiskUtils
	{
		#region Test data creation
#if DEBUG
		/// <summary>
		/// Creates the test data.
		/// </summary>
		public void CreateTestData()
		{
			Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Personal), true);

			CreateFileEntry(FileEntryTypes.Folder, false, "Root folder 1");
			CreateFileEntry(FileEntryTypes.Unknown, false, "Root folder 1", "PDF file 1-1.pdf");
			CreateFileEntry(FileEntryTypes.Unknown, false, "Root folder 1", "PDF file 1-2.pdf");
			CreateFileEntry(FileEntryTypes.Unknown, false, "Root folder 1", "PDF file 1-3.pdf");
			CreateFileEntry(FileEntryTypes.Unknown, false, "Root folder 1", "Doc file 1-1.doc");
			CreateFileEntry(FileEntryTypes.Unknown, false, "Root folder 1", "Doc file 1-2.docx");

			CreateFileEntry(FileEntryTypes.Folder, false, "Root folder 1", "Subfolder 1");			
			CreateFileEntry(FileEntryTypes.Unknown, false, "Root folder 1", "Subfolder 1", "Excel file 1-1-1.xls");
			CreateFileEntry(FileEntryTypes.Unknown, false, "Root folder 1", "Subfolder 1", "PowerPoint file 1-1-1.ppt");
			CreateFileEntry(FileEntryTypes.Unknown, false, "Root folder 1", "Subfolder 1", "PowerPoint file 1-1-2.pptx");

			CreateFileEntry(FileEntryTypes.Folder, false, "Root folder 2");
			CreateFileEntry(FileEntryTypes.Unknown, false, "Root folder 2", "PDF file 2-1.pdf");	
			CreateFileEntry(FileEntryTypes.Unknown, false, "Root folder 2", "PDF file 2-2.pdf");	

			CreateFileEntry(FileEntryTypes.Folder, false, "Root folder 3");
			CreateFileEntry(FileEntryTypes.Unknown, false, "Root folder 3", "PDF file 3-1.pdf");
			CreateFileEntry(FileEntryTypes.Unknown, false, "Root folder 3", "PowerPoint file 3-1.ppt");
			CreateFileEntry(FileEntryTypes.Unknown, false, "Root folder 3", "PowerPoint file 3-2.pptx");
			CreateFileEntry(FileEntryTypes.Unknown, false, "Root folder 3", "Doc file 1-1.doc");
			CreateFileEntry(FileEntryTypes.Unknown, false, "Root folder 3", "Doc file 1-2.docx");	

			CreateFileEntry(FileEntryTypes.Unknown, false, "PDF file 1.pdf");
			CreateFileEntry(FileEntryTypes.Unknown, false, "PDF file 2.pdf");
			CreateFileEntry(FileEntryTypes.Unknown, false, "PDF file 3.pdf");
			CreateFileEntry(FileEntryTypes.Unknown, false, "Doc file 1.doc");
			CreateFileEntry(FileEntryTypes.Unknown, false, "Doc file 2.docx");
			CreateFileEntry(FileEntryTypes.Unknown, false, "Excel file 1.xls");
			CreateFileEntry(FileEntryTypes.Unknown, false, "PowerPoint file 1.ppt");
			CreateFileEntry(FileEntryTypes.Unknown, false, "PowerPoint file 2.pptx");
		}
#endif
		#endregion

		#region Logic
		/// <summary>
		/// Gets the file entries by the <see cref="path"/>.
		/// </summary>
		/// <param name='path'>The path.</param>
		/// <returns>The file entries.</returns>
		public string[] GetFileEntries(string path)
		{
			if (path == null) {
				path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			}
			return Directory.GetFileSystemEntries(path, "*", SearchOption.TopDirectoryOnly);
		}

		/// <summary>
		/// Gets the value indicates, the FileSystemInfo is a directory.
		/// </summary>
		/// <param name='path'>The path.</param>
		/// <returns><c>true</c>, if the FileSystemInfo is directory, <c>false</c> otherwise.</returns>
		public bool FileSystemInfoIsIsDirectory(string path)
		{
			var fileAttrs = File.GetAttributes(path);
			return (fileAttrs & FileAttributes.Directory) == FileAttributes.Directory;
		}

		/// <summary>
		/// Gets the file name without extension.
		/// </summary>
		/// <param name='path'>The path.</param>
		/// <returns>The file name without extension.</returns>
		public string GetFileNameWithoutExtension(string path)
		{
			return Path.GetFileNameWithoutExtension(path);
		}

		/// <summary>
		/// Gets the file extension.
		/// </summary>
		/// <param name='name'>The file name.</param>
		/// <returns>The file extension.</returns>
		public string GetFileExtension(string name)
		{
			return Path.GetExtension(name);
		}

		/// <summary>
		/// Gets the size of the file.
		/// </summary>
		/// <param name='path'>The file path.</param>
		/// <returns>The file size.</returns> 
		public long GetFileSize(string path)
		{
			var fileInfo = new FileInfo(path);
			if ((fileInfo.Attributes & FileAttributes.Directory) == FileAttributes.Directory) {
				return 0;
			}
			return fileInfo.Length;
		}

		/// <summary>
		/// Creates the file entry.
		/// </summary>
		/// <param name="type">The FileEntry type.</param>
		/// <param name="showAlertOnException">Indicates should be or not appears the alert window if an exception raise.</param>
		/// <param name="pathes">The pathes.</param>
		/// <returns><c>true</c>, if FileSystemInfo was created, <c>false</c> otherwise./returns>
		public bool CreateFileEntry(FileEntryTypes type, bool showAlertOnException = true, params string[] pathes)
		{
			var root = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			var path = Path.Combine(root, Path.Combine(pathes));

			var success = false;
			try {
				if (type == FileEntryTypes.Folder) {
					if (!Directory.Exists(path)) {
						Directory.CreateDirectory(path);
					}
				} else {
					if (!File.Exists(path)) {
						File.WriteAllText(path, path);
					}
				}
				success = true;
			} catch (Exception) {
				if (showAlertOnException) {
					MgrAccessor.CommonUtils.ShowAlert("Warning", "The error occurs when created FileEntry", "Ok");
				}
			}

			return success;
		}

		/// <summary>
		/// Renames the FileSystemInfo.
		/// </summary>
		/// <param name='path'>The FileSystemInfo path.</param>
		/// <param name='newName'>The new FileSystemInfo name.</param>
		/// <param name='pathAfterRename'>The FileSystemInfo path after rename.</param>
		/// <returns><c>true</c>, if FileSystemInfo was renamed, <c>false</c> otherwise./returns>
		public bool RenameFileSystemInfo(string path, string newName, out string pathAfterRename, bool showAlertOnException = true)
		{
			var name = Path.GetFileNameWithoutExtension(path);
			var ext = Path.GetExtension(path);
			if (string.IsNullOrEmpty(newName) || newName == name || newName == name + ext) {
				pathAfterRename = null;
				return false;
			}

			var parentDirPath = Path.GetDirectoryName(path);
			pathAfterRename = Path.Combine(parentDirPath, newName + ext);

			var success = false;
			try {
				var fileInfo = new FileInfo(path);
				if ((fileInfo.Attributes & FileAttributes.Directory) == FileAttributes.Directory) {
					Directory.Move(path, pathAfterRename);  
				} else {
					File.Move(path, pathAfterRename); 
				}
				// TODO! Write to LogWriter
				success = true;
			} catch (Exception) {
				if (showAlertOnException) {
					MgrAccessor.CommonUtils.ShowAlert("Warning", "The error occurs when renamed FileEntry", "Ok");
				}
			}
			
			return success;
		}

		/// <summary>
		/// Deletes the FileSystemInfo.
		/// </summary>
		/// <param name='path'>The FileSystemInfo path.</param>
		/// <returns><c>true</c>, if FileSystemInfo was deleted, <c>false</c> otherwise.</returns>
		public bool DeleteFileSystemInfo(string path, bool showAlertOnException = true)
		{
			var success = false;
			try {
				var fileInfo = new FileInfo(path);
				if ((fileInfo.Attributes & FileAttributes.Directory) == FileAttributes.Directory) {
					if (Directory.Exists(path)) {						
						Directory.Delete(path, true);
					}
				} else {
					if (File.Exists(path)) {
						File.Delete(path);
					}
				}			
				// TODO! Write to LogWriter
				success = true;
			} catch (Exception) {
				if (showAlertOnException) {
					MgrAccessor.CommonUtils.ShowAlert("Warning", "The error occurs when deleted FileEntry", "Ok");
				}
			}
			
			return success;
		}
		#endregion
	}
}

