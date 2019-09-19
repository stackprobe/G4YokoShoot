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
				game.Status = new Status();
				game.Perform();
			}
		}

		public void Test02()
		{
			using (Game game = new Game())
			{
				game.Scenario = new Scenario0002();
				game.Status = new Status();
				game.Perform();
			}
		}

		public void Test03()
		{
			using (Game game = new Game())
			{
				//game.Scenario = new Scenario0001();
				//game.Scenario = new Scenario0002();
				game.Scenario = new Scenario0003();

				game.Status = new Status();
				game.Perform();
			}
		}
	}
}
