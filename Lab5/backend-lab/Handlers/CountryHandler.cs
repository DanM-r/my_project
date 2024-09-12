﻿using backend_lab.Models;
using System.Data;
using System.Data.SqlClient;

namespace backend_lab.Handlers
{
    public class CountryHandler
    {
        private SqlConnection _conexion;

        private string _rutaConexion;
        public CountryHandler()
        {
            var builder = WebApplication.CreateBuilder();
            _rutaConexion =
            builder.Configuration.GetConnectionString("CountriesContext");
            _conexion = new SqlConnection(_rutaConexion);
        }

        private DataTable CrearTablaConsulta(string consulta)
        {
            SqlCommand comandoParaConsulta = new
            SqlCommand(consulta, _conexion);
            SqlDataAdapter adaptadorParaTabla = new
            SqlDataAdapter(comandoParaConsulta);
            DataTable consultaFormatoTabla = new DataTable();
            _conexion.Open();
            adaptadorParaTabla.Fill(consultaFormatoTabla);
            _conexion.Close();
            return consultaFormatoTabla;
        }
        public List<CountryModel> ObtenerPaises()
        {
            List<CountryModel> paises = new List<CountryModel>();
            string consulta = "SELECT * FROM dbo.Pais ";
            DataTable tablaResultado =
            CrearTablaConsulta(consulta);
            foreach (DataRow columna in tablaResultado.Rows)
            {
                paises.Add(
                new CountryModel
                {
                    Id = Convert.ToInt32(columna["Id"]),
                    Nombre = Convert.ToString(columna["Nombre"]),
                    Idioma = Convert.ToString(columna["Idioma"]),
                    Continente =
                Convert.ToString(columna["Continente"]),
                });
            }
            return paises;
        }
    }
}
