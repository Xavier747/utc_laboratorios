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

public partial class academic_public_reservalab_ReservaLaboratorio : System.Web.UI.Page
{
    string cadenaConexion;
    SqlConnection conexion;

    LAB_LABORATORIOS laboratorio2 = new LAB_LABORATORIOS();
    LAB_RESPONSABLE responsable1 = new LAB_RESPONSABLE();
    Personal personal1 = new Personal();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Context.User.Identity.Name == null) Response.Redirect("~/academic/public/Login.aspx");

        if (!IsPostBack)
        {
            llenarFormulario();
        }
    }
    private void llenarFormulario()
    {
        if (Session["laboratorioId"] == null)
        {
            Response.Redirect("~/academic/private/reservalab/ListadoLaboratorio.aspx");
        }
        string codLab = Session["laboratorioId"].ToString();
        var listLab = laboratorio2.LoadLAB_LABORATORIOS("xPK", codLab, "", "", "");

        titulo.InnerText = listLab[0].strNombre_lab;
        txtNombreLaboratorio.Text = listLab[0].strNombre_lab;
        txtNombreLaboratorioDet.Text = listLab[0].strNombre_lab;
        txtNombreLabAct.Text = listLab[0].strNombre_lab;

        var listResponsable = responsable1.LoadLAB_RESPONSABLE("xLaboratorio", codLab, "", "", "");

        for (int i = 0; i < listResponsable.Count; i++)
        {
            var tipoResp = listResponsable[i];
            string cedula = tipoResp.strCod_res;
            var listPersonal = personal1.Load_PERSONAL("xCEDULA", cedula, "", "", "");
            if (tipoResp.strTipo_respo == "Responsable Academico")
            {
                txtResponsableAcademico.Text = listPersonal[0].apellido_alu + " " + listPersonal[0].apellidom_alu + " " + listPersonal[0].nombre_alu;
                txtRespAcadDet.Text = listPersonal[0].apellido_alu + " " + listPersonal[0].apellidom_alu + " " + listPersonal[0].nombre_alu;
                txtNombreRespAcdAct.Text = listPersonal[0].apellido_alu + " " + listPersonal[0].apellidom_alu + " " + listPersonal[0].nombre_alu;
            }
            if (tipoResp.strTipo_respo == "Responsable Administrativo")
            {
                txtResponsableAdministrativo.Text = listPersonal[0].apellido_alu + " " + listPersonal[0].apellidom_alu + " " + listPersonal[0].nombre_alu;
                txtRespAdminDet.Text = listPersonal[0].apellido_alu + " " + listPersonal[0].apellidom_alu + " " + listPersonal[0].nombre_alu;
                txtNombreRespAddAct.Text = listPersonal[0].apellido_alu + " " + listPersonal[0].apellidom_alu + " " + listPersonal[0].nombre_alu;
            }
        }
        llenarDocenteSolicitante();
    }

    public void llenarDocenteSolicitante()
    {
        string codLab = Session["laboratorioId"].ToString();
        var listLab = laboratorio2.LoadLAB_LABORATORIOS("xPK", codLab, "", "", "");

        string codSede = listLab[0].strCod_Sede;
        string codFacultad = listLab[0].strCod_Fac;

        var docente = personal1.Load_PERSONAL("xDocente", codFacultad, codSede, "", "");

        if (docente.Count > 0)
        {
            var listaConcatenada = docente
                .Select(labResp => new {
                    CEDULA_ALU = labResp.cedula_alu,
                    NOMBRE_COMPLETO = labResp.apellido_alu + " " + labResp.apellidom_alu + " " + labResp.nombre_alu // Aquí concatenas lo que necesites
                }).ToList();

            ddlDocentes.DataSource = listaConcatenada;
            ddlDocentes.DataTextField = "NOMBRE_COMPLETO";
            ddlDocentes.DataValueField = "CEDULA_ALU";
            ddlDocentes.DataBind();

            txtEmail.Text = docente[0].correo_alu;
        }
    }
}