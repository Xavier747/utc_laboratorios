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
using System.Web.Configuration;

public partial class academic_private_reservalab_InformacionLaboratorios : System.Web.UI.Page
{
    LAB_RESPONSABLE responsable1 = new LAB_RESPONSABLE();
    LAB_LABORATORIOS laboratorio2 = new LAB_LABORATORIOS();
    LAB_TIPO tipo1 = new LAB_TIPO();
    Personal personal1 = new Personal();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Context.User.Identity.Name == null) Response.Redirect("~/academic/private/Login.aspx");

        if (!IsPostBack)
        {
            cargarTabla();
        }
    }

    public void cargarTabla()
    {
        string strCod_lab = Session["laboratorioId"].ToString();

        var listLaboratorio = laboratorio2.LoadLAB_LABORATORIOS("xPK", strCod_lab, "", "", "");

        nombre.InnerText = listLaboratorio[0].strNombre_lab;
        txtUbicacion.InnerText = listLaboratorio[0].strUbicacion_lab;

        DataList1.DataSource = listLaboratorio;
        DataList1.DataBind();

        DataList2.DataSource = listLaboratorio;
        DataList2.DataBind();
    }
}