using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

/// <summary>
/// Descripción breve de Personal
/// </summary>
public class Personal
{
    public string CEDULA_ALU { get; set; }
    public string NOMBRE_ALU { get; set; }
    public string CORREO_ALU { get; set; }
    public string IMAGEN_ALU { get; set; }

    public Personal() { }

    public List<Personal> LoadPersonal(string comodin, string filtro1, string filtro2, string filtro3, string filtro4)
    {
        var listPersonal = new List<Personal>();

        SqlConnection conexion = new SqlConnection(WebConfigurationManager.AppSettings["conexionBddProductos"]);
        SqlCommand comandoConsulta = new SqlCommand("SIGUTC_GetPERSONAL", conexion);
        comandoConsulta.Parameters.AddWithValue("@Comodin", comodin);
        comandoConsulta.Parameters.AddWithValue("@FILTRO1", filtro1);
        comandoConsulta.Parameters.AddWithValue("@FILTRO2", filtro2);
        comandoConsulta.Parameters.AddWithValue("@FILTRO3", filtro3);
        comandoConsulta.Parameters.AddWithValue("@FILTRO4", filtro4);
        comandoConsulta.CommandType = CommandType.StoredProcedure;
        try
        {
            conexion.Open();
            SqlDataAdapter adaptadorAlbum = new SqlDataAdapter(comandoConsulta);
            DataTable dt = new DataTable();
            adaptadorAlbum.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                listPersonal.Add(
                    new Personal
                    {
                        CEDULA_ALU = row["CEDULA_ALU"].ToString(),
                        NOMBRE_ALU = row["APELLIDO_ALU"].ToString() + " " + row["APELLIDOM_ALU"].ToString() + " " + row["NOMBRE_ALU"].ToString(),
                        IMAGEN_ALU = row["IMAGEN_ALU"].ToString(),
                        CORREO_ALU = row["CORREO_ALU"].ToString()
                    }
                );
            }
        }
        catch (Exception ex)
        {
            Console.Write("TIENES UN ERROR: " + ex.Message);
        }
        conexion.Close();
        return listPersonal;
    }
}