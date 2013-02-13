//
//  FileEntry.cs
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

namespace FileExplorerMobile.Core.Data.Objects
{
	public class FileEntry
	{
		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		public FileEntryTypes FeType { get; set; }

		/// <summary>
		/// Gets or sets the path.
		/// </summary>
		public string Path { get; set; }

		/// <summary>
		/// Gets or sets the name without extension.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the size.
		/// </summary>
		public long Size { get; set; }
	}
}