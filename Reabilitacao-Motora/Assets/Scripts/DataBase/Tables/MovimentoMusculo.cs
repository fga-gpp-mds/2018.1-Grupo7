using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using System.Data;
using DataBaseAttributes;


namespace movimentomusculo
{
  /**
   * Cria relação para cadastro de movimentos dos musculos a serem cadastrados pelo programa.
   */
	public class MovimentoMusculo
	{
		private const int tableId = 7;
		private int IdMusculo;
		private int IdMovimento;

		public int idMusculo { get { return IdMusculo; } set { IdMusculo = value; }}
		public int idMovimento { get { return IdMovimento; } set { IdMovimento = value; }}

		/**
		 * Classe com todos os atributos de um movimentomusculo.
		 */
		public MovimentoMusculo (Object[] columns)
		{
			this.idMusculo = (int)columns[0];
			this.idMovimento = (int)columns[1];
		}

		/**
		 * Cria a relação para cadastro dos movimento do musculo, contendo um id que vem da relação de outra tabela (musculo) como chave primária e estrangeira, assim como idMovimento, que vem da relação movimento.
		 */
		public static void Create()
		{
			DataBase banco = new DataBase();
			string query = "CREATE TABLE IF NOT EXISTS MOVIMENTOMUSCULO (idMusculo INTEGER not null,idMovimento INTEGER not null, foreign key (idMovimento) references MOVIMENTO (idMovimento),foreign key (idMusculo) references MUSCULO (idMusculo),primary key (idMusculo, idMovimento));";
			banco.Create(query);
		}

		/**
		 * Função que insere dados necessários para cadastro de movimentos dos musculos na relação MovimentoMusculo.
		 */
		public static void Insert(int idMusculo,
			int idMovimento)
		{
			using (var conn = new SqliteConnection(GlobalController.path))
			{
				conn.Open();
				var sqlQuery = "insert into MOVIMENTOMUSCULO (";

				int tableSize = TablesManager.Tables[tableId].colName.Count;

				for (int i = 0; i < tableSize; ++i)
				{
					string aux;

					if (i + 1 == tableSize)
					{
						aux = ")";
					}
					else
					{
						aux = ",";
					}

					sqlQuery += (TablesManager.Tables[tableId].colName[i] + aux);
				}

				sqlQuery += string.Format(" values (\"{0}\",\"{1}\")", idMusculo,
					idMovimento);

				using (var cmd = new SqliteCommand(sqlQuery, conn))
				{
					cmd.ExecuteNonQuery();
				}

				conn.Close();
			}
		}

		/**
		 * Função que atualiza dados já cadastrados anteriormente na relação MovimentoMusculo.
		 */
		public static void Update(int idMusculo, int idMovimento)
		{
			using (var conn = new SqliteConnection(GlobalController.path))
			{
				conn.Open();

				var sqlQuery = string.Format("UPDATE \"{0}\" set ", TablesManager.Tables[tableId].tableName);
				sqlQuery += string.Format("\"{0}\"=\"{1}\",", TablesManager.Tables[tableId].colName[0], idMusculo);
				sqlQuery += string.Format("\"{0}\"=\"{1}\" ", TablesManager.Tables[tableId].colName[1], idMovimento);

				sqlQuery += string.Format("WHERE \"{0}\" = \"{1}\", \"{2}\" = \"{3}\"", TablesManager.Tables[tableId].colName[0], idMusculo, TablesManager.Tables[tableId].colName[1], idMusculo);

				using (var cmd = new SqliteCommand(sqlQuery, conn))
				{
					cmd.ExecuteNonQuery();
				}

				conn.Close();
			}
		}

		/**
		 * Função que lê dados já cadastrados anteriormente na relação MovimentoMusculo.
		 */
		public static List<MovimentoMusculo> Read()
		{
			DataBase banco = new DataBase();
			Object[] columns = new Object[] {0, 0};

			List<MovimentoMusculo> muscleMovements = banco.Read<MovimentoMusculo>(TablesManager.Tables[tableId].tableName, columns);

			return muscleMovements;
		}

		/**
		 * Função que deleta dados cadastrados anteriormente na relação MovimentoMusculo.
		 */
		public static void DeleteValue(int id1, int id2)
		{
			using (var conn = new SqliteConnection(GlobalController.path))
			{
				conn.Open();

				var sqlQuery = string.Format("delete from \"{0}\" WHERE \"{1}\" = \"{2}\" AND \"{3}\" = \"{4}\"", TablesManager.Tables[tableId].tableName, TablesManager.Tables[tableId].colName[0], id1, TablesManager.Tables[tableId].colName[1], id2);

				using (var cmd = new SqliteCommand(sqlQuery, conn))
				{
					cmd.ExecuteNonQuery();
				}

				conn.Close();
			}
		}

		/**
		 * Função que apaga a relação MovimentoMusculo inteira de uma vez.
		 */
		public static void Drop()
		{
		 	DataBase banco = new DataBase();
		 	banco.Drop (tableId);
		}
	}
}
