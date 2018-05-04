using UnityEngine;
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
        private static int tableId = 6;
        public int idSessao, idFisioterapeuta, idPaciente;
        public string dataSessao, observacaoSessao;

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

        /**
        * Cria a relação para sessão, contendo um id gerado automaticamente pelo banco como chave primária.
         */
        public static void Create()
        {
            DataBase banco = new DataBase();
            using (banco.conn = new SqliteConnection(GlobalController.instance.path))
            {
                banco.conn.Open();
                banco.cmd = banco.conn.CreateCommand();

                banco.sqlQuery = "CREATE TABLE IF NOT EXISTS SESSAO (idSessao INTEGER primary key AUTOINCREMENT,idFisioterapeuta INTEGER not null,idPaciente INTEGER not null,dataSessao DATE not null,observacaoSessao VARCHAR (300),foreign key (idPaciente) references PACIENTE (idPaciente),foreign key (idFisioterapeuta) references FISIOTERAPEUTA (idFisioterapeuta));";

                banco.cmd.CommandText = banco.sqlQuery;
                banco.cmd.ExecuteScalar();
                banco.conn.Close();
            }
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
            using (banco.conn = new SqliteConnection(GlobalController.instance.path))
            {
                banco.conn.Open();
                banco.cmd = banco.conn.CreateCommand();
                banco.sqlQuery = "insert into SESSAO (";

                int tableSize = TablesManager.Tables[tableId].colName.Count;

                for (int i = 1; i < tableSize; ++i) {
                    string aux = (i+1 == tableSize) ? (")") : (",");
                    banco.sqlQuery += (TablesManager.Tables[tableId].colName[i] + aux);
                }

                banco.sqlQuery += string.Format(" values (\"{0}\",\"{1}\",\"{2}\",\"{3}\")", idFisioterapeuta,
                    idPaciente,
                    dataSessao,
                    observacaoSessao);

                banco.cmd.CommandText = banco.sqlQuery;
                banco.cmd.ExecuteScalar();
                banco.conn.Close();
            }
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
            using (banco.conn = new SqliteConnection(GlobalController.instance.path))
            {
                banco.conn.Open();
                banco.cmd = banco.conn.CreateCommand();

                banco.sqlQuery = string.Format("UPDATE \"{0}\" set ", TablesManager.Tables[tableId].tableName);

                banco.sqlQuery += string.Format("\"{0}\"=\"{1}\",", TablesManager.Tables[tableId].colName[1], idFisioterapeuta);
                banco.sqlQuery += string.Format("\"{0}\"=\"{1}\",", TablesManager.Tables[tableId].colName[2], idPaciente);
                banco.sqlQuery += string.Format("\"{0}\"=\"{1}\",", TablesManager.Tables[tableId].colName[3], dataSessao);
                banco.sqlQuery += string.Format("\"{0}\"=\"{1}\" ", TablesManager.Tables[tableId].colName[4], observacaoSessao);

                banco.sqlQuery += string.Format("WHERE \"{0}\" = \"{1}\"", TablesManager.Tables[tableId].colName[0], id);

                banco.cmd.CommandText = banco.sqlQuery;
                banco.cmd.ExecuteScalar();
                banco.conn.Close();
            }
        }

        /**
        * Função que lê dados já cadastrados anteriormente na relação de sessão.
         */
        public static List<Sessao> Read()
        {
            DataBase banco = new DataBase();
            using (banco.conn = new SqliteConnection(GlobalController.instance.path))
            {
                banco.conn.Open();
                banco.cmd = banco.conn.CreateCommand();
                banco.sqlQuery = "SELECT * " + "FROM SESSAO";
                banco.cmd.CommandText = banco.sqlQuery;
                IDataReader reader = banco.cmd.ExecuteReader();

                List<Sessao> s = new List<Sessao>();

                while (reader.Read())
                {
                    int idSessao = 0;
                    int idFisioterapeuta = 0;
                    int idPaciente = 0;
                    string dataSessao = "";
                    string observacaoSessao = "";

                    if (!reader.IsDBNull(0)) idSessao = reader.GetInt32(0);
                    if (!reader.IsDBNull(1)) idFisioterapeuta = reader.GetInt32(1);
                    if (!reader.IsDBNull(2)) idPaciente = reader.GetInt32(2);
                    if (!reader.IsDBNull(3)) dataSessao = reader.GetString(3);
                    if (!reader.IsDBNull(4)) observacaoSessao = reader.GetString(4);

                    Sessao x = new Sessao(idSessao, idFisioterapeuta, idPaciente, dataSessao, observacaoSessao);
                    s.Add(x);
                }
                reader.Close();
                reader = null;
                banco.cmd.Dispose();
                banco.cmd = null;
                banco.conn.Close();
                banco.conn = null;
                return s;
            }
        }


        public static Sessao ReadValue (int id)
        {
            DataBase banco = new DataBase();
            using (banco.conn = new SqliteConnection(GlobalController.instance.path))
            {
                banco.conn.Open();
                banco.cmd = banco.conn.CreateCommand();
                banco.sqlQuery = "SELECT * " + string.Format("FROM \"{0}\" WHERE \"{1}\" = \"{2}\";", TablesManager.Tables[tableId].tableName, 
                    TablesManager.Tables[tableId].colName[0], 
                    id);
                banco.cmd.CommandText = banco.sqlQuery;
                IDataReader reader = banco.cmd.ExecuteReader();

                int idSessao = 0;
                int idFisioterapeuta = 0;
                int idPaciente = 0;
                string dataSessao = "";
                string observacaoSessao = "";

                if (!reader.IsDBNull(0)) idSessao = reader.GetInt32(0);
                if (!reader.IsDBNull(1)) idFisioterapeuta = reader.GetInt32(1);
                if (!reader.IsDBNull(2)) idPaciente = reader.GetInt32(2);
                if (!reader.IsDBNull(3)) dataSessao = reader.GetString(3);
                if (!reader.IsDBNull(4)) observacaoSessao = reader.GetString(4);

                Sessao x = new Sessao (idSessao, idFisioterapeuta, idPaciente, dataSessao, observacaoSessao);

                reader.Close();
                reader = null;
                banco.cmd.Dispose();
                banco.cmd = null;
                banco.conn.Close();
                banco.conn = null;
                return x;
            }
        }

        /**
        * Função que deleta dados cadastrados anteriormente na relação de sessão.
         */
        public static void DeleteValue(int id)
        {
            DataBase banco = new DataBase();
            using (banco.conn = new SqliteConnection(GlobalController.instance.path))
            {
                banco.conn.Open();
                banco.cmd = banco.conn.CreateCommand();

                banco.sqlQuery = string.Format("delete from \"{0}\" WHERE \"{1}\" = \"{2}\"", TablesManager.Tables[tableId].tableName, TablesManager.Tables[tableId].colName[0], id);

                banco.cmd.CommandText = banco.sqlQuery;
                banco.cmd.ExecuteScalar();
                banco.conn.Close();
            }
        }

        /**
        * Função que apaga a relação de sessão inteira de uma vez.
         */
        public static void Drop()
        {
            DataBase banco = new DataBase();
            using (banco.conn = new SqliteConnection(GlobalController.instance.path))
            {
                banco.conn.Open();
                banco.cmd = banco.conn.CreateCommand();

                banco.sqlQuery = string.Format("DROP TABLE IF EXISTS \"{0}\"", TablesManager.Tables[tableId].tableName);

                banco.cmd.CommandText = banco.sqlQuery;
                banco.cmd.ExecuteScalar();
                banco.conn.Close();
            }
        }
    }

}
