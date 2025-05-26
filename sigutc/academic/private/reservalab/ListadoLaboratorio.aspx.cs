using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using ClassLibraryLaboratorios;
using ClassLibraryTesis;
using System.Web.Configuration;

public partial class academic_private_reservalab_ListadoLaboratorio : System.Web.UI.Page
{
    //Definicion de las variable de coneccion con la base de datos
    SqlConnection conexion = new SqlConnection(WebConfigurationManager.AppSettings["conexionBddProductos"]);

    LAB_LABORATORIOS laboratorio2 = new LAB_LABORATORIOS();
    LAB_RESPONSABLE responsable1 = new LAB_RESPONSABLE();
    LAB_SOFTWARE software1 = new LAB_SOFTWARE();
    LAB_LABSOFTWARE softLab = new LAB_LABSOFTWARE();
    LAB_EXCLUSIVO labExc = new LAB_EXCLUSIVO();
    LAB_TIPO tipoLaboratorio1 = new LAB_TIPO();
    UB_FACULTADES facultad = new UB_FACULTADES();
    UB_SEDES sede = new UB_SEDES();

    private Random rand = new Random();
    public static List<string> softwareSeleccionado;
    public static List<string> nuevosSoftwares;
    public static List<string> softwaresActuales;
    private string filtro;
    private string title;
    private string icon;

    //Metodo principal de la pagina
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Context.User.Identity.Name == "") Response.Redirect("~/academic/public/Login.aspx");

        if (!IsPostBack)
        {
            //llamado a los metodos que se ejecuta al iniciar la pagina     
            cargarTabla();

           
           
            cargarCampoAmplio();
        }
    }
    protected void gvLaboratorios_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLaboratorios.PageIndex = e.NewPageIndex;
        cargarTabla();
    }

    protected void gvLaboratorios_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            string codLab = e.CommandArgument.ToString();


            // Muestra el modal para actualizar los datos
            ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "$('#form_actualizar').modal('show');", true);
        }
        if (e.CommandName == "Eliminar")
        {
            string codLab = e.CommandArgument.ToString();
            string dtFecha_log = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff");
            string strUser_log = Context.User.Identity.Name;

            //Carga los detalles del laboratorio según el ID seleccionado
            laboratorio2.DelLAB_LABORATORIOS("xPkLab", codLab, dtFecha_log, strUser_log, "");

            string title = laboratorio2.resultado ? laboratorio2.msg : laboratorio2.msg;
            string icon = laboratorio2.resultado ? "success" : "error";

            string script = $"showAlertAndReload('{title}', '{icon}');";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowAlert", script, true);
        }
        if (e.CommandName == "Laboratoristas")
        {
            string codLab = e.CommandArgument.ToString();
      
        }
        if (e.CommandName == "Carrera")
        {
            Session["laboratorioId"] = e.CommandArgument.ToString();
            Response.Redirect("LaboratorioCarrera.aspx");
        }
    }
    public void cargarTabla()
    {
        laboratorio2.strCod_Fac = Session["ROL"].ToString() == "ADMINISTRADOR" ? "" : Context.User.Identity.Name;
        string tipoConsulta = Session["ROL"] != null && Convert.ToString(Session["ROL"]) == "ADMINISTRADOR" ? "ALL" : "xIdPersonal";
        string cedula = Context.User.Identity.Name;

        var tablaDatos = laboratorio2.LoadLAB_LABORATORIOS(tipoConsulta, cedula, "", "", "");
        if (tablaDatos != null && tablaDatos.Count > 0)
        {
            gvLaboratorios.DataSource = tablaDatos;
            gvLaboratorios.DataBind();
        }
    }
    public void cargarCampoAmplio()
    {
        string tipoConsulta = "ALL";

        SqlCommand comandoConsulta = new SqlCommand("SIGUTC_GetAREAC", conexion);
        comandoConsulta.Parameters.AddWithValue("@Comodin", tipoConsulta);
        comandoConsulta.Parameters.AddWithValue("@FILTRO1", "");
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
               
              
            }
        }
        catch (Exception ex)
        {
            Response.Write("TIENES UN ERROR: " + ex.Message);
        }
        conexion.Close();
    }
    protected void btnViewImage1_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        vistaCompletaImagen.ImageUrl = btn.CommandArgument;
        ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "$('#view-image').modal('show');", true);
    }

    protected void btnViewImage2_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        vistaCompletaImagen.ImageUrl = btn.CommandArgument;
        ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "$('#view-image').modal('show');", true);
    }

}

