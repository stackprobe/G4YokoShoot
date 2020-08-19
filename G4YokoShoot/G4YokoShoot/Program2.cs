using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
using Charlotte.Common;
using Charlotte.Tools;
using Charlotte.Tests;
using Charlotte.Tests.Games;
using Charlotte.Mains;

namespace Charlotte
{
	public class Program2
	{
		public void Main2()
		{
			try
			{
				Main3();
			}
			catch (Exception e)
			{
				ProcMain.WriteLog(e);
			}
		}

		private void Main3()
		{
			DDMain2.Perform(Main4);
		}

		private void Main4()
		{
			if (ProcMain.ArgsReader.ArgIs("/D"))
			{
				Main4_Debug();
			}
			else
			{
				Main4_Release();
			}
		}

		private void Main4_Debug()
		{
			//new Test0001().Test01();
			//new GameTest().Test01();
			//new GameTest().Test02();
			new GameTest().Test02_B();
			//new GameTest().Test03();
			//new TitleMenu().Perform();
		}

		private void Main4_Release()
		{
			new TitleMenu().Perform();
		}
	}
}
