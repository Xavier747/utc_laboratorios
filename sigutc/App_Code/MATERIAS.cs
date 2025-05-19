using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

public class MATERIAS
{
    private string STRCOD_MATE;
    private string STRCODALT_MATE;
    private string STRNOMBRE_MATE;
    private string STRCOD_SEDE;
    private string STRCOD_FAC;
    private string STRCOD_CAR;

    public string strcod_mate
    {
        get { return STRCOD_MATE; }
        set { STRCOD_MATE = value; }
    }

    public string strcodalt_mate
    {
        get { return STRCODALT_MATE; }
        set { STRCODALT_MATE = value; }
    }

    public string strnombre_mate
    {
        get { return STRNOMBRE_MATE; }
        set { STRNOMBRE_MATE = value; }
    }

    public string strcod_sede
    {
        get { return STRCOD_SEDE; }
        set { STRCOD_SEDE = value; }
    }

    public string strcod_fac
    {
        get { return STRCOD_FAC; }
        set { STRCOD_FAC = value; }
    }

    public string strcod_car
    {
        get { return STRCOD_CAR; }
        set { STRCOD_CAR = value; }
    }

    public MATERIAS() { }

    public List<MATERIAS> Load_MATERIAS(string comodin, string filtro1, string filtro2, string filtro3, string filtro4)
    {
        List<MATERIAS> listMaterias = new List<MATERIAS>();

        SqlConnection conexion = new SqlConnection(WebConfigurationManager.AppSettings["conexionBddProductos"]);
        SqlCommand comandoConsulta = new SqlCommand("SIGUTC_GetMATERIAS", conexion);
        comandoConsulta.Parameters.AddWithValue("@Comodin", comodin);
        comandoConsulta.Parameters.AddWithValue("@FILTRO1", filtro1);
        comandoConsulta.Parameters.AddWithValue("@FILTRO2", filtro2);
        comandoConsulta.Parameters.AddWithValue("@FILTRO3", filtro3);
        comandoConsulta.Parameters.AddWithValue("@FILTRO4", filtro4);
        comandoConsulta.CommandType = CommandType.StoredProcedure;
        try
        {
            conexion.Open();
            SqlDataReader reader1 = comandoConsulta.ExecuteReader();

            while (reader1.Read())
            {
                listMaterias.Add(
                    new MATERIAS
                    {
                        STRCOD_MATE = reader1["strCod_mate"].ToString(),
                        STRCODALT_MATE = reader1["strCodAlt_mate"].ToString(),
                        STRNOMBRE_MATE = reader1["strNombre_mate"].ToString(),
                        STRCOD_SEDE = reader1["strCod_Sede"].ToString(),
                        STRCOD_FAC = reader1["strCod_Fac"].ToString(),
                        STRCOD_CAR = reader1["strCod_Car"].ToString()
                    }
                );
            }
        }
        catch (Exception ex)
        {
            Console.Write("TIENES UN ERROR: " + ex.Message);
        }
        conexion.Close();
        return listMaterias;
    }
}