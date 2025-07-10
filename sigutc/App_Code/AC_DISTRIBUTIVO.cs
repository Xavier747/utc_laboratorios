using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de AC_DISTRIBUTIVO
/// </summary>
public class AC_DISTRIBUTIVO
{
    public AC_DISTRIBUTIVO()
    {

    }

    public AC_DISTRIBUTIVO(
        string _strCod_mate,
        string _strCod_curso,
        string _strCod_pro)
    {
        strCod_mate = _strCod_mate;
        strCod_curso = _strCod_curso;
        strCod_pro = _strCod_pro;
    }

    private string STRCOD_MATE;
    private string STRCOD_CURSO;
    private string STRCOD_PRO;
    private int NUMERR;
    private string MSG;
    private bool RESULTADO;

    public string strCod_mate
    {
        get { return STRCOD_MATE; }
        set { STRCOD_MATE = value; }
    }

    public string strCod_curso
    {
        get { return STRCOD_CURSO; }
        set { STRCOD_CURSO = value; }
    }

    public string strCod_pro
    {
        get { return STRCOD_PRO; }
        set { STRCOD_PRO = value; }
    }

    public int numerr
    {
        get { return NUMERR; }
        set { NUMERR = value; }
    }

    public string msg
    {
        get { return MSG; }
        set { MSG = value; }
    }

    public bool resultado
    {
        get { return RESULTADO; }
        set { RESULTADO = value; }
    }

    public List<AC_DISTRIBUTIVO> LoadAC_DISTRIBUTIVO(string comodin, string filtro1, string filtro2, string filtro3, string filtro4)
    {
        var listG = new List<AC_DISTRIBUTIVO>();

        using (var myConnection = new System.Data.SqlClient.SqlConnection(System.Web.Configuration.WebConfigurationManager.AppSettings["conexionBddProductos"]))
        {
            using (var myCommand = new System.Data.SqlClient.SqlCommand("SIGUTC_GetAC_DISTRIBUTIVO", myConnection))
            {
                myCommand.CommandType = System.Data.CommandType.StoredProcedure;

                var prmComodin = new System.Data.SqlClient.SqlParameter("@COMODIN", System.Data.SqlDbType.VarChar);
                prmComodin.Value = comodin;
                myCommand.Parameters.Add(prmComodin);

                var prmFiltro1 = new System.Data.SqlClient.SqlParameter("@FILTRO1", System.Data.SqlDbType.VarChar);
                prmFiltro1.Value = filtro1;
                myCommand.Parameters.Add(prmFiltro1);

                var prmFiltro2 = new System.Data.SqlClient.SqlParameter("@FILTRO2", System.Data.SqlDbType.VarChar);
                prmFiltro2.Value = filtro2;
                myCommand.Parameters.Add(prmFiltro2);

                var prmFiltro3 = new System.Data.SqlClient.SqlParameter("@FILTRO3", System.Data.SqlDbType.VarChar);
                prmFiltro3.Value = filtro3;
                myCommand.Parameters.Add(prmFiltro3);

                var prmFiltro4 = new System.Data.SqlClient.SqlParameter("@FILTRO4", System.Data.SqlDbType.VarChar);
                prmFiltro4.Value = filtro4;
                myCommand.Parameters.Add(prmFiltro4);

                try
                {
                    myConnection.Open();
                    using (var reader1 = myCommand.ExecuteReader())
                    {
                        while (reader1.Read())
                        {
                            AC_DISTRIBUTIVO acDistrib = new AC_DISTRIBUTIVO
                            {
                                strCod_mate = Convert.IsDBNull(reader1.GetValue(reader1.GetOrdinal("strCod_mate"))) ? string.Empty : Convert.ToString(reader1.GetValue(reader1.GetOrdinal("strCod_mate"))),
                                strCod_curso = Convert.IsDBNull(reader1.GetValue(reader1.GetOrdinal("strCod_curso"))) ? string.Empty : Convert.ToString(reader1.GetValue(reader1.GetOrdinal("strCod_curso"))),
                                strCod_pro = Convert.IsDBNull(reader1.GetValue(reader1.GetOrdinal("strCod_pro"))) ? string.Empty : Convert.ToString(reader1.GetValue(reader1.GetOrdinal("strCod_pro"))),

                                numerr = 0,
                                msg = "Datos extraidos satisfactoriamente...",
                                resultado = true
                            };
                            listG.Add(acDistrib);
                        }
                    }
                    myConnection.Close();
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
        return listG;
    }
}