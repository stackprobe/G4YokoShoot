using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Games
{
	public class Game : IDisposable
	{
		public static Game I = null;

		public Game()
		{
			I = this;
		}

		public void Dispose()
		{
			I = null;
		}

		private Player Player = new Player();

		public void Perform()
		{

		}
	}
}
