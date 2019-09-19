using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Common;
using Charlotte.Game3Common;

namespace Charlotte.Games.Enemies.Shots
{
	public class Tama0001 : IEnemy
	{
		public double X;
		public double Y;
		public double XAdd;
		public double YAdd;

		public void Loaded(Tools.D2Point pt)
		{
			this.X = pt.X;
			this.Y = pt.Y;

			D2Point mvPt = DDUtils.AngleToPoint(
				DDUtils.GetAngle(Game.I.Player.X - this.X, Game.I.Player.Y - this.Y),
				8.0
				);

			this.XAdd = mvPt.X;
			this.YAdd = mvPt.Y;
		}

		public bool EachFrame()
		{
			this.X += this.XAdd;
			this.Y += this.YAdd;

			return DDUtils.IsOutOfScreen(new D2Point(this.X, this.Y), 16.0) == false;
		}

		public Game3Common.Crash GetCrash()
		{
			return CrashUtils.Circle(new D2Point(this.X, this.Y), 16.0);
		}

		public bool Crashed(IWeapon weapon)
		{
			return true;
		}

		public IEnemies.Kind_e GetKind()
		{
			return IEnemies.Kind_e.SHOT;
		}

		public void Draw()
		{
			DDDraw.DrawCenter(Ground.I.Picture.Tama0001, this.X, this.Y);
		}
	}
}
