using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GrProject.Data
{
    class UserInfo
    {
        [PrimaryKey, Column("_id")]
        public int Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string e_mail { get; set; }

        public static int checkUser()
        {
            try
            {
                string db = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "lodosFit.db3");
                var dbCon = new SQLiteConnection(db);

                return dbCon.Table<UserInfo>().ToList().Count;
            }
            catch
            {
                return 0;
            }
        }
    }
}
