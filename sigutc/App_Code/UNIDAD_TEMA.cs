using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

public class UNIDAD_TEMA
{
    private string STRCOD_UNIDTEM;
    private string STRCOD_SILABOC;
    private string STRNUM_UNIDTEM;
    private string STRDESC_UNIDTEM;

    public string strcod_unidtem
    {
        get { return STRCOD_UNIDTEM; }
        set { STRCOD_UNIDTEM = value; }
    }

    public string strcod_silaboc
    {
        get { return STRCOD_SILABOC; }
        set { STRCOD_SILABOC = value; }
    }

    public string strnum_unidtem
    {
        get { return STRNUM_UNIDTEM; }
        set { STRNUM_UNIDTEM = value; }
    }

    public string strdesc_unidtem
    {
        get { return STRDESC_UNIDTEM; }
        set { STRDESC_UNIDTEM = value; }
    }

    public UNIDAD_TEMA() { }

    public List<UNIDAD_TEMA> Load_UNIDAD_TEMA(string comodin, string filtro1, string filtro2, string filtro3, string filtro4)
    {
        List<UNIDAD_TEMA> listaUnidades = new List<UNIDAD_TEMA>();
        SqlConnection conexion = new SqlConnection(WebConfigurationManager.AppSettings["conexionBddProductos"]);
        SqlCommand comandoConsulta = new SqlCommand("SIGUTC_GetUNIDAD_TEMA", conexion);
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
                listaUnidades.Add(
                    new UNIDAD_TEMA
                    {
                        STRCOD_UNIDTEM = reader["strCod_unidTem"].ToString(),
                        STRCOD_SILABOC = reader["strCod_silaboc"].ToString(),
                        STRNUM_UNIDTEM = reader["strNum_unidTem"].ToString(),
                        STRDESC_UNIDTEM = reader["strDesc_unidTem"].ToString()
                    }
                );
            }
        }
        catch (Exception ex)
        {
            Console.Write("TIENES UN ERROR: " + ex.Message);
        }

        conexion.Close();
        return listaUnidades;
    }
}
