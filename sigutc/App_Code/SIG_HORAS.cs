using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

/// <summary>
/// Descripción breve de SIG_HORAS
/// </summary>
public class SIG_HORAS
{
    private string STRCOD_HORAS;
    private DateTime DTINICIO_HORAS;
    private DateTime DTFIN_HORAS;
    private string STRGRUPO_HORAS;
    private string STROBS1_HORAS;

    public string strCod_horas
    {
        get { return STRCOD_HORAS; }
        set { STRCOD_HORAS = value; }
    }

    public DateTime dtInicio_horas
    {
        get { return DTINICIO_HORAS; }
        set { DTINICIO_HORAS = value; }
    }

    public DateTime dtFin_horas
    {
        get { return DTFIN_HORAS; }
        set { DTFIN_HORAS = value; }
    }

    public string strGrupo_horas
    {
        get { return STRGRUPO_HORAS; }
        set { STRGRUPO_HORAS = value; }
    }

    public string strObs1_horas
    {
        get { return STROBS1_HORAS; }
        set { STROBS1_HORAS = value; }
    }


    public SIG_HORAS(){    }

    public List<SIG_HORAS> Load_SG_HORAS(string comodin, string filtro1, string filtro2, string filtro3, string filtro4)
    {
        var listHoras = new List<SIG_HORAS>();

        SqlConnection conexion = new SqlConnection(WebConfigurationManager.AppSettings["conexionBddProductos"]);
        SqlCommand comandoConsulta = new SqlCommand("SIGUTC_GetSIG_HORAS", conexion);
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
                listHoras.Add(new SIG_HORAS
                {
                    STRCOD_HORAS = reader["strCod_horas"].ToString(),
                    DTINICIO_HORAS = Convert.ToDateTime(reader["dtInicio_horas"]),
                    DTFIN_HORAS = Convert.ToDateTime(reader["dtFin_horas"]),
                    STRGRUPO_HORAS = reader["strGrupo_horas"].ToString(),
                    STROBS1_HORAS = reader["strObs1_horas"].ToString()
                });
            }
        }
        catch (Exception ex)
        {
            Console.Write("TIENES UN ERROR: " + ex.Message);
        }
        conexion.Close();
        return listHoras;
    }
}

