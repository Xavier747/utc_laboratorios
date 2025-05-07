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

        //if (Session["Cedula"] == null) Response.Redirect("~/Views/Login.aspx");

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

    protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        //    content_NombreLicencia.Visible = ddlTipo.SelectedValue == "Propietario" ? true : false;
        //    content_NombreLicenciaAct.Visible = ddlTipoAct.SelectedValue == "Propietario" ? true : false;
    }

    //public void llenarFormActualizar()
    //{
    //    lblIdSoftAct.Text = software1.strCod_sof;
    //    txtNombreAct.Text = software1.strNombre_sof;
    //    ddlTipoAct.SelectedValue = software1.strTipoLicencia_sof;

    //    content_NombreLicenciaAct.Visible = software1.strTipoLicencia_sof == "Propietario" ? true : false;

    //    txtNombreLicenciaAct.Text = software1.strNombreLicencia_sof;
    //    txtCantidadAct.Text = software1.intCantidad_sof.ToString();
    //    txtCostoAct.Text = software1.decCostoUnitario_sof.ToString();
    //    txtDescripcionAct.Text = software1.strDescripcion_sof;
    //    txtLinkAct.Text = software1.strUrl_sof;
    //    lblImg1NameAct.Text = software1.strImagen_sof;
    //    ddlSedeAct.SelectedValue = software1.strCod_Sede;
    //    //cargarFacultad();
    //    ddlFacultadAct.SelectedValue = software1.strCod_Fac;
    //}

    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        //    precioUnitario = txtCostoAct.Text != "" ? decimal.Parse(txtCostoAct.Text) : 0;

        //    software1.strNombre_sof = txtNombreAct.Text;
        //    software1.strTipoLicencia_sof = ddlTipoAct.SelectedValue;
        //    software1.strNombreLicencia_sof = txtNombreLicenciaAct.Text;
        //    software1.intCantidad_sof = int.Parse(txtCantidadAct.Text);
        //    software1.decCostoUnitario_sof = precioUnitario;
        //    software1.decCostoTotal_sof = decimal.Parse(txtCantidadAct.Text) * precioUnitario;
        //    software1.strDescripcion_sof = txtDescripcionAct.Text;
        //    software1.strUrl_sof = txtLinkAct.Text;
        //    software1.strCod_sof = lblIdSoftAct.Text;
        //    software1.dtFecha_log = DateTime.Now;
        //    software1.strUser_log = Session["Cedula"].ToString();

        //    if (fulImg1Act.HasFile)
        //    {
        //        try
        //        {
        //            string folderPath = Server.MapPath("~/images/Software/");
        //            string filename = Path.GetFileNameWithoutExtension(fulImg1Act.FileName);
        //            string extension = Path.GetExtension(fulImg1Act.FileName);
        //            string newFilename = filename + extension;
        //            string path = Path.Combine(folderPath, newFilename);

        //            // Verificar si el archivo existe y agregar un sufijo numérico
        //            int counter = 1;
        //            while (File.Exists(path))
        //            {
        //                newFilename = $"{filename}_{counter}{extension}";
        //                path = Path.Combine(folderPath, newFilename);
        //                counter++;
        //            }

        //            fulImg1Act.SaveAs(path);
        //            lblImg1NameAct.Text = newFilename; // Guarda el nombre actualizado en la base de datos
        //        }
        //        catch (Exception ex)
        //        {
        //            Response.Write("La carga falló: " + ex.Message);
        //        }
        //    }
        //    software1.strImagen_sof = lblImg1NameAct.Text;
        //    software1.strCod_Fac = ddlFacultadAct.SelectedValue;
        //    software1.strCod_Sede = ddlSedeAct.SelectedValue;

        //    //bool actualizar = software1.actualizarSoftware();

        //    //title = actualizar == true ? "Los datos se han actualizado correctamente." : "Los datos no se han actualizado correctamente.";
        //    //icon = actualizar == true ? "success" : "error";

        //    string script = $"showAlertAndReload('{title}', '{icon}');";
        //    ClientScript.RegisterStartupScript(this.GetType(), "ShowAlert", script, true);
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
            //software1.strCod_sof = e.CommandArgument.ToString();

            //// Carga los detalles del laboratorio según el ID seleccionado 
            //string comodin = "xCodSof";
            //software1.dtFecha_log = DateTime.Now;
            //software1.strUser_log = Session["Cedula"].ToString();
            ////bool eliminar = software1.eliminarSoftware(comodin);

            ////title = eliminar == true ? "Registro eliminado correctamente." : "Registro no eliminado.";
            ////icon = eliminar == true ? "success" : "error";

            //string script = $"showAlertAndReload('{title}', '{icon}');";
            //ClientScript.RegisterStartupScript(this.GetType(), "ShowAlert", script, true);
        }
    }

    protected void btnVistaCompleta_Click(object sender, ImageClickEventArgs e)
    {
        //    //ImageButton btn = (ImageButton)sender;
        //    //string rutaImagen = btn.CommandArgument;
        //    //vistaCompletaImagen.ImageUrl = rutaImagen;
        //    //ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "$('#view-image').modal('show');", true);
    }

    protected void ddlSede_SelectedIndexChanged(object sender, EventArgs e)
    {
        //cargarFacultad();
    }

}
