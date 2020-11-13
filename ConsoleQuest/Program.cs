using System;

namespace ConsoleQuest
{
	class Program
	{
		static void Main(string[] args)
		{
			Logger.Inject(new ConsoleLogger(), new ConsoleInput());

			
			Logger.Log("Start Game!");
			
			
			Player player = LoadPlayer();
			if (player == null)
			{

			
			Logger.Log("プレイヤーの名前を入力してください");

			string Playname = Logger.ReadInput();

			//create player
			player = new Player(Playname, 150f, 10f, 5f, 1, 15f, 15f);



			}
			else
			{
				Logger.Log("継続する");
			}
			//create world
			World world = new World(player);

			//worldが終了判定(false)を返すまでループ
			while (world.Loop())
			{
				SavePlayer(player);
				//Enter入力を待つ
				Logger.ReadInput();
			}

			//THE END
			Logger.Log("game over.");
		}

		 private static readonly string SavedataPath =
		     System.IO.Directory.GetCurrentDirectory() + "\\savedata.json";
			private static void SavePlayer(Player player)
			{
				SaveData data = new SaveData();
				data.Player = new PlayerSaveData();
				data.Player.HP = player.HP;
				data.Player.MaxHP = player.MaxHP;
				data.Player.Name = player.Name;
				data.Player.AttackPoint = player.AttackPoint;
				data.Player.DefencePoint = player.DefencePoint;
				data.Player.Level = (int)player.Level;
				data.Player.Exp = (int)player.Exp;

				string dataJson = Newtonsoft.Json.JsonConvert.SerializeObject(data);
				System.IO.File.WriteAllText(SavedataPath, dataJson);
			}




		private static Player LoadPlayer(){

			try
			{
				string jsonData = System.IO.File.ReadAllText(SavedataPath);
			SaveData saveData = 
					Newtonsoft.Json.JsonConvert.DeserializeObject<SaveData>(jsonData);
			Player player = new Player(saveData.Player);
				return player;
				
				}
			catch 
			{
				return null;
			}

			/*
				//プログラム(exe)が実行されているフォルダを取得。
				//デバッグ実行の場合は、デバッグ用に生成されたexeの置かれるフォルダとなる
				string currentDirectory = System.IO.Directory.GetCurrentDirectory();
				//currentDirectoryにjsonが置いてある前提
				string jsonPath = currentDirectory + "\\car.json";

				if (!System.IO.File.Exists(jsonPath))
				{
					Console.WriteLine("Jsonファイルが無いので、ダミーデータを元にJsonを生成して書き込みます...");
					Console.WriteLine("出力先:" + jsonPath);

					Player dummy = MakeDefaultData();
					SaveCarJson(dummy, jsonPath);
				}


				//保存されているjsonデータをロードする
				Player loadedCar;
				if (!LoadCarJson(jsonPath, out loadedCar))
				{
					//ロードに失敗した
					return;
				}


		*/
		}


		//Car型インスタンスのJsonを生成して指定のファイルパスに書き込む
		private static void SaveCarJson(Player data, string filePath)
		{
			string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(data);
			System.IO.File.WriteAllText(filePath, jsonText);
		}

		//指定のファイルパスからJsonを読み込み、Car型インスタンスに変換する

		private static bool LoadCarJson(string filePath, out Player loadedInstance)
		{
			//ファイルが存在しない、Jsonフォーマットが正しくないなどの理由で
			//ロードに失敗する可能性があるのでtry-catchを使う
			try
			{
				string jsonText = System.IO.File.ReadAllText(filePath);
				loadedInstance = Newtonsoft.Json.JsonConvert.DeserializeObject<Player>(jsonText);
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine("ロード失敗:" + e.Message);
				loadedInstance = null;
				return false;
			}
		}



		//JSON出力のため、最初に入れておくためのデータを作る
		private static Player MakeDefaultData()
		{
			Player player= new Player("as",150f,10f,10f,1,15f,15f);



			return player;
		}
	}
}

