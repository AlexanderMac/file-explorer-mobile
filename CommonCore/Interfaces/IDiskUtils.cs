//
//  IDiskUtils.cs
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
using FileExplorerMobile.Core.Data.Enums;

namespace FileExplorerMobile.Core.Interfaces
{
	public interface IDiskUtils : IBaseManager
	{
#if DEBUG
		/// <summary>
		/// Creates the test data.
		/// </summary>
		void CreateTestData();
#endif

		/// <summary>
		/// Gets the file entries by the <see cref="path"/>.
		/// </summary>
		/// <param name='path'>The path.</param>
		/// <returns>The file entries.</returns>
		string[] GetFileEntries(string path);

		/// <summary>
		/// Gets the value indicates, the FileSystemInfo is a directory.
		/// </summary>
		/// <param name='path'>The path.</param>
		/// <returns><c>true</c>, if the FileSystemInfo is directory, <c>false</c> otherwise.</returns>
		bool FileSystemInfoIsIsDirectory(string path);

		/// <summary>
		/// Gets the file name without extension.
		/// </summary>
		/// <param name='path'>The path.</param>
		/// <returns>The file name without extension.</returns>
		string GetFileNameWithoutExtension(string path);

		/// <summary>
		/// Gets the file extension.
		/// </summary>
		/// <param name='name'>The file name.</param>
		/// <returns>The file extension.</returns>
		string GetFileExtension(string name);

		/// <summary>
		/// Gets the size of the file.
		/// </summary>
		/// <param name='path'>The file path.</param>
		/// <returns>The file size.</returns> 
		long GetFileSize(string path);

		/// <summary>
		/// Creates the file entry.
		/// </summary>
		/// <param name="type">The FileEntry type.</param>
		/// <param name="showAlertOnException">Indicates should be or not appears the alert window if an exception raise.</param>
		/// <param name="pathes">The pathes.</param>
		/// <returns><c>true</c>, if FileSystemInfo was created, <c>false</c> otherwise./returns>
		bool CreateFileEntry(FileEntryTypes type, bool showAlertOnException = true, params string[] pathes);

		/// <summary>
		/// Renames the FileSystemInfo.
		/// </summary>
		/// <param name='path'>The FileSystemInfo path.</param>
		/// <param name='newName'>The new FileSystemInfo name.</param>
		/// <param name='newPath'>The FileSystemInfo path after rename.</param>
		/// <param name="showAlertOnException">Indicates should be or not appears the alert window if an exception raise.</param>
		/// <returns><c>true</c>, if FileSystemInfo was renamed, <c>false</c> otherwise./returns>
		bool RenameFileSystemInfo(string path, string newName, out string pathAfterRename, bool showAlertOnException = true);

		/// <summary>
		/// Deletes the FileSystemInfo.
		/// </summary>
		/// <param name='path'>The FileSystemInfo path.</param>
		/// <param name="showAlertOnException">Indicates should be or not appears the alert window if an exception raise.</param>
		/// <returns><c>true</c>, if FileSystemInfo was deleted, <c>false</c> otherwise.</returns>
		bool DeleteFileSystemInfo(string path, bool showAlertOnException = true);
	}
}

