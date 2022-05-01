using Microsoft.AspNetCore.Mvc;
using System;
using CountriesbyGDP.Models;
using CountriesbyGDP.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace CountriesbyGDP.Controllers
{
    public class CountriesbyGDPController : Controller
    {
        public StoredProcedureDBContext _context;
        public IConfiguration _config { get; }

        public CountriesbyGDPController
           (
            StoredProcedureDBContext context,
            IConfiguration config
            )
        {
            _context = context;
            _config = config;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IEnumerable<CountriesGdp> SearchResult()
        {
            var result = _context.CountriesGdp
                .FromSqlRaw<CountriesGdp>("spCountriesGDP")
                .ToList();

            return result;
        }

        [HttpGet]
        public IActionResult DynamicSQL()
        {
            string connectionStr = _config.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(connectionStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "spCountriesGDP";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                List<CountriesGdp> model = new List<CountriesGdp>();
                while (sdr.Read())
                {
                    var details = new CountriesGdp();
                    details.Country = sdr["Country"].ToString();
                    details.Region = sdr["Region"].ToString();
                    details.EstimatebyDollar = Convert.ToInt32(sdr["EstimatebyDollar"]);
                    details.Population = Convert.ToInt32(sdr["Population"]);
                    model.Add(details);
                }
                return View(model);
            }
        }
        [HttpPost]
        public IActionResult DynamicSQL(string Country, string Region, int EstimatebyDollar, int Population)
        {
            string connectionStr = _config.GetConnectionString("DefaultConnection");

            using (SqlConnection con = new SqlConnection(connectionStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "spCountriesGDP";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (Country != null)
                {
                    SqlParameter param_fn = new SqlParameter("@Country", Country);
                    cmd.Parameters.Add(param_fn);
                }
                if (Region != null)
                {
                    SqlParameter param_ln = new SqlParameter("@Region", Region);
                    cmd.Parameters.Add(param_ln);
                }
                if (EstimatebyDollar != 0)
                {
                    SqlParameter param_s = new SqlParameter("@EstimatebyDollar", EstimatebyDollar);
                    cmd.Parameters.Add(param_s);
                }
                if (Population != 0)
                {
                    SqlParameter param_s = new SqlParameter("@Population", Population);
                    cmd.Parameters.Add(param_s);
                }
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                List<CountriesGdp> model = new List<CountriesGdp>();
                while (sdr.Read())
                {
                    var details = new CountriesGdp();
                    details.Country = sdr["Country"].ToString();
                    details.Region = sdr["Region"].ToString();
                    details.EstimatebyDollar = Convert.ToInt32(sdr["EstimatebyDollar"]);
                    details.Population = Convert.ToInt32(sdr["Population"]);
                    model.Add(details);
                }
                return View(model);
            }
        }
    }
}
