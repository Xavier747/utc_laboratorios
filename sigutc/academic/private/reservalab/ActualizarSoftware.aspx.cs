using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibraryLaboratorios;
using ClassLibraryTesis;
using System.IO;

public partial class academic_private_reservalab_ActualizarSoftware : System.Web.UI.Page
{
    LAB_SOFTWARE software1 = new LAB_SOFTWARE();
    UB_SEDES sede = new UB_SEDES();
    UB_FACULTADES facultad = new UB_FACULTADES();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cargarTipoLicencia();
            cargarSede();
            cargarFacultad();
            consultarRegistro();
        }
    }

    protected void ddlSedeAct_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlFacultadAct.Items.Clear();
    }

    public void cargarTipoLicencia()
    {
        ddlTipoAct.Items.Add(new ListItem("Propietario", "Propietario"));
        ddlTipoAct.Items.Add(new ListItem("Libre", "Libre"));

        ddlTipoAct.Visible = ddlTipoAct.SelectedValue == "Propietario" ? true : false;
    }

    public void cargarSede()
    {
        var listSede = sede.LoadUB_SEDES("ALL", "", "", "", "");
        if (listSede.Count != 0)
        {
            //ddlSedeAct
            ddlSedeAct.DataSource = listSede;
            ddlSedeAct.DataTextField = "strNombre_Sede";
            ddlSedeAct.DataValueField = "strCod_Sede";
            ddlSedeAct.DataBind();

            lblMsg.Text = sede.msg;
        }
        else
        {
            lblMsg.Text = sede.msg;
        }
    }

    public void consultarRegistro()
    {
        string codLab = Session["codSoft"].ToString();

        var registroLab = software1.LoadLAB_SOFTWARE("xPK", codLab, "", "", "");
    }

    public void cargarFacultad()
    {
        string strCod_Sede = ddlSedeAct.SelectedValue;
        var listFacultad = facultad.LoadUB_FACULTADES("xPKSede", strCod_Sede, "", "", "");

        if (listFacultad.Count != 0)
        {
            ddlFacultadAct.DataSource = listFacultad;
            ddlFacultadAct.DataTextField = "strNombre_fac";
            ddlFacultadAct.DataValueField = "strCod_fac";
            ddlFacultadAct.DataBind();

            lblMsg.Text = software1.msg;
        }
        else
        {
            lblMsg.Text = software1.msg;
        }
    }

    protected void ddlTipoAct_SelectedIndexChanged(object sender, EventArgs e)
    {
        content_NombreLicenciaAct.Visible = ddlTipoAct.SelectedValue == "Propietario" ? true : false;
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/academic/private/reservalab/Software.aspx");
    }

    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        string rutaCarpeta = @"C:\images\Laboratorios";
        decimal precioUnitario = txtCostoAct.Text != "" ? decimal.Parse(txtCostoAct.Text) : 0;

        software1.strNombre_sof = txtNombreAct.Text;
        software1.strTipoLicencia_sof = ddlTipoAct.SelectedValue;
        software1.strNombreLicencia_sof = txtNombreLicenciaAct.Text;
        software1.intCantidad_sof = int.Parse(txtCantidadAct.Text);
        software1.decCostoUnitario_sof = precioUnitario;
        software1.decCostoTotal_sof = decimal.Parse(txtCantidadAct.Text) * precioUnitario;
        software1.strDescripcion_sof = txtDescripcionAct.Text;
        software1.strUrl_sof = txtLinkAct.Text;
        software1.strCod_sof = lblIdSoftAct.Text;
        software1.dtFecha_log = DateTime.Now;
        software1.strUser_log = Session["Cedula"].ToString();

        if (fulImg1Act.HasFile)
        {
            try
            {
                string filename = Path.GetFileNameWithoutExtension(fulImg1Act.FileName);
                string extension = Path.GetExtension(fulImg1Act.FileName);
                string newFilename = filename + extension;
                string path = Path.Combine(rutaCarpeta, newFilename);

                // Verificar si el archivo existe y agregar un sufijo numérico
                int counter = 1;
                while (File.Exists(path))
                {
                    newFilename = $"{filename}_{counter}{extension}";
                    path = Path.Combine(rutaCarpeta, newFilename);
                    counter++;
                }

                fulImg1Act.SaveAs(path);
                lblImg1NameAct.Text = path; // Guarda el nombre actualizado en la base de datos
            }
            catch (Exception ex)
            {
                Response.Write("La carga falló: " + ex.Message);
            }
        }

        software1.strImagen_sof = lblImg1NameAct.Text;
        software1.strCod_Fac = ddlFacultadAct.SelectedValue;
        software1.strCod_Sede = ddlSedeAct.SelectedValue;

        int actualizar = software1.UpdateLAB_SOFTWARE(software1);

        string title = actualizar != -1 ? software1.msg : software1.msg;
        string icon = actualizar != -1 ? "success" : "error";
        string url = "Software.aspx";

        string script = $"showAlertAndReload('{title}', '{icon}', '{url}');";
        ClientScript.RegisterStartupScript(this.GetType(), "ShowAlert", script, true);
    }

}