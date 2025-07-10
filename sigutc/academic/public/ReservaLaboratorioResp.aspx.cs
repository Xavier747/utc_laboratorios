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
    LAB_LABORATORIOS laboratorio2 = new LAB_LABORATORIOS();
    LAB_RESPONSABLE responsable1 = new LAB_RESPONSABLE();
    Personal personal1 = new Personal();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Context.User.Identity.Name == "") Response.Redirect("~/academic/public/Login.aspx");

        if (!IsPostBack)
        {
            SeguridadUTC sutc = new SeguridadUTC();

            if (Request.QueryString["In"] != null)
            {
                lblCrono.Text = sutc.Desencripta(Request.Params["In"].ToString());

                string[] partes = lblCrono.Text.Split('_');

                lblSede.Text = partes[0];
                lblFacultad.Text = partes[1];

                //Llamado a los metodos que se deben cargar con la pagina
                llenarFormulario();
            }
            else
            {
                Response.Redirect("~/academic/private/reservalab/ListadoLaboratorio.aspx");
            }
        }
    }
    private void llenarFormulario()
    {
        string codLab = lblCrono.Text;
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
    }
}