using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using System.Data;
using DataBaseAttributes;

namespace sessao
{
  /**
  * Cria relação para cadastro dos sessão a serem cadastrados pelo programa.
   */
	public class Sessao
	{
		private const int tableId = 5;
		private int IdSessao;
		private int IdFisioterapeuta;
		private int IdPaciente;
		private string DataSessao;
		private string ObservacaoSessao;

		public int idSessao 
		{
			get 
			{
				return IdSessao; 
			} 
			set 
			{
				IdSessao = value; 
			}
		}

		public int idFisioterapeuta 
		{
			get 
			{
				return IdFisioterapeuta; 
			} 
			set 
			{
				IdFisioterapeuta = value; 
			}
		}

		public int idPaciente 
		{
			get 
			{
				return IdPaciente; 
			} 
			set 
			{
				IdPaciente = value; 
			}
		}

		public string dataSessao 
		{
			get 
			{
				return DataSessao; 
			} 
			set 
			{
				DataSessao = value; 
			}
		}

		public string observacaoSessao 
		{
			get 
			{
				return ObservacaoSessao; 
			} 
			set 
			{
				ObservacaoSessao = value; 
			}
		}


		/**
		 * Classe com todos os atributos de uma sessao.
		 */
		public Sessao(int ids, int idf, int idp, string ds, string os)
		{
			this.idSessao = ids;
			this.idFisioterapeuta = idf;
			this.idPaciente = idp;
			this.dataSessao = ds;
			this.observacaoSessao = os;
		}

		public Sessao(Object[] columns)
		{
			this.idSessao = (int)columns[0];
			this.idFisioterapeuta = (int)columns[1];
			this.idPaciente = (int)columns[2];
			this.dataSessao = (string)columns[3];
			this.observacaoSessao = (string)columns[4];
		}
		/**
		* Cria a relação para sessão, contendo um id gerado automaticamente pelo banco como chave primária.
		 */
		public static void Create()
		{
			DataBase banco = new DataBase();
			string query = "CREATE TABLE IF NOT EXISTS SESSAO (idSessao INTEGER primary key AUTOINCREMENT,idFisioterapeuta INTEGER not null,idPaciente INTEGER not null,dataSessao DATE not null,observacaoSessao VARCHAR (300),foreign key (idPaciente) references PACIENTE (idPaciente),foreign key (idFisioterapeuta) references FISIOTERAPEUTA (idFisioterapeuta));";
			banco.Create(GlobalController.instance.path, query);
		}


		/**
		* Função que insere dados na tabela de sessão.
		 */
		public static void Insert(int idFisioterapeuta,
			int idPaciente,
			string dataSessao,
			string observacaoSessao)
		{
			DataBase banco = new DataBase();
			Object[] columns = new Object[] {idFisioterapeuta,idPaciente,dataSessao,observacaoSessao};
			banco.Insert(GlobalController.instance.path, columns, TablesManager.Tables[tableId].tableName, tableId);
		}


		/**
		* Função que atualiza dados já cadastrados anteriormente na relação de sessão.
		 */
		public static void Update(int id,
			int idFisioterapeuta,
			int idPaciente,
			string dataSessao,
			string observacaoSessao)
		{
			DataBase banco = new DataBase();
			Object[] columns = new Object[] {id,idFisioterapeuta,idPaciente,dataSessao,observacaoSessao};
			banco.Update(GlobalController.instance.path, columns, TablesManager.Tables[tableId].tableName, tableId);
		}

		/**
		* Função que lê dados já cadastrados anteriormente na relação de sessão.
		 */
		public static List<Sessao> Read()
		{
			DataBase banco = new DataBase();
			int idSessaoTemp = 0;
			int idFisioterapeutaTemp = 0;
			int idPacienteTemp = 0;
			string dataSessaoTemp = "";
			string observacaoSessaoTemp = "";

			Object[] columns = new Object[] {idSessaoTemp,idFisioterapeutaTemp,idPacienteTemp,dataSessaoTemp,observacaoSessaoTemp};

			List<Sessao> sessions = banco.Read<Sessao>(GlobalController.instance.path, TablesManager.Tables[tableId].tableName, columns);

			return sessions;
		}


		public static Sessao ReadValue (int id)
		{
			DataBase banco = new DataBase();
			int idSessaoTemp = 0;
			int idFisioterapeutaTemp = 0;
			int idPacienteTemp = 0;
			string dataSessaoTemp = "";
			string observacaoSessaoTemp = "";

			Object[] columns = new Object[] {idSessaoTemp,idFisioterapeutaTemp,idPacienteTemp,dataSessaoTemp,observacaoSessaoTemp};

			Sessao session = banco.ReadValue<Sessao>(GlobalController.instance.path, TablesManager.Tables[tableId].tableName,
				TablesManager.Tables[tableId].colName[0], id, columns);

			return session;
		}

		/**
		* Função que deleta dados cadastrados anteriormente na relação de sessão.
		 */
		public static void DeleteValue(int id)
		{
			DataBase banco = new DataBase();
			banco.DeleteValue (tableId, id);
		}

		/**
		* Função que apaga a relação de sessão inteira de uma vez.
		 */
		public static void Drop()
		{
			DataBase banco = new DataBase();
			banco.Drop (tableId);
		}
	}

}
