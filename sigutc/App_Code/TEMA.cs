using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

public class TEMA
{
    private string STRCOD_TEMA;
    private string STRCOD_UNIDTEM;
    private string STRNUM_TEMA;
    private string STRDESC_TEMA;

    public string strCod_tema
    {
        get { return STRCOD_TEMA; }
        set { STRCOD_TEMA = value; }
    }

    public string strCod_unidTem
    {
        get { return STRCOD_UNIDTEM; }
        set { STRCOD_UNIDTEM = value; }
    }

    public string strNum_tema
    {
        get { return STRNUM_TEMA; }
        set { STRNUM_TEMA = value; }
    }

    public string strDesc_tema
    {
        get { return STRDESC_TEMA; }
        set { STRDESC_TEMA = value; }
    }

    public TEMA() { }

    public List<TEMA> Load_TEMA(string comodin, string filtro1, string filtro2, string filtro3, string filtro4)
    {
        List<TEMA> listTemas = new List<TEMA>();

        SqlConnection conexion = new SqlConnection(WebConfigurationManager.AppSettings["conexionBddProductos"]);
        SqlCommand comandoConsulta = new SqlCommand("SIGUTC_GetTEMA", conexion);
        comandoConsulta.Parameters.AddWithValue("@Comodin", comodin);
        comandoConsulta.Parameters.AddWithValue("@FILTRO1", filtro1);
        comandoConsulta.Parameters.AddWithValue("@FILTRO2", filtro2);
        comandoConsulta.Parameters.AddWithValue("@FILTRO3", filtro3);
        comandoConsulta.Parameters.AddWithValue("@FILTRO4", filtro4);
        comandoConsulta.CommandType = CommandType.StoredProcedure;

        try
        {
            conexion.Open();
            SqlDataReader reader = comandoConsulta.ExecuteReader();

            while (reader.Read())
            {
                listTemas.Add(new TEMA
                {
                    STRCOD_TEMA = reader["strCod_tema"].ToString(),
                    STRCOD_UNIDTEM = reader["strCod_unidTem"].ToString(),
                    STRNUM_TEMA = reader["strNum_tema"].ToString(),
                    STRDESC_TEMA = reader["strDesc_tema"].ToString()
                });
            }
        }
        catch (Exception ex)
        {
            Console.Write("TIENES UN ERROR: " + ex.Message);
        }
        finally
        {
            conexion.Close();
        }

        return listTemas;
    }
}
