using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Services;
using ClassLibraryLaboratorios;

public partial class academic_private_reservalab_InformacionLaboratorios : System.Web.UI.Page
{
    string cadenaConexion;
    SqlConnection conexion;

    public LAB_RESPONSABLE responsable1;
    public LAB_LABORATORIOS laboratorio2;
    public LAB_TIPO tipo1;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.cadenaConexion = ConfigurationManager.ConnectionStrings["conexionBddProductos"].ConnectionString;
        this.conexion = new SqlConnection(this.cadenaConexion);

        //laboratorio2 = new LAB_LABORATORIOS(cadenaConexion);
        //responsable1 = new LAB_RESPONSABLE(cadenaConexion);
        //tipo1 = new LAB_TIPO(cadenaConexion);

        if (Session["Cedula"] == null) Response.Redirect("~/Views/Login.aspx");

        if (!IsPostBack)
        {
            cargarTabla();
        }
    }

    public void cargarTabla()
    {
        try
        {
            //    string comodin = "xPK";
            //    string filtro = "";

            //    laboratorio2.strCod_Fac = Session["laboratorioId"].ToString();
            //    DataTable tablaDatos = laboratorio2.obtenerLaboratorios(comodin, filtro);

            //    nombre.InnerText = tablaDatos.Rows[0]["strNombre_lab"].ToString();
            //    imgLaboratorio1.ImageUrl = "~/images/Laboratorio/" + tablaDatos.Rows[0]["strFotografia1_lab"].ToString();
            //    imgLaboratorio2.ImageUrl = "~/images/Laboratorio/" + tablaDatos.Rows[0]["strFotografia2_lab"].ToString();
            //    txtUbicacion.InnerText = tablaDatos.Rows[0]["strUbicacion_lab"].ToString();

            //tipo1.strCod_tipoLab = tablaDatos.Rows[0]["strCod_tipoLab"].ToString();
            //DataTable tipo = tipo1.consultarTipoLaboratorio(comodin);
            //tipoLaboratorio.InnerHtml = $"<i class='fa fa-check-circle'></i> {tipo.Rows[0]["strNombre_tipoLab"].ToString()}";

            //responsable1.strCod_lab = laboratorio2.strCod_Fac;
            //string tipoConsulta = "xLaboratorio";

            //    List<LAB_RESPONSABLE> responsable = responsable1.detalleResponsableLaboratorio(tipoConsulta);

            //    foreach (LAB_RESPONSABLE resp in responsable)
            //    {
            //        if (resp.strTipo_respo == "Responsable Academico")
            //        {
            //            imgRespAcad.ImageUrl = "~/images/Usuario/" + resp.strObs2_respo;
            //            lblRespAcadInf.InnerText = resp.strObs1_respo;
            //        }
            //        if (resp.strTipo_respo == "Responsable Administrativo")
            //        {
            //            imgRespAdmin.ImageUrl = "~/images/Usuario/" + resp.strObs2_respo;
            //            lblRespAdminInf.InnerText = resp.strObs1_respo;
            //        }
            //    }
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR: " + ex.Message);
        }
    }
}

