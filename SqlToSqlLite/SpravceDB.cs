using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDownloader
{
    internal class SpravceDB
    {
        //connectionstring databáze SQLite k projektu ITN Data Presenter
        private string pripojovaciStringLite;

        //connectionstring databáze MS-SQL u projektu ITN Data Downloader
        private string pripojovaciString;

        //seznam uzivatelu stahnutý třídou ITNetwork
        private List<User> seznamITN;


        public SpravceDB()
        {
            seznamITN = new();
            string adresar = Directory.GetCurrentDirectory();
            pripojovaciStringLite = "Data Source=" + adresar + "\\Database\\Database.sqlite;Version=3;";
            pripojovaciString = "Data Source=(localdb)\\MSSQLLocalDB;AttachDbFilename="+adresar+"\\Database\\Database.mdf;Integrated Security=True";
                //"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=" + adresar + "\\Database\\Database.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        }


        /// <summary>
        /// Uloží načtená data ze seznamu uživatelů do SQLite databáze
        /// </summary>
        public void UlozDoSqlite()
        {
            using (SQLiteConnection spojeni = new SQLiteConnection(pripojovaciStringLite))
            {

                spojeni.Open();
               
                using (SQLiteCommand prikaz = new SQLiteCommand()) {
                    prikaz.Connection = spojeni;

                    prikaz.CommandText = "INSERT INTO" +
                                              " ITN_USERS(Jmeno,UrlObrazek,Urlwww,IdNaITN,Vek,Zkusenost,Aura)" +
                                              " VALUES " +
                                              " (@Jmeno,@UrlObrazek,@Urlwww,@IdNaITN,@Vek,@Zkusenost,@Aura)";

                    for (int i = 0; i < seznamITN.Count; i++)
                    {
                        prikaz.Parameters.Clear();

                        prikaz.Parameters.AddWithValue("@Jmeno", seznamITN[i].Jmeno);
                        prikaz.Parameters.AddWithValue("@UrlObrazek", seznamITN[i].ObrazekWWW);
                        prikaz.Parameters.AddWithValue("@Urlwww", seznamITN[i].Stranka);
                        prikaz.Parameters.AddWithValue("@IdNaITN", seznamITN[i].Id);
                        prikaz.Parameters.AddWithValue("@Vek", seznamITN[i].Vek);
                        prikaz.Parameters.AddWithValue("@Zkusenost", seznamITN[i].Zkusenost);
                        prikaz.Parameters.AddWithValue("@Aura", seznamITN[i].Aura);

                        prikaz.ExecuteNonQuery(); 

                    }



                }
            }



        }

        /// <summary>
        /// Načte data z MS-Sql databáze do seznamu uživatelů  - List<User>
        /// </summary>
        public void ZiskejDataMSSql()
        {
            using (SqlConnection spojeni = new SqlConnection(pripojovaciString))
            {

                spojeni.Open();
                string dotaz = "SELECT * FROM ITN_Users";

                using (SqlDataAdapter adapter = new SqlDataAdapter(dotaz, spojeni))
                using (DataSet vysledky = new DataSet())
                {
                    adapter.Fill(vysledky);

                    string jmeno;
                    string obrazekWWW;
                    string stranka;
                    int id;
                    int vek;
                    int zkusenost;
                    int aura;
                    foreach (DataRow item in vysledky.Tables[0].Rows)
                    {

                        jmeno = item["Jmeno"].ToString();
                        obrazekWWW = item["UrlObrazek"].ToString();
                        stranka = item["Urlwww"].ToString();
                        id = int.Parse(item["IdNaITN"].ToString());
                        vek = int.Parse(item["Vek"].ToString());
                        zkusenost = int.Parse(item["Zkusenost"].ToString());
                        aura = int.Parse(item["Aura"].ToString());

                        seznamITN.Add(new User(jmeno, obrazekWWW, stranka, id, vek, zkusenost, aura));
                       
                    }


                }

            }


        }
    }
}
