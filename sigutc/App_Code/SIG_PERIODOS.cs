using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

/// <summary>
/// Descripción breve de SIG_PERIODOS
/// </summary>
public class SIG_PERIODOS
{
    public SIG_PERIODOS()
    {
    }

    public SIG_PERIODOS(
        string strCod_per,
        int intNum_per,
        int intNumSemanas_per,
        string strCod_Sede,
        string strCod_Fac,
        string strCod_Car,
        DateTime dtFechaIni_per,
        DateTime dtFechaFin_per
        )
    {
        STRCOD_PER = strCod_per;
        INTNUM_PER = intNum_per;
        INTNUMSEMANAS_PER = intNumSemanas_per;
        STRCOD_SEDE = strCod_Sede;
        STRCOD_FAC = strCod_Fac;
        STRCOD_CAR = strCod_Car;
        DTFECHAINI_PER = dtFechaIni_per;
        DTFECHAFIN_PER = dtFechaFin_per;
    }

    private string STRCOD_PER;
    private int INTNUM_PER;
    private int INTNUMSEMANAS_PER;
    private string STRCOD_SEDE;
    private string STRCOD_FAC;
    private string STRCOD_CAR;
    private DateTime DTFECHAINI_PER;
    private DateTime DTFECHAFIN_PER;
    private int NUMERR;
    private string MSG;
    private bool RESULTADO;

    public string strCod_per
    {
        get
        {
            return STRCOD_PER;
        }
        set
        {
            STRCOD_PER = value;
        }
    }

    public int intNum_per
    {
        get
        {
            return INTNUM_PER;
        }
        set
        {
            INTNUM_PER = value;
        }
    }

    public int intNumSemanas_per
    {
        get
        {
            return INTNUMSEMANAS_PER;
        }
        set
        {
            INTNUMSEMANAS_PER = value;
        }
    }

    public string strCod_Sede
    {
        get
        {
            return STRCOD_SEDE;
        }
        set
        {
            STRCOD_SEDE = value;
        }
    }

    public string strCod_Fac
    {
        get
        {
            return STRCOD_FAC;
        }
        set
        {
            STRCOD_FAC = value;
        }
    }

    public string strCod_Car
    {
        get
        {
            return STRCOD_CAR;
        }
        set
        {
            STRCOD_CAR = value;
        }
    }

    public DateTime dtFechaIni_per
    {
        get
        {
            return DTFECHAINI_PER;
        }
        set
        {
            DTFECHAINI_PER = value;
        }
    }

    public DateTime dtFechaFin_per
    {
        get
        {
            return DTFECHAFIN_PER;
        }
        set
        {
            DTFECHAFIN_PER = value;
        }
    }

    public int numerr
    {
        get
        {
            return NUMERR;
        }
        set
        {
            NUMERR = value;
        }
    }

    public string msg
    {
        get
        {
            return MSG;
        }
        set
        {
            MSG = value;
        }
    }

    public bool resultado
    {
        get
        {
            return RESULTADO;
        }
        set
        {
            RESULTADO = value;
        }
    }

    ///////////////// Método Get /////////////////
    public List<SIG_PERIODOS> LoadSIG_PERIODOS(string comodin, string filtro1, string filtro2, string filtro3, string filtro4)
    {
        var listG = new List<SIG_PERIODOS>();
        using (SqlConnection myConnection = new SqlConnection(WebConfigurationManager.AppSettings["conexionBddProductos"]))
        {
            using (SqlCommand myCommand = new SqlCommand("SIGUTC_GetSIG_PERIODOS", myConnection))
            {
                myCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter prmComodin = new SqlParameter("@COMODIN", SqlDbType.VarChar);
                prmComodin.Value = comodin;
                myCommand.Parameters.Add(prmComodin);

                SqlParameter prmFiltro1 = new SqlParameter("@FILTRO1", SqlDbType.VarChar);
                prmFiltro1.Value = filtro1;
                myCommand.Parameters.Add(prmFiltro1);

                SqlParameter prmFiltro2 = new SqlParameter("@FILTRO2", SqlDbType.VarChar);
                prmFiltro2.Value = filtro2;
                myCommand.Parameters.Add(prmFiltro2);

                SqlParameter prmFiltro3 = new SqlParameter("@FILTRO3", SqlDbType.VarChar);
                prmFiltro3.Value = filtro3;
                myCommand.Parameters.Add(prmFiltro3);

                SqlParameter prmFiltro4 = new SqlParameter("@FILTRO4", SqlDbType.VarChar);
                prmFiltro4.Value = filtro4;
                myCommand.Parameters.Add(prmFiltro4);

                try
                {
                    myConnection.Open();
                    using (SqlDataReader reader1 = myCommand.ExecuteReader())
                    {
                        while (reader1.Read())
                        {
                            SIG_PERIODOS sigPeriodos = new SIG_PERIODOS
                            {
                                strCod_per = Convert.IsDBNull(reader1.GetValue(reader1.GetOrdinal("strCod_per"))) ? string.Empty : Convert.ToString(reader1.GetValue(reader1.GetOrdinal("strCod_per"))),
                                intNum_per = Convert.IsDBNull(reader1.GetValue(reader1.GetOrdinal("intNum_per"))) ? 0 : Convert.ToInt32(reader1.GetValue(reader1.GetOrdinal("intNum_per"))),
                                intNumSemanas_per = Convert.IsDBNull(reader1.GetValue(reader1.GetOrdinal("intNumSemanas_per"))) ? 0 : Convert.ToInt32(reader1.GetValue(reader1.GetOrdinal("intNumSemanas_per"))),
                                strCod_Sede = Convert.IsDBNull(reader1.GetValue(reader1.GetOrdinal("strCod_Sede"))) ? string.Empty : Convert.ToString(reader1.GetValue(reader1.GetOrdinal("strCod_Sede"))),
                                strCod_Fac = Convert.IsDBNull(reader1.GetValue(reader1.GetOrdinal("strCod_Fac"))) ? string.Empty : Convert.ToString(reader1.GetValue(reader1.GetOrdinal("strCod_Fac"))),
                                strCod_Car = Convert.IsDBNull(reader1.GetValue(reader1.GetOrdinal("strCod_Car"))) ? string.Empty : Convert.ToString(reader1.GetValue(reader1.GetOrdinal("strCod_Car"))),
                                dtFechaIni_per = Convert.IsDBNull(reader1.GetValue(reader1.GetOrdinal("dtFechaIni_per"))) ? DateTime.MinValue : Convert.ToDateTime(reader1.GetValue(reader1.GetOrdinal("dtFechaIni_per"))),
                                dtFechaFin_per = Convert.IsDBNull(reader1.GetValue(reader1.GetOrdinal("dtFechaFin_per"))) ? DateTime.MinValue : Convert.ToDateTime(reader1.GetValue(reader1.GetOrdinal("dtFechaFin_per"))),
                                numerr = 0,
                                msg = "Datos extraidos satisfactoriamente...",
                                resultado = true
                            };
                            listG.Add(sigPeriodos);
                        }
                    }
                    myConnection.Close();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }

                return listG;
            }
        }
    }
}