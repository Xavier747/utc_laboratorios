using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

public class CURSO
{
    private string STRCOD_CURSO;
    private string STRNOMBRE_CURSO;
    private string STRPARALELO_CURSO;
    private string STRCOD_PER;

    public string strcod_curso
    {
        get { return STRCOD_CURSO; }
        set { STRCOD_CURSO = value; }
    }

    public string strnombre_curso
    {
        get { return STRNOMBRE_CURSO; }
        set { STRNOMBRE_CURSO = value; }
    }

    public string strparalelo_curso
    {
        get { return STRPARALELO_CURSO; }
        set { STRPARALELO_CURSO = value; }
    }

    public string strcod_per
    {
        get { return STRCOD_PER; }
        set { STRCOD_PER = value; }
    }

    public CURSO() { }

    public List<CURSO> Load_CURSO(string comodin, string filtro1, string filtro2, string filtro3, string filtro4)
    {
        List<CURSO> listCursos = new List<CURSO>();
        SqlConnection conexion = new SqlConnection(WebConfigurationManager.AppSettings["conexionBddProductos"]);
        SqlCommand comandoConsulta = new SqlCommand("SIGUTC_GetCURSO", conexion);
        comandoConsulta.CommandType = CommandType.StoredProcedure;

        comandoConsulta.Parameters.AddWithValue("@Comodin", comodin);
        comandoConsulta.Parameters.AddWithValue("@FILTRO1", filtro1);
        comandoConsulta.Parameters.AddWithValue("@FILTRO2", filtro2);
        comandoConsulta.Parameters.AddWithValue("@FILTRO3", filtro3);
        comandoConsulta.Parameters.AddWithValue("@FILTRO4", filtro4);

        try
        {
            conexion.Open();
            SqlDataReader reader = comandoConsulta.ExecuteReader();

            while (reader.Read())
            {
                listCursos.Add(
                    new CURSO
                    {
                        STRCOD_CURSO = reader["strCod_curso"].ToString(),
                        STRNOMBRE_CURSO = reader["strNombre_curso"].ToString(),
                        STRPARALELO_CURSO = reader["strParalelo_curso"].ToString(),
                        STRCOD_PER = reader["strCod_per"].ToString()
                    }
                );
            }
        }
        catch (Exception ex)
        {
            Console.Write("TIENES UN ERROR: " + ex.Message);
        }

        conexion.Close();
        return listCursos;
    }
}
