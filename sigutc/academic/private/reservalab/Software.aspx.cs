using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibraryTesis;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using ClassLibraryLaboratorios;
using ClassLibraryTesis;
using System.Web.Services;

public partial class academic_private_reservaLab_Software : System.Web.UI.Page
{
    LAB_SOFTWARE software1 = new LAB_SOFTWARE();
    UB_SEDES sede = new UB_SEDES();
    UB_FACULTADES facultad = new UB_FACULTADES();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Context.User.Identity.Name == null) Response.Redirect("~/academic/private/Login.aspx");

        if (!IsPostBack)
        {
            cargarSede();
            cargarFacultad();
            cargarSoftware();
        }
    }

    protected void ddlSedeSoft_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlFacultadSoft.Items.Clear();
        cargarFacultad();
        cargarSoftware();
    }

    protected void ddlFacultadSoft_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvSoftware.DataSource = null;
        cargarSoftware();
    }

    protected void gvSoftware_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSoftware.PageIndex = e.NewPageIndex;
    }

    public void cargarSede()
    {
        var listSede = sede.LoadUB_SEDES("ALL", "", "", "", "");
        if (listSede.Count != 0)
        {
            //ddlSede de listar Software
            ddlSedeSoft.DataSource = listSede;
            ddlSedeSoft.DataTextField = "strNombre_Sede";
            ddlSedeSoft.DataValueField = "strCod_Sede";
            ddlSedeSoft.DataBind();

            lblMsg.Text = sede.msg;
        }
        else
        {
            lblMsg.Text = sede.msg;
        }
    }

    public void cargarFacultad()
    {
        string strCod_Sede = ddlSedeSoft.SelectedValue;
        var listFacultad = facultad.LoadUB_FACULTADES("xPKSede", strCod_Sede, "", "", "");

        if (listFacultad.Count != 0)
        {
            ddlFacultadSoft.DataSource = listFacultad;
            ddlFacultadSoft.DataTextField = "strNombre_fac";
            ddlFacultadSoft.DataValueField = "strCod_fac";
            ddlFacultadSoft.DataBind();

            lblMsg.Text = software1.msg;
        }
        else
        {
            lblMsg.Text = software1.msg;
        }
    }

    public void cargarSoftware()
    {
        //lblMsgLstUsuario.Visible = false;
        string strCod_Fac = ddlFacultadSoft.SelectedValue;
        string strCod_Sede = ddlSedeSoft.SelectedValue;

        var listSoftware = software1.LoadLAB_SOFTWARE("xSedeFacultad", strCod_Fac, strCod_Sede, "", "");

        if (listSoftware.Count != 0)
        {
            gvSoftware.DataSource = listSoftware;
            gvSoftware.DataBind();
            lblMsg.Text = software1.msg;
        }
        else
        {
            lblMsg.Text = software1.msg;
        }
    }
    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/private/reservalab/NuevoSoftware.aspx");
    }

    protected void gvSoftware_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            Session["codSoft"] = e.CommandArgument.ToString();
            Response.Redirect("~/academic/private/reservalab/ActualizarSoftware.aspx");
        }
        if (e.CommandName == "Eliminar")
        {
            string codSftware = e.CommandArgument.ToString();

            // Carga los detalles del laboratorio según el ID seleccionado 
            string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string responsable = Context.User.Identity.Name;
            int eliminar = software1.DelLAB_SOFTWARE("xCodSof", codSftware, fecha, responsable, "");

            string title = eliminar != -1 ? "Registro eliminado correctamente." : "Registro no eliminado.";
            string icon = eliminar != -1 ? "success" : "error";

            string script = $"showAlertAndReload('{title}', '{icon}');";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowAlert", script, true);
        }
    }

    protected void btnVistaCompleta_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        string rutaImagen = btn.CommandArgument;
        vistaCompletaImagen.ImageUrl = rutaImagen;
        ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "$('#view-image').modal('show');", true);
    }
}
