using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleQuest
{
	public class World
	{
		private Player MyPlayer;

		public World(Player player)
		{
			MyPlayer = player;
		}

		private static Enemy[] Enemies = new Enemy[]
		{
			new Enemy("野犬", 30, 10, 2, 10),
			new Enemy("大ムカデ",60,20,5,25),
			new Enemy("猿", 20, 10, 4, 10),
			new Enemy("大猿", 70, 25, 15, 40),
			new Enemy("狼", 40, 15, 5, 15),
            new Enemy("鷹", 20, 10, 5, 10),
			new Enemy("虎", 80, 30, 20, 50),
			new Enemy("大鷲", 35, 20, 5, 25),
		};

		public bool Loop()
		{

			
			Logger.Log(Enemies[8].Name+"が現れた！");
			
			Battle battle = new Battle(MyPlayer,Enemies[8]);

			BattleState battleState = BattleState.Continue;

			do
			{
				battleState = battle.AdvanceTurn();

				Logger.ReadInput();
			}
			while (battleState == BattleState.Continue);


			//勝利ならループ継続
			return battleState == BattleState.Win;
		}


	}
}
