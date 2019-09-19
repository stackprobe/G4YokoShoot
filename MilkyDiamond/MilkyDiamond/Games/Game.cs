using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Common;
using Charlotte.Game3Common;
using Charlotte.Games.Enemies;

namespace Charlotte.Games
{
	public class Game : IDisposable
	{
		public IScenario Scenario;

		// <---- prm

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

		public int Frame = 0;

		public void Perform()
		{
			this.Player.X = DDConsts.Screen_W / 4;
			this.Player.Y = DDConsts.Screen_H / 2;

			double enemyAddRate = 0.01;

			for (; ; this.Frame++)
			{
				// プレイヤー行動
				{
					double xa = 0.0;
					double ya = 0.0;

					if (1 <= DDInput.DIR_4.GetInput()) // 左移動
					{
						xa = -1.0;
					}
					if (1 <= DDInput.DIR_6.GetInput()) // 左移動
					{
						xa = 1.0;
					}
					if (1 <= DDInput.DIR_8.GetInput()) // 上移動
					{
						ya = -1.0;
					}
					if (1 <= DDInput.DIR_2.GetInput()) // 下移動
					{
						ya = 1.0;
					}
					double speed = 6.0;

					if (1 <= DDInput.A.GetInput()) // 低速ボタン押下中
					{
						speed = 3.0;
					}
					this.Player.X += xa * speed;
					this.Player.Y += ya * speed;

					DDUtils.Range(ref this.Player.X, 0.0, DDConsts.Screen_W);
					DDUtils.Range(ref this.Player.Y, 0.0, DDConsts.Screen_H);

					if (1 <= DDInput.B.GetInput()) // 攻撃ボタン押下中
					{
						this.Player.Fire();
					}

					// test
					{
						if (DDInput.R.IsPound())
						{
							enemyAddRate += 0.01;
						}
						if (DDInput.L.IsPound())
						{
							enemyAddRate -= 0.01;
						}
						DDUtils.Range(ref enemyAddRate, 0.0, 1.0);
					}
				}

#if true
				this.Scenario.EachFrame();
#else // test
				// 敵の出現(仮)
				{
					for (int c = 0; c < 10; c++)
					{
						if (DDUtils.Random.Real2() < enemyAddRate)
						{
							this.AddEnemy(IEnemies.Load(
								new Enemy0001(),
								DDConsts.Screen_W + 50.0,
								DDConsts.Screen_H * DDUtils.Random.Real()
								));
						}
					}
				}
#endif

				this.EnemyEachFrame();
				this.WeaponEachFrame();

				// Crash
				{
					Crash playerCrash = CrashUtils.Point(new D2Point(this.Player.X, this.Player.Y));

					foreach (WeaponBox weapon in this.Weapons.Iterate())
						weapon.Crash = weapon.Value.GetCrash();

					foreach (EnemyBox enemy in this.Enemies.Iterate())
					{
						Crash enemyCrash = enemy.Value.GetCrash();

						foreach (WeaponBox weapon in this.Weapons.Iterate())
						{
							if (enemyCrash.IsCrashed(weapon.Crash))
							{
								if (enemy.Value.Crashed(weapon.Value) == false) // ? 消滅
									enemy.Dead = true;

								if (weapon.Value.Crashed(enemy.Value) == false) // ? 消滅
									weapon.Dead = true;
							}
						}
						this.Weapons.RemoveAll(weapon => weapon.Dead);

						if (enemyCrash.IsCrashed(playerCrash))
						{
							// TODO
						}
					}
					this.Enemies.RemoveAll(enemy => enemy.Dead);
				}

				// ここから描画

				this.DrawWall();
				this.Player.Draw();
				this.DrawEnemies();
				this.DrawWeapons();

				DDPrint.SetPrint();
				DDPrint.Print(DDEngine.FrameProcessingMillis_Worst + " " + this.Enemies.Count + " " + enemyAddRate.ToString("F2"));

				DDEngine.EachFrame();
			}
		}

		private void DrawWall()
		{
			DDCurtain.DrawCurtain();
		}

		private class EnemyBox
		{
			public IEnemy Value;
			public bool Dead;
		}

		private DDList<EnemyBox> Enemies = new DDList<EnemyBox>();

		public void AddEnemy(IEnemy enemy)
		{
			this.Enemies.Add(new EnemyBox()
			{
				Value = enemy,
			});
		}

		private void EnemyEachFrame()
		{
			foreach (EnemyBox enemy in this.Enemies.Iterate())
			{
				if (enemy.Value.EachFrame() == false) // ? 消滅
				{
					enemy.Dead = true;
				}
			}
			this.Enemies.RemoveAll(enemy => enemy.Dead);
		}

		private void DrawEnemies()
		{
			foreach (EnemyBox enemy in this.Enemies.Iterate())
			{
				enemy.Value.Draw();
			}
		}

		private class WeaponBox
		{
			public IWeapon Value;
			public Crash Crash;
			public bool Dead;
		}

		private DDList<WeaponBox> Weapons = new DDList<WeaponBox>();

		public void AddWeapon(IWeapon weapon)
		{
			this.Weapons.Add(new WeaponBox()
			{
				Value = weapon,
			});
		}

		private void WeaponEachFrame()
		{
			foreach (WeaponBox weapon in this.Weapons.Iterate())
			{
				if (weapon.Value.EachFrame() == false) // ? 消滅
				{
					weapon.Dead = true;
				}
			}
			this.Weapons.RemoveAll(weapon => weapon.Dead);
		}

		private void DrawWeapons()
		{
			foreach (WeaponBox weapon in this.Weapons.Iterate())
			{
				weapon.Value.Draw();
			}
		}
	}
}
