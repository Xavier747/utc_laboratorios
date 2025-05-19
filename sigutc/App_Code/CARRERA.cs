using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

public class CARRERA
{
    private string STRCOD_CAR;
    private string STRCOD_SEDE;
    private string STRCOD_FAC;
    private string STRNOMBRE_CAR;
    private string STRESTADO_CAR;
    private string STRTIPO_CAR;
    private string STRGRUPO_CAR;
    private bool BITESTADO_CAR;
    private DateTime DTFECHA_LOG;
    private string STROBS1_CAR;
    private string STROBS2_CAR;
    private string STROBS3_CAR;
    private string STROBS4_CAR;
    private string STRUSER_LOG;
    private string STRMODALIDAD_CAR;
    private string STRCAMPUS_CAR;

    public string strcod_car
    {
        get { return STRCOD_CAR; }
        set { STRCOD_CAR = value; }
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

    public string strnombre_car
    {
        get { return STRNOMBRE_CAR; }
        set { STRNOMBRE_CAR = value; }
    }

    public string strestado_car
    {
        get { return STRESTADO_CAR; }
        set { STRESTADO_CAR = value; }
    }

    public string strtipo_car
    {
        get { return STRTIPO_CAR; }
        set { STRTIPO_CAR = value; }
    }

    public string strgrupo_car
    {
        get { return STRGRUPO_CAR; }
        set { STRGRUPO_CAR = value; }
    }

    public bool bitestado_car
    {
        get { return BITESTADO_CAR; }
        set { BITESTADO_CAR = value; }
    }

    public DateTime dtfecha_log
    {
        get { return DTFECHA_LOG; }
        set { DTFECHA_LOG = value; }
    }

    public string strobs1_car
    {
        get { return STROBS1_CAR; }
        set { STROBS1_CAR = value; }
    }

    public string strobs2_car
    {
        get { return STROBS2_CAR; }
        set { STROBS2_CAR = value; }
    }

    public string strobs3_car
    {
        get { return STROBS3_CAR; }
        set { STROBS3_CAR = value; }
    }

    public string strobs4_car
    {
        get { return STROBS4_CAR; }
        set { STROBS4_CAR = value; }
    }

    public string struser_log
    {
        get { return STRUSER_LOG; }
        set { STRUSER_LOG = value; }
    }

    public string strmodalidad_car
    {
        get { return STRMODALIDAD_CAR; }
        set { STRMODALIDAD_CAR = value; }
    }

    public string strcampus_car
    {
        get { return STRCAMPUS_CAR; }
        set { STRCAMPUS_CAR = value; }
    }

    public CARRERA() { }

    public List<CARRERA> Load_CARRERAS(string comodin, string filtro1, string filtro2, string filtro3, string filtro4)
    {
        List<CARRERA> listCarreras = new List<CARRERA>();
        SqlConnection conexion = new SqlConnection(WebConfigurationManager.AppSettings["conexionBddProductos"]);
        SqlCommand comandoConsulta = new SqlCommand("SIGUTC_GetUB_CARRERAS", conexion);
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
                listCarreras.Add(
                    new CARRERA
                    {
                        STRCOD_CAR = reader1["strCod_Car"].ToString(),
                        STRCOD_SEDE = reader1["strCod_Sede"].ToString(),
                        STRCOD_FAC = reader1["strCod_Fac"].ToString(),
                        STRNOMBRE_CAR = reader1["strnombre_car"].ToString(),
                        STRESTADO_CAR = reader1["strestado_car"].ToString(),
                        STRTIPO_CAR = reader1["strtipo_car"].ToString(),
                        STRGRUPO_CAR = reader1["strgrupo_car"].ToString(),
                        BITESTADO_CAR = Convert.ToBoolean(reader1["bitestado_car"]),
                        DTFECHA_LOG = Convert.ToDateTime(reader1["dtfecha_log"]),
                        STROBS1_CAR = reader1["strobs1_car"].ToString(),
                        STROBS2_CAR = reader1["strobs2_car"].ToString(),
                        STROBS3_CAR = reader1["strobs3_car"].ToString(),
                        STROBS4_CAR = reader1["strobs4_car"].ToString(),
                        STRUSER_LOG = reader1["struser_log"].ToString(),
                        STRMODALIDAD_CAR = reader1["strmodalidad_car"].ToString(),
                        STRCAMPUS_CAR = reader1["strcampus_car"].ToString()
                    }
                );
            }
        }
        catch (Exception ex)
        {
            Console.Write("TIENES UN ERROR: " + ex.Message);
        }

        conexion.Close();
        return listCarreras;
    }
}
