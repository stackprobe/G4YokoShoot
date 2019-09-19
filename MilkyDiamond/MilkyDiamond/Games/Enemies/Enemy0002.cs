using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Common;
using Charlotte.Game3Common;
using Charlotte.Games.Enemies.Shots;

namespace Charlotte.Games.Enemies
{
	public class Enemy0002 : IEnemy
	{
		public double X;
		public double Y;
		public int Frame = 0;

		public void Loaded(Tools.D2Point pt)
		{
			this.X = pt.X;
			this.Y = pt.Y;
		}

		public bool EachFrame()
		{
			this.Frame++;

			if (this.Frame % 20 == 0)
				Game.I.AddEnemy(IEnemies.Load(new Tama0001(), this.X, this.Y));

			D2Point mvPt = DDUtils.AngleToPoint(
				DDUtils.GetAngle(Game.I.Player.X - this.X, Game.I.Player.Y - this.Y),
				2.5
				);

			this.X += mvPt.X;
			this.Y += mvPt.Y;

			return DDUtils.IsOutOfScreen(new D2Point(this.X, this.Y), 64.0) == false;
		}

		public Game3Common.Crash GetCrash()
		{
			return CrashUtils.Circle(new D2Point(this.X, this.Y), 64.0);
		}

		public int HP = 10;

		public bool Crashed(IWeapon weapon)
		{
			this.HP -= weapon.GetAttackPoint();

			if (this.HP <= 0)
			{
				EffectUtils.中爆発(this.X, this.Y);

				return false;
			}
			return true;
		}

		public IEnemies.Kind_e GetKind()
		{
			return IEnemies.Kind_e.ENEMY;
		}

		public void Draw()
		{
			DDDraw.DrawCenter(Ground.I.Picture.Enemy0002, this.X, this.Y);
		}
	}
}
