using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using ClassLibraryLaboratorios;
using ClassLibraryTesis;

public partial class academic_private_reservalab_ReservaLaboratorio : System.Web.UI.Page
{
    string cadenaConexion;
    SqlConnection conexion;

    LAB_LABORATORIOS laboratorio2 = new LAB_LABORATORIOS();
    LAB_RESPONSABLE responsable1 = new LAB_RESPONSABLE();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Context.User.Identity.Name == null) Response.Redirect("~/academic/private/Login.aspx");

        if (!IsPostBack)
            {
                llenarFormulario();
                llenarTipoMotivo();
            }
    }
    private void llenarFormulario()
    {
        //string comodin = "xPK";
        //string filtro = "";

        //laboratorio2.strCod_Fac = Session["laboratorioId"].ToString();
        //DataTable tablaDatos = laboratorio2.obtenerLaboratorios(comodin, filtro);

        //titulo.InnerText = tablaDatos.Rows[0]["strNombre_lab"].ToString();

        //txtNombreLaboratorio.Text = tablaDatos.Rows[0]["strNombre_lab"].ToString();
        //txtNombreLaboratorioDet.Text = tablaDatos.Rows[0]["strNombre_lab"].ToString();
        //txtNombreLabAct.Text = tablaDatos.Rows[0]["strNombre_lab"].ToString();

        //responsable1.strCod_lab = laboratorio2.strCod_Fac;
        //string tipoConsulta = "xLaboratorio";

        //List<LAB_RESPONSABLE> responsable = responsable1.detalleResponsableLaboratorio(tipoConsulta);

        //foreach (LAB_RESPONSABLE resp in responsable)
        //{
        //    if (resp.strTipo_respo == "Responsable Academico")
        //    {
        //        txtResponsableAcademico.Text = resp.strObs1_respo;
        //        txtRespAcadDet.Text = resp.strObs1_respo;
        //        txtNombreRespAcdAct.Text = resp.strObs1_respo;
        //    }
        //    if (resp.strTipo_respo == "Responsable Administrativo")
        //    {
        //        txtResponsableAdministrativo.Text = resp.strObs1_respo;
        //        txtRespAdminDet.Text = resp.strObs1_respo;
        //        txtNombreRespAddAct.Text = resp.strObs1_respo;
        //    }
        //}

        //llenarDocenteSolicitante();
    }

    public void llenarDocenteSolicitante()
    {
        string tipoConsulta = "xCEDULA";
        string cedulaAlum = Session["Cedula"].ToString();

        SqlCommand comandoConsulta = new SqlCommand("SIGUTC_GetPERSONAL", conexion);
        comandoConsulta.Parameters.AddWithValue("@Comodin", tipoConsulta);
        comandoConsulta.Parameters.AddWithValue("@FILTRO1", cedulaAlum);
        comandoConsulta.Parameters.AddWithValue("@FILTRO2", "");
        comandoConsulta.Parameters.AddWithValue("@FILTRO3", "");
        comandoConsulta.Parameters.AddWithValue("@FILTRO4", "");
        comandoConsulta.CommandType = CommandType.StoredProcedure;
        try
        {
            this.conexion.Open();
            SqlDataAdapter adaptadorAlbum = new SqlDataAdapter(comandoConsulta);
            DataTable dt = new DataTable();
            adaptadorAlbum.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                //txtEmail.Text = row["CORREO_ALU"].ToString();
                //txtNombreSolicitante.Text = row["Responsable"].ToString();
            }
        }
        catch (Exception ex)
        {
            Response.Write("TIENES UN ERROR: " + ex.Message);
        }
        conexion.Close();
    }

    private void llenarTipoMotivo()
    {
        object[] tipoMotivo = { "CLASE PRÁCTICA", "GUIA PRÁCTICA", "TITULACIÓN", "INVESTIGACIÓN", "POSGRADOS", "EXÁMENES FINALES", "EXÁMENES DE GRACIA" };

        //selectTipoMotivo.Items.Add(new ListItem("-- Seleccione una opcion --", ""));
        //selectTipoMotivoAct.Items.Add(new ListItem("-- Seleccione una opcion --", ""));

        for (int i = 0; i < tipoMotivo.Length; i++)
        {
            //selectTipoMotivo.Items.Add(new ListItem(tipoMotivo[i].ToString(), tipoMotivo[i].ToString()));
            //selectTipoMotivoAct.Items.Add(new ListItem(tipoMotivo[i].ToString(), tipoMotivo[i].ToString()));
        }
    }

    [WebMethod]
    public static List<Asignatura> obtenerAsignaturas(int dia)
    {
        List<Asignatura> asignaturas = new List<Asignatura>();

        string cadenaConexion = ConfigurationManager.ConnectionStrings["conexionBddProductos"].ConnectionString;
        SqlConnection conexion = new SqlConnection(cadenaConexion);

        string tipoConsulta = "xDia";

        SqlCommand comandoConsulta = new SqlCommand("SIGUTC_GetMATERIAS", conexion);
        comandoConsulta.Parameters.AddWithValue("@Comodin", tipoConsulta);
        comandoConsulta.Parameters.AddWithValue("@FILTRO1", dia);
        comandoConsulta.Parameters.AddWithValue("@FILTRO2", HttpContext.Current.Session["Cedula"].ToString());
        comandoConsulta.Parameters.AddWithValue("@FILTRO3", "");
        comandoConsulta.Parameters.AddWithValue("@FILTRO4", "");
        comandoConsulta.CommandType = CommandType.StoredProcedure;

        try
        {
            conexion.Open();

            SqlDataAdapter adaptadorAlbum = new SqlDataAdapter(comandoConsulta);
            DataTable dt = new DataTable();
            adaptadorAlbum.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                asignaturas.Add(new Asignatura
                {
                    id = row["strCod_mate"].ToString(),
                    nombre = row["strNombre_mate"].ToString()
                });
            }

        }
        catch (SqlException sqlEx)
        {
            // Manejar errores de SQL
            throw new Exception("Error de SQL: " + sqlEx.Message);
        }
        conexion.Close();

        return asignaturas;
    }

    [WebMethod]
    public static Object[] consultarDetalleAsignatura(string asignaturaId, int dia)
    {
        Object[] detalleAsignaturas = new Object[5];
        List<SIG_HORAS> listaHoras = new List<SIG_HORAS>();

        string cadenaConexion = ConfigurationManager.ConnectionStrings["conexionBddProductos"].ConnectionString;
        SqlConnection conexion = new SqlConnection(cadenaConexion);

        string tipoConsulta = "xAsignatura";

        SqlCommand comandoConsulta = new SqlCommand("SIGUTC_GetMATERIAS", conexion);
        comandoConsulta.Parameters.AddWithValue("@Comodin", tipoConsulta);
        comandoConsulta.Parameters.AddWithValue("@FILTRO1", asignaturaId);
        comandoConsulta.Parameters.AddWithValue("@FILTRO2", dia);
        comandoConsulta.Parameters.AddWithValue("@FILTRO3", "");
        comandoConsulta.Parameters.AddWithValue("@FILTRO4", "");
        comandoConsulta.CommandType = CommandType.StoredProcedure;

        try
        {
            conexion.Open();

            SqlDataAdapter adaptadorAlbum = new SqlDataAdapter(comandoConsulta);
            DataTable dt = new DataTable();
            adaptadorAlbum.Fill(dt);

            // Llenar el DropDownList con los datos obtenidos
            if (dt.Rows.Count > 0)
            {
                // Solo se llena una vez porque todos los registros tienen mismos datos excepto hora
                DataRow row = dt.Rows[0];
                detalleAsignaturas[0] = row["strNombre_curso"].ToString();
                detalleAsignaturas[1] = row["strParalelo_curso"].ToString();
                detalleAsignaturas[2] = row["strNombre_Car"].ToString();
                detalleAsignaturas[3] = row["totalAsistentes"].ToString();
            }

            // Agregamos todas las horas disponibles
            foreach (DataRow row in dt.Rows)
            {
                string codHora = row["strCod_horas"].ToString();
                if (!string.IsNullOrEmpty(codHora))
                {
                    listaHoras.Add(
                        new SIG_HORAS
                        {
                            strCod_horas = row["strCod_horas"].ToString(),
                            dtInicio_horas = Convert.ToDateTime(row["dtInicio_horas"]),
                            dtFin_horas = Convert.ToDateTime(row["dtFin_horas"])
                        }
                    );
                }
            }

            detalleAsignaturas[4] = listaHoras;
        }
        catch (SqlException sqlEx)
        {
            // Manejar errores de SQL
            throw new Exception("Error de SQL: " + sqlEx.Message);
        }
        conexion.Close();
        return detalleAsignaturas;
    }

    [WebMethod]
    public static void guardarReservacion(object[] resbLab)
    {
        //string cadenaConexion = ConfigurationManager.ConnectionStrings["conexionBddProductos"].ConnectionString;
        //LAB_RESERVA reservacion1 = new LAB_RESERVA(cadenaConexion);

        //reservacion1.strCod_reser = HttpContext.Current.Session["laboratorioId"].ToString() + resbLab[4];
        //reservacion1.strTema_reser = resbLab[0].ToString();
        //reservacion1.strDescripcion_reser = resbLab[1].ToString();
        //reservacion1.strMateriales_reser = resbLab[2].ToString();
        //reservacion1.dtFechaInicio_reser = Convert.ToDateTime(resbLab[3]);
        //reservacion1.dtFechaFin_reser = Convert.ToDateTime(resbLab[4]);
        //reservacion1.strTipo_rese = resbLab[5].ToString();
        //reservacion1.intTotalAsistente_reser = Convert.ToInt32(resbLab[6]);
        //reservacion1.strId_HORARIO = resbLab[7].ToString();
        //reservacion1.strCod_lab = HttpContext.Current.Session["laboratorioId"].ToString();

        //reservacion1.registrarReservacion();
    }

    //[WebMethod]
    //public static string listarReservas()
    //{
    //string cadenaConexion = ConfigurationManager.ConnectionStrings["conexionBddProductos"].ConnectionString;
    //LAB_RESERVA reservar1 = new LAB_RESERVA(cadenaConexion);

    //try
    //{
    //    string tipoConsulta = "xLaboratorio";
    //    string codLab = HttpContext.Current.Session["laboratorioId"].ToString();

    //    List<LAB_RESERVA> eventos = reservar1.ObtenerReservas(tipoConsulta, codLab);

    //    // Modificar las fechas dentro de la lista de eventos
    //    var eventosFormateados = new List<object>();
    //    foreach (var evento in eventos)
    //    {
    //        eventosFormateados.Add(new
    //        {
    //            id = evento.strCod_reser,
    //            titulo = evento.strTema_reser,
    //            inicio = evento.dtFechaInicio_reser.ToString("yyyy-MM-ddTHH:mm:ss"),
    //            fin = evento.dtFechaFin_reser.ToString("yyyy-MM-ddTHH:mm:ss")
    //        });
    //    }

    //    return new JavaScriptSerializer().Serialize(eventosFormateados);
    //}
    //catch (Exception ex)
    //{
    //    return new JavaScriptSerializer().Serialize(new { error = ex.Message });
    //}
    //}

    //[WebMethod]
    //public static Object[] detallarEvento(string id)
    //{
    //Object[] evento = new Object[13];
    //string cadenaConexion = ConfigurationManager.ConnectionStrings["conexionBddProductos"].ConnectionString;

    //try
    //{
    //    LAB_RESERVA reservacion1 = new LAB_RESERVA(cadenaConexion);
    //    evento = reservacion1.detarEvento(id);
    //}
    //catch (Exception ex)
    //{
    //    Console.WriteLine("Error: " + ex);
    //}
    //return evento;
    //    }
//}

public class Asignatura
{
    public string id { get; set; }
    public string nombre { get; set; }
}

public class SIG_HORAS
{
    public string strCod_horas { get; set; }
    public DateTime dtInicio_horas { get; set; }
    public DateTime dtFin_horas { get; set; }
}
}