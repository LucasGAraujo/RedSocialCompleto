using Dapper;
using Microsoft.Data.SqlClient;
using RedeSocial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Infrastructure.Repositories
{
    public class WithDapper
    {
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RedeSocial;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public IEnumerable<Post> GetAllPostsByPerfil(string perfil)
        {
            var sql = @"SELECT POS.ID,
                               POS.FOTO,
                               POS.COMENTARIO,
                               POS.DATA,
                               PER.ID,
                               PER.NOME 
                        FROM POSTS POS
                        INNER JOIN PERFILS_ PER
                        ON POS.PERFILID = PER.ID
                        WHERE PER.NOME = @P1;
                       ";
            var result = new List<Post>();


            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                result = sqlConnection.Query<Post>(sql, new { P1 = perfil }).ToList();
                return result;
            }
        }

    }
}
