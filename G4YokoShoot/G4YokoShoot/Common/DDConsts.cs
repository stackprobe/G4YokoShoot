﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public static class DDConsts
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public const string ConfigFile = "Config.conf";
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public const string SaveDataFile = "SaveData.dat";
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public const string ResourceFile_01 = "Resource.dat";
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public const string ResourceFile_02 = "res.dat";
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public const string ResourceDir_01 = @"C:\Dat\Resource";
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public const string ResourceDir_02 = @"..\..\..\..\res";
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public const string UserDatStringsFile = "Properties.dat";

#if false // 例
		// discmt >

		public const int Screen_W = 800;
		public const int Screen_H = 600;

		// < discmt
#else
		// app > @ Screen_WH

		public const int Screen_W = 960;
		public const int Screen_H = 540;

		// < app
#endif

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public const int Screen_W_Min = 100;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public const int Screen_H_Min = 100;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public const int Screen_W_Max = 4000;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public const int Screen_H_Max = 3000;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public const double DefaultVolume = 0.45;
	}
}
