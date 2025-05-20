using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;

public class Personal
{
    private string CEDULA_ALU;
    private string APELLIDO_ALU;
    private string APELLIDOM_ALU;
    private string NOMBRE_ALU;
    private string IMAGEN_ALU;
    private string CORREO_ALU;
    private string strContraseña_Per;

    public string cedula_alu
    {
        get { return CEDULA_ALU; }
        set { CEDULA_ALU = value; }
    }

    public string apellido_alu
    {
        get { return APELLIDO_ALU; }
        set { APELLIDO_ALU = value; }
    }

    public string apellidom_alu
    {
        get { return APELLIDOM_ALU; }
        set { APELLIDOM_ALU = value; }
    }

    public string nombre_alu
    {
        get { return NOMBRE_ALU; }
        set { NOMBRE_ALU = value; }
    }

    public string imagen_alu
    {
        get { return IMAGEN_ALU; }
        set { IMAGEN_ALU = value; }
    }

    public string correo_alu
    {
        get { return CORREO_ALU; }
        set { CORREO_ALU = value; }
    }

    public Personal() { }

    public List<Personal> Load_PERSONAL(string comodin, string filtro1, string filtro2, string filtro3, string filtro4)
    {
        List<Personal> listPersonal = new List<Personal>();
        SqlConnection conexion = new SqlConnection(WebConfigurationManager.AppSettings["conexionBddProductos"]);
        SqlCommand comandoConsulta = new SqlCommand("SIGUTC_GetPERSONAL", conexion);
        comandoConsulta.CommandType = CommandType.StoredProcedure;

        comandoConsulta.Parameters.AddWithValue("@Comodin", comodin);
        comandoConsulta.Parameters.AddWithValue("@FILTRO1", filtro1);
        comandoConsulta.Parameters.AddWithValue("@FILTRO2", filtro2);
        comandoConsulta.Parameters.AddWithValue("@FILTRO3", filtro3);
        comandoConsulta.Parameters.AddWithValue("@FILTRO4", filtro4);

        try
        {
            conexion.Open();
            SqlDataReader reader1 = comandoConsulta.ExecuteReader();

            while (reader1.Read())
            {
                listPersonal.Add(
                    new Personal
                    {
                        cedula_alu = reader1["CEDULA_ALU"].ToString(),
                        apellido_alu = reader1["APELLIDO_ALU"].ToString(),
                        apellidom_alu = reader1["APELLIDOM_ALU"].ToString(),
                        nombre_alu = reader1["NOMBRE_ALU"].ToString(),
                        imagen_alu = reader1["IMAGEN_ALU"].ToString(),
                        correo_alu = reader1["CORREO_ALU"].ToString()
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
