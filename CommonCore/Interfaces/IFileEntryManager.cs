//
//  IFileEntryManager.cs
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
using FileExplorerMobile.Core.Data.Objects;

namespace FileExplorerMobile.Core.Interfaces
{
	public interface IFileEntryManager : IBaseManager
	{
		/// <summary>
		/// Gets the children collection of the <see cref="FileEntry"/> by the <see cref="parentEntry"/>.
		/// </summary>
		/// <param name='parententry'>The parent <see cref="FileEntry"/> object or null.</param>
		/// <returns>The children collection of the <see cref="FileEntry"/>.</returns>
		List<FileEntry> GetChildren(FileEntry parentEntry);

		/// <summary>
		/// Renames the <see cref="entry"/> name for the <see cref="newName"/>.
		/// </summary>
		/// <param name='entry'>The <see cref="FileEntry"/>.</param>
		/// <param name='newName'>The new name.</param>
		/// <returns><value>true</value> if the operation success, otherwise <value>false</value>.</returns>
		bool Rename(FileEntry entry, string newName);

		/// <summary>
		/// Deletes the <see cref="entry"/>.
		/// </summary>
		/// <param name='entry'>The <see cref="FileEntry"/>.</param>
		/// <returns><value>true</value> if the operation success, otherwise <value>false</value>.</returns>
		bool Delete(FileEntry entry);
	}
}

