﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Games;
using Charlotte.Games.Scenarios;

namespace Charlotte.Tests.Games
{
	public class GameTest
	{
		public void Test01()
		{
			using (Game game = new Game())
			{
				game.Scenario = new Scenario0001();
				game.Perform();
			}
		}

		public void Test02()
		{
			using (Game game = new Game())
			{
				game.Scenario = new Scenario0002();
				game.Perform();
			}
		}
	}
}
