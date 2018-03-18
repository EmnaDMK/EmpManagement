using System;
using System.Collections.Generic;
using System.Text;
using Root.Services.Sqlite;
using SQLite;

namespace EmpManagement.Model
{
  public  class Employee : IBaseTable

    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
        public string GSM { get; set; }


    }
}
