using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Game3Common;
using Charlotte.Tools;

namespace Charlotte.Games.Enemies.Bosses
{
	public class Boss0001 : IEnemy
	{
		public double X = DDConsts.Screen_W + 96.0;
		public double Y = DDConsts.Screen_H / 2.0;

		public void Loaded(Tools.D2Point pt)
		{
			this.EachFrameSequencer = EnumerableTools.Supplier(this.GetEachFrameSequencer());
		}

		private IEnumerable<object> GetEachFrameSequencer()
		{
			for (int c = 0; c < 40; c++)
			{
				this.X -= 5.0;

				yield return null;
			}
			for (; ; )
			{
				for (int c = 0; c < 30; c++)
				{
					this.Y += 3.0;

					yield return null;
				}
				for (int c = 0; c < 40; c++)
				{
					this.X -= 3.0;

					yield return null;
				}
				for (int c = 0; c < 60; c++)
				{
					this.Y -= 3.0;

					yield return null;
				}
				for (int c = 0; c < 40; c++)
				{
					this.X += 3.0;

					yield return null;
				}
				for (int c = 0; c < 30; c++)
				{
					this.Y += 3.0;

					yield return null;
				}
			}
		}

		private Func<object> EachFrameSequencer;

		public bool EachFrame()
		{
			this.EachFrameSequencer();
			return true;
		}

		public Game3Common.Crash GetCrash()
		{
			const double WH = 192.0;
			const double CORNER_R = 30.0;

			return CrashUtils.Multi(
				CrashUtils.Rect(new D4Rect(
					this.X - WH / 2.0 + CORNER_R,
					this.Y - WH / 2.0,
					WH - CORNER_R * 2.0,
					WH
					)),
				CrashUtils.Rect(new D4Rect(
					this.X - WH / 2.0,
					this.Y - WH / 2.0 + CORNER_R,
					WH,
					WH - CORNER_R * 2.0
					)),
				CrashUtils.Circle(new D2Point(this.X - (WH / 2.0 - CORNER_R), this.Y - (WH / 2.0 - CORNER_R)), CORNER_R),
				CrashUtils.Circle(new D2Point(this.X + (WH / 2.0 - CORNER_R), this.Y - (WH / 2.0 - CORNER_R)), CORNER_R),
				CrashUtils.Circle(new D2Point(this.X + (WH / 2.0 - CORNER_R), this.Y + (WH / 2.0 - CORNER_R)), CORNER_R),
				CrashUtils.Circle(new D2Point(this.X - (WH / 2.0 - CORNER_R), this.Y + (WH / 2.0 - CORNER_R)), CORNER_R)
				);
		}

		public int HP = 100;

		public bool Crashed(IWeapon weapon)
		{
			this.HP -= weapon.GetAttackPoint();

			if (this.HP <= 0)
			{
				EffectUtils.大爆発(this.X, this.Y);

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
			DDDraw.DrawCenter(Ground.I.Picture.Boss0001, this.X, this.Y);
		}
	}
}
